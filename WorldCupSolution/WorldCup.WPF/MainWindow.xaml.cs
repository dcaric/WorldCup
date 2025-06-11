using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using WorldCup.Data.Models;
using WorldCup.Data.Services;
using System.IO;
using System.Windows.Media.Animation; // Needed for DoubleAnimation
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Text.Json;

namespace WorldCup.WPF
{
    public partial class MainWindow : Window
    {
        private ConfigService _configService;
        private TeamService _teamService;
        private MatchService _matchService;
        private SettingsService _settingsService;
        private LocalizationService _localizationService;
        private ContextMenu _favoritePlayerRemoveContextMenu;
        private ContextMenu _favoritePlayerAddContextMenu;
        private ContextMenu _favoriteTeamRemoveContextMenu;
        private ContextMenu _favoriteTeamInfoContextMenu;


        private List<Team> _teams = new();
        private List<Match> _matches = new();
        private List<Player> _favoritePlayers = new();
        private List<Player> _allPlayersInMatch = new();
        
        private List<Player> _currentHomePlayers = new();
        private List<Player> _currentAwayPlayers = new();

        private TeamStatistics statsHome;
        private TeamStatistics statsAway;

        // Context menues
        MenuItem teamInfo;
        MenuItem removeTeam;
        MenuItem addPlayer;
        MenuItem removePlayer;


        public MainWindow()
        {
            InitializeComponent();

            this.Width = 1024;
            this.Height = 768;
            this.WindowState = WindowState.Normal;


            teamInfo = new MenuItem();
            removeTeam = new MenuItem();
            addPlayer = new MenuItem();
            removePlayer = new MenuItem();

            _configService = new ConfigService();
            _teamService = new TeamService(_configService);
            _matchService = new MatchService(_configService);
            _settingsService = new SettingsService();
            _localizationService = new LocalizationService();

            cmbGender.Items.Add("men");
            cmbGender.Items.Add("women");
            cmbGender.SelectedItem = _configService.Settings.Gender;

            cmbLanguage.Items.Add("en");
            cmbLanguage.Items.Add("hr");
            cmbLanguage.SelectedItem = _configService.Settings.Language;
            _localizationService.LoadLanguage(_configService.Settings.Language);
            ApplyLocalization();

            btnLoadMatches.Click += BtnLoadMatches_Click;
            btnLoadPlayers.Click += BtnLoadPlayers_Click;
            //btnAddToFavorites.Click += BtnAddToFavorites_Click;
            cmbGender.SelectionChanged += CmbGender_SelectionChanged;
            cmbLanguage.SelectionChanged += CmbLanguage_SelectionChanged;
            btnAddFavoriteTeam.Click += BtnAddFavoriteTeam_Click;
            //btnRemoveFavoriteTeam.Click += BtnRemoveFavoriteTeam_Click;
            //btnRemoveFavoritePlayer.Click += BtnRemoveFavoritePlayer_Click;
            //btnTeamInfo.Click += BtnTeamInfo_Click;


            // Instantiate the ContextMenu
            _favoritePlayerRemoveContextMenu = new ContextMenu();
            _favoritePlayerAddContextMenu = new ContextMenu();
            _favoriteTeamRemoveContextMenu = new ContextMenu();
            _favoriteTeamInfoContextMenu = new ContextMenu();


            // Menu item made via ContextMenu for removing favourite player
            removePlayer.Header = "Remove from Favorites"; // Set the text displayed in the menu
            removePlayer.Click += RemoveFromFavorites_Click; // Attach the event handler for when the item is clicked
            _favoritePlayerRemoveContextMenu.Items.Add(removePlayer);

            // Menu item made via ContextMenu for adding favourite player
            addPlayer.Header = "Add to Favorites"; // Set the text displayed in the menu
            addPlayer.Click += BtnAddToFavorites_Click; // Attach the event handler for when the item is clicked
            _favoritePlayerAddContextMenu.Items.Add(addPlayer);

            // Menu item made via ContextMenu for removing favourite player
            removeTeam.Header = "Remove from Favourites"; // Set the text displayed in the menu
            removeTeam.Click += BtnRemoveFavoriteTeam_Click; // Attach the event handler for when the item is clicked
            _favoriteTeamRemoveContextMenu.Items.Add(removeTeam);

            // Menu item made via ContextMenu for removing favourite player
            teamInfo.Header = "Info"; // Set the text displayed in the menu
            teamInfo.Click += BtnTeamInfo_Click; // Attach the event handler for when the item is clicked
            _favoriteTeamInfoContextMenu.Items.Add(teamInfo);



            btnHomeStat.Click += BtnLoadHomeStat_Click;
            btnAwayStat.Click += BtnLoadAwayStat_Click;


            this.Closing += MainWindow_Closing;
            this.Loaded += MainWindow_Loaded;

            canvasPlayers.SizeChanged += (s, e) => RedrawPlayers();

        }


