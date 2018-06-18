using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

namespace vip.Windows
{
    /// <summary>
    /// modifyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class modifyWindow : Window
    {
        VipInfo vip = new VipInfo();
        public modifyWindow()
        {
            InitializeComponent();
            

        }
        private void loadSex()
        {
            SexCB.Items.Add("男");
            SexCB.Items.Add("女");            
        }
        private void loadBirthdayDate()
        {
            for (int i = 0; i < 12; i++)
            {
                birthdayMonthCB.Items.Add((i + 1).ToString());
            }
            for (int i = 0; i < 32; i++)
            {
                birthdayDayCB.Items.Add((i + 1).ToString());
            }
        }
        public modifyWindow(Dictionary<string, string> dic)
        {
            InitializeComponent();
            //vip初始化
            vip.ID = dic["ID"];
            vip.Name = dic["Name"];
            vip.Sex = dic["Sex"];
            vip.Phone = dic["Phone"];
            vip.Birthday = dic["Birthday"];
            vip.Remarks = dic["Remarks"].ToString();

            //加载控件数据
            NameTB.Text = vip.Name;
            loadSex();
            loadBirthdayDate();
            SexCB.SelectedIndex=SexCB.Items.IndexOf(vip.Sex);
            PhoneTB.Text = vip.Phone;

            Regex reg = new Regex(@"(.*)月(.*)日");
            Match match = reg.Match(vip.Birthday);
            birthdayMonthCB.SelectedValue = match.Groups[1].Value;
            birthdayDayCB.SelectedValue = match.Groups[2].Value;
            //BirthdayDP.SelectedDate =Convert.ToDateTime(vip.Birthday);
            RemarksTB.Text = vip.Remarks;
            ScoresTB.Text = dic["Scores"];
            tpnManScoresTB.Text= dic["TpnManScore"];
            tpnWomanScoresTB.Text = dic["TpnWomanScore"];
            xyScoresTB.Text = dic["XyScore"];
            cmScoresTB.Text = dic["CmScore"];
        }
   
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string sexTemp;
            if (SexCB.SelectedValue == null)
            {
                sexTemp = "";
            }
            else
            {
                sexTemp = SexCB.SelectedValue.ToString();
            }          
            if (vip.Name != NameTB.Text
                || vip.Sex != sexTemp
                || vip.Phone!= PhoneTB.Text
                || vip.Birthday!= birthdayMonthCB.SelectedValue + "月" + birthdayDayCB.SelectedValue + "日"
                || vip.Remarks!= RemarksTB.Text)
            {
                var dic = new Dictionary<string, object>();
                dic["ID"] = vip.ID;
                if (vip.Name != NameTB.Text)
                {
                    dic["Name"] = NameTB.Text;
                }
                if (vip.Phone != PhoneTB.Text)
                {
                    dic["Phone"] = PhoneTB.Text;
                }
                if (vip.Remarks != RemarksTB.Text)
                {
                    dic["Remarks"] = RemarksTB.Text;
                }
                if (vip.Sex != SexCB.SelectedValue.ToString())
                {
                    dic["Sex"] = SexCB.SelectedValue;
                }
                if (vip.Birthday != birthdayMonthCB.SelectedValue + "月" + birthdayDayCB.SelectedValue + "日")
                {
                    dic["Birthday"] = birthdayMonthCB.SelectedValue + "月" + birthdayDayCB.SelectedValue + "日";
                }
                if (Core.SqlAction.VipModify(dic))
                {
                    var mainWindow = (MainWindow)Owner;
                    mainWindow.reload(PhoneTB.Text.Replace(" ", ""));
                    this.Close();
                    //MessageBox.Show("添加成功");
                }
            }
            else
            {
                this.Close();
            }          
        }
        private void checkNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Tools.isInputNumber(e))
            {
                //MessageBox.Show("请输入数字！");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
