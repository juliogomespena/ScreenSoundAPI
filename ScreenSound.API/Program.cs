using ScreenSound.API.Endpoints;
using ScreenSound.Banco;
using ScreenSound.Models;
using System.Text.Json.Serialization;

namespace ScreenSound.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ScreenSoundEfContext>();
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

        var app = builder.Build();

        ArtistsEndpoints.Add(app);
        MusicsEndpoints.Add(app);

        app.UseSwagger();
        app.UseSwaggerUI();

        app.Run();
    }
}