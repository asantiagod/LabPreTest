    using LabPreTest.Shared.DTO;
    using LabPreTest.Shared.Entities;
    using LabPreTest.Shared.Responses;

    namespace LabPreTest.Backend.UnitOfWork.Interfaces
    {
        public interface IMedicianUnitOfWork
        {
            Task<ActionResponse<IEnumerable<Medic>>> GetAsync();

            Task<ActionResponse<Medic>> GetAsync(int id);

            Task<ActionResponse<IEnumerable<Medic>>> GetAsync(PagingDTO paging);

            Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO pagination);
        }
    }
