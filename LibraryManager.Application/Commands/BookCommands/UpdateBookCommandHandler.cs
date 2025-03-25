using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.BookCommands;

public class UpdateBookCommandHandler(IBookRepository bookRepository)
    : IRequestHandler<UpdateBookCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(
        UpdateBookCommand request,
        CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetById(request.Id);
        if (book is null)
            return ResultViewModel<int>.Error("Livro não encontrado.");

        book.Update(request.Title, request.Author, request.Isbn, request.PublishedYear, request.AvailableCopies);
        await bookRepository.Update(book);

        return ResultViewModel<int>.Success(book.Id);
    }
}