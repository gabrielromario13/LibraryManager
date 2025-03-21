using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.ViewModels;

public class BookViewModel(int id, string title, string author, int publishedYear, int totalLoans)
{
    public int Id { get; private set; } = id;
    public string Title { get; private set; } = title;
    public string Author { get; private set; } = author;
    public int PublishedYear { get; private set; } = publishedYear;
    public int TotalLoans { get; private set; } = totalLoans;

    public static BookViewModel FromEntity(Book book)
    {
        var loansActive = book.Loans?.Count(loan => loan.IsActive) ?? 0;
        return new(book.Id, book.Title, book.Author, book.PublishedYear, loansActive);
    }
}