using HomeMusicLibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeMusicLibrary.Entities;

public sealed class DbContextSqLite : DbContext
{
    private readonly string _path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    
    public DbSet<AlbumModel> Albums { get; set; }
    public DbSet<ArtistModel> Artists { get; set; }
    public DbSet<SpotifyId> SpotifyIds { get; set; }
    public DbSet<TrackModel> Tracks { get; set; }

    public DbContextSqLite()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=" + _path + "/.config/HomeMusicLibrary/" + "HomeMusicLibrary.sqlite");
    }
}