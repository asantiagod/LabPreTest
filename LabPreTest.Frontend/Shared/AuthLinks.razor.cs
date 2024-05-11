using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop.Implementation;

namespace LabPreTest.Frontend.Shared
{
    public partial class AuthLinks
    {
        public string? photoUser;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;

        protected override async Task OnParametersSetAsync()
        {
            var authenticationState = await AuthenticationStateTask;
            var claims = authenticationState.User.Claims.ToList();
            var photoClaim = claims.FirstOrDefault(x => x.Type == "Photo");
            
            if(photoClaim != null)
                photoUser = photoClaim.Value;
        }
    }
}
