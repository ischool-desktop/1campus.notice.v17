using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
