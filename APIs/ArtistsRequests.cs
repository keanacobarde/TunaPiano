namespace TunaPiano.APIs
{
    public class ArtistsRequests
    {
        public static void Map(WebApplication app)
        {
            // GET LIST OF ALL SONGS
            app.MapGet("/artists", (TunaPianoDbContext db) => {
                return db.Artists.ToList();
            });
        }
    }
}
