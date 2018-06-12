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
        private static string vipName="";
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
            vipName = dic["Name"];
            vipPhone = dic["Phone"];
            vipScore = dic["Scores"];

            NameTB.Text = dic["Name"];
            SexCB.SelectedIndex=SexCB.Items.IndexOf(dic["Sex"]);
            ScoresTB.Text = dic["Scores"];
            PhoneTB.Text = dic["Phone"];
            RemarksTB.Text = dic["Remarks"];
            
            
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
                        //var sql = "select ID from vip where (Name='" + vipName + "' and Phone='" + vipPhone + "')";
                        //var sql = "select ID from vip where (Phone='" + vipPhone + "')";
                        //DataTable dt = sh.Select(sql);
                        //if (dt.Rows.Count != 0)
                        //{
                            var dic = new Dictionary<string, object>();
                            dic["Name"] = NameTB.Text;
                            dic["Sex"]= SexCB.SelectedValue;
                            dic["Scores"] = ScoresTB.Text;
                            dic["Phone"] = PhoneTB.Text;
                            if (vipScore != ScoresTB.Text) {
                                dic["LastModiTime"] = DateTime.Now.ToLongDateString().ToString();
                            }
                            dic["Remarks"] = RemarksTB.Text;
                            //sh.Update("vip", dic,"ID", dt.Rows[0]["ID"].ToString());
                            sh.Update("vip", dic, "Phone", vipPhone);
                            return true;
                        //}
                        //else
                        //{
                         //   MessageBox.Show("会员已存在");
                        //    return false;
                       // }
                    //

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
    }
}
