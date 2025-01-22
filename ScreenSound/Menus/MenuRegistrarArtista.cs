using ScreenSound.Banco;
using ScreenSound.Models.Models;

namespace ScreenSound.Menus;

internal class MenuRegistrarArtista : Menu
{
    public override void Execute(IRepository<Artist> artist)
    {
        base.Execute(artist);
        ShowOptionTitle("Registro dos Artistas");
        Console.Write("Digite o nome do artista que deseja registrar: ");
        string artistName = Console.ReadLine()!;
        Console.Write("Digite a bio do artista que deseja registrar: ");
        string artistBio = Console.ReadLine()!;
        Artist artistToBeAdded = new Artist(artistName, artistBio);
        artist.Add(artistToBeAdded);
        Console.WriteLine($"O artista {artistName} foi registrado com sucesso!");
        Thread.Sleep(4000);
        Console.Clear();
    }
}
