﻿using System;
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
    /// deleteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class deleteWindow : Window
    {
        VipInfo vip = new VipInfo();
        public deleteWindow()
        {
            InitializeComponent();
            

        }
        private void loadSex()
        {
            SexCB.Items.Add("男");
            SexCB.Items.Add("女");
            
        }

        public deleteWindow(Dictionary<string, string> dic)
        {
            InitializeComponent();
            //vip初始化
            vip.ID = dic["ID"];
            vip.Name = dic["Name"];
            vip.Sex = dic["Sex"];
            vip.Phone = dic["Phone"];
            vip.Birthday = dic["Birthday"];
            vip.Remarks = dic["Remarks"].ToString();

            //加载控件数据
            NameTB.Text = vip.Name;
            loadSex();
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmToDel = MessageBox.Show("确认要删除:\n\n姓名："+vip.Name+"\n性别："+vip.Sex+"\n电话："+vip.Phone, "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmToDel == MessageBoxResult.Yes)
            {
                //此处加删除的操作
                if (Core.SqlAction.DeleteVip(vip.ID))
                {
                    var mainWindow = (MainWindow)Owner;
                    mainWindow.reload("");
                    this.Close();
                    //MessageBox.Show("添加成功");
                }
            }
            else
            {
                //此处加不删除的操作
            }

            

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
