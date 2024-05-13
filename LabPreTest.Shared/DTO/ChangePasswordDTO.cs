using LabPreTest.Shared.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPreTest.Shared.DTO
{
    public class ChangePasswordDTO
    {
        [DataType(DataType.Password)]
        [Display(Name = EntityMessages.CurrentPassword)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        [MinLength(6, ErrorMessage = EntityMessages.MinLengthErrorMessage)]
        public string CurrentPassword { get; set; } = null! ;

        [DataType(DataType.Password)]
        [Display(Name = EntityMessages.NewPassword)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        [MinLength(6, ErrorMessage = EntityMessages.MinLengthErrorMessage)]
        public string NewPassword { get; set; } = null!;


        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = EntityMessages.PasswordConfirmErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        [MinLength(6, ErrorMessage = EntityMessages.MinLengthErrorMessage)]
        public string Confirm { get; set; } = null!;



    }
}
