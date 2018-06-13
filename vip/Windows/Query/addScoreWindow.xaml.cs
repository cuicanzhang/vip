﻿using System;
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

        private bool addScore()
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

        private void checkInput(object sender, TextCompositionEventArgs e)
        {
            Windows.Tools.checkInput(e);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (addScore())
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

        private void RemarksTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void ScoresTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            subScore();
        }
        private void subScore()
        {
            var addtpnManScore= addtpnManScoreTB.Text.Replace(" ", "");
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
            addScoreLB.Content = (int.Parse(addtpnManScore) + int.Parse(addtpnWomanScore) + int.Parse(addxyScore) + int.Parse(addcmScore)).ToString();
            finalScoreLB.Content = (int.Parse(ScoresLB.Content.ToString()) + int.Parse(addScoreLB.Content.ToString())).ToString();
            /*
        scoresTB.Text = (int.Parse(tpnManScore)
                        + int.Parse(tpnWomanScore)
                        + int.Parse(xyScore)
                        + int.Parse(cmScore)).ToString();
                        */
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
