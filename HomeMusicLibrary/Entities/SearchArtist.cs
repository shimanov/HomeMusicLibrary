using SpotifyAPI.Web;

namespace HomeMusicLibrary.Entities;

public class SearchArtist
{
    public string token;
    public string artist;

    public async Task SearchArtistTask(string request)
    {
        if (request.Length != 0)
        {
            var spotify = new SpotifyClient(token);
            int compare = 0;
            try
            {
                var search = await spotify.Search.Item(new SearchRequest(SearchRequest.Types.Artist, artist));
                await foreach (var a in spotify.Paginate(search.Artists, response => response.Artists))
                {
                    
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