using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Messages;
using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.DTO
{
    public class UserDTO : User
    {
        [DataType(DataType.Password)]
        [Display(Name = EntityMessages.PasswordDiplayName)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = EntityMessages.StringLengthRangeErrorMessage)]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = EntityMessages.PasswordConfirmErrorMessage)]
        [Display(Name = EntityMessages.PasswordConfirmDiplayName)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = EntityMessages.StringLengthRangeErrorMessage)]
        public string PasswordConfirm { get; set; } = null!;
    }
}