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
                tb.Columns.Add(new SQLiteColumn("ID", ColType.Integer, true,true,true,""));
                tb.Columns.Add(new SQLiteColumn("Name", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("Sex", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("Phone", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("Birthday", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("Remarks", ColType.Text));
                
                tb.Columns.Add(new SQLiteColumn("Scores", ColType.Integer, false, false, true, "0"));
                tb.Columns.Add(new SQLiteColumn("TpnManScore", ColType.Integer, false, false, true, "0"));
                tb.Columns.Add(new SQLiteColumn("TpnWomanScore", ColType.Integer, false, false, true, "0"));
                tb.Columns.Add(new SQLiteColumn("XyScore", ColType.Integer, false, false, true, "0"));
                tb.Columns.Add(new SQLiteColumn("CmScore", ColType.Integer, false, false, true, "0"));

                tb.Columns.Add(new SQLiteColumn("LastModiTime", ColType.Text));
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
        void vipSelect(string searchStr)
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
            vipSelect(serarchTB.Text);        
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
                    Windows.Query.modifyWindow w = new Windows.Query.modifyWindow(dic);
                    w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    w.Owner = this;
                    w.ShowDialog();
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
        private void dispDataFridLoadingRow(object sender, DataGridRowEventArgs e)
        {
            //加载行
            e.Row.Header = e.Row.GetIndex() + 1;
        }
        private void addScore_CLick(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView mySelectedElement = (DataRowView)dispDataGrid.SelectedItem;
                if (mySelectedElement != null)
                {
                    var dic = new Dictionary<string, string>();
                    dic["Name"] = mySelectedElement[1].ToString();
                    dic["Sex"] = mySelectedElement[2].ToString();
                    dic["Phone"] = mySelectedElement[3].ToString();
                    dic["Birdthday"] = mySelectedElement[4].ToString();
                    dic["Remarks"] = mySelectedElement[5].ToString();

                    dic["Scores"] = mySelectedElement[6].ToString();
                    dic["TpnManScore"] = mySelectedElement[7].ToString();
                    dic["TpnWomanScore"] = mySelectedElement[8].ToString();
                    dic["XyScore"] = mySelectedElement[9].ToString();
                    dic["CmScore"] = mySelectedElement[10].ToString();

                    dic["LastModiTime"] = mySelectedElement[11].ToString();
                    dic["CreateTime"] = mySelectedElement[12].ToString();
                    Windows.Query.addScoreWindow w = new Windows.Query.addScoreWindow(dic);
                    w.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    w.Owner = this;
                    w.ShowDialog();
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
        private void subScore_CLick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("-");
        }

        private void dispDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
