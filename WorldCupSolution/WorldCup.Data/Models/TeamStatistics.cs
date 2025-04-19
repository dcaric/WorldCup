using System.Numerics;

namespace WorldCup.Data.Models;

public class TeamStatistics
{
    public string Country { get; set; }
    public int AttemptsOnGoal { get; set; }
    public int OnTarget { get; set; }
    public int OffTarget { get; set; }
    public int Blocked { get; set; }
    public int Woodwork { get; set; }
    public int Corners { get; set; }
    public int Offsides { get; set; }
    public int BallPossession { get; set; }
    public int PassAccuracy { get; set; }
    public int NumPasses { get; set; }
    public int PassesCompleted { get; set; }
    public int DistanceCovered { get; set; }
    public int BallsRecovered { get; set; }
    public int Tackles { get; set; }
    public int Clearances { get; set; }
    public int YellowCards { get; set; }
    public int RedCards { get; set; }
    public int FoulsCommitted { get; set; }
    public string Tactics { get; set; }
    public List<Player> StartingEleven { get; set; }
    public List<Player> Substitutes { get; set; }
}
