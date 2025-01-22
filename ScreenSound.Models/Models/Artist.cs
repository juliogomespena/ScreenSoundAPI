using System.Text.Json.Serialization;

namespace ScreenSound.Models.Models;

public class Artist(string name, string bio, string profilePicture = "= \"https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png\"")
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public string Bio { get; set; } = bio;
    public string ProfilePicture { get; set; } = profilePicture;
    public virtual ICollection<Music> Musics { get; set; } = new List<Music>();

    public void AddMusic(Music music)
    {
        Musics.Add(music);
    }

    public void ShowDiscography()
    {
        Console.WriteLine($"Discografia do artista {Name}");
        foreach (var music in Musics)
        {
            Console.WriteLine($"Música: {music.Name} - ({music.ReleaseYear})");
        }
    }

    public override string ToString()
    {
        return $@"Id: {Id}
            Name: {Name}
            Foto de Perfil: {ProfilePicture}
            Bio: {Bio}";
    }
}