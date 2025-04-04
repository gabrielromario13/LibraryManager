using LibraryManager.Domain.Entities;

namespace LibraryManager.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetDetailsById(int id);
    Task<bool> EmailExists(string email);
}