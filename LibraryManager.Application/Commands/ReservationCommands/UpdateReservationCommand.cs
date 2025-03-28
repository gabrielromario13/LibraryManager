using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Commands.ReservationCommands;

public class UpdateReservationCommand(int id) : IRequest<ResultViewModel<int>>
{
    public int Id { get; private set; } = id;
}