using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Queries.PenaltyQueries;

public class GetAllPenaltiesQuery : IRequest<ResultViewModel<List<PenaltyViewModel>>>;