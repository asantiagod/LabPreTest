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
            return new ActionResponse<IEnumerable<Patient>>
            {
                WasSuccess = true,
                Result = patient
            };
        }

        public override async Task<ActionResponse<Patient>> GetAsync(int id)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(c => c.Id == id);
            if (patient == null)
            {
                return new ActionResponse<Patient>
                {
                    WasSuccess = false,
                    Message = MessageStrings.DbCountryNotFoundMessage
                };
            }

            return new ActionResponse<Patient>
            {
                WasSuccess = true,
                Result = patient
            };
        }

        public override async Task<ActionResponse<IEnumerable<Patient>>> GetAsync(PagingDTO paging)
        {
            var queryable = _context.Patients
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            return new ActionResponse<IEnumerable<Patient>>
            {
                WasSuccess = true,
                Result = await queryable
                .OrderBy(x => x.Name)
                .Paginate(paging)
                .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging)
        {
            var queryable = _context.Patients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            int count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling((double)count / paging.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };

        }

    }
}