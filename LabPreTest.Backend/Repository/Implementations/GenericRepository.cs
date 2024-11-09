using LabPreTest.Backend.Data;
using LabPreTest.Backend.Helpers;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace LabPreTest.Backend.Repository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _entity;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public virtual async Task<ActionResponse<T>> AddAsync(T entity)
        {
            _context.Add(entity);
            return await SaveContextChangesAsync(entity);
        }

        public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            if (row == null)
                return ActionResponse<T>.BuildFailed(MessageStrings.DbRecordNotFoundMessage);

            try
            {
                // TODO: check if the next row can be taken out of the try-catch
                _entity.Remove(row);
                await _context.SaveChangesAsync();
                return ActionResponse<T>.BuildSuccessful(row);
            }
            catch
            {
                return ActionResponse<T>.BuildFailed(MessageStrings.DbDeleteErrorMessage);
            }
        }

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync()
        {
            var result = await _entity.AsNoTracking().ToListAsync();
            return ActionResponse<IEnumerable<T>>.BuildSuccessful(result);
        }

        public virtual async Task<ActionResponse<T>> GetAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            if (row == null)
                return ActionResponse<T>.BuildFailed(MessageStrings.DbRecordNotFoundMessage);

            return ActionResponse<T>.BuildSuccessful(row);
        }

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PagingDTO paging)
        {
            var result = await _entity.AsQueryable()
                               .Paginate(paging).ToListAsync();
            return ActionResponse<IEnumerable<T>>.BuildSuccessful(result);
        }

        public virtual async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paging)

        {
            var queryable = _entity.AsQueryable();
            var count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling((double)count / paging.RecordsNumber);
            return ActionResponse<int>.BuildSuccessful(totalPages);
        }

        public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
        {
            _context.Update(entity);
            return await SaveContextChangesAsync(entity);
        }

        protected ActionResponse<T> DbUpdateExceptionActionResponse()
        {
            return ActionResponse<T>.BuildFailed(MessageStrings.DbUpdateExceptionMessage);
        }

        protected ActionResponse<T> ExceptionActionResponse(Exception ex)
        {
            return ActionResponse<T>.BuildFailed(ex.Message);
        }

        protected async Task<ActionResponse<DTO>> SaveContextChangesAsync<DTO>(DTO result)
        {
            try
            {
                await _context.SaveChangesAsync();
                return ActionResponse<DTO>.BuildSuccessful(result);
            }
            catch(DbUpdateException)
            {
                return ActionResponse<DTO>.BuildFailed(MessageStrings.DbUpdateExceptionMessage);
            }
            catch (Exception ex)
            {
                return ActionResponse<DTO>.BuildFailed(ex.Message);
            }
        }
    }
}