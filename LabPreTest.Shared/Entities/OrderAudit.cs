using LabPreTest.Shared.Enums;
using LabPreTest.Shared.Interfaces;

namespace LabPreTest.Shared.Entities
{
    public class OrderAudit : IEntityWithId
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public ChangeType ChangeType { get; set; }
        public DateTime ChangeDate { get; set; }
        public string ChangeBy { get; set; } = null!;
        public string OldValues { get; set; } = null!; // Old values in JSON format
        public string NewValues { get; set; } = null!; // New values in JSON format
    }
}