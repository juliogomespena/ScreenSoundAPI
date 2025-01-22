using ScreenSound.Models;
using System.Text.Json.Serialization;

namespace ScreenSound.API.APIModels;

[JsonSerializable(typeof(ArtistGetModel))]
public record ArtistGetModel
(
    int Id,
    string Name,
    string Bio,
    string ProfilePicture,
    List<string>? Musics = null
);
