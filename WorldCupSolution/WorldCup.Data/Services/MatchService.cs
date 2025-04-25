using System.Text.Json;
using WorldCup.Data.Models;
using System.Windows;

namespace WorldCup.Data.Services;

public class MatchService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public MatchService()
    {
        _httpClient = new HttpClient();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    private string BaseUrl(string gender) =>
        $"https://worldcup-vua.nullbit.hr/{gender.ToLower()}";


    private readonly ConfigService _config;

    public MatchService(ConfigService configService)
    {
        _httpClient = new HttpClient();
        _config = configService;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    // Get all matches
    // return is a List of Match, input parameter is gender and default is "man"
    public async Task<List<Match>> GetAllMatchesAsync(string gender = "men")
    {
        // checks if thre is locally saved file of matches
        // it will be if console app run before and it saved all JSONs
        if (_config.Settings.DataSource == "local")
        {
            var path = $"./Data/{gender}_matches.json"; // adjust if needed
            if (File.Exists(path))
            {
                // reads locally saved file
                var json = await File.ReadAllTextAsync(path);
                return JsonSerializer.Deserialize<List<Match>>(json, _jsonOptions) ?? new();
            }
            return new(); // fallback empty if file not found
        }

        // it comes here if there was no locally saved JSON for matches
        // here it sends API request to the backend and fetches matches
        var url = $"{BaseUrl(gender)}/matches";
        var response = await _httpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<List<Match>>(response, _jsonOptions) ?? new();
    }


    // Get matches for a specific country
    public async Task<List<Match>> GetMatchesForTeamAsync(string gender, string fifaCode)
    {
        var url = $"{BaseUrl(gender)}/matches/country?fifa_code={fifaCode}";
        Console.WriteLine(">>> URL: " + url);

        var response = await _httpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<List<Match>>(response, _jsonOptions) ?? new();
    }
}
