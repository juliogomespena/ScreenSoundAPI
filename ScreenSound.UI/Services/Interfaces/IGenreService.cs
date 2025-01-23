using ScreenSound.Models.APIModels;

namespace ScreenSound.UI.Services.Interfaces;

public interface IGenreService
{
	Task<ICollection<GenreGetModel>?> ListAllAsync();
}