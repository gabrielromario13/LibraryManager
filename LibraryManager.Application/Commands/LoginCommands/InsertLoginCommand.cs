using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Commands.LoginCommands;

public class InsertLoginCommand(string email, string password)
    : IRequest<ResultViewModel<string>>
{
    public string Email { get; private set; } = email;
    public string Password { get; private set; } = password;
}