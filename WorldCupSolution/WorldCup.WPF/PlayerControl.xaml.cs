using System.Windows;
using System.Windows.Controls;

namespace WorldCup.WPF
{
    public partial class PlayerControl : UserControl
    {
        public PlayerControl()
        {
            InitializeComponent();
        }

        public string PlayerName
        {
            get => txtName.Text;
            set => txtName.Text = value;
        }

        public string PlayerNumber
        {
            get => txtNumber.Text;
            set => txtNumber.Text = value;
        }
    }
}
