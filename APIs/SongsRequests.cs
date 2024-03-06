using Microsoft.EntityFrameworkCore;
using TunaPiano.Models;

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

        // CREATING A SONG
        app.MapPost("/songs", (TunaPianoDbContext db, Song newSong) =>
        {
            Song checkSong = db.Songs.FirstOrDefault(s => s.Id == newSong.Id);
            if (checkSong == null)
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

            // UPDATING A SONG
            app.MapPut("/songs/{id}/edit", (TunaPianoDbContext db, int id, song songToUpdateInfo) =>
            {
                song songToUpdate = db.songs.FirstOrDefault(p => p.Id == id);
                if (songToUpdate == null)
                {
                    return Results.NotFound();
                }

                if (songToUpdateInfo.Title != null)
                {
                    songToUpdate.Title = songToUpdateInfo.Title;
                }
                if (songToUpdateInfo.Artist_id != null)
                {
                    songToUpdate.artist_id = songToUpdateInfo.Artist_id;
                }
                if (songToUpdateInfo.Albumn != null)
                {
                    songToUpdate.Albumn = songToUpdateInfo.Albumn;
                }
                if (songToUpdateInfo.Length != null)
                {
                    songToUpdate.Length = songToUpdateInfo.Length;
                }

                db.SaveChanges();

                return Results.NoContent();
            });

            // DELETING A SONG
            app.MapDelete("/songs/{id}", (TunaPianoDbContext db, int id) =>
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
