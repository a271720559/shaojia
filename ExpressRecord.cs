using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OrderManage
{
    public partial class ExpressRecord : Form
    {
        SqlConn conn = new SqlConn();
        public List<ResultEntry> resultList = new List<ResultEntry>();
        public ExpressRecord()
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
            List<string> status = new List<string>();
            status.Add("");
            status.Add("已揽收");
            status.Add("已签收");
            status.Add("揽收失败");
            status.Add("签收失败");
            this.cmbExpressStatus.DataSource = status;
            foreach (var control in this.Controls)
            {
                if (control is Label)
                {
                    var lb = control as Label;
                    lb.BackColor = Color.Transparent;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string command = string.Empty;
            resultList = new List<ResultEntry>();
            command = "select side,shop,\"order\",buyID,buyDate,sendDate,company,expressNo,province,shopName,status,importDate from ExpressRecord where 1=1 and status!=0 ";
            if (this.timePickFrom.Value != null)
            {
                var timeFrom = this.timePickFrom.Value;
                timeFrom = timeFrom.AddHours(-this.timePickFrom.Value.Hour).AddMinutes(-this.timePickFrom.Value.Minute).AddSeconds(-this.timePickFrom.Value.Second);

                command += " and convert(datetime,importDate) >='" + timeFrom.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            if (this.timePickTo.Value != null)
            {
                var timeTo = this.timePickTo.Value;
                timeTo = timeTo.AddHours(-this.timePickTo.Value.Hour).AddMinutes(-this.timePickTo.Value.Minute).AddSeconds(-this.timePickTo.Value.Second);
                timeTo = timeTo.AddHours(23).AddMinutes(59).AddSeconds(59);
                command += " and convert(datetime,importDate) <='" + timeTo.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            switch(this.cmbExpressStatus.Text)
            {
                case "已揽收":
                    command += " and status=1";
                    break;
                case "已签收":
                    command += " and status=4";
                    break;
                case "揽收失败":
                    command += " and status=5";
                    break;
                case "签收失败":
                    command += " and status=6";
                    break;
            }
            DataTable dt = conn.GetDataTableBySql(command);
            if (dt.Rows.Count > 0)
            {
                int seqNum = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ResultEntry entry = new ResultEntry();
                    entry.seqNum = seqNum;
                    entry.side = dt.Rows[i][0].ToString();
                    entry.shop = dt.Rows[i][1].ToString();
                    entry.order = dt.Rows[i][2].ToString();
                    entry.buyID = dt.Rows[i][3].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[i][4].ToString()))
                        entry.buyDate = DateTime.Parse(dt.Rows[i][4].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[i][5].ToString()))
                        entry.sendDate = DateTime.Parse(dt.Rows[i][5].ToString());
                    entry.company = dt.Rows[i][6].ToString();
                    entry.expressNo = dt.Rows[i][7].ToString();
                    entry.province = dt.Rows[i][8].ToString();
                    entry.shopName = dt.Rows[i][9].ToString();
                    entry.status = int.Parse(dt.Rows[i][10].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[i][11].ToString()))
                        entry.importDate = DateTime.Parse(dt.Rows[i][11].ToString());
                    switch (entry.status)
                    {
                        case 1:
                            entry.statusDesc = "已揽收";
                            break;
                        case 4:
                            entry.statusDesc = "已签收";
                            break;
                        case 5:
                            entry.statusDesc = "揽收查询失败";
                            break;
                        case 6:
                            entry.statusDesc = "签收查询失败";
                            break;
                    }
                    resultList.Add(entry);
                    seqNum++;
                }
               
            }
            this.dataGridView1.DataSource = null;
            this.dataGridView1.DataSource = resultList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string saveFileName = "";
                //bool fileSaved = false;  
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xls";
                saveDialog.Filter = "Excel文件|*.xls";
                //saveDialog.FileName = fileName;  
                saveDialog.ShowDialog();
                saveFileName = saveDialog.FileName;
                if (saveFileName.IndexOf(":") < 0) return; //被点了取消   
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                    return;
                }

                Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1  

                //写入标题  
                for (int i = 0; i < this.dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                //写入数值  
                for (int r = 0; r < dataGridView1.Rows.Count; r++)
                {
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        worksheet.Cells[r + 2, i + 1] = dataGridView1.Rows[r].Cells[i].Value;
                    }
                    System.Windows.Forms.Application.DoEvents();
                }
                worksheet.Columns.EntireColumn.AutoFit();//列宽自适应  
                //if (Microsoft.Office.Interop.cmbxType.Text != "Notification")  
                //{  
                //    Excel.Range rg = worksheet.get_Range(worksheet.Cells[2, 2], worksheet.Cells[ds.Tables[0].Rows.Count + 1, 2]);  
                //    rg.NumberFormat = "00000000";  
                //}  

                if (saveFileName != "")
                {
                    try
                    {
                        workbook.Saved = true;
                        workbook.SaveCopyAs(saveFileName);
                        //fileSaved = true;  
                    }
                    catch (Exception ex)
                    {
                        //fileSaved = false;  
                        MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                    }

                }
                //else  
                //{  
                //    fileSaved = false;  
                //}  
                xlApp.Quit();
                GC.Collect();//强行销毁   

                //if (System.IO.File.Exists(saveFileName)) System.Diagnostics.Process.Start(saveFileName); //打开EXCEL  
                //MessageBox.Show(fileName + "的简明资料保存成功", "提示", MessageBoxButtons.OK);  
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出有问题:" + ex.Message, "提示", MessageBoxButtons.OK);
            }  
        }
    }
    public class ResultEntry
    {
        public int id { get; set; }
        public int seqNum { get; set; }
        public string side { get; set; }
        public string shop { get; set; }
        public string order { get; set; }
        public string buyID { get; set; }
        public DateTime buyDate { get; set; }
        public DateTime sendDate { get; set; }
        public DateTime importDate { get; set; }
        public string company { get; set; }
        public string expressNo { get; set; }
        public string province { get; set; }
        public string shopName { get; set; }
        public int status { get; set; }//5揽收查询失败   1揽收     2签收  6 签收查询失败 4 已签收
        public string statusDesc { get; set; }
    }
}
