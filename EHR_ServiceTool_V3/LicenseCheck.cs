using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace EHR_ServiceTool_V3
{
    class LicenseCheck
    {
        private static readonly byte[] salt = Encoding.Unicode.GetBytes("OZKNSMSK");
        private static readonly int iterantions = 2000;
        public static bool Resolve(string LicenseKey)
        {
            string ProcessorID = GetHardWareInfo("Win32_Processor", "ProcessorId");
            string MachineID = GetMachineID(ProcessorID);
            string RecoverPart1 = SolveKeyPart4AndRecoverPart1(LicenseKey.Substring(18, 5));
            string RecoverPart2 = SolveKeyPart3AndRecoverPart2(LicenseKey.Substring(12, 5));
            string RecoverPart3 = SolveKeyPart2AndRecoverPart3(LicenseKey.Substring(6, 5));
            string RecoverPart4 = SolveKeyPart1AndRecoverPart4(LicenseKey.Substring(0, 5));

            string RecoveredMachineID = RecoverPart1 + "-" + RecoverPart2 + "-" + RecoverPart3 + "-" + RecoverPart4;

            return String.Equals(MachineID, RecoveredMachineID);
        }

        public static string Encrypt(string LicenseKey, string PassWord)
        {
            byte[] LicenseKeyBytes = Encoding.Unicode.GetBytes(LicenseKey);
            var AES = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(PassWord, salt, iterantions);
            AES.Key = pbkdf2.GetBytes(32);
            AES.IV = pbkdf2.GetBytes(16);
            var MemoryStream = new MemoryStream();
            using (var CryptoStream = new CryptoStream(MemoryStream, AES.CreateEncryptor(), CryptoStreamMode.Write))
            {
                CryptoStream.Write(LicenseKeyBytes, 0, LicenseKeyBytes.Length);
            }
            return Convert.ToBase64String(MemoryStream.ToArray());
        }

        public static string Decrypt(string EncryptedLicenseKey, string Password)
        {
            byte[] EncryptedLicenseKeyBytes = Convert.FromBase64String(EncryptedLicenseKey);
            var AES = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(Password, salt, iterantions);
            AES.Key = pbkdf2.GetBytes(32);
            AES.IV = pbkdf2.GetBytes(16);
            var MemoryStream = new MemoryStream();
            using (var CryptoStream = new CryptoStream(MemoryStream, AES.CreateDecryptor(), CryptoStreamMode.Write))
            {
                CryptoStream.Write(EncryptedLicenseKeyBytes, 0, EncryptedLicenseKeyBytes.Length);

            }
            return Encoding.Unicode.GetString(MemoryStream.ToArray());
        }
        public static string GetHardWareInfo(string HardWareClass, string Syntax)
        {
            ManagementObjectSearcher Search = new ManagementObjectSearcher("root\\CIMV2", "select * from " + HardWareClass);
            string Info = String.Empty;
            foreach (ManagementObject MO in Search.Get())
            {
                Info = Convert.ToString(MO[Syntax]);
                return Info;
            }

            return "";
        }

        public static string FillZero(string SourceString)
        {
            String Target = string.Empty;
            String Zero = "00000";
            Target = Zero.Substring(0, (5 - SourceString.Length)) + SourceString;
            return Target;
        }
        public static string GetMachineID(string ProcessorID)
        {
            int i = 0;
            String Part1 = string.Empty;
            String Part2 = string.Empty;
            String Part3 = string.Empty;
            String Part4 = string.Empty;
            foreach (char ch in ProcessorID)
            {

                if (i <= 3)
                {
                    Part1 = Part1 + ch;
                }
                else if ((i > 3) && (i <= 7))
                {
                    Part2 = Part2 + ch;
                }
                else if ((i > 7) && (i <= 11))
                {
                    Part3 = Part3 + ch;
                }
                else if ((i > 11))
                {
                    Part4 = Part4 + ch;
                }

                i++;

            }
            Int64 GetNum1, GetNum2, GetNum3, GetNum4;
            GetNum1 = Convert.ToInt64(Part1, 16);
            GetNum2 = Convert.ToInt64(Part2, 16);
            GetNum3 = Convert.ToInt64(Part3, 16);
            GetNum4 = Convert.ToInt64(Part4, 16);

            Part1 = FillZero(GetNum1.ToString());
            Part2 = FillZero(GetNum2.ToString());
            Part3 = FillZero(GetNum3.ToString());
            Part4 = FillZero(GetNum4.ToString());

            return Part1 + "-" + Part2 + "-" + Part3 + "-" + Part4;
        }

        public static string SolveKeyPart1AndRecoverPart4(string KeyPart1)
        {
            string RecoverPart4 = string.Empty;
            char First = Convert.ToChar(KeyPart1.Substring(0, 1));
            char Second = Convert.ToChar(KeyPart1.Substring(1, 1));
            char Third = Convert.ToChar(KeyPart1.Substring(2, 1));
            char Fourth = Convert.ToChar(KeyPart1.Substring(3, 1));
            char Fifth = Convert.ToChar(KeyPart1.Substring(4, 1));

            switch (First)
            {
                case 'M':
                    RecoverPart4 = "0";
                    break;
                case 'I':
                    RecoverPart4 = "1";
                    break;
                case 'K':
                    RecoverPart4 = "2";
                    break;
                case 'N':
                    RecoverPart4 = "3";
                    break;
                case 'A':
                    RecoverPart4 = "4";
                    break;
                case 'T':
                    RecoverPart4 = "5";
                    break;
                case 'E':
                    RecoverPart4 = "6";
                    break;
                case 'B':
                    RecoverPart4 = "7";
                    break;
                case 'R':
                    RecoverPart4 = "8";
                    break;
                case 'U':
                    RecoverPart4 = "9";
                    break;

            }
            switch (Second)
            {
                case 'R':
                    RecoverPart4 = RecoverPart4 + "0";
                    break;
                case 'K':
                    RecoverPart4 = RecoverPart4 + "1";
                    break;
                case 'O':
                    RecoverPart4 = RecoverPart4 + "2";
                    break;
                case 'M':
                    RecoverPart4 = RecoverPart4 + "3";
                    break;
                case 'S':
                    RecoverPart4 = RecoverPart4 + "4";
                    break;
                case 'A':
                    RecoverPart4 = RecoverPart4 + "5";
                    break;
                case 'L':
                    RecoverPart4 = RecoverPart4 + "6";
                    break;
                case 'P':
                    RecoverPart4 = RecoverPart4 + "7";
                    break;
                case 'X':
                    RecoverPart4 = RecoverPart4 + "8";
                    break;
                case 'Z':
                    RecoverPart4 = RecoverPart4 + "9";
                    break;

            }
            switch (Third)
            {
                case 'Q':
                    RecoverPart4 = RecoverPart4 + "0";
                    break;
                case 'Z':
                    RecoverPart4 = RecoverPart4 + "1";
                    break;
                case 'N':
                    RecoverPart4 = RecoverPart4 + "2";
                    break;
                case 'B':
                    RecoverPart4 = RecoverPart4 + "3";
                    break;
                case 'V':
                    RecoverPart4 = RecoverPart4 + "4";
                    break;
                case 'I':
                    RecoverPart4 = RecoverPart4 + "5";
                    break;
                case 'T':
                    RecoverPart4 = RecoverPart4 + "6";
                    break;
                case 'Y':
                    RecoverPart4 = RecoverPart4 + "7";
                    break;
                case 'J':
                    RecoverPart4 = RecoverPart4 + "8";
                    break;
                case 'D':
                    RecoverPart4 = RecoverPart4 + "9";
                    break;

            }
            switch (Fourth)
            {
                case 'P':
                    RecoverPart4 = RecoverPart4 + "0";
                    break;
                case 'O':
                    RecoverPart4 = RecoverPart4 + "1";
                    break;
                case 'L':
                    RecoverPart4 = RecoverPart4 + "2";
                    break;
                case 'S':
                    RecoverPart4 = RecoverPart4 + "3";
                    break;
                case 'A':
                    RecoverPart4 = RecoverPart4 + "4";
                    break;
                case 'R':
                    RecoverPart4 = RecoverPart4 + "5";
                    break;
                case 'G':
                    RecoverPart4 = RecoverPart4 + "6";
                    break;
                case 'J':
                    RecoverPart4 = RecoverPart4 + "7";
                    break;
                case 'T':
                    RecoverPart4 = RecoverPart4 + "8";
                    break;
                case 'F':
                    RecoverPart4 = RecoverPart4 + "9";
                    break;
            }
            switch (Fifth)
            {
                case 'H':
                    RecoverPart4 = RecoverPart4 + "0";
                    break;
                case 'W':
                    RecoverPart4 = RecoverPart4 + "1";
                    break;
                case 'D':
                    RecoverPart4 = RecoverPart4 + "2";
                    break;
                case 'A':
                    RecoverPart4 = RecoverPart4 + "3";
                    break;
                case 'X':
                    RecoverPart4 = RecoverPart4 + "4";
                    break;
                case 'L':
                    RecoverPart4 = RecoverPart4 + "5";
                    break;
                case 'O':
                    RecoverPart4 = RecoverPart4 + "6";
                    break;
                case 'K':
                    RecoverPart4 = RecoverPart4 + "7";
                    break;
                case 'U':
                    RecoverPart4 = RecoverPart4 + "8";
                    break;
                case 'M':
                    RecoverPart4 = RecoverPart4 + "9";
                    break;

            }

            return RecoverPart4;
        }
        public static string SolveKeyPart2AndRecoverPart3(string KeyPart2)
        {
            string RecoverPart3 = string.Empty;
            char First = Convert.ToChar(KeyPart2.Substring(0, 1));
            char Second = Convert.ToChar(KeyPart2.Substring(1, 1));
            char Third = Convert.ToChar(KeyPart2.Substring(2, 1));
            char Fourth = Convert.ToChar(KeyPart2.Substring(3, 1));
            char Fifth = Convert.ToChar(KeyPart2.Substring(4, 1));

            switch (First)
            {
                case 'O':
                    RecoverPart3 = "0";
                    break;
                case 'Z':
                    RecoverPart3 = "1";
                    break;
                case 'K':
                    RecoverPart3 = "2";
                    break;
                case 'A':
                    RecoverPart3 = "3";
                    break;
                case 'N':
                    RecoverPart3 = "4";
                    break;
                case 'S':
                    RecoverPart3 = "5";
                    break;
                case 'I':
                    RecoverPart3 = "6";
                    break;
                case 'M':
                    RecoverPart3 = "7";
                    break;
                case 'E':
                    RecoverPart3 = "8";
                    break;
                case 'Q':
                    RecoverPart3 = "9";
                    break;

            }
            switch (Second)
            {
                case 'Y':
                    RecoverPart3 = RecoverPart3 + "0";
                    break;
                case 'A':
                    RecoverPart3 = RecoverPart3 + "1";
                    break;
                case 'R':
                    RecoverPart3 = RecoverPart3 + "2";
                    break;
                case 'D':
                    RecoverPart3 = RecoverPart3 + "3";
                    break;
                case 'I':
                    RecoverPart3 = RecoverPart3 + "4";
                    break;
                case 'M':
                    RecoverPart3 = RecoverPart3 + "5";
                    break;
                case 'E':
                    RecoverPart3 = RecoverPart3 + "6";
                    break;
                case 'T':
                    RecoverPart3 = RecoverPart3 + "7";
                    break;
                case 'F':
                    RecoverPart3 = RecoverPart3 + "8";
                    break;
                case 'H':
                    RecoverPart3 = RecoverPart3 + "9";
                    break;

            }
            switch (Third)
            {
                case 'A':
                    RecoverPart3 = RecoverPart3 + "0";
                    break;
                case 'C':
                    RecoverPart3 = RecoverPart3 + "1";
                    break;
                case 'F':
                    RecoverPart3 = RecoverPart3 + "2";
                    break;
                case 'H':
                    RecoverPart3 = RecoverPart3 + "3";
                    break;
                case 'K':
                    RecoverPart3 = RecoverPart3 + "4";
                    break;
                case 'M':
                    RecoverPart3 = RecoverPart3 + "5";
                    break;
                case 'O':
                    RecoverPart3 = RecoverPart3 + "6";
                    break;
                case 'P':
                    RecoverPart3 = RecoverPart3 + "7";
                    break;
                case 'Y':
                    RecoverPart3 = RecoverPart3 + "8";
                    break;
                case 'Z':
                    RecoverPart3 = RecoverPart3 + "9";
                    break;

            }
            switch (Fourth)
            {
                case 'E':
                    RecoverPart3 = RecoverPart3 + "0";
                    break;
                case 'D':
                    RecoverPart3 = RecoverPart3 + "1";
                    break;
                case 'K':
                    RecoverPart3 = RecoverPart3 + "2";
                    break;
                case 'L':
                    RecoverPart3 = RecoverPart3 + "3";
                    break;
                case 'N':
                    RecoverPart3 = RecoverPart3 + "4";
                    break;
                case 'T':
                    RecoverPart3 = RecoverPart3 + "5";
                    break;
                case 'F':
                    RecoverPart3 = RecoverPart3 + "6";
                    break;
                case 'P':
                    RecoverPart3 = RecoverPart3 + "7";
                    break;
                case 'I':
                    RecoverPart3 = RecoverPart3 + "8";
                    break;
                case 'R':
                    RecoverPart3 = RecoverPart3 + "9";
                    break;
            }
            switch (Fifth)
            {
                case 'R':
                    RecoverPart3 = RecoverPart3 + "0";
                    break;
                case 'S':
                    RecoverPart3 = RecoverPart3 + "1";
                    break;
                case 'F':
                    RecoverPart3 = RecoverPart3 + "2";
                    break;
                case 'Y':
                    RecoverPart3 = RecoverPart3 + "3";
                    break;
                case 'K':
                    RecoverPart3 = RecoverPart3 + "4";
                    break;
                case 'G':
                    RecoverPart3 = RecoverPart3 + "5";
                    break;
                case 'D':
                    RecoverPart3 = RecoverPart3 + "6";
                    break;
                case 'M':
                    RecoverPart3 = RecoverPart3 + "7";
                    break;
                case 'X':
                    RecoverPart3 = RecoverPart3 + "8";
                    break;
                case 'U':
                    RecoverPart3 = RecoverPart3 + "9";
                    break;

            }

            return RecoverPart3;
        }
        public static string SolveKeyPart3AndRecoverPart2(string KeyPart3)
        {
            string RecoverPart2 = string.Empty;
            char First = Convert.ToChar(KeyPart3.Substring(0, 1));
            char Second = Convert.ToChar(KeyPart3.Substring(1, 1));
            char Third = Convert.ToChar(KeyPart3.Substring(2, 1));
            char Fourth = Convert.ToChar(KeyPart3.Substring(3, 1));
            char Fifth = Convert.ToChar(KeyPart3.Substring(4, 1));

            switch (First)
            {
                case 'M':
                    RecoverPart2 = "0";
                    break;
                case 'K':
                    RecoverPart2 = "1";
                    break;
                case 'Q':
                    RecoverPart2 = "2";
                    break;
                case 'G':
                    RecoverPart2 = "3";
                    break;
                case 'F':
                    RecoverPart2 = "4";
                    break;
                case 'I':
                    RecoverPart2 = "5";
                    break;
                case 'W':
                    RecoverPart2 = "6";
                    break;
                case 'Z':
                    RecoverPart2 = "7";
                    break;
                case 'O':
                    RecoverPart2 = "8";
                    break;
                case 'P':
                    RecoverPart2 = "9";
                    break;

            }
            switch (Second)
            {
                case 'H':
                    RecoverPart2 = RecoverPart2 + "0";
                    break;
                case 'I':
                    RecoverPart2 = RecoverPart2 + "1";
                    break;
                case 'E':
                    RecoverPart2 = RecoverPart2 + "2";
                    break;
                case 'R':
                    RecoverPart2 = RecoverPart2 + "3";
                    break;
                case 'X':
                    RecoverPart2 = RecoverPart2 + "4";
                    break;
                case 'S':
                    RecoverPart2 = RecoverPart2 + "5";
                    break;
                case 'K':
                    RecoverPart2 = RecoverPart2 + "6";
                    break;
                case 'L':
                    RecoverPart2 = RecoverPart2 + "7";
                    break;
                case 'A':
                    RecoverPart2 = RecoverPart2 + "8";
                    break;
                case 'V':
                    RecoverPart2 = RecoverPart2 + "9";
                    break;

            }
            switch (Third)
            {
                case 'G':
                    RecoverPart2 = RecoverPart2 + "0";
                    break;
                case 'O':
                    RecoverPart2 = RecoverPart2 + "1";
                    break;
                case 'T':
                    RecoverPart2 = RecoverPart2 + "2";
                    break;
                case 'N':
                    RecoverPart2 = RecoverPart2 + "3";
                    break;
                case 'S':
                    RecoverPart2 = RecoverPart2 + "4";
                    break;
                case 'A':
                    RecoverPart2 = RecoverPart2 + "5";
                    break;
                case 'L':
                    RecoverPart2 = RecoverPart2 + "6";
                    break;
                case 'M':
                    RecoverPart2 = RecoverPart2 + "7";
                    break;
                case 'Y':
                    RecoverPart2 = RecoverPart2 + "8";
                    break;
                case 'Z':
                    RecoverPart2 = RecoverPart2 + "9";
                    break;

            }
            switch (Fourth)
            {
                case 'K':
                    RecoverPart2 = RecoverPart2 + "0";
                    break;
                case 'A':
                    RecoverPart2 = RecoverPart2 + "1";
                    break;
                case 'F':
                    RecoverPart2 = RecoverPart2 + "2";
                    break;
                case 'D':
                    RecoverPart2 = RecoverPart2 + "3";
                    break;
                case 'N':
                    RecoverPart2 = RecoverPart2 + "4";
                    break;
                case 'U':
                    RecoverPart2 = RecoverPart2 + "5";
                    break;
                case 'Y':
                    RecoverPart2 = RecoverPart2 + "6";
                    break;
                case 'R':
                    RecoverPart2 = RecoverPart2 + "7";
                    break;
                case 'M':
                    RecoverPart2 = RecoverPart2 + "8";
                    break;
                case 'H':
                    RecoverPart2 = RecoverPart2 + "9";
                    break;
            }
            switch (Fifth)
            {
                case 'L':
                    RecoverPart2 = RecoverPart2 + "0";
                    break;
                case 'R':
                    RecoverPart2 = RecoverPart2 + "1";
                    break;
                case 'F':
                    RecoverPart2 = RecoverPart2 + "2";
                    break;
                case 'S':
                    RecoverPart2 = RecoverPart2 + "3";
                    break;
                case 'X':
                    RecoverPart2 = RecoverPart2 + "4";
                    break;
                case 'B':
                    RecoverPart2 = RecoverPart2 + "5";
                    break;
                case 'N':
                    RecoverPart2 = RecoverPart2 + "6";
                    break;
                case 'W':
                    RecoverPart2 = RecoverPart2 + "7";
                    break;
                case 'A':
                    RecoverPart2 = RecoverPart2 + "8";
                    break;
                case 'P':
                    RecoverPart2 = RecoverPart2 + "9";
                    break;

            }

            return RecoverPart2;
        }
        public static string SolveKeyPart4AndRecoverPart1(string KeyPart4)
        {
            string RecoverPart1 = string.Empty;
            char First = Convert.ToChar(KeyPart4.Substring(0, 1));
            char Second = Convert.ToChar(KeyPart4.Substring(1, 1));
            char Third = Convert.ToChar(KeyPart4.Substring(2, 1));
            char Fourth = Convert.ToChar(KeyPart4.Substring(3, 1));
            char Fifth = Convert.ToChar(KeyPart4.Substring(4, 1));

            switch (First)
            {
                case 'W':
                    RecoverPart1 = "0";
                    break;
                case 'H':
                    RecoverPart1 = "1";
                    break;
                case 'O':
                    RecoverPart1 = "2";
                    break;
                case 'A':
                    RecoverPart1 = "3";
                    break;
                case 'M':
                    RecoverPart1 = "4";
                    break;
                case 'I':
                    RecoverPart1 = "5";
                    break;
                case 'B':
                    RecoverPart1 = "6";
                    break;
                case 'E':
                    RecoverPart1 = "7";
                    break;
                case 'R':
                    RecoverPart1 = "8";
                    break;
                case 'T':
                    RecoverPart1 = "9";
                    break;

            }
            switch (Second)
            {
                case 'B':
                    RecoverPart1 = RecoverPart1 + "0";
                    break;
                case 'A':
                    RecoverPart1 = RecoverPart1 + "1";
                    break;
                case 'L':
                    RecoverPart1 = RecoverPart1 + "2";
                    break;
                case 'G':
                    RecoverPart1 = RecoverPart1 + "3";
                    break;
                case 'E':
                    RecoverPart1 = RecoverPart1 + "4";
                    break;
                case 'S':
                    RecoverPart1 = RecoverPart1 + "5";
                    break;
                case 'T':
                    RecoverPart1 = RecoverPart1 + "6";
                    break;
                case 'U':
                    RecoverPart1 = RecoverPart1 + "7";
                    break;
                case 'R':
                    RecoverPart1 = RecoverPart1 + "8";
                    break;
                case 'K':
                    RecoverPart1 = RecoverPart1 + "9";
                    break;

            }
            switch (Third)
            {
                case 'S':
                    RecoverPart1 = RecoverPart1 + "0";
                    break;
                case 'I':
                    RecoverPart1 = RecoverPart1 + "1";
                    break;
                case 'V':
                    RecoverPart1 = RecoverPart1 + "2";
                    break;
                case 'A':
                    RecoverPart1 = RecoverPart1 + "3";
                    break;
                case 'C':
                    RecoverPart1 = RecoverPart1 + "4";
                    break;
                case 'P':
                    RecoverPart1 = RecoverPart1 + "5";
                    break;
                case 'E':
                    RecoverPart1 = RecoverPart1 + "6";
                    break;
                case 'L':
                    RecoverPart1 = RecoverPart1 + "7";
                    break;
                case 'T':
                    RecoverPart1 = RecoverPart1 + "8";
                    break;
                case 'Q':
                    RecoverPart1 = RecoverPart1 + "9";
                    break;

            }
            switch (Fourth)
            {
                case 'Q':
                    RecoverPart1 = RecoverPart1 + "0";
                    break;
                case 'A':
                    RecoverPart1 = RecoverPart1 + "1";
                    break;
                case 'S':
                    RecoverPart1 = RecoverPart1 + "2";
                    break;
                case 'M':
                    RecoverPart1 = RecoverPart1 + "3";
                    break;
                case 'I':
                    RecoverPart1 = RecoverPart1 + "4";
                    break;
                case 'W':
                    RecoverPart1 = RecoverPart1 + "5";
                    break;
                case 'R':
                    RecoverPart1 = RecoverPart1 + "6";
                    break;
                case 'T':
                    RecoverPart1 = RecoverPart1 + "7";
                    break;
                case 'G':
                    RecoverPart1 = RecoverPart1 + "8";
                    break;
                case 'L':
                    RecoverPart1 = RecoverPart1 + "9";
                    break;
            }
            switch (Fifth)
            {
                case 'Y':
                    RecoverPart1 = RecoverPart1 + "0";
                    break;
                case 'O':
                    RecoverPart1 = RecoverPart1 + "1";
                    break;
                case 'U':
                    RecoverPart1 = RecoverPart1 + "2";
                    break;
                case 'R':
                    RecoverPart1 = RecoverPart1 + "3";
                    break;
                case 'P':
                    RecoverPart1 = RecoverPart1 + "4";
                    break;
                case 'I':
                    RecoverPart1 = RecoverPart1 + "5";
                    break;
                case 'C':
                    RecoverPart1 = RecoverPart1 + "6";
                    break;
                case 'F':
                    RecoverPart1 = RecoverPart1 + "7";
                    break;
                case 'A':
                    RecoverPart1 = RecoverPart1 + "8";
                    break;
                case 'K':
                    RecoverPart1 = RecoverPart1 + "9";
                    break;

            }

            return RecoverPart1;
        }
    }
}
