
using LabPreTest.Shared.Messages;
using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.Entities
{
    public class State
    {
        public int Id { get; set; }

        [Display ( Name = EntityMessages.StateDisplayName)]
        [MaxLength (100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required (ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country? Country { get; set; }
        public ICollection<City>? Cities { get; set; }

        [Display(Name = EntityMessages.CitiesDisplayMessage)]
        public int CitiesNumber => Cities == null || Cities.Count == 0 ? 0 : Cities.Count;

    }
}
