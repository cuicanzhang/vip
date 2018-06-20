using System;
using System.Collections.Generic;
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
    /// addAdminWindows.xaml 的交互逻辑
    /// </summary>
    public partial class modifyAdminWindow : Window
    {
        AdminInfo admin = new AdminInfo();
        public modifyAdminWindow()
        {
            InitializeComponent();
        }
        public modifyAdminWindow(Dictionary<string, string> dic)
        {
            InitializeComponent();
            //vip初始化
            admin.ID = dic["ID"];
            admin.adminName = dic["adminName"];
            admin.LastLoginTime = dic["LastLoginTime"];
            admin.CreateTime = dic["CreateTime"];

            //加载控件数据
            adminNameTB.Text = admin.adminName;

        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dic = new Dictionary<string, object>();
            dic["ID"] = admin.ID;

            if (adminNameTB.Text != admin.adminName)
            {
                dic["adminName"] = adminNameTB.Text;
            }

            if (adminRePassPB.Password != "" || adminPassPB.Password != "")
            {
                if (adminRePassPB.Password == adminPassPB.Password)
                {
                    dic["adminPass"] = Tools.StringToMD5Hash(adminPassPB.Password);
                }
                else
                {
                    MsgBoxWindow.Show("提示：", "两次输入的密码必须相同！");
                    //MessageBox.Show("两次输入的密码必须相同");
                }
            }
            

            if (dic.Count > 1)
            {
                if (Core.SqlAction.ModifyAdmin(dic))
                {
                    var mainWindow = (MainWindow)Owner;
                    mainWindow.reloadAdmin(adminNameTB.Text.Replace(" ", ""));
                    this.Close();
                    //MessageBox.Show("添加成功");
                }
            }



        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
