namespace EHR_ServiceTool_V3
{
    partial class License
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
            this.LicenseKeyTextBox = new System.Windows.Forms.TextBox();
            this.LicenseKeyLabel = new System.Windows.Forms.Label();
            this.MachineIDLabel = new System.Windows.Forms.Label();
            this.MachineIDTextBox = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LicenseKeyTextBox
            // 
            this.LicenseKeyTextBox.Location = new System.Drawing.Point(102, 62);
            this.LicenseKeyTextBox.Name = "LicenseKeyTextBox";
            this.LicenseKeyTextBox.Size = new System.Drawing.Size(200, 20);
            this.LicenseKeyTextBox.TabIndex = 7;
            // 
            // LicenseKeyLabel
            // 
            this.LicenseKeyLabel.AutoSize = true;
            this.LicenseKeyLabel.Location = new System.Drawing.Point(12, 65);
            this.LicenseKeyLabel.Name = "LicenseKeyLabel";
            this.LicenseKeyLabel.Size = new System.Drawing.Size(74, 13);
            this.LicenseKeyLabel.TabIndex = 6;
            this.LicenseKeyLabel.Text = "License Key  :";
            // 
            // MachineIDLabel
            // 
            this.MachineIDLabel.AutoSize = true;
            this.MachineIDLabel.Location = new System.Drawing.Point(12, 29);
            this.MachineIDLabel.Name = "MachineIDLabel";
            this.MachineIDLabel.Size = new System.Drawing.Size(74, 13);
            this.MachineIDLabel.TabIndex = 5;
            this.MachineIDLabel.Text = "Machine ID   :";
            // 
            // MachineIDTextBox
            // 
            this.MachineIDTextBox.BackColor = System.Drawing.Color.White;
            this.MachineIDTextBox.Location = new System.Drawing.Point(102, 26);
            this.MachineIDTextBox.Name = "MachineIDTextBox";
            this.MachineIDTextBox.ReadOnly = true;
            this.MachineIDTextBox.Size = new System.Drawing.Size(200, 20);
            this.MachineIDTextBox.TabIndex = 4;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(119, 100);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 9;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // License
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(320, 141);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.LicenseKeyTextBox);
            this.Controls.Add(this.LicenseKeyLabel);
            this.Controls.Add(this.MachineIDLabel);
            this.Controls.Add(this.MachineIDTextBox);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MaximumSize = new System.Drawing.Size(336, 180);
            this.MinimumSize = new System.Drawing.Size(336, 180);
            this.Name = "License";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter License Key";
            this.Load += new System.EventHandler(this.License_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LicenseKeyTextBox;
        private System.Windows.Forms.Label LicenseKeyLabel;
        private System.Windows.Forms.Label MachineIDLabel;
        private System.Windows.Forms.TextBox MachineIDTextBox;
        private System.Windows.Forms.Button OKButton;
    }
}