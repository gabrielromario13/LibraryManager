using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.ViewModels;

public class LoanViewModel(
    int id,
    int idUser,
    int idBook,
    DateTime loanDate)
{
    public int Id { get; private set; } = id;
    public int IdUser { get; private set; } = idUser;
    public int IdBook { get; private set; } = idBook;
    public DateTime LoanDate { get; private set; } = loanDate;

    public static LoanViewModel FromEntity(Loan loan)
        => new(loan.Id, loan.UserId, loan.BookId, loan.LoanDate);
}