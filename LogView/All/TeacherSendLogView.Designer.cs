namespace _1campus.notice.v17.LogView.All
{
    partial class TeacherSendLogView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.colNoticeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSendTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTeacherCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSendCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessageCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.AllowUserToResizeRows = false;
            this.dataGridViewX1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewX1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNoticeID,
            this.colSendTime,
            this.colTeacherCount,
            this.colTitle,
            this.colSendCount,
            this.colMessageCount});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewX1.MultiSelect = false;
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewX1.Size = new System.Drawing.Size(762, 463);
            this.dataGridViewX1.TabIndex = 14;
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(700, 491);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "離開";
            // 
            // colNoticeID
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.colNoticeID.DefaultCellStyle = dataGridViewCellStyle1;
            this.colNoticeID.HeaderText = "ID";
            this.colNoticeID.Name = "colNoticeID";
            this.colNoticeID.Width = 60;
            // 
            // colSendTime
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightCyan;
            this.colSendTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSendTime.HeaderText = "發送時間";
            this.colSendTime.Name = "colSendTime";
            this.colSendTime.Width = 140;
            // 
            // colTeacherCount
            // 
            this.colTeacherCount.HeaderText = "發送教師";
            this.colTeacherCount.Name = "colTeacherCount";
            // 
            // colTitle
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightCyan;
            this.colTitle.DefaultCellStyle = dataGridViewCellStyle3;
            this.colTitle.HeaderText = "標題";
            this.colTitle.Name = "colTitle";
            this.colTitle.Width = 230;
            // 
            // colSendCount
            // 
            this.colSendCount.HeaderText = "發送數";
            this.colSendCount.Name = "colSendCount";
            this.colSendCount.Width = 85;
            // 
            // colMessageCount
            // 
            this.colMessageCount.HeaderText = "已讀數";
            this.colMessageCount.Name = "colMessageCount";
            this.colMessageCount.Width = 85;
            // 
            // TeacherSendLogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 528);
            this.Controls.Add(this.dataGridViewX1);
            this.Controls.Add(this.btnExit);
            this.DoubleBuffered = true;
            this.Name = "TeacherSendLogView";
            this.Text = "教師發送檢視器";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNoticeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSendTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTeacherCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSendCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessageCount;
    }
}