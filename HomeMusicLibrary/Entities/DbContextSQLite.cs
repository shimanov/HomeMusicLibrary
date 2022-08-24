using HomeMusicLibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeMusicLibrary.Entities;

public class DbContextSqLite : DbContext
{
    private readonly string _path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    
    public DbSet<Album> Albums { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<SpotifyId> SpotifyIds { get; set; }
    public DbSet<Track> Tracks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=" + _path + "/.config/HomeMusicLibrary/" + "HomeMusicLibrary.db");
    }
}