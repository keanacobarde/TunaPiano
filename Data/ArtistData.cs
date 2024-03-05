using TunaPiano.Models;

namespace TunaPiano.Data
{
    public class ArtistData
    {
        public static List<Artist> Artists = new List<Artist>
            {
            new Artist { Id = 1, Name = "John Doe", Age = 30, Bio = "A talented musician from New York." },
            new Artist { Id = 2, Name = "Jane Smith", Age = 25, Bio = "Painter known for vibrant and expressive artworks." },
            new Artist { Id = 3, Name = "Bob Johnson", Age = 35, Bio = "Sculptor specializing in abstract forms." },
            new Artist { Id = 4, Name = "Alice Williams", Age = 28, Bio = "Photographer capturing the beauty of nature." },
            };
    }
}
