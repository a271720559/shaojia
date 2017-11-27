using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OrderManage.ExpressReceiveManage;

namespace OrderManage
{
    public partial class ExpressReceive : Form
    {
        private List<ExpressReceiveEntry> ResultList = new List<ExpressReceiveEntry>();
        private SqlConn conn = new SqlConn();
        List<string> DealAuthorizationUserList = new List<string>();//处理权限授权人员
        List<string> FinishAuthorizationUserList = new List<string>();//完结权限授权人员
        public ExpressReceive()
        {
            InitializeComponent();
            foreach (var control in this.Controls)
            {
                if (control is Label)
                {
                    var lb = control as Label;
                    lb.BackColor = Color.Transparent;
                }
            }
            List<string> StatusList = new List<string>() { "", "未处理", "已处理", "已超时", "已完结","紧急" };
            this.cmbStatus.DataSource = StatusList;
            List<string> TimeList = new List<string>() { "录入时间", "处理时间","完结时间" };
            this.cmbTime.DataSource = TimeList;
            List<string> ResultList = new List<string>() { "", "已退款", "已补发" };
            this.cmbResult.DataSource = ResultList;
            this.dgShow.AutoGenerateColumns = false;
            RoadResion();
            init();
            this.dgShow.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
        }

        private void init()
        {
            DealAuthorizationUserList = new List<string>();
            FinishAuthorizationUserList = new List<string>();
            string command = string.Empty;
            //查询处理授权人员
            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='ExpressDeal'";
            DataTable dt = conn.GetDataTableBySql(command);
            var InputUser = dt.Rows[0][0].ToString().Split(',').ToList();
            foreach (var item in InputUser)
            {
                DealAuthorizationUserList.Add(item);
            }
            //查询完结授权人员
            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='ExpressFinish'";
            dt = conn.GetDataTableBySql(command);
            var DealUser = dt.Rows[0][0].ToString().Split(',').ToList();
            foreach (var item in DealUser)
            {
                FinishAuthorizationUserList.Add(item);
            }

            if (!string.IsNullOrWhiteSpace(UserEntry.Name))
            {
                if (DealAuthorizationUserList.Count() > 0 && DealAuthorizationUserList.Where(v => v == UserEntry.Name).Count() > 0)
                {
                    //this.btnInput.Visible = true;
                    this.btnDeal.Enabled = true;
                }
                else
                    this.btnDeal.Enabled = false;
                if (FinishAuthorizationUserList.Count() > 0 && FinishAuthorizationUserList.Where(v => v == UserEntry.Name).Count() > 0)
                {
                    //this.btnInput.Visible = true;
                    this.btnFinish.Enabled = true;
                }
                else
                    this.btnFinish.Enabled = false;
                if (UserEntry.Name == "管理员")
                    this.btnAu.Enabled = true;
                else
                    this.btnAu.Enabled = false;
            }
        }

