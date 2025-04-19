using System.Text.Json;
using WorldCup.Data.Models;

namespace WorldCup.Data.Services;

public class TeamService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public TeamService()
    {
        _httpClient = new HttpClient();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    private string BaseUrl(string gender) =>
        $"https://worldcup-vua.nullbit.hr/{gender.ToLower()}";

    // 🔹 Get all teams (basic info)
    public async Task<List<Team>> GetTeamsAsync(string gender = "men")
    {
        var url = $"{BaseUrl(gender)}/teams";
        var response = await _httpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<List<Team>>(response, _jsonOptions) ?? new();
    }

    // 🔹 Get all team results (with match stats)
    public async Task<List<TeamResult>> GetTeamResultsAsync(string gender = "men")
    {
        var url = $"{BaseUrl(gender)}/teams/results";
        var response = await _httpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<List<TeamResult>>(response, _jsonOptions) ?? new();
    }

    // 🔹 Get all group results
    public async Task<List<GroupResult>> GetGroupResultsAsync(string gender = "men")
    {
        var url = $"{BaseUrl(gender)}/teams/group_results";
        var response = await _httpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<List<GroupResult>>(response, _jsonOptions) ?? new();
    }
}
