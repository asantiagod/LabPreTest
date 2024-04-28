namespace LabPreTest.Shared.Interfaces
{
    public interface IOrderEntity
    {
        int patientId { get; set; }
        string patientName { get; set; }
        string medicName { get; set; }
        public DateTime createdAt { get; set; }
        public List<int> TestIds { get; set; }
    }
}