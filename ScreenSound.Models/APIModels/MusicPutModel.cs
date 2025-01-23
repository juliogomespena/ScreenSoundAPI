using ScreenSound.Models;
using System.Text.Json.Serialization;

namespace ScreenSound.Models.APIModels;

[JsonSerializable(typeof(MusicPutModel))]
public record MusicPutModel
(
    int Id,
    int ArtistId,
    string? Name = null,
    int? ReleaseYear = null,
    List<string>? Genres = null
);