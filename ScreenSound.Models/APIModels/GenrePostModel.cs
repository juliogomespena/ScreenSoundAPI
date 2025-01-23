using System.Text.Json.Serialization;

namespace ScreenSound.API.APIModels;

[JsonSerializable(typeof(GenrePostModel))]
public record GenrePostModel
(
    string Name,
    string? Description
);
