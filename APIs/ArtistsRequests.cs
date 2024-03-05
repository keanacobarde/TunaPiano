using TunaPiano.Models;
using Microsoft.EntityFrameworkCore;

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

            // CREATING AN ARTIST
            app.MapPost("/artists", (TunaPianoDbContext db, Artist newArtist) =>
            {
                Artist checkArtist = db.Artists.FirstOrDefault(a => a.Id == newArtist.Id);
                if (checkArtist != null)
                {
                    try
                    {
                        db.Artists.Add(newArtist);
                        db.SaveChanges();
                        return Results.Created($"/Artists/{newArtist.Id}", newArtist);
                    }
                    catch (DbUpdateException)
                    {
                        return Results.BadRequest("Invalid data submitted");
                    }
                }
                else
                {
                    return Results.Conflict("Artist already exists");
                }
            });


        }
    }
}
