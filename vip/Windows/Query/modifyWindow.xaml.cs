using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
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
    /// modifyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class modifyWindow : Window
    {
        VipInfo vip = new VipInfo();
        public modifyWindow()
        {
            InitializeComponent();
            

        }
        private void loadSex()
        {
            SexCB.Items.Add("男");
            SexCB.Items.Add("女");
            
        }

        public modifyWindow(Dictionary<string, string> dic)
        {
            InitializeComponent();
            //加载会员性别
            loadSex();
            vip.Name = dic["Name"];
            
            vip.Sex = dic["Sex"];
            vip.Phone = dic["Phone"];
            vip.Birthday = dic["Birthday"];
            vip.Remarks = dic["Remarks"].ToString() ;

            //加载控件数据
            NameTB.Text = vip.Name;
            SexCB.SelectedIndex=SexCB.Items.IndexOf(vip.Sex);
            PhoneTB.Text = vip.Phone;
            BirthdayDP.SelectedDate =Convert.ToDateTime(vip.Birthday);
            RemarksTB.Text = vip.Remarks;

            
            ScoresTB.Text = dic["Scores"];
            tpnManScoresTB.Text= dic["TpnManScore"];
            tpnWomanScoresTB.Text = dic["TpnWomanScore"];
            xyScoresTB.Text = dic["XyScore"];
            cmScoresTB.Text = dic["CmScore"];
            
        }
        private bool vipModify()
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

                        var sql = "select * from vip where (Phone='" + PhoneTB.Text.Replace(" ", "") + "')";
                        DataTable dt = sh.Select(sql);
                        if (dt.Rows.Count == 0)
                        {
                            var dic = new Dictionary<string, object>();
                            dic["Name"] = NameTB.Text;
                            dic["Sex"] = SexCB.SelectedValue;
                            dic["Phone"] = PhoneTB.Text;
                            dic["Birthday"] = BirthdayDP.SelectedDate.Value.ToString("yyyy年MM月dd日");
                            dic["Remarks"] = RemarksTB.Text;
                            /*
                                dic["Scores"] = ScoresTB.Text;
                                dic["TpnManScore"]=tpnManScoresTB.Text ;
                                dic["TpnWomanScore"]=tpnWomanScoresTB.Text  ;
                                dic["XyScore"]=xyScoresTB.Text ;
                                dic["CmScore"]=cmScoresTB.Text ;
                                */

                            //if (vipScore != ScoresTB.Text) {
                            //       dic["LastModiTime"] = DateTime.Now.ToLongDateString().ToString();
                            //   }

                            sh.Update("vip", dic, "Phone", vip.Phone);
                            return true;
                        }else
                        {
                            MessageBox.Show("会员手机号码重复");
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
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string sexTemp;
            if (SexCB.SelectedValue == null)
            {
                sexTemp = "";
            }
            else
            {
                sexTemp = SexCB.SelectedValue.ToString();
            }
           
            var c = PhoneTB.Text;
            var d = BirthdayDP.SelectedDate.Value.ToString("yyyy年MM月dd日");
            var ea = RemarksTB.Text;
            if (vip.Name != NameTB.Text
                || vip.Sex != sexTemp
                || vip.Phone!= PhoneTB.Text
                ||vip.Birthday!= BirthdayDP.SelectedDate.Value.ToString("yyyy年MM月dd日")
                ||vip.Remarks!= RemarksTB.Text)
            {
                if (vipModify())
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
        private void checkInput(object sender, TextCompositionEventArgs e)
        {
            Windows.Tools.checkInput(e);
        }
        private void ScoresTB_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Tools.isInputNumber(e))
            {
                //MessageBox.Show("请输入数字！");
            }
        }
        private void PhoneTB_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!Tools.isInputNumber(e))
            {
                //MessageBox.Show("请输入数字！");
            }
        }

        private void tpnWomanScoresTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tpnManScoresTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void xyScoresTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cmScoresTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tpnManScoresTB_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
