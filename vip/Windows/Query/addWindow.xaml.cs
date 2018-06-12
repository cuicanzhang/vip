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
            //加载会员性别
            loadSex();

        }
        private void loadSex()
        {
            SexCB.Items.Add("男");
            SexCB.Items.Add("女");

        }
        private bool vipAdd()
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
                            dic["Scores"] = ScoresTB.Text.Replace(" ", "");
                            dic["Phone"] = PhoneTB.Text.Replace(" ", "");
                            dic["LastModiTime"] = DateTime.Now.ToLongDateString().ToString();
                            dic["Remarks"] = RemarksTB.Text;
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
            else if (PhoneTB.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("[电话] 必须填写");
            }
            else
            {
                if (vipAdd())
                {
                    this.Close();
                    //MessageBox.Show("添加成功");
                }
            }
            
        }

        private void SexCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
