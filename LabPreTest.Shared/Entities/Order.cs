using LabPreTest.Shared.Interfaces;
using LabPreTest.Shared.Messages;
using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.Entities
{
    public class Order : IOrderEntity
    {
        public int Id { get; set; }

        [Display(Name = EntityMessages.OrderPatientIdDisplayName)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public int patientId { get; set; }

        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string patientName { get; set; } 

        [Display(Name = EntityMessages.OrderMedicIdDisplayName)]

        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public string medicName { get; set; }
        [DataType(DataType.Date)]
        public DateTime createdAt { get; set; }
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public List<int> TestIds { get; set; } = null!;

        public int TestNumber => TestIds == null || TestIds.Count == 0 ? 0 : TestIds.Count();
    }
}