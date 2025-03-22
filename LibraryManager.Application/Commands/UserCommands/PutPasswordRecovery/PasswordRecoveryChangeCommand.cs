using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Commands.UserCommands.PutPasswordRecovery;

public class PasswordRecoveryChangeCommand(string email, string code, string newPassword)
    : IRequest<ResultViewModel>
{
    public string Email { get; set; } = email;
    public string Code { get; set; } = code;
    public string NewPassword { get; set; } = newPassword;
}