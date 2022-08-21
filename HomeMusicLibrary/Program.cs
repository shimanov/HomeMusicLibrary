using Spectre.Console;

Settings();

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
AnsiConsole.MarkupLine("[aqua]version 0.0.0.1(alpha)[/]");
AnsiConsole.WriteLine("");
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
            "[red]Exit[/]"));

//Menu 1
if (mainMenu == "[cyan3]Add new artist in library[/]")
{
    var menuArtist = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .PageSize(10)
            .Title("Add new artist in library")
            .AddChoices(
                "[springgreen3_1]Add new artist from file[/]", 
                "[springgreen3_1]Add new artist[/]", 
                "[springgreen3_1]Add new album[/]", 
                "[springgreen3_1]Add song[/]", 
                "[springgreen3_1]Main menu[/]"));
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
if (mainMenu == "[cyan3]Exit[/]")
{
    Environment.Exit(0);
}

//Create settings for application
void Settings()
{
    //User profile folder
    var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    string settingsFolder = userFolder + "/.config/HomeMusicLibrary";

    //Try to create folder
    try
    {
        if (Directory.Exists(settingsFolder))
        {
            AnsiConsole.MarkupLine("[yellow]That path exists already.[/]");
            return;
        }

        var info = Directory.CreateDirectory(settingsFolder);
        AnsiConsole.MarkupLine("[yellow]Settings directory was created successfully[/]");

    }
    catch (Exception e)
    {
        AnsiConsole.WriteException(e);
    }
}



