using System;
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
                tb.Columns.Add(new SQLiteColumn("Sex", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("Scores", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("Phone", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("LastModiTime", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("Remarks", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("CreateTime", ColType.Text));
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
                        
                        DataRow row = dt.NewRow();
                        /*
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

            vipSelect();

        }
        
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Windows.Query.addWindow w = new Windows.Query.addWindow();
            w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            w.Owner = this;
            w.ShowDialog();
        }
        
        private void dispDataGrid_MouseDoubleClick(object sender, RoutedEventArgs e)
        {

            try
            {
                DataRowView mySelectedElement = (DataRowView)dispDataGrid.SelectedItem;
                if (mySelectedElement != null)
                {
                    var dic = new Dictionary<string, string>();
                    dic["Name"] = mySelectedElement[1].ToString();
                    dic["Sex"] = mySelectedElement[2].ToString();
                    dic["Scores"] = mySelectedElement[3].ToString();
                    dic["Phone"] = mySelectedElement[4].ToString();
                    dic["LastModiTime"] = mySelectedElement[5].ToString();
                    dic["Remarks"] = mySelectedElement[6].ToString();
                    dic["CreateTime"] = mySelectedElement[7].ToString();
                    Windows.Query.modifyWindow w = new Windows.Query.modifyWindow(dic);
                    w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    w.Owner = this;
                    w.ShowDialog();
                }
            }
            catch
            {

            }
            finally
            {
                dispDataGrid.SelectedItem = null;
            }
            
            

        }
        private void dispDataFridLoadingRow(object sender, DataGridRowEventArgs e)
        {
            //加载行
            e.Row.Header = e.Row.GetIndex() + 1;
        }
        private void addScore_CLick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("+");
        }
        private void subScore_CLick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("-");
        }


    }
}
