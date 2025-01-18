using System.Text.Json.Serialization;

namespace ScreenSound.API.APIModels;

[JsonSerializable(typeof(GenreGetModel))]
public record GenreGetModel
(
    int Id,
    string Name,
    string? Description
);
