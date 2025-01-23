using ScreenSound.Models.APIModels;

namespace ScreenSound.UI.Services.Interfaces;

public interface IMusicService
{
	Task Delete(MusicGetModel music);
	Task<MusicGetModel?> FindByName(string name);
	Task<ICollection<MusicGetModel>?> ListAllAsync();
}