using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.UnitOfWork.Implementations
{
    public class OrdersUnitOfWork : GenericUnitOfWork<Order>,IOrdersUnitOfWork
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersUnitOfWork(IGenericRepository<Order> repository, IOrdersRepository ordersRepository): base(repository)
        {
            _ordersRepository = ordersRepository;
        }
        public Task<ActionResponse<IEnumerable<Order>>> GetAsync(string email, PagingDTO pagination) => _ordersRepository.GetAsync(email, pagination);

        public override Task<ActionResponse<Order>> GetAsync(int id) => _ordersRepository.GetAsync(id);

        public Task<ActionResponse<int>> GetTotalPagesAsync(string email, PagingDTO pagination) =>_ordersRepository.GetTotalPagesAsync(email, pagination);

        public Task<ActionResponse<Order>> UpdateFullAsync(string email, OrderDTO orderDTO) => _ordersRepository.UpdateFullAsync(email, orderDTO);
    }
}
