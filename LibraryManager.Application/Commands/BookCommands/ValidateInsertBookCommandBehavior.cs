using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.BookCommands;

public class ValidateCreateBookCommandBehavior(IBookRepository bookRepository)
    : IPipelineBehavior<InsertBookCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(
        InsertBookCommand request,
        RequestHandlerDelegate<ResultViewModel<int>> next,
        CancellationToken cancellationToken)
    {
        if (request.PublishedYear > DateTime.Now.Year)
            return ResultViewModel<int>.Error("Ano de publicação inválido.");
        
        if (string.IsNullOrWhiteSpace(request.Title))
            return ResultViewModel<int>.Error("O título deve ser informado.");
        
        if (await bookRepository.Exists(b => b.Title == request.Title))
            return ResultViewModel<int>.Error("Título já cadastrado.");

        return await next();
    }
}