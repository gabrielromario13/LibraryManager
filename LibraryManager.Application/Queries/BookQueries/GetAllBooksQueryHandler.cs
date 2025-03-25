using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Queries.BookQueries;

public class GetAllBooksQueryHandler(IBookRepository repository)
    : IRequestHandler<GetAllBooksQuery, ResultViewModel<List<BookViewModel>>>
{
    public async Task<ResultViewModel<List<BookViewModel>>> Handle(
        GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await repository.Get();

        return !books.Any()
            ? ResultViewModel<List<BookViewModel>>.Error("Nenhum livro encontrado.")
            : ResultViewModel<List<BookViewModel>>.Success(
                books.Select(BookViewModel.FromEntity).ToList());
    }
}