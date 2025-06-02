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
        private SettingsService _settingsService;
        private List<Player> _favoritePlayers = new();
        private LocalizationService _localizationService;

        public Form1()
        {
            InitializeComponent();
            _localizationService = new LocalizationService();
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
            _settingsService = new SettingsService();

            // fills combo gender with man and woman
            cmbGender.Items.AddRange(new[] { "men", "women" });
            cmbGender.SelectedIndex = 0;


            // Load favorite players
            _favoritePlayers = _settingsService.LoadFavoritePlayers();
            foreach (var p in _favoritePlayers)
            {
                lstFavouritePlayers.Items.Add(p.Name);
            }

            // Load favorite teams
            var favTeamFile = "./Data/favourite_teams.txt";
            if (File.Exists(favTeamFile))
            {
                var lines = File.ReadAllLines(favTeamFile);
                foreach (var team in lines)
                {
                    lstFavouriteTeams.Items.Add(team);
                }
            }

            cmbLanguage.Items.AddRange(new[] { "en", "hr" }); // English, Croatian
            cmbLanguage.SelectedItem = _configService.Settings.Language;
            _localizationService.LoadLanguage(_configService.Settings.Language);
            ApplyLocalization();

            // calls LoadTeams function
            await LoadTeams();
        }

        private void ApplyLocalization()
        {
            if (_localizationService == null) return;

            this.Text = _localizationService["title"];
            lblGender.Text = _localizationService["gender"];
            lblLanguage.Text = _localizationService["language"];
            lblFavoriteTeam.Text = _localizationService["favoriteTeam"];
            lblFavoritePlayer.Text = _localizationService["favoritePlayer"];
            btnLoadMatches.Text = _localizationService["loadMatches"];
            btnLoadPlayers.Text = _localizationService["loadPlayers"];
            favTeamAdd.Text = _localizationService["addToFavorites"];
            btnRemoveFavoriteTeam.Text = _localizationService["remove"];
            favPlayerAdd.Text = _localizationService["addToFavorites"];
            btnRemoveFavoritePlayer.Text = _localizationService["remove"];
            Country.Text = _localizationService["country"];
            listOfMatches.Text = _localizationService["listOfMatches"];
            listOfPlayers.Text = _localizationService["listOfPlayers"];

            this.Invalidate();
            this.Refresh();
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lstMatches_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void favTeamAdd_Click(object sender, EventArgs e)
        {
            var selectedTeam = cmbFavoriteTeam.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedTeam)) return;

            if (!lstFavouriteTeams.Items.Contains(selectedTeam))
            {
                lstFavouriteTeams.Items.Add(selectedTeam);

                // Ensure Data folder exists
                Directory.CreateDirectory("./Data");

                // Save favorite teams
                File.WriteAllLines("./Data/favourite_teams.txt", lstFavouriteTeams.Items.Cast<string>());
            }
            else
            {
                MessageBox.Show("This team is already in the favorite list.");
            }
        }


        private void favPlayerAdd_Click(object sender, EventArgs e)
        {
            var matchIndex = lstMatches.SelectedIndex;
            var playerIndex = lstPlayers.SelectedIndex;

            if (matchIndex == -1 || playerIndex == -1)
            {
                MessageBox.Show("Please select both a match and a player.");
                return;
            }

            var selectedMatch = _matches[matchIndex];
            var player = selectedMatch.HomeTeamStatistics.StartingEleven[playerIndex];

            if (_favoritePlayers.Any(p => p.Name == player.Name))
            {
                MessageBox.Show("Player is already in favorites.");
                return;
            }

            _favoritePlayers.Add(player);
            lstFavouritePlayers.Items.Add(player.Name);

            // ✅ Ensure folder exists before saving
            Directory.CreateDirectory("./Data");
            _settingsService.SaveFavoritePlayers(_favoritePlayers);
        }

        private void btnRemoveFavoritePlayer_Click_Click(object sender, EventArgs e)
        {
            var selectedIndex = lstFavouritePlayers.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Select a player to remove.");
                return;
            }

            var playerName = lstFavouritePlayers.SelectedItem.ToString();

            // Remove from UI
            lstFavouritePlayers.Items.RemoveAt(selectedIndex);

            // Remove from list
            var player = _favoritePlayers.FirstOrDefault(p => p.Name == playerName);
            if (player != null)
            {
                _favoritePlayers.Remove(player);
                Directory.CreateDirectory("./Data");
                _settingsService.SaveFavoritePlayers(_favoritePlayers);
            }
        }

        private void btnRemoveFavoriteTeam_Click_Click(object sender, EventArgs e)
        {
            var selectedIndex = lstFavouriteTeams.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Select a team to remove.");
                return;
            }

            // Remove from UI
            lstFavouriteTeams.Items.RemoveAt(selectedIndex);

            // Save updated list to disk
            Directory.CreateDirectory("./Data");
            File.WriteAllLines("./Data/favourite_teams.txt", lstFavouriteTeams.Items.Cast<string>());
        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedLanguage = cmbLanguage.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedLanguage))
            {
                _configService.Settings.Language = selectedLanguage;
                _configService.Save(); // Saves to settings.json

                _localizationService.LoadLanguage(selectedLanguage);
                ApplyLocalization();

            }
        }
    }
}
