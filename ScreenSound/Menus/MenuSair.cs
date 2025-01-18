using ScreenSound.Banco;
using ScreenSound.Models;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Execute(IRepository<Artist> artist)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
