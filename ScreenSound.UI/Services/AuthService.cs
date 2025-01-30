using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using ScreenSound.Models.APIModels;
using ScreenSound.UI.Services.Interfaces;

namespace ScreenSound.UI.Services;

public sealed class AuthService(IHttpClientFactory httpClientFactory) : AuthenticationStateProvider, IAuthService
{
	private readonly HttpClient _httpClient = httpClientFactory.CreateClient("API");
	private bool _isAuthenticated = false;

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		_isAuthenticated = false;
		var user = new ClaimsPrincipal();

		var response = await _httpClient.GetAsync("auth/manage/info");

		if(response.IsSuccessStatusCode)
		{
			var info = await response.Content.ReadFromJsonAsync<UserInfoResponse>();

			Claim[] data =
				[
					new Claim(ClaimTypes.Name, info.Email),
					new Claim(ClaimTypes.Email, info.Email)
				];

			var identity = new ClaimsIdentity(data, "Cookies");

			user = new ClaimsPrincipal(identity);
			_isAuthenticated = true;
		}

		return new AuthenticationState(user);
	}

	public async Task<AuthResponse> LoginAsync(string username, string password)
	{
		var response = await _httpClient.PostAsJsonAsync("auth/login?useCookies=true", new
		{
			email = username,
			password
		});

		if (response.IsSuccessStatusCode)
		{
			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
			return new AuthResponse { Success = true };
		}

		return new AuthResponse { Success = false, Errors = [ "Invalid user or password." ] };
	}

	public async Task LogoutAsync()
	{
		await _httpClient.PostAsync("auth/logout", null);
		NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
	}

	public async Task<bool> IsAuthenticated()
	{
		await GetAuthenticationStateAsync();
		return _isAuthenticated;
	}
}
