using Peak.Can.Basic;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TPCANHandle = System.UInt16;

namespace EHR_ServiceTool_V3
{

    public partial class MainForm : Form
    {
        public static int CurrentWidth, CurrentHeight;
        public static bool IsSizeChanged;
        public static int ChildFormSizeChangedCounter, VarUpdateCounter, ParamUpdateCounter;
        private Bitmap Green, Red, White;

        public static string[] ParameterNames = new string[1000];
        public static string[] VariableNames = new string[1000];
        public static string[] ParamPageNames = new string[51];
        public static string[] VarPageNames = new string[51];
        public static Int16[] ParameterValues = new Int16[1000];
        public static Int16[] ParameterValuesCopy = new Int16[1000];
        public static Int16[] VariableValues = new Int16[1000];
        public static int LastReceivedParamPage, LastReceivedParamSize, LastReceivedVarPage, LastReceivedVarSize;
        public static Int16[] LastReceivedParamDatas = new Int16[20];
        public static Int16[] LastReceivedVarDatas = new Int16[20];

        public static string PackageData;
        public static bool PackageStarted, PackageEnded;
        public static bool[] IsLocationFilled = new bool[4];


        public static UInt32 StartID;
        public static UInt16 MaxParamPageNumber, MaxParamValueNumber, MaxVarPageNumber, MaxVarValueNumber;
        public static int CurrentParamPageNumber, CurrentParamValueNumber, HashForThisPage;
        public static UInt32 CurrentParamPageNumber_Serial, CurrentParamValueNumber_Serial, HashForThisPage_Serial;
        public static bool SendToDeviceIsClicked, SendHash;
        public static bool ParShortcutsLoaded, VarShortcutsLoaded;
        public static SettingsForm SetForm;
        public static About AboutForm;
        public static bool SettingsFormDisplayed;
        public static bool LanguageChanged;
        public static bool RS232Checked;
        public static bool USBCANChecked;
        public static bool SettingsRefreshClicked;
        public static bool Connected;
        public static bool DeviceIsCorrect, DeviceGetError;
        public static bool ParamUpdate, VarUpdate, TurnOnLed, ReadParamFromDevice, LoadedDataFile, ParameterRequested;

        // CRC kurulum parametreleri 
        readonly Parameters CRC16Arc_Param = new Parameters("CRC-16/ARC", 16, 0x8005, 0x0, true, true, 0x0, 0xBB3D);
        readonly Parameters CRC32Mpeg2_Param = new Parameters("CRC-32/MPEG-2", 32, 0x04C11DB7, 0xFFFFFFFF, false, false, 0x00000000, 0x0376E6E7);
        readonly Crc CRC16Arc, CRC32Mpeg2;
        #region CAN_Delegates

        private delegate void ReadDelegateHandler();
        #endregion

        #region CAN_Members

        private bool m_IsFD;

        private TPCANHandle m_PcanHandle;

        private TPCANBaudrate m_Baudrate;

        private TPCANType m_HwType;

        private System.Collections.ArrayList m_LastMsgsList;

        private ReadDelegateHandler m_ReadDelegate;

        private System.Threading.AutoResetEvent m_ReceiveEvent;

        private System.Threading.Thread m_ReadThread;

        private TPCANHandle[] m_NonPnPHandles;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            m_IsFD = false;
            CRC16Arc = new Crc(CRC16Arc_Param);
            CRC16Arc.IsRight();
            CRC32Mpeg2 = new Crc(CRC32Mpeg2_Param);
            CRC32Mpeg2.IsRight();



            StartID = SplashScreen.StartID;
            ParShortcutsLoaded = false;
            VarShortcutsLoaded = false;
            DisconnectButton.Enabled = false;
            SendToDeviceIsClicked = false;
            ReadParamFromDevice = false;
            ParameterRequested = false;
            CurrentParamPageNumber = 0;
            CurrentParamValueNumber = 0;
            IsSizeChanged = false;
            ChildFormSizeChangedCounter = 0;
            VarUpdateCounter = 0;
            ParamUpdateCounter = 0;

            CurrentParamPageNumber_Serial = 0;
            String AppDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (File.Exists(AppDirectory + "icon.ico"))
            {
                this.Icon = Icon.ExtractAssociatedIcon(AppDirectory + "icon.ico");
                this.Text = SplashScreen.AppName;
            }
            if (File.Exists(AppDirectory + "background.jpg"))
            {

                Image BackGround = new Bitmap(AppDirectory + "background.jpg");
                this.BackgroundImage = BackGround;
            }

            Green = new Bitmap(AppDirectory + "green.png");
            Red = new Bitmap(AppDirectory + "red.png");
            White = new Bitmap(AppDirectory + "white.png");



            if (File.Exists(SplashScreen.LastConfigFile))
            {
                IniFile cehr = new IniFile(SplashScreen.LastConfigFile);
                parametersToolStripMenuItem.DropDownItems.Clear();
                variablesToolStripMenuItem.DropDownItems.Clear();
                VarShortcutsLoaded = false;
                ParShortcutsLoaded = false;

                for (int i = 1; i <= 1000; i++)
                {
                    ParameterNames[i - 1] = cehr.Read("ParameterNames", "Param" + i.ToString());
                    VariableNames[i - 1] = cehr.Read("VariableNames", "Var" + i.ToString());
                }
                int[] PageNos = new int[50];
                int index = 0;
                for (int k = 1; k <= 50; k++)
                {
                    string ParShortcut = cehr.Read("ParamPageNamesAndShortcuts", "ParamPageShortcut" + k.ToString());
                    if (ParShortcut != string.Empty)
                    {
                        int CommaPos = ParShortcut.IndexOf(',');
                        try
                        {
                            int PageNo = Int32.Parse(ParShortcut.Substring(CommaPos + 1, ParShortcut.Length - CommaPos - 1));
                            ParamPageNames[PageNo] = ParShortcut.Substring(0, CommaPos);
                            PageNos[index] = PageNo;
                            index++;

                        }
                        catch
                        {

                        }

                    }
                }

                ToolStripMenuItem[] items = new ToolStripMenuItem[index];
                for (int i = 0; i < index; i++)
                {

                    items[i] = new ToolStripMenuItem();
                    items[i].Name = PageNos[i].ToString();
                    items[i].Tag = "";
                    if (ParamPageNames[PageNos[i]] != string.Empty)
                    {
                        items[i].Text = ParamPageNames[PageNos[i]];
                    }
                    else
                    {
                        items[i].Text = SplashScreen.LSPage + " " + PageNos[i].ToString();
                    }

                    items[i].Click += new EventHandler(MenuParItemClickHandler);
                    ParShortcutsLoaded = true;

                }
                parametersToolStripMenuItem.DropDownItems.AddRange(items);
                index = 0;
                for (int k = 1; k <= 50; k++)
                {
                    string VarShortcut = cehr.Read("VarPageNamesAndShortcuts", "VarPageShortcut" + k.ToString());
                    if (VarShortcut != string.Empty)
                    {
                        int CommaPos = VarShortcut.IndexOf(',');
                        try
                        {
                            int PageNo = Int32.Parse(VarShortcut.Substring(CommaPos + 1, VarShortcut.Length - CommaPos - 1));
                            VarPageNames[PageNo] = VarShortcut.Substring(0, CommaPos);
                            PageNos[index] = PageNo;
                            index++;

                        }
                        catch
                        {

                        }

                    }
                }


                ToolStripMenuItem[] items2 = new ToolStripMenuItem[index];
                for (int i = 0; i < index; i++)
                {
                    items2[i] = new ToolStripMenuItem();
                    items2[i].Name = PageNos[i].ToString();
                    items2[i].Tag = "";
                    if (VarPageNames[PageNos[i]] != string.Empty)
                    {
                        items2[i].Text = VarPageNames[PageNos[i]];
                    }
                    else
                    {
                        items2[i].Text = SplashScreen.LSPage + " " + PageNos[i].ToString();
                    }

                    items2[i].Click += new EventHandler(MenuVarItemClickHandler);
                    VarShortcutsLoaded = true;
                }
                variablesToolStripMenuItem.DropDownItems.AddRange(items2);

            }






        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            CurrentHeight = this.Height;
            CurrentWidth = this.Width;
            panel1.Refresh();
            for (int i = 0; i < 4; i++)
            {
                IsLocationFilled[i] = false;

            }

            AboutForm = new About();
            AboutForm.Hide();
            SetForm = new SettingsForm();

            SetForm.Hide();
            SettingsFormDisplayed = false;
            LanguageChanged = false;
            Connected = false;
            DeviceIsCorrect = false;
            DeviceGetError = false;
            ParamUpdate = false;
            VarUpdate = false;

            TurnOnLed = false;
            SetLanguage();
            btnHwRefresh_Click(sender, e);
            CPRefreshButton_Click(sender, e);
            SetInfoLabelText();
            GC.Collect();
        }


