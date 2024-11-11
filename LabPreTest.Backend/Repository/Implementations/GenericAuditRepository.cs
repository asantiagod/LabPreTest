using LabPreTest.Backend.Data;
using LabPreTest.Backend.Helpers;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Interfaces;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace LabPreTest.Backend.Repository.Implementations
{
    public class GenericAuditRepository<T> : IGenericAuditRepository<T> where T : class, IAuditRecord
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _entity;

        public GenericAuditRepository(DataContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public async Task<ActionResponse<IEnumerable<T>>> GetAsync()
        {
            var tList = await _entity.AsNoTracking().ToListAsync();
            return ActionResponse<IEnumerable<T>>.BuildSuccessful(tList);
        }

        public async Task<ActionResponse<T>> GetAsync(int id)
        {
            var t = await _entity.FindAsync(id);
            if (t == null)
                return ActionResponse<T>.BuildFailed(MessageStrings.DbRecordNotFoundMessage);
            return ActionResponse<T>.BuildSuccessful(t);
        }

        public async Task<ActionResponse<IEnumerable<T>>> GetAsync(PagingDTO pagingDTO)
        {
            var queryable = _entity.AsQueryable();

            if (pagingDTO.Id > 0)
            {
                var idProperty = GetIdProperty();
                if (idProperty != null)
                {
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var propertyAccess = Expression.MakeMemberAccess(parameter, idProperty);
                    var constant = Expression.Constant(pagingDTO.Id);
                    var equals = Expression.Equal(propertyAccess, constant);
                    var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

                    queryable = queryable.Where(lambda);
                }
                else
                {
                    return ActionResponse<IEnumerable<T>>.BuildFailed($"No se encontró una propiedad de ID para el tipo {typeof(T).Name}.");
                }
            }

            var result = await queryable
                .Paginate(pagingDTO)
                .ToListAsync();

            return ActionResponse<IEnumerable<T>>.BuildSuccessful(result);
        }

        public async Task<ActionResponse<int>> GetTotalPagesAsync(PagingDTO paginDTO)
        {
            //TODO: check if is necessary to filter by EntityID
            var queryable = _entity.AsQueryable();
            var count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling((double)count / paginDTO.RecordsNumber);
            return ActionResponse<int>.BuildSuccessful(totalPages);
        }

        private PropertyInfo GetIdProperty()
        {
            var typeName = typeof(T).Name;
            if (typeof(T) != typeof(OrderAudit))
                throw new NotImplementedException();

            return typeof(T).GetProperty("OrderId");
        }
    }
}