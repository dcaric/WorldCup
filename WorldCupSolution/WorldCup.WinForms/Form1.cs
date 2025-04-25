using WorldCup.Data.Models;
using WorldCup.Data.Services;

namespace WorldCup.WinForms
{
    public partial class Form1 : Form
    {
        private ConfigService _configService;
        private TeamService _teamService;
        private MatchService _matchService;
        private List<Team> _teams;
        private List<Match> _matches;
        public Form1()
        {
            InitializeComponent();
        }


        private async Task LoadTeams()
        {
            var gender = cmbGender.SelectedItem?.ToString() ?? "men";
            _teams = await _teamService.GetTeamsAsync(gender);
            cmbFavoriteTeam.Items.Clear();
            foreach (var team in _teams)
            {
                cmbFavoriteTeam.Items.Add($"{team.FifaCode} - {team.Country}");
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            _configService = new ConfigService();
            _teamService = new TeamService(_configService);
            _matchService = new MatchService(_configService);

            cmbGender.Items.AddRange(new[] { "men", "women" });
            cmbGender.SelectedIndex = 0;

            await LoadTeams();
        }

        private async void btnLoadMatches_Click(object sender, EventArgs e)
        {
            var gender = cmbGender.SelectedItem?.ToString() ?? "men";
            var selectedTeam = cmbFavoriteTeam.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedTeam)) return;

            var fifaCode = selectedTeam.Split('-')[0].Trim();
            _matches = await _matchService.GetMatchesForTeamAsync(gender, fifaCode);
            // Debug FIFA Code before calling
            //MessageBox.Show($"Loading matches for FIFA Code: {fifaCode}");

            try
            {
                _matches = await _matchService.GetMatchesForTeamAsync(gender, fifaCode);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("ERROR: " + ex.Message);
                return;
            }

            lstMatches.Items.Clear();
            foreach (var match in _matches)
            {
                lstMatches.Items.Add($"{match.StageName}: {match.HomeTeamCountry} vs {match.AwayTeamCountry}");
            }
        }

        private void btnLoadPlayers_Click(object sender, EventArgs e)
        {
            var selectedIndex = lstMatches.SelectedIndex;
            if (selectedIndex == -1) return;

            var selectedMatch = _matches[selectedIndex];
            lstPlayers.Items.Clear();

            if (selectedMatch.HomeTeamStatistics == null)
            {
                //MessageBox.Show("No statistics available for this match!");
                return;
            }

            foreach (var player in selectedMatch.HomeTeamStatistics.StartingEleven)
            {
                lstPlayers.Items.Add(player.Name);
            }
        }
    }
}
