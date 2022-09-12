using Spectre.Console;
using SpotifyAPI.Web;

namespace HomeMusicLibrary.Entities;

public class SearchArtist
{
    public string token;

    public async Task SearchArtistTask(string? request)
    {
        if (request.Length != 0)
        {
            var spotify = new SpotifyClient(token);
            int compare = 0;
            try
            {
                var search = await spotify.Search.Item(new SearchRequest(SearchRequest.Types.Artist, 
                    request.ToLower()));
                await foreach (var a in spotify.Paginate(search.Artists, response => response.Artists))
                {
                    compare = string.Compare(request, a.Name, StringComparison.OrdinalIgnoreCase);
                    if (compare == 0)
                    {
                        var art = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Результат поиска  по запросу " + request)
                                .AddChoices(a.Name));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}