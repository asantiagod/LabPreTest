using LabPreTest.Backend.Repository.Implementations;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Backend.UnitOfWork.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;

namespace LabPreTest.Backend.UnitOfWork.Implementations
{
    public class PatientUnitOfWork : GenericUnitOfWork<Patient>, IPatientUnitOfWork
    {
        private readonly IPatientRepository _patientRepository;

        public PatientUnitOfWork(IGenericRepository<Patient> repository, IPatientRepository patientRepository) : base(repository)
        {
            _patientRepository = patientRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Patient>>> GetAsync() => await _patientRepository.GetAsync();

        public override async Task<ActionResponse<Patient>> GetAsync(int id) => await _patientRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<Patient>>> GetAsync(PagingDTO paging) => await _patientRepository.GetAsync(paging);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging) => await _patientRepository.GetTotalPagesAsync(paging);
    }
}