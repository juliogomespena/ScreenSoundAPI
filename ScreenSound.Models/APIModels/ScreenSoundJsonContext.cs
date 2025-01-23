using ScreenSound.Models.APIModels;
using System.Text.Json.Serialization;

namespace ScreenSound.Models;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(ArtistGetModel))]
[JsonSerializable(typeof(ArtistPostModel))]
[JsonSerializable(typeof(ArtistPutModel))]
[JsonSerializable(typeof(MusicGetModel))]
[JsonSerializable(typeof(MusicPostModel))]
[JsonSerializable(typeof(MusicPutModel))]
[JsonSerializable(typeof(GenreGetModel))]
[JsonSerializable(typeof(GenrePostModel))]
[JsonSerializable(typeof(GenrePutModel))]
[JsonSerializable(typeof(List<ArtistGetModel>))]
[JsonSerializable(typeof(List<MusicGetModel>))]
[JsonSerializable(typeof(List<GenreGetModel>))]
public partial class ScreenSoundJsonContext : JsonSerializerContext
{
}
