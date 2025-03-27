using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.ViewModels;

public class LoanViewModel(
    int id,
    int idUser,
    int idBook,
    string status,
    DateTime loanDate,
    DateTime dueDate,
    DateTime? returnDate)
{
    public int Id { get; private set; } = id;
    public int IdUser { get; private set; } = idUser;
    public int IdBook { get; private set; } = idBook;
    public string Status { get; private set; } = status;
    public DateTime LoanDate { get; private set; } = loanDate;
    public DateTime DueDate { get; private set; } = dueDate;
    public DateTime? ReturnDate { get; private set; } = returnDate;

    public static LoanViewModel FromEntity(Loan loan)
        => new(
            loan.Id,
            loan.UserId,
            loan.BookId,
            loan.Status.ToString(),
            loan.LoanDate,
            loan.DueDate,
            loan.ReturnDate);
}