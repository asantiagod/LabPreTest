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

        public async Task<ActionResponse<TestDTO>> AddAsync(TestDTO testDTO)
        {
            var section = _context.Section.FirstOrDefault(s => s.Id == testDTO.SectionID);
            var testTube = _context.TestTubes.FirstOrDefault(t => t.Id == testDTO.TestTubeID);
            ICollection<PreanalyticCondition> conditions = await _context.PreanalyticConditions
               .Where(c => testDTO.Conditions.Contains(c.Id))
               .ToListAsync();

            if (conditions.Count != testDTO.Conditions.Count)
                return ActionResponse<TestDTO>.BuildFailed(MessageStrings.DbParameterNotFoundMessage);

            var test = new Test
            {
                TestID = testDTO.TestID,
                Name = testDTO.Name,
                Section = section!,
                TestTube = testTube!,
                Conditions = conditions
            };

            _context.Tests.Add(test);
            return await SaveChangesAsync(testDTO);
        }

        public async Task<ActionResponse<TestDTO>> UpdateAsync(int id, TestDTO testDTO)
        {
            ICollection<PreanalyticCondition> conditions = await _context.PreanalyticConditions
               .Where(c => testDTO.Conditions.Contains(c.Id))
               .ToListAsync();

            if (conditions.Count != testDTO.Conditions.Count)
                return ActionResponse<TestDTO>.BuildFailed(MessageStrings.DbParameterNotFoundMessage);

            var test = await _context.Tests
                       .Include(x =>  x.Conditions)
                       .FirstOrDefaultAsync(x => x.Id == id);
            if (test == null)
                return ActionResponse<TestDTO>.BuildFailed(MessageStrings.DbRecordNotFoundMessage);
            test.TestID = testDTO.TestID;
            test.Name = testDTO.Name;
            test.Conditions?.Clear(); // Clear old relationships
            test.Conditions = conditions;
            if (test.SectionId != testDTO.SectionID)
            {
                var section = _context.Section.FirstOrDefault(s => s.Id == testDTO.SectionID);
                if (section == null)
                    return ActionResponse<TestDTO>.BuildFailed(MessageStrings.DbParameterNotFoundMessage);
                test.Section = section;
            }
            if (test.TestTubeId != testDTO.TestTubeID)
            {
                var tube = _context.TestTubes.FirstOrDefault(t => t.Id == testDTO.TestTubeID);
                if (tube == null)
                    return ActionResponse<TestDTO>.BuildFailed(MessageStrings.DbParameterNotFoundMessage);
                test.TestTube = tube;
            }

            _context.Update(test);
            return await SaveChangesAsync(testDTO);
        }

        public override async Task<ActionResponse<Test>> DeleteAsync(int id)
        {
            var test = await _context.Tests.Include(t => t.Conditions).FirstOrDefaultAsync(t => t.Id == id);
            if (test == null)
                return ActionResponse<Test>.BuildFailed(MessageStrings.DbRecordNotFoundMessage);

            test.Conditions?.Clear();
            _context.Tests.Remove(test);
            return await SaveChangesAsync(test);
        }

        public override async Task<ActionResponse<IEnumerable<Test>>> GetAsync()
        {
            var test = await _context.Tests
                .OrderBy(x => x.Name)
                .Include(t => t.Section)
                .ThenInclude(s => s.SectionImages)
                .ToListAsync();
            return ActionResponse<IEnumerable<Test>>.BuildSuccessful(test);
        }

        public override async Task<ActionResponse<Test>> GetAsync(int id)
        {
            var test = await _context.Tests
                .Include(x => x.TestTube)
                .Include(x => x.Section)
                .ThenInclude(s => s.SectionImages)
                .Include(x => x.Conditions!)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (test == null)
                return ActionResponse<Test>.BuildFailed(MessageStrings.DbTestNotFoundMessage);

            return ActionResponse<Test>.BuildSuccessful(test);
        }

        public override async Task<ActionResponse<IEnumerable<Test>>> GetAsync(PagingDTO paging)
        {
            var queryable = _context.Tests
                .Include(t => t.Section)
                .ThenInclude(s => s.SectionImages)
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

        private async Task<ActionResponse<T>> SaveChangesAsync<T>(T model,
                                                                  string errorMessage = MessageStrings.DbUpdateExceptionMessage)
        {
            try
            {
                await _context.SaveChangesAsync();
                return ActionResponse<T>.BuildSuccessful(model);
            }
            catch (DbUpdateException)
            {
                return ActionResponse<T>.BuildFailed(errorMessage);
            }
            catch (Exception ex)
            {
                return ActionResponse<T>.BuildFailed(ex.Message);
            }
        }
    }
}