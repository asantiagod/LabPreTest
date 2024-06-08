using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.UnitOfWork.Interfaces
{
    public interface ITemporalOrdersUnitOfWork
    {
        Task<ActionResponse<TemporalOrdersDTO>> AddFullAsync(string email, TemporalOrdersDTO temporalOrderDTO);
        Task<ActionResponse<IEnumerable<TemporalOrder>>> GetAsync(string email);
        Task<ActionResponse<int>> GetCountAsync(string email);
    }
}
