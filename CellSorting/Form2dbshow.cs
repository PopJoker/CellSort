using System;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace CellSorting
{
    public partial class Form2dbshow : Form
    {
        private string dbPath;

        public Form2dbshow()
        {
            InitializeComponent();
            // 初始化資料庫路徑
            string appDataPath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "BatteryGrouping");
            dbPath = System.IO.Path.Combine(appDataPath, "BatteryData.db");

            // 預設選擇第一個 Table
            comboTables.SelectedIndex = 0;

            // 填充開始時間 ComboBox
            for (int i = 0; i < 24; i++) cbStartHour.Items.Add(i.ToString("D2"));
            for (int i = 0; i < 60; i++) { cbStartMinute.Items.Add(i.ToString("D2")); cbStartSecond.Items.Add(i.ToString("D2")); }
            cbStartHour.SelectedIndex = DateTime.Now.Hour;
            cbStartMinute.SelectedIndex = DateTime.Now.Minute;
            cbStartSecond.SelectedIndex = DateTime.Now.Second;

            // 填充結束時間 ComboBox
            for (int i = 0; i < 24; i++) cbEndHour.Items.Add(i.ToString("D2"));
            for (int i = 0; i < 60; i++) { cbEndMinute.Items.Add(i.ToString("D2")); cbEndSecond.Items.Add(i.ToString("D2")); }
            cbEndHour.SelectedIndex = DateTime.Now.Hour;
            cbEndMinute.SelectedIndex = DateTime.Now.Minute;
            cbEndSecond.SelectedIndex = DateTime.Now.Second;

            // 自動載入資料
            LoadData(comboTables.SelectedItem.ToString());
        }

        // 按下 Execute
        private void btnQuery_Click(object sender, EventArgs e)
        {
            string table = comboTables.SelectedItem.ToString();
            string id = txtQuery.Text.Trim();

            DateTime startDate = dtpStart.Value.Date;
            DateTime endDate = dtpEnd.Value.Date;

            int sh = int.Parse(cbStartHour.SelectedItem.ToString());
            int sm = int.Parse(cbStartMinute.SelectedItem.ToString());
            int ss = int.Parse(cbStartSecond.SelectedItem.ToString());

            int eh = int.Parse(cbEndHour.SelectedItem.ToString());
            int em = int.Parse(cbEndMinute.SelectedItem.ToString());
            int es = int.Parse(cbEndSecond.SelectedItem.ToString());

            DateTime startTime = startDate.AddHours(sh).AddMinutes(sm).AddSeconds(ss);
            DateTime endTime = endDate.AddHours(eh).AddMinutes(em).AddSeconds(es);

            if (startTime > endTime)
            {
                MessageBox.Show("開始時間不能晚於結束時間！");
                return;
            }

            // 確保時間區間正確
            if (startTime > endTime)
            {
                MessageBox.Show("開始時間不能晚於結束時間！");
                return;
            }

            try
            {
                using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    conn.Open();

                    // SQL 基本語句
                    string sql = $"SELECT * FROM {table} WHERE ExportTime BETWEEN @start AND @end";

                    // 如果有輸入 ID，再加上 ID 條件
                    if (!string.IsNullOrEmpty(id))
                    {
                        sql += " AND ID = @id";
                    }

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@start", startTime));
                        cmd.Parameters.Add(new SQLiteParameter("@end", endTime));

                        if (!string.IsNullOrEmpty(id))
                            cmd.Parameters.Add(new SQLiteParameter("@id", id));

                        using (var adapter = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvResults.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查詢錯誤：" + ex.Message);
            }
        }


        // 載入資料
        private void LoadData(string sqlOrTable)
        {
            try
            {
                using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    conn.Open();

                    string sql = sqlOrTable.Trim();

                    // 如果使用者只選表格，則改寫為 SELECT *
                    if (!sql.ToUpper().StartsWith("SELECT"))
                    {
                        sql = $"SELECT * FROM {sqlOrTable} LIMIT 100;";
                    }

                    using (var cmd = new SQLiteCommand(sql, conn))
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvResults.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查詢錯誤：" + ex.Message);
            }
        }
    }
}
