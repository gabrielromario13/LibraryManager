using System.Linq.Expressions;
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
        var includeProperties = new List<Expression<Func<Domain.Entities.Book, object>>>()
        {
            c => c.Loans
        };
        
        var books = await repository.GetSingle(b => b.Id == request.Id && b.IsActive, includeProperties);

        if (books is null)
            return ResultViewModel<BookViewModel>.Error("Livro não encontrado.");
        
        var model = BookViewModel.FromEntity(books);
        return ResultViewModel<BookViewModel>.Success(model);
    }
}