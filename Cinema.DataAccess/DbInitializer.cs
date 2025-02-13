using Cinema.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace Cinema.DataAccess
{
    public static class DbInitializer
    {
        private static readonly string _imageExtension = ".jpg";
        public static void Initialize(CinemaDbContext context, string imagePath)
        {
            if (context.Movies.Any() || !Directory.Exists(imagePath)) return;

            IEnumerable<Movie> movies = [
            new()
            {
                Title = "Oppenheimer",
                Synopsis = "Oppenheimer is a 2023 epic biographical drama film written, produced, and directed by Christopher Nolan.[8] It follows the life of J. Robert Oppenheimer, the American theoretical physicist who helped develop the first nuclear weapons during World War II.",
                Director = "Christopher Nolan",
                Image = File.ReadAllBytes(Path.Combine(imagePath, $"kacsa.{_imageExtension}"))
            },
            new()
            {
                Title = "The Brutalist",
                Synopsis = "The Brutalist is a 2024 epic period drama film directed and produced by Brady Corbet, who co-wrote the screenplay with Mona Fastvold.[6] It stars Adrien Brody as a Hungarian-Jewish Holocaust survivor who immigrates to the United States, where he struggles to achieve the American Dream until a wealthy client changes his life.",
                Director = "Brady Corbet",
                Image = File.ReadAllBytes(Path.Combine(imagePath, $"cica.{_imageExtension}"))
            }];

            context.Movies.AddRange(movies);

            context.SaveChanges();
        }
    }
}
