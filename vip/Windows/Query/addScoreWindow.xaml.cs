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
        VipInfo vip = new VipInfo();
        public addScoreWindow()
        {
            InitializeComponent();
        }
        public addScoreWindow(Dictionary<string, string> dic)
        {
            InitializeComponent();
            //vip初始化
            vip.ID = dic["ID"];
            vip.Name = dic["Name"];
            vip.Sex = dic["Sex"];
            vip.Phone = dic["Phone"];
            vip.Birthday = dic["Birthday"];
            vip.Remarks = dic["Remarks"];
            vip.Scores = dic["Scores"];
            vip.tpnManScore = dic["TpnManScore"];
            vip.tpnWomanScore = dic["TpnWomanScore"];
            vip.xyScore = dic["XyScore"];
            vip.cmScore = dic["CmScore"];

            //加载控件数据
            NameLB.Content = vip.Name;
            SexLB.Content = vip.Sex;
            PhoneLB.Content = vip.Phone;
            BirthdayLB.Content = vip.Birthday;
            RemarksTB.Text = vip.Remarks;
            ScoresLB.Content = vip.Scores;
            tpnManScoreLB.Content = vip.tpnManScore;
            tpnWomanScoreLB.Content = vip.tpnWomanScore;
            xyScoreLB.Content = vip.xyScore;
            cmScoreLB.Content = vip.cmScore;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private bool changeScores()
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
                        if (finalScoreLB.Content.ToString() != "")
                        {
                            if (finalScoreLB.Content.ToString() != ScoresLB.Content.ToString())
                            {
                                var dic = new Dictionary<string, object>();
                                dic["Scores"] = finalScoreLB.Content;
                                dic["TpnManScore"] = tpnManFinalScoreLB.Content;
                                dic["TpnWomanScore"] = tpnWomanFinalScoreLB.Content;
                                dic["XyScore"] = xyFinalScoreLB.Content;
                                dic["CmScore"] = cmFinalScoreLB.Content;
                                dic["LastModiTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (tpnManFinalScoreLB.Content.ToString()!="0"
                || tpnWomanFinalScoreLB.Content.ToString() != "0"
                || xyFinalScoreLB.Content.ToString() != "0"
                || cmFinalScoreLB.Content.ToString() != "0")
            {
                if (changeScores())
                {
                    this.Close();
                    //MessageBox.Show("添加成功");
                }
            }
            else
            {
                this.Close();
            }
            
        }
        

        private void addScoresTB_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Tools.isInputNumber(e))
            {
                //MessageBox.Show("请输入数字！");
            }
        }

        private void ScoresTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            addScore();
        }
        private void addScore()
        {
            if (scoresTC.SelectedIndex == 0)
            {
                var addtpnManScore = addtpnManScoreTB.Text.Replace(" ", "");
                var addtpnWomanScore = addtpnWomanScoreTB.Text.Replace(" ", "");
                var addxyScore = addxyScoreTB.Text.Replace(" ", "");
                var addcmScore = addcmScoreTB.Text.Replace(" ", "");

                if (addtpnManScore == "")
                {
                    addtpnManScore = "0";
                }
                if (addtpnWomanScore == "")
                {
                    addtpnWomanScore = "0";
                }
                if (addxyScore == "")
                {
                    addxyScore = "0";
                }
                if (addcmScore == "")
                {
                    addcmScore = "0";
                }

                tpnManFinalScoreLB.Content = (int.Parse(tpnManScoreLB.Content.ToString()) + int.Parse(addtpnManScore)).ToString();
                tpnWomanFinalScoreLB.Content = (int.Parse(tpnWomanScoreLB.Content.ToString()) + int.Parse(addtpnWomanScore)).ToString();
                xyFinalScoreLB.Content = (int.Parse(xyScoreLB.Content.ToString()) + int.Parse(addxyScore)).ToString();
                cmFinalScoreLB.Content = (int.Parse(cmScoreLB.Content.ToString()) + int.Parse(addcmScore)).ToString();
                tempScoreDataLB.Content = (int.Parse(addtpnManScore) + int.Parse(addtpnWomanScore) + int.Parse(addxyScore) + int.Parse(addcmScore)).ToString();
                finalScoreLB.Content = (int.Parse(ScoresLB.Content.ToString()) + int.Parse(tempScoreDataLB.Content.ToString())).ToString();
            }
            if (scoresTC.SelectedIndex == 1)
            {
                var subScore = subScoreTB.Text.Replace(" ", "");
                if (subScore == "")
                {
                    subScore = "0";
                }
                if (int.Parse(subScore)<= int.Parse(ScoresLB.Content.ToString()))
                {
                    tempScoreDataLB.Content = (int.Parse(subScore)).ToString();
                    finalScoreLB.Content = (int.Parse(ScoresLB.Content.ToString()) - int.Parse(subScore)).ToString();
                }
                else
                {
                    subScoreTB.Text = ScoresLB.Content.ToString();
                }
                
            }



        }
        private void checkNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Tools.isInputNumber(e))
            {
                //MessageBox.Show("请输入数字！");
            }
        }

        private void scoresTC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (scoresTC.SelectedIndex == 1 )
            {
                tempScoreLB.Content = "兑换积分";
                if (addtpnManScoreTB.Text != "" || addtpnWomanScoreTB.Text != "" || addxyScoreTB.Text != "" || addcmScoreTB.Text != "")
                {
                    addtpnManScoreTB.Text = "";
                    addtpnWomanScoreTB.Text = "";
                    addxyScoreTB.Text = "";
                    addcmScoreTB.Text = "";
                    tpnManFinalScoreLB.Content = "";
                    tpnWomanFinalScoreLB.Content = "";
                    xyFinalScoreLB.Content = "";
                    cmFinalScoreLB.Content = "";
                    tempScoreDataLB.Content = "";
                }                 
            }

            if (scoresTC.SelectedIndex == 0)
            {
                tempScoreLB.Content = "本次积分";
                if (subScoreTB.Text != "")
                {
                    subScoreTB.Text = "";
                    tempScoreDataLB.Content = "";
                }
                    
            }

        }
    }

}
