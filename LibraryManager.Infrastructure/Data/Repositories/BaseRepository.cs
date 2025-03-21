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

    public async Task Remove(T aggregateRoot)
    {
        var result = await _dbSet
            .FirstOrDefaultAsync(c => c.Id == aggregateRoot.Id);

        if (result is not null)
            _dbSet.Remove(result);
    }

    public async Task<T?> GetById(int id) =>
        await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
    
    public async Task<T?> GetSingle(
        Expression<Func<T, bool>> predicate = null!,
        IEnumerable<Expression<Func<T, object>>> includeProperties = null!)
    {
        var query = _dbSet.AsNoTracking();

        if (predicate is not null)
            query = query.Where(predicate);

        if (includeProperties is null) 
            return await query.FirstOrDefaultAsync();

        query = includeProperties.Aggregate(query, (current, property)
            => current.Include(property));

        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate = null!,
        IEnumerable<Expression<Func<T, object>>> includeProperties = null!)
    {
        var query = _dbSet.AsNoTracking();
        
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