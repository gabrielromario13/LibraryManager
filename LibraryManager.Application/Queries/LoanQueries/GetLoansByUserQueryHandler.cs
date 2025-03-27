using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Queries.LoanQueries;

public class GetLoansByUserQueryHandler(ILoanRepository repository)
    : IRequestHandler<GetLoansByUserQuery, ResultViewModel<List<LoanViewModel>>>
{
    public async Task<ResultViewModel<List<LoanViewModel>>> Handle(
        GetLoansByUserQuery request,
        CancellationToken cancellationToken)
    {
        var loans = await repository
            .Get(l => l.UserId == request.UserId);

        return !loans.Any()
            ? ResultViewModel<List<LoanViewModel>>.Error("Usuário não pussui nenhum empréstimo.")
            : ResultViewModel<List<LoanViewModel>>.Success(
                loans.Select(LoanViewModel.FromEntity).ToList());
    }
}