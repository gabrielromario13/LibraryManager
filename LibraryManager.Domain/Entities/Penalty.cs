namespace LibraryManager.Domain.Entities;

public class Penalty(
    int userId,
    int loanId,
    decimal amount) : BaseEntity
{
    public int UserId { get; private set; } = userId;
    public int LoanId { get; private set; } = loanId;
    public decimal Amount { get; private set; } = amount;
    public bool Paid { get; private set; }

    public User User { get; set; } = null!;
    public Loan Loan { get; set; } = null!;

    public void SetAsPaid()
    {
        Paid = true;
    }
}