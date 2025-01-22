using ScreenSound.Banco;
using ScreenSound.Models.Models;

namespace ScreenSound.Menus;

internal class MenuMostrarArtistas : Menu
{
    public override void Execute(IRepository<Artist> artistRepository)
    {
        base.Execute(artistRepository);
        ShowOptionTitle("Exibindo todos os artistas registradas na nossa aplicação");

        var artists = artistRepository.ListAll();
        foreach (var artist in artists)
        {
            Console.WriteLine($"Id: {artist.Id} - {artist.Name}\nBio: {artist.Bio}");

            if (artist.Musics.Count > 0)
            {
                Console.WriteLine("Discrography: ");
                foreach (var music in artist.Musics)
                    Console.WriteLine($"\t - {music.Name} ({music.ReleaseYear})");
            }

            Console.WriteLine();
        }

        Console.WriteLine("\n\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
