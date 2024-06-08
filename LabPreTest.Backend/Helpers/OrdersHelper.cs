using Azure.Storage.Blobs.Models;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Enums;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.Helpers
{
    public class OrdersHelper : IOrdersHelper
    {
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly ITemporalOrdersUnitOfWork _temporalOrdersUnitOfWork;
        private readonly IOrdersUnitOfWork _ordersUnitOfWork;

        public OrdersHelper(IUsersUnitOfWork usersUnitOfWork,
                            ITemporalOrdersUnitOfWork temporalOrdersUnitOfWork,
                            IOrdersUnitOfWork ordersUnitOfWork)
        {
            _usersUnitOfWork = usersUnitOfWork;
            _temporalOrdersUnitOfWork = temporalOrdersUnitOfWork;
            _ordersUnitOfWork = ordersUnitOfWork;
        }
        public async Task<ActionResponse<bool>> ProcessOrderAsync(string email)
        {
            var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
                return ActionResponse<bool>.BuildFailed(BackendMessages.UserNotFoundMessage);

            var actionTemporalOrders = await _temporalOrdersUnitOfWork.GetAsync(email);
            if(!actionTemporalOrders.WasSuccess ||
                actionTemporalOrders.Result == null)
                return ActionResponse<bool>.BuildFailed(BackendMessages.OrderDetailNotFoundMessage);

            var temporalOrders = actionTemporalOrders.Result as List<TemporalOrder>;
            if(temporalOrders!.Count == 0)
                return ActionResponse<bool>.BuildFailed(BackendMessages.OrderDetailNotFoundMessage);

            var order = new Order
            {
                CreatedAt = DateTime.Now,
                Status = OrderStatus.Idle,
                User = user,
                Details = new List<OrderDetail>()
            };

            foreach(var orderDetail in temporalOrders)
            {
                order.Details.Add(new OrderDetail
                {
                    Test = orderDetail.Test,
                    Medic = orderDetail.Medic,
                    Patient = orderDetail.Patient
                });
                
                await _temporalOrdersUnitOfWork.DeleteAsync(orderDetail!.Id);
            }
            
            await _ordersUnitOfWork.AddAsync(order);

            return ActionResponse<bool>.BuildSuccessful(true);
        }
    }
}
