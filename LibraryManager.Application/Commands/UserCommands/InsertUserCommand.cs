using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Commands.UserCommands;

public class InsertUserCommand : IRequest<ResultViewModel<int>>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    
    public Domain.Entities.User ToEntity(string hashedPassword) 
        => new(Name, Email, hashedPassword);
}