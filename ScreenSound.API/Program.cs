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

        builder.Services.AddScoped<IRepository<Artist>, Repository<Artist>>();
        builder.Services.AddScoped<IRepository<Music>, Repository<Music>>();
        builder.Services.AddScoped<IRepository<Genre>, Repository<Genre>>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
        {
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.SerializerOptions.TypeInfoResolver = ScreenSoundJsonContext.Default;
        });

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("https://localhost:7167")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        var app = builder.Build();

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

		app.UseCors();

		ArtistsEndpoints.Add(app);
		MusicsEndpoints.Add(app);
		GenresEndpoints.Add(app);

		app.Run();
    }
}