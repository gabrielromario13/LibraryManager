using System.Text.Json.Serialization;
using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Enums;
using MediatR;

namespace LibraryManager.Application.Commands.BookCommands;

[method: JsonConstructor]
public class UpdateBookCommand(
    int id,
    string title,
    string author,
    string isbn,
    int publishedYear,
    int availableCopies,
    BookStatus status)
    : IRequest<ResultViewModel<int>>
{
    public int Id { get; set; } = id;
    public string Title { get; set; } = title;
    public string Author { get; set; } = author;
    public string Isbn { get; set; } = isbn;
    public int PublishedYear { get; set; } = publishedYear;
    public int AvailableCopies { get; set; } = availableCopies;
    public BookStatus Status { get; set; } = status;
}