using TunaPiano.Models;
using Microsoft.EntityFrameworkCore;

namespace TunaPiano.APIs
{
    public class ArtistsRequests
    {
        public static void Map(WebApplication app)
        {
            // GET LIST OF ALL ARTIST
            app.MapGet("/artists", (TunaPianoDbContext db) => {
                return db.Artists.ToList();
            });

            // CREATING AN ARTIST
            app.MapPost("/artists", (TunaPianoDbContext db, Artist newArtist) =>
            {
                Artist checkArtist = db.Artists.FirstOrDefault(a => a.Id == newArtist.Id);
                if (checkArtist == null)
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

            //UPDATING AN ARTIST
            app.MapPut("/artists/{id}/edit", (TunaPianoDbContext db, int id, Artist artistToUpdateInfo) =>
            {
                Artist artistToUpdate = db.Artists.FirstOrDefault(p => p.Id == id);
                if (artistToUpdate == null)
                {
                    return Results.NotFound();
                }

                if (artistToUpdateInfo.Name != null)
                {
                    artistToUpdate.Name = artistToUpdateInfo.Name;
                }
                if (artistToUpdateInfo.Age != null)
                {
                    artistToUpdate.Age = artistToUpdateInfo.Age;
                }
                if (artistToUpdateInfo.Bio != null)
                {
                    artistToUpdate.Bio = artistToUpdateInfo.Bio;
                }

                db.SaveChanges();

                return Results.NoContent();
            });

            // DELETING AN ARTIST
            app.MapDelete("/artists/{id}", (TunaPianoDbContext db, int id) =>
            {
                Artist selectedArtist = db.Artists.FirstOrDefault(p => p.Id == id);
                if (selectedArtist == null)
                {
                    return Results.NotFound();
                }
                db.Artists.Remove(selectedArtist);
                db.SaveChanges();
                return Results.Ok(db.Artists);
            });
        }
    }
}
