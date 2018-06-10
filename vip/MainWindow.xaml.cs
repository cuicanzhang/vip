﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using System.Windows.Controls;

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
            if (createNewSQLiteDatabase())
            {
                CreateTable("vip");
            }
            

        }



        private bool createNewSQLiteDatabase()
        {
            config.DatabaseFile = "vip.sqlite";
            try
            {
                if (TestConnection())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

        }
        bool TestConnection()
        {
            try
            {
                if (!File.Exists(config.DatabaseFile))
                {
                    using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                    {
                        conn.Open();
                        conn.Close();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        private void CreateTable(string tableName)
        {
            try
            {
                // Creating table....
                SQLiteTable tb = new SQLiteTable(tableName);
                tb.Columns.Add(new SQLiteColumn("id", ColType.Integer, true,true,true,""));
                tb.Columns.Add(new SQLiteColumn("Name", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("Scores", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("Phone", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("Remarks", ColType.Text));
                // Execute Table Creation
                using (SQLiteConnection conn = new SQLiteConnection(config.DataSource))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;

                        SQLiteHelper sh = new SQLiteHelper(cmd);

                        sh.DropTable(tableName);
                        sh.CreateTable(tb);

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void vipSelect()
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
                    
                        var sql = "select * from vip";
                        DataTable dt = sh.Select(sql);
                        /*
                        DataRow row = dt.NewRow();
                        row["ID"] = 11;
                        row["Name"] = "第三个";
                        row["Scores"] = 333;
                        row["Phone"] = 33333333333;
                        row["Remarks"] = 3333333333;
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
            vipSelect();
        }
        
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Query.addWindow w = new Windows.Query.addWindow();
            w.Show();
        }
    }
}
