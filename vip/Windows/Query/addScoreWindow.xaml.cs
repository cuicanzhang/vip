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
            string str1 = ScoresLB.Content.ToString();
            string str2 = addScoresTB.Text;

            finalScoresLB.Content = int.Parse(str1) + int.Parse(str2);
        }

        private void addScoresTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            subScore();
        }
        private void checkInput(object sender, TextCompositionEventArgs e)
        {
            Windows.Tools.checkInput(e);
        }
    }

}
