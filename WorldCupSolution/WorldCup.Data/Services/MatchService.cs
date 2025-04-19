using System.Text.Json;
using WorldCup.Data.Models;

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

    // 🔹 Get all matches
    public async Task<List<Match>> GetAllMatchesAsync(string gender = "men")
    {
        var url = $"{BaseUrl(gender)}/matches";
        var response = await _httpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<List<Match>>(response, _jsonOptions) ?? new();
    }

    // 🔹 Get matches for a specific country
    public async Task<List<Match>> GetMatchesForTeamAsync(string gender, string fifaCode)
    {
        var url = $"{BaseUrl(gender)}/matches/country?fifa_code={fifaCode}";
        var response = await _httpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<List<Match>>(response, _jsonOptions) ?? new();
    }
}
