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
        private static void checkInput(KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 ||
                (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                e.Key == Key.Back ||
                e.Key == Key.Left || e.Key == Key.Right))
            {

            }
        }
    }
}
