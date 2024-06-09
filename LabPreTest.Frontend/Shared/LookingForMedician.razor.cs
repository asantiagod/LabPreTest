using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace LabPreTest.Frontend.Shared
{
    public partial class LookingForMedician
    {
        private string? DocumentId;
        private Medic? Medician { get; set; }
        private string? MedicianName { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;

        private async Task SaveChangesAsync()
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok(Medician));
        }

        private async Task CloseModalAsync()
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }

        private async Task FindUserAsync()
        {
            if (DocumentId == null)
                return;

            var responseHttp = await Repository.GetAsync<Medic>($"{ApiRoutes.MedicianDocumentRoute}/{DocumentId}");
            if (responseHttp.Error)
            {
                var errorMessage = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", errorMessage, SweetAlertIcon.Error);
                return;
            }

            Medician = responseHttp.Response;
            MedicianName = Medician!.Name;
        }
    }
}