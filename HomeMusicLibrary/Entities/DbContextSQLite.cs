using Microsoft.EntityFrameworkCore;

namespace HomeMusicLibrary.Entities;

public class DbContextSqLite : DbContext
{
    string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    // public DbSet<> {
    //     get;
    //     set;
    // }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=" + path + "/.config/HomeMusicLibrary/" + "HomeMusicLibrary.db");
    }
}