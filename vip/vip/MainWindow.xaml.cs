using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace vip
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private static SqLiteHelper conn;
        public MainWindow()
        {
            InitializeComponent();
            //vipInfoDG.ItemsSource = Employee.GetEmployees();//绑定数据源
            conn = new SqLiteHelper("data source=mydb.db");
            //创建名为table1的数据表
            conn.CreateTable("vip", new string[] { "ID", "Name", "Scores", "Phone","Remark" }, new string[] { "INTEGER", "TEXT", "INTEGER", "TEXT", "TEXT" });
            conn.InsertValues("vip", new string[] { "1", "张三", "5000", "13800138000","firest" });
            conn.InsertValues("vip", new string[] { "2", "李四", "1000", "13888888888","" });


            //更新数据，将Name="张三"的记录中的Name改为"Zhang3"
            conn.UpdateValues("vip", new string[] { "Name" }, new string[] { "张三" }, "Name", "Zhang3");

            //删除Name="张三"且Scores=1000的记录,DeleteValuesOR方法类似
            conn.DeleteValuesAND("vip", new string[] { "Name", "Scores" }, new string[] { "张三", "2000" }, new string[] { "=", "=" });


            //读取整张表
            SQLiteDataReader reader = conn.ReadFullTable("vip");
            while (reader.Read())
            {
                //SQLiteDataAdapter
                //读取ID
                Log("" + reader.GetInt32(reader.GetOrdinal("ID")));
                //读取Name
                Log("" + reader.GetString(reader.GetOrdinal("Name")));
                //读取Scores
                Log("" + reader.GetInt32(reader.GetOrdinal("Scores")));
                //读取Phone
                Log(reader.GetString(reader.GetOrdinal("Phone")));
                //读取Remark
                Log(reader.GetString(reader.GetOrdinal("Remark")));
            }
        }

        void Log(string s)
        {
            debugRTB.AppendText(s.ToString());
        }
        //写一个刷新数据的方法(跟查看数据一样)


    }
}
