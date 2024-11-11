using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.Helpers
{
    public interface IOrdersHelper
    {
        Task<ActionResponse<Order>> ProcessOrderAsync(string email);
    }
}
