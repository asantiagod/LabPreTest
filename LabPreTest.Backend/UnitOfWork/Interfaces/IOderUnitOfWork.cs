using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.UnitOfWork.Interfaces
{
    public interface IOrderUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Order>>> GetAsync();

        Task<ActionResponse<Order>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Order>>> GetAsync(PagingDTO paging);

        Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO pagination);
    }
}
