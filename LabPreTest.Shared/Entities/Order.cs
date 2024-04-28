using LabPreTest.Shared.Messages;
using System.ComponentModel.DataAnnotations;

namespace LabPreTest.Shared.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = EntityMessages.OrderPatientIdDisplayName)]
        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public int patientId { get; set; }

        [Display(Name = EntityMessages.OrderMedicIdDisplayName)]
        public int medicId { get; set; }

        [Required(ErrorMessage = EntityMessages.RequiredErrorMessage)]
        public ICollection<int> TestIds { get; set; } = null!;

        public int TestNumber => TestIds == null || TestIds.Count == 0 ? 0 : TestIds.Count();
    }
}