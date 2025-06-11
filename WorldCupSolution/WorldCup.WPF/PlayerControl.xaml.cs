using System.Windows;
using System.Windows.Controls;

namespace WorldCup.WPF
{
    public partial class PlayerControl : UserControl
    {
        public PlayerControl(string playerName, string playerShirt)
        {
            InitializeComponent();
            txtPlayerName.Text = playerName;
            txtPlayerShirt.Text = playerShirt;
        }
    }
}
