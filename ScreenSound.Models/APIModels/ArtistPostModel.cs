using System.Text.Json.Serialization;

namespace ScreenSound.API.APIModels;

[JsonSerializable(typeof(ArtistPostModel))]
public record ArtistPostModel
(
    string Name,
    string Bio,
    string ProfilePicture
);
