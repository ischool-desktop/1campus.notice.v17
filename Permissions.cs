﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1campus.notice.v17
{
    class Permissions
    {
        public static string 發送學生智慧簡訊 { get { return "1campus.notice.v17.LogView.MessageLogView.student.PostNotice"; } }
        public static bool 發送學生智慧簡訊權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[發送學生智慧簡訊].Executable;
            }
        }
        public static string 發送老師智慧簡訊 { get { return "1campus.notice.v17.LogView.MessageLogView.teacher.PostNotice"; } }
        public static bool 發送老師智慧簡訊權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[發送老師智慧簡訊].Executable;
            }
        }

        //==========

        public static string 學校發給學生歷程 { get { return "1campus.notice.v17.LogView.MessageLogView.student"; } }
        public static bool 學校發給學生歷程權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[學校發給學生歷程].Executable;
            }
        }

        public static string 學校發給教師歷程 { get { return "1campus.notice.v17.LogView.MessageLogView.teacher"; } }
        public static bool 學校發給教師歷程權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[學校發給教師歷程].Executable;
            }
        }

        public static string 教師發給學生歷程 { get { return "1campus.notice.v17.LogView.MessageLogView.teacher_student"; } }
        public static bool 教師發給學生歷程權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[教師發給學生歷程].Executable;
            }
        }
    }
}
