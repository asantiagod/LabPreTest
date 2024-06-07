using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPreTest.Shared.Enums
{
    public enum OrderStatus
    {
        [Description("Asignada")]
        Idle,
        
        [Description("En proceso")]
        InProcess,
        
        [Description("Cerrada")]
        Closed,
        
        [Description("Cancelada")]
        Canceled
    }
}
