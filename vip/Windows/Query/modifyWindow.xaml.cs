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
    /// modifyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class modifyWindow : Window
    {

        private static string vipPhone = "";
        private static string vipScore = "";
        public modifyWindow()
        {
            InitializeComponent();
            

        }
        private void loadSex()
        {
            SexCB.Items.Add("男");
            SexCB.Items.Add("女");
            
        }

        public modifyWindow(Dictionary<string, string> dic)
        {
            InitializeComponent();
            //加载会员性别
            loadSex();
            
            vipPhone = dic["Phone"];
            vipScore = dic["Scores"];

            NameTB.Text = dic["Name"];
            SexCB.SelectedIndex=SexCB.Items.IndexOf(dic["Sex"]);
            PhoneTB.Text = dic["Phone"];
            RemarksTB.Text = dic["Remarks"];

            ScoresTB.Text = dic["Scores"];
            tpnManScoresTB.Text= dic["TpnManScore"];
            tpnWomanScoresTB.Text = dic["TpnWomanScore"];
            xyScoresTB.Text = dic["XyScore"];
            cmScoresTB.Text = dic["CmScore"];
        }
        private bool vipModify()
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
                            var dic = new Dictionary<string, object>();
                            dic["Name"] = NameTB.Text;
                            dic["Sex"]= SexCB.SelectedValue;                          
                            dic["Phone"] = PhoneTB.Text;
                            dic["Remarks"] = RemarksTB.Text;

                            dic["Scores"] = ScoresTB.Text;
                            dic["TpnManScore"]=tpnManScoresTB.Text ;
                            dic["TpnWomanScore"]=tpnWomanScoresTB.Text  ;
                            dic["XyScore"]=xyScoresTB.Text ;
                            dic["CmScore"]=cmScoresTB.Text ;

                        if (vipScore != ScoresTB.Text) {
                                dic["LastModiTime"] = DateTime.Now.ToLongDateString().ToString();
                            }
                            
                            sh.Update("vip", dic, "Phone", vipPhone);
                            return true;
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
            if (vipModify())
            {
                this.Close();
                //MessageBox.Show("添加成功");
            }
        }
        private void checkInput(object sender, TextCompositionEventArgs e)
        {
            Windows.Tools.checkInput(e);
        }
        private void ScoresTB_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Tools.isInputNumber(e))
            {
                //MessageBox.Show("请输入数字！");
            }
        }
        private void PhoneTB_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Tools.isInputNumber(e))
            {
                //MessageBox.Show("请输入数字！");
            }
        }

        private void tpnWomanScoresTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tpnManScoresTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void xyScoresTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cmScoresTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
