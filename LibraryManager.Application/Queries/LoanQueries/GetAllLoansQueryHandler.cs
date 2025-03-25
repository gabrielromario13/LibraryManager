using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Queries.LoanQueries;

public class GetAllLoansQueryHandler(ILoanRepository repository)
    : IRequestHandler<GetAllLoansQuery, ResultViewModel<List<LoanViewModel>>>
{
    public async Task<ResultViewModel<List<LoanViewModel>>> Handle(GetAllLoansQuery request,
        CancellationToken cancellationToken)
    {
        var loans = await repository.Get();

        return !loans.Any()
            ? ResultViewModel<List<LoanViewModel>>.Error("Nenhum empréstimo encontrado.")
            : ResultViewModel<List<LoanViewModel>>.Success(
                loans.Select(LoanViewModel.FromEntity).ToList());
    }
}