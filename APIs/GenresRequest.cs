using TunaPiano.Models;
using Microsoft.EntityFrameworkCore;

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

            //CREATING A GENRE
            app.MapPost("/Genres", (TunaPianoDbContext db, Genre newGenre) =>
            {
                Genre checkGenre = db.Genres.FirstOrDefault(g => g.Id == newGenre.Id);
                if (checkGenre != null)
                {
                    try
                    {
                        db.Genres.Add(newGenre);
                        db.SaveChanges();
                        return Results.Created($"/Genres/{newGenre.Id}", newGenre);
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
        }
    }
}
