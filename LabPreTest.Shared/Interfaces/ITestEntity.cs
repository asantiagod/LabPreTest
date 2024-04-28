using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPreTest.Shared.Interfaces
{
    public interface ITestEntity
    {
        int TestID { get; set; }
        string Name { get; set; }
        string Recipient { get; set; }
        string Conditions { get; set; }
        string Section { get; set; }
    }
}
