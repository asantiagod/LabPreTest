using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Interfaces;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.Repository.Interfaces
{
    public interface IGenericAuditRepository<T> where T : class, IAuditRecord
    {
        Task<ActionResponse<IEnumerable<T>>> GetAsync();

        Task<ActionResponse<T>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<T>>> GetAsync(PagingDTO pagingDTO);

        Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO pagination);
    }
}