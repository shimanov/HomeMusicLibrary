using HomeMusicLibrary.Model;
using Spectre.Console;
using SpotifyAPI.Web;

namespace HomeMusicLibrary.Entities;

public class Album
{
    public string token;
    public string artistId;

    public async Task AlbumTask()
    {
        var spotifyAlbum = new SpotifyClient(token);
        var searchAlbum = await spotifyAlbum.Artists.GetAlbums(artistId);
        if (searchAlbum.Items != null)
        {
            foreach (var album in searchAlbum.Items)
            {
                await using (var context = new DbContextSqLite())
                {
                    var albumDb = new AlbumModel()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ArtistId = artistId,
                        AlbumName = album.Name,
                        TypeAlbum = album.AlbumType,
                        RealiseDate = album.ReleaseDate,
                        TotalTracks = album.TotalTracks,
                        AlbumId = album.Id
                    };
                    try
                    {
                        await context.Albums.AddRangeAsync(albumDb);
                        await context.SaveChangesAsync();
                        AnsiConsole.MarkupLine("[yellow2] DEBUG: Album: {0}\n realise date: {1}\n Type: {2}\n Total Tracks: {3}\n Id: {4}\n\n " +
                                               ":check_mark_button: Записано в таблицу\n\n[/]",
                            album.Name, album.ReleaseDate, album.Type, album.TotalTracks, album.Id);
                    }
                    catch (Exception e)
                    {
                        AnsiConsole.WriteException(e);
                    }
                }
            }
        }
    }
}