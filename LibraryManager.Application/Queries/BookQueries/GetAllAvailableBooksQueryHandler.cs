using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Enums;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Queries.BookQueries;

public class GetAllAvailableBooksQueryHandler(IBookRepository repository)
    : IRequestHandler<GetAllAvailableBooksQuery, ResultViewModel<List<BookViewModel>>>
{
    public async Task<ResultViewModel<List<BookViewModel>>> Handle(
        GetAllAvailableBooksQuery request,
        CancellationToken cancellationToken)
    {
        var books = await repository.Get(b => b.AvailableCopies > 0);

        return ResultViewModel<List<BookViewModel>>
            .Success(books.Select(BookViewModel.FromEntity).ToList());
    }
}