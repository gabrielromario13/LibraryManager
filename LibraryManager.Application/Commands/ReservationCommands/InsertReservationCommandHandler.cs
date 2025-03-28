using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.ReservationCommands;

public class InsertReservationCommandHandler(IReservationRepository repository)
    : IRequestHandler<InsertReservationCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(
        InsertReservationCommand request,
        CancellationToken cancellationToken)
    {
        var reservation = request.ToEntity();

        await repository.Add(reservation);
            
        return ResultViewModel<int>.Success(reservation.Id);
    }
}