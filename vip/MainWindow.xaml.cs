using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
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
        /// <summary>
        /// 该函数设置由不同线程产生的窗口的显示状态。
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分。</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。</returns>
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary>
        /// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。系统给创建前台窗口的线程分配的权限稍高于其他线程。
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄。</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零。</returns>
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;
        /// <summary>
        /// 获取正在运行的实例，没有运行的实例返回null;
        /// </summary>
        private static Process RuningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] Processes = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (Process process in Processes)
            {
                if (process.Id != currentProcess.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == currentProcess.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 显示已运行的程序。
        /// </summary>
        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL); //显示，可以注释掉
            SetForegroundWindow(instance.MainWindowHandle);            //放到前端
        }

        public MainWindow()
        {
            Process process = RuningInstance();
            if (process == null)
            {
                InitializeComponent();
                //数据库初始化
                conn.Init();
                LoginWindow login = new LoginWindow();
                login.ShowDialog();
                if (login.DialogResult != Convert.ToBoolean(1))
                {
                    this.Close();
                }
                Tools.birthdayDic();
                dataReLoad();

            }
            else
            {
                //MessageBox.Show("应用程序已经在运行中。。。");
                HandleRunningInstance(process);
                //System.Threading.Thread.Sleep(1000);
                System.Environment.Exit(1);
            }

        }
        public void clearVipInfo()
        {
            NameTB.Text = "";
            SexTB.Text = "";
            BirthdayTB.Text = "";
            RemarksTB.Text = "";
            PhoneTB.Text = "";
            ScoresTB.Text = "";
            CreateTime.Text = "";
            LastModiTime.Text = "";
            TotalCostLB.Content = "";
            TpnManScoreTB.Text = "";
            TpnWomanScoreTB.Text = "";
            XyScoreTB.Text = "";
            CmScoreTB.Text = "";
            ManShoeScoreTB.Text = "";
            WomanShoeScoreTB.Text = "";
            HatScoreTB.Text = "";
            BeltScoreTB.Text = "";
            BagScoreTB.Text = "";
        }
        public void dataReLoad()
        {
            Core.SqlAction.SelectBirthday();
            vipCountLB.Content = config.vipCount;
            vipBirthdayCountLB.Content = config.vipBirthdayCount;

        }
        public void reloadNone(string str)
        {
            dispDataGrid.ItemsSource = Core.SqlAction.Select(str).DefaultView;
            clearVipInfo();
            dataReLoad();
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
            clearVipInfo();
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

            dic["TotalCost"] = mySelectedElement[6].ToString();
            dic["Scores"] = mySelectedElement[7].ToString();
            dic["TpnManScore"] = mySelectedElement[8].ToString();
            dic["TpnWomanScore"] = mySelectedElement[9].ToString();
            dic["XyScore"] = mySelectedElement[10].ToString();
            dic["CmScore"] = mySelectedElement[11].ToString();

            dic["ManShoeScore"] = mySelectedElement[12].ToString();
            dic["WomanShoeScore"] = mySelectedElement[13].ToString();
            dic["HatScore"] = mySelectedElement[14].ToString();
            dic["BeltScore"] = mySelectedElement[15].ToString();
            dic["BagScore"] = mySelectedElement[16].ToString();


            dic["LastModiTime"] = mySelectedElement[17].ToString();
            dic["CreateTime"] = mySelectedElement[18].ToString();
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

                TotalCostLB.Content = dic["TotalCost"];
                ScoresTB.Text=dic["Scores"];
                TpnManScoreTB.Text=dic["TpnManScore"];
                TpnWomanScoreTB.Text=dic["TpnWomanScore"];
                XyScoreTB.Text=dic["XyScore"];
                CmScoreTB.Text=dic["CmScore"] ;

                ManShoeScoreTB.Text = dic["ManShoeScore"];
                WomanShoeScoreTB.Text = dic["WomanShoeScore"];
                HatScoreTB.Text = dic["HatScore"];
                BeltScoreTB.Text = dic["BeltScore"];
                BagScoreTB.Text = dic["BagScore"];

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
            var ddir = Environment.CurrentDirectory + "\\dbBack\\" +  DateTime.Now.ToString("yyyyMM") +"\\"+ DateTime.Now.ToString("dd") + "\\";
                if (!Directory.Exists(ddir))
            {
                Directory.CreateDirectory(ddir);
            }
            string pLocalFilePath = sdir;//要复制的文件路径
            string pSaveFilePath = ddir+ "vip["+ DateTime.Now.ToString("HHmm") + "].sqlite";//指定存储的路径
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

        private void ButtonMinimized_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void XyScoreTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
