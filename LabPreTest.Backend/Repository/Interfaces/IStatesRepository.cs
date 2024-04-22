using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.Repository.Interfaces
{
    public interface IStatesRepository
    {
        Task<ActionResponse<IEnumerable<State>>> GetAsync();
        Task<ActionResponse<State>> GetAsync(int id);
    }
}
