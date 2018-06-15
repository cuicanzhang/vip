﻿using System;
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




        }
        public void reload(string str)
        {
            Select(str);
            dispDataGrid.SelectedIndex = 0;
            //dispDataGrid.Focus();

        }
        void Select(string searchStr)
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
                        string sql="";
                        if (searchStr.Length == 11)
                        {
                            sql = "select * from vip where(Phone='" + searchStr + "')";
                        }
                        else if (searchStr.Length>0&&searchStr.Length < 11)
                        {
                            sql = "select * from vip where(Name='" + searchStr + "')";
                        }
                        else 
                        {
                            sql = "select * from vip";
                        }
                        DataTable dt = sh.Select(sql);
                        /*
                        DataRow row = dt.NewRow();
                        
                        row["Sex"] = 11;
                        row["Name"] = "第三个";
                        row["Scores"] = 333;
                        row["Phone"] = 33333333333;
                        row["LastModiTime"] = "11111111";
                        row["Remarks"] = 3333333333;
                        row["CreateTime"] = "11111111";
                        dt.Rows.Add(row);
                       */

                        dispDataGrid.ItemsSource = dt.DefaultView;
                        dispDataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;

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
        private void findAllBtn_Click(object sender, RoutedEventArgs e)
        {
            Select(serarchTB.Text);        
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


    }
}
