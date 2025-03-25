using LibraryManager.Domain.Entities;

namespace LibraryManager.Domain.Repositories;

public interface IBookRepository : IBaseRepository<Book>
{
    Task<Book?> GetDetailsById(int id);
}