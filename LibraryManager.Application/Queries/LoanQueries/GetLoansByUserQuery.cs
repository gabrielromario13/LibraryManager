using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Queries.LoanQueries;

public class GetLoansByUserQuery(int userId) : IRequest<ResultViewModel<List<LoanViewModel>>>
{
    public int UserId { get; private set; } = userId;
}