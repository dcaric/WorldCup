using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorldCup.Data.Models;

namespace WorldCup.WPF
{
    /// <summary>
    /// Interaction logic for TeamInfoWindow.xaml
    /// </summary>
    public partial class TeamInfoWindow : Window
    {
        public TeamInfoWindow(string country, string fifaCode, TeamResult stats)
        {
            InitializeComponent();

            txtTeamName.Text = $"Team: {country}";
            txtFifaCode.Text = $"FIFA Code: {fifaCode}";
            txtWins.Text = $"Wins: {stats.Wins}";
            txtLosses.Text = $"Losses: {stats.Losses}";
            txtGoals.Text = $"Goals: {stats.GoalsFor} / Against: {stats.GoalsAgainst}";


        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
