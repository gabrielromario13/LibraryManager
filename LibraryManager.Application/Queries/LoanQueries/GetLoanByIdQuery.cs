using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Queries.LoanQueries;

public class GetLoanByIdQuery(int id) : IRequest<ResultViewModel<LoanViewModel>>
{
    public int Id { get; private set; } = id;
}