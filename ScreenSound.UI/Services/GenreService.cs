using ScreenSound.Models.APIModels;
using ScreenSound.UI.Services.Interfaces;
using System.Net.Http.Json;

namespace ScreenSound.UI.Services;

public class GenreService (IHttpClientFactory httpClientFactory) : IGenreService
{
	private readonly HttpClient _httpClient = httpClientFactory.CreateClient("API");

	public async Task<ICollection<GenreGetModel>?> ListAllAsync() => await _httpClient.GetFromJsonAsync<ICollection<GenreGetModel>?>("Genres");
}
