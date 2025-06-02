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
            cmbGender = new ComboBox();
            cmbFavoriteTeam = new ComboBox();
            btnLoadMatches = new Button();
            btnLoadPlayers = new Button();
            lstMatches = new ListBox();
            lstPlayers = new ListBox();
            lblGender = new Label();
            Country = new Label();
            favPlayerAdd = new Button();
            lstFavouritePlayers = new ListBox();
            lblFavoritePlayer = new Label();
            lstFavouriteTeams = new ListBox();
            lblFavoriteTeam = new Label();
            favTeamAdd = new Button();
            btnRemoveFavoritePlayer = new Button();
            btnRemoveFavoriteTeam = new Button();
            cmbLanguage = new ComboBox();
            lblLanguage = new Label();
            SuspendLayout();
            // 
            // listOfMatches
            // 
            listOfMatches.AutoSize = true;
            listOfMatches.Location = new Point(79, 123);
            listOfMatches.Name = "listOfMatches";
            listOfMatches.Size = new Size(87, 15);
            listOfMatches.TabIndex = 6;
            listOfMatches.Text = "List of matches\n";
            // 
            // listOfPlayers
            // 
            listOfPlayers.AutoSize = true;
            listOfPlayers.Location = new Point(79, 270);
            listOfPlayers.Name = "listOfPlayers";
            listOfPlayers.Size = new Size(79, 15);
            listOfPlayers.TabIndex = 7;
            listOfPlayers.Text = "List of players\n";
            // 
            // cmbGender
            // 
            cmbGender.FormattingEnabled = true;
            cmbGender.Location = new Point(79, 33);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(121, 23);
            cmbGender.TabIndex = 8;
            cmbGender.SelectedIndexChanged += cmbGender_SelectedIndexChanged;
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
            btnLoadMatches.Location = new Point(451, 33);
            btnLoadMatches.Name = "btnLoadMatches";
            btnLoadMatches.Size = new Size(135, 23);
            btnLoadMatches.TabIndex = 10;
            btnLoadMatches.Text = "Load matches";
            btnLoadMatches.UseVisualStyleBackColor = true;
            btnLoadMatches.Click += btnLoadMatches_Click;
            // 
            // btnLoadPlayers
            // 
            btnLoadPlayers.Location = new Point(592, 33);
            btnLoadPlayers.Name = "btnLoadPlayers";
            btnLoadPlayers.Size = new Size(135, 23);
            btnLoadPlayers.TabIndex = 11;
            btnLoadPlayers.Text = "Load players";
            btnLoadPlayers.UseVisualStyleBackColor = true;
            btnLoadPlayers.Click += btnLoadPlayers_Click;
            // 
            // lstMatches
            // 
            lstMatches.FormattingEnabled = true;
            lstMatches.ItemHeight = 15;
            lstMatches.Location = new Point(79, 141);
            lstMatches.Name = "lstMatches";
            lstMatches.Size = new Size(259, 94);
            lstMatches.TabIndex = 12;
            lstMatches.SelectedIndexChanged += lstMatches_SelectedIndexChanged;
            // 
            // lstPlayers
            // 
            lstPlayers.FormattingEnabled = true;
            lstPlayers.ItemHeight = 15;
            lstPlayers.Location = new Point(79, 288);
            lstPlayers.Name = "lstPlayers";
            lstPlayers.Size = new Size(259, 94);
            lstPlayers.TabIndex = 13;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Location = new Point(79, 15);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(45, 15);
            lblGender.TabIndex = 14;
            lblGender.Text = "Gender";
            lblGender.Click += label3_Click;
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
            // favPlayerAdd
            // 
            favPlayerAdd.Location = new Point(79, 388);
            favPlayerAdd.Name = "favPlayerAdd";
            favPlayerAdd.Size = new Size(259, 23);
            favPlayerAdd.TabIndex = 16;
            favPlayerAdd.Text = "Add to Favourite Player Lists";
            favPlayerAdd.UseVisualStyleBackColor = true;
            favPlayerAdd.Click += favPlayerAdd_Click;
            // 
            // lstFavouritePlayers
            // 
            lstFavouritePlayers.FormattingEnabled = true;
            lstFavouritePlayers.ItemHeight = 15;
            lstFavouritePlayers.Location = new Point(397, 144);
            lstFavouritePlayers.Name = "lstFavouritePlayers";
            lstFavouritePlayers.Size = new Size(203, 94);
            lstFavouritePlayers.TabIndex = 17;
            // 
            // lblFavoritePlayer
            // 
            lblFavoritePlayer.AutoSize = true;
            lblFavoritePlayer.Location = new Point(396, 123);
            lblFavoritePlayer.Name = "lblFavoritePlayer";
            lblFavoritePlayer.Size = new Size(96, 15);
            lblFavoritePlayer.TabIndex = 18;
            lblFavoritePlayer.Text = "Favourite Players";
            // 
            // lstFavouriteTeams
            // 
            lstFavouriteTeams.FormattingEnabled = true;
            lstFavouriteTeams.ItemHeight = 15;
            lstFavouriteTeams.Location = new Point(396, 287);
            lstFavouriteTeams.Name = "lstFavouriteTeams";
            lstFavouriteTeams.Size = new Size(203, 94);
            lstFavouriteTeams.TabIndex = 19;
            // 
            // lblFavoriteTeam
            // 
            lblFavoriteTeam.AutoSize = true;
            lblFavoriteTeam.Location = new Point(397, 269);
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
            // btnRemoveFavoritePlayer
            // 
            btnRemoveFavoritePlayer.Location = new Point(398, 239);
            btnRemoveFavoritePlayer.Name = "btnRemoveFavoritePlayer";
            btnRemoveFavoritePlayer.Size = new Size(202, 23);
            btnRemoveFavoritePlayer.TabIndex = 22;
            btnRemoveFavoritePlayer.Text = "Remove player";
            btnRemoveFavoritePlayer.UseVisualStyleBackColor = true;
            btnRemoveFavoritePlayer.Click += btnRemoveFavoritePlayer_Click_Click;
            // 
            // btnRemoveFavoriteTeam
            // 
            btnRemoveFavoriteTeam.Location = new Point(397, 382);
            btnRemoveFavoriteTeam.Name = "btnRemoveFavoriteTeam";
            btnRemoveFavoriteTeam.Size = new Size(203, 23);
            btnRemoveFavoriteTeam.TabIndex = 23;
            btnRemoveFavoriteTeam.Text = "Remove team";
            btnRemoveFavoriteTeam.UseVisualStyleBackColor = true;
            btnRemoveFavoriteTeam.Click += btnRemoveFavoriteTeam_Click_Click;
            // 
            // cmbLanguage
            // 
            cmbLanguage.FormattingEnabled = true;
            cmbLanguage.Location = new Point(657, 382);
            cmbLanguage.Name = "cmbLanguage";
            cmbLanguage.Size = new Size(121, 23);
            cmbLanguage.TabIndex = 24;
            cmbLanguage.SelectedIndexChanged += cmbLanguage_SelectedIndexChanged;
            // 
            // lblLanguage
            // 
            lblLanguage.AutoSize = true;
            lblLanguage.Location = new Point(657, 359);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new Size(59, 15);
            lblLanguage.TabIndex = 25;
            lblLanguage.Text = "Language";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblLanguage);
            Controls.Add(cmbLanguage);
            Controls.Add(btnRemoveFavoriteTeam);
            Controls.Add(btnRemoveFavoritePlayer);
            Controls.Add(favTeamAdd);
            Controls.Add(lblFavoriteTeam);
            Controls.Add(lstFavouriteTeams);
            Controls.Add(lblFavoritePlayer);
            Controls.Add(lstFavouritePlayers);
            Controls.Add(favPlayerAdd);
            Controls.Add(Country);
            Controls.Add(lblGender);
            Controls.Add(lstPlayers);
            Controls.Add(lstMatches);
            Controls.Add(btnLoadPlayers);
            Controls.Add(btnLoadMatches);
            Controls.Add(cmbFavoriteTeam);
            Controls.Add(cmbGender);
            Controls.Add(listOfPlayers);
            Controls.Add(listOfMatches);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label listOfMatches;
        private Label listOfPlayers;
        private ComboBox cmbGender;
        private ComboBox cmbFavoriteTeam;
        private Button btnLoadMatches;
        private Button btnLoadPlayers;
        private ListBox lstMatches;
        private ListBox lstPlayers;
        private Label lblGender;
        private Label Country;
        private Button favPlayerAdd;
        private ListBox lstFavouritePlayers;
        private Label lblFavoritePlayer;
        private ListBox lstFavouriteTeams;
        private Label lblFavoriteTeam;
        private Button favTeamAdd;
        private Button btnRemoveFavoritePlayer;
        private Button btnRemoveFavoriteTeam;
        private ComboBox cmbLanguage;
        private Label lblLanguage;
    }
}
