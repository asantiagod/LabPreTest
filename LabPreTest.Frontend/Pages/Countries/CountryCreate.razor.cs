using LabPreTest.Frontend.Repositories;
using LabPreTest.Frontend.Shared;
using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace LabPreTest.Frontend.Pages.Countries
{
    public partial class CountryCreate
    {
        private Country country = new();
        private FormWithName<Country>? countryForm;
        [Inject] private IRepository Repository { get; set; } = null!;
        //[Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/countries", country);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                // TODO: sweer alert
                //await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            Return();
            //var toast = SweetAlertService.Mixin(new SweetAlertOptions
            //{
            //    Toast = true,
            //    Position = SweetAlertPosition.BottomEnd,
            //    ShowConfirmButton = true,
            //    Timer = 3000
            //});
            //await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito.");
        }

        private void Return()
        {
            countryForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/countries");
        }
    }
}