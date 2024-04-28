using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.UnitOfWork.Implementations
{
    public class TestUnitOfWork : GenericUnitOfWork<Test>, ITestUnitOfWork
    {
        private readonly ITestRepository _testRepository;

        public TestUnitOfWork(IGenericRepository<Test> repository, ITestRepository testRepository) : base(repository)
        {
            _testRepository = testRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Test>>> GetAsync() => await _testRepository.GetAsync();

        public override async Task<ActionResponse<Test>> GetAsync(int id) => await _testRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<Test>>> GetAsync(PagingDTO paging) => await _testRepository.GetAsync(paging);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging) => await _testRepository.GetTotalPagesAsync(paging);
    }
}
