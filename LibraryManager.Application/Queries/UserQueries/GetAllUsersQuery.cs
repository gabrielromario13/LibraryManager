using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Queries.UserQueries;

public class GetAllUsersQuery : IRequest<ResultViewModel<List<UserViewModel>>>;