using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ScreenSound.API.Endpoints;
using ScreenSound.Banco;
using ScreenSound.Models;
using ScreenSound.Models.Models;
using System.Text.Json.Serialization;

namespace ScreenSound.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ScreenSoundEfContext>((options) =>
        {
            options
                .UseSqlServer(builder.Configuration["ConnectionStrings:ScreenSoundDB"])
                .UseLazyLoadingProxies();
        });

        builder.Services.AddIdentityApiEndpoints<User>()
            .AddEntityFrameworkStores<ScreenSoundEfContext>();

        builder.Services.AddAuthorization();

        builder.Services.AddScoped<IRepository<Artist>, Repository<Artist>>();
        builder.Services.AddScoped<IRepository<Music>, Repository<Music>>();
        builder.Services.AddScoped<IRepository<Genre>, Repository<Genre>>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
        {
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        });

        builder.Services.AddCors(options =>
            options.AddPolicy("wasm", policy =>
                policy.WithOrigins([builder.Configuration["BackendUrl"] ?? "https://localhost:7059/",
                builder.Configuration["FrontendUrl"] ?? "https://localhost:7167/"])
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(pol => true)
                    .AllowAnyHeader()
                    .AllowCredentials()));

        var app = builder.Build();

		app.UseCors("wasm");

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}
        else
        {
			app.UseExceptionHandler("/Error");
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

        app.UseAuthorization();

        app.MapGroup("auth").MapIdentityApi<User>().WithTags("Authorization");

        app.MapPost("auth/logout", async ([FromServices] SignInManager<User> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }).RequireAuthorization().WithTags("Authorization");

		ArtistsEndpoints.Add(app);
		MusicsEndpoints.Add(app);
		GenresEndpoints.Add(app);

		app.Run();
    }
}