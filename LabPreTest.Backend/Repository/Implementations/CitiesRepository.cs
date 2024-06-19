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
            return ActionResponse<IEnumerable<City>>.BuildSuccessful(cities);
        }

        public async Task<IEnumerable<City>> GetComboAsync(int stateId)
        {
            return await _context.Cities
                .Where(s => s.StateId == stateId)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public override async Task<ActionResponse<City>> GetAsync(int id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);

            if (city == null)
                return ActionResponse<City>.BuildFailed(MessageStrings.DbCityNotFoundMessage);

            return ActionResponse<City>.BuildSuccessful(city);
        }

        public override async Task<ActionResponse<IEnumerable<City>>> GetAsync(PagingDTO paging)
        {
            var queryable = _context.Cities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            var result = await queryable
                            .OrderBy(x => x.Name)
                            .Paginate(paging)
                            .ToListAsync();
            return ActionResponse<IEnumerable<City>>.BuildSuccessful(result);
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging)
        {
            var queryable = _context.Cities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            int count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling((double)count / paging.RecordsNumber);
            return ActionResponse<int>.BuildSuccessful(totalPages);
        }
    }
}