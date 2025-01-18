using ScreenSound.Banco;
using ScreenSound.Models;

namespace ScreenSound.Menus;

internal class MenuRemoverArtista : Menu
{
    public override void Execute(IRepository<Artist> artistRepository)
    {
        base.Execute(artistRepository);
        ShowOptionTitle("Remover Artistas");

        Artist? artist = null;

        do
        {
            Console.Write("Digite o nome do artista que deseja remover: ");
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

        artistRepository.Remove(artist);
        Console.WriteLine($"O artista {artist.Name} foi removido com sucesso!");
        Thread.Sleep(4000);
        Console.Clear();
    }
}
