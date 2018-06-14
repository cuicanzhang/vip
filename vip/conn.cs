using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
    }
}
