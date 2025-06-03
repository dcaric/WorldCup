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
        private ContextMenuStrip _favoritePlayerContextMenu;



        public Form1()
        {
            InitializeComponent();
            _favoritePlayerContextMenu = new ContextMenuStrip();
           // _favoritePlayerContextMenu.Items.Add("Remove from Favorites", null, RemoveFromFavorites_Click);


            _localizationService = new LocalizationService();

            /*
            panelPlayers.AllowDrop = true;
            panelFavoritePlayers.AllowDrop = true;
            */

            panelPlayers.DragEnter += Panel_DragEnter;
            panelFavoritePlayers.DragEnter += Panel_DragEnter;

            panelPlayers.DragDrop += PanelPlayers_DragDrop;
            panelFavoritePlayers.DragDrop += PanelFavoritePlayers_DragDrop;

        }

       /* private void RemoveFromFavorites_Click(object? sender, EventArgs e)
        {
            if (_favoritePlayerContextMenu.SourceControl is PlayerControl playerControl)
            {
                panelFavoritePlayers.Controls.Remove(playerControl);
                panelPlayers.Controls.Add(playerControl);

                playerControl.SetFavorite(false);
                playerControl.ContextMenuStrip = null; // remove context menu

                // Remove from model
                var player = playerControl.PlayerData;
                _favoritePlayers.RemoveAll(p => p.Name == player.Name);
                _settingsService.SaveFavoritePlayers(_favoritePlayers);
            }
        }*/

        private void PlayerControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.DoDragDrop(this, DragDropEffects.Move);
            }
        }


        private void Panel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(PlayerControl)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void PanelPlayers_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetData(typeof(PlayerControl)) is PlayerControl control)
            {
                panelFavoritePlayers.Controls.Remove(control);
                panelPlayers.Controls.Add(control);

                // Update favorite list
                var player = control.PlayerData;
                _favoritePlayers.RemoveAll(p => p.Name == player.Name);
                _settingsService.SaveFavoritePlayers(_favoritePlayers);
            }
        }



        private void PanelFavoritePlayers_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetData(typeof(PlayerControl)) is PlayerControl control)
            {
                panelPlayers.Controls.Remove(control);
                panelFavoritePlayers.Controls.Add(control);
                control.ContextMenuStrip = _favoritePlayerContextMenu;

                // Update favorite list
                var player = control.PlayerData;
                if (!_favoritePlayers.Any(p => p.Name == player.Name))
                    _favoritePlayers.Add(player);

                _settingsService.SaveFavoritePlayers(_favoritePlayers);
            }
        }




        // 
        private async Task LoadTeams()
        {
            var gender = cmbGender.SelectedItem?.ToString() ?? "men";

            try
            {
                _teams = await _teamService.GetTeamsAsync(gender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // user-friendly error shown in UI
                return;
            }

            cmbFavoriteTeam.Items.Clear();
            foreach (var team in _teams)
            {
                cmbFavoriteTeam.Items.Add($"{team.FifaCode} - {team.Country}");
            }
        }


        // starts first at program start
        private async void Form1_Load(object sender, EventArgs e)
        {
            _configService = new ConfigService();
            _teamService = new TeamService(_configService);
            _matchService = new MatchService(_configService);
            _settingsService = new SettingsService();

            cmbGender.Items.AddRange(new[] { "men", "women" });
            cmbGender.SelectedIndex = 0;

            //cmbTeamSide.Items.AddRange(new[] { "Home", "Away" });
            //cmbTeamSide.SelectedIndex = 0;

            // Load favorite players from disk
            _favoritePlayers = _settingsService.LoadFavoritePlayers();

            // 🟢 Render favorite player controls visually
            panelFavoritePlayers.Controls.Clear();
            foreach (var p in _favoritePlayers)
            {

                var ctrl = new PlayerControl(p, true);
                ctrl.Margin = new Padding(5);
                ctrl.ContextMenuStrip = _favoritePlayerContextMenu;

                panelFavoritePlayers.Controls.Add(ctrl);
            }

            // Load favorite teams from file
            var favTeamFile = "./Data/favourite_teams.txt";
            if (File.Exists(favTeamFile))
            {
                var lines = File.ReadAllLines(favTeamFile);
                foreach (var team in lines)
                {
                    lstFavouriteTeams.Items.Add(team);
                }
            }

            cmbLanguage.Items.AddRange(new[] { "en", "hr" });
            cmbLanguage.SelectedItem = _configService.Settings.Language;
            _localizationService.LoadLanguage(_configService.Settings.Language);
            ApplyLocalization();

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
            favTeamAdd.Text = _localizationService["addToFavorites"];
            btnRemoveFavoriteTeam.Text = _localizationService["remove"];
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
                // fetches all matches for specific gender and team
                _matches = await _matchService.GetMatchesForTeamAsync(gender, fifaCode);

                if (_matches.Count == 0)
                {
                    MessageBox.Show(_localizationService["noMatchesFound"]);

                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(_localizationService["matchLoadError"]);
                System.Diagnostics.Debug.WriteLine("LoadMatches error: " + ex.Message);
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

        private void cmbTeamSide_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstFavouritePlayers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panelPlayers_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelPlayers_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void lblFavoritePlayer_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadPlayers_Click(object sender, EventArgs e)
        {
            var selectedIndex = lstMatches.SelectedIndex;
            if (selectedIndex == -1) return;

            var selectedMatch = _matches[selectedIndex];

            TeamStatistics stats = null;

            stats = selectedMatch.HomeTeamStatistics;
            /*
            if (cmbTeamSide.SelectedItem?.ToString() == "Home")
            {
                stats = selectedMatch.HomeTeamStatistics;
            }
            else if (cmbTeamSide.SelectedItem?.ToString() == "Away")
            {
                stats = selectedMatch.AwayTeamStatistics;
            }
            */

            if (stats == null)
            {
                MessageBox.Show("No statistics available for this match/side.");
                return;
            }


            // DO NOT clear panelFavoritePlayers — it contains your favorites already
            panelPlayers.Controls.Clear();

            foreach (var player in stats.StartingEleven.Concat(stats.Substitutes))
            {
                bool isFavorite = _favoritePlayers.Any(p => p.Name == player.Name);

                // Skip adding to main list if it's already in favorites
                if (isFavorite) continue;

                var playerControl = new PlayerControl(player, isFavorite);
                playerControl.Margin = new Padding(5);
                panelPlayers.Controls.Add(playerControl);
            }
        }

    }
}
