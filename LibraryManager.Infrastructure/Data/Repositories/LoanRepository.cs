using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Data.Repositories;

public class LoanRepository(AppDbContext context) : BaseRepository<Loan>(context), ILoanRepository
{
    public async Task<List<Loan>> GetAll()
    {
        var loans = await context.Loans.Where(l => l.IsActive)
            .Include(l => l.User)
            .Include(l => l.Book)
            .ToListAsync();
        return loans;
    }

    public async Task<Loan?> GetDetailsById(int id)
    {
        var loans = await context.Loans
            .Include(l => l.User)
            .Include(l => l.Book)
            .FirstOrDefaultAsync(l => l.Id == id);
        return loans;
    }
}