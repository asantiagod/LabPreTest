using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.Repository.Interfaces
{
    public interface IOrdersRepository
    {
        Task<ActionResponse<IEnumerable<Order>>> GetAsync(string email, PagingDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(string email, PagingDTO pagination);

        Task<ActionResponse<Order>> GetAsync(int id);

        Task<ActionResponse<Order>> UpdateFullAsync(string email, OrderDTO orderDTO);

        Task<ActionResponse<OrderDetailDTO>> UpdateAsync(string email, int detailId, OrderDetailDTO orderDetailDTO);
    }
}