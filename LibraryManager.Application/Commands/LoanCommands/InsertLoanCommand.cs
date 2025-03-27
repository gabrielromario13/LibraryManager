using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Entities;
using MediatR;

namespace LibraryManager.Application.Commands.LoanCommands;

public class InsertLoanCommand : IRequest<ResultViewModel<int>>
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime LoanDate { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; }

    public Loan ToEntity()
        => new(UserId, BookId, LoanDate, DueDate);
}