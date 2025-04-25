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
            label1 = new Label();
            label2 = new Label();
            cmbGender = new ComboBox();
            cmbFavoriteTeam = new ComboBox();
            btnLoadMatches = new Button();
            btnLoadPlayers = new Button();
            lstMatches = new ListBox();
            lstPlayers = new ListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(79, 155);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 6;
            label1.Text = "List of matches\n";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(79, 270);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 7;
            label2.Text = "List of players\n";
            // 
            // cmbGender
            // 
            cmbGender.FormattingEnabled = true;
            cmbGender.Location = new Point(72, 23);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(121, 23);
            cmbGender.TabIndex = 8;
            cmbGender.SelectedIndexChanged += cmbGender_SelectedIndexChanged;
            // 
            // cmbFavoriteTeam
            // 
            cmbFavoriteTeam.FormattingEnabled = true;
            cmbFavoriteTeam.Location = new Point(261, 25);
            cmbFavoriteTeam.Name = "cmbFavoriteTeam";
            cmbFavoriteTeam.Size = new Size(121, 23);
            cmbFavoriteTeam.TabIndex = 9;
            cmbFavoriteTeam.SelectedIndexChanged += cmbFavoriteTeam_SelectedIndexChanged;
            // 
            // btnLoadMatches
            // 
            btnLoadMatches.Location = new Point(499, 32);
            btnLoadMatches.Name = "btnLoadMatches";
            btnLoadMatches.Size = new Size(135, 23);
            btnLoadMatches.TabIndex = 10;
            btnLoadMatches.Text = "Load matches";
            btnLoadMatches.UseVisualStyleBackColor = true;
            btnLoadMatches.Click += btnLoadMatches_Click;
            // 
            // btnLoadPlayers
            // 
            btnLoadPlayers.Location = new Point(499, 112);
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
            lstMatches.Location = new Point(79, 173);
            lstMatches.Name = "lstMatches";
            lstMatches.Size = new Size(259, 94);
            lstMatches.TabIndex = 12;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lstPlayers);
            Controls.Add(lstMatches);
            Controls.Add(btnLoadPlayers);
            Controls.Add(btnLoadMatches);
            Controls.Add(cmbFavoriteTeam);
            Controls.Add(cmbGender);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private ComboBox cmbGender;
        private ComboBox cmbFavoriteTeam;
        private Button btnLoadMatches;
        private Button btnLoadPlayers;
        private ListBox lstMatches;
        private ListBox lstPlayers;
    }
}
