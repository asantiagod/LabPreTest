using System.Net;
using CurrieTechnologies.Razor.SweetAlert2;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.PagesRoutes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace LabPreTest.Frontend.Pages.Orders
{
    //[Authorize(Roles = FrontendStrings.UserString)]
    public partial class OrdersIndex
    {
        private int currentPage = 1;
        private int totalPages;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [Parameter, SupplyParameterFromQuery] public string Page { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string RecordNumberQueryString { get; set; } = string.Empty;

        public List<Order>? orders { get; set; }

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

            var url = ApiRoutes.OrdersRoute + "/" + ApiRoutes.TotalPages;
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

        private async Task<bool> LoadListAsync(int page)
        {
            var url = ApiRoutes.OrdersRoute;
            if (RecordNumberQueryString.ToLower().Contains("full"))
                url += $"/{ApiRoutes.Full}";
            else
                url += $"?page={page}&{RecordNumberQueryString}";

            if (!string.IsNullOrWhiteSpace(Filter))
                url += $"&filter={Filter}";

            var responseHttp = await Repository.GetAsync<List<Order>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }

            orders = responseHttp.Response;
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

        private async Task DeleteAsync(Order order)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmation",
                Text = $"Are you sure you want to delete the country: {order.Id}?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
            });
            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<Order>(ApiRoutes.OrdersRoute + $"/{order.Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo(PagesRoutes.CreateCountry);
                }
                else
                {
                    var mensajeError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                }
                return;
            }

            await LoadAsync();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: FrontendMessages.RecordDeletedMessage);
        }
    }
}
