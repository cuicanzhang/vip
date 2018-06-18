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
    public partial class addAdminWindow : Window
    {
        public addAdminWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
                if (adminNameTB.Text.Replace(" ", "") == "")
                {
                    MessageBox.Show("[用户名] 必须填写");
                }
                else if (adminPassPB.Password == "")
                {
                    MessageBox.Show("[用户密码] 必须填写");
                }
                else if (adminRePassPB.Password != adminPassPB.Password)
                {
                    MessageBox.Show("两次输入的密码不一致！");
                }
                else
                {
                    var dic = new Dictionary<string, object>();
                    dic["adminName"] = adminNameTB.Text.Replace(" ", "");
                    dic["adminPass"] =Tools.StringToMD5Hash(adminPassPB.Password);
                    dic["CreateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    if (Core.SqlAction.AddAdmin(dic))
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
