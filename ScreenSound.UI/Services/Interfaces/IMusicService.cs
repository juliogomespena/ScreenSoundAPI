using ScreenSound.Models.APIModels;

namespace ScreenSound.UI.Services.Interfaces;

public interface IMusicService
{
	Task Add(MusicPostModel music);
	Task Delete(MusicGetModel music);
	Task<MusicGetModel?> FindByName(string name);
	Task<ICollection<MusicGetModel>?> ListAllAsync();
	Task Update(MusicPutModel music);
}