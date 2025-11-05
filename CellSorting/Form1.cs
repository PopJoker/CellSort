using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
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
            foreach (System.Windows.Forms.Control ctl in this.Controls)
            {
                ctl.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }
            cbboxSwithMode.SelectedIndex = 0;
        }

        private int groupSize;
        private double vLimit;
        private double rLimit;
        private int dLimit;
        private int cLimit;

        private bool fakeMode = false; // 模擬模式開啟

        private List<BatteryCell> ImportOrFake(string filePath, string mode)
        {
            var list = ImportFromExcel(filePath, mode);

            if (fakeMode)
            {
                int serial = 0;

                foreach (var cell in list)
                {
                    if (string.IsNullOrWhiteSpace(cell.ID))
                    {
                        cell.ID = $"GA0125A-{DateTime.Today:yyMMdd}{serial:D4}";
                        cell.Date = DateTime.Today.AddDays(-serial % 15);
                        serial++;
                    }
                }
            }
            else
            {
                foreach (var cell in list)
                {
                    if (!string.IsNullOrWhiteSpace(cell.ID))
                    {
                        cell.Date = ParseDateFromId(cell.ID, mode);
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
            public double Capacity { get; set; } // 新增容量
            public DateTime Date { get; set; }
            public int? Pack { get; set; } // 分組結果
            public string RejectReason { get; set; }

            // 新增完整欄位
            public double DeltaV { get; set; }
            public int DeltaDay { get; set; }
            public double DeltaCapacity { get; set; } // 新增 Δ容量
            public int GroupSize { get; set; }
            public double VoltageLimit { get; set; }
            public double IRLimit { get; set; }
            public int DayLimit { get; set; }
            public double CapacityLimit { get; set; } // 分組容量差上限
        }

        private static readonly Dictionary<char, int> Base31Map = new Dictionary<char, int>
        {
            {'1',1},{'2',2},{'3',3},{'4',4},{'5',5},{'6',6},{'7',7},{'8',8},{'9',9},
            {'A',10},{'B',11},{'C',12},{'D',13},{'E',14},{'F',15},{'G',16},{'H',17},{'J',18},{'K',19},
            {'L',20},{'M',21},{'N',22},{'P',23},{'R',24},{'S',25},{'T',26},{'U',27},{'V',28},{'W',29},{'X',30},{'Y',31}
        };

        private void Log(string msg)
        {
            if (lstLog.InvokeRequired)
            {
                lstLog.Invoke(new Action(() =>
                {
                    lstLog.Items.Add(msg);
                    lstLog.TopIndex = lstLog.Items.Count - 1; // 自動滾動到最後
                }));
            }
            else
            {
                lstLog.Items.Add(msg);
                lstLog.TopIndex = lstLog.Items.Count - 1; // 自動滾動到最後
            }
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} - {msg}");
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            string mode = cbboxSwithMode.SelectedItem?.ToString() ?? "EVE";

            // 讀取設定值
            if (!int.TryParse(txtCellNum.Text, out groupSize))
            {
                MessageBox.Show("請輸入有效的分組數字！");
                return;
            }

            if (!double.TryParse(txtIR.Text, out rLimit))
                rLimit = 0;

            if (!double.TryParse(txtVdelta.Text, out vLimit))
                vLimit = 0;

            if (!int.TryParse(txtDDelta.Text, out dLimit))
                dLimit = 0;

            if ((mode == "EVE") && !int.TryParse(txtCapacityDelta.Text, out cLimit))
                cLimit = 0;

            var ofd = new OpenFileDialog { Filter = "Excel Files|*.xlsx;*.xls" };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            string savePath = null;
            using (var sfd = new SaveFileDialog { Filter = "Excel Files|*.xlsx", FileName = $"CellSortingResult_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx" })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;
                savePath = sfd.FileName;
            }

            btnImport.Enabled = false;
            progressBar1.Value = 0;
            lblStatus.Text = "開始匯入...";

            try
            {
                btnImport.Enabled = false;
                progressBar1.Value = 0;
                lblStatus.Text = "開始匯入...";

                // 匯入 Excel
                var cells = await Task.Run(() =>
                {
                    UpdateProgress(10, "匯入 Excel...");
                    var list = ImportOrFake(ofd.FileName, mode);
                    if (list == null) return null;

                    foreach (var cell in list)
                    {
                        if (!string.IsNullOrWhiteSpace(cell.ID))
                        {
                            try { cell.Date = ParseDateFromId(cell.ID, mode); }
                            catch { cell.Date = DateTime.Today; }
                        }
                    }

                    UpdateProgress(30, "匯入完成");
                    return list;
                });

                if (cells == null) return;

                // 分組
                List<List<BatteryCell>> groups = null;
                await Task.Run(() =>
                {
                    UpdateProgress(40, "開始分組...");

                    try
                    {
                        if (mode == "Gus")
                        {
                            groups = GroupCells(cells, groupSize, vLimit, rLimit, dLimit);
                        }
                        else if (mode == "EVE")
                        {
                            groups = GroupCellsEVE(cells, groupSize, vLimit, rLimit, dLimit, cLimit);
                        }
                        else
                        {
                            groups = GroupCells(cells, groupSize, vLimit, rLimit, dLimit);
                        }
                    }
                    catch (Exception ex)
                    {
                        // 捕捉所有異常
                        MessageBox.Show($"分組過程發生錯誤：{ex.Message}", "錯誤",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        groups = new List<List<BatteryCell>>();
                    }
                });

                // 匯出 Excel
                await Task.Run(() =>
                {
                    UpdateProgress(75, "開始匯出...");
                    try
                    {
                        ExportToExcelToPath(groups, cells, savePath, mode);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"匯出 Excel 發生錯誤：{ex.Message}", "錯誤",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        private DateTime ParseDateFromId(string id, string mode)
        {
            if (string.IsNullOrWhiteSpace(id)) return DateTime.Today;

            if (mode == "EVE")
            {
                if (id.Length < 8) return DateTime.Today;
                try
                {
                    char yearChar = id[5];
                    char monthChar = id[6];
                    char dayChar = id[7];

                    int year = Base31Map.ContainsKey(yearChar) ? 2000 + Base31Map[yearChar] : 2000;
                    int month = Base31Map.ContainsKey(monthChar) ? Base31Map[monthChar] : 1;
                    int day = Base31Map.ContainsKey(dayChar) ? Base31Map[dayChar] : 1;

                    return new DateTime(year, month, day);
                }
                catch
                {
                    return DateTime.Today;
                }
            }
            else if (mode == "Gus")
            {
                string core = id.Split('-').LastOrDefault();
                if (string.IsNullOrWhiteSpace(core) || core.Length < 4)
                    return DateTime.Today;

                try
                {
                    int year = 2000 + int.Parse(core.Substring(0, 2));

                    char monthChar = core[2];
                    int month = Char.IsDigit(monthChar) ? int.Parse(monthChar.ToString()) : (Base31Map.ContainsKey(monthChar) ? Base31Map[monthChar] : 1);

                    char dayChar = core[3];
                    int day = Char.IsDigit(dayChar) ? int.Parse(dayChar.ToString()) : (Base31Map.ContainsKey(dayChar) ? Base31Map[dayChar] : 1);

                    return new DateTime(year, month, day);
                }
                catch
                {
                    return DateTime.Today;
                }
            }

            // fallback
            return DateTime.Today;
        }

        private void ExportToExcelToPath(List<List<BatteryCell>> groups, List<BatteryCell> allCells, string path, string mode)
        {
            using (var workbook = new XLWorkbook())
            {
                var wsSuccess = workbook.Worksheets.Add("Success");
                var wsFail = workbook.Worksheets.Add("Rejects");

                int col = 1;

                // ======= 設定標題 =======
                wsSuccess.Cell(1, col++).Value = "Pack";
                wsSuccess.Cell(1, col++).Value = "ID";
                wsSuccess.Cell(1, col++).Value = "Resistance(Ω)";
                wsSuccess.Cell(1, col++).Value = "Voltage(V)";

                if (mode == "EVE")
                {
                    wsSuccess.Cell(1, col++).Value = "Capacity";
                }

                wsSuccess.Cell(1, col++).Value = "Date";
                wsSuccess.Cell(1, col++).Value = "DeltaV(mV)";
                wsSuccess.Cell(1, col++).Value = "DeltaDay";

                if (mode == "EVE")
                {
                    wsSuccess.Cell(1, col++).Value = "DeltaCapacity";
                }

                wsSuccess.Cell(1, col++).Value = "GroupSize";
                wsSuccess.Cell(1, col++).Value = "VoltageLimit(mV)";
                wsSuccess.Cell(1, col++).Value = "IRLimit(mΩ)";
                wsSuccess.Cell(1, col++).Value = "DayLimit";

                if (mode == "EVE")
                {
                    wsSuccess.Cell(1, col++).Value = "CapacityLimit";
                }

                // ======= 填入成功分組 =======
                int row = 2;
                foreach (var group in groups)
                {
                    Console.WriteLine($"Group Pack {group[0].Pack}");
                    foreach (var cell in group)
                    {
                        Console.WriteLine($"  Cell {cell.ID}, Pack={cell.Pack}");
                        col = 1;
                        wsSuccess.Cell(row, col++).Value = cell.Pack;
                        wsSuccess.Cell(row, col++).Value = cell.ID;
                        wsSuccess.Cell(row, col++).Value = cell.Resistance;
                        wsSuccess.Cell(row, col++).Value = cell.Voltage;

                        if (mode == "EVE")
                            wsSuccess.Cell(row, col++).Value = cell.Capacity;

                        wsSuccess.Cell(row, col++).Value = cell.Date;
                        wsSuccess.Cell(row, col++).Value = cell.DeltaV;
                        wsSuccess.Cell(row, col++).Value = cell.DeltaDay;

                        if (mode == "EVE")
                            wsSuccess.Cell(row, col++).Value = cell.DeltaCapacity;

                        wsSuccess.Cell(row, col++).Value = cell.GroupSize;
                        wsSuccess.Cell(row, col++).Value = cell.VoltageLimit;
                        wsSuccess.Cell(row, col++).Value = cell.IRLimit;
                        wsSuccess.Cell(row, col++).Value = cell.DayLimit;

                        if (mode == "EVE")
                            wsSuccess.Cell(row, col++).Value = cell.CapacityLimit;

                        row++;
                    }
                }
                wsSuccess.Columns().AdjustToContents();

                // ======= 填入失敗電芯 =======
                col = 1;
                wsFail.Cell(1, col++).Value = "ID";
                wsFail.Cell(1, col++).Value = "Resistance(Ω)";
                wsFail.Cell(1, col++).Value = "Voltage(V)";

                if (mode == "EVE")
                    wsFail.Cell(1, col++).Value = "Capacity";

                wsFail.Cell(1, col++).Value = "Date";
                wsFail.Cell(1, col++).Value = "Reason";
                wsFail.Cell(1, col++).Value = "DeltaV(mV)";
                wsFail.Cell(1, col++).Value = "DeltaDay";

                if (mode == "EVE")
                    wsFail.Cell(1, col++).Value = "DeltaCapacity";

                wsFail.Cell(1, col++).Value = "GroupSize";
                wsFail.Cell(1, col++).Value = "VoltageLimit(mV)";
                wsFail.Cell(1, col++).Value = "IRLimit(mΩ)";
                wsFail.Cell(1, col++).Value = "DayLimit";

                if (mode == "EVE")
                    wsFail.Cell(1, col++).Value = "CapacityLimit";

                row = 2;
                foreach (var cell in allCells.Where(c => c.Pack == null))
                {
                    col = 1;
                    wsFail.Cell(row, col++).Value = cell.ID;
                    wsFail.Cell(row, col++).Value = cell.Resistance;
                    wsFail.Cell(row, col++).Value = cell.Voltage;

                    if (mode == "EVE")
                        wsFail.Cell(row, col++).Value = cell.Capacity;

                    wsFail.Cell(row, col++).Value = cell.Date;
                    wsFail.Cell(row, col++).Value = cell.RejectReason;
                    wsFail.Cell(row, col++).Value = cell.DeltaV;
                    wsFail.Cell(row, col++).Value = cell.DeltaDay;

                    if (mode == "EVE")
                        wsFail.Cell(row, col++).Value = cell.DeltaCapacity;

                    wsFail.Cell(row, col++).Value = cell.GroupSize;
                    wsFail.Cell(row, col++).Value = cell.VoltageLimit;
                    wsFail.Cell(row, col++).Value = cell.IRLimit;
                    wsFail.Cell(row, col++).Value = cell.DayLimit;

                    if (mode == "EVE")
                        wsFail.Cell(row, col++).Value = cell.CapacityLimit;

                    row++;
                }
                wsFail.Columns().AdjustToContents();

                // ======= 新增索引頁面 =======
                var wsIndex = workbook.Worksheets.Add("ID_to_Pack");

                // 設定標題
                wsIndex.Cell(1, 1).Value = "ID";
                wsIndex.Cell(1, 2).Value = "to Pack";

                // 假設給使用者 50 個輸入欄位
                int maxRows = 50;

                for (int rowIndex = 2; rowIndex <= maxRows + 1; rowIndex++)
                {
                    // 第一欄留空給使用者輸入 ID
                    wsIndex.Cell(rowIndex, 1).Value = "";

                    // 第二欄公式查 Success 工作表 ID (B列) 對應 Pack (A列)
                    wsIndex.Cell(rowIndex, 2).FormulaA1 =
                         $"=IFERROR(INDEX(Success!A:A, MATCH({wsIndex.Cell(rowIndex, 1).Address}, Success!B:B, 0)), \"N/A\")";
                }

                wsIndex.Columns().AdjustToContents();


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

        private List<List<BatteryCell>> GroupCells(
    List<BatteryCell> cells,
    int groupSize,
    double vLimit,
    double rLimit,
    int dLimit)
        {
            var ungrouped = new LinkedList<BatteryCell>(cells.OrderBy(c => c.Voltage)); // 預排序
            var result = new List<List<BatteryCell>>();
            int packIndex = 1;

            int totalCells = ungrouped.Count;
            int processedCells = 0;

            // 先剔除 IR 超標電芯
            var node = ungrouped.First;
            while (node != null)
            {
                var next = node.Next;
                var c = node.Value;
                if (chkCheckIR.Checked && c.Resistance * 1000 > rLimit)
                {
                    c.RejectReason = $"內阻超標：{c.Resistance * 1000:F4} mΩ > 上限 {rLimit:F4} mΩ";
                    c.DeltaV = 0;
                    c.DeltaDay = 0;
                    c.GroupSize = groupSize;
                    c.VoltageLimit = vLimit;
                    c.IRLimit = rLimit;
                    c.DayLimit = dLimit;
                    Log($"Reject IR 超標: {c.ID} ({c.RejectReason})");

                    ungrouped.Remove(node);
                    processedCells++;
                    if (processedCells % 5 == 0)
                    {
                        int percent = 40 + (int)(35.0 * processedCells / totalCells);
                        UpdateProgress(percent, $"分組中...已處理 {processedCells}/{totalCells} 顆");
                    }
                }
                node = next;
            }

            // 分組主循環
            while (ungrouped.Count >= groupSize)
            {
                // 取前 groupSize 顆作 candidate
                var candidateGroup = ungrouped.Take(groupSize).ToList();

                double deltaV = (candidateGroup.Max(c => c.Voltage) - candidateGroup.Min(c => c.Voltage)) * 1000;
                int deltaDay = (candidateGroup.Max(c => c.Date) - candidateGroup.Min(c => c.Date)).Days;

                bool violation = (chkCheckVdelta.Checked && deltaV > vLimit) ||
                                 (chkCheckDdelta.Checked && deltaDay > dLimit) ||
                                 (chkCheckIR.Checked && candidateGroup.Any(c => c.Resistance * 1000 > rLimit));

                if (!violation)
                {
                    // 成功分組
                    foreach (var c in candidateGroup)
                    {
                        c.Pack = packIndex;
                        c.DeltaV = deltaV;
                        c.DeltaDay = deltaDay;
                        c.GroupSize = groupSize;
                        c.VoltageLimit = vLimit;
                        c.IRLimit = rLimit;
                        c.DayLimit = dLimit;
                    }
                    result.Add(candidateGroup);
                    Log($"成功分組 Pack {packIndex}: {string.Join(", ", candidateGroup.Select(x => x.ID))} ΔV={deltaV:F1} mV ΔDay={deltaDay}");
                    packIndex++;

                    // 從 ungrouped 移除
                    foreach (var c in candidateGroup) ungrouped.Remove(c);

                    processedCells += candidateGroup.Count;
                    if (processedCells % 5 == 0)
                    {
                        int percent = 40 + (int)(35.0 * processedCells / totalCells);
                        UpdateProgress(percent, $"分組中...已處理 {processedCells}/{totalCells} 顆");
                    }
                }
                else
                {
                    // 標記最離群或違規的電芯為 Reject
                    var outlier = candidateGroup
                        .OrderByDescending(c =>
                            Math.Abs(c.Voltage - candidateGroup.Average(x => x.Voltage)) * 1000 +
                            Math.Abs(c.Date.DayOfYear - candidateGroup.Average(x => x.Date.DayOfYear))
                        ).First();

                    outlier.RejectReason = $"無法組成有效 {groupSize} 顆組 (ΔV={deltaV:F1}, ΔDay={deltaDay}, IR={outlier.Resistance * 1000:F4})";
                    ungrouped.Remove(outlier);
                    Log($"Reject: {outlier.ID} ({outlier.RejectReason})");
                    processedCells++;

                    if (processedCells % 5 == 0)
                    {
                        int percent = 40 + (int)(35.0 * processedCells / totalCells);
                        UpdateProgress(percent, $"分組中...已處理 {processedCells}/{totalCells} 顆");
                    }
                }
            }

            // 尾端不足組，全部 Reject
            foreach (var c in ungrouped)
            {
                double deltaV = 0;
                int deltaDay = 0;
                c.Pack = null;
                if (c.RejectReason == null)
                {
                    c.RejectReason = $"尾端不足組或無法組成有效 {groupSize} 顆組，剩餘 {ungrouped.Count} 顆";
                    if (ungrouped.Count > 1)
                    {
                        deltaV = (ungrouped.Max(x => x.Voltage) - ungrouped.Min(x => x.Voltage)) * 1000;
                        deltaDay = (ungrouped.Max(x => x.Date) - ungrouped.Min(x => x.Date)).Days;
                    }
                }
                c.DeltaV = deltaV;
                c.DeltaDay = deltaDay;
                c.GroupSize = groupSize;
                c.VoltageLimit = vLimit;
                c.IRLimit = rLimit;
                c.DayLimit = dLimit;

                Log($"尾端標記 Reject: {c.ID} ({c.RejectReason})");
                processedCells++;

                if (processedCells % 5 == 0)
                {
                    int percent = 40 + (int)(35.0 * processedCells / totalCells);
                    UpdateProgress(percent, $"分組中...已處理 {processedCells}/{totalCells} 顆");
                }
            }

            return result;
        }


        private List<List<BatteryCell>> GroupCellsEVE(
    List<BatteryCell> cells,
    int groupSize,
    double vLimit,
    double rLimit,
    double dLimit,
    double cLimit
)
        {
            var processedSet = new HashSet<string>();
            var result = new List<List<BatteryCell>>();
            int packIndex = 1;
            int totalCells = cells.Count;
            int processedCells = 0;
            int maxRetry = 5; // 每個電芯最大重試次數

            // 篩選 IR 合格
            var ungrouped = new LinkedList<BatteryCell>(
                cells.Where(c => !chkCheckIR.Checked || c.Resistance * 1000 <= rLimit)
                     .OrderBy(c => c.Voltage)
            );

            // 剔除 IR 超標
            foreach (var c in cells.Except(ungrouped))
                MarkRejected(c, $"內阻超標：{c.Resistance * 1000:F2} mΩ > 上限 {rLimit:F2} mΩ", ref processedCells, totalCells, processedSet);

            // 記錄重試次數
            var retryCount = ungrouped.ToDictionary(c => c.ID, c => 0);

            // 依前綴分組
            var prefixGroups = ungrouped.GroupBy(c => GetIdPrefix(c.ID));
            foreach (var prefixGroup in prefixGroups)
            {
                var subResult = GroupCellsBySubsetRetry(prefixGroup.ToList(), groupSize, vLimit, rLimit, dLimit, cLimit,
                                                        ref packIndex, ref processedCells, totalCells, processedSet, retryCount, maxRetry);
                result.AddRange(subResult);
            }

            return result;
        }

        private List<List<BatteryCell>> GroupCellsBySubsetRetry(
            List<BatteryCell> subset,
            int groupSize,
            double vLimit,
            double rLimit,
            double dLimit,
            double cLimit,
            ref int packIndex,
            ref int processedCells,
            int totalCells,
            HashSet<string> processedSet,
            Dictionary<string, int> retryCount,
            int maxRetry
        )
        {
            var result = new List<List<BatteryCell>>();
            var ungrouped = new LinkedList<BatteryCell>(subset.OrderBy(c => c.Voltage));

            while (ungrouped.Count >= groupSize)
            {
                var candidateGroup = ungrouped.Take(groupSize).ToList();

                double deltaV = (candidateGroup.Max(c => c.Voltage) - candidateGroup.Min(c => c.Voltage)) * 1000;
                double deltaC = candidateGroup.Max(c => c.Capacity) - candidateGroup.Min(c => c.Capacity);
                int deltaDay = (candidateGroup.Max(c => c.Date) - candidateGroup.Min(c => c.Date)).Days;

                bool violation = (chkCheckVdelta.Checked && deltaV > vLimit) ||
                                 (chkCheckCapacity.Checked && deltaC > cLimit) ||
                                 (chkCheckDdelta.Checked && deltaDay > dLimit);

                if (!violation)
                {
                    foreach (var c in candidateGroup)
                    {
                        c.Pack = packIndex;
                        c.DeltaV = deltaV;
                        c.DeltaCapacity = deltaC;
                        c.DeltaDay = deltaDay;
                        c.GroupSize = groupSize;
                        c.VoltageLimit = vLimit;
                        c.IRLimit = rLimit;
                        c.CapacityLimit = cLimit;
                        c.DayLimit = (int)Math.Round(dLimit, 0);
                    }

                    result.Add(candidateGroup);
                    packIndex++;

                    foreach (var c in candidateGroup) ungrouped.Remove(c);
                    foreach (var c in candidateGroup)
                        if (processedSet.Add(c.ID)) processedCells++;

                    int percent = 40 + (int)(35.0 * processedCells / totalCells);
                    UpdateProgress(percent, $"分組中...已處理 {processedCells}/{totalCells} 顆");
                }
                else
                {
                    double avgV = candidateGroup.Average(c => c.Voltage);
                    double avgC = candidateGroup.Average(c => c.Capacity);
                    double avgDay = candidateGroup.Average(c => c.Date.ToOADate());

                    var outlier = candidateGroup.OrderByDescending(c =>
                        Math.Abs(c.Voltage - avgV) * 1000 +
                        Math.Abs(c.Capacity - avgC) +
                        Math.Abs(c.Date.ToOADate() - avgDay)).First();

                    retryCount[outlier.ID]++;

                    if (retryCount[outlier.ID] > maxRetry)
                    {
                        MarkRejected(outlier,
                                     $"重試 {maxRetry} 次仍無法組成有效 {groupSize} 顆組 (ΔV={deltaV:F1}, ΔC={deltaC:F0}, ΔDay={deltaDay})",
                                     ref processedCells, totalCells, processedSet);
                        ungrouped.Remove(outlier);
                    }
                    else
                    {
                        // 放回尾端，下次重試
                        ungrouped.Remove(outlier);
                        ungrouped.AddLast(outlier);
                        Log($"重試分組，暫存離群電芯 {outlier.ID} (第 {retryCount[outlier.ID]} 次)");
                    }

                    int percent = 40 + (int)(35.0 * processedCells / totalCells);
                    UpdateProgress(percent, $"分組中...已處理 {processedCells}/{totalCells} 顆");
                }
            }

            // 尾端不足組
            foreach (var c in ungrouped)
            {
                double deltaV = ungrouped.Count > 1 ? (ungrouped.Max(x => x.Voltage) - ungrouped.Min(x => x.Voltage)) * 1000 : 0;
                double deltaC = ungrouped.Count > 1 ? ungrouped.Max(x => x.Capacity) - ungrouped.Min(x => x.Capacity) : 0;
                int deltaDay = ungrouped.Count > 1 ? (ungrouped.Max(x => x.Date) - ungrouped.Min(x => x.Date)).Days : 0;

                if (string.IsNullOrEmpty(c.RejectReason))
                    c.RejectReason = $"尾端不足組或無法組成有效 {groupSize} 顆組，剩餘 {ungrouped.Count} 顆";

                c.Pack = null;
                c.DeltaV = deltaV;
                c.DeltaCapacity = deltaC;
                c.DeltaDay = deltaDay;
                c.GroupSize = groupSize;
                c.VoltageLimit = vLimit;
                c.IRLimit = rLimit;
                c.CapacityLimit = cLimit;
                c.DayLimit = (int)Math.Round(dLimit, 0);

                if (processedSet.Add(c.ID))
                    processedCells++;
            }

            return result;
        }

        private void MarkRejected(BatteryCell c, string reason, ref int processedCells, int totalCells, HashSet<string> processedSet)
        {
            c.RejectReason = reason;
            c.Pack = null;
            c.DeltaV = 0;
            c.DeltaCapacity = 0;
            c.DeltaDay = 0;
            Log($"電芯 {c.ID} 標記 Reject: {reason}");
            if (processedSet.Add(c.ID))
                processedCells++;
            int percent = 40 + (int)(35.0 * processedCells / totalCells);
            UpdateProgress(percent, $"分組中...已處理 {processedCells}/{totalCells} 顆");
        }
        /// <summary>
        /// 取得電芯ID前綴（例如 "H9250E1GP1107270" → "H9250"）
        /// </summary>
        private string GetIdPrefix(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "UNKNOWN";

            // 前綴取連續字母（直到數字出現）
            var prefix = new string(id.TakeWhile(char.IsLetter).ToArray());

            if (string.IsNullOrWhiteSpace(prefix))
                prefix = id.Length >= 5 ? id.Substring(0, 5) : id;

            return prefix;
        }

        private double SafeGetDouble(IXLCell cell)
        {
            if (cell == null || cell.IsEmpty())
                return 0; // 空白直接給 0

            try
            {
                return cell.GetDouble();
            }
            catch
            {
                // 如果 GetDouble 失敗，改用字串解析
                var s = cell.GetString().Trim();
                if (double.TryParse(s, out double val))
                    return val;
                return 0; // 都失敗就給 0
            }
        }

        private List<BatteryCell> ImportFromExcel(string filePath, string mode)
        {
            var list = new List<BatteryCell>();
            var idDict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            using (var workbook = new XLWorkbook(filePath))
            {
                var ws = workbook.Worksheets.First();
                int row = 1;

                while (!ws.Cell(row, 1).IsEmpty())
                {
                    string id = ws.Cell(row, 1).GetString().Trim().ToUpper();
                    double res = SafeGetDouble(ws.Cell(row, 2));
                    double volt = SafeGetDouble(ws.Cell(row, 3));
                    double capacity = SafeGetDouble(ws.Cell(row, 4));

                    DateTime date = fakeMode ? DateTime.Today : ParseDateFromId(id, mode);

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
                        Resistance = res,
                        Voltage = volt,
                        Capacity = capacity,
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
                btn.BackColor = System.Drawing.Color.LightBlue; // 背景變亮
                btn.ForeColor = System.Drawing.Color.White;     // 文字變白
                btn.Font = new System.Drawing.Font(btn.Font, FontStyle.Bold); // 字體加粗
            }
        }

        private void btnImport_MouseLeave(object sender, EventArgs e)
        {
            var btn = sender as System.Windows.Forms.Button;
            if (btn != null)
            {
                btn.BackColor = SystemColors.Control; // 恢復預設背景
                btn.ForeColor = SystemColors.ControlText; // 恢復文字顏色
                btn.Font = new System.Drawing.Font(btn.Font, FontStyle.Regular); // 恢復普通字體
            }
        }

        private void chkCheckVdelta_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheckVdelta.Checked)
            {
                txtVdelta.BackColor = System.Drawing.Color.WhiteSmoke;
                txtVdelta.Enabled = true;
            }
            else
            {
                txtVdelta.Text = "";
                txtVdelta.BackColor = System.Drawing.Color.LightGray;
                txtVdelta.Enabled = false;
            }
        }

        private void chkCheckDdelta_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheckDdelta.Checked)
            {
                txtDDelta.BackColor = System.Drawing.Color.WhiteSmoke;
                txtDDelta.Enabled = true;
            }
            else
            {
                txtDDelta.Text = "";
                txtDDelta.BackColor = System.Drawing.Color.LightGray;
                txtDDelta.Enabled = false;
            }
        }

        private void chkCheckIR_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheckIR.Checked)
            {
                txtIR.BackColor = System.Drawing.Color.WhiteSmoke;
                txtIR.Enabled = true;
            }
            else
            {
                txtIR.Text = "";
                txtIR.BackColor = System.Drawing.Color.LightGray;
                txtIR.Enabled = false; 
            }
        }

        private void chkCheckCapacity_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheckCapacity.Checked)
            {
                txtCapacityDelta.BackColor = System.Drawing.Color.WhiteSmoke;
                txtCapacityDelta.Enabled = true;
            }
            else
            {
                txtCapacityDelta.Text = "";
                txtCapacityDelta.BackColor = System.Drawing.Color.LightGray;
                txtCapacityDelta.Enabled = false;
            }
        }

        private void cbboxSwithMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbboxSwithMode.SelectedItem == null)
                return;

            string mode = cbboxSwithMode.SelectedItem.ToString();

            if (mode == "Gus")
            {
                // Gus 模式 → 鎖住容量欄位
                txtCapacityDelta.Enabled = false;
                txtCapacityDelta.BackColor = System.Drawing.Color.LightGray;
                txtCapacityDelta.Text = "";
                chkCheckCapacity.Checked = false;
                chkCheckCapacity.Enabled = false;
            }
            else if (mode == "EVE")
            {
                // EVE 模式 → 開啟容量欄位
                txtCapacityDelta.Enabled = true;
                txtCapacityDelta.BackColor = System.Drawing.Color.WhiteSmoke;
                chkCheckCapacity.Checked = true;
                chkCheckCapacity.Enabled = true;
            }
        }

    }
}
