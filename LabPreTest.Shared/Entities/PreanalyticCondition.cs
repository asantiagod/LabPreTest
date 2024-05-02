
using LabPreTest.Shared.Interfaces;
using LabPreTest.Shared.Messages;
using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.Entities
{
    public class PreanalyticCondition: IEntityWithId, IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = EntityMessages.PreanalyticConditionDisplayName)]
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Name { get; set; } = null!;

        [Display(Name = EntityMessages.PreanalyticConditionDisplayDescription)]
        [MaxLength(300, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Description { get; set; } = null!;
    }
}
