using Microsoft.AspNetCore.Components;
using LabPreTest.Frontend.Repositories;
using LabPreTest.Shared.Entities;

namespace LabPreTest.Frontend.Pages.Medicians
{
    public partial class MedicianIndex
    {
        [Inject] private IRepository Repository {  get; set; } = null!;

        public List<Country>? Medician { get; set; };


    }
}
