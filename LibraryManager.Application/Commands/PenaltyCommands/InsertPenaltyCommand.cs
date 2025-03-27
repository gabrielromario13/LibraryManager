using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Entities;
using MediatR;

namespace LibraryManager.Application.Commands.PenaltyCommands;

public class InsertPenaltyCommand : IRequest<ResultViewModel<int>>
{
    public int UserId { get; set; }
    public int LoanId { get; set; }
    public decimal Amount { get; set; }
    
    public Penalty ToEntity()
        => new(UserId, LoanId, Amount);
}