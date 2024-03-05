namespace TunaPiano.APIs
{
    public class SongsRequests
    {
        public static void Map(WebApplication app)
        {
            // GET LIST OF ALL SONGS
            app.MapGet("/songs", (TunaPianoDbContext db) => { 
                return db.Songs.ToList();
            });
        }
    }
}
