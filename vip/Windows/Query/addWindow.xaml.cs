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
        }
        void vipAdd()
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
                        var sql= "select * from vip where (Name='"+ NameTB.Text+"' and Phone='"+ PhoneTB.Text+"')";
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
                        }
                        else
                        {
                            MessageBox.Show("会员已存在");
                        }
                        
                                             
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
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
            vipAdd();
        }
    }
}
