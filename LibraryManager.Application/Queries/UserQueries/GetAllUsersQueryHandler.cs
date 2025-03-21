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
        var includeProperties = new List<Expression<Func<Domain.Entities.User, object>>>()
        {
            x => x.Loans
        };
        var user = await repository.Get(includeProperties: includeProperties);

        if (user is null)
            return ResultViewModel<List<UserViewModel>>.Error("Nenhum usuário cadastrado.");

        var model = user.Select(UserViewModel.FromEntity).ToList();

        return ResultViewModel<List<UserViewModel>>.Success(model);
    }
}