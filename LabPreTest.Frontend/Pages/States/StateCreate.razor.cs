using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Frontend.Shared;
using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.PagesRoutes;

namespace LabPreTest.Frontend.Pages.States
{
    [Authorize(Roles = FrontendStrings.AdminString)]
    public partial class StateCreate
    {
        private State state = new();
        private FormWithName<State>? stateForm;

        [Parameter] public int CountryId { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        private async Task CreateAsync()
        {
            state.CountryId = CountryId;
            var responseHttp = await Repository.PostAsync(ApiRoutes.StatesRoute, state);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            Return();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: FrontendMessages.RecordCreatedMessage);
        }

        private void Return()
        {
            stateForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo(PagesRoutes.DetailsCountry + $"/{CountryId}");
        }
    }
}