using System.Text.Json.Serialization;

namespace ScreenSound.Models.APIModels;

[JsonSerializable(typeof(GenrePostModel))]
public record GenrePostModel
(
    string Name,
    string? Description
);
