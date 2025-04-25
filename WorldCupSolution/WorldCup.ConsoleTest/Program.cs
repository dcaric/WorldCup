using WorldCup.Data.Services;
using WorldCup.Data.Models;
using System.Numerics;

class Program
{
    static async Task Main(string[] args)
    {
        var configService = new ConfigService();
        var matchService = new MatchService(configService);
        var teamService = new TeamService(configService);
        var settingsService = new SettingsService();

        Console.WriteLine("--- CONFIG SETTINGS ---");
        Console.WriteLine($"Gender: {configService.Settings.Gender}");
        Console.WriteLine($"Language: {configService.Settings.Language}");
        Console.WriteLine($"Data Source: {configService.Settings.DataSource}");

        Console.WriteLine("--- LOADING TEAMS ---");
        var teams = await teamService.GetTeamsAsync(configService.Settings.Gender);
        Console.WriteLine($"Loaded {teams.Count} teams.");

        Console.WriteLine("--- LOADING TEAM RESULTS ---");
        var teamResults = await teamService.GetTeamResultsAsync(configService.Settings.Gender);
        Console.WriteLine($"Loaded {teamResults.Count} team results.");

        Console.WriteLine("--- LOADING GROUP RESULTS ---");
        var groupResults = await teamService.GetGroupResultsAsync(configService.Settings.Gender);
        foreach (var group in groupResults)
        {
            Console.WriteLine($"Group {group.Letter}: {group.OrderedTeams.Count} teams");
        }

        Console.WriteLine("--- LOADING MATCHES ---");
        var matches = await matchService.GetAllMatchesAsync(configService.Settings.Gender);
        Console.WriteLine($"Loaded {matches.Count} matches.");

        Console.WriteLine("--- LOADING MATCHES FOR FAVORITE TEAM ---");
        var favoriteTeam = configService.Settings.FavoriteTeam;
        var matchesForTeam = await matchService.GetMatchesForTeamAsync(configService.Settings.Gender, favoriteTeam);
        Console.WriteLine($"{matchesForTeam.Count} matches found for team {favoriteTeam}.");

        Console.WriteLine("--- TEST FAVORITE PLAYERS SAVE/LOAD ---");
        var favPlayers = settingsService.LoadFavoritePlayers();
        Console.WriteLine($"Loaded {favPlayers.Count} favorite players.");

        favPlayers.Add(new Player { Name = "Test Player", Captain = false, ShirtNumber = 99, Position = "Midfield" });
        settingsService.SaveFavoritePlayers(favPlayers);

        Console.WriteLine("Added 'Test Player' to favorites and saved.");

        Console.WriteLine("--- TEST FAVORITE TEAM SAVE/LOAD ---");
        settingsService.SaveFavoriteTeam("FRA");
        var favTeam = settingsService.LoadFavoriteTeam();
        Console.WriteLine($"Favorite team is now: {favTeam}");

        Console.WriteLine("\n--- ALL TESTS FINISHED ---");


        static async Task DownloadJsonFile(string gender, string endpoint)
        {
            var client = new HttpClient();
            var baseUrl = $"https://worldcup-vua.nullbit.hr/{gender}";
            var url = $"{baseUrl}/{endpoint}";

            var safeEndpointName = endpoint.Replace("/", "_");
            var fileName = $"./Data/{gender}_{safeEndpointName}.json";

            Console.WriteLine($"Downloading {url}...");

            try
            {
                var response = await client.GetStringAsync(url);
                Directory.CreateDirectory("./Data");
                await File.WriteAllTextAsync(fileName, response);
                Console.WriteLine($"Saved to {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to download {url}: {ex.Message}");
            }
        }

        Console.WriteLine("\n--- DOWNLOADING JSON FILES FOR OFFLINE MODE ---");

        await DownloadJsonFile("men", "matches");
        await DownloadJsonFile("men", "teams");
        await DownloadJsonFile("men", "teams/group_results");

        await DownloadJsonFile("women", "matches");
        await DownloadJsonFile("women", "teams");
        await DownloadJsonFile("women", "teams/group_results");

        Console.WriteLine("--- DOWNLOAD COMPLETED ---");


    }
}
