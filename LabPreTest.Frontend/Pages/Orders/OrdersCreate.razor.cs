using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
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

namespace LabPreTest.Frontend.Pages.Orders
{
    [Authorize(Roles = FrontendStrings.UserString)]
    public partial class OrdersCreate
    {
        private List<TemporalOrder>? TemporalOrders { get; set; }
        private Medic? Medician { get; set; }
        private int MedicValue { get; set; }
        private Patient? Patient { get; set; }
        private int PatientValue { get; set; }
        private int NumberOfTests { get; set; }
        private bool IsAddButtonDisabled { get; set; } = true;
        private bool IsSelectorDisabled { get; set; } = false;

        public bool IsEditable { get; set; } = true;

        [CascadingParameter] private IModalService ModalService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadTemporalOrdersAsync();
            SetButtonStatus();
            SetSelectorStatus();
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

        private async Task LoadTemporalOrdersAsync()
        {
            try
            {
                TemporalOrders = await LoadListAsync<TemporalOrder>(ApiRoutes.TemporalOrdersMyRoute);
                if (TemporalOrders!.Any())
                {
                    NumberOfTests = TemporalOrders!.Count;
                    var fto = TemporalOrders.First();
                    Medician = fto.Medic;
                    MedicValue = fto.MedicId;
                    Patient = fto.Patient;
                    PatientValue = fto.PatientId;
                }
            }
            catch (Exception ex)
            {
                await SweetAlertService.FireAsync("Error", ex.Message, SweetAlertIcon.Error);
            }

            SetButtonStatus();
            SetSelectorStatus();
        }

        private void SetButtonStatus()
        {
            if (MedicValue != 0 && PatientValue != 0)
                IsAddButtonDisabled = false;
            else
                IsAddButtonDisabled = true;
        }

        private void SetSelectorStatus()
        {
            // avoid changes of medician or patient if there are a pending order
            if (TemporalOrders != null && TemporalOrders.Any())
                IsSelectorDisabled = true;
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
                                                          MedicId = MedicValue,
                                                          PatientId = PatientValue
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

        private async void ShowFindPatientModal()
        {
            var modalMessage = ModalService.Show<LookingForPatient>();
            var result = await modalMessage.Result;

            if (result.Confirmed && result.Data != null)
            {
                Patient = (Patient)result.Data;
                PatientValue = Patient.Id;
                SetButtonStatus();
                StateHasChanged();
            }
        }

        private async void ShowFindMedicianModal()
        {
            var modalMessage = ModalService.Show<LookingForMedician>();
            var result = await modalMessage.Result;

            if (result.Confirmed && result.Data != null)
            {
                Medician = (Medic)result.Data;
                MedicValue = Medician.Id;
                SetButtonStatus();
                StateHasChanged();
            }
        }

        private void ShowEditModal(int testId)
        {
            var parameter = new ModalParameters()
                .Add(nameof(OrdersShowTestDetails.Id), testId);
            ModalService.Show<OrdersShowTestDetails>(parameter);
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
    }
}