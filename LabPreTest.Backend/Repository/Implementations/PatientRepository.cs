using LabPreTest.Backend.Data;
using LabPreTest.Backend.Helpers;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace LabPreTest.Backend.Repository.Implementations
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        private readonly DataContext _context;

        public PatientRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Patient>>> GetAsync()
        {
            var patient = await _context.Patients
                .OrderBy(x => x.Name)
                .ToListAsync();
            return ActionResponse<IEnumerable<Patient>>.BuildSuccessful(patient);
        }

        public override async Task<ActionResponse<Patient>> GetAsync(int id)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(c => c.Id == id);
            if (patient == null)
                return ActionResponse<Patient>.BuildFailed(MessageStrings.DbRecordNotFoundMessage);

            return ActionResponse<Patient>.BuildSuccessful(patient);
        }

        public override async Task<ActionResponse<IEnumerable<Patient>>> GetAsync(PagingDTO paging)
        {
            var queryable = _context.Patients
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            var result = await queryable
                        .OrderBy(x => x.Name)
                        .Paginate(paging)
                        .ToListAsync();
            return ActionResponse<IEnumerable<Patient>>.BuildSuccessful(result);
        }

        public async Task<ActionResponse<Patient>> GetAsync(string documentId)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.DocumentId == documentId);

            if (patient == null)
                return ActionResponse<Patient>.BuildFailed(MessageStrings.DbRecordNotFoundMessage);

            return ActionResponse<Patient>.BuildSuccessful(patient);
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging)
        {
            var queryable = _context.Patients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            int count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling((double)count / paging.RecordsNumber);
            return  ActionResponse<int>.BuildSuccessful(totalPages);
        }
    }
}