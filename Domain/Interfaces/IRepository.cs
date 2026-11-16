using System.Linq.Expressions;

namespace Lab11_MercyJanampa.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetByIdAsync(Guid id);

    Task<T?> FirstOrDefaultAsync(
        Expression<Func<T, bool>> predicate
    );

    Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate
    );

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task<int> SaveChangesAsync();
}