using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.ReservationCommands;

public class UpdateReservationCommandHandler(IReservationRepository repository)
    : IRequestHandler<UpdateReservationCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(
        UpdateReservationCommand request,
        CancellationToken cancellationToken)
    {
        var reservation = await repository.GetById(request.Id);
        if (reservation is null)
            return ResultViewModel<int>.Error("Reserva não encontrada.");

        reservation.Reserved();
        await repository.Update(reservation);

        return ResultViewModel<int>.Success(reservation.Id);
    }
}