using ClosedXML.Excel;
using DocumentFormat.OpenXml.Math;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CellSorting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 固定窗體大小
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            // 禁止自動縮放
            this.AutoSize = false;
            this.AutoScaleMode = AutoScaleMode.None;

            // 每個控件 Anchor 設為 Top, Left
            foreach (Control ctl in this.Controls)
            {
                ctl.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }
        }

        private int groupSize;
        private double vLimit;
        private double rLimit;
        private int dLimit;

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblStatus;

        private bool fakeMode = true; // 模擬模式開啟

        private List<BatteryCell> ImportOrFake(string filePath)
        {
            var list = ImportFromExcel(filePath);

            if (fakeMode)
            {
                int serial = 0;

                foreach (var cell in list)
                {
                    // 如果 ID 是空，就生成
                    if (string.IsNullOrWhiteSpace(cell.ID))
                    {
                        // 使用今天日期 + 序號生成唯一 ID
                        cell.ID = $"GA0125A-{DateTime.Today:yyMMdd}{serial:D4}";

                        // 模擬日期：今天往前 0~14 天
                        cell.Date = DateTime.Today.AddDays(-serial % 15);

                        serial++;
                    }
                }
            }
            else
            {
                // 正式模式：如果 ID 有值，解析日期
                foreach (var cell in list)
                {
                    if (!string.IsNullOrWhiteSpace(cell.ID))
                    {
                        try
                        {
                            cell.Date = ParseDateFromId(cell.ID);
                        }
                        catch
                        {
                            cell.Date = DateTime.Today; // 解析失敗就給今天
                        }
                    }
                }
            }

            return list;
        }

        public class BatteryCell
        {
            public string ID { get; set; }
            public double Voltage { get; set; }
            public double Resistance { get; set; }
            public DateTime Date { get; set; }
            public int? Pack { get; set; } // 分組結果
            public string RejectReason { get; set; }

            // 新增完整欄位
            public double DeltaV { get; set; }      // 電壓差
            public int DeltaDay { get; set; }       // 日期差
            public int GroupSize { get; set; }      // 分組大小
            public double VoltageLimit { get; set; } // 電壓上限
            public double IRLimit { get; set; }      // 內阻上限
            public int DayLimit { get; set; }        // 日期限制
        }


        private static readonly Dictionary<char, int> Base31Map = new Dictionary<char, int>
        {
            {'1',1},{'2',2},{'3',3},{'4',4},{'5',5},{'6',6},{'7',7},{'8',8},{'9',9},
            {'A',10},{'B',11},{'C',12},{'D',13},{'E',14},{'F',15},{'G',16},{'H',17},{'J',18},{'K',19},
            {'L',20},{'M',21},{'N',22},{'P',23},{'R',24},{'S',25},{'T',26},{'U',27},{'V',28},{'W',29},{'X',30},{'Y',31}
        };

        //private DateTime ParseDateFromId(string id)
        //{
        //    // 範例 ID: GA0125A-2564021204
        //    // 最後幾碼 "2564021204"
        //    string core = id.Split('-').Last();

        //    // 第3碼是月份（例如 6 = 六月）
        //    // 第4碼是日期（例如 4 = 4日）
        //    // 但你給的例子要看實際格式，我先假設是這樣
        //    int year = 2020 + (core[0] - '0');  // 例如 25 => 2025
        //    int month = Base31Map[core[2]];
        //    int day = Base31Map[core[3]];

        //    return new DateTime(year, month, day);
        //}
        private DateTime ParseDateFromId(string id)
        {
            // 範例 ID: GA0125A-2564021204
            string core = id.Split('-').LastOrDefault();
            if (string.IsNullOrWhiteSpace(core) || core.Length < 4)
                return DateTime.Today;

            try
            {
                // 年份：前兩碼
                int year = 2000 + int.Parse(core.Substring(0, 2));

                // 月份與日期：第三、第四碼使用 Base31Map
                int month = Base31Map.ContainsKey(core[2]) ? Base31Map[core[2]] : 1;
                int day = Base31Map.ContainsKey(core[3]) ? Base31Map[core[3]] : 1;

                return new DateTime(year, month, day);
            }
            catch
            {
                return DateTime.Today;
            }
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            // 讀取設定值
            groupSize = int.Parse(txtCellNum.Text);
            rLimit = double.Parse(txtIR.Text);
            vLimit = double.Parse(txtVdelta.Text);
            dLimit = int.Parse(txtDDelta.Text);

            // 1️⃣ UI 執行緒選檔
            var ofd = new OpenFileDialog { Filter = "Excel Files|*.xlsx;*.xls" };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            // 2️⃣ UI 執行緒選儲存檔名
            string savePath = null;
            using (var sfd = new SaveFileDialog { Filter = "Excel Files|*.xlsx", FileName = $"CellSortingResult_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx" })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;
                savePath = sfd.FileName;
            }

            // 3️⃣ UI 更新
            btnImport.Enabled = false;
            progressBar1.Value = 0;
            lblStatus.Text = "開始匯入...";

            try
            {
                // 匯入
                var cells = await Task.Run(() =>
                {
                    UpdateProgress(10, "匯入 Excel...");
                    var list = ImportOrFake(ofd.FileName);
                    UpdateProgress(30, "匯入完成");
                    return list;
                });

                if (cells == null) return;

                // 分組
                var groups = await Task.Run(() =>
                {
                    UpdateProgress(40, "開始分組...");
                    var res = GroupCells(cells, groupSize, vLimit, rLimit, dLimit);
                    UpdateProgress(70, "分組完成");
                    return res;
                });

                // 匯出
                await Task.Run(() =>
                {
                    UpdateProgress(75, "開始匯出...");
                    ExportToExcelToPath(groups, cells, savePath); // ⚠ 傳路徑，不用 Dialog
                    UpdateProgress(100, "完成!");
                });

                MessageBox.Show($"已輸出結果至：{savePath}", "匯出完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                btnImport.Enabled = true;
                lblStatus.Text = "就緒";
            }
        }

        private void ExportToExcelToPath(List<List<BatteryCell>> groups, List<BatteryCell> allCells, string path)
        {
            using (var workbook = new XLWorkbook())
            {
                // ======= 成功分組 =======
                var wsSuccess = workbook.Worksheets.Add("Success");

                // 標題欄位
                wsSuccess.Cell(1, 1).Value = "Pack";
                wsSuccess.Cell(1, 2).Value = "ID";
                wsSuccess.Cell(1, 3).Value = "Voltage";
                wsSuccess.Cell(1, 4).Value = "Resistance";
                wsSuccess.Cell(1, 5).Value = "Date";
                wsSuccess.Cell(1, 6).Value = "DeltaV";
                wsSuccess.Cell(1, 7).Value = "DeltaDay";
                wsSuccess.Cell(1, 8).Value = "GroupSize";
                wsSuccess.Cell(1, 9).Value = "VoltageLimit";
                wsSuccess.Cell(1, 10).Value = "IRLimit";
                wsSuccess.Cell(1, 11).Value = "DayLimit";

                int row = 2;
                foreach (var group in groups)
                {
                    foreach (var cell in group)
                    {
                        wsSuccess.Cell(row, 1).Value = cell.Pack;
                        wsSuccess.Cell(row, 2).Value = cell.ID;
                        wsSuccess.Cell(row, 3).Value = cell.Voltage;
                        wsSuccess.Cell(row, 4).Value = cell.Resistance;
                        wsSuccess.Cell(row, 5).Value = cell.Date;
                        wsSuccess.Cell(row, 6).Value = cell.DeltaV;
                        wsSuccess.Cell(row, 7).Value = cell.DeltaDay;
                        wsSuccess.Cell(row, 8).Value = cell.GroupSize;
                        wsSuccess.Cell(row, 9).Value = cell.VoltageLimit;
                        wsSuccess.Cell(row, 10).Value = cell.IRLimit;
                        wsSuccess.Cell(row, 11).Value = cell.DayLimit;
                        row++;
                    }
                }
                wsSuccess.Columns().AdjustToContents();

                // ======= 失敗電芯 =======
                var wsFail = workbook.Worksheets.Add("Rejects");
                wsFail.Cell(1, 1).Value = "ID";
                wsFail.Cell(1, 2).Value = "Voltage";
                wsFail.Cell(1, 3).Value = "Resistance";
                wsFail.Cell(1, 4).Value = "Date";
                wsFail.Cell(1, 5).Value = "Reason";
                wsFail.Cell(1, 6).Value = "DeltaV";
                wsFail.Cell(1, 7).Value = "DeltaDay";
                wsFail.Cell(1, 8).Value = "GroupSize";
                wsFail.Cell(1, 9).Value = "VoltageLimit";
                wsFail.Cell(1, 10).Value = "IRLimit";
                wsFail.Cell(1, 11).Value = "DayLimit";

                row = 2;
                foreach (var cell in allCells.Where(c => c.Pack == null))
                {
                    wsFail.Cell(row, 1).Value = cell.ID;
                    wsFail.Cell(row, 2).Value = cell.Voltage;
                    wsFail.Cell(row, 3).Value = cell.Resistance;
                    wsFail.Cell(row, 4).Value = cell.Date;
                    wsFail.Cell(row, 5).Value = cell.RejectReason;
                    wsFail.Cell(row, 6).Value = cell.DeltaV;
                    wsFail.Cell(row, 7).Value = cell.DeltaDay;
                    wsFail.Cell(row, 8).Value = cell.GroupSize;
                    wsFail.Cell(row, 9).Value = cell.VoltageLimit;
                    wsFail.Cell(row, 10).Value = cell.IRLimit;
                    wsFail.Cell(row, 11).Value = cell.DayLimit;
                    row++;
                }
                wsFail.Columns().AdjustToContents();

                workbook.SaveAs(path);
            }
        }


        private void UpdateProgress(int percent, string message)
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value = percent;
                    lblStatus.Text = message;
                }));
            }
            else
            {
                progressBar1.Value = percent;
                lblStatus.Text = message;
            }
        }

        private List<List<BatteryCell>> GroupCells(List<BatteryCell> cells, int groupSize, double vLimit, double rLimit, int dLimit)
        {
            var result = new List<List<BatteryCell>>();
            var ungrouped = new List<BatteryCell>(cells);
            int packIndex = 1;

            while (ungrouped.Count > 0)
            {
                var currentGroup = new List<BatteryCell>();
                currentGroup.Add(ungrouped[0]);
                ungrouped.RemoveAt(0);

                for (int i = ungrouped.Count - 1; i >= 0; i--)
                {
                    var c = ungrouped[i];

                    double avgVoltage = currentGroup.Average(x => x.Voltage);
                    DateTime firstDate = currentGroup[0].Date;
                    double avgDaysOffset = currentGroup.Average(d => (d.Date - firstDate).TotalDays);
                    DateTime avgDate = firstDate.AddDays(avgDaysOffset);

                    double vDiff = Math.Abs(c.Voltage - avgVoltage);
                    int dDiff = Math.Abs((c.Date - avgDate).Days);

                    List<string> reasons = new List<string>();
                    if (vDiff > vLimit) reasons.Add($"電壓差 {vDiff:F4} > {vLimit:F4}");
                    if (c.Resistance > rLimit) reasons.Add($"內阻 {c.Resistance:F4} > {rLimit:F4}");
                    if (dDiff > dLimit) reasons.Add($"日期差 {dDiff} 天 > {dLimit} 天");

                    if (reasons.Count == 0)
                    {
                        currentGroup.Add(c);
                        ungrouped.RemoveAt(i);
                        if (currentGroup.Count == groupSize)
                            break;
                    }
                    else
                    {
                        c.RejectReason = string.Join("; ", reasons);
                    }
                }

                if (currentGroup.Count == groupSize)
                {
                    double minV = currentGroup.Min(x => x.Voltage);
                    double maxV = currentGroup.Max(x => x.Voltage);
                    DateTime minDate = currentGroup.Min(x => x.Date);
                    DateTime maxDate = currentGroup.Max(x => x.Date);

                    foreach (var cc in currentGroup)
                    {
                        cc.Pack = packIndex;
                        cc.DeltaV = maxV - minV;
                        cc.DeltaDay = (maxDate - minDate).Days;
                        cc.GroupSize = groupSize;
                        cc.VoltageLimit = vLimit;
                        cc.IRLimit = rLimit;
                        cc.DayLimit = dLimit;
                    }

                    result.Add(new List<BatteryCell>(currentGroup));
                    packIndex++;
                }
                else
                {
                    foreach (var cc in currentGroup)
                    {
                        if (string.IsNullOrEmpty(cc.RejectReason))
                            cc.RejectReason = $"尾端不足組：僅有 {currentGroup.Count} 顆，不滿 {groupSize} 顆";

                        // 即使不滿組，也把完整欄位記錄
                        cc.DeltaV = 0;
                        cc.DeltaDay = 0;
                        cc.GroupSize = currentGroup.Count;
                        cc.VoltageLimit = vLimit;
                        cc.IRLimit = rLimit;
                        cc.DayLimit = dLimit;
                    }
                }
            }

            return result;
        }


        private List<BatteryCell> ImportFromExcel(string filePath)
        {
            var list = new List<BatteryCell>();
            var idDict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            using (var workbook = new XLWorkbook(filePath))
            {
                var ws = workbook.Worksheets.First();
                int row = 1;

                while (!ws.Cell(row, 2).IsEmpty()) // 第二列判斷空白
                {
                    string id = ws.Cell(row, 1).GetString().Trim().ToUpper();
                    double volt = ws.Cell(row, 2).GetDouble();
                    double res = ws.Cell(row, 3).GetDouble();//* 1000;

                    // fakeMode 下不要解析 ID
                    DateTime date = fakeMode ? DateTime.Today : ParseDateFromId(id);

                    // 檢查重複
                    if (!string.IsNullOrWhiteSpace(id) && idDict.ContainsKey(id))
                    {
                        int firstRow = idDict[id];
                        MessageBox.Show($"ID 重複：{id}，第 {firstRow} 列與第 {row} 列重複，請檢查 Excel 後重新匯入！",
                                        "重複警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }

                    list.Add(new BatteryCell
                    {
                        ID = id,
                        Voltage = volt,
                        Resistance = res,
                        Date = date
                    });

                    if (!string.IsNullOrWhiteSpace(id))
                        idDict[id] = row;

                    row++;
                }
            }

            return list;
        }

        private void btnImport_MouseEnter(object sender, EventArgs e)
        {
            var btn = sender as System.Windows.Forms.Button;
            if (btn != null)
            {
                btn.BackColor = Color.LightBlue; // 背景變亮
                btn.ForeColor = Color.White;     // 文字變白
                btn.Font = new Font(btn.Font, FontStyle.Bold); // 字體加粗
            }
        }

        private void btnImport_MouseLeave(object sender, EventArgs e)
        {
            var btn = sender as System.Windows.Forms.Button;
            if (btn != null)
            {
                btn.BackColor = SystemColors.Control; // 恢復預設背景
                btn.ForeColor = SystemColors.ControlText; // 恢復文字顏色
                btn.Font = new Font(btn.Font, FontStyle.Regular); // 恢復普通字體
            }
        }
    }
}
