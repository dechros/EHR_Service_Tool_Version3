using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace EHR_ServiceTool_V3
{


    public partial class SettingsForm : Form
    {
        private static UInt32 StartID;
        public SettingsForm()
        {
            InitializeComponent();



            string LanguageFolder = AppDomain.CurrentDomain.BaseDirectory + "//Language";
            DirectoryInfo di = new DirectoryInfo(LanguageFolder);
            FileInfo[] files = di.GetFiles("*.lng");
            foreach (FileInfo file in files)
            {
                string LangName = file.Name;
                int DotPos = LangName.IndexOf(".");
                LangName = LangName.Substring(0, DotPos);
                LanguageComboBox.Items.Add(LangName);
            }
            String AppDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (File.Exists(AppDirectory + "icon.ico"))
            {
                this.Icon = Icon.ExtractAssociatedIcon(AppDirectory + "icon.ico");

            }

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                int StartID = int.Parse(StartIDTextBox.Text, System.Globalization.NumberStyles.HexNumber);

            }
            catch
            {
                StartIDTextBox.Text = SplashScreen.StartID.ToString("X4");
            }
            this.Text = SplashScreen.LSSettings;
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.SettingsFormDisplayed = false;
        }

        private void LanguageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
        private void LoadLanguage(IniFile lng)
        {
            SplashScreen.LSFile = lng.Read("Strings", "File");
            SplashScreen.LSPage = lng.Read("Strings", "Page");
            SplashScreen.LSOpenDataFile = lng.Read("Strings", "Open Data File");
            SplashScreen.LSSaveDataFile = lng.Read("Strings", "Save Data File");
            SplashScreen.LSOpenConfigFile = lng.Read("Strings", "Open Config File");
            SplashScreen.LSParameters = lng.Read("Strings", "Parameters");
            SplashScreen.LSVariables = lng.Read("Strings", "Variables");
            SplashScreen.LSAbout = lng.Read("Strings", "About");
            SplashScreen.LSSettings = lng.Read("Strings", "Settings");
            SplashScreen.LSNextPage = lng.Read("Strings", "Next Page");
            SplashScreen.LSPreviousPage = lng.Read("Strings", "Previous Page");
            SplashScreen.LSSendToDevice = lng.Read("Strings", "Send To Device");
            SplashScreen.LSConfigFileType = lng.Read("Strings", "Config File");
            SplashScreen.LSHardwareType = lng.Read("Strings", "Hardware Type");
            SplashScreen.LSStartID = lng.Read("Strings", "Start ID");
            SplashScreen.LSLanguage = lng.Read("Strings", "Language");
            SplashScreen.LSDevice = lng.Read("Strings", "Device");
            SplashScreen.LSOK = lng.Read("Strings", "OK");
            SplashScreen.LSCancel = lng.Read("Strings", "Cancel");
            SplashScreen.LSRefresh = lng.Read("Strings", "Refresh");
            SplashScreen.LSDevicePreferences = lng.Read("Strings", "Device Preferences");
            SplashScreen.LSConnect = lng.Read("Strings", "Connect");
            SplashScreen.LSDisconnect = lng.Read("Strings", "Disconnect");
            SplashScreen.LSMachineID = lng.Read("Strings", "Machine ID");
            SplashScreen.LSLicenseKey = lng.Read("Strings", "License Key");
            SplashScreen.LSLicenseKeyIsNotValid = lng.Read("Strings", "License Key Is Not Valid");
            SplashScreen.LSEnterLicenseKey = lng.Read("Strings", "Enter License Key");
            SplashScreen.LSLicenseKeyMismatch = lng.Read("Strings", "License Key Mismatch");
            SplashScreen.LSBeSureEnterLicenseCorrectly = lng.Read("Strings", "Be Sure To Enter Your License Key Corretly");
            SplashScreen.LSLicenseKeyNotEligible = lng.Read("Strings", "License Key Not Eligible");
            SplashScreen.LSLicenseKeyFormat = lng.Read("Strings", "License Key Format: XXXXX-XXXXX-XXXXX-XXXXX");
            SplashScreen.LSReEnterLicenseKey = lng.Read("Strings", "Re-Enter License Key");
            SplashScreen.LSLicenseFileIsCorrupted = lng.Read("Strings", "License File Is Corrupted");
            SplashScreen.LSStatus = lng.Read("Strings", "Status");
            SplashScreen.LSOnline = lng.Read("Strings", "Online");
            SplashScreen.LSOffline = lng.Read("Strings", "Offline");
            SplashScreen.LSConnection = lng.Read("Strings", "Connection");
            SplashScreen.LSVerified = lng.Read("Strings", "Verified");
            SplashScreen.LSUnverified = lng.Read("Strings", "Unverified");
            SplashScreen.LSNoDeviceSelection = lng.Read("Strings", "No Device Selection");
            SplashScreen.LSGotoSettingsAndSelectDevice = lng.Read("Strings", "Go to Settings And Select Device");
            SplashScreen.LSSelectedDeviceIsDisplayedInTheLowerLeft = lng.Read("Strings", "Selected Device Is Displayed In The Lower Left");
            SplashScreen.LSDeviceDisconnected = lng.Read("Strings", "Device Disconnected");
            SplashScreen.LSParametersCouldNotBeSent = lng.Read("Strings", "Parameters Could Not Be Sent");
            SplashScreen.LSParametersWereSuccessfullySent = lng.Read("Strings", "Parameters Were Successfully Sent");
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            string LanguageName = LanguageComboBox.SelectedItem.ToString();

            if (LanguageName != SplashScreen.SelectedLanguage)
            {
                string AppDirectory = AppDomain.CurrentDomain.BaseDirectory;
                IniFile lng = new IniFile(AppDirectory + "//Language//" + LanguageName + ".lng");
                LoadLanguage(lng);
                SplashScreen.SelectedLanguage = LanguageName;
                MainForm.LanguageChanged = true;
            }
            if (RS232RButton.Checked)
            {
                SplashScreen.HardwareType = 0;
                if (DeviceSelectComboBox.SelectedIndex > -1)
                {
                    SplashScreen.SelectedCPDeviceIndex = DeviceSelectComboBox.SelectedIndex;

                    SplashScreen.SelectedComportName = DeviceSelectComboBox.SelectedItem.ToString();
                }
                SplashScreen.SelectedCPBaudrateIndex = BaudrateComboBox.SelectedIndex;

            }
            else
            {
                SplashScreen.HardwareType = 1;
                if (DeviceSelectComboBox.SelectedIndex > -1)
                {
                    SplashScreen.SelectedCANDeviceIndex = DeviceSelectComboBox.SelectedIndex;

                    SplashScreen.SelectedCANDeviceName = DeviceSelectComboBox.SelectedItem.ToString();
                }
                SplashScreen.SelectedCANBaudrateIndex = BaudrateComboBox.SelectedIndex;

            }
            SplashScreen.StartID = StartID;
            this.Close();
        }

        private void RS232RButton_CheckedChanged(object sender, EventArgs e)
        {
            if (RS232RButton.Checked)
            {
                MainForm.RS232Checked = true;
            }
        }

        private void USBCANRButton_CheckedChanged(object sender, EventArgs e)
        {
            if (USBCANRButton.Checked)
            {
                MainForm.USBCANChecked = true;
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            MainForm.SettingsRefreshClicked = true;
        }

        private void SettingsCancelButton_Click(object sender, EventArgs e)
        {
            StartID = SplashScreen.StartID;
            this.Close();
        }

        private void StartIDTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (StartIDTextBox.Text != string.Empty)
                {
                    StartID = UInt32.Parse(StartIDTextBox.Text, System.Globalization.NumberStyles.HexNumber);
                }


            }
            catch
            {
                StartIDTextBox.Text = SplashScreen.StartID.ToString("X4");
            }
        }
    }
}
