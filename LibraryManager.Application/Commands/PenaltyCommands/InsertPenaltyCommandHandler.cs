using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.PenaltyCommands;

public class InsertPenaltyCommandHandler(
    IPenaltyRepository penaltyRepository,
    ILoanRepository loanRepository)
    : IRequestHandler<InsertPenaltyCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(
        InsertPenaltyCommand request,
        CancellationToken cancellationToken)
    {
        var loan = await loanRepository.GetById(request.LoanId);
        if (loan is null)
            return ResultViewModel<int>.Error("Empréstimo não encontrado.");
        
        loan.SetOverdue();
        await loanRepository.Update(loan);
        
        var penalty = request.ToEntity();
        await penaltyRepository.Add(penalty);
        
        return ResultViewModel<int>.Success(penalty.Id);
    }
}