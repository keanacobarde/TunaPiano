namespace TunaPiano.APIs
using Microsoft.EntityFrameworkCore;
using TunaPiano.Models;

    public class SongsRequests
{
        public static void Map(WebApplication app)
        {
            // GET LIST OF ALL SONGS
            app.MapGet("/songs", (TunaPianoDbContext db) => { 
                return db.Songs.ToList();
            });

        // CREATING A SONG
        app.MapPost("/Songs", (TunaPianoDbContext db, Song newSong) =>
        {
            Song checkSong = db.Songs.FirstOrDefault(s => s.Id == newSong.Id);
            if (checkSong != null)
            {
                try
                {
                    db.Songs.Add(newSong);
                    db.SaveChanges();
                    return Results.Created($"/Songs/{newSong.Id}", newSong);
                }
                catch (DbUpdateException)
                {
                    return Results.BadRequest("Invalid data submitted");
                }
            }
            else
            {
                return Results.Conflict("Song already exists");
            }
        });

        // DELETING A SONG
        app.MapDelete("/Songs/{id}", (TunaPianoDbContext db, int id) =>
        {
            Song selectedSong = db.Songs.FirstOrDefault(p => p.Id == id);
            if (selectedSong == null)
            {
                return Results.NotFound();
            }
            db.Songs.Remove(selectedSong);
            db.SaveChanges();
            return Results.Ok(db.Songs);
        });

    }
    }
}
