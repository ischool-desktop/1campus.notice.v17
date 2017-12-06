using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using CefSharp;
using CefSharp.WinForms;

namespace _1campus.notice.v17
{
    public static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [FISCA.MainMethod]
        public static void Main()
        {


            //重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要
            //重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要
            //重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要重要

            //2017/8/16 穎驊註解，以下方法尋找cefSharp 路徑都失敗， 後來曜明 包裝了一個特別的版本 FISCA with Chrome embed 就可以了 ，
            //因此注意以後執行本程式碼 必須為FISCA with Chrome embed 的 ischool 主程式 ， 
            // 下載 位置:https://storage.googleapis.com/ischool-dra/1admin/FISCAWithChrome.7z

            #region 失敗的方法

            ////取得目前的專案dll檔的 絕對路徑(若在使用者端 就會抓絕對的使用者電腦路徑)
            //FileInfo projectDllURL = new FileInfo(Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", ""));
            ////取得html 相對路徑
            //string cefLibRelativeURL = Path.Combine(projectDllURL.DirectoryName, "CefLibrary"); 
            //string envirRefPath = Environment.GetEnvironmentVariable("PATH")  +";" +cefLibRelativeURL ;
            //Environment.SetEnvironmentVariable("PATH", envirRefPath, EnvironmentVariableTarget.Process);

            //var pluginsPath = Path.Combine(Environment.CurrentDirectory, "CefLibrary");
            //var path = Environment.GetEnvironmentVariable("PATH") + ";" + pluginsPath;
            //Environment.SetEnvironmentVariable("PATH", path, EnvironmentVariableTarget.Process); 
            #endregion
            {
                K12.Presentation.NLDPanels.Student.RibbonBarItems["公告"]["發送公告"].Image = Properties.Resources.speech_balloon_64;
                K12.Presentation.NLDPanels.Student.RibbonBarItems["公告"]["發送公告"].Size = FISCA.Presentation.RibbonBarButton.MenuButtonSize.Large;
                K12.Presentation.NLDPanels.Student.RibbonBarItems["公告"]["發送公告"].Enable = false;
                K12.Presentation.NLDPanels.Student.SelectedSourceChanged += delegate
                {
                    K12.Presentation.NLDPanels.Student.RibbonBarItems["公告"]["發送公告"].Enable = K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0;
                };
                K12.Presentation.NLDPanels.Student.RibbonBarItems["公告"]["發送公告"].Click += delegate
                {
                    //必須要使用greening帳號登入才能用
                    if (FISCA.Authentication.DSAServices.AccountType == FISCA.Authentication.AccountType.Greening)
                    {
                        new PostNotice(PostNotice.Type.Student).ShowDialog();
                    }
                    else
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("必須是Greening帳號方可使用本功能");
                    }
                };
            }
            {
                K12.Presentation.NLDPanels.Teacher.RibbonBarItems["公告"]["發送公告"].Image = Properties.Resources.speech_balloon_64;
                K12.Presentation.NLDPanels.Teacher.RibbonBarItems["公告"]["發送公告"].Size = FISCA.Presentation.RibbonBarButton.MenuButtonSize.Large;
                K12.Presentation.NLDPanels.Teacher.RibbonBarItems["公告"]["發送公告"].Enable = false;
                K12.Presentation.NLDPanels.Teacher.SelectedSourceChanged += delegate
                {
                    K12.Presentation.NLDPanels.Teacher.RibbonBarItems["公告"]["發送公告"].Enable = K12.Presentation.NLDPanels.Teacher.SelectedSource.Count > 0;
                };
                K12.Presentation.NLDPanels.Teacher.RibbonBarItems["公告"]["發送公告"].Click += delegate
                {
                    //必須要使用greening帳號登入才能用
                    if (FISCA.Authentication.DSAServices.AccountType == FISCA.Authentication.AccountType.Greening)
                    {
                        new PostNotice(PostNotice.Type.Teacher).ShowDialog();
                    }
                    else
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("必須是Greening帳號方可使用本功能");
                    }
                };
            }  
        }
        
    }
}
