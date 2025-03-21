using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Queries.BookQueries;

public class GetAllBooksQueryHandler(IBookRepository repository)
    : IRequestHandler<GetAllBooksQuery, ResultViewModel<List<BookItemViewModel>>>
{
    public async Task<ResultViewModel<List<BookItemViewModel>>> Handle(
        GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await repository.Get(x => x.IsActive);

        if (!books.Any())
            return ResultViewModel<List<BookItemViewModel>>.Error("Nenhum livro encontrado.");
        
        var model = books.Select(BookItemViewModel.FromEntity).ToList();

        return ResultViewModel<List<BookItemViewModel>>.Success(model);
    }
}