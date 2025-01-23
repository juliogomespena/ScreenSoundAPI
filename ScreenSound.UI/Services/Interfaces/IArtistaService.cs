using ScreenSound.API.APIModels;

namespace ScreenSound.UI.Services.Interfaces
{
    public interface IArtistaService
    {
        Task Add(ArtistPostModel artist);
		Task Delete(ArtistGetModel artist);
		Task<ArtistGetModel?> FindByName(string name);
        Task<ICollection<ArtistGetModel>?> ListAllArtistsAsync();
		Task Update(ArtistPutModel artist);
	}
}