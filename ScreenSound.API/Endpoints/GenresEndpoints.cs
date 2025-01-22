﻿using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.APIModels;
using ScreenSound.Banco;
using ScreenSound.Models.Models;

namespace ScreenSound.API.Endpoints
{
    public static class GenresEndpoints
    {
        public static void Add(this WebApplication app)
        {
            app.MapGet("/Genres", (IRepository<Genre> genreRepository) =>
            {
                var genres = genreRepository.ListAll();
                return Results.Ok(genres.Select(g => new GenreGetModel(g.Id, g.Name, g.Description)).ToList());
            });

            app.MapGet("/Genres/{name}", (string name, IRepository<Genre> genreRepository) =>
            {
                var genre = genreRepository.FindByParameter(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (genre is null)
                    return Results.NotFound("Genre not found.");

                return Results.Ok(new GenreGetModel(genre.Id, genre.Name, genre.Description));
            });

            app.MapPost("/Genres", ([FromBody] GenrePostModel genrePostModel, IRepository<Genre> genreRepository) =>
            {
                if (string.IsNullOrEmpty(genrePostModel.Name))
                    return Results.BadRequest("Genre name cannot be null");

                var genre = new Genre(genrePostModel.Name) { Description = genrePostModel.Description };
                genreRepository.Add(genre);

                return Results.Ok();
            });

            app.MapPut("/Genres", ([FromBody] GenrePutModel genrePutModel, IRepository<Genre> genreRepository) =>
            {
                var genreFound = genreRepository.FindByParameter(g => g.Id == genrePutModel.Id);

                if (genreFound is null)
                    return Results.NotFound("Genre not found.");

                genreFound.Name = string.IsNullOrEmpty(genrePutModel.Name) ? genreFound.Name : genrePutModel.Name;
                genreFound.Description = string.IsNullOrEmpty(genrePutModel.Description) ? genreFound.Description : genrePutModel.Description;

                genreRepository.Update(genreFound);
                return Results.Ok();
            });

            app.MapDelete("/Genres/{id}", (int id, IRepository<Genre> genreRepository) =>
            {
                var genre = genreRepository.FindByParameter(g => g.Id == id);

                if (genre is null)
                    return Results.NotFound("Genre not found.");

                genreRepository.Remove(genre);
                return Results.NoContent();
            });
        }
    }
}
