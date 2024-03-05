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
