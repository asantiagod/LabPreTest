namespace LabPreTest.Shared.Entities
{
    public class TestCondition
    {
        public int Id { get; set; }

        public PreanalyticCondition? Condition { get; set; }
        public int ConditionId { get; set; }

        public Test? Test { get; set; }
        public int TestId { get; set; }
    }
}