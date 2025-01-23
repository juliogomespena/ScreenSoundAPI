using ScreenSound.Models.APIModels;

namespace ScreenSound.UI.Services.Interfaces
{
    public interface IArtistService
    {
        Task Add(ArtistPostModel artist);
		Task Delete(ArtistGetModel artist);
		Task<ArtistGetModel?> FindByName(string name);
        Task<ICollection<ArtistGetModel>?> ListAllAsync();
		Task Update(ArtistPutModel artist);
	}
}