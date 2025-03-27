using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Queries.PenaltyQueries;

public class GetPenaltiesByUserQuery(int userId)
    : IRequest<ResultViewModel<List<PenaltyViewModel>>>
{
    public int UserId { get; private set; } = userId;
}