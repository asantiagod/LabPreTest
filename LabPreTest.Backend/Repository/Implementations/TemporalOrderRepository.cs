using LabPreTest.Backend.Data;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace LabPreTest.Backend.Repository.Implementations
{
    public class TemporalOrderRepository : GenericRepository<TemporalOrder>, ITemporalOrdersRepository
    {
        private readonly DataContext _context;
        private readonly IUsersRepository _usersRepository;

        public TemporalOrderRepository(DataContext context, IUsersRepository usersRepository) : base(context)
        {
            _context = context;
            _usersRepository = usersRepository;
        }

        public async Task<ActionResponse<TemporalOrdersDTO>> AddFullAsync(string email, TemporalOrdersDTO temporalOrderDTO)
        {
            var actionResponse = await CheckRelatedEntitiesAsync(email, temporalOrderDTO);
            if (!actionResponse.WasSuccess)
                return ActionResponse<TemporalOrdersDTO>.BuildFailed(actionResponse.Message!);

            var temporalOrder = actionResponse.Result!;
            _context.Add(temporalOrder);

            return await SaveContextChangesAsync(temporalOrderDTO);
        }

        public async Task<ActionResponse<TemporalOrdersDTO>> UpdateAsync(string email, TemporalOrdersDTO temporalOrderDTO)
        {
            var tempOrder = await _context.TemporalOrders.FirstOrDefaultAsync(to => to.Id == temporalOrderDTO.Id);
            if (tempOrder == null)
                return ActionResponse<TemporalOrdersDTO>.BuildFailed(MessageStrings.DbRecordNotFoundMessage);

            var actionResponse = await CheckRelatedEntitiesAsync(email, temporalOrderDTO);
            if (!actionResponse.WasSuccess)
                return ActionResponse<TemporalOrdersDTO>.BuildFailed(actionResponse.Message!);

            var to = actionResponse.Result!;
            tempOrder.User = to.User;
            tempOrder.Test = to.Test;
            tempOrder.Medic = to.Medic;
            tempOrder.Patient = to.Patient;
            _context.Update(tempOrder);

            return await SaveContextChangesAsync(temporalOrderDTO);
        }

        public async Task<ActionResponse<IEnumerable<TemporalOrder>>> GetAsync(string email)
        {
            var temporalOrders = await _context.TemporalOrders
                .Where(to => to.User!.Email == email)
                .ToListAsync();

            return ActionResponse<IEnumerable<TemporalOrder>>.BuildSuccessful(temporalOrders);
        }

        public async Task<ActionResponse<int>> GetCountAsync(string email)
        {
            var count = await _context.TemporalOrders
                .Where(x => x.User!.Email == email)
                .CountAsync();
            return ActionResponse<int>.BuildSuccessful(count);
        }

        private async Task<ActionResponse<TemporalOrder>> CheckRelatedEntitiesAsync(string email, TemporalOrdersDTO temporalOrderDTO)
        {
            var test = await _context.Tests.FirstOrDefaultAsync(t => t.Id == temporalOrderDTO.TestId);
            if (test == null)
                return ActionResponse<TemporalOrder>.BuildFailed("El examen no existe");

            var medician = await _context.Medicians.FirstOrDefaultAsync(m => m.Id == temporalOrderDTO.MedicId);
            if (medician == null)
                return ActionResponse<TemporalOrder>.BuildFailed("El medico no existe");

            var patient = await _context.Patients.FirstOrDefaultAsync(m => m.Id == temporalOrderDTO.PatientId);
            if (patient == null)
                return ActionResponse<TemporalOrder>.BuildFailed("El paciente no existe");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return ActionResponse<TemporalOrder>.BuildFailed("El usuario no existe");

            return ActionResponse<TemporalOrder>.BuildSuccessful(new TemporalOrder
            {
                User = user,
                Test = test,
                Medic = medician,
                Patient = patient
            });
        }
    }
}