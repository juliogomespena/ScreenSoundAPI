using System.Text.Json.Serialization;

namespace ScreenSound.API.APIModels;

[JsonSerializable(typeof(ArtistPutModel))]
public record ArtistPutModel
(
    int Id,
    string? Name = null, 
    string? Bio = null, 
    string? ProfilePicture = null
);
