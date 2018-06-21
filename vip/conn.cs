using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using vip.Windows;

namespace vip
{
    class conn
    {
        
        public static bool Init()
        {
            try
            {
                if (TestConnection())
                {
                    CreateTable("vip");
                    CreateAdminTable("admin");
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
        private static bool TestConnection()
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
        private static void CreateAdminTable(string tableName)
        {
            try
            {
                // Creating table....
                SQLiteTable tb = new SQLiteTable(tableName);
                tb.Columns.Add(new SQLiteColumn("ID", ColType.Integer, true, true, true, ""));
                tb.Columns.Add(new SQLiteColumn("adminName", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("adminPass", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("adminPower", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("LastLoginTime", ColType.Text));
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
                        ////
                        var dic = new Dictionary<string, object>();
                        dic["adminName"] = "admin";
                        dic["adminPass"] = Tools.StringToMD5Hash("123456");
                        dic["adminPower"] = "";
                        dic["LastLoginTime"] = "";
                        dic["CreateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        sh.Insert("admin", dic);
                        ///
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private static void CreateTable(string tableName)
        {
            try
            {
                // Creating table....
                SQLiteTable tb = new SQLiteTable(tableName);
                tb.Columns.Add(new SQLiteColumn("ID", ColType.Integer, true, true, true, ""));
                tb.Columns.Add(new SQLiteColumn("Name", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("Sex", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("Phone", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("Birthday", ColType.Text));
                tb.Columns.Add(new SQLiteColumn("Remarks", ColType.Text));

                tb.Columns.Add(new SQLiteColumn("TotalCost", ColType.Integer, false, false, true, "0"));
                tb.Columns.Add(new SQLiteColumn("Scores", ColType.Integer, false, false, true, "0"));
                tb.Columns.Add(new SQLiteColumn("TpnManScore", ColType.Integer, false, false, true, "0"));
                tb.Columns.Add(new SQLiteColumn("TpnWomanScore", ColType.Integer, false, false, true, "0"));
                tb.Columns.Add(new SQLiteColumn("XyScore", ColType.Integer, false, false, true, "0"));
                tb.Columns.Add(new SQLiteColumn("CmScore", ColType.Integer, false, false, true, "0"));

                
                tb.Columns.Add(new SQLiteColumn("ManShoeScore", ColType.Integer, false, false, true, "0"));
                tb.Columns.Add(new SQLiteColumn("WomanShoeScore", ColType.Integer, false, false, true, "0"));
                tb.Columns.Add(new SQLiteColumn("HatScore", ColType.Integer, false, false, true, "0"));
                tb.Columns.Add(new SQLiteColumn("BeltScore", ColType.Integer, false, false, true, "0"));
                tb.Columns.Add(new SQLiteColumn("BagScore", ColType.Integer, false, false, true, "0"));

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
    }
}
