using System.Text.Json.Serialization;

namespace ScreenSound.Models.APIModels;

[JsonSerializable(typeof(GenreGetModel))]
public record GenreGetModel
(
    int Id,
    string Name,
    string? Description
);
