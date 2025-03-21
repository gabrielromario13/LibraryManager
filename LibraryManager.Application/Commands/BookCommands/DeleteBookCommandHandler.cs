using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.BookCommands;

public class DeleteBookCommandHandler(IBookRepository repository)
    : IRequestHandler<DeleteBookCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await repository.GetById(request.Id);

        if (book is null)
            return ResultViewModel.Error("Livro não encontrado.");

        book.Deactivate();
        book.Unavailable();
        await repository.Update(book);
        
        return ResultViewModel.Success();
    }
}