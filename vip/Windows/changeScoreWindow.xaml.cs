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

namespace vip.Windows
{
    /// <summary>
    /// changeScoreWindow.xaml 的交互逻辑
    /// </summary>
    public partial class changeScoreWindow : Window
    {
        VipInfo vip = new VipInfo();
        public changeScoreWindow()
        {
            InitializeComponent();
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }
        public changeScoreWindow(Dictionary<string, string> dic)
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
            vip.manShoeScore = dic["ManShoeScore"];
            vip.womanShoeScore = dic["WomanShoeScore"];
            vip.hatScore = dic["HatScore"];
            vip.beltScore = dic["BeltScore"];
            vip.bagScore = dic["BagScore"];
            //加载控件数据

            if (vip.Sex == "男")
            {
                NameLB.Content = vip.Name + " （先生）";
            }
            else if (vip.Sex == "女")
            {
                NameLB.Content = vip.Name + " （女士）";
            }
            else
            {
                MessageBox.Show("异常");
            }
            //SexLB.Content = vip.Sex;
            PhoneLB.Content = vip.Phone;
            BirthdayLB.Content = vip.Birthday;
            RemarksTB.Text = vip.Remarks;
            ScoresLB.Content = vip.Scores;
            finalScoreLB.Content=vip.Scores;

            tpnManScoreLB.Content = vip.tpnManScore;
            tpnWomanScoreLB.Content = vip.tpnWomanScore;
            xyScoreLB.Content = vip.xyScore;
            cmScoreLB.Content = vip.cmScore;
            manShoeScoreLB.Content = vip.manShoeScore;
            womanShoeScoreLB.Content = vip.womanShoeScore;
            hatScoreLB.Content = vip.hatScore;
            beltScoreLB.Content = vip.beltScore;
            bagScoreLB.Content = vip.bagScore;


            tpnManScoreTLB.Content = vip.tpnManScore;
            tpnWomanScoreTLB.Content = vip.tpnWomanScore;
            xyScoreTLB.Content = vip.xyScore;
            cmScoreTLB.Content = vip.cmScore;
            //manShoeScoreTLB.Content = vip.manShoeScore;
            //womanShoeScoreTLB.Content = vip.womanShoeScore;
            //hatScoreTLB.Content = vip.hatScore;
            //beltScoreTLB.Content = vip.beltScore;
            //bagScoreTLB.Content = vip.bagScore;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

           

