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
using vip.Windows;

namespace vip
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
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
        private void txt_Pwd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    btn_login_Click(btn_login, null);
                    break;

                default:
                    break;
            }
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            if(adminNameLB.Text.Trim()!=""&& adminPassPB.Password != "")
            {
                if (Core.SqlAction.CheckLogin(adminNameLB.Text.Trim(), adminPassPB.Password))
                {
                    this.DialogResult = Convert.ToBoolean(1);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("账号或密码错误！");
                }
            }
            else
            {
                MessageBox.Show("请填写用户名和密码！");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
