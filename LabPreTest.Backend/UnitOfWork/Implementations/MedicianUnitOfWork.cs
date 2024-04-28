using LabPreTest.Backend.Repository.Implementations;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.UnitOfWork.Implementations
{
    public class MediciansUnitOfWork : GenericUnitOfWork<Medic>, IMedicianUnitOfWork
    {
        private readonly IMedicianRepository _medicianRepository;

        public MediciansUnitOfWork(IGenericRepository<Medic> repository, IMedicianRepository medicianRepository) : base(repository)
        {
            _medicianRepository = medicianRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Medic>>> GetAsync() => await _medicianRepository.GetAsync();

        public override async Task<ActionResponse<Medic>> GetAsync(int id) => await _medicianRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<Medic>>> GetAsync(PagingDTO paging) => await _medicianRepository.GetAsync(paging);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging) => await _medicianRepository.GetTotalPagesAsync(paging);
    }
}