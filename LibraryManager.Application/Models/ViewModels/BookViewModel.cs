using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.ViewModels;

public class BookViewModel(
    int id,
    string title,
    string author,
    string isbn,
    int publishedYear,
    int totalCopies,
    int availableCopies)
{
    public int Id { get; private set; } = id;
    public string Title { get; private set; } = title;
    public string Author { get; private set; } = author;
    public string Isbn { get; private set; } = isbn;
    public int PublishedYear { get; private set; } = publishedYear;
    public int TotalCopies { get; private set; } = totalCopies;
    public int AvailableCopies { get; private set; } = availableCopies;

    public static BookViewModel FromEntity(Book book) =>
        new(book.Id,
            book.Title,
            book.Author,
            book.Isbn,
            book.PublishedYear,
            book.TotalCopies,
            book.AvailableCopies);
}