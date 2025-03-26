using Cinema.DataAccess;
using Cinema.DataAccess.Config;
using Cinema.DataAccess.Exceptions;
using Cinema.DataAccess.Models;
using Cinema.DataAccess.Services;
using Cinema.DataAccess.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;

namespace ELTE.Cinema.Tests.UnitTests;

public class ReservationsServiceTests : IDisposable
{
    private readonly CinemaDbContext _context;
    private readonly ReservationService _reservationsService;

    public ReservationsServiceTests()
    {
        // Configure in-memory database
        var options = new DbContextOptionsBuilder<CinemaDbContext>()
            .UseInMemoryDatabase("TestReservationDatabase") // Unique database for each test
            .Options;

        _context = new CinemaDbContext(options);

        // Configure ReservationSettings
        var reservationSettings = Options.Create(new ReservationSettings
        {
            MaximumNumberOfSeats = 6
        });

        // Mock email service
        Mock<IEmailsService> mockEmailService = new();

        // Initialize the ReservationsService
        _reservationsService = new ReservationService(
            _context,
            reservationSettings,
            mockEmailService.Object);

        SeedDatabase();
    }

    #region Add

    [Fact]
    public async Task AddAsync_ThrowsNotFound_WhenScreeningNotExists()
    {
        var testReservation = new Reservation()
        {
            Email = "Test",
            Name = "Test",
            Phone = "Test",
            Seats =
            [
                new() { Position = new SeatPosition(0, 0)}
            ]
        };
        Task test() => _reservationsService.AddAsync(9999, testReservation);
        await Assert.ThrowsAsync<EntityNotFoundException>(test);
    }

    [Fact]
    public async Task AddAsync_ThrowsInvalidData_WhenDuplicatePositions()
    {
        var testReservation = new Reservation()
        {
            Email = "Test",
            Name = "Test",
            Phone = "Test",
            Seats =
            [
                new() { Position = new SeatPosition(0, 0)},
                new() { Position = new SeatPosition(0, 0)}
            ]
        };
        Task test() => _reservationsService.AddAsync(1, testReservation);
        await Assert.ThrowsAsync<InvalidDataException>(test);
    }

    [Fact]
    public async Task AddAsync_ThrowsArgumentException_WhenAlreadyReserved()
    {
        // TODO
    }

    [Fact]
    public async Task AddAsync_ThrowsArgumentException_WhenExceedingSeatLimit()
    {
        // TODO
    }

    [Fact]
    public async Task AddAsync_AddsReservation()
    {
        // TODO
    }

    #endregion


    #region Helper

    private void SeedDatabase()
    {
        var screening = new Screening
        {
            Movie = new Movie { Id = 1, Director = "Test Director", Length = 120, Year = 2024, Title = "Test Movie", CreatedAt = DateTime.UtcNow, Image = [], Synopsis = "" },
            Room = new Room { Id = 1, Rows = 10, Columns = 10, Name = "Room 1", CreatedAt = DateTime.UtcNow },
            Seats = new List<Seat>(),
            CreatedAt = DateTime.UtcNow,
            StartsAt = DateTime.Now.AddDays(1),
        };

        _context.Screenings.Add(screening);

        _context.SaveChanges();
    }

    #endregion

    public void Dispose()
    {
        _context.Database.EnsureDeleted(); // Deletes the in-memory database
        _context.Dispose();
    }
}