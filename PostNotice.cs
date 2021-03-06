﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CefSharp;
using CefSharp.WinForms;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Xml;
using FISCA.Presentation.Controls;

namespace _1campus.notice.v17
{
    public partial class PostNotice : FISCA.Presentation.Controls.BaseForm
    {
        private BackgroundWorker _backgroundWorker;

        public enum Type { Student, Teacher }
        private Type _Type { get; set; }
        private string msgTitle; //發送標題
        private string displaySender; //發送單位

        ChromiumWebBrowser _myBrowser;
        private string webContentText; //發送內容

        TaskScheduler ts { get; set; }

        List<string> postList { get; set; }

        List<string> userLimitList { get; set; }
        List<string> userNameList { get; set; }
        bool IsLimit { get; set; }
        int LimitCount = 20;

        string IsCheckX1 = "false";
        string IsCheckX2 = "false";

        public PostNotice(PostNotice.Type type)
        {
            _Type = type;

            InitializeComponent();

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += new DoWorkEventHandler(_backgroundWorker_DoWork);
            _backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_backgroundWorker_RunWorkerCompleted);

        }

        private void PostNotice_Load(object sender, EventArgs e)
        {
            // 2017/11/13 穎驊應恩正要求，加入傳送給家長、學生 選項，如果是老師模式，則將兩按鈕關閉
            if (_Type == Type.Teacher)
            {
                labelX2.Visible = false;
                checkBoxX1.Visible = false;
                checkBoxX2.Visible = false;
            }

            //取得發送單位清單
            postList = DataManager.GetDisPlaySender();
            comboBoxEx1.Items.AddRange(postList.ToArray());

            //取得目前的專案dll檔的 絕對路徑(若在使用者端 就會抓絕對的使用者電腦路徑)
            FileInfo projectDllURL = new FileInfo(Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", ""));
            //取得html 相對路徑
            string htmlRelativeURL = Path.Combine(projectDllURL.DirectoryName, "Redactor\\indexwhi.html");

            ChromiumWebBrowser myBrowser = new ChromiumWebBrowser(htmlRelativeURL);
            _myBrowser = myBrowser;

            this.panel1.Controls.Add(myBrowser);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                if (this.comboBoxEx1.Text == "" || this.tbTitle.Text == "")
                {
                    MsgBox.Show("請輸入標題/單位");
                    return;
                }

                //    <Request>
                //    <Title>鄭明典：今日發展是關鍵</Title>
                //    <DisplaySender>芝儀</DisplaySender>
                //    <Message>鄭明典今（25）日上午於臉書上指出，目前在菲律賓東方海面上有一熱帶性低氣壓，是否會再增強須看今日發展狀況。</Message>
                //    <Category>教務</Category>
                //    <TargetStudent>17736</TargetStudent>
                //    </Request>

                // 2018/6/13 穎驊註解，與俊傑測試 僑泰發送(5000人)， 發現人太多會發送不出去，在此建立背景執行序，以100人一包 分批上傳。
                msgTitle = tbTitle.Text;
                displaySender = comboBoxEx1.Text;
                ts = TaskScheduler.FromCurrentSynchronizationContext();

                IsCheckX1 = checkBoxX1.Checked ? "true" : "false";
                IsCheckX2 = checkBoxX2.Checked ? "true" : "false";


                _backgroundWorker.RunWorkerAsync();

            }
            else
            {
                MsgBox.Show("系統忙碌中");
            }

        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> targetTotalIDList = new List<string>();
            userLimitList = new List<string>();
            userNameList = new List<string>();

            if (_Type == Type.Student)
            {
                targetTotalIDList = K12.Presentation.NLDPanels.Student.SelectedSource;
                DataTable dt = tool._Q.Select(string.Format("select id,name from student where id in ('{0}')", string.Join("','", targetTotalIDList)));

                int x = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string name = "" + row["name"];
                    userNameList.Add(name);

                    if (x < LimitCount)
                    {
                        userLimitList.Add(name);
                        x++;
                    }
                    else
                    {
                        IsLimit = true;
                    }
                }

            }
            else if (_Type == Type.Teacher)
            {
                targetTotalIDList = K12.Presentation.NLDPanels.Teacher.SelectedSource;
                DataTable dt = tool._Q.Select(string.Format("select id,teacher_name from teacher where id in ('{0}')", string.Join("','", targetTotalIDList)));

                int x = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string teacher_name = "" + row["teacher_name"];
                    userNameList.Add(teacher_name);
                    if (x < LimitCount)
                    {
                        userLimitList.Add(teacher_name);
                        x++;
                    }
                    else
                    {
                        IsLimit = true;
                    }
                }
            }

            BatchPushNotice(targetTotalIDList);

        }

        private void BatchPushNotice(List<string> targetIDList)
        {
            //必須要使用greening帳號登入才能用
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            XmlElement root = doc.CreateElement("Request");

            //標題
            var eleTitle = doc.CreateElement("Title");
            eleTitle.InnerText = msgTitle;
            root.AppendChild(eleTitle);
            //發送人員
            var eleDisplaySender = doc.CreateElement("DisplaySender");
            eleDisplaySender.InnerText = displaySender;
            root.AppendChild(eleDisplaySender);

            object[] array1 = new object[] { };

            var task = _myBrowser.EvaluateScriptAsync("getData", array1);
            task.ContinueWith(t =>
            {
                //背景

                if (!t.IsFaulted)
                {
                    webContentText = t.Result.Result.ToString();
                    Console.WriteLine(t.Result.Result.ToString());

                    if (webContentText != "")
                    {

                        var eleMessage = doc.CreateElement("Message");
                        eleMessage.InnerText = webContentText;
                        root.AppendChild(eleMessage);

                        //選取學生
                        if (_Type == Type.Student)
                        {
                            {
                                //StudentVisible
                                var ele = doc.CreateElement("StudentVisible");

                                // 學生_依使用者選項發送
                                ele.InnerText = IsCheckX1;
                                root.AppendChild(ele);
                            }
                            {
                                //ParentVisible
                                var ele = doc.CreateElement("ParentVisible");

                                //ele.InnerText = "true";

                                // 家長_依使用者選項發送
                                ele.InnerText = IsCheckX2;

                                root.AppendChild(ele);
                            }
                            foreach (string each in K12.Presentation.NLDPanels.Student.SelectedSource)
                            {
                                //發送對象
                                var eleTargetStudent = doc.CreateElement("TargetStudent");
                                eleTargetStudent.InnerText = each;
                                root.AppendChild(eleTargetStudent);
                            }
                        }
                        else if (_Type == Type.Teacher)
                        {
                            foreach (string each in K12.Presentation.NLDPanels.Teacher.SelectedSource)
                            {
                                //發送對象
                                var eleTarget = doc.CreateElement("TargetTeacher");
                                eleTarget.InnerText = each;
                                root.AppendChild(eleTarget);
                            }
                        }

                        //送出
                        FISCA.DSAClient.XmlHelper xmlHelper = new FISCA.DSAClient.XmlHelper(root);
                        var conn = new FISCA.DSAClient.Connection();
                        conn.Connect(FISCA.Authentication.DSAServices.AccessPoint, "1campus.notice.admin.v17", FISCA.Authentication.DSAServices.PassportToken);
                        var resp = conn.SendRequest("PostNotice", xmlHelper);


                        //處理Log
                        StringBuilder sb_log = new StringBuilder();
                        List<string> byAction = new List<string>();
                        if (_Type == Type.Student)
                        {
                            if (IsCheckX1 == "true")
                            {
                                byAction.Add("學生");
                            }

                            if (IsCheckX2 == "true")
                            {
                                byAction.Add("家長");
                            }
                        }
                        else if (_Type == Type.Teacher)
                        {
                            byAction.Add("老師");
                        }
                        sb_log.AppendLine(string.Format("發送對象「{0}」", string.Join("，", byAction)));
                        sb_log.AppendLine(string.Format("發送標題「{0}」", msgTitle));
                        sb_log.AppendLine(string.Format("發送單位「{0}」", displaySender));
                        sb_log.AppendLine(string.Format("發送內容「{0}」", webContentText));
                        if (IsLimit)
                        {
                            sb_log.AppendLine(string.Format("對象清單「{0}」", string.Join(",", userLimitList) + "\n等..." + userNameList.Count + "人"));
                        }
                        else
                        {
                            sb_log.AppendLine(string.Format("對象清單「{0}」", string.Join(",", userLimitList)));
                        }

                        FISCA.LogAgent.ApplicationLog.Log("智慧簡訊", "發送", sb_log.ToString());


                    }
                    else
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("請輸入內容");
                    }
                }
                else
                {
                    FISCA.Presentation.Controls.MsgBox.Show("task IsFaulted");
                }
            }).ContinueWith(t =>
            {
                //前景







            }, ts);

        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == false)
            {
                if (e.Error == null)
                {
                    MsgBox.Show("發送成功");
                    this.Close();
                }
                else
                {
                    MsgBox.Show("發生錯誤：\n" + e.Error.Message);
                }
            }
            else
            {
                MsgBox.Show("已取消發送");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // 2017/12/6 穎驊與曜明討論後，將此Shutdown 註解掉，原因是 若在此Shutdown 後，ischool 本體再也沒機會 initiate chrome，
            //若之後再點取功能將會當機， 目前將 initiate/Shutdown 都放置 FISCA 統一在ischool 主程式開啟/關閉 時管理
            //關閉
            //Cef.Shutdown();

            this.Close();
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxX1.Checked)
            {
                if (!checkBoxX2.Checked)
                {
                    checkBoxX1.Checked = true;
                }
            }
        }

        private void checkBoxX2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxX2.Checked)
            {
                if (!checkBoxX1.Checked)
                {
                    checkBoxX2.Checked = true;
                }
            }
        }
    }
}
