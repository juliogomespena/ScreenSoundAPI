using ScreenSound.API.APIModels;
using ScreenSound.UI.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;

namespace ScreenSound.UI.Services;

public class ArtistaService(IHttpClientFactory httpClientFactory) : IArtistaService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("API");

    public async Task<ICollection<ArtistGetModel>?> ListAllArtistsAsync() => await _httpClient.GetFromJsonAsync<ICollection<ArtistGetModel>>("Artists");

    public async Task Add(ArtistPostModel artist) => await _httpClient.PostAsJsonAsync("Artists", artist);

	public async Task<ArtistGetModel?> FindByName(string name)
	{
		var response = await _httpClient.GetAsync($"Artists/{name}");
		if (response.IsSuccessStatusCode)
		{
			var artist = await response.Content.ReadFromJsonAsync<ArtistGetModel>();
			return artist;
		}
		return null;
	}

	public async Task Delete(ArtistGetModel artist) => await _httpClient.DeleteAsync($"Artists/{artist.Id}");

	public async Task Update(ArtistPutModel artist) => await _httpClient.PutAsJsonAsync("Artists", artist);
}
