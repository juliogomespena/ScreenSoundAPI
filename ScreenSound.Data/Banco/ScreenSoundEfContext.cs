using Microsoft.EntityFrameworkCore;
using ScreenSound.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco;

public class ScreenSoundEfContext : DbContext
{
    public ScreenSoundEfContext(DbContextOptions options) : base(options) { }

    public ScreenSoundEfContext()
    {
    }

    private readonly string _dbConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = ScreenSound-EFv2; Integrated Security = True; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = false";

    public DbSet<Artist> Artist { get; set; }

    public DbSet<Music> Music { get; set; }

    public DbSet<Genre> Genre { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>()
            .HasMany(m => m.Musics)
            .WithMany(g => g.Genres);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;

        optionsBuilder
            .UseSqlServer(_dbConnectionString)
            .UseLazyLoadingProxies();
    }
}
