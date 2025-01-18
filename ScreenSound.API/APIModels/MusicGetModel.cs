using ScreenSound.Models;
using System.Text.Json.Serialization;

namespace ScreenSound.API.APIModels;

[JsonSerializable(typeof(MusicGetModel))]
public record MusicGetModel
(
    int? Id = null,
    string? Name = null,
    int? ReleaseYear = null,
    int? ArtistId = null,
    string? ArtistName = null
);