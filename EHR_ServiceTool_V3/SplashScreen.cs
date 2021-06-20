using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EHR_ServiceTool_V3
{

    public partial class SplashScreen : Form
    {
        int Cnt;
        public static bool Licensed;
        private static bool CorrectLicenseFile;

        public static string SelectedLanguage;
        public static string LastConfigFile, LastDataFile;
        public static int HardwareType, SelectedCPDeviceIndex, SelectedCPBaudrateIndex, SelectedCANDeviceIndex, SelectedCANBaudrateIndex;
        public static string SelectedComportName, SelectedCANDeviceName;
        public static UInt32 StartID;
        public static string AppName;
        // Dil stringleri burada //
        public static string LSFile, LSPage, LSOpenDataFile, LSSaveDataFile, LSOpenConfigFile, LSParameters, LSVariables,
        LSSettings, LSAbout, LSNextPage, LSPreviousPage, LSSendToDevice, LSConfigFileType, LSHardwareType, LSStartID,
        LSLanguage, LSDevice, LSOK, LSCancel, LSRefresh, LSDevicePreferences, LSConnect, LSDisconnect, LSMachineID,
        LSLicenseKey, LSLicenseKeyIsNotValid, LSEnterLicenseKey, LSLicenseKeyMismatch, LSBeSureEnterLicenseCorrectly,
        LSLicenseKeyNotEligible, LSLicenseKeyFormat, LSReEnterLicenseKey, LSLicenseFileIsCorrupted, LSStatus, LSOnline, LSOffline,
        LSConnection, LSVerified, LSUnverified, LSNoDeviceSelection, LSGotoSettingsAndSelectDevice, LSSelectedDeviceIsDisplayedInTheLowerLeft,
        LSDataFileType, LSDeviceDisconnected, LSParametersCouldNotBeSent, LSParametersWereSuccessfullySent, LSLicenceCheckSuccesful;

        private void SplashScreenPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void SplashScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            String AppDirectory = AppDomain.CurrentDomain.BaseDirectory;
            IniFile sehr = new IniFile(AppDirectory + "Settings.sehr");
            sehr.Write("AppSettings", "Language", SelectedLanguage);
            sehr.Write("AppSettings", "LastConfigFile", LastConfigFile);
            sehr.Write("AppSettings", "LastDataFile", LastDataFile);
            sehr.Write("AppSettings", "HardwareType", HardwareType.ToString());
            sehr.Write("AppSettings", "SelectedCPBaudrateIndex", SelectedCPBaudrateIndex.ToString());
            sehr.Write("AppSettings", "SelectedCANBaudrateIndex", SelectedCANBaudrateIndex.ToString());
            sehr.Write("AppSettings", "SelectedComportName", SelectedComportName);
            sehr.Write("AppSettings", "SelectedCANDeviceName", SelectedCANDeviceName);
            sehr.Write("AppSettings", "StartID", StartID.ToString());

        }

        public SplashScreen()
        {
            InitializeComponent();
        }
        private void SplashScreen_Load(object sender, EventArgs e)
        {

            String AppDirectory = AppDomain.CurrentDomain.BaseDirectory;
            IniFile sehr = new IniFile(AppDirectory + "Settings.sehr");
            SelectedLanguage = sehr.Read("AppSettings", "Language");
            LastConfigFile = sehr.Read("AppSettings", "LastConfigFile");
            LastDataFile = sehr.Read("AppSettings", "LastDataFile");
            HardwareType = Int32.Parse(sehr.Read("AppSettings", "HardwareType"));
            SelectedCPBaudrateIndex = Int32.Parse(sehr.Read("AppSettings", "SelectedCPBaudrateIndex"));
            SelectedCANBaudrateIndex = Int32.Parse(sehr.Read("AppSettings", "SelectedCANBaudrateIndex"));
            SelectedComportName = sehr.Read("AppSettings", "SelectedComportName");
            SelectedCANDeviceName = sehr.Read("AppSettings", "SelectedCANDeviceName");
            StartID = UInt32.Parse(sehr.Read("AppSettings", "StartID"));
            AppName = sehr.Read("AppSettings", "AppName");

            if (File.Exists(AppDirectory + "//Language//" + SelectedLanguage + ".lng"))
            {
                IniFile lng = new IniFile(AppDirectory + "//Language//" + SelectedLanguage + ".lng");

                LoadLanguage(lng);
                label1.Text = LSLicenceCheckSuccesful;
            }
            else
            {
                LoadDefault();
            }




            Licensed = false;
            var OpenFile = new System.Windows.Forms.OpenFileDialog();
            OpenFile.Filter = "png files|*.png";
            OpenFile.FileName = AppDomain.CurrentDomain.BaseDirectory + "logo.png";
            string ConfigIniLoc = AppDomain.CurrentDomain.BaseDirectory + "Settings.sehr";
            IniFile ini = new IniFile(ConfigIniLoc);

            InfoLabel1.Text = ini.Read("InfoSection", "Info1");
            InfoLabel2.Text = ini.Read("InfoSection", "Info2");
            InfoLabel3.Text = ini.Read("InfoSection", "Info3");
            InfoLabel4.Text = ini.Read("InfoSection", "Info4");
            InfoLabel5.Text = ini.Read("InfoSection", "Info5");
            Cnt = 30;
            string Path = AppDomain.CurrentDomain.BaseDirectory + "License.ini";
            if (File.Exists(Path))
            {
                string LicenseKeyEncrypted = File.ReadAllText(Path, Encoding.UTF8);
                if (LicenseKeyEncrypted.Length == 64)
                {
                    string LicenseKey = LicenseCheck.Decrypt(LicenseKeyEncrypted, "EHR_ServiceTool_V3");

                    try
                    {
                        if (LicenseKey.Length == 23)
                        {
                            int i = 0;
                            foreach (char ch in LicenseKey)
                            {

                                if ((i == 5) || (i == 11) || (i == 17))
                                {
                                    if (ch == '-')
                                    {
                                        CorrectLicenseFile = true;
                                    }
                                    else
                                    {
                                        CorrectLicenseFile = false;
                                    }
                                }
                                i++;
                            }
                            if (CorrectLicenseFile)
                            {
                                if (LicenseCheck.Resolve(LicenseKey))
                                {
                                    Licensed = true;

                                    SplashScreenTimer.Enabled = true;
                                }
                                else
                                {
                                    SplashScreenTimer.Enabled = false;
                                    if (MessageBox.Show(SplashScreen.LSEnterLicenseKey, SplashScreen.LSLicenseKeyIsNotValid, MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                                    {
                                        new License().ShowDialog();
                                        if (Licensed)
                                        {
                                            SplashScreenTimer.Enabled = true;
                                        }
                                        else
                                        {
                                            this.Close();
                                        }

                                    }
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
                else
                {
                    MessageBox.Show(SplashScreen.LSReEnterLicenseKey, SplashScreen.LSLicenseFileIsCorrupted, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    File.Delete(Path);
                    new License().ShowDialog();
                    if (Licensed)
                    {
                        SplashScreenTimer.Enabled = true;
                    }
                    else
                    {
                        this.Close();
                    }

                }
            }
            else
            {
                SplashScreenTimer.Enabled = false;
                new License().ShowDialog();
                if (Licensed)
                {
                    SplashScreenTimer.Enabled = true;
                }
                else
                {
                    this.Close();
                }

            }



            if (File.Exists(OpenFile.FileName))
            {

                SplashScreenPictureBox.Image = Image.FromFile(OpenFile.FileName);

            }

        }

        private void SplashScreenTimer_Tick(object sender, EventArgs e)
        {
            Cnt--;
            if (Cnt <= 0)
            {
                SplashScreenTimer.Enabled = false;
                this.Hide();
                new MainForm().ShowDialog();
                this.Close();


            }
        }
        private void LoadLanguage(IniFile lng)
        {
            LSFile = lng.Read("Strings", "File");
            LSPage = lng.Read("Strings", "Page");
            LSOpenDataFile = lng.Read("Strings", "Open Data File");
            LSSaveDataFile = lng.Read("Strings", "Save Data File");
            LSOpenConfigFile = lng.Read("Strings", "Open Config File");
            LSParameters = lng.Read("Strings", "Parameters");
            LSVariables = lng.Read("Strings", "Variables");
            LSAbout = lng.Read("Strings", "About");
            LSSettings = lng.Read("Strings", "Settings");
            LSNextPage = lng.Read("Strings", "Next Page");
            LSPreviousPage = lng.Read("Strings", "Previous Page");
            LSSendToDevice = lng.Read("Strings", "Send To Device");
            LSConfigFileType = lng.Read("Strings", "Config File");
            LSDataFileType = lng.Read("Strings", "Data File");
            LSHardwareType = lng.Read("Strings", "Hardware Type");
            LSStartID = lng.Read("Strings", "Start ID");
            LSLanguage = lng.Read("Strings", "Language");
            LSDevice = lng.Read("Strings", "Device");
            LSOK = lng.Read("Strings", "OK");
            LSCancel = lng.Read("Strings", "Cancel");
            LSRefresh = lng.Read("Strings", "Refresh");
            LSDevicePreferences = lng.Read("Strings", "Device Preferences");
            LSConnect = lng.Read("Strings", "Connect");
            LSDisconnect = lng.Read("Strings", "Disconnect");
            LSMachineID = lng.Read("Strings", "Machine ID");
            LSLicenseKey = lng.Read("Strings", "License Key");
            LSLicenseKeyIsNotValid = lng.Read("Strings", "License Key Is Not Valid");
            LSEnterLicenseKey = lng.Read("Strings", "Enter License Key");
            LSLicenseKeyMismatch = lng.Read("Strings", "License Key Mismatch");
            LSBeSureEnterLicenseCorrectly = lng.Read("Strings", "Be Sure To Enter Your License Key Corretly");
            LSLicenseKeyNotEligible = lng.Read("Strings", "License Key Not Eligible");
            LSLicenseKeyFormat = lng.Read("Strings", "License Key Format: XXXXX-XXXXX-XXXXX-XXXXX");
            LSReEnterLicenseKey = lng.Read("Strings", "Re-Enter License Key");
            LSLicenseFileIsCorrupted = lng.Read("Strings", "License File Is Corrupted");
            LSStatus = lng.Read("Strings", "Status");
            LSOnline = lng.Read("Strings", "Online");
            LSOffline = lng.Read("Strings", "Offline");
            LSConnection = lng.Read("Strings", "Connection");
            LSVerified = lng.Read("Strings", "Verified");
            LSUnverified = lng.Read("Strings", "Unverified");
            LSNoDeviceSelection = lng.Read("Strings", "No Device Selection");
            LSGotoSettingsAndSelectDevice = lng.Read("Strings", "Go to Settings And Select Device");
            LSSelectedDeviceIsDisplayedInTheLowerLeft = lng.Read("Strings", "Selected Device Is Displayed In The Lower Left");
            LSDeviceDisconnected = lng.Read("Strings", "Device Disconnected");
            LSParametersCouldNotBeSent = lng.Read("Strings", "Parameters Could Not Be Sent");
            LSParametersWereSuccessfullySent = lng.Read("Strings", "Parameters Were Successfully Sent");
            LSLicenceCheckSuccesful = lng.Read("Strings", "Licence Check Succesful");
        }
        private void LoadDefault()
        {
            LSFile = "File";
            LSPage = "Page";
        }
    }
}
