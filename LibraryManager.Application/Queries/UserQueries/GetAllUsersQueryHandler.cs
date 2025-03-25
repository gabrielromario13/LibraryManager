using System.Linq.Expressions;
using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Application.Queries.UserQueries;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Queries.UserQueries;

public class GetAllUsersQueryHandler(IUserRepository repository)
    : IRequestHandler<GetAllUsersQuery, ResultViewModel<List<UserViewModel>>>
{
    public async Task<ResultViewModel<List<UserViewModel>>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var user = await repository.Get();
        
        return user is null
            ? ResultViewModel<List<UserViewModel>>.Error("Nenhum usuário cadastrado.")
            : ResultViewModel<List<UserViewModel>>.Success(user.Select(UserViewModel.FromEntity).ToList());
    }
}