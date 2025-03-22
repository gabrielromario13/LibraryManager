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
        var books = await repository.Get(x => x.IsActive);

        if (!books.Any())
            return ResultViewModel<List<BookViewModel>>.Error("Nenhum livro encontrado.");
        
        var model = books.Select(BookViewModel.FromEntity).ToList();

        return ResultViewModel<List<BookViewModel>>.Success(model);
    }
}