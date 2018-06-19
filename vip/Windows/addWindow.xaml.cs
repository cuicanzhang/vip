using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace vip.Windows
{
    /// <summary>
    /// addWindow.xaml 的交互逻辑
    /// </summary>
    public partial class addWindow : Window
    {
        public addWindow()
        {
            InitializeComponent();
            //加载控件数据
            loadSex();
            loadBirthdayDate();
            //BirthdayDP.SelectedDate = DateTime.Now.Date;
            // scoresTB.Text = "0";
            //tpnManScoreTB.Text = "0";
            //tpnWomanScoreTB.Text = "0";
            //xyScoreTB.Text = "0";
            //cmScoreTB.Text = "0";
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }
        private void loadSex()
        {
            SexCB.Items.Add("男");
            SexCB.Items.Add("女");
        }
        private void loadBirthdayDate()
        {
            birthdayMonthCB.ItemsSource=Tools.birthdayDic()["month"];
            birthdayDayCB.ItemsSource=Tools.birthdayDic()["day"];
           
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NameTB.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("[姓名] 必须填写");
            }
            else if (SexCB.SelectedValue == null)
            {
                MessageBox.Show("[性别] 必须填写");
            }
            else if (PhoneTB.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("[电话] 必须填写");
            }

            else
            {
                var dic = new Dictionary<string, object>();
                dic["Name"] = NameTB.Text.Replace(" ", "");
                dic["Sex"] = SexCB.SelectedValue;
                dic["Phone"] = PhoneTB.Text.Replace(" ", "");
                dic["Birthday"] = birthdayMonthCB.SelectedValue+"月"+birthdayDayCB.SelectedValue+"日";
                //dic["Birthday"] = BirthdayDP.SelectedDate.Value.ToString("yyyy-MM-dd");
                dic["Remarks"] = RemarksTB.Text;

                dic["Scores"] = scoresTB.Text.Replace(" ", "");
                dic["TpnManScore"] = tpnManScoreTB.Text.Replace(" ", "");
                dic["TpnWomanScore"] = tpnWomanScoreTB.Text.Replace(" ", "");
                dic["XyScore"] = xyScoreTB.Text.Replace(" ", "");
                dic["CmScore"] = cmScoreTB.Text.Replace(" ", "");

                dic["LastModiTime"] = DateTime.Now.ToString("yyyy-MM-dd");
                dic["CreateTime"] = dic["LastModiTime"];

                if (Core.SqlAction.AddVip(dic))
                {
                    var mainWindow = (MainWindow)Owner;
                    mainWindow.reload(PhoneTB.Text.Replace(" ", ""));
                    this.Close();
                    //MessageBox.Show("添加成功");
                }
            }
            
        }
        private void checkNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Tools.isInputNumber(e))
            {
                //MessageBox.Show("请输入数字！");
            }
        }
        private void ScoresTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            subScore();
            String.Format("{0:N}", 1234567.891);
        }
        private void subScore()
        {
            var tpnManScore = tpnManScoreTB.Text.Replace(" ", "");
            var tpnWomanScore = tpnWomanScoreTB.Text.Replace(" ", "");
            var xyScore = xyScoreTB.Text.Replace(" ", "");
            var cmScore = cmScoreTB.Text.Replace(" ", "");

            if (tpnManScore == "")
            {
                tpnManScore = "0";
            }
            if (tpnWomanScore == "")
            {
                tpnWomanScore = "0";
            }
            if (xyScore == "")
            {
                xyScore = "0";
            }
            if (cmScore == "")
            {
                cmScore = "0";
            }
            scoresTB.Text = (int.Parse(tpnManScore) 
                            + int.Parse(tpnWomanScore) 
                            + int.Parse(xyScore) 
                            + int.Parse(cmScore)).ToString();
        }

        private void SexCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
