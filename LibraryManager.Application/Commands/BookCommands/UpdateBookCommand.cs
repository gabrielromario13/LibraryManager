using System.Text.Json.Serialization;
using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Commands.BookCommands;

public class UpdateBookCommand : IRequest<ResultViewModel<int>>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public int AvailableCopies { get; set; }
}