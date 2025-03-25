using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Auth;
using LibraryManager.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Data.Repositories;

public class UserRepository(AppDbContext context, IAuthService auth)
    : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> GetDetailsById(int id) =>
        await context.Users
            .Include(u => u.Loans)
            .ThenInclude(l => l.Book)
            .SingleOrDefaultAsync(u => u.Id == id);

    public async Task UpdatePassword(User user, string newPassword)
    {
        user.UpdatePassword(newPassword);
        await context.SaveChangesAsync();
    }

    public async Task<User?> AuthenticateUser(string email, string password)
    {
        var hashPassword = auth.ComputeHash(password);
        return await context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == hashPassword);
    }
}