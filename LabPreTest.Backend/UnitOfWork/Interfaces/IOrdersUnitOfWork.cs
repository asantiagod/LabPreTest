using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.UnitOfWork.Interfaces
{
    public interface IOrdersUnitOfWork
    {
        Task<ActionResponse<Order>> AddAsync(Order order);

        Task<ActionResponse<IEnumerable<Order>>> GetAsync(string email, PagingDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(string email, PagingDTO pagination);

        Task<ActionResponse<Order>> GetAsync(int id);

        Task<ActionResponse<Order>> UpdateFullAsync(string email, OrderDTO orderDTO);

        Task<ActionResponse<OrderDetailDTO>> UpdateAsync(string email, int detailId, OrderDetailDTO orderDetailDTO);
    }
}