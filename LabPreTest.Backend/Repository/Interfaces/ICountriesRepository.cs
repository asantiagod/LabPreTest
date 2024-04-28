using LabPreTest.Shared.Entities;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.Repository.Interfaces
{
    public interface ICountriesRepository
    {
        Task<ActionResponse<IEnumerable<Country>>> GetAsync();

        Task<ActionResponse<Country>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Country>>> GetAsync(PagingDTO paging);

        Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO pagination); 
    }
}
