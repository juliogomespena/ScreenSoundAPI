using Microsoft.AspNetCore.Components.Authorization;
using ScreenSound.Models.APIModels;

namespace ScreenSound.UI.Services.Interfaces;

public interface IAuthService
{
	Task<AuthenticationState> GetAuthenticationStateAsync();
	Task<bool> IsAuthenticated();
	Task<AuthResponse> LoginAsync(string username, string password);
	Task LogoutAsync();
}
