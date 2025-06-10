namespace WorldCup.WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listOfMatches = new Label();
            listOfPlayers = new Label();
            cmbFavoriteTeam = new ComboBox();
            btnLoadMatches = new Button();
            lstMatches = new ListBox();
            Country = new Label();
            lblFavoritePlayer = new Label();
            lstFavouriteTeams = new ListBox();
            lblFavoriteTeam = new Label();
            favTeamAdd = new Button();
            btnRemoveFavoriteTeam = new Button();
            panelPlayers = new FlowLayoutPanel();
            panelFavoritePlayers = new FlowLayoutPanel();
            btnLoadPlayers = new Button();
            btnSettings = new Button();
            SuspendLayout();
            // 
            // listOfMatches
            // 
            listOfMatches.AutoSize = true;
            listOfMatches.Location = new Point(487, 154);
            listOfMatches.Name = "listOfMatches";
            listOfMatches.Size = new Size(87, 15);
            listOfMatches.TabIndex = 6;
            listOfMatches.Text = "List of matches\n";
            // 
            // listOfPlayers
            // 
            listOfPlayers.AutoSize = true;
            listOfPlayers.Location = new Point(21, 283);
            listOfPlayers.Name = "listOfPlayers";
            listOfPlayers.Size = new Size(79, 15);
            listOfPlayers.TabIndex = 7;
            listOfPlayers.Text = "List of players\n";
            // 
            // cmbFavoriteTeam
            // 
            cmbFavoriteTeam.FormattingEnabled = true;
            cmbFavoriteTeam.Location = new Point(261, 33);
            cmbFavoriteTeam.Name = "cmbFavoriteTeam";
            cmbFavoriteTeam.Size = new Size(170, 23);
            cmbFavoriteTeam.TabIndex = 9;
            cmbFavoriteTeam.SelectedIndexChanged += cmbFavoriteTeam_SelectedIndexChanged;
            // 
            // btnLoadMatches
            // 
            btnLoadMatches.Location = new Point(487, 33);
            btnLoadMatches.Name = "btnLoadMatches";
            btnLoadMatches.Size = new Size(259, 23);
            btnLoadMatches.TabIndex = 10;
            btnLoadMatches.Text = "Load matches";
            btnLoadMatches.UseVisualStyleBackColor = true;
            btnLoadMatches.Click += btnLoadMatches_Click;
            // 
            // lstMatches
            // 
            lstMatches.FormattingEnabled = true;
            lstMatches.ItemHeight = 15;
            lstMatches.Location = new Point(487, 57);
            lstMatches.Name = "lstMatches";
            lstMatches.Size = new Size(259, 94);
            lstMatches.TabIndex = 12;
            lstMatches.SelectedIndexChanged += lstMatches_SelectedIndexChanged;
            // 
            // Country
            // 
            Country.AutoSize = true;
            Country.Location = new Point(262, 13);
            Country.Name = "Country";
            Country.Size = new Size(50, 15);
            Country.TabIndex = 15;
            Country.Text = "Country";
            // 
            // lblFavoritePlayer
            // 
            lblFavoritePlayer.AutoSize = true;
            lblFavoritePlayer.Location = new Point(21, 507);
            lblFavoritePlayer.Name = "lblFavoritePlayer";
            lblFavoritePlayer.Size = new Size(96, 15);
            lblFavoritePlayer.TabIndex = 18;
            lblFavoritePlayer.Text = "Favourite Players";
            // 
            // lstFavouriteTeams
            // 
            lstFavouriteTeams.FormattingEnabled = true;
            lstFavouriteTeams.ItemHeight = 15;
            lstFavouriteTeams.Location = new Point(487, 220);
            lstFavouriteTeams.Name = "lstFavouriteTeams";
            lstFavouriteTeams.Size = new Size(259, 94);
            lstFavouriteTeams.TabIndex = 19;
            lstFavouriteTeams.SelectedIndexChanged += lstFavouriteTeams_SelectedIndexChanged;
            // 
            // lblFavoriteTeam
            // 
            lblFavoriteTeam.AutoSize = true;
            lblFavoriteTeam.Location = new Point(487, 317);
            lblFavoriteTeam.Name = "lblFavoriteTeam";
            lblFavoriteTeam.Size = new Size(88, 15);
            lblFavoriteTeam.TabIndex = 20;
            lblFavoriteTeam.Text = "Favourite Team";
            // 
            // favTeamAdd
            // 
            favTeamAdd.Location = new Point(261, 64);
            favTeamAdd.Name = "favTeamAdd";
            favTeamAdd.Size = new Size(170, 23);
            favTeamAdd.TabIndex = 21;
            favTeamAdd.Text = "Add to Favourite Team List";
            favTeamAdd.UseVisualStyleBackColor = true;
            favTeamAdd.Click += favTeamAdd_Click;
            // 
            // btnRemoveFavoriteTeam
            // 
            btnRemoveFavoriteTeam.Location = new Point(487, 191);
            btnRemoveFavoriteTeam.Name = "btnRemoveFavoriteTeam";
            btnRemoveFavoriteTeam.Size = new Size(259, 23);
            btnRemoveFavoriteTeam.TabIndex = 23;
            btnRemoveFavoriteTeam.Text = "Remove team";
            btnRemoveFavoriteTeam.UseVisualStyleBackColor = true;
            btnRemoveFavoriteTeam.Click += btnRemoveFavoriteTeam_Click_Click;
            // 
            // panelPlayers
            // 
            panelPlayers.AllowDrop = true;
            panelPlayers.AutoScroll = true;
            panelPlayers.BorderStyle = BorderStyle.FixedSingle;
            panelPlayers.FlowDirection = FlowDirection.TopDown;
            panelPlayers.Location = new Point(21, 125);
            panelPlayers.Name = "panelPlayers";
            panelPlayers.Size = new Size(410, 155);
            panelPlayers.TabIndex = 27;
            panelPlayers.WrapContents = false;
            panelPlayers.Paint += panelPlayers_Paint_1;
            // 
            // panelFavoritePlayers
            // 
            panelFavoritePlayers.AllowDrop = true;
            panelFavoritePlayers.AutoScroll = true;
            panelFavoritePlayers.BorderStyle = BorderStyle.FixedSingle;
            panelFavoritePlayers.FlowDirection = FlowDirection.TopDown;
            panelFavoritePlayers.Location = new Point(21, 317);
            panelFavoritePlayers.Name = "panelFavoritePlayers";
            panelFavoritePlayers.Size = new Size(410, 187);
            panelFavoritePlayers.TabIndex = 28;
            panelFavoritePlayers.WrapContents = false;
            // 
            // btnLoadPlayers
            // 
            btnLoadPlayers.Location = new Point(21, 96);
            btnLoadPlayers.Name = "btnLoadPlayers";
            btnLoadPlayers.Size = new Size(128, 23);
            btnLoadPlayers.TabIndex = 29;
            btnLoadPlayers.Text = "Load players";
            btnLoadPlayers.UseVisualStyleBackColor = true;
            btnLoadPlayers.Click += btnLoadPlayers_Click;
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(784, 500);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(75, 23);
            btnSettings.TabIndex = 30;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(888, 539);
            Controls.Add(btnSettings);
            Controls.Add(btnLoadPlayers);
            Controls.Add(panelFavoritePlayers);
            Controls.Add(panelPlayers);
            Controls.Add(btnRemoveFavoriteTeam);
            Controls.Add(favTeamAdd);
            Controls.Add(lblFavoriteTeam);
            Controls.Add(lstFavouriteTeams);
            Controls.Add(lblFavoritePlayer);
            Controls.Add(Country);
            Controls.Add(lstMatches);
            Controls.Add(btnLoadMatches);
            Controls.Add(cmbFavoriteTeam);
            Controls.Add(listOfPlayers);
            Controls.Add(listOfMatches);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label listOfMatches;
        private Label listOfPlayers;
        private ComboBox cmbFavoriteTeam;
        private Button btnLoadMatches;
        private ListBox lstMatches;
        private Label Country;
        private Label lblFavoritePlayer;
        private ListBox lstFavouriteTeams;
        private Label lblFavoriteTeam;
        private Button favTeamAdd;
        private Button btnRemoveFavoriteTeam;
        private FlowLayoutPanel panelPlayers;
        private FlowLayoutPanel panelFavoritePlayers;
        private Button btnLoadPlayers;
        private Button btnSettings;
    }
}
