namespace LabPreTest.Shared.Entities
{
    public class TemporalOrder
    {
        public int Id { get; set; }

        public User? User { get; set; }
        public string? UserId { get; set; }

        public Test? Test { get; set; }
        public int TestId { get; set; }

        public Medic? Medic { get; set; }
        public int MedicId { get; set; }

        public Patient? Patient { get; set; }
        public int PatientId { get; set; }
    }
}