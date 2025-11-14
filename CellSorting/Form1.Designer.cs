using System;
using System.Drawing;
using System.Windows.Forms;

namespace CellSorting
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnImport = new System.Windows.Forms.Button();
            this.txtCellNum = new System.Windows.Forms.TextBox();
            this.txtIR = new System.Windows.Forms.TextBox();
            this.txtVdelta = new System.Windows.Forms.TextBox();
            this.txtDDelta = new System.Windows.Forms.TextBox();
            this.txtCapacityDelta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBoxParams = new System.Windows.Forms.GroupBox();
            this.chkCheckIR = new System.Windows.Forms.CheckBox();
            this.chkCheckVdelta = new System.Windows.Forms.CheckBox();
            this.chkCheckDdelta = new System.Windows.Forms.CheckBox();
            this.chkCheckCapacity = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cbboxSwithMode = new System.Windows.Forms.ComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lstLog = new System.Windows.Forms.ListBox();
            this.btnHistory = new System.Windows.Forms.Button();
            this.groupBoxParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(201)))), ((int)(((byte)(176)))));
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("微軟正黑體", 22F, System.Drawing.FontStyle.Bold);
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.Location = new System.Drawing.Point(30, 479);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(250, 60);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "開始匯入分選";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            this.btnImport.MouseEnter += new System.EventHandler(this.btnImport_MouseEnter);
            this.btnImport.MouseLeave += new System.EventHandler(this.btnImport_MouseLeave);
            // 
            // txtCellNum
            // 
            this.txtCellNum.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCellNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCellNum.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.txtCellNum.Location = new System.Drawing.Point(300, 30);
            this.txtCellNum.Name = "txtCellNum";
            this.txtCellNum.Size = new System.Drawing.Size(200, 52);
            this.txtCellNum.TabIndex = 1;
            // 
            // txtIR
            // 
            this.txtIR.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtIR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIR.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.txtIR.Location = new System.Drawing.Point(300, 85);
            this.txtIR.Name = "txtIR";
            this.txtIR.Size = new System.Drawing.Size(200, 52);
            this.txtIR.TabIndex = 3;
            // 
            // txtVdelta
            // 
            this.txtVdelta.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtVdelta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVdelta.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.txtVdelta.Location = new System.Drawing.Point(300, 140);
            this.txtVdelta.Name = "txtVdelta";
            this.txtVdelta.Size = new System.Drawing.Size(200, 52);
            this.txtVdelta.TabIndex = 5;
            // 
            // txtDDelta
            // 
            this.txtDDelta.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtDDelta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDDelta.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.txtDDelta.Location = new System.Drawing.Point(300, 195);
            this.txtDDelta.Name = "txtDDelta";
            this.txtDDelta.Size = new System.Drawing.Size(200, 52);
            this.txtDDelta.TabIndex = 7;
            // 
            // txtCapacityDelta
            // 
            this.txtCapacityDelta.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtCapacityDelta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCapacityDelta.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.txtCapacityDelta.Location = new System.Drawing.Point(300, 250);
            this.txtCapacityDelta.Name = "txtCapacityDelta";
            this.txtCapacityDelta.Size = new System.Drawing.Size(200, 52);
            this.txtCapacityDelta.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 28F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label1.Location = new System.Drawing.Point(30, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "電芯分選系統";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label2.Location = new System.Drawing.Point(20, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 40);
            this.label2.TabIndex = 0;
            this.label2.Text = "每組Cell數量";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.label3.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label3.Location = new System.Drawing.Point(20, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 40);
            this.label3.TabIndex = 2;
            this.label3.Text = "內阻設定 (mΩ)";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.label4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label4.Location = new System.Drawing.Point(20, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(250, 40);
            this.label4.TabIndex = 4;
            this.label4.Text = "壓差設定 (mV)";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.label5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label5.Location = new System.Drawing.Point(20, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(270, 40);
            this.label5.TabIndex = 6;
            this.label5.Text = "生產日期差額設定";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.label6.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label6.Location = new System.Drawing.Point(20, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(250, 40);
            this.label6.TabIndex = 8;
            this.label6.Text = "容量差設定(mAH)";
            // 
            // groupBoxParams
            // 
            this.groupBoxParams.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.groupBoxParams.Controls.Add(this.label2);
            this.groupBoxParams.Controls.Add(this.txtCellNum);
            this.groupBoxParams.Controls.Add(this.label3);
            this.groupBoxParams.Controls.Add(this.txtIR);
            this.groupBoxParams.Controls.Add(this.label4);
            this.groupBoxParams.Controls.Add(this.txtVdelta);
            this.groupBoxParams.Controls.Add(this.label5);
            this.groupBoxParams.Controls.Add(this.txtDDelta);
            this.groupBoxParams.Controls.Add(this.label6);
            this.groupBoxParams.Controls.Add(this.txtCapacityDelta);
            this.groupBoxParams.Controls.Add(this.chkCheckIR);
            this.groupBoxParams.Controls.Add(this.chkCheckVdelta);
            this.groupBoxParams.Controls.Add(this.chkCheckDdelta);
            this.groupBoxParams.Controls.Add(this.chkCheckCapacity);
            this.groupBoxParams.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.groupBoxParams.Location = new System.Drawing.Point(30, 90);
            this.groupBoxParams.Name = "groupBoxParams";
            this.groupBoxParams.Size = new System.Drawing.Size(687, 319);
            this.groupBoxParams.TabIndex = 1;
            this.groupBoxParams.TabStop = false;
            this.groupBoxParams.Text = "分組參數";
            // 
            // chkCheckIR
            // 
            this.chkCheckIR.Checked = true;
            this.chkCheckIR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCheckIR.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.chkCheckIR.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.chkCheckIR.Location = new System.Drawing.Point(506, 96);
            this.chkCheckIR.Name = "chkCheckIR";
            this.chkCheckIR.Size = new System.Drawing.Size(150, 30);
            this.chkCheckIR.TabIndex = 10;
            this.chkCheckIR.Text = "檢查內阻";
            this.chkCheckIR.CheckedChanged += new System.EventHandler(this.chkCheckIR_CheckedChanged);
            // 
            // chkCheckVdelta
            // 
            this.chkCheckVdelta.Checked = true;
            this.chkCheckVdelta.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCheckVdelta.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.chkCheckVdelta.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.chkCheckVdelta.Location = new System.Drawing.Point(506, 151);
            this.chkCheckVdelta.Name = "chkCheckVdelta";
            this.chkCheckVdelta.Size = new System.Drawing.Size(150, 30);
            this.chkCheckVdelta.TabIndex = 11;
            this.chkCheckVdelta.Text = "檢查壓差";
            this.chkCheckVdelta.CheckedChanged += new System.EventHandler(this.chkCheckVdelta_CheckedChanged);
            // 
            // chkCheckDdelta
            // 
            this.chkCheckDdelta.Checked = true;
            this.chkCheckDdelta.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCheckDdelta.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.chkCheckDdelta.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.chkCheckDdelta.Location = new System.Drawing.Point(506, 206);
            this.chkCheckDdelta.Name = "chkCheckDdelta";
            this.chkCheckDdelta.Size = new System.Drawing.Size(180, 30);
            this.chkCheckDdelta.TabIndex = 12;
            this.chkCheckDdelta.Text = "檢查日期差";
            this.chkCheckDdelta.CheckedChanged += new System.EventHandler(this.chkCheckDdelta_CheckedChanged);
            // 
            // chkCheckCapacity
            // 
            this.chkCheckCapacity.Checked = true;
            this.chkCheckCapacity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCheckCapacity.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.chkCheckCapacity.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.chkCheckCapacity.Location = new System.Drawing.Point(506, 261);
            this.chkCheckCapacity.Name = "chkCheckCapacity";
            this.chkCheckCapacity.Size = new System.Drawing.Size(180, 30);
            this.chkCheckCapacity.TabIndex = 13;
            this.chkCheckCapacity.Text = "檢查容量差";
            this.chkCheckCapacity.CheckedChanged += new System.EventHandler(this.chkCheckCapacity_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(30, 404);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(686, 25);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblStatus.Location = new System.Drawing.Point(25, 432);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(630, 25);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "就緒";
            // 
            // cbboxSwithMode
            // 
            this.cbboxSwithMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbboxSwithMode.FormattingEnabled = true;
            this.cbboxSwithMode.Items.AddRange(new object[] {
            "Gus",
            "EVE"});
            this.cbboxSwithMode.Location = new System.Drawing.Point(330, 37);
            this.cbboxSwithMode.Name = "cbboxSwithMode";
            this.cbboxSwithMode.Size = new System.Drawing.Size(200, 33);
            this.cbboxSwithMode.TabIndex = 5;
            this.cbboxSwithMode.SelectedIndexChanged += new System.EventHandler(this.cbboxSwithMode_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // lstLog
            // 
            this.lstLog.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.lstLog.FormattingEnabled = true;
            this.lstLog.ItemHeight = 25;
            this.lstLog.Location = new System.Drawing.Point(286, 435);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(431, 104);
            this.lstLog.TabIndex = 14;
            // 
            // btnHistory
            // 
            this.btnHistory.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnHistory.FlatAppearance.BorderSize = 0;
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Font = new System.Drawing.Font("微軟正黑體", 17F, System.Drawing.FontStyle.Bold);
            this.btnHistory.ForeColor = System.Drawing.Color.White;
            this.btnHistory.Location = new System.Drawing.Point(536, 20);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(179, 54);
            this.btnHistory.TabIndex = 15;
            this.btnHistory.Text = "查詢歷史數據";
            this.btnHistory.UseVisualStyleBackColor = false;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(740, 562);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.cbboxSwithMode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxParams);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnImport);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "電芯分選";
            this.groupBoxParams.ResumeLayout(false);
            this.groupBoxParams.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private GroupBox groupBoxParams;
        private Button btnImport;
        private TextBox txtCellNum;
        private TextBox txtIR;
        private TextBox txtVdelta;
        private TextBox txtDDelta;
        private TextBox txtCapacityDelta;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private CheckBox chkCheckIR;
        private CheckBox chkCheckVdelta;
        private CheckBox chkCheckDdelta;
        private CheckBox chkCheckCapacity;
        private ProgressBar progressBar1;
        private Label lblStatus;
        private ComboBox cbboxSwithMode;
        private ContextMenuStrip contextMenuStrip1;
        private ListBox lstLog;
        private Button btnHistory;
    }
}
