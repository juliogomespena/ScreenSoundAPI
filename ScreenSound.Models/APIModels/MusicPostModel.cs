using ScreenSound.Models;
using System.Text.Json.Serialization;

namespace ScreenSound.Models.APIModels;

[JsonSerializable(typeof(MusicPostModel))]
public record MusicPostModel
(
    string Name,
    int ReleaseYear,
    int ArtistId,
    List<string>? Genres = null
);