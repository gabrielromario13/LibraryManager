using LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Models.ViewModels;

public class LoanViewModel(
    int id,
    int idUser,
    string userName,
    int idBook,
    string bookTitle,
    DateTime loanDate,
    DateTime returnDate)
{
    public int Id { get; private set; } = id;
    public int IdUser { get; private set; } = idUser;
    public string UserName { get; private set; } = userName;
    public int IdBook { get; private set; } = idBook;
    public string BookTitle { get; private set; } = bookTitle;
    public DateTime LoanDate { get; private set; } = loanDate;
    public DateTime ReturnDate { get; private set; } = returnDate;

    public static LoanViewModel FromEntity(Loan loan)
        => new(loan.Id, loan.UserId, loan.User.Name, loan.BookId, loan.Book.Title, loan.LoanDate, loan.ReturnDate);
}