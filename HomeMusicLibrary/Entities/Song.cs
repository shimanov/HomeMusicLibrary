using HomeMusicLibrary.Model;
using Spectre.Console;
using SpotifyAPI.Web;

namespace HomeMusicLibrary.Entities;

public class Song
{
    public string token;
    public string albumId;

    public async Task SongTask()
    {
        var spotifySong = new SpotifyClient(token);
        var searchSongs = await spotifySong.Albums.GetTracks(albumId);
        if (searchSongs.Items != null)
        {
            foreach (var song in searchSongs.Items)
            {
                await using (var context = new DbContextSqLite())
                {
                    var s = new SongModel()
                    {
                        Id = Guid.NewGuid().ToString(),
                        AlbumId = song.Id,
                        TrackNumber = song.TrackNumber,
                        TrackName = song.Name,
                        Duration = song.DurationMs
                    };
                    await context.Songs.AddRangeAsync(s);
                    await context.SaveChangesAsync();
                }

                Console.WriteLine("DEBUG: {0}. {1}. {2}", 
                    song.TrackNumber, song.Name, song.DurationMs);
            }
        }
    }
}