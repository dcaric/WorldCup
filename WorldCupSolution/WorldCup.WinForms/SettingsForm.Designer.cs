namespace WorldCup.WinForms
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbGender = new ComboBox();
            cmbLanguage = new ComboBox();
            lblGender = new Label();
            lblLanguage = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // cmbGender
            // 
            cmbGender.FormattingEnabled = true;
            cmbGender.Location = new Point(29, 12);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(121, 23);
            cmbGender.TabIndex = 0;
            cmbGender.SelectedIndexChanged += cmbGender_SelectedIndexChanged;
            // 
            // cmbLanguage
            // 
            cmbLanguage.FormattingEnabled = true;
            cmbLanguage.Location = new Point(173, 12);
            cmbLanguage.Name = "cmbLanguage";
            cmbLanguage.Size = new Size(121, 23);
            cmbLanguage.TabIndex = 1;
            cmbLanguage.SelectedIndexChanged += cmbLabguage_SelectedIndexChanged;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Location = new Point(28, 46);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(45, 15);
            lblGender.TabIndex = 2;
            lblGender.Text = "Gender";
            lblGender.TextAlign = ContentAlignment.BottomCenter;
            lblGender.Click += lblGender_Click;
            // 
            // lblLanguage
            // 
            lblLanguage.AutoSize = true;
            lblLanguage.Location = new Point(173, 46);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new Size(59, 15);
            lblLanguage.TabIndex = 3;
            lblLanguage.Text = "Language";
            lblLanguage.Click += lblLanguage_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(249, 127);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(169, 129);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(351, 156);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(lblLanguage);
            Controls.Add(lblGender);
            Controls.Add(cmbLanguage);
            Controls.Add(cmbGender);
            Name = "SettingsForm";
            Text = "SettingsForm";
            Load += SettingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbGender;
        private ComboBox cmbLanguage;
        private Label lblGender;
        private Label lblLanguage;
        private Button btnSave;
        private Button btnCancel;
    }
}