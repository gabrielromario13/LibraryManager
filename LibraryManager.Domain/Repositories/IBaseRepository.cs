using System.Linq.Expressions;
using LibraryManager.Domain.Entities;

namespace LibraryManager.Domain.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task Add(T aggregateRoot);
    Task Update(T aggregateRoot);
    Task Remove(T aggregateRoot);
    Task<T?> GetById(int id);
    Task<T?> GetSingle(Expression<Func<T, bool>> predicate = null!,
        IEnumerable<Expression<Func<T, object>>> includeProperties = null!);
    Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate = null!,
        IEnumerable<Expression<Func<T, object>>> includeProperties = null!);
    Task<bool> Exists(Expression<Func<T, bool>> predicate);

}