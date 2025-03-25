using LibraryManager.Domain.Enums;

namespace LibraryManager.Domain.Entities;

public class Loan(
    int userId,
    int bookId,
    DateTime loanDate,
    DateTime dueDate) : BaseEntity
{
    public int UserId { get; private set; } = userId;
    public int BookId { get; private set; } = bookId;
    public DateTime LoanDate { get; private set; } = loanDate;
    public DateTime DueDate { get; private set; } = dueDate;
    public DateTime? ReturnDate { get; private set; }

    public User User { get; set; } = null!;
    public Book Book { get; set; } = null!;

    public void SetReturnDate() => ReturnDate = DateTime.UtcNow;
}