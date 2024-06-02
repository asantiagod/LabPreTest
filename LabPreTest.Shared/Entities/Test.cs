using LabPreTest.Shared.Interfaces;
using LabPreTest.Shared.Messages;
using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.Entities
{
    public class Test : IEntityWithId, ITestEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public int TestID { get; set; }

        [Display(Name = EntityMessages.PatientDisplayName)]
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Section { get; set; } = null!;

        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Recipient { get; set; } = null!;

        [MaxLength(1000, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Conditions { get; set; } = null!;
    }
}