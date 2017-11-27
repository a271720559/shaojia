using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExpressManage
{
    public partial class Main : Form
    {
        public List<ResultEntry> resultList = new List<ResultEntry>();
        public List<ResultEntry> showList = new List<ResultEntry>();
        SqlConn conn = new SqlConn();
        string BusinessID = string.Empty;
        string AppKey = string.Empty;
        KdApiOrderDistinguish ApiDisting = new KdApiOrderDistinguish();
        KdApiSearchDemo ApiSearch = new KdApiSearchDemo();
        int isDealCount = 0;
        public Main()
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            province pro = new province();
            pro.Show();
        }

        private void btnImport_Click(object sender, EventArgs e)
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
                resultList = new List<ResultEntry>();
                showList = new List<ResultEntry>();
                for (int i = 1; i < dt.Rows.Count; i++)
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
                    resultList.Add(entry);
                    seqNum++;
                }
                if (resultList.Count() > 0)
                {
                    isDealCount = 0;
                    //处理快递单
                    DealExpress(resultList);
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.DataSource = this.showList;
                    this.tip.Visible = false;
                }
            }
        }

        private void DealExpress(List<ResultEntry> result)
        {
            List<ResultEntry> needSearchInTimeList = new List<ResultEntry>();//需要马上查询的快递单
            List<ResultEntry> UnNeedSearchInTimeList = new List<ResultEntry>();//延时查询
            foreach(var item in result)
            {
                if (!string.IsNullOrWhiteSpace(item.side) && item.side == "拼多多")
                {
                    needSearchInTimeList.Add(item);
                }
                else
                    UnNeedSearchInTimeList.Add(item);
            }
            //处理拼多多
            if (needSearchInTimeList.Count() > 0)
            {
                int needApplyCount = needSearchInTimeList.Count();
                List<int> searchSeq = new List<int>();//查询过的序号
                int allApplyCount = 2950;//日查询量
                int take = 0;//跳过的数
                string command = string.Empty;
                while (needApplyCount > 0)
                {
                    Random rd = new Random();
                    int seq = rd.Next(1, 11);
                    while (searchSeq.Where(v => v == seq).Count() > 0)
                    {
                        seq = rd.Next(1, 11);
                    }
                    //查询使用的帐号
                    command = "select * from Accounts where seqNum=" + seq + "";
                    DataTable dt = conn.GetDataTableBySql(command);
                    if (dt.Rows.Count > 0)
                    {
                        var busID = dt.Rows[0]["businessID"].ToString();
                        var appKey = dt.Rows[0]["appkey"].ToString();
                        int todayApplyCount = 0;
                        if (!string.IsNullOrWhiteSpace(busID))
                        {
                            //查询该帐号当天申请次数
                            command = "select * from ApplyRecord where businessID=" + busID + " and Datename(year,applDate)+'-'+Datename(month,applDate)+'-'+Datename(day,applDate)='" + DateTime.Today.ToString("yyyy-MM-dd") + "'";
                            int? id = null;
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
                                //跳过已查询的
                                needSearchInTimeList = needSearchInTimeList.Skip(take).Take(thisTimeCount).ToList();
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
                                DealExpressNo(needSearchInTimeList);
                            }
                        }

                    }
                    
                }
            }
            //处理天猫站点
            if (UnNeedSearchInTimeList.Count() > 0)
            {


                string command = string.Empty;
                foreach(var item in UnNeedSearchInTimeList)
                {
                    command = "insert into ExpressRecord(side,shop,\"order\",buyID,buyDate,sendDate,company,expressNo,province,status,importDate,shopName) values ('" + item.side + "','" + item.shop + "','" + item.order + "','" + item.buyID + "','" + item.buyDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + item.sendDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + item.company + "','" + item.expressNo + "','" + item.province + "',0,'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','"+item.shopName+"')";
                    conn.ExecuteSql(command);
                }
            }
        }
        public void DealExpressNo(List<ResultEntry> expressNoList)
        {
            foreach (var item in expressNoList)
            {
                int status = this.QueryExpress(item.expressNo);
                if (status == 0)//无物流信息
                {
                    showList.Add(item);
                }
                //if (!string.IsNullOrWhiteSpace(item.expressNo))
                //{
                //    string code = string.Empty;
                //    switch(item.expressNo.Substring(0,1))
                //    {
                //        case "7":
                //            code = "HTKY";
                //            break;
                //        case "8":
                //            code = "ANE";
                //            break;
                //        case "9":
                //            code = "YZPY";
                //            break;
                //    }
                //    if (!string.IsNullOrWhiteSpace(code))
                //    {
                //        var searchExpress = ApiSearch.getOrderTracesByJson(code, item.expressNo, this.BusinessID, this.AppKey);
                //        var searchList = searchExpress.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                //        var statue = searchList.Where(v => !string.IsNullOrWhiteSpace(v) && v.Contains("State")).FirstOrDefault();
                //        if (statue != null)
                //        {
                //            var status = statue.Replace("\"", "").Replace("State", "").Replace(":", "").Replace(",", "").Replace(" ", "");
                //            if (status == "0")//无物流信息
                //            {
                //                showList.Add(item);
                //            }
                //        }
                //        else
                //            showList.Add(item);
                //    }
                //}
                isDealCount++;
                this.tip.Visible = true;
                this.tip.Text = "需处理的快递总数:" + this.resultList.Count + Environment.NewLine +
                                "已处理数:" + isDealCount + Environment.NewLine +
                                "剩余数量:" +(this.resultList.Count - isDealCount);
                Application.DoEvents();
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
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

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)  //判断是否最小化
            {
                this.ShowInTaskbar = false;  //不显示在系统任务栏
                notifyIcon1.Visible = true;  //托盘图标可见
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = true;  //显示在系统任务栏
                this.WindowState = FormWindowState.Normal; //还原窗体
                notifyIcon1.Visible = false;  //托盘图标隐藏
            }
        }

        private void btnSerive_Click(object sender, EventArgs e)
        {
            if (this.btnSerive.Text == "开启服务")
            {
                this.timer1.Start();
                this.btnSerive.Text = "关闭服务";
            }
            else
            {
                this.timer1.Stop();
                this.btnSerive.Text = "开启服务";
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.btnSerive.Text == "关闭服务")
            {
                List<ResultEntry> resultList = new List<ResultEntry>();
                string command = string.Empty;
                command = "select * from ExpressRecord where status=0 or status=1";
                DataTable dt = conn.GetDataTableBySql(command);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ResultEntry entry = new ResultEntry();
                        entry.id = int.Parse(dt.Rows[i]["id"].ToString());
                        entry.status = int.Parse(dt.Rows[i]["status"].ToString());
                        entry.expressNo = dt.Rows[i]["expressNo"].ToString();
                        entry.importDate = DateTime.Parse(dt.Rows[i]["importDate"].ToString());
                        resultList.Add(entry);
                    }
                }
                if (resultList.Count() > 0)
                {
                    command = string.Empty;
                    foreach (var item in resultList)
                    {
                        //1.当站点为拼多多时   导入进去后，马上进行查询是否有揽收记录。如果没有需要导出详细信息
                        //2.当站点为淘宝天猫时 导入进去后，发货日期超过24小时后，开始查询是否有揽收记录，如果没有 需要导出详细信息
                        //3.当站点为淘宝天猫时  发货日期超过预设的时间时（按照省份、快递预设一个时间），开始查询是否签收。如果没有，需要导出详细信息。
                        //处理揽收快递
                        if (item.status == 0)
                        {
                            var timespan = DateTime.Now - item.importDate;
                            if (timespan.TotalHours > 24)//发货日期超过24小时后
                            {
                                //物流状态：2-在途中,3-签收,4-问题件
                                int queryInt = this.QueryExpress(item.expressNo);
                                if (queryInt == 0)
                                {
                                    command = "update ExpressRecord set status=5 where id=" + item.id + "";
                                    conn.ExecuteSql(command);
                                }
                                else if (queryInt == 2 || queryInt == 3 || queryInt == 4)
                                {
                                    command = "update ExpressRecord set status=1 where id=" + item.id + "";
                                    conn.ExecuteSql(command);
                                }
                            }
                        }
                        //处理签收快递
                        else if (item.status == 1)
                        {
                            //查询省份的签收天数
                            command = "select days from ProvinceSet where province='" + item.province + "'";
                            dt = conn.GetDataTableBySql(command);
                            int days = 0;
                            if (dt.Rows.Count > 0)
                            {
                                days = int.Parse(dt.Rows[0][0].ToString());
                            }
                            int hours = 24 * days;
                            var timespan = DateTime.Now - item.importDate;
                            //发货日期超过预设的时间时
                            if (timespan.TotalHours >= hours)
                            {
                                //物流状态：2-在途中,3-签收,4-问题件
                                int queryInt = this.QueryExpress(item.expressNo);
                                if (queryInt == 3)
                                {
                                    command = "update ExpressRecord set status=4 where id=" + item.id + "";
                                    conn.ExecuteSql(command);
                                }
                                else
                                {
                                    command = "update ExpressRecord set status=6 where id=" + item.id + "";
                                    conn.ExecuteSql(command);
                                }
                            }
                        }
                    }
                }
            }
        }

        public int QueryExpress(string expressNo)
        {
            //物流状态：2-在途中,3-签收,4-问题件
            if (string.IsNullOrWhiteSpace(expressNo))
                return 0;
            string code = string.Empty;
            switch (expressNo.Substring(0, 1))
            {
                case "7":
                    code = "HTKY";
                    break;
                case "8":
                    code = "ANE";
                    break;
                case "9":
                    code = "YZPY";
                    break;
            }
            if (!string.IsNullOrWhiteSpace(code))
            {
                List<int> searchSeq = new List<int>();//查询过的序号
                Random rd = new Random();
                string command = string.Empty;
                while (true)
                {
                    int seq = rd.Next(1, 11);
                    while (searchSeq.Where(v => v == seq).Count() > 0)
                    {
                        seq = rd.Next(1, 11);
                    }
                    //查询使用的帐号
                    command = "select * from Accounts where seqNum=" + seq + "";
                    DataTable dt = conn.GetDataTableBySql(command);
                    if (dt.Rows.Count > 0)
                    {
                        var busID = dt.Rows[0]["businessID"].ToString();
                        var appKey = dt.Rows[0]["appkey"].ToString();
                        int todayApplyCount = 0;
                        if (!string.IsNullOrWhiteSpace(busID))
                        {
                            //查询该帐号当天申请次数
                            command = "select * from ApplyRecord where businessID=" + busID + " and Datename(year,applDate)+'-'+Datename(month,applDate)+'-'+Datename(day,applDate)='" + DateTime.Today.ToString("yyyy-MM-dd") + "'";
                            int? id = null;
                            dt = conn.GetDataTableBySql(command);
                            if (dt.Rows.Count > 0)
                            {
                                todayApplyCount = int.Parse(dt.Rows[0]["applyCount"].ToString());
                                id = int.Parse(dt.Rows[0]["id"].ToString());
                            }
                            //剩余查询总数量
                            int surplusCount = 2950 - todayApplyCount;
                            if (surplusCount > 0)
                            {
                                todayApplyCount++;
                                //更新数据库已查询的数
                                if (id.HasValue)
                                {
                                    command = "update ApplyRecord set applyCount=" + todayApplyCount + " where id=" + id + "";
                                    conn.ExecuteSql(command);
                                }
                                else
                                {
                                    command = "insert into ApplyRecord(businessID,applyCount) values('" + busID + "'," + todayApplyCount + ")";
                                    conn.ExecuteSql(command);
                                }
                                this.BusinessID = busID;
                                this.AppKey = appKey;
                                break;
                            }
                        }
                    }
                }
                var searchExpress = ApiSearch.getOrderTracesByJson(code, expressNo, this.BusinessID, this.AppKey);
                var searchList = searchExpress.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
                var statue = searchList.Where(v => !string.IsNullOrWhiteSpace(v) && v.Contains("State")).FirstOrDefault();
                if (statue != null)
                {
                    var status = statue.Replace("\"", "").Replace("State", "").Replace(":", "").Replace(",", "").Replace(" ", "");
                    return int.Parse(status);
                }
                else
                    return 0;
            }
            else
                return 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KdApiSubscribeDemo api = new KdApiSubscribeDemo();
            api.orderTracesSubByJson();
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
    }
}
