using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Commands.LoanCommands;

public class InsertLoanCommand : IRequest<ResultViewModel<int>>
{
    public int IdUser { get; set; }
    public int IdBook { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }

    public Domain.Entities.Loan ToEntity()
        => new(IdUser, IdBook, LoanDate.Date, DueDate.Date);
}