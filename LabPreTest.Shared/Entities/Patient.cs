using LabPreTest.Shared.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPreTest.Shared.Entities
{
    public class Medician
    {
        public int Id { get; set; }

  
        [MaxLength(20, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string DocumentId { get; set; }

        [Display(Name = EntityMessages.MedicianDisplayName)]
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Name { get; set; }


        [DataType(DataType.Date)]
        public string BirthDay { get; set; }
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [DataType(DataType.PhoneNumber)]
        public int Cellphone { get; set; }
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        public string Address { get; set; }
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        public string UserName { get; set; }

    }
}
