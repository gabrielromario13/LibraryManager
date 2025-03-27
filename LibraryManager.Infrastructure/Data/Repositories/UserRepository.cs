using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Auth;
using LibraryManager.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Data.Repositories;

public class UserRepository(AppDbContext context)
    : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> GetDetailsById(int id) =>
        await context.Users
            .Include(u => u.Loans)
            .ThenInclude(l => l.Book)
            .SingleOrDefaultAsync(u => u.Id == id);

    public async Task<bool> EmailExists(string email) =>
        await context.Users.AnyAsync(u => u.Email == email);
}