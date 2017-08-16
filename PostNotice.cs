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

namespace _1campus.notice.v17
{
    public partial class PostNotice : FISCA.Presentation.Controls.BaseForm
    {
        public enum Type { Student, Teacher }
        private Type _Type { get; set; }

        public PostNotice(PostNotice.Type type)
        {
            _Type = type;
            InitializeComponent();
        }

        //2017/8/14 穎驊新增 CefSharp 架構
        ChromiumWebBrowser _myBrowser;

        private void PostNotice_Load(object sender, EventArgs e)
        {
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
            FileInfo projectDllURL = new FileInfo(Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", ""));
            //取得html 相對路徑
            string htmlRelativeURL = Path.Combine(projectDllURL.DirectoryName, "HTMLResouces\\html\\content.html"); ;

            //2017/8/15 穎驊註解， 下面為穎驊的開發機 Html 的絕對位子，在開發測試完畢後，將使用相對路徑 找到html 檔案
            //ChromiumWebBrowser myBrowser = new ChromiumWebBrowser(@"C:\Users\ED\Documents\ischool_github\1campus.notice.v17\HTMLResouces\html\content.html");
            ChromiumWebBrowser myBrowser = new ChromiumWebBrowser(htmlRelativeURL);

            //ChromiumWebBrowser myBrowser = new ChromiumWebBrowser(@"http://www.maps.google.com");
            _myBrowser = myBrowser;
            
            this.panel1.Controls.Add(myBrowser);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //關閉
            Cef.Shutdown();

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

            //必須要使用greening帳號登入才能用
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            var root = doc.CreateElement("Request");

            //發送者名稱
            var eleTitle = doc.CreateElement("Title");
            eleTitle.InnerText = tbTitle.Text;
            root.AppendChild(eleTitle);
            //發送人員
            var eleDisplaySender = doc.CreateElement("DisplaySender");
            eleDisplaySender.InnerText = tbDisplaySender.Text;
            root.AppendChild(eleDisplaySender);

            
            string webContentText = "";

            // 一定要傳參數，就算空的也沒關係
            object[] array1 = new object[] { };

            //2017/8/14 穎驊新增， 取得視窗下面Web 的輸入html內容  
            var task = _myBrowser.EvaluateScriptAsync("getData", array1);

            task.ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    var response = t.Result;

                    if (response.Success == true)
                    {
                        //MessageBox.Show(response.Result.ToString());

                        webContentText = response.Result.ToString();


                        //2017/8/14 穎驊新增， 檢查'使用者沒有輸入內容，跳出提醒，以避免server 爆炸， 
                        //不把button disable 動態偵測輸入內容有無原因為，CefSharp API 的回傳沒有那模即時，異步處理會有問題。
                        if (webContentText != "")
                        {
                            //訊息內容
                            //var eleMessage = doc.CreateElement("Message");
                            //eleMessage.InnerText = tbMessageContent.Text;
                            //root.AppendChild(eleMessage);

                            //訊息內容
                            var eleMessage = doc.CreateElement("Message");
                            eleMessage.InnerText = webContentText;
                            root.AppendChild(eleMessage);

                            //選取學生
                            if (_Type == Type.Student)
                            {
                                {
                                    //StudentVisible
                                    var ele = doc.CreateElement("StudentVisible");
                                    ele.InnerText = "true";
                                    root.AppendChild(ele);
                                }
                                {
                                    //ParentVisible
                                    var ele = doc.CreateElement("ParentVisible");
                                    ele.InnerText = "true";
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
                            //選取教師
                            if (_Type == Type.Teacher)
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
                            conn.SendRequest("PostNotice", xmlHelper);

                            FISCA.Presentation.Controls.MsgBox.Show("發送成功");
                            this.Close();
                        }
                        else
                        {
                            FISCA.Presentation.Controls.MsgBox.Show("請輸入訊息內容!!");
                        }
                    }
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());           
        }

        // 若想要呼叫 Chrome 的開法者工具視窗，可以呼叫此方法。
        private void ShowDevTools()
        {
            _myBrowser.ShowDevTools();
        }

        private void PostNotice_Shown(object sender, EventArgs e)
        {

        }
    }
}
