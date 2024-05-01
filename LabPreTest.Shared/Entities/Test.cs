using LabPreTest.Shared.Interfaces;
using LabPreTest.Shared.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPreTest.Shared.Entities
{
    public class Test : IEntityWithId, ITestEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public int TestID {  get; set; }
        [Display(Name = EntityMessages.PatientDisplayName)]
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Section { get; set; }
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string? Recipient { get; set; }
        [MaxLength(1000, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string? Conditions { get; set; }

    }
}
