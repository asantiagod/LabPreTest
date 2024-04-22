using LabPreTest.Backend.Data;
using LabPreTest.Backend.Repository.Interfaces;
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
    }
}