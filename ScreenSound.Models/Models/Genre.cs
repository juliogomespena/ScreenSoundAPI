using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Models.Models;

public class Genre(string name)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public string? Description { get; set; }
    public virtual ICollection<Music>? Musics { get; set; }
}
