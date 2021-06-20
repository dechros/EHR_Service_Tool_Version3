namespace EHR_ServiceTool_V3
{
    partial class MainForm
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDataFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDataFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openConfigFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHwRefresh = new System.Windows.Forms.Button();
            this.CPRefreshButton = new System.Windows.Forms.Button();
            this.cbbBaudrates = new System.Windows.Forms.ComboBox();
            this.BaudrateComboBox = new System.Windows.Forms.ComboBox();
            this.cbbChannel = new System.Windows.Forms.ComboBox();
            this.ComportNamesComboBox = new System.Windows.Forms.ComboBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.ReceiveDataTimer = new System.Windows.Forms.Timer(this.components);
            this.SettingsControlTimer = new System.Windows.Forms.Timer(this.components);
            this.SerialPortMain = new System.IO.Ports.SerialPort(this.components);
            this.InformationLabel = new System.Windows.Forms.Label();
            this.ParamSendTimer = new System.Windows.Forms.Timer(this.components);
            this.BottomGroupBox = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.BottomGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.parametersToolStripMenuItem,
            this.variablesToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(784, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDataFileToolStripMenuItem,
            this.saveDataFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.openConfigFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openDataFileToolStripMenuItem
            // 
            this.openDataFileToolStripMenuItem.Name = "openDataFileToolStripMenuItem";
            this.openDataFileToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.openDataFileToolStripMenuItem.Text = "Open Data File";
            this.openDataFileToolStripMenuItem.Click += new System.EventHandler(this.openDataFileToolStripMenuItem_Click);
            // 
            // saveDataFileToolStripMenuItem
            // 
            this.saveDataFileToolStripMenuItem.Name = "saveDataFileToolStripMenuItem";
            this.saveDataFileToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.saveDataFileToolStripMenuItem.Text = "Save Data File";
            this.saveDataFileToolStripMenuItem.Click += new System.EventHandler(this.saveDataFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(200, 6);
            // 
            // openConfigFileToolStripMenuItem
            // 
            this.openConfigFileToolStripMenuItem.Name = "openConfigFileToolStripMenuItem";
            this.openConfigFileToolStripMenuItem.Size = new System.Drawing.Size(203, 26);
            this.openConfigFileToolStripMenuItem.Text = "Open Config File";
            this.openConfigFileToolStripMenuItem.Click += new System.EventHandler(this.openConfigFileToolStripMenuItem_Click);
            // 
            // parametersToolStripMenuItem
            // 
            this.parametersToolStripMenuItem.Name = "parametersToolStripMenuItem";
            this.parametersToolStripMenuItem.Size = new System.Drawing.Size(96, 24);
            this.parametersToolStripMenuItem.Text = "Parameters";
            this.parametersToolStripMenuItem.Click += new System.EventHandler(this.parametersToolStripMenuItem_Click);
            // 
            // variablesToolStripMenuItem
            // 
            this.variablesToolStripMenuItem.Name = "variablesToolStripMenuItem";
            this.variablesToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.variablesToolStripMenuItem.Text = "Variables";
            this.variablesToolStripMenuItem.Click += new System.EventHandler(this.variablesToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(76, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // btnHwRefresh
            // 
            this.btnHwRefresh.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnHwRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHwRefresh.Location = new System.Drawing.Point(227, 632);
            this.btnHwRefresh.Name = "btnHwRefresh";
            this.btnHwRefresh.Size = new System.Drawing.Size(57, 23);
            this.btnHwRefresh.TabIndex = 51;
            this.btnHwRefresh.Text = "Refresh";
            this.btnHwRefresh.Visible = false;
            this.btnHwRefresh.Click += new System.EventHandler(this.btnHwRefresh_Click);
            // 
            // CPRefreshButton
            // 
            this.CPRefreshButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.CPRefreshButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CPRefreshButton.Location = new System.Drawing.Point(303, 632);
            this.CPRefreshButton.Name = "CPRefreshButton";
            this.CPRefreshButton.Size = new System.Drawing.Size(57, 23);
            this.CPRefreshButton.TabIndex = 52;
            this.CPRefreshButton.Text = "Refresh";
            this.CPRefreshButton.Visible = false;
            this.CPRefreshButton.Click += new System.EventHandler(this.CPRefreshButton_Click);
            // 
            // cbbBaudrates
            // 
            this.cbbBaudrates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbBaudrates.Items.AddRange(new object[] {
            "1 MBit/sec",
            "800 kBit/s",
            "500 kBit/sec",
            "250 kBit/sec",
            "125 kBit/sec",
            "100 kBit/sec",
            "95,238 kBit/s",
            "83,333 kBit/s",
            "50 kBit/sec",
            "47,619 kBit/s",
            "33,333 kBit/s",
            "20 kBit/sec",
            "10 kBit/sec",
            "5 kBit/sec"});
            this.cbbBaudrates.Location = new System.Drawing.Point(790, 636);
            this.cbbBaudrates.Name = "cbbBaudrates";
            this.cbbBaudrates.Size = new System.Drawing.Size(116, 21);
            this.cbbBaudrates.TabIndex = 50;
            this.cbbBaudrates.Visible = false;
            this.cbbBaudrates.SelectedIndexChanged += new System.EventHandler(this.cbbBaudrates_SelectedIndexChanged);
            // 
            // BaudrateComboBox
            // 
            this.BaudrateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BaudrateComboBox.Items.AddRange(new object[] {
            "1200",
            "1800",
            "2400",
            "4800",
            "7200",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200",
            "128000"});
            this.BaudrateComboBox.Location = new System.Drawing.Point(521, 635);
            this.BaudrateComboBox.Name = "BaudrateComboBox";
            this.BaudrateComboBox.Size = new System.Drawing.Size(116, 21);
            this.BaudrateComboBox.TabIndex = 52;
            this.BaudrateComboBox.Visible = false;
            // 
            // cbbChannel
            // 
            this.cbbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbChannel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbChannel.Location = new System.Drawing.Point(657, 636);
            this.cbbChannel.Name = "cbbChannel";
            this.cbbChannel.Size = new System.Drawing.Size(127, 25);
            this.cbbChannel.TabIndex = 33;
            this.cbbChannel.Visible = false;
            this.cbbChannel.SelectedIndexChanged += new System.EventHandler(this.cbbChannel_SelectedIndexChanged);
            // 
            // ComportNamesComboBox
            // 
            this.ComportNamesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComportNamesComboBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComportNamesComboBox.Location = new System.Drawing.Point(379, 635);
            this.ComportNamesComboBox.Name = "ComportNamesComboBox";
            this.ComportNamesComboBox.Size = new System.Drawing.Size(127, 25);
            this.ComportNamesComboBox.TabIndex = 52;
            this.ComportNamesComboBox.Visible = false;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectButton.Location = new System.Drawing.Point(568, 9);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(102, 24);
            this.ConnectButton.TabIndex = 2;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DisconnectButton.Location = new System.Drawing.Point(676, 9);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(102, 24);
            this.DisconnectButton.TabIndex = 3;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // ReceiveDataTimer
            // 
            this.ReceiveDataTimer.Interval = 1;
            this.ReceiveDataTimer.Tick += new System.EventHandler(this.ReceiveDataTimer_Tick);
            // 
            // SettingsControlTimer
            // 
            this.SettingsControlTimer.Tick += new System.EventHandler(this.SettingsControlTimer_Tick);
            // 
            // InformationLabel
            // 
            this.InformationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InformationLabel.AutoSize = true;
            this.InformationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.InformationLabel.Location = new System.Drawing.Point(31, 13);
            this.InformationLabel.Name = "InformationLabel";
            this.InformationLabel.Size = new System.Drawing.Size(0, 18);
            this.InformationLabel.TabIndex = 4;
            // 
            // ParamSendTimer
            // 
            this.ParamSendTimer.Enabled = true;
            this.ParamSendTimer.Interval = 200;
            this.ParamSendTimer.Tick += new System.EventHandler(this.ParamSendTimer_Tick);
            // 
            // BottomGroupBox
            // 
            this.BottomGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BottomGroupBox.Controls.Add(this.panel1);
            this.BottomGroupBox.Controls.Add(this.InformationLabel);
            this.BottomGroupBox.Controls.Add(this.ConnectButton);
            this.BottomGroupBox.Controls.Add(this.DisconnectButton);
            this.BottomGroupBox.Location = new System.Drawing.Point(0, 524);
            this.BottomGroupBox.Name = "BottomGroupBox";
            this.BottomGroupBox.Size = new System.Drawing.Size(784, 34);
            this.BottomGroupBox.TabIndex = 53;
            this.BottomGroupBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(25, 25);
            this.panel1.TabIndex = 55;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.BottomGroupBox);
            this.Controls.Add(this.btnHwRefresh);
            this.Controls.Add(this.CPRefreshButton);
            this.Controls.Add(this.cbbBaudrates);
            this.Controls.Add(this.BaudrateComboBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.cbbChannel);
            this.Controls.Add(this.ComportNamesComboBox);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.BottomGroupBox.ResumeLayout(false);
            this.BottomGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDataFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveDataFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem openConfigFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem variablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.Timer ReceiveDataTimer;
        private System.Windows.Forms.Timer SettingsControlTimer;
        public System.Windows.Forms.ComboBox cbbChannel;
        public System.Windows.Forms.ComboBox cbbBaudrates;
        public System.Windows.Forms.Button btnHwRefresh;
        private System.IO.Ports.SerialPort SerialPortMain;
        public System.Windows.Forms.ComboBox BaudrateComboBox;
        public System.Windows.Forms.ComboBox ComportNamesComboBox;
        public System.Windows.Forms.Button CPRefreshButton;
        private System.Windows.Forms.Label InformationLabel;
        private System.Windows.Forms.Timer ParamSendTimer;
        public System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.GroupBox BottomGroupBox;
        private System.Windows.Forms.Panel panel1;
    }
}

