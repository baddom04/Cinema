using Cinema.DataAccess;
using Cinema.DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ELTE.Cinema.Tests.IntegrationTests;

public class MoviesControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public MoviesControllerIntegrationTests()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "IntegrationTest");
        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Replace the real database with an in-memory database
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<CinemaDbContext>));
                if (descriptor != null) services.Remove(descriptor);

                services.AddDbContext<CinemaDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestMoviesDatabase");
                });

                //Seed the database with initial data
                using var scope = services.BuildServiceProvider().CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<CinemaDbContext>();
                db.Database.EnsureCreated();

                SeedMovies(db);
            });
        });

        _client = _factory.CreateClient();
    }

    #region Get

    [Fact]
    public async Task GetMovies_ReturnsAllMovies()
    {
        // TODO
    }

    [Fact]
    public async Task GetMovieById_ReturnsMovie_WhenMovieExists()
    {
        // TODO
    }

    [Fact]
    public async Task GetMovieById_ReturnsNotFound_WhenMovieNotExists()
    {
        // TODO
    }

    #endregion

    #region Helpers
    private void SeedMovies(CinemaDbContext context)
    {
        context.Movies.AddRange(
            new Movie { Title = "Test Movie 1", Year = 2020, Director = "Director 1", Synopsis = "Synopsis 1", Length = 120, Image = new byte[] { 1, 2, 3 } },
            new Movie { Title = "Test Movie 2", Year = 2021, Director = "Director 2", Synopsis = "Synopsis 2", Length = 90, Image = new byte[] { 1, 2, 3 } }
        );

        context.SaveChanges();
    }

    public void Dispose()
    {
        using var scope = _factory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<CinemaDbContext>();
        db.Database.EnsureDeleted();

        _factory.Dispose();
        _client.Dispose();
    }

    #endregion
}