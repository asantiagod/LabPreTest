using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name = "Country")]
        [MaxLength(100, ErrorMessage = "The field {0} can't be more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Name { get; set; } = null!;
    }
}