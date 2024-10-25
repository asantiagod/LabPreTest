using LabPreTest.Shared.Enums;

namespace LabPreTest.Shared.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public Order? Order { get; set; }
        public int OrderId { get; set; }

        public Test? Test { get; set; }
        public int TestId { get; set; }

        public Medic? Medic { get; set; }
        public int MedicId { get; set; }

        public Patient? Patient { get; set; }
        public int PatientId { get; set; }
        public OrderStatus? Status { get; set; }
    }
}