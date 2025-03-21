using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Queries.BookQueries;

public class GetAllBooksQuery : IRequest<ResultViewModel<List<BookItemViewModel>>>;