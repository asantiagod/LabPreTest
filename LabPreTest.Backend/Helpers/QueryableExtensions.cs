using LabPreTest.Shared.DTO;

namespace LabPreTest.Backend.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PagingDTO paging)
        {
            return queryable
                .Skip((paging.Page - 1) * paging.RecordsNumber)
                .Take(paging.RecordsNumber);
        }
    }
}