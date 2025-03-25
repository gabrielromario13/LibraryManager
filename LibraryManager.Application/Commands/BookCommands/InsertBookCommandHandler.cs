using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.BookCommands;

public class InsertBookCommandHandler(IBookRepository bookRepository)
    : IRequestHandler<InsertBookCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(
        InsertBookCommand request,
        CancellationToken cancellationToken)
    {
        var book = request.ToEntity();

        await bookRepository.Add(book);
            
        return ResultViewModel<int>.Success(book.Id);
    }
}