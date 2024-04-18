
using LabPreTest.Shared.Messages;
using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.Entities
{
    public class City
    {
        public int Id { get; set; }
        [Display(Name = EntityMessages.CityDisplayName)]
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required (ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Name {  get; set; }

    }
}
