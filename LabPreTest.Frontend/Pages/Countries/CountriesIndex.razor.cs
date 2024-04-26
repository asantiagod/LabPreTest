using LabPreTest.Frontend.Repositories;
using LabPreTest.Shared.ApiRoutes;
using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace LabPreTest.Frontend.Pages.Countries
{
    public partial class CountriesIndex
    {
        private int currentPage = 1;
        private int totalPages;

        [Inject] private IRepository Repository { get; set; } = null!;

        //TODO: sweet alert
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [Parameter, SupplyParameterFromQuery] public string Page {  get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter {  get; set; } = string.Empty;

        public List<Country>? Countries { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync(int page = 1)
        {
            if(!String.IsNullOrWhiteSpace(Page)) 
                page = Convert.ToInt32(Page);

            bool ok = await LoadListAsync(page);
            //if(ok)
        }

        private async Task<bool> LoadListAsync(int page)
        {
            var url = ApiRoutes.CountriesRoute + $"?page={page}";
            if (!string.IsNullOrWhiteSpace(Filter))
                url += $"&filter={Filter}";

            var responseHttp = await Repository.GetAsync<List<Country>>(url);
            if(responseHttp.Error)
            {
                //TODO sweet alert
                var message = await responseHttp.GetErrorMessageAsync();
                return false;
            }

            Countries = responseHttp.Response;
            return true;
        }
    }
}
