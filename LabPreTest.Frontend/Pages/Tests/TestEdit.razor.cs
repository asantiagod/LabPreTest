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

namespace LabPreTest.Frontend.Pages.Tests
{
    [Authorize(Roles = FrontendStrings.AdminString)]
    public partial class TestEdit
    {
        private Test? test;
        private FormForTest<Test>? testForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

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

        private async Task EditAsync()
        {
            TestDTO testDTO = new TestDTO
            {
                Name = test.Name,
                TestID = test.TestID,
                SectionID = test.Section.Id,
                TestTubeID = test.TestTube.Id,
                Conditions = []
            };

            if (test.Conditions == null)
            {
                await SweetAlertService.FireAsync("Error",
                                                  FrontendMessages.PreanalyticalConditionsNotFound,
                                                  SweetAlertIcon.Error);
                return;
            }

            foreach (var c in test.Conditions)
                testDTO.Conditions.Add(c.Id);

            var responseHttp = await Repository.PutAsync($"{ApiRoutes.TestDTORoute}/{test.Id}", testDTO);
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
            testForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo(PagesRoutes.Tests);
        }
    }
}
