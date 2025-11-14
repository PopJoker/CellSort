using System;
using System.Drawing;
using System.Windows.Forms;

namespace CellSorting
{
    partial class Form2dbshow
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private GroupBox groupBoxSearch;
        private Label lblTable;
        private ComboBox comboTables;
        private Label lblQuery;
        private TextBox txtQuery;
        private Button btnQuery;
        private Label lblStart;
        private Label lblEnd;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private ComboBox cbStartHour;
        private ComboBox cbStartMinute;
        private ComboBox cbStartSecond;
        private ComboBox cbEndHour;
        private ComboBox cbEndMinute;
        private ComboBox cbEndSecond;
        private DataGridView dgvResults;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.comboTables = new System.Windows.Forms.ComboBox();
            this.lblQuery = new System.Windows.Forms.Label();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.lblStart = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.cbStartHour = new System.Windows.Forms.ComboBox();
            this.cbStartMinute = new System.Windows.Forms.ComboBox();
            this.cbStartSecond = new System.Windows.Forms.ComboBox();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.cbEndHour = new System.Windows.Forms.ComboBox();
            this.cbEndMinute = new System.Windows.Forms.ComboBox();
            this.cbEndSecond = new System.Windows.Forms.ComboBox();
            this.btnQuery = new System.Windows.Forms.Button();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.groupBoxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("微軟正黑體", 28F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblTitle.Location = new System.Drawing.Point(20, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(600, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "分選資料庫檢視器";
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.groupBoxSearch.Controls.Add(this.lblTable);
            this.groupBoxSearch.Controls.Add(this.comboTables);
            this.groupBoxSearch.Controls.Add(this.lblQuery);
            this.groupBoxSearch.Controls.Add(this.txtQuery);
            this.groupBoxSearch.Controls.Add(this.lblStart);
            this.groupBoxSearch.Controls.Add(this.dtpStart);
            this.groupBoxSearch.Controls.Add(this.cbStartHour);
            this.groupBoxSearch.Controls.Add(this.cbStartMinute);
            this.groupBoxSearch.Controls.Add(this.cbStartSecond);
            this.groupBoxSearch.Controls.Add(this.lblEnd);
            this.groupBoxSearch.Controls.Add(this.dtpEnd);
            this.groupBoxSearch.Controls.Add(this.cbEndHour);
            this.groupBoxSearch.Controls.Add(this.cbEndMinute);
            this.groupBoxSearch.Controls.Add(this.cbEndSecond);
            this.groupBoxSearch.Controls.Add(this.btnQuery);
            this.groupBoxSearch.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.groupBoxSearch.Location = new System.Drawing.Point(20, 70);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.Size = new System.Drawing.Size(1108, 130);
            this.groupBoxSearch.TabIndex = 1;
            this.groupBoxSearch.TabStop = false;
            this.groupBoxSearch.Text = "查詢條件";
            // 
            // lblTable
            // 
            this.lblTable.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.lblTable.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTable.Location = new System.Drawing.Point(10, 35);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(100, 30);
            this.lblTable.TabIndex = 0;
            this.lblTable.Text = "資料表:";
            // 
            // comboTables
            // 
            this.comboTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTables.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.comboTables.Items.AddRange(new object[] {
            "SuccessCells",
            "RejectCells"});
            this.comboTables.Location = new System.Drawing.Point(110, 33);
            this.comboTables.Name = "comboTables";
            this.comboTables.Size = new System.Drawing.Size(150, 37);
            this.comboTables.TabIndex = 1;
            // 
            // lblQuery
            // 
            this.lblQuery.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.lblQuery.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblQuery.Location = new System.Drawing.Point(280, 35);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(60, 30);
            this.lblQuery.TabIndex = 2;
            this.lblQuery.Text = "ID:";
            // 
            // txtQuery
            // 
            this.txtQuery.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.txtQuery.Location = new System.Drawing.Point(346, 33);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(180, 39);
            this.txtQuery.TabIndex = 3;
            // 
            // lblStart
            // 
            this.lblStart.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.lblStart.Location = new System.Drawing.Point(538, 33);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(100, 30);
            this.lblStart.TabIndex = 4;
            this.lblStart.Text = "開始時間";
            // 
            // dtpStart
            // 
            this.dtpStart.CustomFormat = "yyyy-MM-dd";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(644, 33);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(156, 39);
            this.dtpStart.TabIndex = 5;
            // 
            // cbStartHour
            // 
            this.cbStartHour.Location = new System.Drawing.Point(810, 33);
            this.cbStartHour.Name = "cbStartHour";
            this.cbStartHour.Size = new System.Drawing.Size(50, 37);
            this.cbStartHour.TabIndex = 6;
            // 
            // cbStartMinute
            // 
            this.cbStartMinute.Location = new System.Drawing.Point(870, 33);
            this.cbStartMinute.Name = "cbStartMinute";
            this.cbStartMinute.Size = new System.Drawing.Size(50, 37);
            this.cbStartMinute.TabIndex = 7;
            // 
            // cbStartSecond
            // 
            this.cbStartSecond.Location = new System.Drawing.Point(930, 33);
            this.cbStartSecond.Name = "cbStartSecond";
            this.cbStartSecond.Size = new System.Drawing.Size(50, 37);
            this.cbStartSecond.TabIndex = 8;
            // 
            // lblEnd
            // 
            this.lblEnd.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.lblEnd.Location = new System.Drawing.Point(538, 78);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(100, 30);
            this.lblEnd.TabIndex = 9;
            this.lblEnd.Text = "結束時間";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(644, 78);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(156, 39);
            this.dtpEnd.TabIndex = 10;
            // 
            // cbEndHour
            // 
            this.cbEndHour.Location = new System.Drawing.Point(810, 78);
            this.cbEndHour.Name = "cbEndHour";
            this.cbEndHour.Size = new System.Drawing.Size(50, 37);
            this.cbEndHour.TabIndex = 11;
            // 
            // cbEndMinute
            // 
            this.cbEndMinute.Location = new System.Drawing.Point(870, 78);
            this.cbEndMinute.Name = "cbEndMinute";
            this.cbEndMinute.Size = new System.Drawing.Size(50, 37);
            this.cbEndMinute.TabIndex = 12;
            // 
            // cbEndSecond
            // 
            this.cbEndSecond.Location = new System.Drawing.Point(930, 78);
            this.cbEndSecond.Name = "cbEndSecond";
            this.cbEndSecond.Size = new System.Drawing.Size(50, 37);
            this.cbEndSecond.TabIndex = 13;
            // 
            // btnQuery
            // 
            this.btnQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(201)))), ((int)(((byte)(176)))));
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Font = new System.Drawing.Font("微軟正黑體", 14F, System.Drawing.FontStyle.Bold);
            this.btnQuery.ForeColor = System.Drawing.Color.White;
            this.btnQuery.Location = new System.Drawing.Point(1010, 38);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(76, 75);
            this.btnQuery.TabIndex = 14;
            this.btnQuery.Text = "執行查詢";
            this.btnQuery.UseVisualStyleBackColor = false;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dgvResults
            // 
            this.dgvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvResults.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvResults.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvResults.ColumnHeadersHeight = 29;
            this.dgvResults.Location = new System.Drawing.Point(20, 210);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersWidth = 51;
            this.dgvResults.Size = new System.Drawing.Size(1199, 380);
            this.dgvResults.TabIndex = 2;
            // 
            // Form2dbshow
            // 
            this.ClientSize = new System.Drawing.Size(1241, 600);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.groupBoxSearch);
            this.Controls.Add(this.dgvResults);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.MinimumSize = new System.Drawing.Size(820, 620);
            this.Name = "Form2dbshow";
            this.Text = "分選資料庫檢視器";
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
