using System.Text.Json;
using WorldCup.Data.Models;

namespace WorldCup.Data.Services;

public class SettingsService
{
    private const string FavoritePlayersPath = "favorites.json";
    private const string FavoriteTeamPath = "favorite_team.txt";

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    // 🔹 Load favorite players from JSON file
    public List<Player> LoadFavoritePlayers()
    {
        if (!File.Exists(FavoritePlayersPath))
            return new List<Player>();

        var json = File.ReadAllText(FavoritePlayersPath);
        return JsonSerializer.Deserialize<List<Player>>(json, _jsonOptions) ?? new();
    }

    // 🔹 Save favorite players to JSON file
    public void SaveFavoritePlayers(List<Player> players)
    {
        var json = JsonSerializer.Serialize(players, _jsonOptions);
        File.WriteAllText(FavoritePlayersPath, json);
    }

    // 🔹 Load favorite team FIFA code
    public string LoadFavoriteTeam()
    {
        return File.Exists(FavoriteTeamPath)
            ? File.ReadAllText(FavoriteTeamPath)
            : "CRO"; // default fallback
    }

    // 🔹 Save favorite team FIFA code
    public void SaveFavoriteTeam(string fifaCode)
    {
        File.WriteAllText(FavoriteTeamPath, fifaCode);
    }
}
