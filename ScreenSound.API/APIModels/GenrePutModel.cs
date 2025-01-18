using System.Text.Json.Serialization;

namespace ScreenSound.API.APIModels;

[JsonSerializable(typeof(GenrePutModel))]
public record GenrePutModel
(
    int Id,
    string? Name = null,
    string? Description = null
);
