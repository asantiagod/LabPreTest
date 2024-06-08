
namespace LabPreTest.Shared.Responses
{
    public class ActionResponse<T>
    {
        public bool WasSuccess { get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }
        public static ActionResponse<T> BuildFailed(string errorMessage)
        {
            return new ActionResponse<T>
            {
                WasSuccess = false,
                Message = errorMessage,
            };
        }

        public static ActionResponse<T> BuildSuccessful(T result)
        {
            return new ActionResponse<T>
            {
                WasSuccess = true,
                Result = result
            };
        }
    }
}