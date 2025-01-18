using ScreenSound.Banco;
using ScreenSound.Models;

namespace ScreenSound.Menus;

internal class MenuRegistrarMusica : Menu
{
    public override void Execute(IRepository<Artist> artistRepository)
    {
        base.Execute(artistRepository);
        ShowOptionTitle("Registro de Músicas");

        Console.Write("Digite o nome do artista a quem a música pertence: ");
        string artistName = Console.ReadLine()!;

        try
        {
            artistName = artistName.Trim();
            Artist? artist = artistRepository.FindByParameter(a => a.Name.Equals(artistName, StringComparison.OrdinalIgnoreCase));

            if (artist == null)
                throw new Exception("Artista não encontrado.");

            Console.Write("Digite o nome da música: ");
            string musicName = Console.ReadLine()!;
            Console.Write("Digite o ano de lançamento da música: ");
            int musicReleaseYear = int.TryParse(Console.ReadLine(), out int releaseYear) ? releaseYear : throw new Exception("Ano inválido");

            var music = new Music(musicName, musicReleaseYear) { Artist = artist };
            artist.AddMusic(music);

            artistRepository.Update(artist);

            Console.WriteLine($"A música {musicName} foi registrada com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Thread.Sleep(4000);
        Console.Clear();
    }
}
