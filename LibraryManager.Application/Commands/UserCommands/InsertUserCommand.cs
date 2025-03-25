using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Enums;
using MediatR;

namespace LibraryManager.Application.Commands.UserCommands;

public class InsertUserCommand : IRequest<ResultViewModel<int>>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public UserRoles Role { get; set; } = UserRoles.Reader;
    
    public Domain.Entities.User ToEntity(string hashedPassword) 
        => new(Name, Email, hashedPassword, Role);
}