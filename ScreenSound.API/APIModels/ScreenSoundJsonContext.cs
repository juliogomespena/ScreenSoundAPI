using ScreenSound.API.APIModels;
using System.Text.Json.Serialization;

namespace ScreenSound.Models;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(ArtistGetModel))]
[JsonSerializable(typeof(ArtistPostModel))]
[JsonSerializable(typeof(ArtistPutModel))]
[JsonSerializable(typeof(MusicGetModel))]
[JsonSerializable(typeof(MusicPostModel))]
[JsonSerializable(typeof(MusicPutModel))]
[JsonSerializable(typeof(List<ArtistGetModel>))]
[JsonSerializable(typeof(List<MusicGetModel>))]
public partial class ScreenSoundJsonContext : JsonSerializerContext
{
}
