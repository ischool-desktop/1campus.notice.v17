using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void verifyInput(object sender, EventArgs e)
        {
            this.btnSend.Enabled = (this.tbDisplaySender.Text != "" && this.tbTitle.Text != "" && this.tbMessageContent.Text != "");
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
            //訊息內容
            var eleMessage = doc.CreateElement("Message");
            eleMessage.InnerText = tbMessageContent.Text;
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
            if(_Type == Type.Teacher)
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
    }
}
