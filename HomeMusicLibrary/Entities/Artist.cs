using HomeMusicLibrary.Model;
using Spectre.Console;
using SpotifyAPI.Web;

namespace HomeMusicLibrary.Entities;

public class Artist
{
    public string token;
    private string artist;

    public async Task ArtistTask()
    {
        string[] art = await File.ReadAllLinesAsync(Environment.CurrentDirectory + "/art.txt");
        if (art.Length != 0)
        {
            foreach (var s in art)
            {
                artist = s;
                var spotify = new SpotifyClient(token);
                int compareResult = 0; 
                try
                {
                    
                    var search = await spotify.Search.Item(new SearchRequest(SearchRequest.Types.Artist, artist));
                    await foreach (var a in spotify.Paginate(search.Artists, response => response.Artists))
                    {
                        compareResult = string.Compare(artist, a.Name, StringComparison.OrdinalIgnoreCase);
                        if (compareResult == 0)
                        {
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
                                AnsiConsole.MarkupLine(
                                    "[yellow2] DEBUG: Artist: {0}\n ID: {1}\n :check_mark_button: Записано в таблицу\n[/]",
                                    a.Name, a.Id);
                            }
                            catch (Exception e)
                            {
                                AnsiConsole.WriteException(e);
                            }
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red3_1] DEBUG: {0}[/]", compareResult);
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
            AnsiConsole.WriteLine("[yellow2] Нет данных для поиска[/]");
        }
    }
}