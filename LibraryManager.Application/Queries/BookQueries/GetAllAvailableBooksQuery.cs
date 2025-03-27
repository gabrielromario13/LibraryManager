using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Queries.BookQueries;

public class GetAllAvailableBooksQuery : IRequest<ResultViewModel<List<BookViewModel>>>;