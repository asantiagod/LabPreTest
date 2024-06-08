using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.Helpers
{
    public interface IOrdersHelper
    {
        Task<ActionResponse<bool>> ProcessOrderAsync(string email);
    }
}
