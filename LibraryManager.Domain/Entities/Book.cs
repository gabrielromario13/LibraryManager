using LibraryManager.Domain.Enums;

namespace LibraryManager.Domain.Entities;

public class Book(
    string title,
    string author,
    string isbn,
    int publishedYear,
    int availableCopies = 1,
    BookStatus status = BookStatus.Available) : BaseEntity
{
    public string Title { get; private set; } = title;
    public string Author { get; private set; } = author;
    public string Isbn { get; private set; } = isbn;
    public int PublishedYear { get; private set; } = publishedYear;
    public int AvailableCopies { get; private set; } = availableCopies;
    public BookStatus Status { get; private set; } = status;
    
    public ICollection<Loan> Loans { get; set; } = null!;
    public ICollection<Reservation> Reservations { get; set; } = null!;
    
    public void Update(string title, string author, string isbn, int publishedYear, int availableCopies)
    {
        Title = title;
        Author = author;
        Isbn = isbn;
        PublishedYear = publishedYear;
        AvailableCopies = availableCopies;
    }
    
    public void Loaned()
    {
        if (Status is BookStatus.Available)
            Status = BookStatus.Loaned;
    }

    public void Available()
    {
        if (Status is BookStatus.Loaned or BookStatus.Reserved)
            Status = BookStatus.Available;
    }

    public void Reserved()
    {
        if (Status is BookStatus.Available)
            Status = BookStatus.Reserved;
    }

    public void Unavailable()
    {
        if (Status is not BookStatus.Loaned)
            Status = BookStatus.Unavailable;
    }
}