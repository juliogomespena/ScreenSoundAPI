using ScreenSound.Banco;
using ScreenSound.Models.Models;

namespace ScreenSound.Menus;

internal class MenuAtualizarArtista : Menu
{
    public override void Execute(IRepository<Artist> artistRepository)
    {
        base.Execute(artistRepository);
        ShowOptionTitle("Atualizar Artistas");

        Artist? artist = null;

        do
        {
            Console.Write("Digite o nome do artista que deseja atualizar: ");
            string artistName = Console.ReadLine()!;

            try
            {
                artistName = artistName.Trim();
                artist = artistRepository.FindByParameter(a => a.Name.Equals(artistName, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } while (artist is null);

        Console.Write("Digite o nome atualizado do artista: ");
        artist.Name = Console.ReadLine()!;
        Console.Write("Digite a bio atualizada do artista: ");
        artist.Bio = Console.ReadLine()!;

        artistRepository.Update(artist);
        Console.WriteLine($"O artista {artist.Name} foi atualizado com sucesso!");
        Thread.Sleep(4000);
        Console.Clear();
    }
}
