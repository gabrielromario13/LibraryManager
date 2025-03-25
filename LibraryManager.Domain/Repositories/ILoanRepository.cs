using LibraryManager.Domain.Entities;

namespace LibraryManager.Domain.Repositories;

public interface ILoanRepository : IBaseRepository<Loan>
{
    Task<Loan?> GetDetailsById(int id);
}