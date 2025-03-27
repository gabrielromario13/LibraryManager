using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Data.Repositories;

public class LoanRepository(AppDbContext context)
    : BaseRepository<Loan>(context), ILoanRepository
{
}