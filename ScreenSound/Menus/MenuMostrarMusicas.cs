﻿//using ScreenSound.Modelos;

//namespace ScreenSound.Menus;

//internal class MenuMostrarMusicas : Menu
//{
//    public override void Executar(Dictionary<string, Artist> artistasRegistrados)
//    {
//        base.Executar(artistasRegistrados);
//        ShowOptionTitle("Exibir detalhes do artista");
//        Console.Write("Digite o nome do artista que deseja conhecer melhor: ");
//        string nomeDoArtista = Console.ReadLine()!;
//        if (artistasRegistrados.ContainsKey(nomeDoArtista))
//        {
//            Artist artista = artistasRegistrados[nomeDoArtista];
//            Console.WriteLine("\nDiscografia:");
//            artista.ShowDiscography();
//            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
//            Console.ReadKey();
//            Console.Clear();
//        }
//        else
//        {
//            Console.WriteLine($"\nO artista {nomeDoArtista} não foi encontrado!");
//            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
//            Console.ReadKey();
//            Console.Clear();
//        }
//    }
//}
