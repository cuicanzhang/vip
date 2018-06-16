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
            LoginWindow login = new LoginWindow();
            login.ShowDialog();
            if(login.DialogResult!=Convert.ToBoolean(1))
            {
                this.Close();
            }




        }
        public void reload(string str)
        {
            dispDataGrid.ItemsSource = Core.SqlAction.Select(str).DefaultView;
            //dispDataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
            dispDataGrid.SelectedIndex = 0;
            //dispDataGrid.Focus();

        }
        private void findAllBtn_Click(object sender, RoutedEventArgs e)
        {
            dispDataGrid.ItemsSource = Core.SqlAction.Select(serarchTB.Text).DefaultView;
            dispDataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
        }
        
        
        private void menuAction(string action)
        {
            try
            {
                if (action == "add")
                    {
                        Windows.Query.addWindow w = new Windows.Query.addWindow();
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
                            Windows.Query.modifyWindow w = new Windows.Query.modifyWindow(initDic(mySelectedElement));
                            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                            w.Owner = this;
                            w.ShowDialog();
                        }
                        if (action == "changeScore")
                        {
                            Windows.Query.changeScoreWindow w = new Windows.Query.changeScoreWindow(initDic(mySelectedElement));
                            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                            w.Owner = this;
                            w.ShowDialog();
                        }
                        if (action == "delete")
                        {
                            Windows.Query.deleteWindow w = new Windows.Query.deleteWindow(initDic(mySelectedElement));
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
    }
}
