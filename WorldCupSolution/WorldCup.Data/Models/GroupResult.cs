namespace WorldCup.Data.Models;

public class GroupResult
{
    public int Id { get; set; }
    public string Letter { get; set; }
    public List<TeamResult> OrderedTeams { get; set; }
}
