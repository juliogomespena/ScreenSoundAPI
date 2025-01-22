using ScreenSound.Banco;
using ScreenSound.Menus;
using ScreenSound.Models.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        Dictionary<int, Menu> menu = new();
        menu.Add(1, new MenuRegistrarArtista());
        menu.Add(2, new MenuRemoverArtista());
        menu.Add(3, new MenuAtualizarArtista());
        menu.Add(4, new MenuMostrarArtistas());
        menu.Add(5, new MenuRegistrarMusica());
        //menu.Add(4, new MenuMostrarMusicas());
        menu.Add(-1, new MenuSair());

        IRepository<Artist> artistRepository = new Repository<Artist>(new ScreenSoundEfContext());

        void ShowHeader()
        {
            Console.WriteLine(@"

░██████╗░█████╗░██████╗░███████╗███████╗███╗░░██╗  ░██████╗░█████╗░██╗░░░██╗███╗░░██╗██████╗░
██╔════╝██╔══██╗██╔══██╗██╔════╝██╔════╝████╗░██║  ██╔════╝██╔══██╗██║░░░██║████╗░██║██╔══██╗
╚█████╗░██║░░╚═╝██████╔╝█████╗░░█████╗░░██╔██╗██║  ╚█████╗░██║░░██║██║░░░██║██╔██╗██║██║░░██║
░╚═══██╗██║░░██╗██╔══██╗██╔══╝░░██╔══╝░░██║╚████║  ░╚═══██╗██║░░██║██║░░░██║██║╚████║██║░░██║
██████╔╝╚█████╔╝██║░░██║███████╗███████╗██║░╚███║  ██████╔╝╚█████╔╝╚██████╔╝██║░╚███║██████╔╝
╚═════╝░░╚════╝░╚═╝░░╚═╝╚══════╝╚══════╝╚═╝░░╚══╝  ╚═════╝░░╚════╝░░╚═════╝░╚═╝░░╚══╝╚═════╝░
");
            Console.WriteLine("Boas vindas ao Screen Sound 3.0!");
        }

        void ShowMenuOptions()
        {
            ShowHeader();
            Console.WriteLine("\nDigite 1 para registrar um artista");
            Console.WriteLine("Digite 2 para remover um artista");
            Console.WriteLine("Digite 3 para atualizar um artista");
            Console.WriteLine("Digite 4 para mostrar todos os artistas");
            Console.WriteLine("Digite 5 para registrar uma música");
            Console.WriteLine("Digite -1 para sair");

            Console.Write("\nDigite a sua opção: ");
            string optionChoosen = Console.ReadLine()!;
            int numericOption = int.Parse(optionChoosen);

            if (menu.ContainsKey(numericOption))
            {
                Menu choosenMenu = menu[numericOption];
                choosenMenu.Execute(artistRepository);
                if (numericOption > 0) ShowMenuOptions();
            }
            else
            {
                Console.WriteLine("Opção inválida");
            }
        }

        ShowMenuOptions();
    }
}
