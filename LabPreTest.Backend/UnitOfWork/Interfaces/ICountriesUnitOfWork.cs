using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;
using LabPreTest.Shared.DTO;

namespace LabPreTest.Backend.UnitOfWork.Interfaces
{
    public interface ICountriesUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Country>>> GetAsync();

        Task<ActionResponse<Country>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Country>>> GetAsync(PagingDTO paging);

        Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO pagination);

        Task<IEnumerable<Country>> GetComboAsync();
    }
}