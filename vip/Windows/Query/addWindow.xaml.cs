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

namespace vip.Windows.Query
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
            BirthdayDP.SelectedDate = DateTime.Now.Date;
            // scoresTB.Text = "0";
            //tpnManScoreTB.Text = "0";
            //tpnWomanScoreTB.Text = "0";
            //xyScoreTB.Text = "0";
            //cmScoreTB.Text = "0";
        }
        private void loadSex()
        {
            SexCB.Items.Add("男");
            SexCB.Items.Add("女");
        }
        private bool Add()
        {
            using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        SQLiteHelper sh = new SQLiteHelper(cmd);
                        //var sql= "select * from vip where (Name='"+ NameTB.Text+"' and Phone='"+ PhoneTB.Text+"')";
                        var sql = "select * from vip where (Phone='" + PhoneTB.Text.Replace(" ", "") + "')";
                        DataTable dt = sh.Select(sql);
                        if (dt.Rows.Count == 0)
                        {
                            //int count = sh.ExecuteScalar<int>("select count(*) from vip;") + 1;
                            var dic = new Dictionary<string, object>();
                            dic["Name"] = NameTB.Text.Replace(" ", "");
                            dic["Sex"] = SexCB.SelectedValue;
                            dic["Phone"] = PhoneTB.Text.Replace(" ", "");
                            dic["Birthday"] = BirthdayDP.SelectedDate.Value.ToString("yyyy-MM-dd");
                            dic["Remarks"] = RemarksTB.Text;

                            dic["Scores"] = scoresTB.Text.Replace(" ", "");
                            dic["TpnManScore"] = tpnManScoreTB.Text.Replace(" ", "");
                            dic["TpnWomanScore"] = tpnWomanScoreTB.Text.Replace(" ", "");
                            dic["XyScore"] = xyScoreTB.Text.Replace(" ", "");
                            dic["CmScore"] = cmScoreTB.Text.Replace(" ", "");

                            dic["LastModiTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            dic["CreateTime"] = dic["LastModiTime"];

                            sh.Insert("vip", dic);
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("会员已存在");
                            return false;
                        }                                             
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
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
                if (Add())
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
    }
}
