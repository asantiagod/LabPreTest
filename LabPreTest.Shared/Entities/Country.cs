using LabPreTest.Shared.Interfaces;
using LabPreTest.Shared.Messages;
using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.Entities
{
    public class Country : IEntityWithId, IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = EntityMessages.CountryDisplayName)]
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Name { get; set; } = null!;

        public ICollection<State>? States { get; set; }

        [Display(Name = EntityMessages.StatesCitiesDisplayMessage)]
        public int StatesNumber => States == null || States.Count == 0 ? 0 : States.Count;
    }
}