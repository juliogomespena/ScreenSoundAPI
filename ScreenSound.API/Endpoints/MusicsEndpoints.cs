using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.APIModels;
using ScreenSound.Banco;
using ScreenSound.Models;

namespace ScreenSound.API.Endpoints;

public static class MusicsEndpoints
{
    public static void Add(this WebApplication app)
    {
        app.MapGet("/Musics", (IRepository<Music> musicRepository) =>
        {
            var musics = musicRepository.ListAll()
            .Select(m => new MusicGetModel()
            {
                Id = m.Id,
                Name = m.Name,
                ReleaseYear = m.ReleaseYear,
                ArtistId = m.ArtistId,
                ArtistName = m.Artist.Name
            }).ToList();

            return Results.Ok(musics);
        });

        app.MapGet("/Musics/{name}", (string name, IRepository<Music> musicRepository) =>
        {
            var music = musicRepository.FindByParameter(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (music is null)
                return Results.NotFound("Music not found.");

            var musicApiModel = new MusicGetModel()
            {
                Id = music.Id,
                Name = music.Name,
                ReleaseYear = music.ReleaseYear,
                ArtistId = music.ArtistId,
                ArtistName = music.Artist.Name
            };

            return Results.Ok(musicApiModel);
        });

        app.MapPost("/Musics", ([FromBody] MusicPostModel musicPostModel, IRepository<Music> musicRepository) =>
        {
            if (string.IsNullOrEmpty(musicPostModel.Name))
                return Results.BadRequest("Music name cannot be null or empty.");
            if (musicPostModel.ArtistId == 0)
                return Results.BadRequest("Artist not found.");
            if (musicPostModel.ReleaseYear == 0)
                return Results.BadRequest("Release year cannot be null or empty.");

            var music = new Music(musicPostModel.Name, musicPostModel.ArtistId, musicPostModel.ReleaseYear);

            musicRepository.Add(music);
            return Results.Ok();
        });

        app.MapPut("/Musics", ([FromBody] MusicPutModel musicPutModel, IRepository<Music> musicRepository) =>
        {
            if (musicPutModel.Id == 0)
                return Results.BadRequest("Music id cannot be null.");

            var musicFound = musicRepository.FindByParameter(m => m.Id == musicPutModel.Id);

            if (musicFound is null)
                return Results.NotFound("Music not found.");

            musicFound.Name = string.IsNullOrEmpty(musicPutModel.Name) ? musicFound.Name : musicPutModel.Name;
            musicFound.ReleaseYear = musicPutModel.ReleaseYear is null || musicPutModel.ReleaseYear == 0 ? musicFound.ReleaseYear : musicPutModel.ReleaseYear;
            musicFound.ArtistId = musicPutModel.ArtistId == 0 ? musicFound.ArtistId : musicPutModel.ArtistId;

            musicRepository.Update(musicFound);
            return Results.Ok();
        });

        app.MapDelete("/Musics/{id}", (int id, IRepository<Music> musicRepository) =>
        {
            var music = musicRepository.FindByParameter(m => m.Id == id);

            if (music is null)
                return Results.NotFound("Music not found.");

            musicRepository.Remove(music);
            return Results.NoContent();
        });
    }
}
