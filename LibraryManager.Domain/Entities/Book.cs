namespace LibraryManager.Domain.Entities;

public class Book(
    string title,
    string author,
    string isbn,
    int publishedYear,
    short totalCopies) : BaseEntity
{
    public string Title { get; private set; } = title;
    public string Author { get; private set; } = author;
    public string Isbn { get; private set; } = isbn;
    public int PublishedYear { get; private set; } = publishedYear;
    public short TotalCopies { get; private set; } = totalCopies;
    public short AvailableCopies { get; private set; } = totalCopies;

    public ICollection<Loan> Loans { get; set; } = null!;
    public ICollection<Reservation> Reservations { get; set; } = null!;

    public void Update(
        string title,
        string author,
        string isbn,
        int publishedYear,
        short totalCopies,
        short availableCopies)
    {
        Title = title;
        Author = author;
        Isbn = isbn;
        PublishedYear = publishedYear;
        TotalCopies = totalCopies;
        AvailableCopies = availableCopies;
    }

    public void IncrementAvailableCopies()
    {
        if (AvailableCopies < TotalCopies)
            AvailableCopies++;
    }

    public void DecrementAvailableCopies() => AvailableCopies--;
}