        private void BtnLoadHomeStat_Click(object sender, RoutedEventArgs e)
        {
            if (statsHome == null)
            {
                MessageBox.Show("No info.");
                return;
            }

            try
            {
                var statWindow = new Statistic(statsHome);
                statWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void BtnLoadAwayStat_Click(object sender, RoutedEventArgs e)
        {
            if (statsAway == null)
            {
                MessageBox.Show("No info.");
                return;
            }

            try
            {
                var statWindow = new Statistic(statsAway);
                statWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }



        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadTeams();
            LoadFavoriteTeams();
            LoadFavoritePlayers();
        }

        private void ApplyLocalization()
        {
            this.Title = _localizationService["title"];
            lblGender.Content = _localizationService["gender"];
            lblLanguage.Content = _localizationService["language"];
            lblFavoriteTeam.Content = _localizationService["favoriteTeam"];

            btnLoadMatches.Content = _localizationService["loadMatches"];
            btnLoadPlayers.Content = _localizationService["loadPlayers"];
            btnAddFavoriteTeam.Content = _localizationService["addFavoriteTeam"];
            //btnRemoveFavoriteTeam.Content = _localizationService["remove"];
            //btnRemoveFavoritePlayer.Content = _localizationService["removeFavoritePlayer"];
            //btnAddToFavorites.Content = _localizationService["addToFavorites"];
            //btnTeamInfo.Content = _localizationService["teamInfo"];

            miTeamInfo.Header = _localizationService["teamInfo"];
            miRemoveTeam.Header = _localizationService["remove"];
            miAddPlayer.Header = _localizationService["addToFavorites"];
            miRemovePayer.Header = _localizationService["removeFavoritePlayer"];
            

            // Use the correct x:Name prefixes from your XAML (grp instead of gb)
            grpFavoriteTeams.Header = _localizationService["favoriteTeamsHeader"];
            grpFavoritePlayers.Header = _localizationService["favoritePlayersHeader"];
            grpMatches.Header = _localizationService["matchesHeader"];
            grpPlayers.Header = _localizationService["playersHeader"];
            grpPlayerLayout.Header = _localizationService["playerLayoutHeader"];
        }


        private void CmbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedGender = cmbGender.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedGender))
            {
                _configService.Settings.Gender = selectedGender;
                _configService.Save();
                lstPlayers.Items.Clear();
                _ = LoadTeams();
            }
        }

