using LabPreTest.Backend.Data;
using LabPreTest.Backend.Helpers;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace LabPreTest.Backend.Repository.Implementations
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Order>>> GetAsync()
        {
            var order = await _context.Orders
                .OrderBy(x => x.Id)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Order>>
            {
                WasSuccess = true,
                Result = order
            };
        }

        public override async Task<ActionResponse<Order>> GetAsync(int id)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return new ActionResponse<Order>
                {
                    WasSuccess = false,
                    Message = MessageStrings.DbCountryNotFoundMessage
                };
            }

            return new ActionResponse<Order>
            {
                WasSuccess = true,
                Result = order
            };
        }

        public override async Task<ActionResponse<IEnumerable<Order>>> GetAsync(PagingDTO paging)
        {
            var queryable = _context.Orders
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Id.ToString().Contains(paging.Filter.ToLower()));

            return new ActionResponse<IEnumerable<Order>>
            {
                WasSuccess = true,
                Result = await queryable
                .OrderBy(x => x.Id)
                .Paginate(paging)
                .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging)
        {
            var queryable = _context.Orders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Id.ToString().Contains(paging.Filter.ToLower()));

            int count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling((double)count / paging.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };

        }

    }
}