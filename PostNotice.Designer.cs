namespace _1campus.notice.v17
{
    partial class PostNotice
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
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.btnSend = new DevComponents.DotNetBar.ButtonX();
            this.tbTitle = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.tbMessageContent = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbDisplaySender = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(463, 639);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "離開";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSend
            // 
            this.btnSend.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.AutoSize = true;
            this.btnSend.BackColor = System.Drawing.Color.Transparent;
            this.btnSend.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(382, 639);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 25);
            this.btnSend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSend.TabIndex = 11;
            this.btnSend.Text = "發送";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.tbTitle.Border.Class = "TextBoxBorder";
            this.tbTitle.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbTitle.Location = new System.Drawing.Point(92, 41);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(447, 25);
            this.tbTitle.TabIndex = 14;
            this.tbTitle.TextChanged += new System.EventHandler(this.verifyInput);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(11, 42);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 13;
            this.labelX1.Text = "發送標題：";
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(12, 72);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(74, 21);
            this.labelX4.TabIndex = 15;
            this.labelX4.Text = "發送內容：";
            // 
            // tbMessageContent
            // 
            this.tbMessageContent.Anchor = System.Windows.Forms.AnchorStyles.None;
            // 
            // 
            // 
            this.tbMessageContent.Border.Class = "TextBoxBorder";
            this.tbMessageContent.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbMessageContent.Enabled = false;
            this.tbMessageContent.Location = new System.Drawing.Point(12, 99);
            this.tbMessageContent.Multiline = true;
            this.tbMessageContent.Name = "tbMessageContent";
            this.tbMessageContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessageContent.Size = new System.Drawing.Size(55, 39);
            this.tbMessageContent.TabIndex = 16;
            this.tbMessageContent.Visible = false;
            this.tbMessageContent.TextChanged += new System.EventHandler(this.verifyInput);
            // 
            // tbDisplaySender
            // 
            this.tbDisplaySender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.tbDisplaySender.Border.Class = "TextBoxBorder";
            this.tbDisplaySender.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbDisplaySender.Location = new System.Drawing.Point(93, 10);
            this.tbDisplaySender.Name = "tbDisplaySender";
            this.tbDisplaySender.Size = new System.Drawing.Size(446, 25);
            this.tbDisplaySender.TabIndex = 18;
            this.tbDisplaySender.TextChanged += new System.EventHandler(this.verifyInput);
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(12, 12);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(74, 21);
            this.labelX6.TabIndex = 17;
            this.labelX6.Text = "發送單位：";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(93, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 561);
            this.panel1.TabIndex = 19;
            // 
            // PostNotice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 676);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tbDisplaySender);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.tbMessageContent);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSend);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(568, 715);
            this.MinimumSize = new System.Drawing.Size(568, 715);
            this.Name = "PostNotice";
            this.Text = "發送公告";
            this.Load += new System.EventHandler(this.PostNotice_Load);
            this.Shown += new System.EventHandler(this.PostNotice_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.ButtonX btnSend;
        private DevComponents.DotNetBar.Controls.TextBoxX tbTitle;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX tbMessageContent;
        private DevComponents.DotNetBar.Controls.TextBoxX tbDisplaySender;
        private DevComponents.DotNetBar.LabelX labelX6;
        private System.Windows.Forms.Panel panel1;
    }
}