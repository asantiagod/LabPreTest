using Blazored.Modal.Services;
using Blazored.Modal;
using LabPreTest.Frontend.Services;
using Microsoft.AspNetCore.Components;
using CurrieTechnologies.Razor.SweetAlert2;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.DTO;

namespace LabPreTest.Frontend.Pages.Auth
{
    public partial class Logout
    {
        private bool wasClose;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ILoginService LoginService { get; set; } = null!;
        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;
        [CascadingParameter] private IModalService modal { get; set; } = null!;

        [Parameter] public EventCallback<bool> OnConfirm { get; set; }

        private async Task CloseModalAsync()
        {
            wasClose = true;
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }

        private async Task LogoutAsync()
        {
            if (wasClose)
            {
                NavigationManager.NavigateTo("/");
                return;
            }
            await LoginService.LogoutAsync();
            NavigationManager.NavigateTo("/");
        }


    }
}