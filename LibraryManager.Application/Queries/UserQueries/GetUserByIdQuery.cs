using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Queries.UserQueries;

public class GetUserByIdQuery(int id) : IRequest<ResultViewModel<UserViewModel>>
{
    public int Id { get; private set; } = id;
}