        private void RoadResion()
        {
            List<string> ResionList = new List<string>();
            ResionList.Add("");
            string command = string.Empty;
            command = "select * from ResionSet ";
            DataTable dt = conn.GetDataTableBySql(command);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ResionList.Add(dt.Rows[i]["Resion"].ToString());
                }
            }
            this.cmbResion.DataSource = null;
            this.cmbResion.DataSource = ResionList;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            ResultList = new List<ExpressReceiveEntry>();
            string command = string.Empty;
            command = "select * from ExpressReceive where IsDelete =0 ";
            if (!string.IsNullOrWhiteSpace(this.txtExpressNo.Text))
            {
                command += " and ExpressNo like '%" + this.txtExpressNo.Text + "%'";
            }
            if (!string.IsNullOrWhiteSpace(this.txtCompanyName.Text))
            {
                command += " and ExpressCompany like '%" + this.txtCompanyName.Text + "%'";
            }
            if (!string.IsNullOrWhiteSpace(this.cmbResion.Text))
            {
                command += " and Resion like '%" + this.cmbResion.Text + "%'";
            }
            if (!string.IsNullOrWhiteSpace(this.cmbStatus.Text))
            {
                command += " and Status like '%" + this.cmbStatus.Text + "%'";
            }
            if(!string.IsNullOrWhiteSpace(this.cmbResult.Text))
            {
                command += " and Result like '%" + this.cmbResult.Text + "%'";
            }
            if(!string.IsNullOrWhiteSpace(this.txtCreateUser.Text))
            {
                command += " and CreateUser like '%"+this.txtCreateUser.Text+"%'";
            }
            if (this.timePickFrom.Value != null)
            {
                var timeFrom = this.timePickFrom.Value;
                timeFrom = timeFrom.AddHours(-this.timePickFrom.Value.Hour).AddMinutes(-this.timePickFrom.Value.Minute).AddSeconds(-this.timePickFrom.Value.Second);
                //DealTime //FinishTime
                if (this.cmbTime.Text == "录入时间")
                    command += " and convert(datetime,CreateTime) >='" + timeFrom.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                else if (this.cmbTime.Text == "处理时间")
                    command += " and convert(datetime,DealTime) >='" + timeFrom.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                else if(this.cmbTime.Text == "完结时间")
                    command += " and convert(datetime,FinishTime) >='" + timeFrom.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            if (this.timePickTo.Value != null)
            {
                var timeTo = this.timePickTo.Value;
                timeTo = timeTo.AddHours(-this.timePickTo.Value.Hour).AddMinutes(-this.timePickTo.Value.Minute).AddSeconds(-this.timePickTo.Value.Second);
                timeTo = timeTo.AddHours(23).AddMinutes(59).AddSeconds(59);

                if (this.cmbTime.Text == "录入时间")
                    command += " and convert(datetime,CreateTime) <='" + timeTo.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                else if (this.cmbTime.Text == "处理时间")
                    command += " and convert(datetime,DealTime) <='" + timeTo.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                else if (this.cmbTime.Text == "完结时间")
                    command += " and convert(datetime,FinishTime) <='" + timeTo.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                //command += " and convert(datetime,CreateTime) <='" + timeTo.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            DataTable dt = conn.GetDataTableBySql(command);
            if (dt.Rows.Count > 0)
            {
                int seq = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ExpressReceiveEntry entry = new ExpressReceiveEntry();
                    entry.SeqNum = seq;
                    entry.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    entry.Status = dt.Rows[i]["Status"].ToString();
                    entry.ExpressNo = dt.Rows[i]["ExpressNo"].ToString();
                    entry.ExpressCompany = dt.Rows[i]["ExpressCompany"].ToString();
                    entry.BuyAdress = dt.Rows[i]["BuyAdress"].ToString();
                    entry.Resion = dt.Rows[i]["Resion"].ToString();
                    entry.CreateUser = dt.Rows[i]["CreateUser"].ToString();
                    entry.DealUser = dt.Rows[i]["DealUser"].ToString();
                    entry.Remark = dt.Rows[i]["Remark"].ToString();
                    entry.Remark2 = dt.Rows[i]["Remark2"].ToString();
                    entry.Remark3 = dt.Rows[i]["Remark3"].ToString();
                    entry.Remark4 = dt.Rows[i]["Remark4"].ToString();
                    entry.Result = dt.Rows[i]["Result"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[i]["DealTime"].ToString()))
                        entry.DealTime = DateTime.Parse(dt.Rows[i]["DealTime"].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[i]["CreateTime"].ToString()))
                        entry.CreateTime = DateTime.Parse(dt.Rows[i]["CreateTime"].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[i]["FinishTime"].ToString()))
                        entry.FinishTime = DateTime.Parse(dt.Rows[i]["FinishTime"].ToString());
                    seq++;
                    ResultList.Add(entry);
                }
                //处理已超时
                command = "select * from ResionSet";
                dt = conn.GetDataTableBySql(command);
                if (dt.Rows.Count > 0)
                {
                    List<ResionEntry> resionList = new List<ResionEntry>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ResionEntry entry = new ResionEntry();
                        entry.Days = int.Parse(dt.Rows[i]["Days"].ToString());
                        entry.Resion = dt.Rows[i]["Resion"].ToString();
                        resionList.Add(entry);
                    }
                    foreach (var item in ResultList.Where(v => v.Status == "已处理" || v.Status == "未处理").ToList())
                    {
                        var resionEntry = resionList.Where(v => v.Resion == item.Resion).FirstOrDefault();
                        if (resionEntry != null)
                        {
                            var time = resionEntry.Days * 24;
                            TimeSpan? totalTime = DateTime.Now-item.CreateTime;
                            if (totalTime.HasValue && totalTime.Value.TotalHours > time)
                            {
                                command = " update ExpressReceive set Status='已超时' where id='" + item.ID + "'";
                                string message = conn.ExecuteSql(command);
                                item.Status = "已超时";
                                //if (!string.IsNullOrWhiteSpace(message))
                                //    MessageBox.Show("序号:" + item.SeqNum + " 处理失败" + "失败原因:" + message);
                                
                            }
                        }
                    }

                }
            }
            this.dgShow.DataSource = null;
            this.dgShow.DataSource = ResultList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExpressCompanySet company = new ExpressCompanySet();
            company.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResionInput resion = new ResionInput();
            resion.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ExpressInput input = new ExpressInput();
            input.Show();
        }

        private void btnDeal_Click(object sender, EventArgs e)
        {
            Deal();
        }

        private void Deal()
        {
            if (MessageBox.Show("        确认处理?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var selectRow in this.ResultList.Where(v => v.IsSelect == true).ToList())
                {
                //foreach (var selectRow in this.dgShow.SelectedRows)
                //{
                    //var gridRow = selectRow as DataGridViewRow;
                    //var row = gridRow.DataBoundItem as ExpressReceiveEntry;
                    var row = selectRow;
                    string command = string.Empty;
                    if (row != null && row.ID.HasValue && row.Status == "未处理" )//&& row == UserEntry.Name)
                    {
                        command = " update ExpressReceive set Status='已处理',DealUser='"+UserEntry.Name+"',DealTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"' where id='" + row.ID + "'";
                        string message = conn.ExecuteSql(command);
                        if (!string.IsNullOrWhiteSpace(message))
                            MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                        foreach (var item in ResultList)
                        {
                            if (item.ID == row.ID)
                            {
                                item.Status = "已处理";
                                item.DealUser = UserEntry.Name;
                                item.DealTime = DateTime.Now;
                            }
                        }
                    }
                }
                this.dgShow.DataSource = null;
                this.dgShow.DataSource = ResultList;
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void Finish()
        {
            if (MessageBox.Show("        确认完结?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var selectRow in this.ResultList.Where(v => v.IsSelect == true).ToList())
                {
                    //foreach (var selectRow in this.dgShow.SelectedRows)
                    //{
                    //var gridRow = selectRow as DataGridViewRow;
                    //var row = gridRow.DataBoundItem as ExpressReceiveEntry;
                    var row = selectRow;
                    string command = string.Empty;
                    if (row != null && row.ID.HasValue )//&& row.Status == "已处理" && row.RefundUserName == UserEntry.Name)
                    {
                        command = " update ExpressReceive set Status='已完结',FinishTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id='" + row.ID + "'";
                        string message = conn.ExecuteSql(command);
                        if (!string.IsNullOrWhiteSpace(message))
                            MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                        foreach (var item in ResultList)
                        {
                            if (item.ID == row.ID)
                            {
                                item.Status = "已完结";
                                item.FinishTime = DateTime.Now;
                            }
                        }
                    }
                }
                this.dgShow.DataSource = null;
                this.dgShow.DataSource = ResultList;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Jinji();
        }

        private void Jinji()
        {
            if (MessageBox.Show("        确认设置紧急?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var selectRow in this.ResultList.Where(v => v.IsSelect == true).ToList())
                {
                    //foreach (var selectRow in this.dgShow.SelectedRows)
                    //{
                    //var gridRow = selectRow as DataGridViewRow;
                    //var row = gridRow.DataBoundItem as ExpressReceiveEntry;
                    var row = selectRow;
                    string command = string.Empty;
                    if (row != null && row.ID.HasValue)//&& row.Status == "已处理" && row.RefundUserName == UserEntry.Name)
                    {
                        command = " update ExpressReceive set Status='紧急' where id='" + row.ID + "'";
                        string message = conn.ExecuteSql(command);
                        if (!string.IsNullOrWhiteSpace(message))
                            MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                        foreach (var item in ResultList)
                        {
                            if (item.ID == row.ID)
                            {
                                item.Status = "紧急";
                                item.FinishTime = DateTime.Now;
                            }
                        }
                    }
                }
                this.dgShow.DataSource = null;
                this.dgShow.DataSource = ResultList;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UpdateRemark();
        }
        private void UpdateRemark()
        {
            if (MessageBox.Show("          确认更改回复?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string command = string.Empty;
                //foreach (var selectRow in this.resultList.Where(v => v.IsSelect == true).ToList())
                //{
                foreach (var selectRow in this.ResultList.Where(v => v.IsSelect == true).ToList())
                {
                    //foreach (var selectRow in this.dgShow.SelectedRows)
                    //{
                    //var gridRow = selectRow as DataGridViewRow;
                    //var row = gridRow.DataBoundItem as ExpressReceiveEntry;
                    var row = selectRow;
                    if (row != null && row.ID.HasValue)
                    {
                        command = "update \"ExpressReceive\" set \"Remark\"='" + row.Remark + "',Remark2='"+row.Remark2+"',Remark3='"+row.Remark3+"',Remark4='"+row.Remark4+"' where ID='" + row.ID + "'";
                        string message = conn.ExecuteSql(command);
                        if (!string.IsNullOrWhiteSpace(message))
                            MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                    }
                    //if(row.)
                }
                Search();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.dgShow.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in this.dgShow.SelectedCells)
                {
                    if (cell.FormattedValueType.Name == "Boolean")
                    {
                        var isCheckBox = (DataGridViewCheckBoxCell)cell;
                        if (isCheckBox != null)
                        {
                            if ((Boolean)isCheckBox.Value == false)
                                isCheckBox.Value = true;
                            else
                                isCheckBox.Value = false;
                        }
                    }
                }
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
                for (int i = 0; i < this.dgShow.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dgShow.Columns[i].HeaderText;
                }
                //写入数值  
                for (int r = 0; r < dgShow.Rows.Count; r++)
                {
                    for (int i = 0; i < dgShow.ColumnCount; i++)
                    {
                        worksheet.Cells[r + 2, i + 1] = dgShow.Rows[r].Cells[i].Value;
                    }
                    System.Windows.Forms.Application.DoEvents();
                }
                worksheet.Columns.EntireColumn.AutoFit();//列宽自适应  
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

        private void btnRefund_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("        确认已退款?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var selectRow in this.ResultList.Where(v => v.IsSelect == true).ToList())
                {
                    //foreach (var selectRow in this.dgShow.SelectedRows)
                    //{
                    //var gridRow = selectRow as DataGridViewRow;
                    //var row = gridRow.DataBoundItem as ExpressReceiveEntry;
                    var row = selectRow;
                    string command = string.Empty;
                    if (row != null && row.ID.HasValue)//&& row == UserEntry.Name)
                    {
                        command = " update ExpressReceive set Result='已退款' where id='" + row.ID + "'";
                        string message = conn.ExecuteSql(command);
                        if (!string.IsNullOrWhiteSpace(message))
                            MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                        foreach (var item in ResultList)
                        {
                            if (item.ID == row.ID)
                            {
                                item.Result = "已退款";
                            }
                        }
                    }
                }
                this.dgShow.DataSource = null;
                this.dgShow.DataSource = ResultList;
            }
        }

        private void btnReSend_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("        确认已补发?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var selectRow in this.ResultList.Where(v => v.IsSelect == true).ToList())
                {
                    //foreach (var selectRow in this.dgShow.SelectedRows)
                    //{
                    //var gridRow = selectRow as DataGridViewRow;
                    //var row = gridRow.DataBoundItem as ExpressReceiveEntry;
                    var row = selectRow;
                    string command = string.Empty;
                    if (row != null && row.ID.HasValue)//&& row == UserEntry.Name)
                    {
                        command = " update ExpressReceive set Result='已补发' where id='" + row.ID + "'";
                        string message = conn.ExecuteSql(command);
                        if (!string.IsNullOrWhiteSpace(message))
                            MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                        foreach (var item in ResultList)
                        {
                            if (item.ID == row.ID)
                            {
                                item.Result = "已补发";
                            }
                        }
                    }
                }
                this.dgShow.DataSource = null;
                this.dgShow.DataSource = ResultList;
            }
        }

        private void btnAu_Click(object sender, EventArgs e)
        {
            ExpressAuthorization exAu = new ExpressAuthorization();
            exAu.Show();
        }

        private void dgShow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.dgShow.SelectedCells.Count > 0)
                {
                    var selectCell = (DataGridViewCheckBoxCell)this.dgShow.SelectedCells[0];
                    if (selectCell != null)
                    {
                        if ((bool)selectCell.Value == false)
                            selectCell.Value = true;
                        else
                            selectCell.Value = false;
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("        确认删除?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var selectRow in this.ResultList.Where(v => v.IsSelect == true).ToList())
                {
                    //foreach (var selectRow in this.dgShow.SelectedRows)
                    //{
                    //var gridRow = selectRow as DataGridViewRow;
                    //var row = gridRow.DataBoundItem as ExpressReceiveEntry;
                    var row = selectRow;
                    string command = string.Empty;
                    if (row != null && row.ID.HasValue && row.CreateUser == UserEntry.Name)
                    {
                        command = " update ExpressReceive set IsDelete=1 where id='" + row.ID + "'";
                        string message = conn.ExecuteSql(command);
                        if (!string.IsNullOrWhiteSpace(message))
                            MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                        //foreach (var item in ResultList)
                        //{
                        //    if (item.ID == row.ID)
                        //    {
                        //        item.Result = "已补发";
                        //    }
                        //}
                    }
                }
                Search();
                //this.dgShow.DataSource = null;
                //this.dgShow.DataSource = ResultList;
            }
        }
    }
}
