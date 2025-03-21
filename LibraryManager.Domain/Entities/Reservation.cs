using LibraryManager.Domain.Enums;

namespace LibraryManager.Domain.Entities;

public class Reservation(int userId, int bookId) : BaseEntity
{
    public int UserId { get; private set; } = userId;
    public int BookId { get; private set; } = bookId;
    public ReservationStatus Status { get; private set; } = ReservationStatus.Pending;

    public User User { get; set; } = null!;
    public Book Book { get; set; } = null!;
}