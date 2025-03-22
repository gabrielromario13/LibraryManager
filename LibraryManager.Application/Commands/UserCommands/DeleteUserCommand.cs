using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Commands.UserCommands;

public class DeleteUserCommand(int id) : IRequest<ResultViewModel>
{
    public int Id { get; private set; } = id;
}