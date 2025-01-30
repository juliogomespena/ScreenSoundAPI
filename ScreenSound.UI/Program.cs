using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ScreenSound.UI.Services;
using ScreenSound.UI.Services.Interfaces;
using MudBlazor.Services;
using MudBlazor;
using ScreenSound.UI.Handlers;
using Microsoft.AspNetCore.Components.Authorization;

namespace ScreenSound.UI
{
	public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

			builder.Services.AddMudServices();

			builder.Services.AddAuthorizationCore();
			builder.Services.AddScoped<IAuthService, AuthService>(sp => 
                (AuthService)sp.GetRequiredService<AuthenticationStateProvider>());
			builder.Services.AddScoped<AuthenticationStateProvider, AuthService>();

			builder.Services.AddScoped<CookieHandler>();

			builder.Services.AddScoped<IArtistService, ArtistService>();
			builder.Services.AddScoped<IMusicService, MusicService>();
			builder.Services.AddScoped<IGenreService, GenreService>();

            builder.Services.AddHttpClient("API", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["APIServer:Url"]!);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddHttpMessageHandler<CookieHandler>();

            MudGlobal.UnhandledExceptionHandler = (exception) => Console.WriteLine(exception);

            await builder.Build().RunAsync();
        }
    }
}
