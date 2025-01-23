using ScreenSound.Models;
using System.Text.Json.Serialization;

namespace ScreenSound.Models.APIModels;

[JsonSerializable(typeof(MusicGetModel))]
public record MusicGetModel
(
    int Id,
    string Name,
    int ArtistId,
    string ArtistName,
	int? ReleaseYear = null,
	List<string>? Genres = null
);