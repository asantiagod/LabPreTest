using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.UnitOfWork.Implementations
{
    public class OrderUnitOfWork : GenericUnitOfWork<Order>, IOrderUnitOfWork
    {
        private readonly IOrderRepository _orderRepository;

        public OrderUnitOfWork(IGenericRepository<Order> repository, IOrderRepository ordernRepository) : base(repository)
        {
            _orderRepository = ordernRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Order>>> GetAsync() => await _orderRepository.GetAsync();

        public override async Task<ActionResponse<Order>> GetAsync(int id) => await _orderRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<Order>>> GetAsync(PagingDTO paging) => await _orderRepository.GetAsync(paging);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging) => await _orderRepository.GetTotalPagesAsync(paging);
    }
}