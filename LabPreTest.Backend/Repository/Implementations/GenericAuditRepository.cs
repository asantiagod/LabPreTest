using LabPreTest.Backend.Data;
using LabPreTest.Backend.Helpers;
using LabPreTest.Backend.Repository.Interfaces;
using LabPreTest.Shared.DTO;
using LabPreTest.Shared.Messages;
using LabPreTest.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace LabPreTest.Backend.Repository.Implementations
{
    public class GenericAuditRepository<T> : GenericRepository<T>, IGenericAuditRepository<T> where T : class
    {
        public GenericAuditRepository(DataContext context) : base(context)
        {
        }

        public override Task<ActionResponse<T>> AddAsync(T model)
        {
            throw new NotImplementedException();
        }

        public override Task<ActionResponse<T>> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public override Task<ActionResponse<T>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}