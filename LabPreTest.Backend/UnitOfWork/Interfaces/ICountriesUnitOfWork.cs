using LabPreTest.Shared.Responses;
using LabPreTest.Shared.Entities;

namespace LabPreTest.Backend.UnitOfWork.Interfaces
{
    public interface ICountriesUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Country>>> GetAsync();

        Task<ActionResponse<Country>> GetAsync(int id);

        // TODO: get with paging information
        //....
    }
}