using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace vip.Windows.Query
{
    /// <summary>
    /// addScoreWindow.xaml 的交互逻辑
    /// </summary>
    public partial class addScoreWindow : Window
    {
        public addScoreWindow()
        {
            InitializeComponent();
        }
        public addScoreWindow(Dictionary<string, string> dic)
        {
            InitializeComponent();
            NameLB.Content = dic["Name"];
            SexLB.Content = dic["Sex"]; ;
            ScoresLB.Content = dic["Scores"];
            PhoneLB.Content = dic["Phone"];
            
            //finalScoresLB.Content = "aaa";


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void subScore()
        {
            if (addScoresTB.Text.Replace(" ", "") != "")
            {
                int int1 = int.Parse(ScoresLB.Content.ToString());
                int int2 = int.Parse(addScoresTB.Text.Replace(" ", ""));
                finalScoresLB.Content = int1 + int2;
            }
            else
            {
                finalScoresLB.Content = ScoresLB.Content.ToString();
            }
            
           
            
        }

        private void addScoresTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            subScore();
        }
        private void checkInput(object sender, TextCompositionEventArgs e)
        {
            Windows.Tools.checkInput(e);
        }
        /*
        private void addScoresTB_OnPreviewKeyDown(object sender,KeyEventArgs e)
        {

        }
        protected override void OnPreviewKeyDown( KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 ||
                (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                e.Key == Key.Back ||
                e.Key == Key.Left || e.Key == Key.Right))
            {
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = true;
                }

            }
        }
        */
    }

}
