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
        public modifyWindow()
        {
            InitializeComponent();
            
        }

        public modifyWindow(DataRowView mySelectedElement)
        {
            InitializeComponent();
            /*
            var dic = new Dictionary<string, object>();            
            dic["Name"] = mySelectedElement[1];
            dic["Scores"] = mySelectedElement[2];
            dic["Phone"] = mySelectedElement[3];
            dic["Remarks"] = mySelectedElement[4];
            */
            
            NameTB.Text = mySelectedElement[1].ToString();
            ScoresTB.Text = mySelectedElement[2].ToString();
            PhoneTB.Text = mySelectedElement[3].ToString();
            RemarksTB.Text = mySelectedElement[4].ToString();
            
            
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
                        var sql = "select * from vip where (Name='" + NameTB.Text + "' and Phone='" + PhoneTB.Text + "')";
                        DataTable dt = sh.Select(sql);
                        if (dt.Rows.Count == 0)
                        {
                            int count = sh.ExecuteScalar<int>("select count(*) from vip;") + 1;
                            var dic = new Dictionary<string, object>();
                            dic["Name"] = NameTB.Text;
                            dic["Scores"] = ScoresTB.Text;
                            dic["Phone"] = PhoneTB.Text;
                            dic["Remarks"] = RemarksTB.Text;
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
            if (vipModify())
            {
                this.Close();
                //MessageBox.Show("添加成功");
            }
        }
    }
}
