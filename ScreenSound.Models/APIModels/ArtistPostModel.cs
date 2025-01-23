using System.Text.Json.Serialization;

namespace ScreenSound.Models.APIModels;

[JsonSerializable(typeof(ArtistPostModel))]
public record ArtistPostModel
(
    string Name,
    string Bio,
    string ProfilePicture
);
