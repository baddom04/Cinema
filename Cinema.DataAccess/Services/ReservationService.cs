﻿using Cinema.DataAccess.Config;
using Cinema.DataAccess.Exceptions;
using Cinema.DataAccess.Models;
using Cinema.DataAccess.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Cinema.DataAccess.Services
{
    public class ReservationService(CinemaDbContext context, IOptions<ReservationSettings> reservationSettings, IEmailsService emailsService) : IReservationService
    {
        private readonly CinemaDbContext _context = context;
        private readonly ReservationSettings _reservationSettings = reservationSettings.Value;
        private readonly IEmailsService _emailsService = emailsService;
        public async Task AddAsync(long screeningId, Reservation reservation)
        {
            if (reservation.Seats.Count == 0)
                throw new ArgumentException("At least 1 seat must be selected.", nameof(reservation.Seats));
            if (reservation.Seats.Count > _reservationSettings.MaximumNumberOfSeats)
                throw new ArgumentException($"Positions contains more items than {_reservationSettings.MaximumNumberOfSeats}.", nameof(reservation.Seats));

            var duplicates = reservation.Seats.GroupBy(s => s.Position);
            if (duplicates.Any(g => g.Count() > 1))
                throw new InvalidDataException("Duplicate positions are not allowed.");

            var screening = await _context.Screenings
                .Include(s => s.Seats)
                .Include(s => s.Room)
                .FirstOrDefaultAsync(s => s.Id == screeningId);

            if (screening is null)
                throw new EntityNotFoundException(nameof(Screening));

            reservation.CreatedAt = DateTime.UtcNow;

            foreach (var seat in reservation.Seats)
            {
                if (seat.ScreeningId != screeningId)
                    throw new ArgumentException("All seats must belong to the same screening.", nameof(reservation.Seats));

                if (seat.Position.Row < 1 || screening.Room.Rows < seat.Position.Row)
                    throw new ArgumentOutOfRangeException(nameof(seat.Position), $"Invalid position: {seat.Position.Row}, {seat.Position.Column}.");

                if (seat.Position.Column < 1 || screening.Room.Columns < seat.Position.Column)
                    throw new ArgumentOutOfRangeException(nameof(seat.Position), $"Invalid position: {seat.Position.Row}, {seat.Position.Column}.");

                var alreadyReserved = screening.Seats.Any(s => s.Position == seat.Position);
                if (alreadyReserved)
                    throw new ArgumentException($"Position: {seat.Position.Row}, {seat.Position.Column} is already reserved or sold.", nameof(seat.Position));
            }

            _context.Reservations.Add(reservation);

            try
            {
                await _context.SaveChangesAsync();

                _ = _emailsService.SendEmailAsync(reservation.Email,
                    "[Cinema] new reservation",
                    GenerateEmailBody(screening, reservation));
            }
            catch (DbUpdateException ex)
            {
                throw new SaveFailedException("Failed to create reservation", ex);
            }
        }
        private static string GenerateEmailBody(Screening screening, Reservation reservation)
        {
            return $"""
<p>
    <b>Your reservation has been created successfully!</b>
</p>
<table>
    <tr>
        <td><b>Movie:</b></td>
        <td>{screening.Movie.Title}</td>
    </tr>
    <tr>
        <td><b>Time:</b></td>
        <td>{screening.StartsAt}</td>
    </tr>
    <tr>
        <td><b>Room:</b></td>
        <td>{screening.Room.Name}</td>
    </tr>
    <tr>
        <td><b>Seats:</b></td>
        <td>{string.Join(", ", reservation.Seats.Select(s => $"(Row {s.Position.Row}, Col {s.Position.Column})"))}</td>
    </tr>
</table>
""";
        }
    }
}
