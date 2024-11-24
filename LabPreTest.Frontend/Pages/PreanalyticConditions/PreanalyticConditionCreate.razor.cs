using CurrieTechnologies.Razor.SweetAlert2;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Frontend.Shared;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.PagesRoutes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace LabPreTest.Frontend.Pages.PreanalyticConditions
{
    //[Authorize(Roles = FrontendStrings.AdminString)]
    public partial class  PreanalyticConditionCreate
    {
        private FormForPreanalyticCondition<PreanalyticCondition>? preanalyticConditionForm;

        private PreanalyticCondition preanalyticCond = new PreanalyticCondition();
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync(ApiRoutes.PreanalyticConditionsRoute, preanalyticCond);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: FrontendMessages.RecordChangedMessage);
        }


        private void Return()
        {
            preanalyticConditionForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo(PagesRoutes.PreanalyticConditions);
        }
    }
}
