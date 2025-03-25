using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.UserCommands;

public class UpdateUserCommandHandler(IUserRepository repository)
    : IRequestHandler<UpdateUserCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetById(request.Id);
        if (user is null)
            return ResultViewModel.Error("Usuário não encontrado.");

        user.Update(request.Name, request.Email);
        await repository.Update(user);

        return ResultViewModel.Success();
    }
}