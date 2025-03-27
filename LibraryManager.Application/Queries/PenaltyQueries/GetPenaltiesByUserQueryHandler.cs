using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Queries.PenaltyQueries;

public class GetPenaltiesByUserQueryHandler(IPenaltyRepository repository)
    : IRequestHandler<GetPenaltiesByUserQuery, ResultViewModel<List<PenaltyViewModel>>>
{
    public async Task<ResultViewModel<List<PenaltyViewModel>>> Handle(
        GetPenaltiesByUserQuery request,
        CancellationToken cancellationToken)
    {
        var penalties = await repository
            .Get(l => l.UserId == request.UserId);

        return !penalties.Any()
            ? ResultViewModel<List<PenaltyViewModel>>.Error("Usuário não pussui nenhuma penalidade.")
            : ResultViewModel<List<PenaltyViewModel>>.Success(
                penalties.Select(PenaltyViewModel.FromEntity).ToList());
    }
}