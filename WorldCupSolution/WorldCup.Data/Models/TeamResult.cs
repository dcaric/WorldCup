namespace WorldCup.Data.Models;

public class TeamResult
{
    public int Id { get; set; }                       
    public string Country { get; set; }
    public string? AlternateName { get; set; }       
    public string FifaCode { get; set; }
    public int GroupId { get; set; }
    public string GroupLetter { get; set; }
    public int Wins { get; set; }
    public int Draws { get; set; }
    public int Losses { get; set; }
    public int GamesPlayed { get; set; }
    public int Points { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int GoalDifferential { get; set; }
}
