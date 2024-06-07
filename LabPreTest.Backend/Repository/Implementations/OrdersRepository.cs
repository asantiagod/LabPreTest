using LabPreTest.Backend.Data;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
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
            {
                return new ActionResponse<IEnumerable<Order>>
                {
                    WasSuccess = false,
                    Message = "Usuario no valido"
                };
            }

            //var query = _context.Orders
            //    .Include(o => o.User)

            throw new NotImplementedException();
        }

        public Task<ActionResponse<int>> GetTotalPagesAsync(string email, PagingDTO pagination)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResponse<Order>> UpdateFullAsync(string email, OrderDTO orderDTO)
        {
            throw new NotImplementedException();
        }
    }
}