using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.Repository.Interfaces
{
    public interface ITestRepository
    {
        Task<ActionResponse<IEnumerable<Test>>> GetAsync();

        Task<ActionResponse<Test>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Test>>> GetAsync(PagingDTO paging);

        Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO pagination);

        Task<ActionResponse<TestDTO>> AddAsync(TestDTO testDTO);

        Task<ActionResponse<TestDTO>> UpdateAsync(int id, TestDTO testDTO);

        Task<ActionResponse<Test>> DeleteAsync(int id);
    }
}