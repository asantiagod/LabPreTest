using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using LabPreTest.Frontend.Pages.Tests;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Dynamic;

namespace LabPreTest.Frontend.Shared
{
    public partial class LookingForTest
    {
        private List<Test>? Tests;
        private Test? SelectedTest { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            Tests = await LoadTestsAsync();
        }

        private async Task<List<Test>?> LoadTestsAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Test>>(ApiRoutes.TestFullRoute);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return null;
            }
            return responseHttp.Response;
        }

        private async Task SaveChangesAsync()
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok(SelectedTest));
        }

        private async Task CloseModalAsync()
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }

        private void TestChanged(ChangeEventArgs e)
        {
            int id = Convert.ToInt32(e.Value!);
            SelectedTest = Tests!.FirstOrDefault(x => x.Id == id);
            Console.WriteLine($"SelectedTest.Id: {id}\tSelectedTest.Name: {SelectedTest!.Name}");
        }
    }
}