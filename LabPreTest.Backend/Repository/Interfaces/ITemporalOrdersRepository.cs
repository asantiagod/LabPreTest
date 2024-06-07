using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.Repository.Interfaces
{
    public interface ITemporalOrdersRepository
    {
        Task<ActionResponse<TemporalOrdersDTO>> AddFullAsync(string email, TemporalOrdersDTO temporalOrderDTO);
        Task<ActionResponse<IEnumerable<TemporalOrder>>> GetAsync(string email);
        Task<ActionResponse<int>> GetCountAsync(string email);
    }
}
