using AutoMapper;
using Cinema.DataAccess;
using Cinema.DataAccess.Models;
using Cinema.DataAccess.Services;
using Cinema.WebApi.Controllers;
using Cinema.WebApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ELTE.Cinema.Tests.ControllerTests;

public class ScreeningsControllerIntegrationTests: IDisposable
{
    private readonly CinemaDbContext _context;
    private readonly ScreeningsController _controller;

    public ScreeningsControllerIntegrationTests()
    {
        // Set up the in-memory database
        var options = new DbContextOptionsBuilder<CinemaDbContext>()
            .UseInMemoryDatabase("TestScreeningDatabase")
            .Options;
        _context = new CinemaDbContext(options);
        
        // Initialize dependencies
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        var mapper = mapperConfig.CreateMapper();

        var screeningService = new ScreeningService(_context);

        // Initialize the controller
        _controller = new ScreeningsController(mapper, screeningService);
        
        //Init database
        SeedDatabase();
    }

    #region Get

    [Fact]
    public async Task GetScreenings_ReturnsScreeningList()
    {
        // TODO
    }
    
    [Fact]
    public async Task GetScreenings_ReturnsScreeningListForMovie()
    {
        // TODO
    }
    
    [Fact]
    public async Task GetScreenings_ReturnsScreeningListForRoom()
    {
        // TODO   
    }
    
    [Fact]
    public async Task GetScreenings_ReturnsScreeningListForInterval()
    {
        // TODO
    }
    
    [Fact]
    public async Task GetScreenings_ReturnsScreeningWithAllParameters()
    {
        // TODO
    }

    [Fact]
    public async Task GetScreeningById_ThrowsNotFound_WhenDoesNotExist()
    {
        // TODO
    }
    
    [Fact]
    public async Task GetScreeningById_ReturnsScreening_WhenExists()
    {
        // TODO
    }

    #endregion

    #region Helpers

    private void SeedDatabase()
    {
        if (_context.Movies.Any() || _context.Rooms.Any())
            return; // Prevent duplicate seeding

        // Seed Rooms
        var rooms = new List<Room>
        {
            new() { Name = "Room A", Rows = 10, Columns = 12, CreatedAt = DateTime.UtcNow },
            new() { Name = "Room B", Rows = 15, Columns = 15, CreatedAt = DateTime.UtcNow },
            new() { Name = "Room C", Rows = 20, Columns = 20, CreatedAt = DateTime.UtcNow },
        };

        // Seed Movies
        var movies = new List<Movie>
        {
            new()
            {
                Title = "Inception",
                Year = 2010,
                Director = "Christopher Nolan",
                Synopsis = "A skilled thief is offered a chance to have his criminal history erased as payment for the implantation of another person's idea into a target's subconscious.",
                Length = 148,
                Image = [], // Placeholder image data
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Title = "The Matrix",
                Year = 1999,
                Director = "The Wachowskis",
                Synopsis = "A computer hacker learns about the true nature of his reality and his role in the war against its controllers.",
                Length = 136,
                Image = [], // Placeholder image data
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Title = "Interstellar",
                Year = 2014,
                Director = "Christopher Nolan",
                Synopsis = "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
                Length = 169,
                Image = [], // Placeholder image data
                CreatedAt = DateTime.UtcNow
            },
        };

        _context.Rooms.AddRange(rooms);
        _context.Movies.AddRange(movies);
        
        _context.SaveChanges();
        
        //Clear Change-tracker
        DetachAllEntities();
    }
    
    private void DetachAllEntities()
    {
        var trackedEntities = _context.ChangeTracker.Entries().ToList();
        foreach (var entry in trackedEntities)
        {
            entry.State = EntityState.Detached;
        }
    }

    #endregion
    
    public void Dispose()
    {
        _context.Database.EnsureDeleted(); // Deletes the in-memory database
        _context.Dispose();
    }
}