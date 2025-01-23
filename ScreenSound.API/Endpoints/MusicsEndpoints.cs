using Microsoft.AspNetCore.Mvc;
using ScreenSound.Models.APIModels;
using ScreenSound.Banco;
using ScreenSound.Models.Models;

namespace ScreenSound.API.Endpoints;

public static class MusicsEndpoints
{
    public static void Add(this WebApplication app)
    {
        app.MapGet("/Musics", (IRepository<Music> musicRepository) =>
        {
            var musics = musicRepository.ListAll()
            .Select(m => new MusicGetModel(m.Id, m.Name, m.ArtistId, m.Artist.Name)
            {
                ReleaseYear = m.ReleaseYear,
                Genres = m.Genres?.Select(g => g.Name).ToList()
            }).ToList();

            return Results.Ok(musics);
        });

        app.MapGet("/Musics/{name}", (string name, IRepository<Music> musicRepository) =>
        {
            var music = musicRepository.FindByParameter(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (music is null)
                return Results.NotFound("Music not found.");

            var musicApiModel = new MusicGetModel(music.Id, music.Name, music.ArtistId, music.Artist.Name)
            {
                ReleaseYear = music.ReleaseYear,
                Genres = music.Genres?.Select(g => g.Name).ToList()
            };

            return Results.Ok(musicApiModel);
        });

        app.MapPost("/Musics", ([FromBody] MusicPostModel musicPostModel, IRepository<Music> musicRepository, IRepository<Genre> genreRepository) =>
        {
            if (string.IsNullOrEmpty(musicPostModel.Name))
                return Results.BadRequest("Music name cannot be null or empty.");
            if (musicPostModel.ArtistId == 0)
                return Results.BadRequest("Artist not found.");
            if (musicPostModel.ReleaseYear == 0)
                return Results.BadRequest("Release year cannot be null or empty.");

            var genres = new List<Genre>();

            if (musicPostModel.Genres is not null)
                genres = DetermineGenresToBeUpdated(musicPostModel.Genres, genreRepository);

            var music = new Music(musicPostModel.Name, musicPostModel.ArtistId, musicPostModel.ReleaseYear)
            {
                Genres = genres
            };

            musicRepository.Add(music);
            return Results.Ok();
        });

        app.MapPut("/Musics", ([FromBody] MusicPutModel musicPutModel, IRepository<Music> musicRepository, IRepository<Genre> genreRepository) =>
        {
            if (musicPutModel.Id == 0)
                return Results.BadRequest("Music id cannot be null.");

            var musicFound = musicRepository.FindByParameter(m => m.Id == musicPutModel.Id);

            if (musicFound is null)
                return Results.NotFound("Music not found.");

            var genres = new List<Genre>();

            if (musicPutModel.Genres is not null)
                genres = DetermineGenresToBeUpdated(musicPutModel.Genres, genreRepository, musicFound.Genres);

            musicFound.Name = string.IsNullOrEmpty(musicPutModel.Name) ? musicFound.Name : musicPutModel.Name;
            musicFound.ReleaseYear = musicPutModel.ReleaseYear is null || musicPutModel.ReleaseYear == 0 ? musicFound.ReleaseYear : musicPutModel.ReleaseYear;
            musicFound.ArtistId = musicPutModel.ArtistId == 0 ? musicFound.ArtistId : musicPutModel.ArtistId;
            musicFound.Genres = genres;

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

    private static List<Genre> DetermineGenresToBeUpdated(List<string> genresToBeUpdated, IRepository<Genre> genreRepository, ICollection<Genre>? existingGenresInMusic = null)
    {
        var genresResult = new List<Genre>();

        foreach (var genre in genresToBeUpdated)
        {
            var genreFound = genreRepository.FindByParameter(g => g.Name.Equals(genre, StringComparison.OrdinalIgnoreCase));

            if (genreFound is not null)
            {
                if (existingGenresInMusic == null || !existingGenresInMusic.Any(g => g.Name.Equals(genre, StringComparison.OrdinalIgnoreCase)))
                    genresResult.Add(genreFound);
                else
                    genresResult.Add(existingGenresInMusic.First(g => g.Name.Equals(genre, StringComparison.OrdinalIgnoreCase)));
            }
            else
                genresResult.Add(new Genre(genre));
        }

        if (genresResult.Count == 0 && existingGenresInMusic != null)
            return existingGenresInMusic.ToList();

        if (existingGenresInMusic != null)
        {
            var genresToBeRemoved = existingGenresInMusic.Where(g => !genresToBeUpdated.Any(genre => genre.Equals(g.Name, StringComparison.OrdinalIgnoreCase))).ToList();
            foreach (var genre in genresToBeRemoved)
                genresResult.Remove(genre);
        }

        return genresResult;
    }
}
