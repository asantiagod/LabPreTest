using CurrieTechnologies.Razor.SweetAlert2;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;

namespace LabPreTest.Frontend.Shared
{
    public partial class FormForShowTestOrderCreation<TModel> where TModel : ITestEntity
    {
        private EditContext editContext = null!;
        private List<Section>? Sections;
        private List<TestTube>? TestTubes;
        private List<PreanalyticCondition>? PreanalyticConditions;

        //variables to controll some UI variables
        private int SectionDefaultValue;

        private int TestTubeDefaultValue;

        [EditorRequired, Parameter] public TModel Model { get; set; } = default!;
        [EditorRequired, Parameter] public string Label { get; set; } = null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        public bool FormPostedSuccessfully { get; set; }

        protected override async Task OnInitializedAsync()
        {
            editContext = new(Model);
            await LoadSectionsAsync();
            await LoadTestTubesAsync();
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

        private async Task LoadSectionsAsync()
        {
            Sections = await LoadListAsync<Section>(ApiRoutes.SectionRoute);
        }

        private async Task LoadTestTubesAsync()
        {
            TestTubes = await LoadListAsync<TestTube>(ApiRoutes.TestTubeRoute);
        }
    }
}