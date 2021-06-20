namespace EHR_ServiceTool_V3
{
    partial class SplashScreen
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
            this.SplashScreenTimer = new System.Windows.Forms.Timer(this.components);
            this.InfoLabel1 = new System.Windows.Forms.Label();
            this.InfoLabel2 = new System.Windows.Forms.Label();
            this.InfoLabel3 = new System.Windows.Forms.Label();
            this.InfoLabel4 = new System.Windows.Forms.Label();
            this.InfoLabel5 = new System.Windows.Forms.Label();
            this.SplashScreenPictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SplashScreenPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // SplashScreenTimer
            // 
            this.SplashScreenTimer.Tick += new System.EventHandler(this.SplashScreenTimer_Tick);
            // 
            // InfoLabel1
            // 
            this.InfoLabel1.AutoSize = true;
            this.InfoLabel1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.InfoLabel1.Location = new System.Drawing.Point(41, 27);
            this.InfoLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.InfoLabel1.Name = "InfoLabel1";
            this.InfoLabel1.Size = new System.Drawing.Size(161, 17);
            this.InfoLabel1.TabIndex = 1;
            this.InfoLabel1.Text = "VVVVVVVVVVVVVVVVV";
            // 
            // InfoLabel2
            // 
            this.InfoLabel2.AutoSize = true;
            this.InfoLabel2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.InfoLabel2.Location = new System.Drawing.Point(41, 54);
            this.InfoLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.InfoLabel2.Name = "InfoLabel2";
            this.InfoLabel2.Size = new System.Drawing.Size(161, 17);
            this.InfoLabel2.TabIndex = 2;
            this.InfoLabel2.Text = "VVVVVVVVVVVVVVVVV";
            // 
            // InfoLabel3
            // 
            this.InfoLabel3.AutoSize = true;
            this.InfoLabel3.ForeColor = System.Drawing.Color.MidnightBlue;
            this.InfoLabel3.Location = new System.Drawing.Point(41, 81);
            this.InfoLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.InfoLabel3.Name = "InfoLabel3";
            this.InfoLabel3.Size = new System.Drawing.Size(161, 17);
            this.InfoLabel3.TabIndex = 3;
            this.InfoLabel3.Text = "VVVVVVVVVVVVVVVVV";
            // 
            // InfoLabel4
            // 
            this.InfoLabel4.AutoSize = true;
            this.InfoLabel4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.InfoLabel4.Location = new System.Drawing.Point(41, 108);
            this.InfoLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.InfoLabel4.Name = "InfoLabel4";
            this.InfoLabel4.Size = new System.Drawing.Size(161, 17);
            this.InfoLabel4.TabIndex = 4;
            this.InfoLabel4.Text = "VVVVVVVVVVVVVVVVV";
            // 
            // InfoLabel5
            // 
            this.InfoLabel5.AutoSize = true;
            this.InfoLabel5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.InfoLabel5.Location = new System.Drawing.Point(41, 135);
            this.InfoLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.InfoLabel5.Name = "InfoLabel5";
            this.InfoLabel5.Size = new System.Drawing.Size(0, 17);
            this.InfoLabel5.TabIndex = 5;
            // 
            // SplashScreenPictureBox
            // 
            this.SplashScreenPictureBox.Location = new System.Drawing.Point(20, 135);
            this.SplashScreenPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.SplashScreenPictureBox.Name = "SplashScreenPictureBox";
            this.SplashScreenPictureBox.Size = new System.Drawing.Size(1041, 242);
            this.SplashScreenPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SplashScreenPictureBox.TabIndex = 0;
            this.SplashScreenPictureBox.TabStop = false;
            this.SplashScreenPictureBox.Click += new System.EventHandler(this.SplashScreenPictureBox_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(41, 401);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 6;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 428);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InfoLabel5);
            this.Controls.Add(this.InfoLabel4);
            this.Controls.Add(this.InfoLabel3);
            this.Controls.Add(this.InfoLabel2);
            this.Controls.Add(this.InfoLabel1);
            this.Controls.Add(this.SplashScreenPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SplashScreen_FormClosed);
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SplashScreenPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer SplashScreenTimer;
        private System.Windows.Forms.Label InfoLabel1;
        private System.Windows.Forms.Label InfoLabel2;
        private System.Windows.Forms.Label InfoLabel3;
        private System.Windows.Forms.Label InfoLabel4;
        private System.Windows.Forms.Label InfoLabel5;
        private System.Windows.Forms.PictureBox SplashScreenPictureBox;
        private System.Windows.Forms.Label label1;
    }
}

