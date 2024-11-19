using LabPreTest.Backend.Data;
using LabPreTest.Backend.Helpers;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Enums;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace LabPreTest.Backend.Repository.Implementations
{
    public class OrdersRepository : GenericRepository<Order>, IOrdersRepository
    {
        private readonly DataContext _context;
        private readonly IUsersRepository _usersRepository;

        public OrdersRepository(DataContext context, IUsersRepository usersRepository) : base(context)
        {
            _context = context;
            _usersRepository = usersRepository;
        }

        public async Task<ActionResponse<IEnumerable<Order>>> GetAsync(string email, PagingDTO pagination)
        {
            var user = await _usersRepository.GetUserAsync(email);
            if (user == null)
                return ActionResponse<IEnumerable<Order>>.BuildFailed("Usuario no valido");

            var queryable = _context.Orders
                .Include(o => o.Details!)
                .AsQueryable();

            var isAdmin = await _usersRepository.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
                queryable = queryable.Where(o => o.User!.Email == email);

            var result = await queryable
                .OrderBy(x => x.Id)
                .Paginate(pagination)
                .Select(o => new Order
                {
                    Id = o.Id,
                    CreatedAt = o.CreatedAt,
                    Status = o.Status,
                    UserId = o.UserId,
                    Details = o.Details,
                })
                .ToListAsync();
            return ActionResponse<IEnumerable<Order>>.BuildSuccessful(result);
        }

        public async Task<ActionResponse<int>> GetTotalPagesAsync(string email, PagingDTO pagination)
        {
            var user = await _usersRepository.GetUserAsync(email);
            if (user == null)
                return ActionResponse<int>.BuildFailed("Usuario no valido");

            var queryable = _context.Orders.AsQueryable();

            var isAdmin = await _usersRepository.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
                queryable = queryable.Where(o => o.User!.Email == email);

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);

            return ActionResponse<int>.BuildSuccessful((int)totalPages);
        }

        public override async Task<ActionResponse<Order>> GetAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Details!)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return ActionResponse<Order>.BuildFailed("La orden no existe");

            return ActionResponse<Order>.BuildSuccessful(order);
        }

        //TODO: check if is necessary delete this method
        public async Task<ActionResponse<Order>> UpdateFullAsync(string email, OrderDTO orderDTO)
        {
            if (!await HavePermissionsAsync(email))
                return ActionResponse<Order>.BuildFailed(MessageStrings.UserDoesNotHavePermissions);

            var order = await _context.Orders
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.Id != orderDTO.Id);
            if (order == null)
                return ActionResponse<Order>.BuildFailed("La orden no existe");

            order.Status = orderDTO.Status;
            _context.Update(order);
            await _context.SaveChangesAsync();

            return ActionResponse<Order>.BuildSuccessful(order);
        }

        public async Task<ActionResponse<OrderDetailDTO>> UpdateAsync(string email, int detailId, OrderDetailDTO orderDetailDTO)
        {
            if (!await HavePermissionsAsync(email))
                return ActionResponse<OrderDetailDTO>.BuildFailed(MessageStrings.UserDoesNotHavePermissions);
            var order = await _context.Orders
                .Include(o => o.Details)
                .FirstOrDefaultAsync(o => o.Id == orderDetailDTO.OrderId);
            if (order == null || order.Details == null)
                return ActionResponse<OrderDetailDTO>.BuildFailed(MessageStrings.DbParameterNotFoundMessage);

            var newDetail = order.Details.FirstOrDefault(d => d.Id == detailId);
            if (newDetail == null)
                return ActionResponse<OrderDetailDTO>.BuildFailed(MessageStrings.DbParameterNotFoundMessage);

            if (orderDetailDTO.TestId.HasValue)
            {
                var newTest = await _context.Tests.FirstOrDefaultAsync(t => t.Id == orderDetailDTO.TestId);
                if (newTest == null)
                    return ActionResponse<OrderDetailDTO>.BuildFailed(MessageStrings.DbRecordNotFoundMessage);
                newDetail.Test = newTest;
            }

            if (orderDetailDTO.MedicId.HasValue)
                if (!await UpdateMedicAsync(order, (int)orderDetailDTO.MedicId))
                    return ActionResponse<OrderDetailDTO>.BuildFailed(MessageStrings.DbRecordNotFoundMessage);

            if (orderDetailDTO.PatientId.HasValue)
                if (!await UpdatePatientAsync(order, (int)orderDetailDTO.PatientId))
                    return ActionResponse<OrderDetailDTO>.BuildFailed(MessageStrings.DbRecordNotFoundMessage);

            if (orderDetailDTO.Status.HasValue)
                newDetail.Status = orderDetailDTO.Status;

            int nTestsInProcess = 0;
            int nTestsClosed = 0;
            int nTestsCanceled = 0;
            foreach (var d in order.Details)
            {
                switch (d.Status)
                {
                    case OrderStatus.OrdenEnProceso:
                        nTestsInProcess++;
                        break;

                    case OrderStatus.OrdenFinalizada:
                        nTestsClosed++;
                        break;

                    case OrderStatus.OrdenAnulada:
                        nTestsCanceled++;
                        break;
                }
            }
            
            var nTestProcessed = nTestsClosed + nTestsCanceled;
            if (nTestProcessed == order.Details.Count)
            {
                if (nTestsCanceled == order.Details.Count)
                    order.Status = OrderStatus.OrdenAnulada;
                else
                    order.Status = OrderStatus.OrdenFinalizada;
            }
            else if (nTestsInProcess > 0 || nTestProcessed > 0)
                order.Status = OrderStatus.OrdenEnProceso;
            else
                order.Status = OrderStatus.OrdenCreada;

            _context.Orders.Update(order);
            return await SaveContextChangesAsync<OrderDetailDTO>(orderDetailDTO);
        }

        private async Task<bool> HavePermissionsAsync(string email)
        {
            var user = await _usersRepository.GetUserAsync(email);
            if (user == null)
                return false;

            var isAdmin = await _usersRepository.IsUserInRoleAsync(user, UserType.Admin.ToString());
            var isUser = await _usersRepository.IsUserInRoleAsync(user, UserType.User.ToString());
            if (!isAdmin && !isUser)
                return false;
            return true;
        }

        private async Task<bool> UpdateMedicAsync(Order order, int medicId)
        {
            var medic = await _context.Medicians.FirstOrDefaultAsync(m => m.Id == medicId);
            if (medic == null)
                return false;

            if (order.Details != null)
                foreach (var detail in order.Details)
                    detail.Medic = medic;

            return true;
        }

        private async Task<bool> UpdatePatientAsync(Order order, int patientId)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(m => m.Id == patientId);
            if (patient == null)
                return false;

            if (order.Details != null)
                foreach (var detail in order.Details)
                    detail.Patient = patient;

            return true;
        }
    }
}