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
        NotDefined,

        [Description("Asignada")]
        OrdenCreada,

        [Description("En proceso")]
        OrdenEnProceso,

        [Description("Cerrada")]
        OrdenFinalizada,

        [Description("Cancelada")]
        OrdenAnulada
    }
}