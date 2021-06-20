namespace EHR_ServiceTool_V3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.HardwareTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.USBCANRButton = new System.Windows.Forms.RadioButton();
            this.RS232RButton = new System.Windows.Forms.RadioButton();
            this.DevicePreferencesGroupBox = new System.Windows.Forms.GroupBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BaudrateComboBox = new System.Windows.Forms.ComboBox();
            this.DeviceLabel = new System.Windows.Forms.Label();
            this.DeviceSelectComboBox = new System.Windows.Forms.ComboBox();
            this.StartIDLabel = new System.Windows.Forms.Label();
            this.LanguageLabel = new System.Windows.Forms.Label();
            this.StartIDTextBox = new System.Windows.Forms.TextBox();
            this.LanguageComboBox = new System.Windows.Forms.ComboBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.SettingsCancelButton = new System.Windows.Forms.Button();
            this.HardwareTypeGroupBox.SuspendLayout();
            this.DevicePreferencesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // HardwareTypeGroupBox
            // 
            this.HardwareTypeGroupBox.Controls.Add(this.USBCANRButton);
            this.HardwareTypeGroupBox.Controls.Add(this.RS232RButton);
            resources.ApplyResources(this.HardwareTypeGroupBox, "HardwareTypeGroupBox");
            this.HardwareTypeGroupBox.Name = "HardwareTypeGroupBox";
            this.HardwareTypeGroupBox.TabStop = false;
            // 
            // USBCANRButton
            // 
            resources.ApplyResources(this.USBCANRButton, "USBCANRButton");
            this.USBCANRButton.Name = "USBCANRButton";
            this.USBCANRButton.TabStop = true;
            this.USBCANRButton.UseVisualStyleBackColor = true;
            this.USBCANRButton.CheckedChanged += new System.EventHandler(this.USBCANRButton_CheckedChanged);
            // 
            // RS232RButton
            // 
            resources.ApplyResources(this.RS232RButton, "RS232RButton");
            this.RS232RButton.Name = "RS232RButton";
            this.RS232RButton.TabStop = true;
            this.RS232RButton.UseVisualStyleBackColor = true;
            this.RS232RButton.CheckedChanged += new System.EventHandler(this.RS232RButton_CheckedChanged);
            // 
            // DevicePreferencesGroupBox
            // 
            this.DevicePreferencesGroupBox.Controls.Add(this.RefreshButton);
            this.DevicePreferencesGroupBox.Controls.Add(this.label1);
            this.DevicePreferencesGroupBox.Controls.Add(this.BaudrateComboBox);
            this.DevicePreferencesGroupBox.Controls.Add(this.DeviceLabel);
            this.DevicePreferencesGroupBox.Controls.Add(this.DeviceSelectComboBox);
            resources.ApplyResources(this.DevicePreferencesGroupBox, "DevicePreferencesGroupBox");
            this.DevicePreferencesGroupBox.Name = "DevicePreferencesGroupBox";
            this.DevicePreferencesGroupBox.TabStop = false;
            // 
            // RefreshButton
            // 
            resources.ApplyResources(this.RefreshButton, "RefreshButton");
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // BaudrateComboBox
            // 
            this.BaudrateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BaudrateComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.BaudrateComboBox, "BaudrateComboBox");
            this.BaudrateComboBox.Name = "BaudrateComboBox";
            // 
            // DeviceLabel
            // 
            resources.ApplyResources(this.DeviceLabel, "DeviceLabel");
            this.DeviceLabel.Name = "DeviceLabel";
            // 
            // DeviceSelectComboBox
            // 
            this.DeviceSelectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeviceSelectComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.DeviceSelectComboBox, "DeviceSelectComboBox");
            this.DeviceSelectComboBox.Name = "DeviceSelectComboBox";
            // 
            // StartIDLabel
            // 
            resources.ApplyResources(this.StartIDLabel, "StartIDLabel");
            this.StartIDLabel.Name = "StartIDLabel";
            // 
            // LanguageLabel
            // 
            resources.ApplyResources(this.LanguageLabel, "LanguageLabel");
            this.LanguageLabel.Name = "LanguageLabel";
            // 
            // StartIDTextBox
            // 
            resources.ApplyResources(this.StartIDTextBox, "StartIDTextBox");
            this.StartIDTextBox.Name = "StartIDTextBox";
            this.StartIDTextBox.TextChanged += new System.EventHandler(this.StartIDTextBox_TextChanged);
            // 
            // LanguageComboBox
            // 
            this.LanguageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanguageComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.LanguageComboBox, "LanguageComboBox");
            this.LanguageComboBox.Name = "LanguageComboBox";
            this.LanguageComboBox.SelectedIndexChanged += new System.EventHandler(this.LanguageComboBox_SelectedIndexChanged);
            // 
            // OKButton
            // 
            resources.ApplyResources(this.OKButton, "OKButton");
            this.OKButton.Name = "OKButton";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // SettingsCancelButton
            // 
            resources.ApplyResources(this.SettingsCancelButton, "SettingsCancelButton");
            this.SettingsCancelButton.Name = "SettingsCancelButton";
            this.SettingsCancelButton.UseVisualStyleBackColor = true;
            this.SettingsCancelButton.Click += new System.EventHandler(this.SettingsCancelButton_Click);
            // 
            // SettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SettingsCancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.LanguageComboBox);
            this.Controls.Add(this.StartIDTextBox);
            this.Controls.Add(this.LanguageLabel);
            this.Controls.Add(this.StartIDLabel);
            this.Controls.Add(this.DevicePreferencesGroupBox);
            this.Controls.Add(this.HardwareTypeGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.HardwareTypeGroupBox.ResumeLayout(false);
            this.HardwareTypeGroupBox.PerformLayout();
            this.DevicePreferencesGroupBox.ResumeLayout(false);
            this.DevicePreferencesGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.RadioButton USBCANRButton;
        public System.Windows.Forms.RadioButton RS232RButton;
        public System.Windows.Forms.ComboBox BaudrateComboBox;
        public System.Windows.Forms.ComboBox DeviceSelectComboBox;
        public System.Windows.Forms.TextBox StartIDTextBox;
        public System.Windows.Forms.ComboBox LanguageComboBox;
        public System.Windows.Forms.Button OKButton;
        public System.Windows.Forms.Button SettingsCancelButton;
        public System.Windows.Forms.Button RefreshButton;
        public System.Windows.Forms.Label DeviceLabel;
        public System.Windows.Forms.GroupBox HardwareTypeGroupBox;
        public System.Windows.Forms.GroupBox DevicePreferencesGroupBox;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label StartIDLabel;
        public System.Windows.Forms.Label LanguageLabel;
    }
}