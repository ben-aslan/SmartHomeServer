using Core.Entities;
using System.Linq.Expressions;

namespace Core.DataAccess;

public interface IEntityRepository<T> where T : class, IEntity, new()
{
    T First(Expression<Func<T, bool>> filter);
    T? FirstOrDefault(Expression<Func<T, bool>> filter);
    T? GetOrDefault(Expression<Func<T, bool>> filter);
    T Get(Expression<Func<T, bool>> filter);
    bool Any(Expression<Func<T, bool>> filter);
    IList<T> GetList(Expression<Func<T, bool>> filter = null!);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void AddRange(IEnumerable<T> entities);
    void RemoveRange(IEnumerable<T> entities);

    Task<T> FirstAsync(Expression<Func<T, bool>> filter);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter);
    Task<T?> GetOrDefaultAsync(Expression<Func<T, bool>> filter);
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
    Task<IList<T>> GetListAsync(Expression<Func<T, bool>> filter = null!);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task RemoveRangeAsync(IEnumerable<T> entities);
}
