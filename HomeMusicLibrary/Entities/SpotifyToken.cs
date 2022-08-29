using System.Net.Http.Json;
using Newtonsoft.Json;
using Spectre.Console;
using SpotifyAPI.Web;

namespace HomeMusicLibrary.Entities;

public class SpotifyToken
{
    public async Task<string> Token()
    {
        //Read ClientId and ClientSecret from settings file
        var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) 
                         + "/.config/HomeMusicLibrary/.settings";
        StreamReader reader = new StreamReader(userFolder);
        string jsonStr = await reader.ReadToEndAsync();
        SettingsFile settingsFile = JsonConvert.DeserializeObject<SettingsFile>(jsonStr) 
                                    ?? throw new InvalidOperationException();
        
        //Get access token
        var config = SpotifyClientConfig.CreateDefault();
        var request = new ClientCredentialsRequest(settingsFile.ClientID, settingsFile.ClientSecret);
        var response = await new OAuthClient(config).RequestToken(request);
        AnsiConsole.MarkupLine("[yellow]DEGUG: Token: {0}\n Expired: {1}\n ExpiresIn: {2}[/]", 
            response.AccessToken, response.IsExpired, response.ExpiresIn);
        return response.AccessToken;
    }
}