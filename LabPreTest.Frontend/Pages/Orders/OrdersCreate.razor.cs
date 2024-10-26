﻿using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using LabPreTest.Frontend.Pages.Medician;
using LabPreTest.Frontend.Pages.Patients;
using LabPreTest.Frontend.Pages.Tests;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Frontend.Shared;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Enums;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.PagesRoutes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace LabPreTest.Frontend.Pages.Orders
{
    [Authorize(Roles = FrontendStrings.UserString)]
    public partial class OrdersCreate
    {
        private List<TemporalOrder>? TemporalOrders { get; set; }
        private List<Medic>? Medicians { get; set; }
        private int MedicValue { get; set; }
        private List<Patient>? Patients { get; set; }
        private int PatientValue { get; set; }
        private string? PatientDocumentValue { get; set; }
        private string? MedicDocumentValue { get; set; }

        private Patient? patient;

        private List<PreanalyticCondition>? conditions; 

        private List<Test>? tests;

        private bool wasClose;
        private int NumberOfTests { get; set; }
        private bool IsAddButtonDisabled { get; set; } = true;
        private bool IsSelectorDisabled { get; set; } = false;
        private Patient? SelectedPatient { get; set; }
        private Medic? SelectedMedic { get; set; }

        private Test? SelectedTest { get; set; }    
        private bool PatientFound { get; set; }

        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;
        [CascadingParameter] private IModalService ModalService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadTemporalOrdersAsync();
            await LoadMediciansAsync();
            await LoadPatientsAsync();
            await LoadTestAsync();
            await LoadTestConditions();
            SetButtonStatus();
            SetSelectorStatus();
        }
        private async Task<int?> FindPatient()
        {
            if (!string.IsNullOrWhiteSpace(PatientDocumentValue))
            {
                SelectedPatient = Patients.FirstOrDefault(p => p.DocumentId.ToString() == PatientDocumentValue);
                if (SelectedPatient == null)
                {
                    var result = await SweetAlertService.FireAsync(new SweetAlertOptions
                    {
                        Title = "Búsqueda de paciente",
                        Text = "Paciente no encontrado. ¿Deseas crear un nuevo paciente?",
                        Icon = SweetAlertIcon.Error,
                        ShowCancelButton = true,
                        ConfirmButtonText = "Sí",
                        CancelButtonText = "No"
                    });

                    if (result.IsConfirmed)
                    {
                        ShowCreatePatientModal();
                    }
                    return null;
                }
                StateHasChanged();
                SetButtonStatus();
            }
            return SelectedPatient?.Id;
        }

        private async Task<int?> SearchMedic()
        {
            if (!string.IsNullOrWhiteSpace(MedicDocumentValue))
            {
                SelectedMedic = Medicians.FirstOrDefault(x => x.DocumentId.ToString() == MedicDocumentValue);
                if (SelectedMedic == null)
                {
                    var result = await SweetAlertService.FireAsync(new SweetAlertOptions
                    {
                        Title = "Busqueda de medico",
                        Text = "Médico no encontrado. ¿Deseas crear un nuevo médico?",
                        Icon = SweetAlertIcon.Error,
                        ShowCancelButton = true,
                        ConfirmButtonText = "Sí",
                        CancelButtonText = "No"
                    });
                    if (result.IsConfirmed)
                    {
                        ShowCreateMedicModal();
                    }
                    return null;
                }
                StateHasChanged();
                SetButtonStatus();
            }

            return SelectedMedic?.Id;
        }
        private async Task CloseModalAsync()
        {
            wasClose = true;
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }

        private async Task<List<T>?> LoadListAsync<T>(string apiRoute)
        {
            var responseHttp = await Repository.GetAsync<List<T>>(apiRoute);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return null;
            }
            return responseHttp.Response;
        }

        private async Task ClearTemporalOrder() 
        {
            TemporalOrders = await LoadListAsync<TemporalOrder>(ApiRoutes.TemporalOrdersMyRoute);
            var responseHttp = await Repository.DeleteAsync<TemporalOrder>($"/api/temporalorders/1");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode != HttpStatusCode.NotFound)
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                    return;
                }
            }
        }
        private async Task LoadTemporalOrdersAsync()
        {
            try
            {
                TemporalOrders = await LoadListAsync<TemporalOrder>(ApiRoutes.TemporalOrdersMyRoute);
                NumberOfTests = TemporalOrders!.Count;
            }
            catch (Exception ex)
            {
                await SweetAlertService.FireAsync("Error", ex.Message, SweetAlertIcon.Error);
            }
            SetButtonStatus();
            SetSelectorStatus();
        }

        private async Task LoadMediciansAsync()
        {
            Medicians = await LoadListAsync<Medic>(ApiRoutes.MedicianFullRoute);
            if (TemporalOrders != null && TemporalOrders.Any())
                MedicValue = TemporalOrders.First().MedicId;
            SetButtonStatus();
        }

        private async Task LoadPatientsAsync()
        {
            Patients = await LoadListAsync<Patient>(ApiRoutes.PatientsFullRoute);
            if (TemporalOrders != null && TemporalOrders.Any())
                PatientValue = TemporalOrders.First().PatientId;
            SetButtonStatus();
        }

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync(ApiRoutes.OrdersRoute,
                                                          new OrderDTO { Status = OrderStatus.Idle });
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

        private void PatientChanged(ChangeEventArgs e)
        {
            PatientValue = Convert.ToInt32(e.Value!);
            SetButtonStatus();
        }

        private void MedicChanged(ChangeEventArgs e)
        {
            MedicValue = Convert.ToInt32(e.Value!);
            SetButtonStatus();
        }

        private void SetButtonStatus()
        {
            if (SelectedMedic != null && SelectedPatient != null)
                IsAddButtonDisabled = false;
            else IsAddButtonDisabled = true; 
        }

        private void SetSelectorStatus()
        {
            if (SelectedMedic != null && SelectedPatient != null)
                IsSelectorDisabled = false;
            else IsSelectorDisabled = true;
        }

        private async void ShowAddTestModal()
        {
            var modalMessage = ModalService.Show<LookingForTest>();
            var result = await modalMessage.Result;

            if (result.Confirmed && result.Data != null)
            {
                Test test = (Test)result.Data;
                var responseHttp = await Repository
                        .PostAsync<TemporalOrdersDTO>(ApiRoutes.TemporalOrdersFullRoute,
                                                      new TemporalOrdersDTO
                                                      {
                                                          TestId = test.Id,
                                                          MedicId = SelectedMedic.Id,
                                                          PatientId = SelectedPatient.Id
                                                      });
                if (responseHttp.Error)
                {
                    var errorMessage = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", errorMessage, SweetAlertIcon.Error);
                    return;
                }

                await LoadTemporalOrdersAsync();
                StateHasChanged();
            }
        }

        private void Return()
        {
            NavigationManager.NavigateTo(PagesRoutes.Orders);
        }

        private async void ProcessOrder()
        {
            var responseHttp = await Repository.PostAsync(ApiRoutes.OrdersRoute,
                                                          new OrderDTO { Status = OrderStatus.Idle });

            if (responseHttp.Error)
            {
                var errorMessage = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", errorMessage, SweetAlertIcon.Error);
            }
            NavigationManager.NavigateTo(PagesRoutes.Orders);
        }

        private async Task LoadTestAsync()
        {
            tests = await LoadListAsync<Test>(ApiRoutes.TestFullRoute);
        }
        private async Task LoadTestConditions()
        {
            conditions = await LoadListAsync<PreanalyticCondition>(ApiRoutes.TestFullRoute);
        }

        private void ShowCreateMedicModal()
        {
            ModalService.Show<MedicOrderCreate>();
        }
        private void ShowCreatePatientModal()
        {
            ModalService.Show<PatientOrderCreate>();
        }
    }
}