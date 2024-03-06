using TunaPiano.Models;
using Microsoft.EntityFrameworkCore;

namespace TunaPiano.APIs
{
    public class GenresRequest
    {
        public static void Map(WebApplication app)
        {
            // GET LIST OF ALL GenreS
            app.MapGet("/genres", (TunaPianoDbContext db) => {
                return db.Genres.ToList();
            });

            //CREATING A GENRE
            app.MapPost("/genres", (TunaPianoDbContext db, Genre newGenre) =>
            {
                Genre checkGenre = db.Genres.FirstOrDefault(g => g.Id == newGenre.Id);
                if (checkGenre == null)
                {
                    try
                    {
                        db.Genres.Add(newGenre);
                        db.SaveChanges();
                        return Results.Created($"/genres/{newGenre.Id}", newGenre);
                    }
                    catch (DbUpdateException)
                    {
                        return Results.BadRequest("Invalid data submitted");
                    }
                }
                else
                {
                    return Results.Conflict("Genre already exists");
                }
            });

            // UPDATING A GENRE
            app.MapPut("/genres/{id}/edit", (TunaPianoDbContext db, int id, Genre genreToUpdateInfo) =>
            {
                Genre genreToUpdate = db.Genres.FirstOrDefault(p => p.Id == id);
                if (genreToUpdate == null)
                {
                    return Results.NotFound();
                }

                if (genreToUpdateInfo.Description != null)
                {
                    genreToUpdate.Description = genreToUpdateInfo.Description;
                }

                db.SaveChanges();

                return Results.NoContent();
            });

            // DELETING A GENRE
            app.MapDelete("/genres/{id}", (TunaPianoDbContext db, int id) =>
            {
                Genre selectedGenre = db.Genres.FirstOrDefault(p => p.Id == id);
                if (selectedGenre == null)
                {
                    return Results.NotFound();
                }
                db.Genres.Remove(selectedGenre);
                db.SaveChanges();
                return Results.Ok(db.Genres);
            });
        }
    }
}
