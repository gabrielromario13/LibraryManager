using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Data.Repositories;

public class BookRepository(AppDbContext context)
    : BaseRepository<Book>(context), IBookRepository
{
    public async Task<Book?> GetDetailsById(int id)
    {
        var books = await context.Books
            .Include(b => b.Loans)
            .SingleOrDefaultAsync(b => b.Id == id);
        return books;
    }
}