using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Commands.UserCommands.PutPasswordRecovery;

public class PasswordRecoveryValidateCommand(string email, string code)
    : IRequest<ResultViewModel>
{
    public string Email { get; set; } = email;
    public string Code { get; set; } = code;
}