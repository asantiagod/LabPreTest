using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Interfaces;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.UnitOfWork.Implementations
{
    public class GenericAuditUnitOfWork<T> : IGenericAuditUnitOfWork<T> where T : class, IAuditRecord
    {
        private readonly IGenericAuditRepository<T> _repository;

        public GenericAuditUnitOfWork(IGenericAuditRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<ActionResponse<IEnumerable<T>>> GetAsync() => _repository.GetAsync();

        public Task<ActionResponse<IEnumerable<T>>> GetAsync(PagingDTO pagingDTO) => _repository.GetAsync(pagingDTO);

        public Task<ActionResponse<T>> GetAsync(int id) => _repository.GetAsync(id);

        public Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO pagination) => _repository.GetTotalPagesAsync(pagination);
    }
}