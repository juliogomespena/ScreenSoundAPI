using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ScreenSound.Models.APIModels;
using ScreenSound.Banco;
using ScreenSound.Models.Models;

namespace ScreenSound.API.Endpoints;

public static class ArtistsEndpoints
{
    public static void Add(this WebApplication app)
    {
        app.MapGet("/Artists", (IRepository<Artist> artistRepository) =>
            {
                var artists = artistRepository.ListAll()
                .Select(a => new ArtistGetModel(a.Id, a.Name, a.Bio, a.ProfilePicture, a.Musics.Select(m => m.Name).ToList()))
                .ToList();

                return Results.Ok(artists);
            });

        app.MapGet("/Artists/{name}", (string name, IRepository<Artist> artistRepository) =>
        {
            var artist = artistRepository.FindByParameter(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (artist is null)
                return Results.NotFound();

            var artistGetModel = new ArtistGetModel(artist.Id, artist.Name, artist.Bio, artist.ProfilePicture, artist.Musics.Select(m => m.Name).ToList());

            return Results.Ok(artistGetModel);
        });

        app.MapPost("/Artists", async ([FromServices]IHostEnvironment hostEnvironment, [FromBody] ArtistPostModel artistPostModel, IRepository<Artist> artistRepository) =>
        {
            if (string.IsNullOrEmpty(artistPostModel.Name))
                return Results.BadRequest("Artist name cannot be null.");
            if (string.IsNullOrEmpty(artistPostModel.Bio))
                return Results.BadRequest("Artist bio cannot be null.");
            if (string.IsNullOrEmpty(artistPostModel.ProfilePicture))
                return Results.BadRequest("Artist profile picture cannot be null.");

            var profilePictureFileName = $"{DateTime.Now.ToString("ddMMyyyyHHmmss")}-{artistPostModel.Name.Trim()}.jpg";
            var filePath = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", "images", "artistprofilepictures", profilePictureFileName);
            using MemoryStream memoryStream = new(Convert.FromBase64String(artistPostModel.ProfilePicture));
            using FileStream fileStream = new(filePath, FileMode.Create);
            await memoryStream.CopyToAsync(fileStream);

            var artist = new Artist(artistPostModel.Name, artistPostModel.Bio, $"/images/artistprofilepictures/{profilePictureFileName}");

            artistRepository.Add(artist);
            return Results.Ok();
        });

        app.MapPut("/Artists", async ([FromServices]IHostEnvironment hostEnvironment, [FromBody] ArtistPutModel artistPutModel, IRepository<Artist> artistRepository) =>
        {
            if (artistPutModel.Id == 0)
                return Results.BadRequest("Music id cannot be null.");

            var artistFound = artistRepository.FindByParameter(a => a.Id == artistPutModel.Id);

            if (artistFound is null)
                return Results.NotFound();

			var profilePictureFileName = $"{DateTime.Now.ToString("ddMMyyyyHHmmss")}-{artistFound.Name.Trim()}.jpg";

			if (!string.IsNullOrEmpty(artistPutModel.ProfilePicture))
            {
                var filePath = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", "images", "artistprofilepictures", profilePictureFileName);
                using MemoryStream memoryStream = new(Convert.FromBase64String(artistPutModel.ProfilePicture));
                using FileStream fileStream = new(filePath, FileMode.Create);
                await memoryStream.CopyToAsync(fileStream);
            }

			artistFound.Name = string.IsNullOrEmpty(artistPutModel.Name) ? artistFound.Name : artistPutModel.Name;
            artistFound.Bio = string.IsNullOrEmpty(artistPutModel.Bio) ? artistFound.Bio : artistPutModel.Bio;
            artistFound.ProfilePicture = string.IsNullOrEmpty(artistPutModel.ProfilePicture) ? artistFound.ProfilePicture : $"/images/artistprofilepictures/{profilePictureFileName}";


			artistRepository.Update(artistFound);
            return Results.Ok();
        });

        app.MapDelete("/Artists/{id}", (int id, IRepository<Artist> artistRepository) =>
        {
            var artist = artistRepository.FindByParameter(a => a.Id == id);

            if (artist is null)
                return Results.NotFound();

            artistRepository.Remove(artist);
            return Results.NoContent();
        });
    }
}
