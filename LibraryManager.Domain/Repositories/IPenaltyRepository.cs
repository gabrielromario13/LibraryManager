using LibraryManager.Domain.Entities;

namespace LibraryManager.Domain.Repositories;

public interface IPenaltyRepository : IBaseRepository<Penalty>
{
    Task<Penalty?> GetDetailsById(int id);
    Task<IEnumerable<Penalty>> GetAll();
    Task<IEnumerable<Penalty>> GetAllByUser(int userId);
}