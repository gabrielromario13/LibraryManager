using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Commands.LoanCommands;

public class InsertLoanCommand : IRequest<ResultViewModel<int>>
{
    public required int IdUser { get; set; }
    public required int IdBook { get; set; }
    public required DateTime LoanDate { get; set; }
    public required DateTime DueDate { get; set; }

    public Domain.Entities.Loan ToEntity()
        => new(IdUser, IdBook, LoanDate.Date, DueDate.Date);
}