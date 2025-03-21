using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Queries.LoanQueries;

public class GetAllLoansQuery : IRequest<ResultViewModel<List<LoanViewModel>>>
{
    
}