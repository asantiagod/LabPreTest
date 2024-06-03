using LabPreTest.Shared.Entities;

namespace LabPreTest.Shared.Interfaces
{
    public interface ITestEntity
    {
        int TestID { get; set; }
        string Name { get; set; }
        string Recipient { get; set; }
        ICollection<TestCondition>? Conditions { get; set; }
        Section Section { get; set; }
        //int SectionId { get; set; }
        TestTube TestTube { get; set; }
        int TestTubeId { get; set; }
    }
}