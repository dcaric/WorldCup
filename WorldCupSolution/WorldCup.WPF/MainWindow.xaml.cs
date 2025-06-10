using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using WorldCup.Data.Models;
using WorldCup.Data.Services;
using System.IO;
using System.Windows.Media.Animation; // Needed for DoubleAnimation


namespace WorldCup.WPF
{
    public partial class MainWindow : Window
    {
        private ConfigService _configService;
        private TeamService _teamService;
        private MatchService _matchService;
        private SettingsService _settingsService;
        private LocalizationService _localizationService;
        private ContextMenu _favoritePlayerContextMenu;

        private List<Team> _teams = new();
        private List<Match> _matches = new();
        private List<Player> _favoritePlayers = new();
        private List<Player> _allPlayersInMatch = new();

        public MainWindow()
        {
            InitializeComponent();

            this.Width = 1024;
            this.Height = 768;
            this.WindowState = WindowState.Normal;


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
            btnRemoveFavoriteTeam.Click += BtnRemoveFavoriteTeam_Click;
            //btnRemoveFavoritePlayer.Click += BtnRemoveFavoritePlayer_Click;
            btnTeamInfo.Click += BtnTeamInfo_Click;


            // Instantiate the ContextMenu
            _favoritePlayerContextMenu = new ContextMenu();

            // Create a MenuItem and add it to the ContextMenu's Items collection
            MenuItem removeItem = new MenuItem();
            removeItem.Header = "Remove from Favorites"; // Set the text displayed in the menu
            removeItem.Click += RemoveFromFavorites_Click; // Attach the event handler for when the item is clicked

            _favoritePlayerContextMenu.Items.Add(removeItem);

            this.Closing += MainWindow_Closing;
            this.Loaded += MainWindow_Loaded;
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
            btnRemoveFavoriteTeam.Content = _localizationService["remove"];
            //btnRemoveFavoritePlayer.Content = _localizationService["removeFavoritePlayer"];
            //btnAddToFavorites.Content = _localizationService["addToFavorites"];
            btnTeamInfo.Content = _localizationService["teamInfo"];

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
            var stats = selectedMatch.HomeTeamStatistics;
            var fifaCode = cmbFavoriteTeam.SelectedItem?.ToString().Split('-')[0].Trim();
            System.Diagnostics.Debug.WriteLine($"fifaCode ${fifaCode}");

            if (selectedMatch.AwayTeam.Code == fifaCode)
            {
                stats = selectedMatch.AwayTeamStatistics;
            }

            if (stats == null)
            {
                MessageBox.Show("No statistics available.");
                return;
            }

            _allPlayersInMatch = stats.StartingEleven.Concat(stats.Substitutes).ToList();

            lstPlayers.Items.Clear();
            // Iterate through all players (starting eleven + substitutes)
            foreach (var player in _allPlayersInMatch)
            {
                // Format the player information as a string
                string playerInfo = $"{player.ShirtNumber} - {player.Name} ({player.Position})";
                if (player.Captain)
                {
                    playerInfo += " 🧢"; // Add captain emoji if applicable
                }
                lstPlayers.Items.Add(playerInfo);
            }

            // Display on field
            DisplayPlayersOnField(_allPlayersInMatch);
        }

        private void BtnAddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = lstPlayers.SelectedIndex;
            if (selectedIndex == -1) return;

            var playerName = lstPlayers.SelectedItem.ToString();
            var player = _allPlayersInMatch.FirstOrDefault(p => p.Name == playerName);
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

            var selectedIndex = cmbFavoriteTeam.SelectedIndex;
            if (selectedIndex == -1)
            {
                MessageBox.Show("No team selected.");
                return;
            }

            var selected = cmbFavoriteTeam.Items[selectedIndex].ToString();
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



        private void DisplayPlayersOnField(List<Player> players)
        {
            canvasPlayers.Children.Clear();

            // Dummy layout
            var layout = new List<Point>
    {
        new Point(220, 20),  // Goalie
        new Point(50, 80), new Point(150, 80), new Point(250, 80), new Point(350, 80),
        new Point(50, 160), new Point(150, 160), new Point(250, 160), new Point(350, 160),
        new Point(150, 240), new Point(250, 240),
    };

            for (int i = 0; i < players.Count && i < layout.Count; i++)
            {
                var playerControl = new PlayerControl(players[i].Name)
                {
                    Opacity = 0 // Start invisible
                };

                Canvas.SetLeft(playerControl, layout[i].X);
                Canvas.SetTop(playerControl, layout[i].Y);
                canvasPlayers.Children.Add(playerControl);

                // Fade-in animation
                var fadeIn = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.5),
                    BeginTime = TimeSpan.FromSeconds(i * 0.5) // delay each by 0.5 seconds
                };

                // Apply animation to the control
                Storyboard.SetTarget(fadeIn, playerControl);
                Storyboard.SetTargetProperty(fadeIn, new PropertyPath("Opacity"));

                var storyboard = new Storyboard();
                storyboard.Children.Add(fadeIn);
                storyboard.Begin();
            }
        }




    }
}