using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EHR_ServiceTool_V3
{
    public partial class License : Form
    {
        private int CorrectLicenseFile = 0;
        public License()
        {
            InitializeComponent();
            LicenseKeyLabel.Text = SplashScreen.LSLicenseKey + " :";
            MachineIDLabel.Text = SplashScreen.LSMachineID + " :";
            OKButton.Text = SplashScreen.LSOK;
            this.Text = SplashScreen.LSEnterLicenseKey + "...";

        }

        private void License_Load(object sender, EventArgs e)
        {
            //this.Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.BaseDirectory + "program.ico");
            MachineIDTextBox.Text = LicenseCheck.GetMachineID(LicenseCheck.GetHardWareInfo("Win32_Processor", "ProcessorId"));

        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (LicenseKeyTextBox.Text.Length == 23)
                {

                    int i = 0;
                    foreach (char ch in LicenseKeyTextBox.Text)
                    {

                        if ((i == 5) || (i == 11) || (i == 17))
                        {
                            if (ch == '-')
                            {
                                CorrectLicenseFile++;
                            }
                            else
                            {
                                CorrectLicenseFile = 0;
                            }
                        }
                        i++;
                    }
                    if (CorrectLicenseFile == 3)
                    {
                        if (LicenseCheck.Resolve(LicenseKeyTextBox.Text))
                        {
                            string Path = AppDomain.CurrentDomain.BaseDirectory + "License.ini";
                            using (FileStream fs = File.Create(Path))
                            {
                                string EncryptedLicense = LicenseCheck.Encrypt(LicenseKeyTextBox.Text, "EHR_ServiceTool_V3");
                                Byte[] License = new UTF8Encoding(true).GetBytes(EncryptedLicense);

                                fs.Write(License, 0, License.Length);

                            }

                            SplashScreen.Licensed = true;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(SplashScreen.LSLicenseKeyIsNotValid, SplashScreen.LSLicenseKeyMismatch, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(SplashScreen.LSBeSureEnterLicenseCorrectly + "\n" + SplashScreen.LSLicenseKeyFormat, SplashScreen.LSLicenseKeyNotEligible, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {

            }

        }
    }
}
