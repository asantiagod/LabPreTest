using LabPreTest.Shared.Enums;

namespace LabPreTest.Shared.Interfaces
{
    public interface IAuditRecord
    {
        int EntityId { get; }
        ChangeType ChangeType { get; set; }
        DateTime ChangeDate { get; set; }
        string ChangeBy { get; set; }
        string OldValues { get; set; } // Old values in JSON format
        string NewValues { get; set; } // New values in JSON format
    }
}