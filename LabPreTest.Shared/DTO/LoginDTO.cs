using LabPreTest.Shared.Messages;
using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        [EmailAddress(ErrorMessage = EntityMessages.InvalidEmailErrorMessage)]
        public string Email { get; set; } = null!;

        [Display(Name = EntityMessages.PasswordDiplayName)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        [MinLength(6, ErrorMessage =EntityMessages.MinLengthErrorMessage)]
        public string Password { get; set; } = null!;
    }
}