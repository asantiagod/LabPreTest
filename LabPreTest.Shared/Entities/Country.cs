using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.Entities
{
    public class Country
    {
        public int Id { get; set; }

        [Display(Name = "CountryDisplayName",
                 ResourceType = typeof(Resources.EntitiesResources))]
        [MaxLength(100,
                   ErrorMessageResourceType = typeof(Resources.EntitiesResources),
                   ErrorMessageResourceName = "MaxLengthErrorMessage")]
        [Required(ErrorMessageResourceName = "RequiredErrorMessage",
                  ErrorMessageResourceType = typeof(Resources.EntitiesResources))]
        public string Name { get; set; } = null!;
    }
}