
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

    }
}
