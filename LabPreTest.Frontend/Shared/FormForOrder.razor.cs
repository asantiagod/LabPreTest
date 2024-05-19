using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using System.ComponentModel;

namespace LabPreTest.Frontend.Shared
{
    public partial class FormForOrder<TModel> where TModel : IOrderEntity
    {
        private EditContext editContext = null!;
        private List<Patient>? Patients;
        private List<Medic>? Medics;
        private List<Test> SelectedTests = new();

        [CascadingParameter] private IModalService ModalService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        [EditorRequired, Parameter] public TModel Model { get; set; } = default!;
        [EditorRequired, Parameter] public string Label { get; set; } = null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        public bool FormPostedSuccessfully { get; set; }

        protected override async Task OnInitializedAsync()
        {
            editContext = new(Model);
            await LoadPatientsAsync();
            await LoadMedicsAsync();
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

        private async Task LoadMedicsAsync()
        {
            Medics = await LoadListAsync<Medic>(ApiRoutes.MedicianFullRoute);
        }

        private async Task LoadPatientsAsync()
        {
            Patients = await LoadListAsync<Patient>(ApiRoutes.PatientsFullRoute);
        }

        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();
            if (!formWasEdited || FormPostedSuccessfully)
            {
                return;
            }

            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = "¿Deseas abandonar la página y perder los cambios?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
            });
            var confirm = !string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            context.PreventNavigation();
        }

        private void PatientChanged(ChangeEventArgs e)
        {
            int id = Convert.ToInt32(e.Value!);
            Model.patientId = id;
            Model.patientName = Patients!.First(p => p.Id == id).Name;
        }

        private void MedicChanged(ChangeEventArgs e)
        {
            int id = Convert.ToInt32(e.Value!);

            //TODO: add medicId field
            //Model.medicId = id;
            Model.medicName = Medics!.First(p => p.Id == id).Name;
        }

        private async void ShowAddTestModal()
        {
            var message = ModalService.Show<LookingForTest>();
            var result = await message.Result;

            if (result.Confirmed && result.Data != null)
            {
                Test test = (Test)result.Data;
                SelectedTests.Add(test);
                
                List<int> ids = new();
                foreach (var t in SelectedTests)
                    ids.Add(t.Id);

                Model.TestIds = ids;
                StateHasChanged();
            }
        }
    }
}