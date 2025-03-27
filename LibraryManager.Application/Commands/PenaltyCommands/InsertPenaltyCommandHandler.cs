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
        var loanExists = await loanRepository.Exists(l => l.Id == request.LoanId);
        if (!loanExists)
            return ResultViewModel<int>.Error("Empréstimo não encontrado.");
        
        var penalty = request.ToEntity();

        await penaltyRepository.Add(penalty);
            
        return ResultViewModel<int>.Success(penalty.Id);
    }
}