using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Frontend.Services;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.DTO;
using Microsoft.AspNetCore.Components;

namespace LabPreTest.Frontend.Pages.Auth
{
    public partial class Login
    {
        private LoginDTO loginDTO = new();

        private bool wasClose;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ILoginService LoginService { get; set; } = null!;
        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;
        [CascadingParameter] private IModalService modal { get; set; } = null!;

        [Parameter] public EventCallback<bool> OnConfirm { get; set; }

        private async Task CloseModalAsync()
        {
            wasClose = true;
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }

        private async Task LoginAsync()
        {
            var responseHttp = await Repository.PostAsync<LoginDTO, TokenDTO>(ApiRoutes.AccountsLogin, loginDTO);
            if (wasClose)
            {
                NavigationManager.NavigateTo("/");
                return;
            }
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await LoginService.LoginAsync(responseHttp.Response!.Token);
            NavigationManager.NavigateTo("/");
        }

   
        private void ShowModal()
        {
            modal.Show<RecoverPassword>();
        }
    }
}