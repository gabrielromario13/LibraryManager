using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Infrastructure.Data.Repositories;

public class ReservationRepository(AppDbContext context)
    : BaseRepository<Reservation>(context), IReservationRepository;