using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.Repository.Interfaces
{
    public interface ICitiesRepository
    {
        Task<ActionResponse<IEnumerable<City>>> GetAsync();

        Task<ActionResponse<City>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<City>>> GetAsync(PagingDTO paging);

        Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO pagination);
    }
}