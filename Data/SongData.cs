using TunaPiano.Models;

namespace TunaPiano.Data
{
    public class SongData
    {
        public static List<Song> Songs = new List<Song>
            {
            new Song { Id = 1, Title = "Stairway to Heaven", Artist_Id = 1, Album = "Led Zeppelin IV", Length = "8:02" },
            new Song { Id = 2, Title = "Bohemian Rhapsody", Artist_Id = 2, Album = "A Night at the Opera", Length = "5:55" },
            new Song { Id = 3, Title = "Shape of You", Artist_Id = 3, Album = "÷ (Divide)", Length = "3:53" },
            new Song { Id = 4, Title = "Happy", Artist_Id = 4, Album = "G I R L", Length = "3:53" },
            };
    }
}
