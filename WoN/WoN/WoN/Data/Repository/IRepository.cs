using System.Linq.Expressions;

namespace WoN.Data.Repository;

public interface IRepository<T>
{
    IEnumerable<T> GetAsync(IFilter<T>? filter = null);
}

public interface IFilter<T>
{
    Expression<Func<T, bool>> GetFilter();
}

