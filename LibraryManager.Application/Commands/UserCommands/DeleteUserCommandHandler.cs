using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.UserCommands;

public class DeleteUserCommandHandler(IUserRepository repository)
    : IRequestHandler<DeleteUserCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetById(request.Id);
        if (user is null)
            return ResultViewModel.Error("Usuário não encontrado.");

        user.Deactivate();
        await repository.Update(user);

        return ResultViewModel.Success();
    }
}