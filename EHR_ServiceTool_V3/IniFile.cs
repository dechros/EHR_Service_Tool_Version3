using System.Runtime.InteropServices;
using System.Text;

namespace EHR_ServiceTool_V3
{
    class IniFile
    {

        private string filePath;



        [DllImport("kernel32")]
        public static extern bool WritePrivateProfileString(byte[] section, byte[] key, byte[] val, string filePath);
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(byte[] section, byte[] key, byte[] def, byte[] retVal, int size, string filePath);

        public IniFile(string filePath)
        {
            this.filePath = filePath;
        }

        private static byte[] getBytes(string s, string encodingName)
        {
            return null == s ? null : Encoding.GetEncoding(encodingName).GetBytes(s);
        }
        //public void Write(string section, string key, string value)
        //{
        //    WritePrivateProfileString(section, key, value, this.filePath);
        //}

        //public string Read(string section, string key)
        //{
        //    //StringBuilder SB = new StringBuilder(255);

        //    //int i = GetPrivateProfileString(section, key, "", SB, 255, this.filePath);

        //    //return SB.ToString();
        //}
        public string Read(string section, string key)
        {
            string fileName = filePath;
            string encodingName = "utf-8";
            int size = 1024;
            string def = "";
            byte[] buffer = new byte[size];
            int count = GetPrivateProfileString(getBytes(section, encodingName), getBytes(key, encodingName), getBytes(def, encodingName), buffer, size, fileName);
            return Encoding.GetEncoding(encodingName).GetString(buffer, 0, count).Trim();
        }
        public bool Write(string section, string key, string value)
        {
            string fileName = filePath;
            string encodingName = "utf-8";
            return WritePrivateProfileString(getBytes(section, encodingName), getBytes(key, encodingName), getBytes(value, encodingName), fileName);
        }
        public string FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }
    }


}
