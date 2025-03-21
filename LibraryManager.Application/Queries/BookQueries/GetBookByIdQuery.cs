using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Queries.BookQueries;

public class GetBookByIdQuery(int id) : IRequest<ResultViewModel<BookViewModel>>
{
    public int Id { get; private set; } = id;
}