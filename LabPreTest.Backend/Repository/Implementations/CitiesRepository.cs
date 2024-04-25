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
    public class CitiesRepository : GenericRepository<City>, ICitiesRepository
    {
        private readonly DataContext _context;

        public CitiesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<City>>> GetAsync()
        {
            var cities = await _context.Cities
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<City>>
            {
                WasSuccess = true,
                Result = cities
            };
        }

        public override async Task<ActionResponse<City>> GetAsync(int id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);

            if (city == null)
            {
                return new ActionResponse<City>
                {
                    WasSuccess = false,
                    Message = MessageStrings.DbCityNotFoundMessage,
                };
            }

            return new ActionResponse<City>
            {
                WasSuccess = true,
                Result = city
            };
        }

        public override async Task<ActionResponse<IEnumerable<City>>> GetAsync(PagingDTO paging)
        {
            var queryable = _context.Cities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            return new ActionResponse<IEnumerable<City>>
            {
                WasSuccess = true,
                Result = await queryable
                .OrderBy(x => x.Name)
                .Paginate(paging)
                .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging)
        {
            var queryable = _context.Cities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

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