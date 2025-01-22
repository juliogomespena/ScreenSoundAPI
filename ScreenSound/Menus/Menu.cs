using ScreenSound.Banco;
using ScreenSound.Models.Models;

namespace ScreenSound.Menus;

internal class Menu
{
    public void ShowOptionTitle(string title)
    {
        int numberOfLeters = title.Length;
        string asterisk = string.Empty.PadLeft(numberOfLeters, '*');
        Console.WriteLine(asterisk);
        Console.WriteLine(title);
        Console.WriteLine(asterisk + "\n");
    }
    public virtual void Execute(IRepository<Artist> artistRepository)
    {
        Console.Clear();
    }
}
