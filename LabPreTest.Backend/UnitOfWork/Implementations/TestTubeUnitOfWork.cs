using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.UnitOfWork.Implementations
{
    public class TestTubeUnitOfWork : GenericUnitOfWork<TestTube>, ITestTubeUnitOfWork
    {
        private readonly ITestTubeRepository _testTubeRepository;

        public TestTubeUnitOfWork(IGenericRepository<TestTube> repository, ITestTubeRepository testTubeRepository) : base(repository)
        {
            _testTubeRepository = testTubeRepository;
        }

        public override async Task<ActionResponse<IEnumerable<TestTube>>> GetAsync() => await _testTubeRepository.GetAsync();

        public override async Task<ActionResponse<TestTube>> GetAsync(int id) => await _testTubeRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<TestTube>>> GetAsync(PagingDTO paging) => await _testTubeRepository.GetAsync(paging);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO pagination) => await _testTubeRepository.GetTotalPagesAsync(pagination);
    }
}