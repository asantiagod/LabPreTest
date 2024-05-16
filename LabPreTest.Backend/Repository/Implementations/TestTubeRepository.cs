using LabPreTest.Backend.Data;
using LabPreTest.Backend.Helpers;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace LabPreTest.Backend.Repository.Implementations
{
    public class TestTubeRepository : GenericRepository<TestTube>, ITestTubeRepository
    {
        private readonly DataContext _context;

        public TestTubeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<TestTube>>> GetAsync()
        {
            var testTubes = await _context.TestTubes
                .OrderBy(t => t.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<TestTube>>
            {
                WasSuccess = true,
                Result = testTubes
            };
        }

        public override async Task<ActionResponse<TestTube>> GetAsync(int id)
        {
            var testTube = await _context.TestTubes
                        .FirstOrDefaultAsync(c => c.Id == id);
            if (testTube == null)
            {
                return new ActionResponse<TestTube>
                {
                    WasSuccess = false,
                    Message = MessageStrings.DbCountryNotFoundMessage
                };
            }

            return new ActionResponse<TestTube>
            {
                WasSuccess = true,
                Result = testTube
            };
        }

        public override async Task<ActionResponse<IEnumerable<TestTube>>> GetAsync(PagingDTO paging)
        {
            var queryable = _context.TestTubes
                 .AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            return new ActionResponse<IEnumerable<TestTube>>
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
            var queryable = _context.TestTubes.AsQueryable();

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