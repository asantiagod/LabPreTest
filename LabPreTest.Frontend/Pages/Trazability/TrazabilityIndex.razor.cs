using System.Net;
using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.PagesRoutes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace LabPreTest.Frontend.Pages.Trazability
{
    [Authorize(Roles = FrontendStrings.AdminString)]
    public partial class TrazabilityIndex
    {
        private int currentPage = 1;
        private int totalPages;
        private string newChanges;
        private int orderValue;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [Parameter, SupplyParameterFromQuery] public string Page { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string RecordNumberQueryString { get; set; } = string.Empty;
        [CascadingParameter] private IModalService ModalService { get; set; } = null!;

        public List<OrderAudit>? Trazability { get; set; }

        public OrderAudit? orderAuditFiltered {  get; set; } 

        private JObject? orderAudit;

        protected override async Task OnInitializedAsync()
        {
            await SelectedRedordsNumberAsync("10");
            await LoadAsync();
        }

        private async Task SelectedPageAsync(int page)
        {
            currentPage = page;
            await LoadAsync(page);
        }

        private async Task SelectedRedordsNumberAsync(string recordsNumber)
        {
            RecordNumberQueryString = $"RecordsNumber={recordsNumber}";
            await LoadAsync();
        }

        private async Task LoadAsync(int page = 1)
        {
            if (!String.IsNullOrWhiteSpace(Page))
                page = Convert.ToInt32(Page);

            bool ok = await LoadListAsync(page);
            if (ok)
                await LoadTotalPagesAsync();
        }

        private async Task LoadTotalPagesAsync()
        {
            if (RecordNumberQueryString.ToLower().Contains("full"))
            {
                totalPages = 1;
                return;
            }

            var url = "api/OrderAudits/" + ApiRoutes.TotalPages;
            url += $"?{RecordNumberQueryString}";
            if (!string.IsNullOrWhiteSpace(Filter))
                url += $"&filter={Filter}";

            var responseHttp = await Repository.GetAsync<int>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            totalPages = responseHttp.Response;
        }
        protected async Task SearchOrder()
        {

            if (orderValue != 0)
            {
                var responseHttp = await Repository.GetAsync<OrderAudit>($"/api/orderaudits/{orderValue}");

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
                        ResetValue();
                    }
                    else
                    {
                        await SweetAlertService.FireAsync(new SweetAlertOptions
                        {
                            Title = "Error",
                            Text = "Ocurrió un error al buscar la orden. Por favor, intente nuevamente.",
                            Icon = SweetAlertIcon.Error
                        });
                        ResetValue();
                    }
                }
                else
                {
                    orderAuditFiltered = responseHttp.Response;
                    StateHasChanged();
                }
            }
            else
            {
                await SweetAlertService.FireAsync(new SweetAlertOptions
                {
                    Title = "Error",
                    Text = "Orden sin informacion de trazabilidad.",
                    Icon = SweetAlertIcon.Error
                });
            }

        }
        private void ResetValue()
        {
            if (orderValue != 0)
                orderValue = 0;
            StateHasChanged();
        }
        private async Task<bool> LoadListAsync(int page)
        {
            var url = "api/OrderAudits/";
            if (RecordNumberQueryString.ToLower().Contains("full"))
                url += $"{ApiRoutes.Full}";
            else
                url += $"?page={page}&{RecordNumberQueryString}";

            if (!string.IsNullOrWhiteSpace(Filter))
                url += $"&filter={Filter}";

            var responseHttp = await Repository.GetAsync<List<OrderAudit>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }

            Trazability = responseHttp.Response;

            return true;
        }

        private async Task FilterCallback(string filter)
        {
            Filter = filter;
            await ApplyFilterAsync();
            StateHasChanged();
        }

        private async Task ApplyFilterAsync()
        {
            int page = 1;
            await SelectedPageAsync(page);
        }

    }
}