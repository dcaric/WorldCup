namespace WorldCup.WinForms
{
    partial class PlayerControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Label lblInfo;
        private PictureBox picStar;
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        private void InitializeComponent()
        {
            lblInfo = new Label();
            picStar = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picStar).BeginInit();
            SuspendLayout();
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(10, 10);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(0, 15);
            lblInfo.TabIndex = 0;
            // 
            // picStar
            // 
            picStar.Location = new Point(361, 9);
            picStar.Name = "picStar";
            picStar.Size = new Size(16, 16);
            picStar.SizeMode = PictureBoxSizeMode.StretchImage;
            picStar.TabIndex = 1;
            picStar.TabStop = false;
            // 
            // PlayerControl
            // 
            BackColor = SystemColors.ControlLight;
            Controls.Add(lblInfo);
            Controls.Add(picStar);
            Name = "PlayerControl";
            Size = new Size(380, 30);
            Load += PlayerControl_Load;
            ((System.ComponentModel.ISupportInitialize)picStar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
    

        #endregion
    }
}
