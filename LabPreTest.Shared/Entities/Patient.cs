using LabPreTest.Shared.Messages;
using LabPreTest.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPreTest.Shared.Entities
{
    public class Patient : IEntityWithId, IUserEntity
    {
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string DocumentId { get; set; } = null!;

        [Display(Name = EntityMessages.PatientDisplayName)]
        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string Name { get; set; } = null!;

        [DataType(DataType.Date)]
        public string? BirthDay { get; set; }

        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [DataType(DataType.PhoneNumber)]
        public string? Cellphone { get; set; }

        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        public string? Address { get; set; }

        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [MaxLength(100, ErrorMessage = EntityMessages.MaxLengthErrorMessage)]
        public string? UserName { get; set; }

        public ICollection<Order>? Orders { get; set; }

        [Display(Name = EntityMessages.StatesCitiesDisplayMessage)]
        public int OrdersNumber => Orders == null || Orders.Count == 0 ? 0 : Orders.Count;
        
        public ICollection<TemporalOrder>? TemporalOrders { get; set; }
    }
}