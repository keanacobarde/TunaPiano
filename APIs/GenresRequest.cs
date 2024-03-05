namespace TunaPiano.APIs
{
    public class GenresRequest
    {
        public static void Map(WebApplication app)
        {
            // GET LIST OF ALL SONGS
            app.MapGet("/genres", (TunaPianoDbContext db) => {
                return db.Genres.ToList();
            });
        }
    }
}
