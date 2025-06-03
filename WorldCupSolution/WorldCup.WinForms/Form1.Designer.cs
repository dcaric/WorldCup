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
            lstMatches = new ListBox();
            lblGender = new Label();
            Country = new Label();
            lblFavoritePlayer = new Label();
            lstFavouriteTeams = new ListBox();
            lblFavoriteTeam = new Label();
            favTeamAdd = new Button();
            btnRemoveFavoriteTeam = new Button();
            cmbLanguage = new ComboBox();
            lblLanguage = new Label();
            cmbTeamSide = new ComboBox();
            panelPlayers = new FlowLayoutPanel();
            panelFavoritePlayers = new FlowLayoutPanel();
            btnLoadPlayers = new Button();
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
            listOfPlayers.Location = new Point(16, 141);
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
            // lblFavoritePlayer
            // 
            lblFavoritePlayer.AutoSize = true;
            lblFavoritePlayer.Location = new Point(21, 317);
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
            // cmbLanguage
            // 
            cmbLanguage.FormattingEnabled = true;
            cmbLanguage.Location = new Point(817, 33);
            cmbLanguage.Name = "cmbLanguage";
            cmbLanguage.Size = new Size(59, 23);
            cmbLanguage.TabIndex = 24;
            cmbLanguage.SelectedIndexChanged += cmbLanguage_SelectedIndexChanged;
            // 
            // lblLanguage
            // 
            lblLanguage.AutoSize = true;
            lblLanguage.Location = new Point(817, 10);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new Size(59, 15);
            lblLanguage.TabIndex = 25;
            lblLanguage.Text = "Language";
            // 
            // cmbTeamSide
            // 
            cmbTeamSide.FormattingEnabled = true;
            cmbTeamSide.Location = new Point(157, 115);
            cmbTeamSide.Name = "cmbTeamSide";
            cmbTeamSide.Size = new Size(127, 23);
            cmbTeamSide.TabIndex = 26;
            cmbTeamSide.SelectedIndexChanged += cmbTeamSide_SelectedIndexChanged;
            // 
            // panelPlayers
            // 
            panelPlayers.AllowDrop = true;
            panelPlayers.AutoScroll = true;
            panelPlayers.BorderStyle = BorderStyle.FixedSingle;
            panelPlayers.FlowDirection = FlowDirection.TopDown;
            panelPlayers.Location = new Point(21, 159);
            panelPlayers.Name = "panelPlayers";
            panelPlayers.Size = new Size(263, 155);
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
            panelFavoritePlayers.Location = new Point(21, 340);
            panelFavoritePlayers.Name = "panelFavoritePlayers";
            panelFavoritePlayers.Size = new Size(263, 187);
            panelFavoritePlayers.TabIndex = 28;
            panelFavoritePlayers.WrapContents = false;
            // 
            // btnLoadPlayers
            // 
            btnLoadPlayers.Location = new Point(21, 115);
            btnLoadPlayers.Name = "btnLoadPlayers";
            btnLoadPlayers.Size = new Size(128, 23);
            btnLoadPlayers.TabIndex = 29;
            btnLoadPlayers.Text = "Load players";
            btnLoadPlayers.UseVisualStyleBackColor = true;
            btnLoadPlayers.Click += btnLoadPlayers_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(888, 539);
            Controls.Add(btnLoadPlayers);
            Controls.Add(panelFavoritePlayers);
            Controls.Add(panelPlayers);
            Controls.Add(cmbTeamSide);
            Controls.Add(lblLanguage);
            Controls.Add(cmbLanguage);
            Controls.Add(btnRemoveFavoriteTeam);
            Controls.Add(favTeamAdd);
            Controls.Add(lblFavoriteTeam);
            Controls.Add(lstFavouriteTeams);
            Controls.Add(lblFavoritePlayer);
            Controls.Add(Country);
            Controls.Add(lblGender);
            Controls.Add(lstMatches);
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
        private ListBox lstMatches;
        private Label lblGender;
        private Label Country;
        private Label lblFavoritePlayer;
        private ListBox lstFavouriteTeams;
        private Label lblFavoriteTeam;
        private Button favTeamAdd;
        private Button btnRemoveFavoriteTeam;
        private ComboBox cmbLanguage;
        private Label lblLanguage;
        private ComboBox cmbTeamSide;
        private FlowLayoutPanel panelPlayers;
        private FlowLayoutPanel panelFavoritePlayers;
        private Button btnLoadPlayers;
    }
}
