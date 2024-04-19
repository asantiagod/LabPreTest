using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.Repository.Interfaces
{
    public interface ICountriesRepository
    {
        Task<ActionResponse<IEnumerable<Country>>> GetAsync();

        Task<ActionResponse<Country>> GetAsync(int id);

        // TODO: get with paging information
        //....
    }
}
