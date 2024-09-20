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
    public class TestRepository : GenericRepository<Test>, ITestRepository
    {
        private readonly DataContext _context;

        public TestRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Test>>> GetAsync()
        {
            var test = await _context.Tests
                .OrderBy(x => x.Name)
                .ToListAsync();
            return ActionResponse<IEnumerable<Test>>.BuildSuccessful(test);
        }

        public override async Task<ActionResponse<Test>> GetAsync(int id)
        {
            var test = await _context.Tests
                .Include(x => x.TestTube)
                .Include(x => x.Section)
                .Include(x => x.Conditions!)
                .ThenInclude(c => c.Condition)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (test == null)
                return ActionResponse<Test>.BuildFailed(MessageStrings.DbTestNotFoundMessage);

            return ActionResponse<Test>.BuildSuccessful(test);
        }

        public override async Task<ActionResponse<IEnumerable<Test>>> GetAsync(PagingDTO paging)
        {
            var queryable = _context.Tests
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            var result = await queryable
                .OrderBy(x => x.Name)
                .Paginate(paging)
                .ToListAsync();
            return ActionResponse<IEnumerable<Test>>.BuildSuccessful(result);
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging)
        {
            var queryable = _context.Tests.AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            int count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling((double)count / paging.RecordsNumber);
            return ActionResponse<int>.BuildSuccessful(totalPages);
        }
    }
}