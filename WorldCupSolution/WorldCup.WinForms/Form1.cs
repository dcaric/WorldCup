using WorldCup.Data.Models;
using WorldCup.Data.Services;

namespace WorldCup.WinForms
{
    public partial class Form1 : Form
    {
        // creates objects for each service
        private ConfigService _configService;
        private TeamService _teamService;
        private MatchService _matchService;
        private List<Team> _teams;
        private List<Match> _matches;
        public Form1()
        {
            InitializeComponent();
        }

        // 
        private async Task LoadTeams()
        {
            var gender = cmbGender.SelectedItem?.ToString() ?? "men";

            // calls method GetTeamsAsync from team service
            _teams = await _teamService.GetTeamsAsync(gender);

            // clears combo favourite teams
            cmbFavoriteTeam.Items.Clear();
            // loads data into combo favourite teams
            foreach (var team in _teams)
            {
                cmbFavoriteTeam.Items.Add($"{team.FifaCode} - {team.Country}");
            }
        }

        // starts first at program start
        private async void Form1_Load(object sender, EventArgs e)
        {
            // initializes objects for each service
            _configService = new ConfigService();
            _teamService = new TeamService(_configService);
            _matchService = new MatchService(_configService);

            // fills combo gender with man and woman
            cmbGender.Items.AddRange(new[] { "men", "women" });
            cmbGender.SelectedIndex = 0;

            // calls LoadTeams function
            await LoadTeams();
        }

        // function which is called at button laod matches is clicked
        private async void btnLoadMatches_Click(object sender, EventArgs e)
        {
            // fetch from cmbGender value, converts into string and in
            // case nothing is found takes "man"
            var gender = cmbGender.SelectedItem?.ToString() ?? "men";
            //MessageBox.Show($"gender: {gender}");

            // takes from cmbFavoriteTeam value
            var selectedTeam = cmbFavoriteTeam.SelectedItem?.ToString();
            // it no team is selected exits
            if (string.IsNullOrEmpty(selectedTeam)) return;

            // extracts fifaCode from the string, splits by "-" and takes first part
            var fifaCode = selectedTeam.Split('-')[0].Trim();

            // Debug FIFA Code before calling
            //MessageBox.Show($"Loading matches for FIFA Code: {fifaCode}");

            try
            {
                // calls GetMatchesForTeamAsync from match service
                // fetches all matches for specific gender and team
                _matches = await _matchService.GetMatchesForTeamAsync(gender, fifaCode);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("ERROR: " + ex.Message);
                return;
            }

            // cleast list 
            lstMatches.Items.Clear();

            // fills list of matches 
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

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbFavoriteTeam_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