        private void addScoresTB_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Tools.isInputNumber(e))
            {
                //MessageBox.Show("请输入数字！");
            }
        }
        private void ScoresAdd_TextChanged(object sender, TextChangedEventArgs e)
        {
            ScoresAdd();
        }
        private void ScoresSub_TextChanged(object sender, TextChangedEventArgs e)
        {
            ScoresSub();
        }
        private void ScoresExchange_TextChanged(object sender, TextChangedEventArgs e)
        {
            ScoresExchange();
        }
        private void ScoresAdd()
        {
            var addtpnManScore = addtpnManScoreTB.Text.Replace(" ", "");
            var addtpnWomanScore = addtpnWomanScoreTB.Text.Replace(" ", "");
            var addxyScore = addxyScoreTB.Text.Replace(" ", "");
            var addcmScore = addcmScoreTB.Text.Replace(" ", "");

            var addmanShoeScore = addmanShoeScoreTB.Text.Replace(" ", "");
            var addwomanShoeScore = addwomanShoeScoreTB.Text.Replace(" ", "");
            var addhatScore = addhatScoreTB.Text.Replace(" ", "");
            var addbeltScore = addbeltScoreTB.Text.Replace(" ", "");
            var addbagScore = addbagScoreTB.Text.Replace(" ", "");

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
            if (addmanShoeScore == "")
            {
                addmanShoeScore = "0";
            }
            if (addwomanShoeScore == "")
            {
                addwomanShoeScore = "0";
            }
            if (addhatScore == "")
            {
                addhatScore = "0";
            }
            if (addbeltScore == "")
            {
                addbeltScore = "0";
            }
            if (addbagScore == "")
            {
                addbagScore = "0";
            }
            tpnManFinalScoreLB.Content = (int.Parse(tpnManScoreLB.Content.ToString()) + int.Parse(addtpnManScore)).ToString();
            tpnWomanFinalScoreLB.Content = (int.Parse(tpnWomanScoreLB.Content.ToString()) + int.Parse(addtpnWomanScore)).ToString();
            xyFinalScoreLB.Content = (int.Parse(xyScoreLB.Content.ToString()) + int.Parse(addxyScore)).ToString();
            cmFinalScoreLB.Content = (int.Parse(cmScoreLB.Content.ToString()) + int.Parse(addcmScore)).ToString();
            manShoeFinalScoreLB.Content = (int.Parse(manShoeScoreLB.Content.ToString()) + int.Parse(addmanShoeScore)).ToString();
            womanShoeFinalScoreLB.Content = (int.Parse(womanShoeScoreLB.Content.ToString()) + int.Parse(addwomanShoeScore)).ToString();
            hatFinalScoreLB.Content = (int.Parse(hatScoreLB.Content.ToString()) + int.Parse(addhatScore)).ToString();
            beltFinalScoreLB.Content = (int.Parse(beltScoreLB.Content.ToString()) + int.Parse(addbeltScore)).ToString();
            bagFinalScoreLB.Content = (int.Parse(bagScoreLB.Content.ToString()) + int.Parse(addbagScore)).ToString();

            tempScoreDataLB.Content = (int.Parse(addtpnManScore) 
                                        + int.Parse(addtpnWomanScore) 
                                        + int.Parse(addxyScore) 
                                        + int.Parse(addcmScore)
                                        + int.Parse(addmanShoeScore)
                                        + int.Parse(addwomanShoeScore)
                                        + int.Parse(addhatScore)
                                        + int.Parse(addbeltScore)
                                        + int.Parse(addbagScore)
                                        ).ToString();
            finalScoreLB.Content = (int.Parse(ScoresLB.Content.ToString()) + int.Parse(tempScoreDataLB.Content.ToString())).ToString();
        }
        private void ScoresExchange()
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
        private void ScoresSub()
        {
            var subtpnManScore = subtpnManScoreTB.Text.Replace(" ", "");
            var subtpnWomanScore = subtpnWomanScoreTB.Text.Replace(" ", "");
            var subxyScore = subxyScoreTB.Text.Replace(" ", "");
            var subcmScore = subcmScoreTB.Text.Replace(" ", "");

            var subtpnManScore1 = subtpnManScoreTB.Text.Replace(" ", "");
            var subtpnWomanScore1 = subtpnWomanScoreTB.Text.Replace(" ", "");
            var subxyScore1 = subxyScoreTB.Text.Replace(" ", "");
            var subcmScore1 = subcmScoreTB.Text.Replace(" ", "");

            if (subtpnManScore == "")
            {
                subtpnManScore = "0";
                subtpnManScore1 = "0";
            
            }
            if (subtpnWomanScore == "")
            {
                subtpnWomanScore = "0";
                subtpnWomanScore1 = "0";
           
            }
            if (subxyScore == "")
            {
                subxyScore = "0";
                subxyScore1 = "0";
          
            }
            if (subcmScore == "")
            {
                subcmScore = "0";
                subcmScore1 = "0";
    
            }

            if (int.Parse(subtpnManScore) <= int.Parse(tpnManScoreTLB.Content.ToString()))
            {
                tpnManFinalScoreTLB.Content = (int.Parse(tpnManScoreTLB.Content.ToString()) - int.Parse(subtpnManScore)).ToString();
                
            }
            else
            {
                subtpnManScoreTB.Text = tpnManScoreTLB.Content.ToString();
            }

            if (int.Parse(subtpnWomanScore) <= int.Parse(tpnWomanScoreTLB.Content.ToString()))
            {
                tpnWomanFinalScoreTLB.Content = (int.Parse(tpnWomanScoreTLB.Content.ToString()) - int.Parse(subtpnWomanScore)).ToString();
                
            }
            else
            {
                subtpnWomanScoreTB.Text = tpnWomanScoreTLB.Content.ToString();
            }

            if (int.Parse(subxyScore) <= int.Parse(xyScoreTLB.Content.ToString()))
            {
                xyFinalScoreTLB.Content = (int.Parse(xyScoreTLB.Content.ToString()) - int.Parse(subxyScore)).ToString();
                
            }
            else
            {
                subxyScoreTB.Text = xyScoreTLB.Content.ToString();
            }

            if (int.Parse(subcmScore) <= int.Parse(cmScoreTLB.Content.ToString()))
            {
                cmFinalScoreTLB.Content = (int.Parse(cmScoreTLB.Content.ToString()) - int.Parse(subcmScore)).ToString();
                
            }
            else
            {
                subcmScoreTB.Text = cmScoreTLB.Content.ToString();
            }


            tempScoreDataLB.Content = (int.Parse(subtpnManScore1)
                                        + int.Parse(subtpnWomanScore1)
                                        + int.Parse(subxyScore1)
                                        + int.Parse(subcmScore1)).ToString();
            var aa = ScoresLB.Content.ToString();
            var bb = tempScoreDataLB.Content.ToString();
            finalScoreLB.Content = (int.Parse(ScoresLB.Content.ToString()) - int.Parse(tempScoreDataLB.Content.ToString())).ToString();

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
                if (addtpnManScoreTB.Text != "" || addtpnWomanScoreTB.Text != "" || addxyScoreTB.Text != "" || addcmScoreTB.Text != ""
                    || subtpnManScoreTB.Text != "" || subtpnWomanScoreTB.Text != "" || subxyScoreTB.Text != "" || subcmScoreTB.Text != "")
                {
                    addtpnManScoreTB.Text = "";
                    addtpnWomanScoreTB.Text = "";
                    addxyScoreTB.Text = "";
                    addcmScoreTB.Text = "";
                    tpnManFinalScoreLB.Content = "";
                    tpnWomanFinalScoreLB.Content = "";
                    xyFinalScoreLB.Content = "";
                    cmFinalScoreLB.Content = "";
                    

                    subtpnManScoreTB.Text = "";
                    subtpnWomanScoreTB.Text = "";
                    subxyScoreTB.Text = "";
                    subcmScoreTB.Text = "";
                    tpnManFinalScoreTLB.Content = "";
                    tpnWomanFinalScoreTLB.Content = "";
                    xyFinalScoreTLB.Content = "";
                    cmFinalScoreTLB.Content = "";

                    tempScoreDataLB.Content = "";
                }                 
            }

            if (scoresTC.SelectedIndex == 0)
            {
                tempScoreLB.Content = "本次积分";
                tpnManFinalScoreLB.Content = vip.tpnManScore;
                tpnWomanFinalScoreLB.Content = vip.tpnWomanScore;
                xyFinalScoreLB.Content = vip.xyScore;
                cmFinalScoreLB.Content = vip.cmScore;
                manShoeFinalScoreLB.Content = vip.manShoeScore;
                womanShoeFinalScoreLB.Content = vip.womanShoeScore;
                hatFinalScoreLB.Content = vip.hatScore;
                beltFinalScoreLB.Content = vip.beltScore;
                bagFinalScoreLB.Content = vip.bagScore;


                if (subScoreTB.Text != "")
                {
                    subScoreTB.Text = "";
                    tempScoreDataLB.Content = "";
                }
                    
            }
            if (scoresTC.SelectedIndex == 2)
            {
                tempScoreLB.Content = "本次退分";
                tpnManFinalScoreTLB.Content = vip.tpnManScore;
                tpnWomanFinalScoreTLB.Content = vip.tpnWomanScore;
                xyFinalScoreTLB.Content = vip.xyScore;
                cmFinalScoreTLB.Content = vip.cmScore;

                if (subScoreTB.Text != "")
                {
                    subScoreTB.Text = "";
                    tempScoreDataLB.Content = "";
                }


            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ScoresAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tempScoreDataLB.Content.ToString() != "0")
            {
                if (ChangeScores("ScoresAdd"))
                {
                    var mainWindow = (MainWindow)Owner;
                    mainWindow.reload(vip.Phone);
                    this.Close();
                    //MessageBox.Show("添加成功");
                }
            }
            else
            {
                this.Close();
            }

        }
        private bool ChangeScores(string ScoresAdd)
        {

            try
            {
                if(ScoresAdd== "ScoresAdd")
                {
                    if (int.Parse(tempScoreDataLB.Content.ToString()) > 0)
                    {
                        var dic = new Dictionary<string, object>();
                        dic["ID"] = vip.ID;
                        if (finalScoreLB.Content.ToString() != ScoresLB.Content.ToString())
                        {
                            dic["Scores"] = finalScoreLB.Content;
                        }
                        if (tpnManFinalScoreLB.Content.ToString() != tpnManScoreLB.Content.ToString())
                        {
                            dic["TpnManScore"] = tpnManFinalScoreLB.Content;
                        }
                        if (tpnWomanFinalScoreLB.Content.ToString() != tpnWomanScoreLB.Content.ToString())
                        {
                            dic["TpnWomanScore"] = tpnWomanFinalScoreLB.Content;
                        }

                        if (xyFinalScoreLB.Content.ToString() != xyScoreLB.Content.ToString())
                        {
                            dic["XyScore"] = xyFinalScoreLB.Content;
                        }

                        if (cmFinalScoreLB.Content.ToString() != cmScoreLB.Content.ToString())
                        {
                            dic["CmScore"] = cmFinalScoreLB.Content;
                        }
                        if (manShoeFinalScoreLB.Content.ToString() != manShoeScoreLB.Content.ToString())
                        {
                            dic["ManShoeScore"] = manShoeFinalScoreLB.Content;
                        }
                        if (womanShoeFinalScoreLB.Content.ToString() != womanShoeScoreLB.Content.ToString())
                        {
                            dic["WomanShoeScore"] = womanShoeFinalScoreLB.Content;
                        }
                        if (hatFinalScoreLB.Content.ToString() != hatScoreLB.Content.ToString())
                        {
                            dic["HatScore"] = hatFinalScoreLB.Content;
                        }
                        if (beltFinalScoreLB.Content.ToString() != beltScoreLB.Content.ToString())
                        {
                            dic["BeltScore"] = beltFinalScoreLB.Content;
                        }
                        if (bagFinalScoreLB.Content.ToString() != bagScoreLB.Content.ToString())
                        {
                            dic["BagScore"] = bagFinalScoreLB.Content;
                        }

                        dic["LastModiTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        Core.SqlAction.ChangeScores(dic);
                        return true;
                    }

                return true;
                }
                else if(ScoresAdd == "ScoresSub")
                {
                    if (int.Parse(tempScoreDataLB.Content.ToString())> 0)
                    {
                        var dic = new Dictionary<string, object>();
                        dic["ID"] = vip.ID;
                        if (finalScoreLB.Content.ToString() != ScoresLB.Content.ToString())
                        {
                            dic["Scores"] = finalScoreLB.Content;
                        }
                        if (tpnManFinalScoreTLB.Content.ToString() != tpnManScoreTLB.Content.ToString())
                        {
                            dic["TpnManScore"] = tpnManFinalScoreTLB.Content;
                        }
                        if (tpnWomanFinalScoreTLB.Content.ToString() != tpnWomanScoreTLB.Content.ToString())
                        {
                            dic["TpnWomanScore"] = tpnWomanFinalScoreTLB.Content;
                        }

                        if (xyFinalScoreTLB.Content.ToString() != xyScoreTLB.Content.ToString())
                        {
                            dic["XyScore"] = xyFinalScoreTLB.Content;
                        }

                        if (cmFinalScoreTLB.Content.ToString() != cmScoreTLB.Content.ToString())
                        {
                            dic["CmScore"] = cmFinalScoreTLB.Content;
                        }
                        //dic["LastModiTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        Core.SqlAction.ChangeScores(dic);
                        return true;
                    }

                    return true;
                }
                else if (ScoresAdd == "ScoresExchange")
                {
                    if (int.Parse(subScoreTB.Text) > 0)
                    {
                        var dic = new Dictionary<string, object>();
                        dic["ID"] = vip.ID;
                        if (finalScoreLB.Content.ToString() != ScoresLB.Content.ToString())
                        {
                            dic["Scores"] = finalScoreLB.Content;
                        }
                        //dic["LastModiTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        Core.SqlAction.ChangeScores(dic);
                        return true;
                    }

                    return true;
                }
                else
                {
                    return true;
                }

                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

        }

        private void ScoresSub_Click(object sender, RoutedEventArgs e)
        {
            if (tempScoreDataLB.Content.ToString() != "0")
            {
                if (ChangeScores("ScoresSub"))
                {
                    var mainWindow = (MainWindow)Owner;
                    mainWindow.reload(vip.Phone);
                    this.Close();
                    //MessageBox.Show("添加成功");
                }
            }
            else
            {
                this.Close();
            }
        }

        private void ScoresExchange_Click(object sender, RoutedEventArgs e)
        {
            if (tempScoreDataLB.Content.ToString() != "0")
            {
                if (ChangeScores("ScoresExchange"))
                {
                    var mainWindow = (MainWindow)Owner;
                    mainWindow.reload(vip.Phone);
                    this.Close();
                    //MessageBox.Show("添加成功");
                }
            }
            else
            {
                this.Close();
            }
        }
    }

}