        private void CmbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedLanguage = cmbLanguage.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedLanguage))
            {
                _configService.Settings.Language = selectedLanguage;
                _configService.Save();
                _localizationService.LoadLanguage(selectedLanguage);
                ApplyLocalization();
            }
        }

        private async Task LoadTeams()
        {
            try
            {
                var gender = cmbGender.SelectedItem?.ToString() ?? "men";
                _teams = await _teamService.GetTeamsAsync(gender);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            cmbFavoriteTeam.Items.Clear();
            foreach (var team in _teams)
            {
                cmbFavoriteTeam.Items.Add($"{team.FifaCode} - {team.Country}");
            }

            // 🔧 Ensure something is selected by default
            if (cmbFavoriteTeam.Items.Count > 0 && cmbFavoriteTeam.SelectedIndex == -1)
                cmbFavoriteTeam.SelectedIndex = 0;
        }

        private void LoadFavoriteTeams()
        {
            lstFavouriteTeams.Items.Clear();
            var path = "./Data/favourite_teams.txt";
            if (File.Exists(path))
            {
                var teams = File.ReadAllLines(path);
                foreach (var t in teams)
                    lstFavouriteTeams.Items.Add(t);
            }
        }

        private void LoadFavoritePlayers()
        {
            _favoritePlayers = _settingsService.LoadFavoritePlayers();
            lstFavoritePlayers.Items.Clear();
            /*foreach (var player in _favoritePlayers)
            {
                lstFavoritePlayers.Items.Add(player.Name);
            }*/
            
            foreach (var player in _favoritePlayers)
            {
                // Format the player information as a string
                string playerInfo = $"{player.ShirtNumber} - {player.Name} ({player.Position})";
                if (player.Captain)
                {
                    playerInfo += " 🧢"; // Add captain emoji if applicable
                }
                lstFavoritePlayers.Items.Add(playerInfo);
            }

        }

        private async void BtnLoadMatches_Click(object sender, RoutedEventArgs e)
        {
            var gender = cmbGender.SelectedItem?.ToString() ?? "men";
            var selectedTeam = cmbFavoriteTeam.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedTeam)) return;

            var fifaCode = selectedTeam.Split('-')[0].Trim();
            try
            {
                _matches = await _matchService.GetMatchesForTeamAsync(gender, fifaCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            lstMatches.Items.Clear();
            foreach (var match in _matches)
            {
                lstMatches.Items.Add($"{match.StageName}: {match.HomeTeamCountry} vs {match.AwayTeamCountry}");
            }
        }

        private void BtnLoadPlayers_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = lstMatches.SelectedIndex;
            if (selectedIndex == -1) return;

            var selectedMatch = _matches[selectedIndex];
            statsHome = selectedMatch.HomeTeamStatistics;
            statsAway = selectedMatch.AwayTeamStatistics;
            var fifaCode = cmbFavoriteTeam.SelectedItem?.ToString().Split('-')[0].Trim();
            System.Diagnostics.Debug.WriteLine($"fifaCode ${fifaCode}");

            if (selectedMatch.AwayTeam.Code == fifaCode)
            {
                statsHome = selectedMatch.AwayTeamStatistics;
                statsAway = selectedMatch.HomeTeamStatistics;
            }

            if (statsHome == null)
            {
                MessageBox.Show("No statistics available.");
                return;
            }

            List<Player> _allPlayers = new();

            var homeTeam = JsonSerializer.Serialize(statsHome, new JsonSerializerOptions { WriteIndented = true });
            var awayTeam = JsonSerializer.Serialize(statsHome, new JsonSerializerOptions { WriteIndented = true });

            System.Diagnostics.Debug.WriteLine($"statsHome: {homeTeam}");
            System.Diagnostics.Debug.WriteLine($"statsAway: {awayTeam}");

            lblHomeTeam.Content = statsHome.Country;
            lblAwayTeam.Content = statsAway.Country;

            _allPlayersInMatch = statsHome.StartingEleven.Concat(statsHome.Substitutes).ToList();
            _currentHomePlayers = statsHome.StartingEleven.ToList();
            _currentAwayPlayers = statsAway.StartingEleven.ToList();

            lstPlayers.Items.Clear();
            // Iterate through all players (starting eleven + substitutes)
            foreach (var player in _allPlayersInMatch)
            {
                // do not add player to the main list since it is in the facourite list
                bool isFavorite = _favoritePlayers.Any(p => p.Name == player.Name);
                if (isFavorite) continue;

                // add player to the list of players
                string playerInfo = $"{player.ShirtNumber} - {player.Name} ({player.Position})";
                if (player.Captain)
                {
                    playerInfo += " 🧢"; // Add captain emoji if applicable
                }
                lstPlayers.Items.Add(playerInfo);
            }

            // Display on field
            DisplayPlayersOnField();
        }

        private void DisplayPlayersOnField()
        {
            RedrawPlayers();
        }

        private void RedrawPlayers()
        {
            canvasPlayers.Children.Clear();

            double fieldWidth = canvasPlayers.ActualWidth;
            double fieldHeight = canvasPlayers.ActualHeight;

            if (fieldWidth == 0 || fieldHeight == 0)
                return;

            List<Point> homeLayout = new()
            {
                new Point(0.0, 0.5), new Point(0.1, 0.2), new Point(0.1, 0.4),
                new Point(0.1, 0.6), new Point(0.1, 0.8), new Point(0.2, 0.25),
                new Point(0.2, 0.5), new Point(0.2, 0.75), new Point(0.30, 0.2),
                new Point(0.30, 0.5), new Point(0.30, 0.8)
            };

            List<Point> awayLayout = new()
            {
                new Point(0.9, 0.5), new Point(0.8, 0.2), new Point(0.8, 0.4),
                new Point(0.8, 0.6), new Point(0.8, 0.8), new Point(0.7, 0.25),
                new Point(0.7, 0.5), new Point(0.7, 0.75), new Point(0.6, 0.2),
                new Point(0.6, 0.5), new Point(0.6, 0.8)
            };

            void AddPlayers(List<Player> players, List<Point> layout)
            {
                for (int i = 0; i < players.Count && i < layout.Count; i++)
                {
                    var player = players[i];
                    string playerName = $"{player.Name}";
                    string playerShirt = $"{player.ShirtNumber}";
                    var control = new PlayerControl(playerName, playerShirt) { Opacity = 0 };

                    double x = layout[i].X * fieldWidth;
                    double y = layout[i].Y * fieldHeight;

                    Canvas.SetLeft(control, x);
                    Canvas.SetTop(control, y);
                    canvasPlayers.Children.Add(control);

                    var fadeIn = new DoubleAnimation
                    {
                        From = 0,
                        To = 1,
                        Duration = TimeSpan.FromSeconds(0.5),
                        BeginTime = TimeSpan.FromSeconds(i * 0.2)
                    };
                    Storyboard.SetTarget(fadeIn, control);
                    Storyboard.SetTargetProperty(fadeIn, new PropertyPath("Opacity"));

                    var sb = new Storyboard();
                    sb.Children.Add(fadeIn);
                    sb.Begin();
                }
            }

            AddPlayers(_currentHomePlayers, homeLayout);
            AddPlayers(_currentAwayPlayers, awayLayout);
        }





        private void BtnAddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = lstPlayers.SelectedIndex;
            if (selectedIndex == -1) return;

            var playerName = lstPlayers.SelectedItem.ToString();
            System.Diagnostics.Debug.WriteLine($"playerName: {playerName}");
            if (playerName == null) return;

            var player = _allPlayersInMatch.FirstOrDefault(p => playerName.Contains(p.Name));
            System.Diagnostics.Debug.WriteLine($"player: {player}");

            if (player == null || _favoritePlayers.Any(p => p.Name == player.Name))
            {
                MessageBox.Show("This player is already in the favorite list.");
                return;

            }

            _favoritePlayers.Add(player);
            _settingsService.SaveFavoritePlayers(_favoritePlayers);
            LoadFavoritePlayers();
            MessageBox.Show($"Added {player.Name} to favorites.");
        }

        private void BtnRemoveFavoritePlayer_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = lstFavoritePlayers.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Please select a player to remove.");
                return;
            }

            var playerName = lstFavoritePlayers.SelectedItem.ToString();
            var playerToRemove = _favoritePlayers.FirstOrDefault(p => p.Name == playerName);

            if (playerToRemove != null)
            {
                _favoritePlayers.Remove(playerToRemove);
                _settingsService.SaveFavoritePlayers(_favoritePlayers);
                LoadFavoritePlayers(); // Refresh UI
            }
        }


        private void BtnAddFavoriteTeam_Click(object sender, RoutedEventArgs e)
        {
            var selectedTeam = cmbFavoriteTeam.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedTeam)) return;

            if (!lstFavouriteTeams.Items.Contains(selectedTeam))
            {
                lstFavouriteTeams.Items.Add(selectedTeam);
                Directory.CreateDirectory("./Data");
                File.WriteAllLines("./Data/favourite_teams.txt", lstFavouriteTeams.Items.Cast<string>());
            }
            else
            {
                MessageBox.Show("This team is already in the favorite list.");
            }
        }

        private void BtnRemoveFavoriteTeam_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = lstFavouriteTeams.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("Please select a team to remove.");
                return;
            }

            lstFavouriteTeams.Items.RemoveAt(selectedIndex);

            // Save the updated list back to disk
            Directory.CreateDirectory("./Data");
            File.WriteAllLines("./Data/favourite_teams.txt", lstFavouriteTeams.Items.Cast<string>());
        }


        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }


        private async void BtnTeamInfo_Click(object sender, RoutedEventArgs e)
        {

            var selectedIndex = lstFavouriteTeams.SelectedIndex; // cmbFavoriteTeam.SelectedIndex;
            System.Diagnostics.Debug.WriteLine($"selectedIndex: {selectedIndex}");

            if (selectedIndex == -1)
            {
                MessageBox.Show("No team selected.");
                return;
            }

            var selected =  lstFavouriteTeams.SelectedItem.ToString();
            System.Diagnostics.Debug.WriteLine($"selected: {selected}");

            var fifaCode = selected.Split('-')[0].Trim();

            var team = _teams.FirstOrDefault(t => t.FifaCode == fifaCode);
            try
            {
                var results = await _teamService.GetTeamResultsAsync(cmbGender.SelectedItem?.ToString() ?? "men");
                var stats = results.FirstOrDefault(r => r.FifaCode == fifaCode);

                if (team != null && stats != null)
                {
                    var infoWindow = new TeamInfoWindow(team.Country, fifaCode, stats);
                    infoWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Could not load team info.");
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }


        private async void LoadTeamStat(object sender, RoutedEventArgs e)
        {

            var selectedIndex = lstFavouriteTeams.SelectedIndex; // cmbFavoriteTeam.SelectedIndex;
            System.Diagnostics.Debug.WriteLine($"selectedIndex: {selectedIndex}");

            if (selectedIndex == -1)
            {
                MessageBox.Show("No team selected.");
                return;
            }

            var selected = lstFavouriteTeams.SelectedItem.ToString();
            System.Diagnostics.Debug.WriteLine($"selected: {selected}");

            var fifaCode = selected.Split('-')[0].Trim();

            var team = _teams.FirstOrDefault(t => t.FifaCode == fifaCode);
            try
            {
                var results = await _teamService.GetTeamResultsAsync(cmbGender.SelectedItem?.ToString() ?? "men");
                var stats = results.FirstOrDefault(r => r.FifaCode == fifaCode);

                if (team != null && stats != null)
                {
                    var infoWindow = new TeamInfoWindow(team.Country, fifaCode, stats);
                    infoWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Could not load team info.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void RemoveFromFavorites_Click(object sender, RoutedEventArgs e)
        {
            // Find the MenuItem that was clicked
            MenuItem menuItem = sender as MenuItem;

            // Get the ContextMenu that owns this MenuItem
            ContextMenu contextMenu = menuItem.Parent as ContextMenu;

            // The PlacementTarget of the ContextMenu is the element it was displayed on.
            // If it was set on ListBoxItem (Option A), the PlacementTarget is the ListBoxItem.
            // If it was set on the ListBox (Option B), the PlacementTarget is the ListBox itself.
            if (contextMenu != null)
            {
                // For Option A (ItemContainerStyle binding):
                ListBoxItem clickedItem = contextMenu.PlacementTarget as ListBoxItem;
                if (clickedItem != null && clickedItem.Content is string playerName)
                {
                    RemovePlayerFromFavoritesLogic(playerName);
                }
                // For Option B (ContextMenu directly on ListBox), you'd need to find the selected item:
                else if (contextMenu.PlacementTarget is ListBox listBox && listBox.SelectedItem is string selectedPlayerName)
                {
                    RemovePlayerFromFavoritesLogic(selectedPlayerName);
                }
            }
        }

        // 🟢 Helper method to encapsulate the removal logic
        private void RemovePlayerFromFavoritesLogic(string fullPlayerStringFromListBox)
        {
            System.Diagnostics.Debug.WriteLine($"Attempting to remove player from list using: {fullPlayerStringFromListBox}");

            // Use RemoveAll with a predicate that checks if the fullPlayerStringFromListBox
            // contains the Name of any player in the _favoritePlayers list.
            // We'll iterate the _favoritePlayers list to find the actual Player object to remove.

            Player playerToRemove = null;

            // Find the player object in the _favoritePlayers list whose name is contained
            // within the fullPlayerStringFromListBox
            foreach (var favPlayer in _favoritePlayers)
            {
                // Case-insensitive comparison is often good for names
                if (fullPlayerStringFromListBox.Contains(favPlayer.Name, StringComparison.OrdinalIgnoreCase))
                {
                    playerToRemove = favPlayer;
                    break; // Found the player, no need to continue searching
                }
            }

            System.Diagnostics.Debug.WriteLine($"Player found to remove: {playerToRemove?.Name ?? "None"}");
            System.Diagnostics.Debug.WriteLine($"Before removal, favorite players count: {_favoritePlayers.Count}");

            if (playerToRemove != null)
            {
                _favoritePlayers.Remove(playerToRemove); // This removes the actual object instance
                System.Diagnostics.Debug.WriteLine($"After removal, favorite players count: {_favoritePlayers.Count}");

                _settingsService.SaveFavoritePlayers(_favoritePlayers);
                LoadFavoritePlayers(); // Refresh UI
                MessageBox.Show($"{playerToRemove.Name} removed from favorites.");
            }
            else
            {
                MessageBox.Show($"Player matching '{fullPlayerStringFromListBox}' not found in favorites.");
            }
        }



 

    }
}