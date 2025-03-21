using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.ViewModels;

public class BookItemViewModel(int id, string title, string author, int publishedYear)
{
    public int Id { get; private set; } = id;
    public string Title { get; private set; } = title;
    public string Author { get; private set; } = author;
    public int PublishedYear { get; private set; } = publishedYear;

    public static BookItemViewModel FromEntity(Book book)
        => new(book.Id, book.Title, book.Author, book.PublishedYear);
}