using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Commands.PenaltyCommands;

public class UpdatePenaltyCommand(int id) : IRequest<ResultViewModel<int>>
{
    public int Id { get; private set; } = id;
}