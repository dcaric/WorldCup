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
            InitializeComponent();
            PlayerData = player;
            IsFavorite = isFavorite;
            lblInfo.Text = $"{player.ShirtNumber} - {player.Name} ({player.Position})" +
            (player.Captain ? " 🧢" : "");
            picStar.Visible = isFavorite;

            this.MouseDown += PlayerControl_MouseDown;
            this.MouseMove += PlayerControl_MouseMove;
        }

        private void PlayerControl_MouseDown(object sender, MouseEventArgs e)
        {
            _isDragging = true;
            _dragStartPoint = e.Location;
        }

        private void PlayerControl_MouseMove(object sender, MouseEventArgs e)
        {
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
    }
}
