using System.ComponentModel;

namespace LabPreTest.Shared.Enums
{
    public enum ChangeType
    {
        [Description("Creada")]
        Insert,
        [Description("Actualizada")]
        Update,
        [Description("Borrada")]
        Delete
    }
}