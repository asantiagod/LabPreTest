using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Frontend.Shared;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.PagesRoutes;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace LabPreTest.Frontend.Pages.Orders
{
    public partial class OrdersShowTestDetails
    {
        private Test? test;
        private FormForShowTest<Test>? testForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;

        [EditorRequired, Parameter] public int Id { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var responseHttp = await Repository.GetAsync<Test>(ApiRoutes.TestRoute + $"/{Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo(PagesRoutes.Tests);
                }
                else
                {
                    var messsage = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messsage, SweetAlertIcon.Error);
                }
            }
            else
            {
                test = responseHttp.Response;
            }
        }
        private async void Return()
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }
    }
}
