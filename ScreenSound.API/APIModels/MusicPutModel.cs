using ScreenSound.Models;
using System.Text.Json.Serialization;

namespace ScreenSound.API.APIModels;

[JsonSerializable(typeof(MusicPutModel))]
public record MusicPutModel
(
    int Id,
    int ArtistId,
    string? Name = null,
    int? ReleaseYear = null
);