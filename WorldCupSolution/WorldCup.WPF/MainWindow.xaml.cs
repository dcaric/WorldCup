using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorldCup.Data.Models;
using WorldCup.Data.Services;

namespace WorldCup.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ConfigService _configService;
        private TeamService _teamService;
        private MatchService _matchService;
        private List<Team> _teams;
        private List<Match> _matches;
        private SettingsService _settingsService;
        private List<Player> _favoritePlayers = new();

        public MainWindow()
        {
            InitializeComponent();

            // Initialize services
            _configService = new ConfigService();
            _teamService = new TeamService(_configService);
            _matchService = new MatchService(_configService);
            _settingsService = new SettingsService();

            // Event handlers
            btnLoadMatches.Click += BtnLoadMatches_Click;
            btnLoadPlayers.Click += BtnLoadPlayers_Click;
            btnAddToFavorites.Click += BtnAddToFavorites_Click;

            // Populate gender combo
            cmbGender.Items.Add("men");
            cmbGender.Items.Add("women");
            cmbGender.SelectedIndex = 0;

            // Load teams on startup
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadTeams();
            LoadFavoritePlayers();

        }

        private void LoadFavoritePlayers()
        {
            _favoritePlayers = _settingsService.LoadFavoritePlayers();
            lstFavoritePlayers.Items.Clear();
            foreach (var player in _favoritePlayers)
            {
                lstFavoritePlayers.Items.Add(player.Name);
            }
        }

        private void BtnAddToFavorites_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = lstPlayers.SelectedIndex;
            if (selectedIndex == -1) return;

            var selectedMatch = _matches[lstMatches.SelectedIndex];
            var player = selectedMatch.HomeTeamStatistics.StartingEleven[selectedIndex];

            if (_favoritePlayers.Any(p => p.Name == player.Name))
            {
                MessageBox.Show("Player already in favorites!");
                return;
            }

            _favoritePlayers.Add(player);
            _settingsService.SaveFavoritePlayers(_favoritePlayers);
            LoadFavoritePlayers();
            MessageBox.Show($"Added {player.Name} to favorites.");
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

        private async void BtnLoadMatches_Click(object sender, RoutedEventArgs e)
        {
            var gender = cmbGender.SelectedItem?.ToString() ?? "men";
            var selectedTeam = cmbFavoriteTeam.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedTeam)) return;

            var fifaCode = selectedTeam.Split('-')[0].Trim();
            _matches = await _matchService.GetMatchesForTeamAsync(gender, fifaCode);

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
            lstPlayers.Items.Clear();

            if (selectedMatch.HomeTeamStatistics == null)
            {
                MessageBox.Show("No statistics available for this match.");
                return;
            }

            foreach (var player in selectedMatch.HomeTeamStatistics.StartingEleven)
            {
                lstPlayers.Items.Add(player.Name);
            }
        }
    }
}