using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Application.Queries.LoanQueries;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Queries.PenaltyQueries;

public class GetAllPenaltiesQueryHandler(IPenaltyRepository repository)
    : IRequestHandler<GetAllPenaltiesQuery, ResultViewModel<List<PenaltyViewModel>>>
{
    public async Task<ResultViewModel<List<PenaltyViewModel>>> Handle(GetAllPenaltiesQuery request,
        CancellationToken cancellationToken)
    {
        var penalties = await repository.Get();

        return ResultViewModel<List<PenaltyViewModel>>
            .Success(penalties.Select(PenaltyViewModel.FromEntity).ToList());
    }
}