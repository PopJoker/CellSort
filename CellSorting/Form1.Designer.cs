using System.Windows.Forms;
using System.Drawing;

namespace CellSorting
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnImport = new System.Windows.Forms.Button();
            this.txtCellNum = new System.Windows.Forms.TextBox();
            this.txtIR = new System.Windows.Forms.TextBox();
            this.txtVdelta = new System.Windows.Forms.TextBox();
            this.txtDDelta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxParams = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
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
            this.btnImport.Location = new System.Drawing.Point(179, 390);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(250, 60);
            this.btnImport.TabIndex = 2;
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
            this.groupBoxParams.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.groupBoxParams.Location = new System.Drawing.Point(30, 90);
            this.groupBoxParams.Name = "groupBoxParams";
            this.groupBoxParams.Size = new System.Drawing.Size(548, 280);
            this.groupBoxParams.TabIndex = 1;
            this.groupBoxParams.TabStop = false;
            this.groupBoxParams.Text = "分組參數";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label2.Location = new System.Drawing.Point(20, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 40);
            this.label2.TabIndex = 0;
            this.label2.Text = "幾個cell一組";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.label3.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label3.Location = new System.Drawing.Point(20, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 40);
            this.label3.TabIndex = 2;
            this.label3.Text = "內阻設定";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 20F);
            this.label4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label4.Location = new System.Drawing.Point(20, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(250, 40);
            this.label4.TabIndex = 4;
            this.label4.Text = "壓差設定";
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
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(30, 350);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(548, 25);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblStatus.Location = new System.Drawing.Point(30, 320);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(630, 25);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "就緒";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(609, 470);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxParams);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblStatus);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "電芯分選";
            this.groupBoxParams.ResumeLayout(false);
            this.groupBoxParams.PerformLayout();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.GroupBox groupBoxParams;


        #endregion

        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.TextBox txtCellNum;
        private System.Windows.Forms.TextBox txtIR;
        private System.Windows.Forms.TextBox txtVdelta;
        private System.Windows.Forms.TextBox txtDDelta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

