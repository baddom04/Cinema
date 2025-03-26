using Cinema.DataAccess.Config;
using Cinema.DataAccess.Models;
using Cinema.DataAccess.Services;
using Cinema.DataAccess.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Cinema.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration config)
        {
            //Config
            services.Configure<ReservationSettings>(config.GetSection("ReservationSettings"));
            services.Configure<EmailSettings>(config.GetSection("EmailSettings"));

            // Database
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<CinemaDbContext>(options => options
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies()
            );

            services.AddIdentity<User, UserRole>(options =>
            {
                // Password settings.
                options.Password.RequiredLength = 6;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<CinemaDbContext>()
            .AddDefaultTokenProviders();

            // Services
            services.AddScoped<IMoviesService, MovieService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IScreeningService, ScreeningService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IEmailsService, SmtpEmailsService>();

            return services;
        }
    }
}
