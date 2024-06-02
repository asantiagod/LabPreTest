using System.ComponentModel.DataAnnotations;
using LabPreTest.Shared.Interfaces;
using LabPreTest.Shared.Messages;

namespace LabPreTest.Shared.Entities
{
    public class Section : IEntityWithId, IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = EntityMessages.SectionDisplayName)]
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Name { get; set; } = null!;
    }
}