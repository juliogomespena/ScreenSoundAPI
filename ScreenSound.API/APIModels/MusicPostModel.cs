using ScreenSound.Models;
using System.Text.Json.Serialization;

namespace ScreenSound.API.APIModels;

[JsonSerializable(typeof(MusicPostModel))]
public record MusicPostModel
(
    string Name,
    int ReleaseYear,
    int ArtistId
);