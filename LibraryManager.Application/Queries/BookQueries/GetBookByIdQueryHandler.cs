using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Queries.BookQueries;

public class GetBookByIdQueryHandler(IBookRepository repository)
    : IRequestHandler<GetBookByIdQuery, ResultViewModel<BookViewModel>>
{
    public async Task<ResultViewModel<BookViewModel>> Handle(GetBookByIdQuery request,
        CancellationToken cancellationToken)
    {
        var books = await repository.GetDetailsById(request.Id);
        
        return books is null
            ? ResultViewModel<BookViewModel>.Error("Livro não encontrado.")
            : ResultViewModel<BookViewModel>.Success(BookViewModel.FromEntity(books));
    }
}