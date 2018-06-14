using System;
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

namespace _1campus.notice.v17
{
    public partial class PostNotice : FISCA.Presentation.Controls.BaseForm
    {
        private BackgroundWorker _backgroundWorker;

        public enum Type { Student, Teacher }
        private Type _Type { get; set; }
        private string msgTitle; //發送標題
        private string displaySender; //發送單位
        private string msgText; //發送內容

        public PostNotice(PostNotice.Type type)
        {
            _Type = type;

            InitializeComponent();

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += new DoWorkEventHandler(_backgroundWorker_DoWork);
            //_backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(_backgroundWorker_ProgressChanged);
            _backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_backgroundWorker_RunWorkerCompleted);
            //_backgroundWorker.WorkerReportsProgress = true;

        }

        //2017/8/14 穎驊新增 CefSharp 架構
        ChromiumWebBrowser _myBrowser;

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> targetTotalIDList = new List<string>();

            if (_Type == Type.Student)
            {
                targetTotalIDList = K12.Presentation.NLDPanels.Student.SelectedSource;
            }

            if (_Type == Type.Teacher)
            {
                targetTotalIDList = K12.Presentation.NLDPanels.Teacher.SelectedSource;
            }

            int pages = 0; // 看可以分成幾批
            if (targetTotalIDList.Count <= 100) // 100 人 切一份
            {
                pages = 1;
            }                
            try
            {
                pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(targetTotalIDList.Count) / Convert.ToDecimal(100)));
            }
            catch (System.Exception ex)
            {
                pages = 0;
            }
           
            for (int i = 0; i < pages; i++)
            {
                Console.WriteLine("執行到" + i + 1 + "分批");
                List<string> tempList = (List<string>) targetTotalIDList.Skip(i * 100).Take(100).ToList();
                if (tempList != null)
                {
                    BatchPushNotice(tempList);
                }                    
            }
            
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FISCA.Presentation.Controls.MsgBox.Show("發送成功");
            this.Close();
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

            string webContentText = "";

            //訊息內容
            //var eleMessage = doc.CreateElement("Message");
            //eleMessage.InnerText = tbMessageContent.Text;
            //root.AppendChild(eleMessage);

            //訊息內容

            var eleMessage = doc.CreateElement("Message");
            eleMessage.InnerText = msgText;
            root.AppendChild(eleMessage);

            //選取學生
            if (_Type == Type.Student)
            {
                {
                    //StudentVisible
                    var ele = doc.CreateElement("StudentVisible");

                    //ele.InnerText = "true";

                    // 學生_依使用者選項發送
                    ele.InnerText = checkBoxX1.Checked ? "true" : "false";

                    root.AppendChild(ele);
                }
                {
                    //ParentVisible
                    var ele = doc.CreateElement("ParentVisible");

                    //ele.InnerText = "true";

                    // 家長_依使用者選項發送
                    ele.InnerText = checkBoxX2.Checked ? "true" : "false";

                    root.AppendChild(ele);
                }
                foreach (string each in targetIDList)
                {
                    //發送對象
                    var eleTargetStudent = doc.CreateElement("TargetStudent");
                    eleTargetStudent.InnerText = each;
                    root.AppendChild(eleTargetStudent);
                }
            }
            //選取教師
            if (_Type == Type.Teacher)
            {
                foreach (string each in targetIDList)
                {
                    //發送對象
                    var eleTarget = doc.CreateElement("TargetTeacher");
                    eleTarget.InnerText = each;
                    root.AppendChild(eleTarget);
                }
            }

            //送出
            try
            {
                FISCA.DSAClient.XmlHelper xmlHelper = new FISCA.DSAClient.XmlHelper(root);
                var conn = new FISCA.DSAClient.Connection();
                conn.Connect(FISCA.Authentication.DSAServices.AccessPoint, "1campus.notice.admin.v17", FISCA.Authentication.DSAServices.PassportToken);
                var resp = conn.SendRequest("PostNotice", xmlHelper);
            }
            catch
            {
                Console.WriteLine("對象系統編號:" + string.Join(",", targetIDList) + "。發送失敗");
                //FISCA.Presentation.Controls.MsgBox.Show("對象系統編號:" + string.Join(",", targetIDList) +"。發送失敗");
            }

        }



        private void PostNotice_Load(object sender, EventArgs e)
        {     
            // 2017/11/13 穎驊應恩正要求，加入傳送給家長、學生 選項，如果是老師模式，則將兩按鈕關閉
            if (_Type == Type.Teacher)
            {

                labelX2.Visible = false;
                checkBoxX1.Visible = false;
                checkBoxX2.Visible = false;

                labelX2.Enabled = false;
                checkBoxX1.Enabled = false;
                checkBoxX2.Enabled = false;
                Point p1 = new Point();

                p1.X = 12;
                p1.Y = 78;

                labelX4.Location = p1;

                Point p2 = new Point();

                p2.X = 93;
                p2.Y = 78;

                panel1.Location = p2;

                panel1.Height += 34;
            }

            //// 避免連續兩次Cef.Initialize() 會當機
            //if (!Cef.IsInitialized)
            //{

            //    var pluginsPath = Path.Combine(Environment.CurrentDirectory, "CefLibrary");

            //    var exePath = Path.Combine(Environment.CurrentDirectory, "CefLibrary//CefSharp.BrowserSubprocess.exe");


            //    //Cef.Initialize();
            //    CefSettings cf = new CefSettings();


            //    cf.ResourcesDirPath = pluginsPath;
            //    cf.BrowserSubprocessPath = exePath;

            //    Cef.Initialize(cf);
            //}

            //取得目前的專案dll檔的 絕對路徑(若在使用者端 就會抓絕對的使用者電腦路徑)
            //FileInfo projectDllURL = new FileInfo(Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", ""));
            ////取得html 相對路徑
            //string htmlRelativeURL = Path.Combine(projectDllURL.DirectoryName, "HTMLResouces\\html\\content.html"); ;

            //2017/8/15 穎驊註解， 下面為穎驊的開發機 Html 的絕對位子，在開發測試完畢後，將使用相對路徑 找到html 檔案
            //ChromiumWebBrowser myBrowser = new ChromiumWebBrowser(@"C:\Users\ED\Documents\ischool_github\1campus.notice.v17\HTMLResouces\html\content.html");
            //ChromiumWebBrowser myBrowser = new ChromiumWebBrowser(htmlRelativeURL);

            //ChromiumWebBrowser myBrowser = new ChromiumWebBrowser(@"http://www.maps.google.com");
            //_myBrowser = myBrowser;
            
            //this.panel1.Controls.Add(myBrowser);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // 2017/12/6 穎驊與曜明討論後，將此Shutdown 註解掉，原因是 若在此Shutdown 後，ischool 本體再也沒機會 initiate chrome，
            //若之後再點取功能將會當機， 目前將 initiate/Shutdown 都放置 FISCA 統一在ischool 主程式開啟/關閉 時管理
            //關閉
            //Cef.Shutdown();

            this.Close();
        }

        private void verifyInput(object sender, EventArgs e)
        {
            this.btnSend.Enabled = (this.tbDisplaySender.Text != "" && this.tbTitle.Text != ""  );
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

            //    <Request>
            //    <Title>鄭明典：今日發展是關鍵</Title>
            //    <DisplaySender>芝儀</DisplaySender>
            //    <Message>鄭明典今（25）日上午於臉書上指出，目前在菲律賓東方海面上有一熱帶性低氣壓，是否會再增強須看今日發展狀況。</Message>
            //    <Category>教務</Category>
            //    <TargetStudent>17736</TargetStudent>
            //    </Request>

            // 2018/6/13 穎驊註解，與俊傑測試 僑泰發送(5000人)， 發現人太多會發送不出去，在此建立背景執行序，以100人一包 分批上傳。
            msgTitle = tbTitle.Text;
            displaySender = tbDisplaySender.Text;
            msgText = textBoxX1.Text;

            _backgroundWorker.RunWorkerAsync();

            // 2018/6/12 穎驊註解，恩正說 將原本使用的 cef 弄掉，傳送內容不再用原本的html，直接用textBox 傳內碼
            //// 一定要傳參數，就算空的也沒關係
            //object[] array1 = new object[] { };

            ////2017/8/14 穎驊新增， 取得視窗下面Web 的輸入html內容  
            //var task = _myBrowser.EvaluateScriptAsync("getData", array1);

            //task.ContinueWith(t =>
            //{
            //    if (!t.IsFaulted)
            //    {
            //        var response = t.Result;

            //        if (response.Success == true)
            //        {
            //            //MessageBox.Show(response.Result.ToString());

            //            webContentText = response.Result.ToString();


            //            //2017/8/14 穎驊新增， 檢查'使用者沒有輸入內容，跳出提醒，以避免server 爆炸， 
            //            //不把button disable 動態偵測輸入內容有無原因為，CefSharp API 的回傳沒有那模即時，異步處理會有問題。
            //            if (webContentText != "")
            //            {
            //                //訊息內容
            //                //var eleMessage = doc.CreateElement("Message");
            //                //eleMessage.InnerText = tbMessageContent.Text;
            //                //root.AppendChild(eleMessage);

            //                //訊息內容
            //                var eleMessage = doc.CreateElement("Message");
            //                eleMessage.InnerText = webContentText;
            //                root.AppendChild(eleMessage);

            //                //選取學生
            //                if (_Type == Type.Student)
            //                {
            //                    {
            //                        //StudentVisible
            //                        var ele = doc.CreateElement("StudentVisible");

            //                        //ele.InnerText = "true";

            //                        // 學生_依使用者選項發送
            //                        ele.InnerText = checkBoxX1.Checked ? "true" : "false";

            //                        root.AppendChild(ele);
            //                    }
            //                    {
            //                        //ParentVisible
            //                        var ele = doc.CreateElement("ParentVisible");

            //                        //ele.InnerText = "true";

            //                        // 家長_依使用者選項發送
            //                        ele.InnerText = checkBoxX2.Checked ? "true" : "false";

            //                        root.AppendChild(ele);
            //                    }
            //                    foreach (string each in K12.Presentation.NLDPanels.Student.SelectedSource)
            //                    {
            //                        //發送對象
            //                        var eleTargetStudent = doc.CreateElement("TargetStudent");
            //                        eleTargetStudent.InnerText = each;
            //                        root.AppendChild(eleTargetStudent);
            //                    }
            //                }
            //                //選取教師
            //                if (_Type == Type.Teacher)
            //                {
            //                    foreach (string each in K12.Presentation.NLDPanels.Teacher.SelectedSource)
            //                    {
            //                        //發送對象
            //                        var eleTarget = doc.CreateElement("TargetTeacher");
            //                        eleTarget.InnerText = each;
            //                        root.AppendChild(eleTarget);
            //                    }
            //                }

            //                //送出
            //                FISCA.DSAClient.XmlHelper xmlHelper = new FISCA.DSAClient.XmlHelper(root);
            //                var conn = new FISCA.DSAClient.Connection();
            //                conn.Connect(FISCA.Authentication.DSAServices.AccessPoint, "1campus.notice.admin.v17", FISCA.Authentication.DSAServices.PassportToken);
            //                var resp = conn.SendRequest("PostNotice", xmlHelper);

            //                FISCA.Presentation.Controls.MsgBox.Show("發送成功");
            //                this.Close();
            //            }
            //            else
            //            {
            //                FISCA.Presentation.Controls.MsgBox.Show("請輸入訊息內容!!");
            //            }
            //        }
            //        else
            //        {
            //            FISCA.Presentation.Controls.MsgBox.Show("task is not successful");
            //        }
            //    }
            //    else
            //    {
            //        FISCA.Presentation.Controls.MsgBox.Show("task IsFaulted");
            //    }
            //}, TaskScheduler.FromCurrentSynchronizationContext());           
        }

        // 若想要呼叫 Chrome 的開法者工具視窗，可以呼叫此方法。
        private void ShowDevTools()
        {
            _myBrowser.ShowDevTools();
        }

        private void PostNotice_Shown(object sender, EventArgs e)
        {

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
