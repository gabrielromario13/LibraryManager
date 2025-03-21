using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Queries.UserQueries;

public class GetUserByIdQueryHandler(IUserRepository repository)
    : IRequestHandler<GetUserByIdQuery, ResultViewModel<UserViewModel>>
{
    public async Task<ResultViewModel<UserViewModel>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await repository.GetDetailsById(request.Id);
        if (user is null)
            return ResultViewModel<UserViewModel>.Error("Usuário não encontrado.");

        var model = UserViewModel.FromEntity(user);

        return ResultViewModel<UserViewModel>.Success(model);
    }
}