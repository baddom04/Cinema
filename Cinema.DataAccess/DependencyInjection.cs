using Cinema.DataAccess.Services;
using Cinema.DataAccess.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration config)
        {
            // Database
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<CinemaDbContext>(options => options
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies()
            );

            // Services
            services.AddScoped<IMoviesService, MovieService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IScreeningService, ScreeningService>();

            return services;
        }
    }
}
