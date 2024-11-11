using System.Net;
using CurrieTechnologies.Razor.SweetAlert2;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.PagesRoutes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace LabPreTest.Frontend.Pages.SampleTaking
{
    [Authorize(Roles = FrontendStrings.UserString)]
    public partial class SampleTaking
    {
        private int currentPage = 1;
        private int totalPages;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        private int? orderValue { get; set; }

        [Parameter, SupplyParameterFromQuery] public string Page { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string RecordNumberQueryString { get; set; } = string.Empty;
        private Patient? SelectedPatient { get; set; }
        public List<OrderDetail>? ordersDetails { get; set; }

        private int medicId;
        private int patientId;
        private Order? SelectedOrder;


        protected async Task SearchOrder()
        {

            if (orderValue != null)
            {
                var responseHttp = await Repository.GetAsync<Order>($"/api/orders/{orderValue}");

                if (responseHttp.Error)
                {
                    if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    {
                        var result = await SweetAlertService.FireAsync(new SweetAlertOptions
                        {
                            Title = "Orden no encontrada",
                            Icon = SweetAlertIcon.Error,
                            ShowCancelButton = true,
                            ConfirmButtonText = "Aceptar",
                        });
                    }
                    else
                    {
                        await SweetAlertService.FireAsync(new SweetAlertOptions
                        {
                            Title = "Error",
                            Text = "Ocurrió un error al buscar el paciente. Por favor, intente nuevamente.",
                            Icon = SweetAlertIcon.Error
                        });
                    }
                }
                else
                {
                    SelectedOrder = responseHttp.Response;
                    ordersDetails = SelectedOrder.Details.ToList();
                    medicId = ordersDetails.FirstOrDefault().MedicId;
                    patientId = ordersDetails.FirstOrDefault().PatientId;
                    StateHasChanged();
                }
            }
            else
            {
                await SweetAlertService.FireAsync(new SweetAlertOptions
                {
                    Title = "Error",
                    Text = "Documento del paciente inválido.",
                    Icon = SweetAlertIcon.Error
                });
            }
            
        }


    }
}
