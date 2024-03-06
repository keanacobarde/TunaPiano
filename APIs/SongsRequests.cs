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

            // GET DETAILS OF A SINGLE GENRE WITH ASSOCIATED SONGS
            app.MapGet("/songs/{songId}", (TunaPianoDbContext db, int songId) =>
            {
                var song = db.Songs
                    .Include(s => s.Genres)
                    .FirstOrDefault(sg => sg.Id == songId);

                var artist = from artists in db.Artists
                                     join songs in db.Songs on artists.Id equals songs.Artist_Id
                                     select artists;

                var songDetails = new { song, artist };

                return songDetails;
            });

            // SEARCH SONGS BY GENRE
            app.MapGet("/songs/search", (TunaPianoDbContext db, string Query) => 
            { 
                var songsWithGenres = db.Songs
                .Include(s => s.Genres)
                .ToList();

                var searchResults = songsWithGenres.Where(s => s.Genres.Where(g => g.Description.ToLower() == Query.ToLower()).Count() != 0);

                return searchResults; 
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
            app.MapPut("/songs/{id}/edit", (TunaPianoDbContext db, int id, Song songToUpdateInfo) =>
            {
                Song songToUpdate = db.Songs.FirstOrDefault(p => p.Id == id);
                if (songToUpdate == null)
                {
                    return Results.NotFound();
                }

                if (songToUpdateInfo.Title != null)
                {
                    songToUpdate.Title = songToUpdateInfo.Title;
                }
                if (songToUpdateInfo.Artist_Id != null)
                {
                    songToUpdate.Artist_Id = songToUpdateInfo.Artist_Id;
                }
                if (songToUpdateInfo.Album != null)
                {
                    songToUpdate.Album = songToUpdateInfo.Album;
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
