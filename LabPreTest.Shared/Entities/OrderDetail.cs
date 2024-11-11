using LabPreTest.Shared.Enums;
using System.Text.Json.Serialization;

namespace LabPreTest.Shared.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [JsonIgnore]
        public Order? Order { get; set; }

        public int OrderId { get; set; }

        [JsonIgnore]
        public Test? Test { get; set; }

        public int TestId { get; set; }

        [JsonIgnore]
        public Medic? Medic { get; set; }

        public int MedicId { get; set; }

        [JsonIgnore]
        public Patient? Patient { get; set; }

        public int PatientId { get; set; }
        public OrderStatus? Status { get; set; }
    }
}