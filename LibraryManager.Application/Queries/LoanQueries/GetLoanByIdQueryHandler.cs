using System.Linq.Expressions;
using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Queries.LoanQueries;

public class GetLoanByIdQueryHandler(ILoanRepository repository)
    : IRequestHandler<GetLoanByIdQuery, ResultViewModel<LoanViewModel>>
{
    public async Task<ResultViewModel<LoanViewModel>> Handle(GetLoanByIdQuery request,
        CancellationToken cancellationToken)
    {
        var includeProperties = new List<Expression<Func<Domain.Entities.Loan, object>>>()
        {
            x => x.User,
            x => x.Book
        };
        
        var loan = await repository.GetSingle(x => x.Id == request.Id, includeProperties);
        
        if (loan is null)
            return ResultViewModel<LoanViewModel>.Error("Empréstimo não encontrado.");

        var model = LoanViewModel.FromEntity(loan);
        return ResultViewModel<LoanViewModel>.Success(model);
    }
}