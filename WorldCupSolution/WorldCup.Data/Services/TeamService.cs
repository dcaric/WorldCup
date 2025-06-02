using System.Text.Json;
using WorldCup.Data.Models;

namespace WorldCup.Data.Services;

public class TeamService
{
    private readonly HttpClient _httpClient;
    private readonly ConfigService _config;
    private readonly JsonSerializerOptions _jsonOptions;

    public TeamService(ConfigService configService)
    {
        _httpClient = new HttpClient();
        _config = configService;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    private string BaseUrl(string gender) =>
        $"https://worldcup-vua.nullbit.hr/{gender.ToLower()}";

    public async Task<List<Team>> GetTeamsAsync(string gender = "men")
    {
        if (_config.Settings.DataSource == "local")
        {
            var file = $"./Data/{gender}_teams.json";
            if (File.Exists(file))
            {
                var json = await File.ReadAllTextAsync(file);
                return JsonSerializer.Deserialize<List<Team>>(json, _jsonOptions) ?? new();
            }
        }

        var url = $"{BaseUrl(gender)}/teams";
        var response = await _httpClient.GetStringAsync(url);
        //Console.WriteLine($"GetTeamsAsync: response {response}");

        /*
           Deserialize is doing:
           converts JSON like:
            [
                {"country": "Croatia", "fifa_code": "CRO"},
                {"country": "Brazil", "fifa_code": "BRA"}
            ]

        into object like:
        
            teams[0].Country = "Croatia";
            teams[0].FifaCode = "CRO";

            teams[1].Country = "Brazil";
            teams[1].FifaCode = "BRA";
         */
        //Console.WriteLine($"GetTeamsAsync: JsonSerializer {JsonSerializer.Deserialize<List<Team>>(response, _jsonOptions)}");

        return JsonSerializer.Deserialize<List<Team>>(response, _jsonOptions) ?? new();
    }

    public async Task<List<TeamResult>> GetTeamResultsAsync(string gender = "men")
    {
        if (_config.Settings.DataSource == "local")
        {
            var file = $"./Data/{gender}_team_results.json";
            if (File.Exists(file))
            {
                var json = await File.ReadAllTextAsync(file);
                return JsonSerializer.Deserialize<List<TeamResult>>(json, _jsonOptions) ?? new();
            }
        }

        var url = $"{BaseUrl(gender)}/teams/results";
        var response = await _httpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<List<TeamResult>>(response, _jsonOptions) ?? new();
    }

    public async Task<List<GroupResult>> GetGroupResultsAsync(string gender = "men")
    {
        if (_config.Settings.DataSource == "local")
        {
            var file = $"./Data/{gender}_group_results.json";
            if (File.Exists(file))
            {
                var json = await File.ReadAllTextAsync(file);
                return JsonSerializer.Deserialize<List<GroupResult>>(json, _jsonOptions) ?? new();
            }
        }

        var url = $"{BaseUrl(gender)}/teams/group_results";
        //Console.WriteLine($"url {url}");

        var response = await _httpClient.GetStringAsync(url);
       // Console.WriteLine($"response {response}");

        return JsonSerializer.Deserialize<List<GroupResult>>(response, _jsonOptions) ?? new();
    }
}
