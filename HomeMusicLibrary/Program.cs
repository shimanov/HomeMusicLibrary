using System.Net.Http.Json;
using HomeMusicLibrary;
using HomeMusicLibrary.Entities;
using Newtonsoft.Json;
using Spectre.Console;

//FIGLET
var font = FigletFont.Load("src/ANSI_Shadow.flf");

AnsiConsole.Write("\n");
AnsiConsole.Write(
    new FigletText(font, " Home")
        .LeftAligned()
        .Color(Color.Turquoise2));
AnsiConsole.Write(
    new FigletText(font, "   Music")
        .LeftAligned()
        .Color(Color.DarkTurquoise));
AnsiConsole.Write(new FigletText(font, "     Library")
    .LeftAligned()
    .Color(Color.Cyan3));
AnsiConsole.MarkupLine("[aqua]version 0.0.0.3(alpha)[/]");
AnsiConsole.WriteLine("");

Settings(false);

//Spotify token
var spotifyToken = new SpotifyToken();
string token = await spotifyToken.Token();

//Main menu
// 1. Add new artist in library
//      Add from file
//      Add single artist
// 2. View library
//      View Artist
//      View albums
//      View songs
// 3. Exit

//Main menu
var mainMenu = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .PageSize(10)
        .Title("[deepskyblue1]What do you want to do?[/]")
        .MoreChoicesText("[grey](Move up and down to reveal more functions)[/]")
        .AddChoices(
            "[cyan3]Add new artist in library[/]", 
            "[cyan3]View library[/]",
            "[dodgerblue1]Edit settings[/]",
            "[red3_1]Exit[/]"));

//Menu 1
if (mainMenu == "[cyan3]Add new artist in library[/]")
{
    var menuArtist = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .PageSize(10)
            .Title("Add new artist in library")
            .AddChoices(
                "[springgreen3_1]Import artists from Spotify[/]",
                "[springgreen3_1]Add new artist from file[/]",
                "[springgreen3_1]Add new artist[/]", 
                "[springgreen3_1]Add new album[/]", 
                "[springgreen3_1]Add song[/]", 
                "[springgreen3_1]Main menu[/]"));
    if (menuArtist == "[springgreen3_1]Add new artist from file[/]")
    {
        var artist = new Artist()
        {
            token = token
        };
        await artist.ArtistTask();
        
    }
}

//Menu 2
if (mainMenu == "[cyan3]View library[/]")
{
    var menuView = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .PageSize(10)
            .Title("View library")
            .AddChoices(
                "[darkturquoise]View artist[/]", 
                "[darkturquoise]View album[/]", 
                "[darkturquoise]View songs[/]", 
                "[darkturquoise]Main menu[/]"));
}

//Menu 3
if (mainMenu == "[dodgerblue1]Edit settings[/]")
{
    Settings(true);
}

//Menu 4
if (mainMenu == "[red3_1]Exit[/]")
{
    Environment.Exit(0);
}

//Create settings for application
// flag: true - change settings, false - no change settings
void Settings(bool flag)
{
    if (flag == false)
    {
        //User profile folder
        var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        //Application folder
        var settingsFolder = userFolder + "/.config/HomeMusicLibrary";

        //Try to create folder
        try
        {
            if (Directory.Exists(settingsFolder))
            {
                AnsiConsole.MarkupLine("[yellow]DEBUG: That path exists already.[/]");
                AnsiConsole.MarkupLine("[yellow]DEBUG: The file is created earlier[/]");
                //Load settings
            }
            else
            {
                //Create directory
                var info = Directory.CreateDirectory(settingsFolder);
                AnsiConsole.MarkupLine("[yellow]DEBUG: Settings directory was created successfully[/]");

                //Setup and write settings in file
                var settingsFile = new SettingsFile();
                AnsiConsole.MarkupLine("[palegreen1_1]Insert you Spotify ClientID[/]");
                var clientId = Console.ReadLine();
                if (clientId != null) settingsFile.ClientID = clientId;

                AnsiConsole.MarkupLine("[palegreen1_1]Insert you Spotify ClientSecret[/]");
                var clientSecret = Console.ReadLine();
                if (clientSecret != null) settingsFile.ClientSecret = clientSecret;

                var settingsMenu = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[palegreen1_1]Chose database[/]")
                    .AddChoices("SQLite", "PostgreSQL", "MariaDB"));

                if (settingsMenu == "SQLite")
                    //Create SQLite
                    settingsFile.Path = "sqlite";

                //Create settings file
                var jsonResult = JsonConvert.SerializeObject(settingsFile);
                if (File.Exists(settingsFolder + "/.settings")) return;
                using var tw = new StreamWriter(settingsFolder + "/.settings", true);
                tw.WriteLine(jsonResult);
                tw.Close();
                AnsiConsole.MarkupLine("[yellow]DEBUG: Create settings file successful[/]");
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }
    else
    {
        //User profile folder
        var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        //Application folder
        var settingsFolder = userFolder + "/.config/HomeMusicLibrary";
        //Delete .settings file
        File.Delete(settingsFolder + "/.settings");
        //Setup and write settings in file
        var settingsFile = new SettingsFile();
        AnsiConsole.MarkupLine("[palegreen1_1]Insert you Spotify ClientID[/]");
        var clientId = Console.ReadLine();
        if (clientId != null) settingsFile.ClientID = clientId;

        AnsiConsole.MarkupLine("[palegreen1_1]Insert you Spotify ClientSecret[/]");
        var clientSecret = Console.ReadLine();
        if (clientSecret != null) settingsFile.ClientSecret = clientSecret;

        var settingsMenu = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("[palegreen1_1]Chose database[/]")
            .AddChoices("SQLite", "PostgreSQL", "MariaDB"));

        if (settingsMenu == "SQLite")
            //Create SQLite
            settingsFile.Path = "sqlite";

        //Create settings file
        var jsonResult = JsonConvert.SerializeObject(settingsFile);
        if (File.Exists(settingsFolder + "/.settings")) return;
        using var tw = new StreamWriter(settingsFolder + "/.settings", true);
        tw.WriteLine(jsonResult);
        tw.Close();
        AnsiConsole.MarkupLine("[palegreen1_1]Change file settings successfully![/]");
    }
}



