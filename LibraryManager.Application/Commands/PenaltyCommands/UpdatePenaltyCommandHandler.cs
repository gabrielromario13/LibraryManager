using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.PenaltyCommands;

public class UpdatePenaltyCommandHandler(IPenaltyRepository penaltyRepository)
    : IRequestHandler<UpdatePenaltyCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(
        UpdatePenaltyCommand request,
        CancellationToken cancellationToken)
    {
        var penalty = await penaltyRepository.GetById(request.Id);
        if (penalty is null)
            return ResultViewModel<int>.Error("Penalidade não encontrada.");

        penalty.SetAsPaid();
        await penaltyRepository.Update(penalty);

        return ResultViewModel<int>.Success(penalty.Id);
    }
}