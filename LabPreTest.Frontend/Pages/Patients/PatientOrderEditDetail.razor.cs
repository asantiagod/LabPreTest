﻿using CurrieTechnologies.Razor.SweetAlert2;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Frontend.Shared;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.PagesRoutes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace LabPreTest.Frontend.Pages.Patients
{
    [Authorize(Roles = FrontendStrings.UserString)]
    public partial class PatientOrderEditDetail
    {
        private Patient? patient;

        private FormForUserOrderDetail<Patient>? patientForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [EditorRequired, Parameter] public int Id { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var responseHttp = await Repository.GetAsync<Patient>(ApiRoutes.PatientsRoute + $"/{Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo(PagesRoutes.Patients);
                }
                else
                {
                    var messsage = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messsage, SweetAlertIcon.Error);
                }
            }
            else
            {
                patient = responseHttp.Response;
            }
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync(ApiRoutes.PatientsRoute, patient);
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
            patientForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/orders/create");
        }
    }
}
