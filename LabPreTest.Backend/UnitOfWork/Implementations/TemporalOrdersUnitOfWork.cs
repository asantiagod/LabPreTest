using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.UnitOfWork.Implementations
{
    public class TemporalOrdersUnitOfWork : GenericUnitOfWork<TemporalOrder>, ITemporalOrdersUnitOfWork
    {
        private readonly ITemporalOrdersRepository _temporalOrdersRepository;

        public TemporalOrdersUnitOfWork(IGenericRepository<TemporalOrder> repository, ITemporalOrdersRepository temporalOrdersRepository) : base(repository)
        {
            _temporalOrdersRepository = temporalOrdersRepository;
        }

        public Task<ActionResponse<TemporalOrdersDTO>> AddFullAsync(string email, TemporalOrdersDTO temporalOrderDTO) => _temporalOrdersRepository.AddFullAsync(email, temporalOrderDTO);

        public Task<ActionResponse<bool>> DeleteAllAsync(string email) => _temporalOrdersRepository.DeleteAllAsync(email);

        public Task<ActionResponse<IEnumerable<TemporalOrder>>> GetAsync(string email) => _temporalOrdersRepository.GetAsync(email);

        public Task<ActionResponse<int>> GetCountAsync(string email) => _temporalOrdersRepository.GetCountAsync(email);

        public Task<ActionResponse<TemporalOrdersDTO>> UpdateAsync(string email, TemporalOrdersDTO temporalOrderDTO) => _temporalOrdersRepository.UpdateAsync(email, temporalOrderDTO);
    }
}