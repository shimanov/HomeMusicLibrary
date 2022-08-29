using HomeMusicLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using SpotifyAPI.Web;

namespace HomeMusicLibrary.Entities;

public class Artist
{
    public string token;
    private string artist;
    private string url;

    public async Task ArtistTask()
    {
        string[] art = File.ReadAllLines(Environment.CurrentDirectory + "/art.txt");
        if (art.Length != 0)
        {
            foreach (var s in art)
            {
                artist = s;
                var spotify = new SpotifyClient(token);
                try
                {
                    var search = await spotify.Search.Item(new SearchRequest(SearchRequest.Types.Artist, artist));
                    await foreach (var a in spotify.Paginate(search.Artists, (s) => s.Artists))
                    {
                        //TODO: Допилить
                        var count = -1;
                        if (s.Take(a.Name.Length).All(e => s[++count] == a.Name[count])) ++count;
                        AnsiConsole.MarkupLine("[yellow] DEBUG: {0}[/]", count);
                        
                        await using var context = new DbContextSqLite();
                        var entities = new ArtistModel
                        {
                            Id = Guid.NewGuid().ToString(),
                            ArtistName = a.Name,
                            ArtistId = a.Id
                        };
                        try
                        {
                            await context.Artists.AddRangeAsync(entities);
                            await context.SaveChangesAsync();
                            AnsiConsole.MarkupLine("[yellow] DEBUG: Artist: {0}\n ID: {1}\n :check_mark_button: Записано в таблицу\n[/]", 
                                a.Name, a.Id);
                        }
                        catch (Exception e)
                        {
                            AnsiConsole.WriteException(e);
                        }
                    }
                }
                catch (Exception e)
                {
                    AnsiConsole.WriteException(e);
                }
            }   
        }
        else
        {
            AnsiConsole.WriteLine("[yellow3] Нет данных для поиска[/]");
        }
    }
}