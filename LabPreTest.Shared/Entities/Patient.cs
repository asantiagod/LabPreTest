using LabPreTest.Shared.Enums;
using LabPreTest.Shared.Interfaces;
using LabPreTest.Shared.Messages;
using System.ComponentModel.DataAnnotations;

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

        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public GenderType Gender { get; set; }

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

        public ICollection<TemporalOrder>? TemporalOrders { get; set; }

        public ICollection<OrderDetail>? Details { get; set; }
    }
}