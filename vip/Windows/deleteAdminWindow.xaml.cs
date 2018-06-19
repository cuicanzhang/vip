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
    public partial class deleteAdminWindow : Window
    {
        AdminInfo admin = new AdminInfo();
        public deleteAdminWindow()
        {
            InitializeComponent();
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }
        public deleteAdminWindow(Dictionary<string, string> dic)
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {          
            //此处加删除的操作
            if (admin.ID!= config.adminID)
            {
                if (Core.SqlAction.DeleteAdmin(admin.ID))
                {
                    var mainWindow = (MainWindow)Owner;
                    mainWindow.reloadAdmin("");
                    this.Close();
                    //MessageBox.Show("添加成功");
                }
            }
            else
            {
                MessageBox.Show("不能删除当前用户");
            }
                 
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
