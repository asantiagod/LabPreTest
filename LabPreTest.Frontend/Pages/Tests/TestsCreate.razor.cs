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

namespace LabPreTest.Frontend.Pages.Tests
{
    [Authorize(Roles = FrontendStrings.AdminString)]
    public partial class TestsCreate
    {
        private Test test = new();

        private FormForTest<Test>? testForm;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        private async Task CreateAsync()
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

            var responseHttp = await Repository.PostAsync(ApiRoutes.TestDTORoute, testDTO);
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

        private void Return()
        {
            testForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo(PagesRoutes.Tests);
        }
    }
}