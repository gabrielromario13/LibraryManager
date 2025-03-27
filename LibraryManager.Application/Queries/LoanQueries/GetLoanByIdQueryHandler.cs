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
        var loan = await repository.GetById(request.Id);
        
        return loan is null
            ? ResultViewModel<LoanViewModel>.Error("Empréstimo não encontrado.")
            : ResultViewModel<LoanViewModel>.Success(LoanViewModel.FromEntity(loan));
    }
}