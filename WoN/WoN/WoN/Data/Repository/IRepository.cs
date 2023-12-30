using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WoN.Data.Repository;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAsync(params Expression<Func<T, bool>>[]? filter);
}

public class AbstractRepository<T>(DbContext context) : IRepository<T> where T : class
{
    public async Task<IEnumerable<T>> GetAsync(params Expression<Func<T, bool>>[]? filter)
    {
        var query = context.Set<T>().AsQueryable();
        if (filter is null) return await query.ToListAsync();

        query = filter.Aggregate(query, (current, f) => current.Where(f));
        return await query.ToListAsync();
    }
}