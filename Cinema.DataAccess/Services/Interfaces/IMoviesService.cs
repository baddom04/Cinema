﻿using Cinema.DataAccess.Models;

namespace Cinema.DataAccess.Services.Interfaces
{
    public interface IMoviesService
    {
        Task<IReadOnlyCollection<Movie>> GetLatestMoviesAsync(int? count = null);
        Task<Movie> GetByIdAsync(int id);
    }
}
