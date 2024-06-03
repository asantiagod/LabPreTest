namespace LabPreTest.Shared.Interfaces
{
    public interface IEntityWithDescription : IEntityWithName
    {
        string Description { get; set; }
    }
}