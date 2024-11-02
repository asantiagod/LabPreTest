using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.UnitOfWork.Interfaces
{
    public interface IGenericAuditUnitOfWork<T> where T : class
    {
        Task<ActionResponse<IEnumerable<T>>> GetAsync();

        Task<ActionResponse<T>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<T>>> GetAsync(PagingDTO pagingDTO);

        Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO pagination);
    }
}