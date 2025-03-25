using System.Linq.Expressions;
using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Data.Repositories;

public class BaseRepository<T>(DbContext context)
    : IBaseRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task Add(T aggregateRoot)
    {
        await _dbSet.AddAsync(aggregateRoot);
        await context.SaveChangesAsync();
    }

    public async Task Update(T aggregateRoot)
    {
        _dbSet.Update(aggregateRoot);
        await context.SaveChangesAsync();
    }

    public async Task<T?> GetById(int id) =>
        await _dbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id && t.IsActive);
    
    public async Task<T?> GetSingle(
        Expression<Func<T, bool>> predicate = null!,
        IEnumerable<Expression<Func<T, object>>> includeProperties = null!)
    {
        var query = _dbSet.AsNoTracking().Where(t => t.IsActive);

        if (predicate is not null)
            query = query.Where(predicate);

        if (includeProperties is null) 
            return await query.FirstOrDefaultAsync();

        query = includeProperties.Aggregate(query, (current, property)
            => current.Include(property));

        return await query.FirstOrDefaultAsync();
    }

    public virtual async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate = null!,
        IEnumerable<Expression<Func<T, object>>> includeProperties = null!)
    {
        var query = _dbSet.AsNoTracking().Where(t => t.IsActive);
        
        if (predicate is not null)
            query = query.Where(predicate);
        
        if (includeProperties is null) 
            return await query.ToListAsync();

        query = includeProperties.Aggregate(query, (current, property)
            => current.Include(property));

        return await query.ToListAsync();
    }
    
    public async Task<bool> Exists(Expression<Func<T, bool>> predicate) =>
        await _dbSet.AsNoTracking().Where(predicate).AnyAsync();
}