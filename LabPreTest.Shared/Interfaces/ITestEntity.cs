using LabPreTest.Shared.Entities;
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
        ICollection<TestCondition>? Conditions { get; set; }
        Section Section { get; set; }
    }
}
