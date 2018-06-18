using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace vip.Windows
{
    class Tools
    {
        public static void checkInput(TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }
        public static bool isInputNumber(KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
               e.Key == Key.Delete || e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.OemPeriod)
            {
                //按下了Alt、ctrl、shift等修饰键  
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                }
                else
                {
                    return true;
                }

            }
            else//按下了字符等其它功能键  
            {
                e.Handled = true;
            }
            return false;
        }
        public static void checkNumber(object sender, KeyEventArgs e)
        {
            if (!Tools.isInputNumber(e))
            {
                //MessageBox.Show("请输入数字！");
            }
        }
        public static string StringToMD5Hash(string inputString)
        {
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }
        public static Dictionary<string, List<string>> birthdayDic()
        {
            Dictionary<string, List<string>> birthdayDic = new Dictionary<string, List<string>>();

            List<string> monthList = new List<string>();
            List<string> dayList = new List<string>();
            for (int i = 0; i < 12; i++)
            {
                monthList.Add((i + 1).ToString().PadLeft(2,'0'));
            }
            for (int i = 0; i < 31; i++)
            {
                dayList.Add((i + 1).ToString().PadLeft(2, '0'));
            }
            birthdayDic["month"] = monthList;
            birthdayDic["day"] = dayList;
            return birthdayDic;
        }
    }
}
