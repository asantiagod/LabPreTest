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
    public class MedicianRepository : GenericRepository<Medic>, IMedicianRepository
    {
        private readonly DataContext _context;

        public MedicianRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<Medic>>> GetAsync()
        {
            var medician = await _context.Medicians
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Medic>>
            {
                WasSuccess = true,
                Result = medician
            };
        }

        public override async Task<ActionResponse<Medic>> GetAsync(int id)
        {
            var medician = await _context.Medicians
                .FirstOrDefaultAsync(c => c.Id == id);

            if (medician == null)
            {
                return new ActionResponse<Medic>
                {
                    WasSuccess = false,
                    Message = MessageStrings.DbCountryNotFoundMessage
                };
            }

            return new ActionResponse<Medic>
            {
                WasSuccess = true,
                Result = medician
            };
        }

        public override async Task<ActionResponse<IEnumerable<Medic>>> GetAsync(PagingDTO paging)
        {
            var queryable = _context.Medicians
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(paging.Filter))
                queryable = queryable.Where(x => x.Name.ToLower().Contains(paging.Filter.ToLower()));

            return new ActionResponse<IEnumerable<Medic>>
            {
                WasSuccess = true,
                Result = await queryable
                .OrderBy(x => x.Name)
                .Paginate(paging)
                .ToListAsync()
            };
        }

        public async Task<ActionResponse<Medic>> GetAsync(string documentId)
        {
            var medician = await _context.Medicians
               .FirstOrDefaultAsync(c => c.DocumentId == documentId);

            if (medician == null)
                return ActionResponse<Medic>.BuildFailed(MessageStrings.DbRecordNotFoundMessage);

            return ActionResponse<Medic>.BuildSuccessful(medician);
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging)
        {
            var queryable = _context.Medicians.AsQueryable();

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