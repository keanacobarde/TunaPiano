using TunaPiano.Models;

namespace TunaPiano.Data
{
    public class GenreData
    {
        public static List<Genre> Genres = new List<Genre>
            {
                new Genre
                {
                    Id = 1,
                    Description = "Rock",
                },
                new Genre
                {
                    Id = 2,
                    Description = "Pop",
                },
                new Genre
                {
                    Id = 3,
                    Description = "Alternative",
                },
            };
    }
}
