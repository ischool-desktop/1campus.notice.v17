using FISCA;
using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace _1campus.notice.v17
{
    public partial class MessageTeacherUsage : BaseForm
    {
        BackgroundWorker BGW = new BackgroundWorker();


        public MessageTeacherUsage()
        {
            InitializeComponent();
        }

        private void MessageUsage_Load(object sender, EventArgs e)
        {
            BGW.RunWorkerCompleted += BGW_RunWorkerCompleted;
            BGW.DoWork += BGW_DoWork;

            BGW.RunWorkerAsync();
        }

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            List<MessageTeacherCountRow> list = new List<MessageTeacherCountRow>();

            StringBuilder sql = new StringBuilder();
            sql.Append("select notice.uid as 訊息編號,teacher.teacher_name,");
            sql.Append("count(notice_target.uid) as count,notice.post_time,notice.display_sender,");
            sql.Append("notice.title,notice.student_visible,notice.parent_visible,notice.message ");
            sql.Append("from $ischool.1campus.notice notice ");
            sql.Append("join $ischool.1campus.notice_target notice_target on notice_target.ref_notice_id=notice.uid ");
            sql.Append("join teacher on teacher.id=notice.ref_teacher_id ");
            sql.Append("where notice.global_notice='false' and notice.ref_teacher_id is not null ");
            sql.Append("group by notice.uid,notice.title,teacher.teacher_name,notice.post_time ");
            sql.Append("order by notice.post_time DESC ");

            DataTable dt = tool._Q.Select(sql.ToString());


            foreach (DataRow row in dt.Rows)
            {
                MessageTeacherCountRow mcR = new MessageTeacherCountRow();
                mcR.display_sender = "" + row["display_sender"];
                mcR.post_time = "" + row["post_time"];
                mcR.title = "" + row["title"];

                mcR.parent_visible = "" + row["parent_visible"];
                mcR.student_visible = "" + row["student_visible"];
                mcR.message = "" + row["message"];
                mcR.TeacherName = "" + row["teacher_name"];
                mcR.Count = "" + row["count"];

                list.Add(mcR);
            }

            e.Result = list;
        }

        void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                if (e.Error == null)
                {
                    List<MessageTeacherCountRow> list = (List<MessageTeacherCountRow>)e.Result;

                    foreach (MessageTeacherCountRow teacher in list)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridViewX1);


                        row.Cells[Column1.Index].Value = teacher.post_time;
                        row.Cells[Column2.Index].Value = teacher.title;
                        row.Cells[Column3.Index].Value = teacher.TeacherName;
                        row.Cells[Column5.Index].Value = teacher.message;

                        int count = 0;
                        if (int.TryParse(teacher.Count, out count))
                        {
                            row.Cells[Column4.Index].Value = count; //發送數
                        }

                        row.Tag = teacher;

                        dataGridViewX1.Rows.Add(row);
                    }
                }
                else
                {
                    MsgBox.Show("取得資料發生錯誤!!\n" + e.Error.Message);
                }
            }
            else
            {
                MsgBox.Show("背景模式已取消!!");
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewX1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (dataGridViewX1.SelectedRows.Count > 0)
                {
                    MessageTeacherCountRow record = (MessageTeacherCountRow)dataGridViewX1.SelectedRows[0].Tag;
                    TeacherLogViewForm view = new TeacherLogViewForm(record, "student");
                    view.ShowDialog();
                }

            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count > 0)
            {
                MessageTeacherCountRow record = (MessageTeacherCountRow)dataGridViewX1.SelectedRows[0].Tag;
                TeacherLogViewForm view = new TeacherLogViewForm(record, "student");
                view.ShowDialog();
            }
        }
    }

    public class MessageTeacherCountRow
    {
        public string post_time { get; set; }
        public string title { get; set; }
        public string display_sender { get; set; }

        public string parent_visible { get; set; }
        public string student_visible { get; set; }

        public string message { get; set; }

        //====
        public string TeacherName { get; set; }

        //====
        public string Count { get; set; }
    }
}
