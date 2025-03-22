using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Commands.UserCommands.PutPasswordRecovery;

public class PasswordRecoveryRequestCommand(string email) : IRequest<ResultViewModel>
{
    public string Email { get; set; } = email;
}