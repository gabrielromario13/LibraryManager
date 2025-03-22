using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.ViewModels;

public class BookViewModel(
    int id,
    string title,
    string author,
    string isbn,
    int publishedYear,
    int availableCopies,
    string status,
    int totalLoans)
{
    public int Id { get; private set; } = id;
    public string Title { get; private set; } = title;
    public string Author { get; private set; } = author;
    public string Isbn { get; private set; } = isbn;
    public int PublishedYear { get; private set; } = publishedYear;
    public int AvailableCopies { get; private set; } = availableCopies;
    public string Status { get; private set; } = status;
    public int TotalLoans { get; private set; } = totalLoans;

    public static BookViewModel FromEntity(Book book)
    {
        var activeLoans = book.Loans?.Count(loan => loan.IsActive) ?? 0;
        
        return new(
            book.Id,
            book.Title,
            book.Author,
            book.Isbn,
            book.PublishedYear,
            book.AvailableCopies,
            book.Status.ToString(),
            activeLoans);
    }
}