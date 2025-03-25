using LibraryManager.Application.Models.ViewModels;
using MediatR;
using Newtonsoft.Json;

namespace LibraryManager.Application.Commands.UserCommands;

public class UpdateUserCommand : IRequest<ResultViewModel>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}