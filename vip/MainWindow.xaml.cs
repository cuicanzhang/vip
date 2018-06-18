using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using vip.Windows;

namespace vip
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //数据库初始化
            conn.Init();
            /*
            StackPanel pannel = new StackPanel();
            Button button=new Button() { Content="button1"};
            TextBlock textblock = new TextBlock() { Text = "TextBlock1" };
            pannel.Children.Add(button);
            pannel.Children.Add(textblock);
            TabItem item = as TabItem;
            */

            LoginWindow login = new LoginWindow();
            login.ShowDialog();
            if(login.DialogResult!=Convert.ToBoolean(1))
            {
                this.Close();
            }
            Tools.birthdayDic();
            dataReLoad();
        }
        public void dataReLoad()
        {
            Core.SqlAction.SelectBirthday();
            vipCountLB.Content = config.vipCount;
            vipBirthdayCountLB.Content = config.vipBirthdayCount;

        }
        public void reload(string str)
        {
            dispDataGrid.ItemsSource = Core.SqlAction.Select(str).DefaultView;
            //dispDataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
            dispDataGrid.SelectedIndex = 0;
            //dispDataGrid.Focus();
            dataReLoad();
        }
        public void reloadAdmin(string str)
        {
            dispAdminDataGrid.ItemsSource = Core.SqlAction.SelectAdmin(str).DefaultView;
            //dispDataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
            dispDataGrid.SelectedIndex = 0;
            //dispDataGrid.Focus();
        }
        private void findAllBtn_Click(object sender, RoutedEventArgs e)
        {
            dispDataGrid.ItemsSource = Core.SqlAction.Select(serarchTB.Text).DefaultView;
            dispDataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
        }
        private void dispAllBtn_Click(object sender, RoutedEventArgs e)
        {
            dispDataGrid.ItemsSource = Core.SqlAction.Select("").DefaultView;
            dispDataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
        }
        private void DispBirthday_Click(object sender, RoutedEventArgs e)
        {
            dispDataGrid.ItemsSource = Core.SqlAction.SelectBirthday().DefaultView;
            dispDataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
        }
        private void menuAdminAction(string action)
        {
            try
            {
                if (action == "addAdmin")
                {
                    Windows.addAdminWindow w = new Windows.addAdminWindow();
                    w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    w.Owner = this;
                    w.ShowDialog();
                }
                else
                {
                    DataRowView mySelectedElement = (DataRowView)dispAdminDataGrid.SelectedItem;
                    if (mySelectedElement != null)
                    {

                        if (action == "modifyAdmin")
                        {
                            Windows.modifyAdminWindow w = new Windows.modifyAdminWindow(initAdminDic(mySelectedElement));
                            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                            w.Owner = this;
                            w.ShowDialog();
                        }
                        if (action == "deleteAdmin")
                        {
                            Windows.deleteAdminWindow w = new Windows.deleteAdminWindow(initAdminDic(mySelectedElement));
                            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                            w.Owner = this;
                            w.ShowDialog();
                        }                        
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dispAdminDataGrid.SelectedItem = null;
            }
        }
        private void menuAction(string action)
        {
            try
            {
                if (action == "add")
                    {
                        Windows.addWindow w = new Windows.addWindow();
                        w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        w.Owner = this;
                        w.ShowDialog();
                    }
                else
                {
                    DataRowView mySelectedElement = (DataRowView)dispDataGrid.SelectedItem;
                    if (mySelectedElement != null)
                    {
                        if (action == "modify")
                        {
                            Windows.modifyWindow w = new Windows.modifyWindow(initDic(mySelectedElement));
                            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                            w.Owner = this;
                            w.ShowDialog();
                        }
                        if (action == "changeScore")
                        {
                            Windows.changeScoreWindow w = new Windows.changeScoreWindow(initDic(mySelectedElement));
                            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                            w.Owner = this;
                            w.ShowDialog();
                        }
                        if (action == "delete")
                        {
                            Windows.deleteWindow w = new Windows.deleteWindow(initDic(mySelectedElement));
                            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                            w.Owner = this;
                            w.ShowDialog();
                        }      
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dispDataGrid.SelectedItem = null;
            }
        }
        private void addAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            menuAdminAction("addAdmin");
        }
        private void modifyAdminAction_Click(object sender, RoutedEventArgs e)
        {
            menuAdminAction("modifyAdmin");
        }
        private void deleteAdminAction_Click(object sender, RoutedEventArgs e)
        {
            menuAdminAction("deleteAdmin");
        }
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            menuAction("add");
        }
        private void modifyAction_Click(object sender, RoutedEventArgs e)
        {
            menuAction("modify");
        }
        private void changeAction_Click(object sender, RoutedEventArgs e)
        {
            menuAction("changeScore");
        }
        private void deleteAction_Click(object sender, RoutedEventArgs e)
        {
            menuAction("delete");
        }
        
        private void dispDataFridLoadingRow(object sender, DataGridRowEventArgs e)
        {
            //加载行
            e.Row.Header = e.Row.GetIndex() + 1;
        }
        private Dictionary<string,string> initDic(DataRowView mySelectedElement)
        {
            var dic = new Dictionary<string, string>();
            dic["ID"] = mySelectedElement[0].ToString();
            dic["Name"] = mySelectedElement[1].ToString();
            dic["Sex"] = mySelectedElement[2].ToString();
            dic["Phone"] = mySelectedElement[3].ToString();
            dic["Birthday"] = mySelectedElement[4].ToString();
            dic["Remarks"] = mySelectedElement[5].ToString();

            dic["Scores"] = mySelectedElement[6].ToString();
            dic["TpnManScore"] = mySelectedElement[7].ToString();
            dic["TpnWomanScore"] = mySelectedElement[8].ToString();
            dic["XyScore"] = mySelectedElement[9].ToString();
            dic["CmScore"] = mySelectedElement[10].ToString();

            dic["LastModiTime"] = mySelectedElement[11].ToString();
            dic["CreateTime"] = mySelectedElement[12].ToString();
            return dic;
        }
        private Dictionary<string, string> initAdminDic(DataRowView mySelectedElement)
        {
            var dic = new Dictionary<string, string>();
            dic["ID"] = mySelectedElement[0].ToString();
            dic["adminName"] = mySelectedElement[1].ToString();
            dic["LastLoginTime"] = mySelectedElement[4].ToString();
            dic["CreateTime"] = mySelectedElement[5].ToString();
            return dic;
        }
        private void dispAdminDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView mySelectedElement = (DataRowView)dispDataGrid.SelectedItem;
            if (mySelectedElement != null)
            {
                var dic = initDic(mySelectedElement);
                NameTB.Text = dic["adminName"];
                LastModiTime.Text = dic["LastLoginTime"];
                CreateTime.Text = dic["CreateTime"];
            }
        }
        private void dispDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView mySelectedElement = (DataRowView)dispDataGrid.SelectedItem;
            if (mySelectedElement != null)
            {
                var dic = initDic(mySelectedElement);
                NameTB.Text = dic["Name"];
                SexTB.Text = dic["Sex"];
                PhoneTB.Text=dic["Phone"];
                BirthdayTB.Text= dic["Birthday"];
                RemarksTB.Text=dic["Remarks"] ;

                ScoresTB.Text=dic["Scores"];
                TpnManScoreTB.Text=dic["TpnManScore"];
                TpnWomanScoreTB.Text=dic["TpnWomanScore"];
                XyScoreTB.Text=dic["XyScore"];
                CmScoreTB.Text=dic["CmScore"] ;

                LastModiTime.Text=dic["LastModiTime"];
                CreateTime.Text=dic["CreateTime"];

            }
        }
        private void checkNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Tools.isInputNumber(e))
            {
                //MessageBox.Show("请输入数字！");
            }
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
            var sdir = Environment.CurrentDirectory+ "\\vip.sqlite";
            var ddir = Environment.CurrentDirectory + "\\dbBack\\";
                if (!Directory.Exists(ddir))
            {
                Directory.CreateDirectory(ddir);
            }
            string pLocalFilePath = sdir;//要复制的文件路径
            string pSaveFilePath = ddir+ "vip["+ DateTime.Now.ToString("yyyyMMddHHmm") + "].sqlite";//指定存储的路径
            if (File.Exists(pLocalFilePath))//必须判断要复制的文件是否存在
            {
                File.Copy(pLocalFilePath, pSaveFilePath, true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
            }

            this.Close();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

                dispAdminDataGrid.ItemsSource = Core.SqlAction.SelectAdmin(adminSearchTB.Text).DefaultView;
                dispAdminDataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;

        }
    }
}
