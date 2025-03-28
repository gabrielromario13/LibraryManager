using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Data.Repositories;

public class PenaltyRepository(AppDbContext context)
    : BaseRepository<Penalty>(context), IPenaltyRepository
{
    public async Task<Penalty?> GetDetailsById(int id) =>
        await context.Penalties
            .Where(l => l.IsActive)
            .Include(l => l.User)
            .Include(l => l.Loan)
            .FirstOrDefaultAsync(l => l.Id == id);
    
    public async Task<IEnumerable<Penalty>> GetAll() =>
        await context.Penalties
            .Where(l => l.IsActive)
            .Include(l => l.User)
            .Include(l => l.Loan)
            .ToListAsync();

    public async Task<IEnumerable<Penalty>> GetAllByUser(int userId) =>
        await context.Penalties
            .Where(l => l.IsActive && l.Paid == false && l.UserId == userId)
            .Include(l => l.Loan)
            .ThenInclude(l => l.User)
            .ToListAsync();
}