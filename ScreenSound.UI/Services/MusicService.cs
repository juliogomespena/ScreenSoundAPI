using ScreenSound.Models.APIModels;
using ScreenSound.UI.Services.Interfaces;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace ScreenSound.UI.Services;

public class MusicService(IHttpClientFactory httpClientFactory) : IMusicService
{
	private readonly HttpClient _httpClient = httpClientFactory.CreateClient("API");

	public async Task<ICollection<MusicGetModel>?> ListAllAsync() => await _httpClient.GetFromJsonAsync<ICollection<MusicGetModel>>("Musics");

	public async Task<MusicGetModel?> FindByName(string name)
	{
		var response = await _httpClient.GetAsync($"Musics/{name}");
		if (response.IsSuccessStatusCode)
		{
			var music = await response.Content.ReadFromJsonAsync<MusicGetModel>();
			return music;
		}
		return null;
	}

	public async Task Delete(MusicGetModel music) => await _httpClient.DeleteAsync($"Musics/{music.Id}");
}
