using ScreenSound.API.APIModels;

namespace ScreenSound.UI.Services.Interfaces
{
    public interface IArtistaService
    {
        Task Add(ArtistPostModel artist);
        Task<ArtistGetModel?> FindByName(string name);
        Task<ICollection<ArtistGetModel>?> ListAllArtistsAsync();
    }
}