using Microsoft.EntityFrameworkCore;

namespace HomeMusicLibrary.Entities;

public class DbContextSQLite : DbContext
{
    // public DbSet<> {
    //     get;
    //     set;
    // }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=HomeMusicLibrary.sqlite");
    }
}