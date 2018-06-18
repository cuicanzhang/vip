using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using vip.Windows;

namespace vip.Core
{
    class SqlAction
    {
        private static SQLiteConnection conn = new SQLiteConnection(config.DataSource);
        private static SQLiteCommand cmd = new SQLiteCommand();

        public static bool CheckLogin(string adminName, string pass)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                SQLiteHelper sh = new SQLiteHelper(cmd);
                var sql = string.Format("select * from admin where (adminName='{0}'and adminPass='{1}')", adminName, Tools.StringToMD5Hash(pass));
                DataTable dt = sh.Select(sql);
                if (dt.Rows.Count != 0)
                {
                    config.adminID = dt.Rows[0]["ID"].ToString();

                    var dic = new Dictionary<string, object>();
                    dic["LastLoginTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sh.Update("admin", dic, "adminName", config.adminID);
                    
                    config.vipCount = sh.ExecuteScalar<int>("select count(*) from admin").ToString();

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
            finally
            {
                conn.Close();
            }
        }
        public static bool AddAdmin(Dictionary<string, object> dic)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                SQLiteHelper sh = new SQLiteHelper(cmd);
                var sql = string.Format("select * from admin where (adminName='{0}')", dic["adminName"]);
                DataTable dt = sh.Select(sql);
                if (dt.Rows.Count == 0)
                {
                    sh.Insert("admin", dic);
                    return true;
                }
                else
                {
                    MessageBox.Show("管理员已存在");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        public static bool DeleteAdmin(string id)
        {
            try
            {            
                conn.Open();
                cmd.Connection = conn;
                SQLiteHelper sh = new SQLiteHelper(cmd);
                var sql = string.Format("DELETE FROM admin WHERE (ID='{0}')", id);
                DataTable dt = sh.Select(sql);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

            finally
            {
                conn.Close();
            }
        }
        public static bool ModifyAdmin(Dictionary<string, object> dic)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                SQLiteHelper sh = new SQLiteHelper(cmd);              
                    sh.Update("admin", dic, "ID", dic["ID"]);
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

            finally
            {
                conn.Close();
            }
        }
        public static DataTable SelectAdmin(string searchStr)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                SQLiteHelper sh = new SQLiteHelper(cmd);
                string sql = "";
                if (searchStr.Length != 0)
                {
                    sql = "select * from admin where(adminName='" + searchStr + "')";
                }
                else
                {
                    sql = "select * from admin";
                }
                DataTable dt = sh.Select(sql);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                DataTable dt = new DataTable();
                return dt;
            }
            finally
            {
                conn.Close();
            }

        }
        public static bool AddVip(Dictionary<string,object > dic)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                SQLiteHelper sh = new SQLiteHelper(cmd);
                //var sql= "select * from vip where (Name='"+ NameTB.Text+"' and Phone='"+ PhoneTB.Text+"')";
                var sql =string.Format( "select * from vip where (Phone='{0}')",dic["Phone"]);
                DataTable dt = sh.Select(sql);
                if (dt.Rows.Count == 0)
                {
                    sh.Insert("vip", dic);
                    return true;
                }
                else
                {
                    MessageBox.Show("会员已存在");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        public static bool DeleteVip(string id)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                SQLiteHelper sh = new SQLiteHelper(cmd);
                var sql = string.Format("DELETE FROM vip WHERE (ID='{0}')", id);
                DataTable dt = sh.Select(sql);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

            finally
            {
                conn.Close();
            }
        }

        public static bool VipModify(Dictionary<string, object> dic)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                SQLiteHelper sh = new SQLiteHelper(cmd);
                if (dic.ContainsKey("Phone"))
                {
                    var sql = string.Format("select * from vip where (ID<>'{0}' and Phone='{1}')", dic["ID"], dic["Phone"]);
                    DataTable dt = sh.Select(sql);
                    if (dt.Rows.Count == 0)
                    {
                        sh.Update("vip", dic, "ID", dic["ID"]);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("会员手机号码重复");
                        return false;
                    }
                }
                else
                {
                    sh.Update("vip", dic, "ID", dic["ID"]);
                    return true;
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

            finally
            {
                conn.Close();
            }
        }

        public static  bool ChangeScores(Dictionary<string, object> dic)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                SQLiteHelper sh = new SQLiteHelper(cmd);
                sh.Update("vip", dic, "ID", dic["ID"]);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        public static DataTable Select(string searchStr)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                SQLiteHelper sh = new SQLiteHelper(cmd);
                string sql = "";
                if (searchStr.Length == 11)
                {
                    sql = "select * from vip where(Phone='" + searchStr + "')";
                }
                else if (searchStr.Length > 0 && searchStr.Length < 11)
                {
                    sql = "select * from vip where(Name='" + searchStr + "')";
                }
                else
                {
                    sql = "select * from vip";
                }
                DataTable dt = sh.Select(sql);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                DataTable dt = new DataTable();
                return dt;
            }
            finally
            {
                conn.Close();
            }

        }
        
    }

}
