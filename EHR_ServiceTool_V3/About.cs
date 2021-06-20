using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EHR_ServiceTool_V3
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            String AppDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (File.Exists(AppDirectory + "icon.ico"))
            {
                this.Icon = Icon.ExtractAssociatedIcon(AppDirectory + "icon.ico");

            }

        }

        private void About_Load(object sender, EventArgs e)
        {
            this.Text = SplashScreen.LSAbout;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.LinkVisited = true;


            System.Diagnostics.Process.Start("https://ehrelektronik.com/");
        }
    }
}
