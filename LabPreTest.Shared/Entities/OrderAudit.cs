using LabPreTest.Shared.Enums;
using LabPreTest.Shared.Interfaces;
using System.Text.Json.Serialization;

namespace LabPreTest.Shared.Entities
{
    public class OrderAudit : IAuditRecord
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        [JsonIgnore]
        public int EntityId => OrderId;

        public ChangeType ChangeType { get; set; }
        public DateTime ChangeDate { get; set; }
        public string ChangeBy { get; set; } = null!;
        public string OldValues { get; set; } = null!; // Old values in JSON format
        public string NewValues { get; set; } = null!; // New values in JSON format
    }
}