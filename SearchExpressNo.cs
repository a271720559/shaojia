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
    public partial class SearchExpressNo : Form
    {
        SqlConn conn = new SqlConn();
        KdApiOrderDistinguish ApiDisting = new KdApiOrderDistinguish();
        KdApiSearchDemo ApiSearch = new KdApiSearchDemo();
        List<ExpressEntry> hasExpress = new List<ExpressEntry>();
        List<ExpressEntry> noHasExpress = new List<ExpressEntry>();
        List<ExpressEntry> failExpress = new List<ExpressEntry>();
        List<string> allExpressNo = new List<string>();
        string BusinessID = string.Empty;
        string AppKey = string.Empty;
        int hasSeq = 1;
        int noHasSeqNo = 1;
        int failSeqNo = 1;
        public SearchExpressNo()
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView3.AutoGenerateColumns = false;
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "日志文件 (*.xls)|*.xls;*.xlsx";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string strCon = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + openFileDialog.FileName + ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'";
                System.Data.OleDb.OleDbConnection myConn = new System.Data.OleDb.OleDbConnection(strCon);
                string strCom = " SELECT * FROM [Sheet1$] ";
                System.Data.OleDb.OleDbDataAdapter myCommand = new System.Data.OleDb.OleDbDataAdapter(strCom, myConn);
                System.Data.DataTable dt = new System.Data.DataTable();
                myCommand.Fill(dt);
                int seqNum = 1;
                allExpressNo = new List<string>();
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    allExpressNo.Add(dt.Rows[i][3].ToString());
                }

                if (allExpressNo.Count() > 0)
                {
                    int allApplyCount = 2900;
                    int take = 0;
                    string command = string.Empty;
                    int needApplyCount = allExpressNo.Count();
                    List<string> applyExpress = new List<string>();
                    List<int> searchSeq = new List<int>();
                    hasExpress = new List<ExpressEntry>();
                    noHasExpress = new List<ExpressEntry>();
                    failExpress = new List<ExpressEntry>();
                    hasSeq = 1;
                    noHasSeqNo = 1;
                    failSeqNo = 1;
                    while (needApplyCount > 0)
                    {
                        Random rd = new Random();
                        int seq = rd.Next(1, 6);
                        while (searchSeq.Where(v => v == seq).Count() > 0)
                        {
                            seq = rd.Next(1, 6);
                        }
                        
                        //查询使用的帐号
                        command = "select * from Accounts where seqNum=" + seq + "";
                        dt = conn.GetDataTableBySql(command);
                        if (dt.Rows.Count > 0)
                        {
                            var busID = dt.Rows[0]["businessID"].ToString();
                            var appKey = dt.Rows[0]["appkey"].ToString();
                            int todayApplyCount = 0;
                            if (!string.IsNullOrWhiteSpace(busID))
                            {
                                //查询该帐号当天申请次数
                                command = "select * from ApplyRecord where businessID=" + busID + " and Datename(year,applDate)+'-'+Datename(month,applDate)+'-'+Datename(day,applDate)='" + DateTime.Today.ToString("yyyy-MM-dd") + "'";
                                int ?id = null;
                                dt = conn.GetDataTableBySql(command);
                                if (dt.Rows.Count > 0)
                                {
                                    todayApplyCount = int.Parse(dt.Rows[0]["applyCount"].ToString());
                                    id = int.Parse(dt.Rows[0]["id"].ToString());
                                }
                                //剩余查询总数量
                                int surplusCount = allApplyCount - todayApplyCount;
                                if (surplusCount > 0)
                                {
                                    //当前需要查询且可查询的数量
                                    int vail = surplusCount - needApplyCount;
                                    //查询数量
                                    int thisTimeCount = 0;
                                    if (vail < 0)
                                    {
                                        thisTimeCount = surplusCount;
                                        //当天已经用满的帐号不再使用
                                        searchSeq.Add(seq);
                                    }
                                    else
                                        thisTimeCount = needApplyCount;
                                    //跳过已查询的的
                                    applyExpress = allExpressNo.Skip(take).Take(thisTimeCount).ToList();
                                    //记录已查询数
                                    take = thisTimeCount;
                                    //减去已查询的数
                                    needApplyCount = needApplyCount - take;

                                    int needUpdateCount = todayApplyCount + take;
                                    //更新数据库已查询的数
                                    if (id.HasValue)
                                    {
                                        command = "update ApplyRecord set applyCount=" + needUpdateCount + " where id=" + id + "";
                                        conn.ExecuteSql(command);
                                    }
                                    else
                                    {
                                        command = "insert into ApplyRecord(businessID,applyCount) values('" + busID + "'," + needUpdateCount + ")";
                                        conn.ExecuteSql(command);
                                    }
                                    this.BusinessID = busID;
                                    this.AppKey = appKey;
                                    DealExpressNo(applyExpress);
                                }
                            }
                            
                        }
                    }
                    this.dataGridView1.DataSource = null;
                    this.dataGridView2.DataSource = null;
                    this.dataGridView3.DataSource = null;
                    this.dataGridView1.DataSource = this.hasExpress;
                    this.dataGridView2.DataSource = this.noHasExpress;
                    this.dataGridView3.DataSource = this.failExpress;
                }
            }
        }
        public void DealExpressNo(List<string> expressNoList)
        {
            //int seq = 1;
            //int seqNo = 1;
            //int seqFail = 1;
            
            foreach (var expressNo in expressNoList)
            {
                if (!string.IsNullOrWhiteSpace(expressNo))
                {
                    string code = string.Empty;
                    var disting = ApiDisting.orderTracesSubByJson(expressNo,this.BusinessID,this.AppKey);
                    var distingList = disting.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                    var shipperCode = distingList.Where(v => !string.IsNullOrWhiteSpace(v) && v.Contains("ShipperCode")).FirstOrDefault();
                    if(!string.IsNullOrWhiteSpace(shipperCode))
                        code = shipperCode.Replace("ShipperCode", "").Replace("\"", "").Replace(",", "").Replace(":", "").Replace(" ", "");
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        if (code == "YZPY")
                            code = "EMS";
                        var searchExpress = ApiSearch.getOrderTracesByJson(code,expressNo,this.BusinessID,this.AppKey);
                        var searchList = searchExpress.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                        var statue = searchList.Where(v => !string.IsNullOrWhiteSpace(v) && v.Contains("State")).FirstOrDefault();
                        if (!string.IsNullOrWhiteSpace(statue))
                        {
                            var status = statue.Replace("\"", "").Replace("State", "").Replace(":", "").Replace(",", "").Replace(" ", "");
                            if (status == "0")
                            {
                                ExpressEntry entry = new ExpressEntry();
                                entry.seqNum = hasSeq;
                                entry.expressNo = expressNo;
                                noHasExpress.Add(entry);
                                hasSeq++;
                            }
                            else
                            {
                                ExpressEntry entry = new ExpressEntry();
                                entry.seqNum = noHasSeqNo;
                                entry.expressNo = expressNo;
                                hasExpress.Add(entry);
                                noHasSeqNo++;
                            }
                        }
                        else
                        {
                            //查询失败
                            ExpressEntry entry = new ExpressEntry();
                            entry.seqNum = failSeqNo;
                            entry.expressNo = expressNo;
                            failExpress.Add(entry);
                            failSeqNo++;
                        }
                        //foreach(var search in searchList)
                        //{
                        //    if (!string.IsNullOrWhiteSpace(search) && search.Contains("State"))
                        //    {
                        //        var status = search.Replace("\"", "").Replace("State", "").Replace(":", "").Replace(",","").Replace(" ","");
                        //        if (status == "0")
                        //        {
                        //            ExpressEntry entry = new ExpressEntry();
                        //            entry.seqNum = seq;
                        //            entry.expressNo = expressNo;
                        //            noHasExpress.Add(entry);
                        //            seq++;
                        //        }
                        //        else
                        //        {
                        //            ExpressEntry entry = new ExpressEntry();
                        //            entry.seqNum = seqNo;
                        //            entry.expressNo = expressNo;
                        //            hasExpress.Add(entry);
                        //            seqNo++;
                        //        }
                        //        break;
                        //    }
                        //}
                    }
                }
            }
           
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
                worksheet.Columns.EntireColumn.NumberFormat = "@";
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

        private void button3_Click(object sender, EventArgs e)
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
                for (int i = 0; i < this.dataGridView2.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView2.Columns[i].HeaderText;
                }
                //写入数值  
                for (int r = 0; r < dataGridView2.Rows.Count; r++)
                {
                    for (int i = 0; i < dataGridView2.ColumnCount; i++)
                    {
                        worksheet.Cells[r + 2, i + 1] = dataGridView2.Rows[r].Cells[i].Value;
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

        private void button4_Click(object sender, EventArgs e)
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
                for (int i = 0; i < this.dataGridView3.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView3.Columns[i].HeaderText;
                }
                //写入数值  
                for (int r = 0; r < dataGridView3.Rows.Count; r++)
                {
                    for (int i = 0; i < dataGridView3.ColumnCount; i++)
                    {
                        worksheet.Cells[r + 2, i + 1] = dataGridView3.Rows[r].Cells[i].Value;
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

        private void button5_Click(object sender, EventArgs e)
        {
            ExpressRecord record = new ExpressRecord();
            record.Show();
        }
    }
    public class ExpressEntry
    {
        public int seqNum { get; set; }
        public string expressNo { get; set; }
    }
}
