using System.Text.Json.Serialization;

namespace ScreenSound.Models.APIModels;

[JsonSerializable(typeof(GenrePutModel))]
public record GenrePutModel
(
    int Id,
    string? Name = null,
    string? Description = null
);
