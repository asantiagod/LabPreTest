using LabPreTest.Backend.Data;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace LabPreTest.Backend.Repository.Implementations
{
    public class TemporalOrderRepository : GenericRepository<TemporalOrder>, ITemporalOrdersRepository
    {
        private readonly DataContext _context;
        private readonly IUsersRepository _usersRepository;

        public TemporalOrderRepository(DataContext context, IUsersRepository usersRepository) : base(context)
        {
            _context = context;
            _usersRepository = usersRepository;
        }

        public async Task<ActionResponse<TemporalOrdersDTO>> AddFullAsync(string email, TemporalOrdersDTO temporalOrderDTO)
        {
            var test = await _context.Tests.FirstOrDefaultAsync(t => t.Id == temporalOrderDTO.TestId);
            if (test == null)
                return GetErrorActionResponse("El examen no existe");

            var medician = await _context.Medicians.FirstOrDefaultAsync(m => m.Id == temporalOrderDTO.MedicId);
            if (medician == null)
                return GetErrorActionResponse("El medico no existe");

            var patient = await _context.Patients.FirstOrDefaultAsync(m => m.Id == temporalOrderDTO.PatientId);
            if (patient == null)
                return GetErrorActionResponse("El paciente no encontrado");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return GetErrorActionResponse("El usuario no existe");

            var temporalOrder = new TemporalOrder
            {
                User = user,
                Test = test,
                Medic = medician,
                Patient = patient
            };

            try
            {
                _context.Add(temporalOrder);
                await _context.SaveChangesAsync();
                return new ActionResponse<TemporalOrdersDTO>
                {
                    WasSuccess = true,
                    Result = temporalOrderDTO
                };
            }
            catch (Exception ex)
            {
                return GetErrorActionResponse(ex.Message);
            }
        }

        public async Task<ActionResponse<IEnumerable<TemporalOrder>>> GetAsync(string email)
        {
            var temporalOrders = await _context.TemporalOrders
                .Include(to => to.User)
                .Include(to => to.Test)
                .Include(to => to.Medic)
                .Include(to => to.Patient)
                .Where(to => to.User!.Email == email)
                .ToListAsync();

            return new ActionResponse<IEnumerable<TemporalOrder>>
            {
                WasSuccess = true,
                Result = temporalOrders
            };
        }

        public async Task<ActionResponse<int>> GetCountAsync(string email)
        {
            var count = await _context.TemporalOrders
                .Where(x => x.User!.Email == email)
                .CountAsync();
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = count
            };
        }

        private static ActionResponse<TemporalOrdersDTO> GetErrorActionResponse(string errorMessage)
        {
            return new ActionResponse<TemporalOrdersDTO>
            {
                WasSuccess = false,
                Message = errorMessage,
            };
        }
    }
}