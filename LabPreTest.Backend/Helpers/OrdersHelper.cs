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
        private readonly ITestUnitOfWork _testUnitOfWork;
        private readonly IMedicianUnitOfWork _medicianUnitOfWork;
        private readonly IPatientUnitOfWork _patientUnitOfWork;

        public OrdersHelper(IUsersUnitOfWork usersUnitOfWork,
                            ITemporalOrdersUnitOfWork temporalOrdersUnitOfWork,
                            IOrdersUnitOfWork ordersUnitOfWork,
                            ITestUnitOfWork testUnitOfWork,
                            IMedicianUnitOfWork medicianUnitOfWork,
                            IPatientUnitOfWork patientUnitOfWork)
        {
            _usersUnitOfWork = usersUnitOfWork;
            _temporalOrdersUnitOfWork = temporalOrdersUnitOfWork;
            _ordersUnitOfWork = ordersUnitOfWork;
            _testUnitOfWork = testUnitOfWork;
            _medicianUnitOfWork = medicianUnitOfWork;
            _patientUnitOfWork = patientUnitOfWork;
        }

        public async Task<ActionResponse<Order>> ProcessOrderAsync(string email)
        {
            var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
                return ActionResponse<Order>.BuildFailed(BackendMessages.UserNotFoundMessage);

            var actionTemporalOrders = await _temporalOrdersUnitOfWork.GetAsync(email);
            if (!actionTemporalOrders.WasSuccess ||
                actionTemporalOrders.Result == null)
                return ActionResponse<Order>.BuildFailed(BackendMessages.OrderDetailNotFoundMessage);

            var temporalOrders = actionTemporalOrders.Result as List<TemporalOrder>;
            if (temporalOrders!.Count == 0)
                return ActionResponse<Order>.BuildFailed(BackendMessages.OrderDetailNotFoundMessage);

            var order = new Order
            {
                CreatedAt = DateTime.Now,
                Status = OrderStatus.Idle,
                User = user,
                Details = new List<OrderDetail>()
            };

            foreach (var orderDetail in temporalOrders)
            {
                var testResponse = await _testUnitOfWork.GetAsync(orderDetail.TestId);
                var medicResponse = await _medicianUnitOfWork.GetAsync(orderDetail.MedicId);
                var patientResponse = await _patientUnitOfWork.GetAsync(orderDetail.PatientId);

                if (!testResponse.WasSuccess || !medicResponse.WasSuccess || !patientResponse.WasSuccess)
                    return ActionResponse<Order>.BuildFailed(MessageStrings.DbRecordNotFoundMessage);

                order.Details.Add(new OrderDetail
                {
                    Test = testResponse.Result,
                    Medic = medicResponse.Result,
                    Patient = patientResponse.Result,
                    Status = OrderStatus.Idle
                });
            }

            var response = await _ordersUnitOfWork.AddAsync(order);
            if (!response.WasSuccess)
                return ActionResponse<Order>.BuildFailed(response.Message!);

            foreach (var orderDetail in temporalOrders)
                await _temporalOrdersUnitOfWork.DeleteAsync(orderDetail!.Id);

            return ActionResponse<Order>.BuildSuccessful(order);
        }
    }
}