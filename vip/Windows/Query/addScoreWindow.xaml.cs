using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace vip.Windows.Query
{
    /// <summary>
    /// addScoreWindow.xaml 的交互逻辑
    /// </summary>
    public partial class addScoreWindow : Window
    {
        public addScoreWindow()
        {
            InitializeComponent();
        }
        public addScoreWindow(Dictionary<string, string> dic)
        {
            InitializeComponent();
            NameLB.Content = dic["Name"];
            SexLB.Content = dic["Sex"];
            PhoneLB.Content = dic["Phone"];
            RemarksTB.Text = dic["Remarks"];

            ScoresLB.Content = dic["Scores"];
            tpnManScoreLB.Content = dic["TpnManScore"];
            tpnWomanScoreLB.Content = dic["TpnWomanScore"];
            xyScoreLB.Content = dic["XyScore"];
            cmScoreLB.Content = dic["CmScore"];


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void subScore()
        {
            if (addScoresTB.Text.Replace(" ", "") != "")
            {
                int int1 = int.Parse(ScoresLB.Content.ToString());
                int int2 = int.Parse(addScoresTB.Text.Replace(" ", ""));
                finalScoresLB.Content = int1 + int2;
            }
            else
            {
                finalScoresLB.Content = ScoresLB.Content.ToString();
            }
            
           
            
        }
        private bool vipScoreModify()
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
                        //var sql = "select ID from vip where (Name='" + vipName + "' and Phone='" + vipPhone + "')";
                        //var sql = "select ID from vip where (Phone='" + vipPhone + "')";
                        //DataTable dt = sh.Select(sql);
                        //if (dt.Rows.Count != 0)
                        //{
                        var dic = new Dictionary<string, object>();
                        
                        dic["Scores"] = finalScoresLB.Content;
                        
                        if (finalScoresLB.Content.ToString() != "")
                        {
                            if (finalScoresLB.Content.ToString() != ScoresLB.Content.ToString())
                            {
                                dic["LastModiTime"] = DateTime.Now.ToLocalTime().ToString();
                                sh.Update("vip", dic, "Phone", PhoneLB.Content);
                                return true;
                            }
                        }

                        
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
            }
        }
        private void addScoresTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            subScore();
        }
        private void checkInput(object sender, TextCompositionEventArgs e)
        {
            Windows.Tools.checkInput(e);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (vipScoreModify())
            {
                this.Close();
                //MessageBox.Show("添加成功");
            }
        }
        

        private void addScoresTB_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Tools.isInputNumber(e))
            {
                //MessageBox.Show("请输入数字！");
            }
        }
        
       
    }

}
