using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Entities;
using MediatR;

namespace LibraryManager.Application.Commands.ReservationCommands;

public class InsertReservationCommand : IRequest<ResultViewModel<int>>
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    
    public Reservation ToEntity()
        => new(UserId, BookId);
}