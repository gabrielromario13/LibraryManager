using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Commands.BookCommands;

public class DeleteBookCommand(int id) : IRequest<ResultViewModel>
{
    public int Id { get; private set; } = id;
}