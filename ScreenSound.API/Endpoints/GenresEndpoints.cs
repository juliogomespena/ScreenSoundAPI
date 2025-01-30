using Microsoft.AspNetCore.Mvc;
using ScreenSound.Models.APIModels;
using ScreenSound.Banco;
using ScreenSound.Models.Models;

namespace ScreenSound.API.Endpoints;

public static class GenresEndpoints
{
    public static void Add(this WebApplication app)
    {
        var group = app.MapGroup("Genres").RequireAuthorization().WithTags("Genres");

			group.MapGet("/", (IRepository<Genre> genreRepository) =>
        {
            var genres = genreRepository.ListAll();
            return Results.Ok(genres.Select(g => new GenreGetModel(g.Id, g.Name, g.Description)).ToList());
        });

        group.MapGet("/{name}", (string name, IRepository<Genre> genreRepository) =>
        {
            var genre = genreRepository.FindByParameter(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (genre is null)
                return Results.NotFound("Genre not found.");

            return Results.Ok(new GenreGetModel(genre.Id, genre.Name, genre.Description));
        });

        group.MapPost("/", ([FromBody] GenrePostModel genrePostModel, IRepository<Genre> genreRepository) =>
        {
            if (string.IsNullOrEmpty(genrePostModel.Name))
                return Results.BadRequest("Genre name cannot be null");

            var genre = new Genre(genrePostModel.Name) { Description = genrePostModel.Description };
            genreRepository.Add(genre);

            return Results.Ok();
        });

        group.MapPut("/", ([FromBody] GenrePutModel genrePutModel, IRepository<Genre> genreRepository) =>
        {
            var genreFound = genreRepository.FindByParameter(g => g.Id == genrePutModel.Id);

            if (genreFound is null)
                return Results.NotFound("Genre not found.");

            genreFound.Name = string.IsNullOrEmpty(genrePutModel.Name) ? genreFound.Name : genrePutModel.Name;
            genreFound.Description = string.IsNullOrEmpty(genrePutModel.Description) ? genreFound.Description : genrePutModel.Description;

            genreRepository.Update(genreFound);
            return Results.Ok();
        });

        group.MapDelete("/{id}", (int id, IRepository<Genre> genreRepository) =>
        {
            var genre = genreRepository.FindByParameter(g => g.Id == id);

            if (genre is null)
                return Results.NotFound("Genre not found.");

            genreRepository.Remove(genre);
            return Results.NoContent();
        });
    }
}
