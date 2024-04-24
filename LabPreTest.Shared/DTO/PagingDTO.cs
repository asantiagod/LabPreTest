namespace LabPreTest.Shared.DTO
{
    public class PagingDTO
    {
        public int Id { get; set; }
        public int Page { get; set; } = 1;
        public int RecordsNumber { get; set; } = 10;
        public string? Filter { get; set; }
    }
}