using Microsoft.EntityFrameworkCore;
using ScreenSound.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco;

public class ScreenSoundEfContext : DbContext
{
    private readonly string _dbConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = ScreenSound-EFv2; Integrated Security = True; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = false";

    public DbSet<Artist> Artist { get; set; }

    public DbSet<Music> Music { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(_dbConnectionString)
            .UseLazyLoadingProxies();
    }
}
