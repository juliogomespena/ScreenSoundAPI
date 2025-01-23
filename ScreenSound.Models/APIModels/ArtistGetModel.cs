using ScreenSound.Models;
using System.Text.Json.Serialization;

namespace ScreenSound.Models.APIModels;

[JsonSerializable(typeof(ArtistGetModel))]
public record ArtistGetModel
(
    int Id,
    string Name,
    string Bio,
    string? ProfilePicture = null,
    List<string>? Musics = null
);
