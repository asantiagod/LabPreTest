using Microsoft.JSInterop;

namespace LabPreTest.Frontend.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public static ValueTask<Object> SetLocalStorage(this IJSRuntime js, string key, string content)
        {
            return js.InvokeAsync<Object>("localStorage.setItem", key, content);
        }

        public static ValueTask<object> GetLocalStorage(this IJSRuntime js, string key)
        {
            return js.InvokeAsync<object>("localStorage.getItem", key);
        }

        public static ValueTask<object> RemoveLocalStorage(this IJSRuntime js, string key)
        {
            return js.InvokeAsync<object>("localStorage.removeItem", key);
        }
    }
}