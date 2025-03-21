using System.Linq.Expressions;
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
        var includeProperties = new List<Expression<Func<Domain.Entities.Loan, object>>>()
        {
            x => x.User,
            x => x.Book
        };
        var loans = await repository.Get(includeProperties: includeProperties);

        if (!loans.Any())
            return ResultViewModel<List<LoanViewModel>>.Error("Nenhum empréstimo encontrado.");

        var model = loans.Select(LoanViewModel.FromEntity).ToList();

        return ResultViewModel<List<LoanViewModel>>.Success(model);
    }
}