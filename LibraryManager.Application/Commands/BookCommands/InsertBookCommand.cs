using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Commands.BookCommands;

public class InsertBookCommand : IRequest<ResultViewModel<int>>
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public int AvailableCopies { get; set; }

    public Domain.Entities.Book ToEntity()
        => new(Title, Author, Isbn, PublishedYear, AvailableCopies);
}