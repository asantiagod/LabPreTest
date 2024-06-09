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
        private List<TemporalOrder>? TemporalOrders { get; set; }        private List<Medic>? Medicians { get; set; }        private int MedicValue { get; set; }        private List<Patient>? Patients { get; set; }        private int PatientValue { get; set; }        private int NumberOfTests { get; set; }        private bool IsAddButtonDisabled { get; set; } = true;        private bool IsSelectorDisabled { get; set; } = false;        [CascadingParameter] private IModalService ModalService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadTemporalOrdersAsync();
            await LoadMediciansAsync();
            await LoadPatientsAsync();
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