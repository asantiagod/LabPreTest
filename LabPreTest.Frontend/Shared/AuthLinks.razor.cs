using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using LabPreTest.Frontend.Pages.Auth;
using Blazored.Modal;

namespace LabPreTest.Frontend.Shared
{
    public partial class AuthLinks
    {
        private NavigationManager NavigationManager { get; set; } = null!;
        public string? photoUser;
        [CascadingParameter] private IModalService Modal { get; set; } = default!;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;

        protected override async Task OnParametersSetAsync()
        {
            var authenticationState = await AuthenticationStateTask;
            var claims = authenticationState.User.Claims.ToList();
            var photoClaim = claims.FirstOrDefault(x => x.Type == "Photo");
            
            if(photoClaim is not null)
            {
                photoUser = photoClaim.Value;
            }

        }

        void ShowModal()
        {
            Modal.Show<Login>();
        }
        void ShowModalLogout()
        {
            
        }

    }
}