        private void MenuParItemClickHandler(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i < 4; i++)
            {

                if (IsLocationFilled[i] == false)
                {
                    IsLocationFilled[i] = true;
                    break;
                }
            }
            if ((i != 4))
            {
                ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
                string text = clickedItem.Name;
                ParametresForm ParamForm = new ParametresForm() { Owner = this };
                ParamForm.MdiParent = this;
                ParamForm.Location = SetLocation(i, ParamForm.Width, ParamForm.Height);

                int PageNo = Int32.Parse(text);
                if (ParamPageNames[PageNo] != null)
                {
                    ParamForm.Text = ParamPageNames[PageNo];
                }
                else
                {
                    ParamForm.Text = SplashScreen.LSParameters + " " + SplashScreen.LSPage + " " + text;
                }


                foreach (Label label in ParamForm.Controls.OfType<Label>())
                {
                    string Name = label.Name;
                    if ((Name != "PageNo") && (Name != "LocationIndex"))
                    {
                        Name = Name.Substring(5, (Name.Length - 5));
                        int ValueNo = Int32.Parse(Name);
                        label.Text = SetLabelText(PageNo, ValueNo);
                    }
                    else
                    {
                        if (Name == "PageNo")
                        {
                            label.Text = text;
                        }
                        else
                        {
                            label.Text = i.ToString();
                        }

                    }


                }

                ParamForm.ShowIcon = false;
                ParamForm.ShowInTaskbar = false;
                ParamForm.SendToBack();


                ParamForm.Focus();
                ParamForm.Show();
                this.Activate();
            }




        }

        private void MenuVarItemClickHandler(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i < 4; i++)
            {

                if (IsLocationFilled[i] == false)
                {
                    IsLocationFilled[i] = true;
                    break;
                }
            }
            if ((i != 4))
            {
                ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
                string text = clickedItem.Name;
                VariablesForm VarForm = new VariablesForm() { Owner = this };
                VarForm.MdiParent = this;
                VarForm.Location = SetLocation(i, VarForm.Width, VarForm.Height);
                int PageNo = Int32.Parse(text);
                if (VarPageNames[PageNo] != null)
                {
                    VarForm.Text = VarPageNames[PageNo];
                }
                else
                {
                    VarForm.Text = SplashScreen.LSVariables + " " + SplashScreen.LSPage + " " + text;
                }

                foreach (Label label in VarForm.Controls.OfType<Label>())
                {
                    string Name = label.Name;
                    if ((Name != "PageNo") && (Name != "LocationIndex"))
                    {
                        Name = Name.Substring(3, (Name.Length - 3));
                        int ValueNo = Int32.Parse(Name);
                        label.Text = SetLabelText(PageNo, ValueNo);
                    }
                    else
                    {
                        if (Name == "PageNo")
                        {
                            label.Text = text;
                        }
                        else
                        {
                            label.Text = i.ToString();
                        }
                    }


                }

                VarForm.ShowIcon = false;
                VarForm.ShowInTaskbar = false;
                VarForm.SendToBack();


                VarForm.Focus();
                VarForm.Show();
                this.Activate();
            }



        }

        private static string SetLabelText(int PageNo, int ValueNo)
        {
            string Text = ((PageNo - 1) * 20 + ValueNo).ToString() + ". " + ParameterNames[((PageNo - 1) * 20) + (ValueNo - 1)] + "..................................................................";
            return Text;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int SelectedLanguageIndex = SetForm.LanguageComboBox.Items.IndexOf(SplashScreen.SelectedLanguage);
            SetForm.LanguageComboBox.SelectedIndex = SelectedLanguageIndex;
            if (SplashScreen.HardwareType == 0)
            {
                SetForm.RS232RButton.Checked = true;
                SetForm.DeviceSelectComboBox.Items.Clear();
                SetForm.BaudrateComboBox.Items.Clear();
                SetForm.DeviceSelectComboBox.Items.AddRange(ComportNamesComboBox.Items.Cast<Object>().ToArray());
                SetForm.BaudrateComboBox.Items.AddRange(BaudrateComboBox.Items.Cast<Object>().ToArray());
                SetForm.BaudrateComboBox.SelectedIndex = SplashScreen.SelectedCPBaudrateIndex;
                if (SetForm.DeviceSelectComboBox.Items.IndexOf(SplashScreen.SelectedComportName) > -1)
                {
                    SetForm.DeviceSelectComboBox.SelectedIndex = SetForm.DeviceSelectComboBox.Items.IndexOf(SplashScreen.SelectedComportName);
                }

            }
            else
            {
                SetForm.USBCANRButton.Checked = true;

                SetForm.DeviceSelectComboBox.Items.Clear();
                SetForm.BaudrateComboBox.Items.Clear();

                SetForm.DeviceSelectComboBox.Items.AddRange(cbbChannel.Items.Cast<Object>().ToArray());
                SetForm.BaudrateComboBox.Items.AddRange(cbbBaudrates.Items.Cast<Object>().ToArray());

                SetForm.BaudrateComboBox.SelectedIndex = SplashScreen.SelectedCANBaudrateIndex;
                if (SetForm.DeviceSelectComboBox.Items.IndexOf(SplashScreen.SelectedCANDeviceName) > -1)
                {
                    SetForm.DeviceSelectComboBox.SelectedIndex = SetForm.DeviceSelectComboBox.Items.IndexOf(SplashScreen.SelectedCANDeviceName);
                }
            }
            SettingsFormDisplayed = true;
            SettingsControlTimer.Enabled = true;
            SetForm.TopMost = true;
            SetForm.BringToFront();
            SetForm.Activate();
            SetForm.ShowDialog();


        }
        #region CAN_Functions 
        private void InitializeBasicComponents()
        {
            m_LastMsgsList = new System.Collections.ArrayList();

            m_ReadDelegate = new ReadDelegateHandler(ReadMessages);

            m_ReceiveEvent = new System.Threading.AutoResetEvent(false);

            m_NonPnPHandles = new TPCANHandle[]
            {

            };


        }
        private void ReadSeriPort()
        {

            try
            {
                if ((Connected) && (SerialPortMain.IsOpen))
                {
                    int BytesToRead = SerialPortMain.BytesToRead;
                    char ReadChar;
                    while (BytesToRead > 0)
                    {
                        ReadChar = Convert.ToChar(SerialPortMain.ReadByte());
                        if (ReadChar == '>')
                        {
                            PackageStarted = true;
                            PackageData = string.Empty;
                        }
                        else if (ReadChar == '<')
                        {
                            PackageStarted = false;
                            ParseData(PackageData);
                            PackageData = string.Empty;
                        }
                        else if (PackageStarted)
                        {

                            PackageData = PackageData + ReadChar;
                        }
                        BytesToRead--;

                    }
                }
                else
                {

                }
            }
            catch (Exception Err)
            {

            }

        }
        private void ParseData(string PackageData)
        {

            string ID, Data, CrcString, PackageDataCopy;
            CrcString = string.Empty;
            ID = string.Empty;
            PackageDataCopy = PackageData;
            int CommaPos = PackageDataCopy.IndexOf(',');
            int FirstCommaPos = CommaPos;
            int LastCommaPos = 0;
            int i = 0;
            while (CommaPos > 0)
            {
                if (i == 0)
                {
                    ID = PackageDataCopy.Substring(0, CommaPos);

                }
                PackageDataCopy = PackageDataCopy.Substring(CommaPos + 1, (PackageDataCopy.Length - (CommaPos + 1)));
                LastCommaPos = LastCommaPos + CommaPos + 1;
                CommaPos = PackageDataCopy.IndexOf(',');
                if (CommaPos < 0)
                {
                    CrcString = PackageDataCopy;

                }
                i++;
            }
            PackageData = PackageData.Substring(0, LastCommaPos);
            byte[] Bytes = Encoding.ASCII.GetBytes(PackageData);
            var Hash = CRC32Mpeg2.ComputeHash(Bytes, 0, Bytes.Length);

            string HashString = BitConverter.ToString(Hash).Replace("-", string.Empty).Substring(8, 8);

            int HashValueDec = Convert.ToInt32(HashString, 16);
            int CrcValueDec = Convert.ToInt32(CrcString, 16);
            if (HashValueDec == CrcValueDec)
            {
                Data = PackageData.Substring(FirstCommaPos + 1, (PackageData.Length - (FirstCommaPos + 2)));
                //Data = Data.Substring(0, Data.IndexOf(','));
                UInt32 RecvID = UInt32.Parse(ID, System.Globalization.NumberStyles.HexNumber);
                if (RecvID == StartID + 1)
                {
                    try
                    {
                        CommaPos = Data.IndexOf(',');
                        MaxParamPageNumber = UInt16.Parse(Data.Substring(0, CommaPos));
                        Data = Data.Substring(CommaPos + 1, (Data.Length - CommaPos - 1));
                        CommaPos = Data.IndexOf(',');
                        MaxParamValueNumber = UInt16.Parse(Data.Substring(0, CommaPos));
                        Data = Data.Substring(CommaPos + 1, (Data.Length - CommaPos - 1));
                        CommaPos = Data.IndexOf(',');
                        MaxVarPageNumber = UInt16.Parse(Data.Substring(0, CommaPos));
                        Data = Data.Substring(CommaPos + 1, (Data.Length - CommaPos - 1));
                        MaxVarValueNumber = UInt16.Parse(Data);

                        string StartIDHex = StartID.ToString("X8");
                        string Answer = ",CNOK,";
                        byte[] BytesOfMessage = Encoding.ASCII.GetBytes(StartIDHex + Answer);
                        var HashValue = CRC32Mpeg2.ComputeHash(BytesOfMessage, 0, BytesOfMessage.Length);
                        string HashStringCalculated = BitConverter.ToString(HashValue).Replace("-", string.Empty).Substring(8, 8);
                        DeviceIsCorrect = true;
                        SetInfoLabelText();
                        SerialPortMain.Write(">" + StartIDHex + Answer + HashStringCalculated + "<");
                    }
                    catch
                    {

                    }




                }
                if ((RecvID >= StartID + 2) && (RecvID <= StartID + 51))
                {
                    UInt32 ParamPageNumber = RecvID - (StartID + 2);
                    for (i = 0; i < (Data.Length / 4); i++)
                    {
                        string Value = Data.Substring(4 * i, 4);
                        string HexValue = Value.Substring(2, 2) + Value.Substring(0, 2);
                        ParameterValues[(20 * ParamPageNumber) + i] = Int16.Parse(HexValue, System.Globalization.NumberStyles.HexNumber);
                        if (ParameterRequested)
                        {
                            if (ParameterValues[(20 * ParamPageNumber) + i] == Int16.Parse(HexValue, System.Globalization.NumberStyles.HexNumber))
                            {
                                ParameterValues[(20 * ParamPageNumber) + i] = Int16.Parse(HexValue, System.Globalization.NumberStyles.HexNumber);
                                ParameterValuesCopy[(20 * ParamPageNumber) + i] = ParameterValues[(20 * ParamPageNumber) + i];
                            }
                            else
                            {
                                ParameterValues[(20 * ParamPageNumber) + i] = Int16.Parse(HexValue, System.Globalization.NumberStyles.HexNumber);
                            }
                        }
                        else
                        {
                            ParameterValues[(20 * ParamPageNumber) + i] = Int16.Parse(HexValue, System.Globalization.NumberStyles.HexNumber);
                            ParameterValuesCopy[(20 * ParamPageNumber) + i] = ParameterValues[(20 * ParamPageNumber) + i];
                        }
                    }
                    if (ParamPageNumber == MaxParamPageNumber)
                    {
                        if (ParameterRequested)
                        {
                            int Length = (MaxParamPageNumber) * 20 + (MaxParamValueNumber + 1);
                            int k;
                            for (k = 0; k < Length; k++)
                            {
                                if (ParameterValues[k] != ParameterValuesCopy[k])
                                {

                                    break;
                                }
                            }
                            if (k == Length)
                            {
                                ParameterRequested = false;
                                MessageBox.Show(SplashScreen.LSParametersWereSuccessfullySent, SplashScreen.LSParametersWereSuccessfullySent, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                ParameterRequested = false;
                                MessageBox.Show(SplashScreen.LSParametersCouldNotBeSent, SplashScreen.LSParametersCouldNotBeSent, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        if (Data.Length / 4 == MaxParamValueNumber + 1)
                        {
                            ParamUpdate = true;
                        }
                    }
                }
                if ((RecvID >= StartID + 52) && (RecvID <= StartID + 101))
                {
                    UInt32 VarPageNumber = RecvID - (StartID + 52);
                    for (i = 0; i < (Data.Length / 4); i++)
                    {
                        string Value = Data.Substring(4 * i, 4);
                        string HexValue = Value.Substring(2, 2) + Value.Substring(0, 2);
                        VariableValues[(20 * VarPageNumber) + i] = Int16.Parse(HexValue, System.Globalization.NumberStyles.HexNumber);



                    }

                    VarUpdate = true; // Todo: Onur

                    if (VarPageNumber == MaxVarPageNumber)
                    {
                        if (Data.Length / 4 == MaxVarValueNumber + 1)
                        {
                            VarUpdate = true;

                        }
                    }

                }
            }
            else
            {
                UInt32 RecvID = UInt32.Parse(ID, System.Globalization.NumberStyles.HexNumber);
                if ((RecvID >= StartID + 2) && (RecvID <= StartID + 51))
                {
                    string StartIDHex = (StartID + 102).ToString("X8");
                    string Answer = "READ";
                    byte[] BytesOfMessage = Encoding.ASCII.GetBytes(StartIDHex + Answer);
                    var HashValue = CRC32Mpeg2.ComputeHash(BytesOfMessage, 0, BytesOfMessage.Length);
                    string HashStringCalculated = BitConverter.ToString(HashValue).Replace("-", string.Empty).Substring(8, 8);
                    SerialPortMain.Write(">" + StartIDHex + Answer + HashStringCalculated + "<");
                }
                else if (!DeviceIsCorrect)
                {
                    Connection_CNNT();
                }
            }


        }
        private void ReadMessages()
        {
            TPCANStatus stsResult;


            do
            {
                stsResult = m_IsFD ? ReadMessage() : ReadMessage();
                if (stsResult == TPCANStatus.PCAN_ERROR_ILLOPERATION)
                    break;
            } while (DisconnectButton.Enabled && (!Convert.ToBoolean(stsResult & TPCANStatus.PCAN_ERROR_QRCVEMPTY)));
        }

        private TPCANStatus ReadMessage()
        {
            TPCANMsg CANMsg;
            TPCANTimestamp CANTimeStamp;
            TPCANStatus stsResult;

            // We execute the "Read" function of the PCANBasic                
            //
            stsResult = PCANBasic.Read(m_PcanHandle, out CANMsg, out CANTimeStamp);
            if (stsResult != TPCANStatus.PCAN_ERROR_QRCVEMPTY)
                // We process the received message
                //
                ProcessMessage(CANMsg, CANTimeStamp);

            return stsResult;
        }
        private void ProcessMessage(TPCANMsg theMsg, TPCANTimestamp itsTimeStamp)
        {
            // Mesaj Idsine bakılır.
            if (theMsg.ID == StartID + 1)
            {
                var hashBytes = CRC16Arc.ComputeHash(theMsg.DATA, 0, 6);
                if ((hashBytes[6] == theMsg.DATA[7]) && (hashBytes[7] == theMsg.DATA[6]))
                {
                    MaxParamPageNumber = (UInt16)theMsg.DATA[0];
                    MaxParamValueNumber = (UInt16)theMsg.DATA[1];
                    MaxVarPageNumber = (UInt16)theMsg.DATA[2];
                    MaxVarValueNumber = (UInt16)theMsg.DATA[3];

                    Connection_CNOK();
                    DeviceIsCorrect = true;
                    SetInfoLabelText();


                }
            }
            // Parametrelerin alındığı Id
            else if (theMsg.ID == StartID + 2)
            {
                // Eğer Data 1.nci parametre ise öncekilerin CRCsi yollamıştır.
                if (theMsg.DATA[1] > 192)
                {
                    int Page = (int)theMsg.DATA[0];

                    int Count = (int)theMsg.DATA[1];

                    Count = Count - 192;

                    //Boyut Tutmuş mu?
                    if (Count == LastReceivedParamSize)
                    {
                        //Msg Crcsi tutmuş mu?
                        var hashBytes = CRC16Arc.ComputeHash(theMsg.DATA, 0, 6);
                        if ((hashBytes[6] == theMsg.DATA[7]) && (hashBytes[7] == theMsg.DATA[6]))
                        {
                            byte[] result = new byte[LastReceivedParamDatas.Length * sizeof(ushort)];
                            Buffer.BlockCopy(LastReceivedParamDatas, 0, result, 0, result.Length);
                            var hashBytes32 = CRC32Mpeg2.ComputeHash(result, 0, (2 * (Count)));
                            if ((hashBytes32[7] == theMsg.DATA[2]) && (hashBytes32[6] == theMsg.DATA[3]) && (hashBytes32[5] == theMsg.DATA[4]) && (hashBytes32[4] == theMsg.DATA[5]))
                            {
                                for (int i = 0; i < Count; i++)
                                {
                                    if (ParameterRequested)
                                    {
                                        if (ParameterValues[(20 * Page) + i] == LastReceivedParamDatas[i])
                                        {
                                            ParameterValues[(20 * Page) + i] = LastReceivedParamDatas[i];
                                            ParameterValuesCopy[(20 * Page) + i] = ParameterValues[(20 * Page) + i];
                                        }
                                        else
                                        {
                                            ParameterValues[(20 * Page) + i] = LastReceivedParamDatas[i];
                                        }
                                    }
                                    else
                                    {
                                        ParameterValues[(20 * Page) + i] = LastReceivedParamDatas[i];
                                        ParameterValuesCopy[(20 * Page) + i] = ParameterValues[(20 * Page) + i];
                                    }

                                }
                                ParamUpdate = true;
                                if (Page == MaxParamPageNumber)
                                {
                                    if (ParameterRequested)
                                    {
                                        int Length = (MaxParamPageNumber) * 20 + (MaxParamValueNumber + 1);
                                        int i = 0;
                                        for (i = 0; i < Length; i++)
                                        {
                                            if (ParameterValues[i] != ParameterValuesCopy[i])
                                            {

                                                break;
                                            }
                                        }
                                        if (i == Length)
                                        {
                                            ParameterRequested = false;
                                            MessageBox.Show(SplashScreen.LSParametersWereSuccessfullySent, SplashScreen.LSParametersWereSuccessfullySent, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            ParameterRequested = false;
                                            MessageBox.Show(SplashScreen.LSParametersCouldNotBeSent, SplashScreen.LSParametersCouldNotBeSent, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                // 1 tane data gelmiş.
                else if (theMsg.DATA[1] > 128)
                {
                    int Page = (int)theMsg.DATA[0];

                    int Count = (int)theMsg.DATA[1];

                    Count = Count - 128;


                    byte[] DataBytes = new byte[2];
                    DataBytes[0] = theMsg.DATA[2];
                    DataBytes[1] = theMsg.DATA[3];
                    short Data = BitConverter.ToInt16(DataBytes, 0);
                    LastReceivedParamDatas[Count] = Data;
                    Count++;



                    LastReceivedParamPage = Page;
                    LastReceivedParamSize = Count;
                }
                // 2 tane gelmiş.
                else if (theMsg.DATA[1] > 64)
                {
                    int Page = (int)theMsg.DATA[0];

                    int Count = (int)theMsg.DATA[1];
                    Count = Count - 64;

                    for (int i = 0; i < 2; i++)
                    {
                        byte[] DataBytes = new byte[2];
                        DataBytes[0] = theMsg.DATA[2 * i + 2];
                        DataBytes[1] = theMsg.DATA[2 * i + 3];
                        short Data = BitConverter.ToInt16(DataBytes, 0);
                        LastReceivedParamDatas[Count] = Data;
                        Count++;

                    }

                    LastReceivedParamPage = Page;
                    LastReceivedParamSize = Count;
                }
                // 3 tane gelmiş.
                else
                {
                    int Page = (int)theMsg.DATA[0];

                    int Count = (int)theMsg.DATA[1];

                    for (int i = 0; i < 3; i++)
                    {
                        byte[] DataBytes = new byte[2];
                        DataBytes[0] = theMsg.DATA[2 * i + 2];
                        DataBytes[1] = theMsg.DATA[2 * i + 3];
                        short Data = BitConverter.ToInt16(DataBytes, 0);
                        LastReceivedParamDatas[Count] = Data;
                        Count++;

                    }

                    LastReceivedParamPage = Page;
                    LastReceivedParamSize = Count;

                }

            }
            else if (theMsg.ID == StartID + 3)
            {
                // Eğer Data 1.nci parametre ise öncekilerin CRCsi yollamıştır.
                if (theMsg.DATA[1] > 192)
                {
                    int Page = (int)theMsg.DATA[0];

                    int Count = (int)theMsg.DATA[1];

                    Count = Count - 192;

                    //Boyut Tutmuş mu?
                    if (Count == LastReceivedVarSize)
                    {
                        //Msg Crcsi tutmuş mu?
                        var hashBytes = CRC16Arc.ComputeHash(theMsg.DATA, 0, 6);
                        if ((hashBytes[6] == theMsg.DATA[7]) && (hashBytes[7] == theMsg.DATA[6]))
                        {
                            byte[] result = new byte[LastReceivedVarDatas.Length * sizeof(ushort)];
                            Buffer.BlockCopy(LastReceivedVarDatas, 0, result, 0, result.Length);
                            var hashBytes32 = CRC32Mpeg2.ComputeHash(result, 0, (2 * (Count)));
                            if ((hashBytes32[7] == theMsg.DATA[2]) && (hashBytes32[6] == theMsg.DATA[3]) && (hashBytes32[5] == theMsg.DATA[4]) && (hashBytes32[4] == theMsg.DATA[5]))
                            {
                                for (int i = 0; i < Count; i++)
                                {
                                    VariableValues[(20 * Page) + i] = LastReceivedVarDatas[i];
                                }
                                VarUpdate = true;
                            }
                        }
                    }
                }
                // 1 tane data gelmiş.
                else if (theMsg.DATA[1] > 128)
                {
                    int Page = (int)theMsg.DATA[0];

                    int Count = (int)theMsg.DATA[1];

                    Count = Count - 128;


                    byte[] DataBytes = new byte[2];
                    DataBytes[0] = theMsg.DATA[2];
                    DataBytes[1] = theMsg.DATA[3];
                    short Data = BitConverter.ToInt16(DataBytes, 0);
                    LastReceivedVarDatas[Count] = Data;
                    Count++;



                    LastReceivedVarPage = Page;
                    LastReceivedVarSize = Count;
                }
                // 2 tane gelmiş.
                else if (theMsg.DATA[1] > 64)
                {
                    int Page = (int)theMsg.DATA[0];

                    int Count = (int)theMsg.DATA[1];
                    Count = Count - 64;

                    for (int i = 0; i < 2; i++)
                    {
                        byte[] DataBytes = new byte[2];
                        DataBytes[0] = theMsg.DATA[2 * i + 2];
                        DataBytes[1] = theMsg.DATA[2 * i + 3];
                        short Data = BitConverter.ToInt16(DataBytes, 0);
                        LastReceivedVarDatas[Count] = Data;
                        Count++;

                    }

                    LastReceivedVarPage = Page;
                    LastReceivedVarSize = Count;
                }
                // 3 tane gelmiş.
                else
                {
                    int Page = (int)theMsg.DATA[0];

                    int Count = (int)theMsg.DATA[1];

                    for (int i = 0; i < 3; i++)
                    {
                        byte[] DataBytes = new byte[2];
                        DataBytes[0] = theMsg.DATA[2 * i + 2];
                        DataBytes[1] = theMsg.DATA[2 * i + 3];
                        short Data = BitConverter.ToInt16(DataBytes, 0);
                        LastReceivedVarDatas[Count] = Data;
                        Count++;

                    }

                    LastReceivedVarPage = Page;
                    LastReceivedVarSize = Count;

                }
            }
        }



        //private void ProcessMessage(TPCANMsg theMsg, TPCANTimestamp itsTimeStamp)
        //{
        //    TPCANMsgFD newMsg;
        //    TPCANTimestampFD newTimestamp;

        //    newMsg = new TPCANMsgFD();
        //    newMsg.DATA = new byte[64];
        //    newMsg.ID = theMsg.ID;
        //    newMsg.DLC = theMsg.LEN;
        //    for (int i = 0; i < ((theMsg.LEN > 8) ? 8 : theMsg.LEN); i++)
        //        newMsg.DATA[i] = theMsg.DATA[i];
        //    newMsg.MSGTYPE = theMsg.MSGTYPE;






        //    //newTimestamp = Convert.ToUInt64(itsTimeStamp.micros + 1000 * itsTimeStamp.millis + 0x100000000 * 1000 * itsTimeStamp.millis_overflow);
        //    //ProcessMessage(newMsg, newTimestamp);
        //}

        //private void ProcessMessage(TPCANMsgFD theMsg, TPCANTimestampFD itsTimeStamp)
        //{
        //    // We search if a message (Same ID and Type) is 
        //    // already received or if this is a new message
        //    //

        //}
        #endregion

        private void btnHwRefresh_Click(object sender, EventArgs e)
        {
            TPCANStatus stsResult;
            uint iChannelsCount;
            bool bIsFD;

            // Clears the Channel comboBox and fill it again with 
            // the PCAN-Basic handles for no-Plug&Play hardware and
            // the detected Plug&Play hardware
            //
            cbbChannel.Items.Clear();
            try
            {
                // Includes all no-Plug&Play Handles
                //for (int i = 0; i < m_NonPnPHandles.Length; i++)
                //  cbbChannel.Items.Add(FormatChannelName(m_NonPnPHandles[i]));

                // Checks for available Plug&Play channels
                //
                stsResult = PCANBasic.GetValue(PCANBasic.PCAN_NONEBUS, TPCANParameter.PCAN_ATTACHED_CHANNELS_COUNT, out iChannelsCount, sizeof(uint));
                if (stsResult == TPCANStatus.PCAN_ERROR_OK)
                {
                    TPCANChannelInformation[] info = new TPCANChannelInformation[iChannelsCount];

                    stsResult = PCANBasic.GetValue(PCANBasic.PCAN_NONEBUS, TPCANParameter.PCAN_ATTACHED_CHANNELS, info);
                    if (stsResult == TPCANStatus.PCAN_ERROR_OK)
                        // Include only connectable channels
                        //
                        foreach (TPCANChannelInformation channel in info)
                            if ((channel.channel_condition & PCANBasic.PCAN_CHANNEL_AVAILABLE) == PCANBasic.PCAN_CHANNEL_AVAILABLE)
                            {
                                bIsFD = (channel.device_features & PCANBasic.FEATURE_FD_CAPABLE) == PCANBasic.FEATURE_FD_CAPABLE;
                                cbbChannel.Items.Add(FormatChannelName(channel.channel_handle, bIsFD));
                            }
                }

                if (cbbChannel.Items.IndexOf(SplashScreen.SelectedCANDeviceName) == -1)
                {
                    SplashScreen.SelectedCANDeviceName = String.Empty;
                }
                else
                {
                    cbbChannel.SelectedIndex = cbbChannel.Items.IndexOf(SplashScreen.SelectedCANDeviceName);
                }
                if (SplashScreen.SelectedCANBaudrateIndex > -1)
                {
                    cbbBaudrates.SelectedIndex = SplashScreen.SelectedCANBaudrateIndex;
                }
                cbbChannel.SelectedIndex = cbbChannel.Items.Count - 1;
                if (stsResult != TPCANStatus.PCAN_ERROR_OK)
                    MessageBox.Show(GetFormatedError(stsResult));
            }
            catch (DllNotFoundException)
            {
                MessageBox.Show("Unable to find the library: PCANBasic.dll !", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
        }
        private string FormatChannelName(TPCANHandle handle)
        {
            return FormatChannelName(handle, false);
        }
        private string FormatChannelName(TPCANHandle handle, bool isFD)
        {
            TPCANDevice devDevice;
            byte byChannel;

            // Gets the owner device and channel for a 
            // PCAN-Basic handle
            //
            if (handle < 0x100)
            {
                devDevice = (TPCANDevice)(handle >> 4);
                byChannel = (byte)(handle & 0xF);
            }
            else
            {
                devDevice = (TPCANDevice)(handle >> 8);
                byChannel = (byte)(handle & 0xFF);
            }

            // Constructs the PCAN-Basic Channel name and return it
            //
            if (isFD)
                return string.Format("{0}:FD {1} ({2:X2}h)", devDevice, byChannel, handle);
            else
                return string.Format("{0} {1} ({2:X2}h)", devDevice, byChannel, handle);
        }
        private string GetFormatedError(TPCANStatus error)
        {
            StringBuilder strTemp;

            // Creates a buffer big enough for a error-text
            //
            strTemp = new StringBuilder(256);
            // Gets the text using the GetErrorText API function
            // If the function success, the translated error is returned. If it fails,
            // a text describing the current error is returned.
            //
            if (PCANBasic.GetErrorText(error, 0, strTemp) != TPCANStatus.PCAN_ERROR_OK)
                return string.Format("An error occurred. Error-code's text ({0:X}) couldn't be retrieved", error);
            else
                return strTemp.ToString();
        }

        private void openConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var OFD = new OpenFileDialog();
            OFD.Filter = SplashScreen.LSConfigFileType + "|*.cehr";
            if (File.Exists(SplashScreen.LastConfigFile))
            {
                OFD.InitialDirectory = SplashScreen.LastConfigFile;
            }
            OFD.Title = SplashScreen.LSOpenConfigFile + "...";
            DialogResult dialogResult = OFD.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                SplashScreen.LastConfigFile = OFD.FileName;
                IniFile cehr = new IniFile(OFD.FileName);
                parametersToolStripMenuItem.DropDownItems.Clear();
                variablesToolStripMenuItem.DropDownItems.Clear();
                VarShortcutsLoaded = false;
                ParShortcutsLoaded = false;

                for (int i = 1; i <= 1000; i++)
                {
                    ParameterNames[i - 1] = cehr.Read("ParameterNames", "P" + i.ToString());
                    VariableNames[i - 1] = cehr.Read("VariableNames", "V" + i.ToString());
                }

                string ParShortcuts = cehr.Read("Shortcuts", "ParameterShortcutPageNos");
                if (ParShortcuts != string.Empty)
                {
                    int[] PageNos = new int[50];
                    int index = 0;
                    string text = string.Empty;
                    foreach (char ch in ParShortcuts)
                    {
                        if (ch == ',')
                        {
                            PageNos[index] = Int32.Parse(text);
                            text = string.Empty;
                            index++;
                        }
                        else
                        {
                            text += ch;
                        }
                    }

                    PageNos[index] = Int32.Parse(text);
                    index++;

                    ToolStripMenuItem[] items = new ToolStripMenuItem[index];
                    for (int i = 0; i < index; i++)
                    {

                        items[i] = new ToolStripMenuItem();
                        items[i].Name = PageNos[i].ToString();
                        items[i].Tag = "";
                        items[i].Text = SplashScreen.LSPage + " " + PageNos[i].ToString();
                        items[i].Click += new EventHandler(MenuParItemClickHandler);
                        ParShortcutsLoaded = true;

                    }
                    parametersToolStripMenuItem.DropDownItems.AddRange(items);
                }


                string VarShortcuts = cehr.Read("Shortcuts", "VariableShortcutPageNos");
                if (VarShortcuts != string.Empty)
                {
                    int[] PageNos = new int[50];
                    int index = 0;
                    string text = string.Empty;
                    foreach (char ch in VarShortcuts)
                    {
                        if (ch == ',')
                        {
                            PageNos[index] = Int32.Parse(text);
                            text = string.Empty;
                            index++;
                        }
                        else
                        {
                            text += ch;
                        }
                    }

                    PageNos[index] = Int32.Parse(text);
                    index++;

                    ToolStripMenuItem[] items2 = new ToolStripMenuItem[index];
                    for (int i = 0; i < index; i++)
                    {
                        items2[i] = new ToolStripMenuItem();
                        items2[i].Name = PageNos[i].ToString();
                        items2[i].Tag = "";
                        items2[i].Text = SplashScreen.LSPage + " " + PageNos[i].ToString();
                        items2[i].Click += new EventHandler(MenuVarItemClickHandler);
                        VarShortcutsLoaded = true;
                    }
                    variablesToolStripMenuItem.DropDownItems.AddRange(items2);
                }
            }




        }

        private void parametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ParShortcutsLoaded)
            {
                int i = 0;
                for (i = 0; i < 4; i++)
                {

                    if (IsLocationFilled[i] == false)
                    {
                        IsLocationFilled[i] = true;
                        break;
                    }
                }
                if ((i != 4))
                {
                    ParametresForm ParamForm = new ParametresForm() { Owner = this };
                    ParamForm.Location = SetLocation(i, ParamForm.Width, ParamForm.Height);
                    ParamForm.MdiParent = this;


                    int PageNo = 1;
                    ParamForm.Text = SplashScreen.LSParameters + " " + SplashScreen.LSPage + " 1";
                    foreach (Label label in ParamForm.Controls.OfType<Label>())
                    {
                        string Name = label.Name;
                        if ((Name != "PageNo") && (Name != "LocationIndex"))
                        {
                            Name = Name.Substring(5, (Name.Length - 5));
                            int ValueNo = Int32.Parse(Name);
                            label.Text = SetLabelText(PageNo, ValueNo);
                        }

                        else
                        {
                            if (Name == "PageNo")
                            {
                                label.Text = "1";
                            }
                            else
                            {
                                label.Text = i.ToString();
                            }
                        }


                    }

                    ParamForm.ShowIcon = false;
                    ParamForm.ShowInTaskbar = false;
                    ParamForm.SendToBack();


                    ParamForm.Focus();
                    ParamForm.Show();
                    this.Activate();
                }

            }

        }

        private void SettingsControlTimer_Tick(object sender, EventArgs e)
        {

            if (SettingsFormDisplayed)
            {
                if (RS232Checked)
                {
                    CPRefreshButton_Click(sender, e);
                    SetForm.DeviceSelectComboBox.Items.Clear();
                    SetForm.BaudrateComboBox.Items.Clear();
                    SetForm.DeviceSelectComboBox.Items.AddRange(ComportNamesComboBox.Items.Cast<Object>().ToArray());
                    SetForm.BaudrateComboBox.Items.AddRange(BaudrateComboBox.Items.Cast<Object>().ToArray());
                    SetForm.BaudrateComboBox.SelectedIndex = SplashScreen.SelectedCPBaudrateIndex;
                    if (SetForm.DeviceSelectComboBox.Items.IndexOf(SplashScreen.SelectedComportName) > -1)
                    {
                        SetForm.DeviceSelectComboBox.SelectedIndex = SetForm.DeviceSelectComboBox.Items.IndexOf(SplashScreen.SelectedComportName);
                    }
                    RS232Checked = false;

                }
                else if (USBCANChecked)
                {
                    btnHwRefresh_Click(sender, e);
                    SetForm.DeviceSelectComboBox.Items.Clear();
                    SetForm.BaudrateComboBox.Items.Clear();

                    SetForm.DeviceSelectComboBox.Items.AddRange(cbbChannel.Items.Cast<Object>().ToArray());
                    SetForm.BaudrateComboBox.Items.AddRange(cbbBaudrates.Items.Cast<Object>().ToArray());

                    SetForm.BaudrateComboBox.SelectedIndex = SplashScreen.SelectedCANBaudrateIndex;
                    if (SetForm.DeviceSelectComboBox.Items.IndexOf(SplashScreen.SelectedCANDeviceName) > -1)
                    {
                        SetForm.DeviceSelectComboBox.SelectedIndex = SetForm.DeviceSelectComboBox.Items.IndexOf(SplashScreen.SelectedCANDeviceName);
                    }

                    USBCANChecked = false;
                }

                if (SettingsRefreshClicked)
                {
                    if (SetForm.RS232RButton.Checked)
                    {
                        RS232Checked = true;
                    }
                    else
                    {
                        USBCANChecked = true;
                    }
                    SettingsRefreshClicked = false;
                }
            }
            else
            {
                if (LanguageChanged)
                {
                    SetLanguage();
                    LanguageChanged = false;
                }
                if (SplashScreen.HardwareType == 0)
                {

                    if (ComportNamesComboBox.Items.Count > 0)
                    {
                        ComportNamesComboBox.SelectedIndex = SplashScreen.SelectedCPDeviceIndex;
                        BaudrateComboBox.SelectedIndex = SplashScreen.SelectedCPBaudrateIndex;


                    }



                }
                else
                {
                    if (cbbChannel.Items.Count > 0)
                    {
                        cbbChannel.SelectedIndex = SplashScreen.SelectedCANDeviceIndex;
                        cbbBaudrates.SelectedIndex = SplashScreen.SelectedCANBaudrateIndex;
                    }

                }
                SetInfoLabelText();
                SettingsControlTimer.Enabled = false;
                StartID = SplashScreen.StartID;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void variablesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (!VarShortcutsLoaded)
            {

                int i = 0;
                for (i = 0; i < 4; i++)
                {

                    if (IsLocationFilled[i] == false)
                    {
                        IsLocationFilled[i] = true;
                        break;
                    }
                }
                if ((i != 4))
                {
                    VariablesForm VarForm = new VariablesForm() { Owner = this };
                    VarForm.MdiParent = this;
                    VarForm.Location = SetLocation(i, VarForm.Width, VarForm.Height);
                    int PageNo = 1;
                    VarForm.Text = SplashScreen.LSVariables + " " + SplashScreen.LSPage + " 1";
                    foreach (Label label in VarForm.Controls.OfType<Label>())
                    {
                        string Name = label.Name;
                        if ((Name != "PageNo") && (Name != "LocationIndex"))
                        {
                            Name = Name.Substring(3, (Name.Length - 3));
                            int ValueNo = Int32.Parse(Name);
                            label.Text = SetLabelText(PageNo, ValueNo);
                        }
                        else
                        {
                            if (Name == "PageNo")
                            {
                                label.Text = "1";
                            }
                            else
                            {
                                label.Text = i.ToString();
                            }
                        }


                    }

                    VarForm.ShowIcon = false;
                    VarForm.ShowInTaskbar = false;
                    VarForm.SendToBack();


                    VarForm.Focus();
                    VarForm.Show();
                    this.Activate();
                }

            }

        }

        private void CPRefreshButton_Click(object sender, EventArgs e)
        {

            string[] AvailablePorts = SerialPort.GetPortNames();
            ComportNamesComboBox.Items.Clear();
            ComportNamesComboBox.Items.AddRange(AvailablePorts);
            if (ComportNamesComboBox.Items.IndexOf(SplashScreen.SelectedComportName) == -1)
            {
                SplashScreen.SelectedComportName = String.Empty;
            }
            else
            {
                ComportNamesComboBox.SelectedIndex = ComportNamesComboBox.Items.IndexOf(SplashScreen.SelectedComportName);
            }
            if (SplashScreen.SelectedCPBaudrateIndex > -1)
            {
                BaudrateComboBox.SelectedIndex = SplashScreen.SelectedCPBaudrateIndex;
            }
            if (ComportNamesComboBox.Items.Count > 0)
            {
                ComportNamesComboBox.SelectedIndex = ComportNamesComboBox.Items.Count - 1;
            }


        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            if (DeviceIsCorrect)
            {
                if (!DeviceGetError)
                {
                    Connection_DCNT();
                }
                if (LoadedDataFile)
                {
                    IniFile dehr = new IniFile(SplashScreen.LastDataFile);
                    for (int i = 1; i <= 1000; i++)
                    {
                        string Data = dehr.Read("ParameterValues", "P" + i.ToString());
                        if (Data != string.Empty)
                        {
                            ParameterValues[i - 1] = Int16.Parse(Data);
                            ParameterValuesCopy[i - 1] = ParameterValues[i - 1];
                        }
                        else
                        {
                            ParameterValues[i - 1] = 0;
                            ParameterValuesCopy[i - 1] = ParameterValues[i - 1];
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        ParameterValues[i] = 0;
                        VariableValues[i] = 0;
                    }
                }

                ParamUpdate = true;
                VarUpdate = true;
            }

            if (SplashScreen.HardwareType == 0)
            {
                if (Connected)
                {

                    SerialPortMain.Close();
                    if (!SerialPortMain.IsOpen)
                    {
                        Connected = false;
                        ConnectButton.Enabled = true;
                        DisconnectButton.Enabled = false;
                        ReceiveDataTimer.Enabled = false;
                        DeviceIsCorrect = false;
                        SetInfoLabelText();
                        SetFormIsLocked(false);
                        panel1.Refresh();

                    }
                }
            }
            else
            {

                PCANBasic.Uninitialize(m_PcanHandle);
                ReceiveDataTimer.Enabled = false;
                if (m_ReadThread != null)
                {
                    m_ReadThread.Abort();
                    m_ReadThread.Join();
                    m_ReadThread = null;
                }

                // Sets the connection status of the main-form
                //
                DeviceIsCorrect = false;
                SetConnectionStatus(false);

            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (SplashScreen.HardwareType == 0)
            {
                if (!Connected)
                {
                    try
                    {
                        if (ComportNamesComboBox.SelectedIndex > -1)
                        {
                            SerialPortMain.PortName = ComportNamesComboBox.SelectedItem.ToString();
                            SerialPortMain.BaudRate = Int32.Parse(BaudrateComboBox.SelectedItem.ToString());

                            SerialPortMain.Open();
                            if (SerialPortMain.IsOpen)
                            {
                                Connected = true;
                                ConnectButton.Enabled = false;
                                DisconnectButton.Enabled = true;
                                ReceiveDataTimer.Enabled = true;
                                SetInfoLabelText();
                                SetFormIsLocked(true);
                                panel1.Refresh();
                                Connection_CNNT();
                            }
                        }
                        else
                        {
                            MessageBox.Show(SplashScreen.LSGotoSettingsAndSelectDevice + "\n" + SplashScreen.LSSelectedDeviceIsDisplayedInTheLowerLeft, SplashScreen.LSNoDeviceSelection, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    catch
                    {

                    }

                }
            }
            else
            {
                TPCANStatus stsResult;

                // Connects a selected PCAN-Basic channel
                //


                stsResult = PCANBasic.Initialize(m_PcanHandle, m_Baudrate, m_HwType, 100, 3);

                if (stsResult != TPCANStatus.PCAN_ERROR_OK)
                {
                    MessageBox.Show(SplashScreen.LSGotoSettingsAndSelectDevice + "\n" + SplashScreen.LSSelectedDeviceIsDisplayedInTheLowerLeft, SplashScreen.LSNoDeviceSelection, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SetConnectionStatus(stsResult == TPCANStatus.PCAN_ERROR_OK);
                }


            }
        }

        /*private void groupBox1_SizeChanged(object sender, EventArgs e)
        {
            //int width = groupBox1.Width;
            //int height = groupBox1.Height;
            
        }*/

        private void SetConnectionStatus(bool bConnected)
        {
            // Buttons
            //

            SetFormIsLocked(bConnected);
            ConnectButton.Enabled = !bConnected;
            Connected = bConnected;
            SetInfoLabelText();
            panel1.Refresh();
            //btnRead.Enabled = bConnected && rdbManual.Checked;
            //btnWrite.Enabled = bConnected;
            DisconnectButton.Enabled = bConnected;
            //btnFilterApply.Enabled = bConnected;
            //btnFilterQuery.Enabled = bConnected;
            //btnGetVersions.Enabled = bConnected;
            btnHwRefresh.Enabled = !bConnected;
            //btnStatus.Enabled = bConnected;
            //btnReset.Enabled = bConnected;
            ReceiveDataTimer.Enabled = bConnected;
            // ComboBoxs
            //
            cbbChannel.Enabled = !bConnected;
            cbbBaudrates.Enabled = !bConnected;
            //cbbHwType.Enabled = !bConnected;
            //cbbIO.Enabled = !bConnected;
            //cbbInterrupt.Enabled = !bConnected;

            // Check-Buttons
            //
            //chbCanFD.Enabled = !bConnected;

            // Hardware configuration and read mode
            //
            //if (!bConnected)
            //    cbbChannel_SelectedIndexChanged(this, new EventArgs());
            //else
            //    rdbTimer_CheckedChanged(this, new EventArgs());

            //// Display messages in grid
            ////
            //tmrDisplay.Enabled = bConnected;
            if (bConnected)
            {

                Connection_CNNT();
                // The message was successfully sent
                //
                //if (stsResult == TPCANStatus.PCAN_ERROR_OK)
                //    IncludeTextMessage("Message was successfully SENT");
                //// An error occurred.  We show the error.
                ////			
                //else
                //    MessageBox.Show(GetFormatedError(stsResult));
            }
        }

        private void openDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = SplashScreen.LSDataFileType + "|*.dehr";
            if (File.Exists(SplashScreen.LastDataFile))
            {

                OFD.InitialDirectory = Path.GetDirectoryName(SplashScreen.LastDataFile);
            }

            OFD.Title = SplashScreen.LSOpenDataFile + "...";
            DialogResult dialogResult = OFD.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                Array.Clear(ParameterValues, 0, ParameterValues.Length);

                IniFile dehr = new IniFile(OFD.FileName);
                for (int i = 1; i <= 1000; i++)
                {
                    string Data = dehr.Read("ParameterValues", "P" + i.ToString());
                    if (Data != string.Empty)
                    {
                        ParameterValues[i - 1] = Int16.Parse(Data);
                        ParameterValuesCopy[i - 1] = ParameterValues[i - 1];
                    }

                }
                SplashScreen.LastDataFile = OFD.FileName;
                LoadedDataFile = true;
                ParamUpdate = true;
            }





        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (Connected)
            {
                if (!DeviceGetError)
                {
                    if (TurnOnLed)
                    {
                        e.Graphics.DrawImage(White, 4, 4);
                    }
                    else
                    {
                        e.Graphics.DrawImage(Green, 4, 4);
                    }
                }
                else
                {
                    e.Graphics.DrawImage(Red, 4, 4);
                }


            }
            else
            {
                if (DeviceGetError)
                {
                    e.Graphics.DrawImage(Red, 4, 4);
                }
                else
                {
                    e.Graphics.DrawImage(White, 4, 4);
                }

            }
        }

        private void saveDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Filter = SplashScreen.LSDataFileType + "|*.dehr";
            if (File.Exists(SplashScreen.LastDataFile))
            {
                SFD.InitialDirectory = Path.GetDirectoryName(SplashScreen.LastDataFile);
            }
            SFD.Title = SplashScreen.LSSaveDataFile + "...";
            SFD.OverwritePrompt = true;

            DialogResult dialogResult = SFD.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                IniFile dehr = new IniFile(SFD.FileName);
                for (int i = 1; i <= 1000; i++)
                {
                    if (ParameterValues[i - 1] != 0)
                    {
                        dehr.Write("ParameterValues", "P" + i.ToString(), ParameterValues[i - 1].ToString());
                        ParameterValuesCopy[i - 1] = ParameterValues[i - 1];
                    }
                }
                SplashScreen.LastDataFile = SFD.FileName;
                ParamUpdate = true;

            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm.ShowDialog();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            CurrentWidth = this.Width;
            CurrentHeight = this.Height;
            IsSizeChanged = true;
        }

        private void ParamSendTimer_Tick(object sender, EventArgs e)
        {
            if (Connected)
            {
                if (SplashScreen.HardwareType == 0)
                {
                    if (!SerialPortMain.IsOpen)
                    {
                        DeviceGetError = true;
                        DisconnectButton_Click(sender, e);
                        panel1.Refresh();
                        if (MessageBox.Show(SplashScreen.LSDeviceDisconnected, SplashScreen.LSDeviceDisconnected, MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                        {
                            DeviceGetError = false;
                            panel1.Refresh();
                        }


                    }
                }
                else
                {
                    TPCANStatus stsResult;
                    String errorName;

                    // Gets the current BUS status of a PCAN Channel.
                    //
                    stsResult = PCANBasic.GetStatus(m_PcanHandle);

                    // Switch On Error Name
                    //
                    switch (stsResult)
                    {
                        case TPCANStatus.PCAN_ERROR_INITIALIZE:
                            errorName = "PCAN_ERROR_INITIALIZE";
                            break;

                        case TPCANStatus.PCAN_ERROR_BUSLIGHT:
                            errorName = "PCAN_ERROR_BUSLIGHT";
                            break;

                        case TPCANStatus.PCAN_ERROR_BUSHEAVY: // TPCANStatus.PCAN_ERROR_BUSWARNING
                            errorName = m_IsFD ? "PCAN_ERROR_BUSWARNING" : "PCAN_ERROR_BUSHEAVY";
                            break;

                        case TPCANStatus.PCAN_ERROR_BUSPASSIVE:
                            errorName = "PCAN_ERROR_BUSPASSIVE";
                            break;

                        case TPCANStatus.PCAN_ERROR_BUSOFF:
                            errorName = "PCAN_ERROR_BUSOFF";
                            break;

                        case TPCANStatus.PCAN_ERROR_OK:
                            errorName = "PCAN_ERROR_OK";
                            break;

                        default:
                            errorName = "Error";
                            break;
                    }
                    if (errorName == "Error")
                    {
                        DeviceGetError = true;
                        DisconnectButton_Click(sender, e);
                        panel1.Refresh();
                        if (MessageBox.Show(SplashScreen.LSDeviceDisconnected, SplashScreen.LSDeviceDisconnected, MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                        {
                            DeviceGetError = false;
                            panel1.Refresh();
                        }
                    }
                }

                if ((!DeviceGetError) && (ParamUpdate || VarUpdate))
                {
                    if (TurnOnLed)
                    {

                        panel1.Refresh();
                        TurnOnLed = false;
                    }
                    else
                    {
                        panel1.Refresh();
                        TurnOnLed = true;

                    }

                }

            }
            if (SendToDeviceIsClicked)
            {
                ParameterRequested = true;
                SendParam();
            }
            if (ReadParamFromDevice)
            {
                Connection_READ();
                ReadParamFromDevice = false;
            }




        }

        private void cbbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bNonPnP;
            string strTemp;

            // Get the handle fromt he text being shown
            //
            strTemp = cbbChannel.Text;
            strTemp = strTemp.Substring(strTemp.IndexOf('(') + 1, 3);

            strTemp = strTemp.Replace('h', ' ').Trim(' ');

            // Determines if the handle belong to a No Plug&Play hardware 
            //
            m_PcanHandle = Convert.ToUInt16(strTemp, 16);
            bNonPnP = m_PcanHandle <= PCANBasic.PCAN_DNGBUS1;
            // Activates/deactivates configuration controls according with the 
            // kind of hardware
            //
            //cbbHwType.Enabled = bNonPnP;
            //cbbIO.Enabled = bNonPnP;
            //cbbInterrupt.Enabled = bNonPnP;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!ConnectButton.Enabled)
            {
                if (DeviceIsCorrect)
                {
                    //Cihazın bilgi basmasını durdurup çıksın
                    DisconnectButton_Click(sender, e);
                }

            }
        }

        private void cbbBaudrates_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbbBaudrates.SelectedIndex)
            {
                case 0:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_1M;
                    break;
                case 1:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_800K;
                    break;
                case 2:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_500K;
                    break;
                case 3:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_250K;
                    break;
                case 4:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_125K;
                    break;
                case 5:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_100K;
                    break;
                case 6:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_95K;
                    break;
                case 7:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_83K;
                    break;
                case 8:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_50K;
                    break;
                case 9:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_47K;
                    break;
                case 10:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_33K;
                    break;
                case 11:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_20K;
                    break;
                case 12:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_10K;
                    break;
                case 13:
                    m_Baudrate = TPCANBaudrate.PCAN_BAUD_5K;
                    break;
            }
        }

        private void ReceiveDataTimer_Tick(object sender, EventArgs e)
        {
            ReceiveDataTimer.Enabled = false;
            if (SplashScreen.HardwareType == 0)
            {
                ReadSeriPort();
            }
            else
            {
                ReadMessages();
            }
            ReceiveDataTimer.Enabled = true;
        }

        private void Connection_CNNT()
        {
            if (SplashScreen.HardwareType == 0)
            {
                string StartIDHex = StartID.ToString("X8");
                string Data = ",CNNT,";
                byte[] Bytes = Encoding.ASCII.GetBytes(StartIDHex + Data);
                var HashValue = CRC32Mpeg2.ComputeHash(Bytes, 0, Bytes.Length);
                string HashString = BitConverter.ToString(HashValue).Replace("-", string.Empty).Substring(8, 8);


                SerialPortMain.Write(">" + StartIDHex + Data + HashString + "<");
            }
            else
            {
                TPCANStatus stsResult;
                TPCANMsg CANMsg;
                CANMsg = new TPCANMsg();
                CANMsg.DATA = new byte[8];
                CANMsg.ID = StartID;
                CANMsg.LEN = 8;
                CANMsg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD;


                CANMsg.DATA[0] = Convert.ToByte('C');
                CANMsg.DATA[1] = Convert.ToByte('N');
                CANMsg.DATA[2] = Convert.ToByte('N');
                CANMsg.DATA[3] = Convert.ToByte('T');
                CANMsg.DATA[4] = Convert.ToByte(null);
                CANMsg.DATA[5] = Convert.ToByte(null);

                var hash = CRC16Arc.ComputeHash(CANMsg.DATA, 0, 6);
                CANMsg.DATA[6] = hash[7];
                CANMsg.DATA[7] = hash[6];


                // The message is sent to the configured hardware
                //
                stsResult = PCANBasic.Write(m_PcanHandle, ref CANMsg);
            }


        }
        private void Connection_CNOK()
        {
            TPCANStatus stsResult;
            TPCANMsg CANMsg;
            CANMsg = new TPCANMsg();
            CANMsg.DATA = new byte[8];
            CANMsg.ID = StartID;
            CANMsg.LEN = 8;
            CANMsg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD;


            CANMsg.DATA[0] = Convert.ToByte('C');
            CANMsg.DATA[1] = Convert.ToByte('N');
            CANMsg.DATA[2] = Convert.ToByte('O');
            CANMsg.DATA[3] = Convert.ToByte('K');
            CANMsg.DATA[4] = Convert.ToByte(null);
            CANMsg.DATA[5] = Convert.ToByte(null);

            var hash = CRC16Arc.ComputeHash(CANMsg.DATA, 0, 6);
            CANMsg.DATA[6] = hash[7];
            CANMsg.DATA[7] = hash[6];

            stsResult = PCANBasic.Write(m_PcanHandle, ref CANMsg);


        }
        private void Connection_DCNT()
        {
            if (SplashScreen.HardwareType == 0)
            {
                string StartIDHex = StartID.ToString("X8");
                string Data = ",DCNT,";
                byte[] Bytes = Encoding.ASCII.GetBytes(StartIDHex + Data);
                var HashValue = CRC32Mpeg2.ComputeHash(Bytes, 0, Bytes.Length);
                string HashString = BitConverter.ToString(HashValue).Replace("-", string.Empty).Substring(8, 8);


                SerialPortMain.Write(">" + StartIDHex + Data + HashString + "<");
            }
            else
            {
                TPCANStatus stsResult;
                TPCANMsg CANMsg;
                CANMsg = new TPCANMsg();
                CANMsg.DATA = new byte[8];
                CANMsg.ID = StartID;
                CANMsg.LEN = 8;
                CANMsg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD;


                CANMsg.DATA[0] = Convert.ToByte('D');
                CANMsg.DATA[1] = Convert.ToByte('C');
                CANMsg.DATA[2] = Convert.ToByte('N');
                CANMsg.DATA[3] = Convert.ToByte('T');
                CANMsg.DATA[4] = Convert.ToByte(null);
                CANMsg.DATA[5] = Convert.ToByte(null);

                var hash = CRC16Arc.ComputeHash(CANMsg.DATA, 0, 6);
                CANMsg.DATA[6] = hash[7];
                CANMsg.DATA[7] = hash[6];


                // The message is sent to the configured hardware
                //
                stsResult = PCANBasic.Write(m_PcanHandle, ref CANMsg);
            }


        }
        private void Connection_READ()
        {
            if (SplashScreen.HardwareType == 0)
            {
                string StartIDHex = StartID.ToString("X8");
                string Data = ",READ,";
                byte[] Bytes = Encoding.ASCII.GetBytes(StartIDHex + Data);
                var HashValue = CRC32Mpeg2.ComputeHash(Bytes, 0, Bytes.Length);
                string HashString = BitConverter.ToString(HashValue).Replace("-", string.Empty).Substring(8, 8);


                SerialPortMain.Write(">" + StartIDHex + Data + HashString + "<");
            }
            else
            {
                TPCANStatus stsResult;
                TPCANMsg CANMsg;
                CANMsg = new TPCANMsg();
                CANMsg.DATA = new byte[8];
                CANMsg.ID = StartID;
                CANMsg.LEN = 8;
                CANMsg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD;


                CANMsg.DATA[0] = Convert.ToByte('R');
                CANMsg.DATA[1] = Convert.ToByte('E');
                CANMsg.DATA[2] = Convert.ToByte('A');
                CANMsg.DATA[3] = Convert.ToByte('D');
                CANMsg.DATA[4] = Convert.ToByte(null);
                CANMsg.DATA[5] = Convert.ToByte(null);

                var hash = CRC16Arc.ComputeHash(CANMsg.DATA, 0, 6);
                CANMsg.DATA[6] = hash[7];
                CANMsg.DATA[7] = hash[6];


                // The message is sent to the configured hardware
                //
                stsResult = PCANBasic.Write(m_PcanHandle, ref CANMsg);
            }


        }
        private void SetLanguage()
        {
            SetForm.HardwareTypeGroupBox.Text = SplashScreen.LSHardwareType;
            SetForm.DevicePreferencesGroupBox.Text = SplashScreen.LSDevicePreferences;
            SetForm.DeviceLabel.Text = SplashScreen.LSDevice + " :";
            SetForm.LanguageLabel.Text = SplashScreen.LSLanguage + " :";
            SetForm.StartIDLabel.Text = SplashScreen.LSStartID + " :";
            SetForm.OKButton.Text = SplashScreen.LSOK;
            SetForm.SettingsCancelButton.Text = SplashScreen.LSCancel;
            SetForm.RefreshButton.Text = SplashScreen.LSRefresh;



            //Menülerin dil dosyaları//

            fileToolStripMenuItem.Text = SplashScreen.LSFile;
            openDataFileToolStripMenuItem.Text = SplashScreen.LSOpenDataFile;
            saveDataFileToolStripMenuItem.Text = SplashScreen.LSSaveDataFile;
            openConfigFileToolStripMenuItem.Text = SplashScreen.LSOpenConfigFile;
            parametersToolStripMenuItem.Text = SplashScreen.LSParameters;
            variablesToolStripMenuItem.Text = SplashScreen.LSVariables;
            aboutToolStripMenuItem.Text = SplashScreen.LSAbout;
            settingsToolStripMenuItem.Text = SplashScreen.LSSettings;
            ConnectButton.Text = SplashScreen.LSConnect;
            DisconnectButton.Text = SplashScreen.LSDisconnect;


            SetInfoLabelText();

        }
        private void SetFormIsLocked(bool LockOption)
        {
            foreach (Control ctrl in SetForm.Controls.OfType<Control>())
            {
                if ((ctrl.Name != "SettingsCancelButton") && (ctrl.Name != "OKButton"))
                {
                    ctrl.Enabled = !LockOption;
                }
            }
        }
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        private void SetInfoLabelText()
        {
            if (!ConnectButton.Enabled)
            {
                if (SplashScreen.HardwareType == 0)
                {
                    try
                    {
                        InformationLabel.Text = SplashScreen.LSStatus + " : " + SplashScreen.LSOnline + " | " + SplashScreen.LSHardwareType + " : RS232" + " | " + SplashScreen.LSDevice + " : " + ComportNamesComboBox.SelectedItem.ToString() + " | Baudrate : " + BaudrateComboBox.SelectedItem.ToString();
                    }
                    catch
                    {
                        InformationLabel.Text = SplashScreen.LSStatus + " : " + SplashScreen.LSOnline + " | " + SplashScreen.LSHardwareType + " : RS232" + " | Baudrate : " + BaudrateComboBox.SelectedItem.ToString();
                    }



                }
                else
                {
                    try
                    {
                        InformationLabel.Text = SplashScreen.LSStatus + " : " + SplashScreen.LSOnline + " | " + SplashScreen.LSHardwareType + " : USBCAN" + " | " + SplashScreen.LSDevice + " : " + cbbChannel.SelectedItem.ToString() + " | Baudrate : " + cbbBaudrates.SelectedItem.ToString();
                    }
                    catch
                    {
                        InformationLabel.Text = SplashScreen.LSStatus + " : " + SplashScreen.LSOnline + " | " + SplashScreen.LSHardwareType + " : USBCAN" + " | Baudrate : " + cbbBaudrates.SelectedItem.ToString();
                    }



                }
                if (DeviceIsCorrect)
                {
                    InformationLabel.Text = InformationLabel.Text + " | " + SplashScreen.LSConnection + " : " + SplashScreen.LSVerified;
                }
                else
                {
                    InformationLabel.Text = InformationLabel.Text + " | " + SplashScreen.LSConnection + " : " + SplashScreen.LSUnverified;
                }
            }
            else
            {
                if (SplashScreen.HardwareType == 0)
                {
                    InformationLabel.Text = SplashScreen.LSStatus + " : " + SplashScreen.LSOffline + " | " + SplashScreen.LSHardwareType + " : RS232";
                    if (ComportNamesComboBox.Items.Count > 0)
                    {
                        InformationLabel.Text = InformationLabel.Text + " | " + SplashScreen.LSDevice + " : " + ComportNamesComboBox.SelectedItem.ToString() + " | Baudrate : " + BaudrateComboBox.SelectedItem.ToString();
                    }
                }
                else
                {
                    InformationLabel.Text = SplashScreen.LSStatus + " : " + SplashScreen.LSOffline + " | " + SplashScreen.LSHardwareType + " : USBCAN";
                    if (cbbChannel.Items.Count > 0)
                    {
                        InformationLabel.Text = InformationLabel.Text + " | " + SplashScreen.LSDevice + " : " + cbbChannel.SelectedItem.ToString() + " | Baudrate : " + cbbBaudrates.SelectedItem.ToString();
                    }
                }
            }


        }
        private Point SetLocation(int LocationIndex, int Formwidth, int Formheight)
        {
            Point PointResult = new Point(0, 0);
            if (LocationIndex == 0)
            {
                Point pointxy = new Point(0, 0);
                PointResult = pointxy;
            }
            else if (LocationIndex == 1)
            {
                int width = this.Width;
                int pointx = width - Formwidth - 20;
                Point pointxy = new Point(pointx, 0);
                PointResult = pointxy;

            }
            else if (LocationIndex == 2)
            {
                int height = this.Height;
                int pointy = height - Formheight - 90;
                Point pointxy = new Point(0, pointy);
                PointResult = pointxy;
            }
            else if (LocationIndex == 3)
            {
                int width = this.Width;
                int pointx = width - Formwidth - 20;
                int height = this.Height;
                int pointy = height - Formheight - 90;
                Point pointxy = new Point(pointx, pointy);
                PointResult = pointxy;
            }
            return PointResult;
        }
        private void SendParam()
        {
            if (SplashScreen.HardwareType == 0)
            {
                if (CurrentParamPageNumber_Serial != MaxParamPageNumber)
                {
                    UInt32 ID = StartID + 102 + CurrentParamPageNumber_Serial;
                    string IDHex = ID.ToString("X8");
                    string Data = ",";
                    for (int i = 0; i < 20; i++)
                    {
                        Data = Data + ParameterValues[CurrentParamPageNumber_Serial * 20 + i].ToString("X4");
                    }
                    Data = Data + ",";
                    byte[] BytesOfMessage = Encoding.ASCII.GetBytes(IDHex + Data);
                    var HashValue = CRC32Mpeg2.ComputeHash(BytesOfMessage, 0, BytesOfMessage.Length);
                    string HashStringCalculated = BitConverter.ToString(HashValue).Replace("-", string.Empty).Substring(8, 8);

                    SerialPortMain.Write(">" + IDHex + Data + HashStringCalculated + "<");
                    CurrentParamPageNumber_Serial++;
                }
                else
                {
                    UInt32 ID = StartID + 102 + CurrentParamPageNumber_Serial;
                    string IDHex = ID.ToString("X8");
                    string Data = ",";
                    for (int i = 0; i < MaxParamValueNumber + 1; i++)
                    {
                        Data = Data + ParameterValues[CurrentParamPageNumber_Serial * 20 + i].ToString("X4");
                    }
                    Data = Data + ",";
                    byte[] BytesOfMessage = Encoding.ASCII.GetBytes(IDHex + Data);
                    var HashValue = CRC32Mpeg2.ComputeHash(BytesOfMessage, 0, BytesOfMessage.Length);
                    string HashStringCalculated = BitConverter.ToString(HashValue).Replace("-", string.Empty).Substring(8, 8);

                    SerialPortMain.Write(">" + IDHex + Data + HashStringCalculated + "<");
                    CurrentParamPageNumber_Serial = 0;
                    SendToDeviceIsClicked = false;
                    ReadParamFromDevice = true;
                }

            }
            else
            {


                TPCANStatus stsResult;
                TPCANMsg CANMsg;
                CANMsg = new TPCANMsg();
                CANMsg.DATA = new byte[8];
                CANMsg.ID = StartID + 4;
                CANMsg.LEN = 8;
                CANMsg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD;

                if (SendHash)
                {
                    CANMsg.DATA[0] = Convert.ToByte(HashForThisPage);


                    if (HashForThisPage != MaxParamPageNumber)
                    {
                        CANMsg.DATA[1] = 212;// ilk iki bit (11) set edilir geriye kalanlara toplam boyut (20) girilir.
                        byte[] Data = new byte[40];
                        Buffer.BlockCopy(ParameterValues, (HashForThisPage * 20), Data, 0, Data.Length);
                        var Hash32 = CRC32Mpeg2.ComputeHash(Data, 0, Data.Length);
                        CANMsg.DATA[2] = Hash32[7];
                        CANMsg.DATA[3] = Hash32[6];
                        CANMsg.DATA[4] = Hash32[5];
                        CANMsg.DATA[5] = Hash32[4];
                        var Hash = CRC16Arc.ComputeHash(CANMsg.DATA, 0, 6);
                        CANMsg.DATA[6] = Hash[7];
                        CANMsg.DATA[7] = Hash[6];
                    }
                    else
                    {
                        SendToDeviceIsClicked = false;
                        CANMsg.DATA[1] = Convert.ToByte(193 + MaxParamValueNumber);
                        byte[] Data = new byte[(MaxParamValueNumber + 1) * 2];
                        Buffer.BlockCopy(ParameterValues, (HashForThisPage * 20), Data, 0, Data.Length);
                        var Hash32 = CRC32Mpeg2.ComputeHash(Data, 0, Data.Length);
                        CANMsg.DATA[2] = Hash32[7];
                        CANMsg.DATA[3] = Hash32[6];
                        CANMsg.DATA[4] = Hash32[5];
                        CANMsg.DATA[5] = Hash32[4];
                        var Hash = CRC16Arc.ComputeHash(CANMsg.DATA, 0, 6);
                        CANMsg.DATA[6] = Hash[7];
                        CANMsg.DATA[7] = Hash[6];

                    }
                    stsResult = PCANBasic.Write(m_PcanHandle, ref CANMsg);
                    SendHash = false;
                    return;
                }


                CANMsg.DATA[0] = Convert.ToByte(CurrentParamPageNumber);
                CANMsg.DATA[1] = Convert.ToByte(CurrentParamValueNumber);
                int CurrentValueIndex = (CurrentParamPageNumber * 20) + CurrentParamValueNumber;
                int EndIndex = (MaxParamPageNumber * 20) + MaxParamValueNumber;
                int i = 0;
                for (i = 0; i < 3; i++)
                {

                    byte[] Data = BitConverter.GetBytes(ParameterValues[CurrentValueIndex]);

                    CANMsg.DATA[2 * i + 2] = Data[0];
                    CANMsg.DATA[2 * i + 3] = Data[1];
                    if (CurrentValueIndex >= EndIndex)
                    {
                        SendHash = true;
                        HashForThisPage = CurrentParamPageNumber;
                        CurrentParamValueNumber = 0;
                        CurrentParamPageNumber = 0;
                        break;

                    }
                    if (CurrentParamValueNumber >= 19)
                    {
                        SendHash = true;
                        HashForThisPage = CurrentParamPageNumber;
                        CurrentParamValueNumber = 0;
                        CurrentParamPageNumber++;
                        break;
                    }
                    CurrentValueIndex++;
                    CurrentParamValueNumber++;
                }

                if (i == 1)
                {
                    byte Old = CANMsg.DATA[1];
                    CANMsg.DATA[1] = Convert.ToByte(Old + 64);

                }
                else if (i == 0)
                {
                    byte Old = CANMsg.DATA[1];
                    CANMsg.DATA[1] = Convert.ToByte(Old + 128);
                }





                // The message is sent to the configured hardware
                //
                stsResult = PCANBasic.Write(m_PcanHandle, ref CANMsg);
            }
        }
    }
}


