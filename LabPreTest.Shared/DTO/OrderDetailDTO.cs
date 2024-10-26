using LabPreTest.Shared.Enums;

namespace LabPreTest.Shared.DTO
{
    public class OrderDetailDTO
    {
        public int OrderId {  get; set; }
        public int? TestId { get; set; }
        public int? MedicId { get; set; }
        public int? PatientId { get; set; }
        public OrderStatus? Status { get; set; }
    }
}