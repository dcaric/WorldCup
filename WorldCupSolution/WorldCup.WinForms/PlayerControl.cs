using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCup.Data.Models;

namespace WorldCup.WinForms
{
    public partial class PlayerControl : UserControl
    {
        public Player PlayerData { get; private set; }
        public bool IsFavorite { get; private set; }

        private bool _isDragging = false;
        private Point _dragStartPoint;

        public PlayerControl(Player player, bool isFavorite)
        {
            System.Diagnostics.Debug.WriteLine($"player name  {player.Name}");

            InitializeComponent();
            PlayerData = player;
            IsFavorite = isFavorite;
            lblInfo.Text = $"{player.ShirtNumber} - {player.Name} ({player.Position})" +
            (player.Captain ? " 🧢" : "");

            // try to find player image
            string fileName = player.Name.ToLower().Replace(" ", "_") + ".jpg";
            string fullPath = Path.Combine("Assets", fileName);
            string fallbackPath = Path.Combine("Assets", "personDefault.jpg");

            if (File.Exists(fullPath))
            {
                using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                {
                    picStar.Image = new Bitmap(Image.FromStream(stream));
                }
            }
            else if (File.Exists(fallbackPath))
            {
                using (var stream = new FileStream(fallbackPath, FileMode.Open, FileAccess.Read))
                {
                    picStar.Image = new Bitmap(Image.FromStream(stream));
                }
            }
            else
            {
                picStar.Image = null; // Optional: fallback behavior if nothing found
            }



            this.MouseDown += PlayerControl_MouseDown;
            this.MouseMove += PlayerControl_MouseMove;
        }

        private void PlayerControl_MouseDown(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"DOWN e {e}");
            _isDragging = true;
            _dragStartPoint = e.Location;
        }

        private void PlayerControl_MouseMove(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"MOVE e {e}");

            if (_isDragging && e.Button == MouseButtons.Left)
            {
                _isDragging = false;
                DoDragDrop(this, DragDropEffects.Move);
            }
        }

        public void SetFavorite(bool favorite)
        {
            IsFavorite = favorite;
            picStar.Visible = favorite;
        }


        /*public void SetFavorite(bool favorite)
        {
            IsFavorite = favorite;
            picStar.Visible = favorite;
        }*/

        private void PlayerControl_Load(object sender, EventArgs e)
        {

        }

        private void picStar_Click(object sender, EventArgs e)
        {

        }
    }
}
