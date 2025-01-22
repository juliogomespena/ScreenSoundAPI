using System.Text.Json.Serialization;

namespace ScreenSound.Models.Models;

public class Music(string name, int artistId, int? releaseYear = null)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public int? ReleaseYear { get; set; } = releaseYear;
    public int ArtistId { get; set; } = artistId;
    public virtual Artist Artist { get; set; } = null!;
    public virtual ICollection<Genre>? Genres { get; set; }

    public void ShowInfo() => Console.WriteLine($"Name: {Name}");

    public override string ToString() => $"Id: {Id}\nName: {Name}";
}