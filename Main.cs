using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OrderManage;
using System.Drawing.Imaging;
using System.IO;

namespace OrderManage
{
    public partial class Main : Form
    {
        public List<OrderEntry> resultList = new List<OrderEntry>();
        private List<RefundDetail> totalMsg = new List<RefundDetail>();
        System.Net.WebClient myWebClient;
        Image image;
        SqlConn sqlConn = new SqlConn();
        List<string> InputAuthorizationUserList = new List<string>();//录入权限授权人员
        List<string> DealAuthorizationUserList = new List<string>();//处理权限授权人员
        List<string> DeleteAuthorizationUserList = new List<string>();//删除权限授权人员
        List<string> UpdateAuthorizationUserList = new List<string>();//更改路径权限授权人员
        List<string> AuditingAuthorizationUserList = new List<string>();//审核权限授权人员
        List<string> PictureAuthorizationUserList = new List<string>();//图片查询权限授权人员
        List<string> ResionList = new List<string>();
        List<string> ShopNameList = new List<string>();
        List<string> AliPayNoList = new List<string>();//支付宝帐号
        public Main()
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
            //this.txtTotal.BackColor = Color.Transparent;
            //this.label1.BackColor = Color.Transparent;
            //this.label2.BackColor = Color.Transparent;
            //this.label3.BackColor = Color.Transparent;
            //this.label4.BackColor = Color.Transparent;
            //this.label5.BackColor = Color.Transparent;
            //this.label6.BackColor = Color.Transparent;
            //this.label7.BackColor = Color.Transparent;
            //this.label17.BackColor = Color.Transparent;
            //this.label12.BackColor = Color.Transparent;
            //this.label10.BackColor = Color.Transparent;
            //this.label16.BackColor = Color.Transparent;
            //this.label13.BackColor = Color.Transparent;
            //this.label8.BackColor = Color.Transparent;
            //this.label9.BackColor = Color.Transparent;
            //this.ttt.BackColor = Color.Transparent;
            //this.lbNo.BackColor = Color.Transparent;
            this.cmbTime.SelectedIndex = 0;
            this.dgShow.AutoGenerateColumns = false;
            this.dgRefundTotal.AutoGenerateColumns = false;
            this.dgShow.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            Init();
            this.StartPosition = FormStartPosition.CenterScreen;
            //注册热键Shift+S，Id号为100。HotKey.KeyModifiers.Shift也可以直接使用数字4来表示。
            //HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Shift, Keys.S);
            ////注册热键Ctrl+B，Id号为101。HotKey.KeyModifiers.Ctrl也可以直接使用数字2来表示。
            //HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Ctrl, Keys.B);
            ////注册热键Alt+D，Id号为102。HotKey.KeyModifiers.Alt也可以直接使用数字1来表示。
            //HotKey.RegisterHotKey(Handle, 102, HotKey.KeyModifiers.Alt, Keys.D);
        }

        private void Init()
        {
            InputAuthorizationUserList = new List<string>();
            DealAuthorizationUserList = new List<string>();
            DeleteAuthorizationUserList = new List<string>();
            UpdateAuthorizationUserList = new List<string>();
            AuditingAuthorizationUserList = new List<string>();
            PictureAuthorizationUserList = new List<string>();
            ResionList = new List<string>();
            ShopNameList = new List<string>();
            AliPayNoList = new List<string>();
            string command = string.Empty;
            //查询录入授权人员
            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='Input'";
            DataTable dt = sqlConn.GetDataTableBySql(command);
            var InputUser = dt.Rows[0][0].ToString().Split(',').ToList();
            foreach (var item in InputUser)
            {
                InputAuthorizationUserList.Add(item);
            }
            //查询处理授权人员
            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='Deal'";
            dt = sqlConn.GetDataTableBySql(command);
            var DealUser = dt.Rows[0][0].ToString().Split(',').ToList();
            foreach (var item in DealUser)
            {
                DealAuthorizationUserList.Add(item);
            }
            //查询退款原因
            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='Resion'";
            dt = sqlConn.GetDataTableBySql(command);
            var resion = dt.Rows[0][0].ToString().Split(',').ToList();
            ResionList.Add("");
            foreach (var item in resion)
            {
                ResionList.Add(item);
            }
            //查询店铺名称
            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='ShopName'";
            dt = sqlConn.GetDataTableBySql(command);
            var shopName = dt.Rows[0][0].ToString().Split(',').ToList();
            ShopNameList.Add("");
            foreach (var item in shopName)
            {
                ShopNameList.Add(item);
            }
            //查询支付宝帐号
            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='AlipayNo'";
            dt = sqlConn.GetDataTableBySql(command);
            var AliPayNo = dt.Rows[0][0].ToString().Split(',').ToList();
            AliPayNoList.Add("");
            foreach (var item in AliPayNo)
            {
                AliPayNoList.Add(item);
            }
            //查询删除授权人员
            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='Delete'";
            dt = sqlConn.GetDataTableBySql(command);
            var DeleteUser = dt.Rows[0][0].ToString().Split(',').ToList();
            foreach (var item in DeleteUser)
            {
                DeleteAuthorizationUserList.Add(item);
            }
            //查询审核授权人员
            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='AuditUser'";
            dt = sqlConn.GetDataTableBySql(command);
            if (dt.Rows.Count > 0)
            {
                var AuditingUser = dt.Rows[0][0].ToString().Split(',').ToList();
                foreach (var item in AuditingUser)
                {
                    AuditingAuthorizationUserList.Add(item);
                }
            }
            else
                this.btnAuditing.Enabled = false;
            //查询更改路径授权人员
            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='UpdatePathUser'";
            dt = sqlConn.GetDataTableBySql(command);
            var UpdatePathUser = dt.Rows[0][0].ToString().Split(',').ToList();
            foreach (var item in UpdatePathUser)
            {
                UpdateAuthorizationUserList.Add(item);
            }
            //查询图片查询授权人员
            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='PictureUser'";
            dt = sqlConn.GetDataTableBySql(command);
            var PictrueUser = dt.Rows[0][0].ToString().Split(',').ToList();
            foreach (var item in PictrueUser)
            {
                PictureAuthorizationUserList.Add(item);
            }
            if (!string.IsNullOrWhiteSpace(UserEntry.Name))
            {
                if (InputAuthorizationUserList.Count() > 0 && InputAuthorizationUserList.Where(v=>v==UserEntry.Name).Count()>0)
                {
                    //this.btnInput.Visible = true;
                    this.btnInput.Enabled = true;
                }
                else
                    this.btnInput.Enabled = false;
                if (DealAuthorizationUserList.Count() > 0 && DealAuthorizationUserList.Where(v => v == UserEntry.Name).Count() > 0)
                {
                    //this.btnInput.Visible = true;
                    this.btnDeal.Enabled = true;
                    this.btnUpdate.Enabled = true;
                    //HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.CtrlAndAlt, Keys.A);
                    //this.btnOpen.Text = "关闭快捷键";
                }
                else
                {
                    this.btnDeal.Enabled = false;
                    this.btnUpdate.Enabled = true;
                }
                if (UpdateAuthorizationUserList.Count() > 0 && UpdateAuthorizationUserList.Where(v => v == UserEntry.Name).Count() > 0)
                    this.btnUpdatePath.Enabled = true;
                else
                    this.btnUpdatePath.Enabled = false;
                if (AuditingAuthorizationUserList.Count() > 0 && AuditingAuthorizationUserList.Where(v => v == UserEntry.Name).Count() > 0)
                    this.btnAuditing.Enabled = true;
                else
                    this.btnAuditing.Enabled = false;
                //if (DeleteAuthorizationUserList.Count() > 0 && DeleteAuthorizationUserList.Contains(UserEntry.Name))
                //{
                //    //this.btnInput.Visible = true;
                //    this.btnDelete.Enabled = true;
                //}
                //else
                //    this.btnDelete.Enabled = false;
                if (PictureAuthorizationUserList.Count() > 0 && PictureAuthorizationUserList.Where(v => v == UserEntry.Name).Count() > 0)
                    this.btnPictureSearch.Enabled = true;
                else
                    this.btnPictureSearch.Enabled = false;

                if (UserEntry.Name == "管理员")
                    this.btnAn.Visible = true;
                else
                    this.btnAn.Visible = false;
            }
            this.btnDelete.Enabled = true;
            this.cmbResion.DataSource = null;
            this.cmbResion.DataSource = ResionList;
            this.cmbShopName.DataSource = null;
            this.cmbShopName.DataSource = ShopNameList;
            this.cmbAlipayNo.DataSource = null;
            this.cmbAlipayNo.DataSource = AliPayNoList;
            List<string> status = new List<string>();
            status.Add("");
            status.Add("已处理");
            status.Add("未处理");
            status.Add("待审核");
            status.Add("未成功");
            status.Add("未补全");
            this.cmbStatus.DataSource = status;
            List<string> lsNo = new List<string>();
            lsNo.Add("");
            lsNo.Add("已处理");
            lsNo.Add("未处理");
            this.cmbNo.DataSource = lsNo;
            List<string> remark = new List<string>();
            remark.Add("");
            remark.Add("有备注");
            remark.Add("无备注");
            this.cmbRemark.DataSource = remark;
            //查询截图路径
            //command = " select \"value\" from \"Value\" where \"id\"=999";
            //dt = sqlConn.GetDataTableBySql(command);
            //var path = dt.Rows[0][0].ToString();
            //if (!string.IsNullOrWhiteSpace(path))
            //    this.tbPath.Text = path;
            //查询流水号
            command = " select \"id\" from \"Value\" where \"value\"='test'";
            dt = sqlConn.GetDataTableBySql(command);
            var no = dt.Rows[0][0].ToString();
            if (!string.IsNullOrWhiteSpace(no))
                this.lbNo.Text = no;

            //查询余额
            //command = " select \"money\" from \"Value\" where \"value\"='"+this.cmb+"'";
            //dt = sqlConn.GetDataTableBySql(command);
            //var balance = dt.Rows[0][0].ToString();
            //if (!string.IsNullOrWhiteSpace(balance))
            //    this.lbBalance.Text = "余额:"+balance;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            resultList = new List<OrderEntry>();
            string command = string.Empty;
            command = "select * from \"OrderRecord\" where \"IsDelete\"='False' ";
            if(!string.IsNullOrWhiteSpace(this.cmbShopName.Text))
            {
                command += " and \"ShopName\" like '%"+this.cmbShopName.Text+"%'";
            }
            if(!string.IsNullOrWhiteSpace(this.tbBuyersID.Text))
            {
                command += " and \"BuyersID\" like '%" + this.tbBuyersID.Text.Trim() + "%'";
            }
            if (!string.IsNullOrWhiteSpace(this.tbRefundWay.Text))
            {
                command += " and \"RefundWay\" like '%" + this.tbRefundWay.Text + "%'";
            }
            if (!string.IsNullOrWhiteSpace(this.tbRemark.Text))
            {
                command += " and \"Remark\" like '%" + this.tbRemark.Text + "%'";
            }
            if (!string.IsNullOrWhiteSpace(this.tbAlipayNo.Text))
            {
                command += " and \"AlipayNo\" like '%" + this.tbAlipayNo.Text + "%'";
            }
            if (!string.IsNullOrWhiteSpace(this.cmbResion.Text))
            {
                command += " and \"RefundResoin\" like '%" + this.cmbResion.Text + "%'";
            }
            if (!string.IsNullOrWhiteSpace(this.tbCreateUser.Text))
            {
                command += " and \"CreateUser\" like '%" + this.tbCreateUser.Text + "%'";
            }
            if (!string.IsNullOrWhiteSpace(this.tbRefundUser.Text))
            {
                command += " and \"RefundUserName\" like '%" + this.tbRefundUser.Text + "%'";
            }
            if(!string.IsNullOrWhiteSpace(this.cmbRemark.Text))
            {
                if (this.cmbRemark.Text == "有备注")
                    command += " and \"Remark\" <>''";
                else
                    command += " and \"Remark\" =''";
            }
            if (!string.IsNullOrWhiteSpace(this.cmbStatus.Text))
            {
                if (this.cmbStatus.Text == "已处理")
                    command += " and \"Status\" ='已处理'";
                else if (this.cmbStatus.Text == "待审核")
                    command += " and \"Status\" ='待审核'";
                else if (this.cmbStatus.Text == "未成功")
                    command += " and \"Status\" ='未成功'";
                else if (this.cmbStatus.Text == "未补全")
                    command += " and \"Status\" ='未补全'";
                else
                    command += " and \"Status\" ='未处理'";
            }
            if (!string.IsNullOrWhiteSpace(this.cmbNo.Text))
            {
                if (this.cmbNo.Text == "已处理")
                    command += " and \"No\" is not null ";
                else
                    command += " and \"No\" is null ";
            }
            if (this.cmbTime.Text == "录入时间")
            {
                if (this.timeRefoundFrom.Value != null)
                {
                    var timeFrom = this.timeRefoundFrom.Value;
                    timeFrom = timeFrom.AddHours(-this.timeRefoundFrom.Value.Hour).AddMinutes(-this.timeRefoundFrom.Value.Minute).AddSeconds(-this.timeRefoundFrom.Value.Second);

                    command += " and convert(datetime,CreateTime) >='" + timeFrom.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }
                if (this.timeRefoundTo.Value != null)
                {
                    var timeTo = this.timeRefoundTo.Value;
                    timeTo = timeTo.AddHours(-this.timeRefoundTo.Value.Hour).AddMinutes(-this.timeRefoundTo.Value.Minute).AddSeconds(-this.timeRefoundTo.Value.Second);
                    timeTo = timeTo.AddHours(23).AddMinutes(59).AddSeconds(59);
                    command += " and convert(datetime,CreateTime) <='" + timeTo.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }
            }
            else if (this.cmbTime.Text == "退款时间")
            {
                if (this.timeRefoundFrom.Value != null)
                {
                    var timeFrom = this.timeRefoundFrom.Value;
                    timeFrom = timeFrom.AddHours(-this.timeRefoundFrom.Value.Hour).AddMinutes(-this.timeRefoundFrom.Value.Minute).AddSeconds(-this.timeRefoundFrom.Value.Second);
                    //timeFrom = timeFrom.AddHours(23).AddMinutes(59).AddSeconds(59);
                    command += " and convert(datetime,RefundTime) >='" + timeFrom.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }
                if (this.timeRefoundTo.Value != null)
                {
                    var timeTo = this.timeRefoundTo.Value;
                    timeTo = timeTo.AddHours(-this.timeRefoundTo.Value.Hour).AddMinutes(-this.timeRefoundTo.Value.Minute).AddSeconds(-this.timeRefoundTo.Value.Second);
                    timeTo = timeTo.AddHours(23).AddMinutes(59).AddSeconds(59);
                    command += " and convert(datetime,RefundTime) <='" + timeTo.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }
            }
            if (this.cmbSort.Text == "价格")
                command += " order by RefundAmount";
            else if (this.cmbSort.Text == "退款时间")
                command += " order by RefundTime";
            else if (this.cmbSort.Text == "录入时间")
                command += " order by convert(datetime,CreateTime)";
            else
                command += " order by convert(datetime,CreateTime)";
            if (this.radioButton1.Checked == true)
                command += " desc";
            else if (this.radioButton2.Checked == true)
                command += " asc";
            DataTable dt = sqlConn.GetDataTableBySql(command);
            if(dt.Rows.Count>0)
            {
                int seq = 1;
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    OrderEntry entry = new OrderEntry();
                    entry.SeqNum = seq;
                    entry.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    entry.Status = dt.Rows[i]["Status"].ToString();
                    entry.ShopName = dt.Rows[i]["ShopName"].ToString();
                    entry.BuyersID = dt.Rows[i]["BuyersID"].ToString();
                    entry.RefundWay = dt.Rows[i]["RefundWay"].ToString();
                    entry.AlipayNo = dt.Rows[i]["AlipayNo"].ToString();
                    //提取姓名
                    if (!string.IsNullOrWhiteSpace(entry.AlipayNo))
                    {
                        string Chinese = string.Empty;
                        string aplipay = entry.AlipayNo;
                        for (int x = 0; x < aplipay.Length;x++ )
                        {
                            if ((int)aplipay[x] > 127)
                            {
                                Chinese += aplipay[x];
                                entry.AlipayNo = entry.AlipayNo.Replace(aplipay[x].ToString(),"");
                            }
                        }
                        entry.AlipayName = Chinese;
                    }
                    entry.RefundAmount = decimal.Parse(dt.Rows[i]["RefundAmount"].ToString());
                    entry.RefundResoin = dt.Rows[i]["RefundResoin"].ToString();
                    entry.Remark = dt.Rows[i]["Remark"].ToString();
                    entry.CreateUser = dt.Rows[i]["CreateUser"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[i]["RefundTime"].ToString()))
                        entry.RefundTime = DateTime.Parse(dt.Rows[i]["RefundTime"].ToString());

                    entry.RefundUserName = dt.Rows[i]["RefundUserName"].ToString();
                    //if (dt.Rows[i]["IsDelete"].ToString() == "1")
                    //    entry.IsDelete = true;
                    //else
                    //    entry.IsDelete = false;
                    if (!string.IsNullOrEmpty(dt.Rows[i]["CreateTime"].ToString()))
                        entry.CreateTime = DateTime.Parse(dt.Rows[i]["CreateTime"].ToString());
                    entry.PDName1 = dt.Rows[i]["PDName1"].ToString();
                    entry.PDName2 = dt.Rows[i]["PDName2"].ToString();
                    entry.PDName3 = dt.Rows[i]["PDName3"].ToString();
                    entry.PDName4 = dt.Rows[i]["PDName4"].ToString();
                    entry.PDName5 = dt.Rows[i]["PDName5"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[i]["No"].ToString()))
                        entry.No = int.Parse(dt.Rows[i]["No"].ToString());
                    else
                        entry.No = null;
                    entry.ExpressNo2 = dt.Rows[i]["ExpressNo2"].ToString();
                    entry.ExpressNo3 = dt.Rows[i]["ExpressNo3"].ToString();
                    entry.RefundAlipayNo = dt.Rows[i]["RefundAlipayNo"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[i]["InputCount"].ToString()))
                        entry.InputCount = int.Parse(dt.Rows[i]["InputCount"].ToString());
                    //entry.InputCount = this.QueryInputCount(entry.BuyersID,entry.CreateTime.Value);
                    seq++;
                    resultList.Add(entry);
                }
            }
            if (this.cbTotalRefund.Checked == true)
            {
                //统计退款原因各自的金额
                totalMsg = new List<RefundDetail>();
                string total = string.Empty;
                foreach (var item in resultList)
                {
                    var detailList = this.QueryDetail(item.ID);
                    if (detailList.Count() > 0)
                    {
                        totalMsg.AddRange(detailList);
                    }
                    else
                    {
                        RefundDetail entry = new RefundDetail();
                        entry.RefundAmount = item.RefundAmount;
                        entry.RefundResoin = item.RefundResoin;
                        entry.orderId = item.ID.Value;
                        totalMsg.Add(entry);
                    }
                }
                if (totalMsg.Count() > 0)
                {
                    decimal totalAmount = 0;
                    var groupResion = totalMsg.GroupBy(v => v.RefundResoin).ToList();
                    foreach (var resion in groupResion)
                    {
                        decimal amount = 0;
                        var data = totalMsg.Where(v => v.RefundResoin == resion.Key).ToList();
                        foreach (var detail in data)
                        {
                            amount += detail.RefundAmount;
                            totalAmount += detail.RefundAmount;
                        }
                        total += resion.Key + ":" + amount + ",";
                    }
                    total = "总退款金额:" + totalAmount + "," + total.Substring(0, total.Length - 1);
                    this.txtTotal.Text = total;
                    //this.lbRefundWayTotal.Text = total;
                }
            }
            //if (this.cmbSort.Text == "价格")
            //    resultList = resultList.OrderBy(v => v.RefundAmount).ToList();
            //else if (this.cmbSort.Text == "退款时间")
            //    resultList = resultList.OrderBy(v => v.RefundTime).ToList();
            //else if (this.cmbSort.Text == "录入时间")
            //    resultList = resultList.OrderBy(v => v.CreateTime).ToList();
            this.dgShow.DataSource = null;
            this.dgShow.DataSource = resultList;
        }

        public int QueryInputCount(string buyID,DateTime createTime)
        {
            int count = 0;
            DateTime lastMonth = DateTime.Now.AddMonths(-1);
            string command = string.Empty;
            command = "select count(*) from OrderRecord where BuyersID='" + buyID + "' and convert(datetime,CreateTime) >='" + lastMonth.ToString("yyyy-MM-dd HH:mm:ss") + "' and convert(datetime,CreateTime) <='" + createTime.ToString("yyyy-MM-dd HH:mm:ss") + "'  and IsDelete='false'";
            DataTable dt = sqlConn.GetDataTableBySql(command);
            if (dt.Rows.Count > 0)
            {
                count += int.Parse(dt.Rows[0][0].ToString());
            }
            return count;
        }

        public List<RefundDetail> QueryDetail(int? orderID)
        {
            List<RefundDetail> result = new List<RefundDetail>();
            string command = string.Empty;
            command = "select RefundAmount,RefundResoin,code from RefundDetail where orderId ='" + orderID + "'";
            DataTable dt = sqlConn.GetDataTableBySql(command);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RefundDetail entry = new RefundDetail();
                    entry.RefundAmount = decimal.Parse(dt.Rows[i][0].ToString());
                    entry.RefundResoin = dt.Rows[i][1].ToString();
                    entry.code = dt.Rows[i][2].ToString();
                    entry.orderId = orderID.Value;
                    result.Add(entry);
                }
            }
            return result;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            Input input = new Input(ShopNameList,ResionList);
            input.Show();
        }

        private void btnDeal_Click(object sender, EventArgs e)
        {
            //this.dgShow.Rows[this.dgShow.SelectedCells[0].RowIndex].Selected = true;
            //if (this.dgShow.SelectedRows.Count > 0)
            //{
                
            //}
            Deal();
        }
        private void Deal()
        {
            if (MessageBox.Show("          确认处理?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string command = string.Empty;
                command = "select \"id\" from \"Value\" where \"value\"='test'";
                DataTable dt1 = sqlConn.GetDataTableBySql(command);
                int maxValue = int.Parse(dt1.Rows[0][0].ToString());
                int nextValue = maxValue + 1;
                command = "update \"Value\" set id='" + nextValue + "' where \"value\"='test'";
                string message = sqlConn.ExecuteSql(command);
                this.lbNo.Text = "" + maxValue;

                //command = "select \"id\" from \"Value\" where \"value\"='balance'";
                //dt1 = sqlConn.GetDataTableBySql(command);
                //int balance = int.Parse(dt1.Rows[0][0].ToString());

                foreach (var selectRow in this.resultList.Where(v => v.IsSelect == true).ToList())
                {
                    //foreach (var selectRow in this.dgShow.SelectedRows)
                    //{
                    //var gridRow = selectRow as DataGridViewRow;
                    //var row = gridRow.DataBoundItem as OrderEntry;
                    var row = selectRow;
                    if (row != null && row.ID.HasValue)
                    {
                        if (row.Status == "待审核")
                        {
                            MessageBox.Show("序号:" + row.SeqNum + " 状态为待审核,请审核后再处理.");
                            continue;
                        }
                        if (row.RefundWay == "支付宝")
                        {
                            if (string.IsNullOrWhiteSpace(this.cmbAlipayNo.Text) == true)
                            {
                                MessageBox.Show("序号:" + row.SeqNum + " 退款方式是支付宝,请选择退款支付宝帐号");
                                continue;
                            }
                            command = "select \"No\" from \"OrderRecord\" where ID='" + row.ID + "'";
                            DataTable dt = sqlConn.GetDataTableBySql(command);
                            var status = dt.Rows[0][0].ToString();
                            if (!string.IsNullOrWhiteSpace(status) && status != "0")
                            {
                                //MessageBox.Show("序号:" + row.SeqNum + " 已被处理,不能重复处理");
                                //continue;
                            }
                            else
                            {
                                //if (string.IsNullOrWhiteSpace(this.cmbAlipayNo.Text) == true)
                                //{
                                //    command = "update \"OrderRecord\" set \"RefundUserName\"='" + UserEntry.Name + "',\"RefundTime\"='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',\"No\"='" + maxValue + "' where ID='" + row.ID + "'";
                                //}
                                //else

                                command = "update \"OrderRecord\" set \"RefundUserName\"='" + UserEntry.Name + "',\"RefundTime\"='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',\"No\"='" + maxValue + "',\"RefundAlipayNo\"='" + this.cmbAlipayNo.Text + "' where ID='" + row.ID + "'";
                                message = sqlConn.ExecuteSql(command);
                                //if (!string.IsNullOrWhiteSpace(message))
                                //    MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                                foreach (var item in resultList)
                                {
                                    if (item.ID == row.ID)
                                    {
                                        item.No = maxValue;
                                        item.RefundAlipayNo = this.cmbAlipayNo.Text;
                                    }
                                }

                                
                            }
                        }

                        //更新余额
                        if (row.RefundAmount > 0)
                        {
                            command = "update \"value\" set money=money-" + row.RefundAmount + " where value='" + this.cmbAlipayNo.SelectedItem + "'";
                            message = sqlConn.ExecuteSql(command);

                        }

                        command = "select \"Status\",\"RefundUserName\" from \"OrderRecord\" where ID='" + row.ID + "'";
                        DataTable tb = sqlConn.GetDataTableBySql(command);
                        var status1 = tb.Rows[0][0].ToString();
                        var refundUserName = tb.Rows[0][1].ToString();
                        if (status1 == "已处理" && refundUserName == UserEntry.Name)
                        {
                            //MessageBox.Show("序号:" + row.SeqNum + " 已被处理,不能重复处理");
                            //continue;
                            if (MessageBox.Show("    序号:" + row.SeqNum + " 已被处理,是否取消已处理状态?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                command = "update \"OrderRecord\" set \"RefundUserName\"='',\"RefundTime\"='',\"Status\"='未处理',\"RefundAlipayNo\"='',\"No\"='' where ID='" + row.ID + "'";
                                message = sqlConn.ExecuteSql(command);
                                if (!string.IsNullOrWhiteSpace(message))
                                {
                                    MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                                    continue;
                                }
                                foreach (var item in resultList)
                                {
                                    if (item.ID == row.ID)
                                    {
                                        item.Status = "未处理";
                                        item.RefundTime = null;
                                        item.RefundUserName = "";
                                        item.RefundAlipayNo = "";
                                        item.No = null;
                                    }
                                }
                                continue;
                            }
                            else
                                continue;
                        }
                        else if (status1 != "已处理")
                        {
                            command = "update \"OrderRecord\" set \"RefundUserName\"='" + UserEntry.Name + "',\"RefundTime\"='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',\"Status\"='已处理',\"No\"='" + maxValue + "',\"RefundAlipayNo\"='" + this.cmbAlipayNo.Text + "' where ID='" + row.ID + "'";
                            message = sqlConn.ExecuteSql(command);
                            if (!string.IsNullOrWhiteSpace(message))
                                MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                            foreach (var item in resultList)
                            {
                                if (item.ID == row.ID)
                                {
                                    item.Status = "已处理";
                                    item.RefundTime = DateTime.Now;
                                    item.RefundUserName = UserEntry.Name;
                                    item.RefundAlipayNo = this.cmbAlipayNo.Text;
                                    item.No = maxValue;
                                }
                            }
                        }
                        else
                            continue;

                        
                    }

                    //if(row.)
                }

                //foreach (var selectRow in this.dgShow.SelectedRows)
                //{
                //    var gridRow = selectRow as DataGridViewRow;
                //    var row = gridRow.DataBoundItem as OrderEntry;
                //    if (row!=null&&row.ID.HasValue)
                //    {
                //        command = "select \"Status\" from \"OrderRecord\" where ID='" + row.ID + "'";
                //        DataTable dt = sqlConn.GetDataTableBySql(command);
                //        var status = dt.Rows[0][0].ToString();
                //        if (status == "已处理")
                //        {
                //            MessageBox.Show("序号:"+row.SeqNum+" 已被处理,不能重复处理");
                //            continue;
                //        }
                //        command = "update \"OrderRecord\" set \"RefundUserName\"='" + UserEntry.Name + "',\"RefundTime\"='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',\"Status\"='已处理' where ID='" + row.ID + "'";
                //        string message = sqlConn.ExecuteSql(command);
                //        if (!string.IsNullOrWhiteSpace(message))
                //            MessageBox.Show("序号:"+row.SeqNum+" 处理失败"+"失败原因:"+message);
                //        foreach(var item in resultList)
                //        {
                //            if (item.ID == row.ID)
                //                item.Status = "已处理";
                //        }
                //    }
                //    //if(row.)
                //}

                this.dgShow.DataSource = null;
                this.dgShow.DataSource = resultList;
                UpdateBalace();
                //Search();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //if (this.dgShow.SelectedRows.Count > 0)
            //{
                if (MessageBox.Show("          确认删除?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string command = string.Empty;
                    foreach (var selectRow in this.resultList.Where(v => v.IsSelect == true).ToList())
                    {
                        //foreach (var selectRow in this.dgShow.SelectedRows)
                        //{
                        //var gridRow = selectRow as DataGridViewRow;
                        //var row = gridRow.DataBoundItem as OrderEntry;
                        var row = selectRow;
                        if (row != null && row.ID.HasValue)
                        {
                            command = "select \"CreateUser\",\"Status\" from \"OrderRecord\" where \"ID\"='"+row.ID+"'";
                            DataTable dt = sqlConn.GetDataTableBySql(command);
                            var name = dt.Rows[0][0].ToString();
                            var status = dt.Rows[0][1].ToString();
                            if(name!=UserEntry.Name)
                            {
                                if (DeleteAuthorizationUserList.Where(v => v == UserEntry.Name).Count() == 0)
                                {
                                    MessageBox.Show("您没有权限删除其他人录入的数据");
                                    continue;
                                }
                                //else
                                //MessageBox.Show("只有录入人能删除");
                                //continue;
                            }
                            if (status == "已处理" && DeleteAuthorizationUserList.Where(v => v == UserEntry.Name).Count() == 0)
                            {
                                MessageBox.Show("已处理的数据不能删除!");
                                continue;
                            }
                            command = "update \"OrderRecord\" set \"IsDelete\"='true' where ID='" + row.ID + "'";
                            string message = sqlConn.ExecuteSql(command);
                            if (!string.IsNullOrWhiteSpace(message))
                                MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                        }
                        //if(row.)
                    }
                    Search();
                }
           // }
        }

        private void btnAn_Click(object sender, EventArgs e)
        {
            AuthorizationSet set = new AuthorizationSet(InputAuthorizationUserList,DealAuthorizationUserList,ResionList,ShopNameList,DeleteAuthorizationUserList);
            set.Show();
            //this.Hide();
        }

        private void btnRefush_Click(object sender, EventArgs e)
        {
            Init();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //if(this.dgShow.SelectedRows.Count>0)
            //{
                
            //}
            UpdateRemark();
        }
        private void UpdateRemark()
        {
            if (MessageBox.Show("          确认更改备注?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string command = string.Empty;
                foreach (var selectRow in this.resultList.Where(v => v.IsSelect == true).ToList())
                {
                    //foreach (var selectRow in this.dgShow.SelectedRows)
                    //{
                    //var gridRow = selectRow as DataGridViewRow;
                    //var row = gridRow.DataBoundItem as OrderEntry;
                    var row = selectRow;
                    if (row != null && row.ID.HasValue)
                    {
                        command = "update \"OrderRecord\" set \"Remark\"='" + row.Remark + "' where ID='" + row.ID + "'";
                        string message = sqlConn.ExecuteSql(command);
                        if (!string.IsNullOrWhiteSpace(message))
                            MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                    }
                    //if(row.)
                }
                Search();
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

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            //注销Id号为100的热键设定
            HotKey.UnregisterHotKey(Handle, 100);
            Application.ExitThread();
        }

        private void btnTopMost_Click(object sender, EventArgs e)
        {
            if (this.TopMost == false)
            {
                this.TopMost = true;
                this.btnTopMost.Text = "取消顶置";
            }
            else
            {
                this.TopMost = false;
                this.btnTopMost.Text = "窗口顶置";
            }
        }

        private void btnUpdateRecord_Click(object sender, EventArgs e)
        {
            //if (this.dgShow.SelectedRows.Count > 0)
            //{
                if (MessageBox.Show("          确认修改记录?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string command = string.Empty;
                    foreach (var selectRow in this.resultList.Where(v => v.IsSelect == true).ToList())
                    {
                        //foreach (var selectRow in this.dgShow.SelectedRows)
                        //{
                        //var gridRow = selectRow as DataGridViewRow;
                        //var row = gridRow.DataBoundItem as OrderEntry;
                        var row = selectRow;
                        if (row != null && row.ID.HasValue && row.Status != "已处理" && (row.CreateUser == UserEntry.Name || row.Status == "未成功" || row.Status == "未补全"))
                        {
                            command = "update \"OrderRecord\" set \"Remark\"='" + row.Remark + "',\"ShopName\"='" + row.ShopName + "',\"BuyersID\"='" + row.BuyersID + "',\"RefundWay\"='" + row.RefundWay + "',\"AlipayNo\"='" + row.AlipayNo + "',\"RefundAmount\"='" + row.RefundAmount + "',\"RefundResoin\"='" + row.RefundResoin + "',\"PDName1\"='" + row.PDName1 + "',\"PDName2\"='" + row.PDName2 + "',\"PDName3\"='" + row.PDName3 + "',\"PDName4\"='" + row.PDName4 + "',\"PDName5\"='" + row.PDName5 + "',\"ExpressNo2\"='"+row.ExpressNo2+"',\"ExpressNo3\"='"+row.ExpressNo3+"' where ID='" + row.ID + "'";
                            string message = sqlConn.ExecuteSql(command);
                            if (!string.IsNullOrWhiteSpace(message))
                                MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                            else
                                MessageBox.Show("修改成功!");
                        }
                        else if (row.No.HasValue)
                            MessageBox.Show("已处理的不能修改!");
                        else if(row.CreateUser!=UserEntry.Name)
                            MessageBox.Show("只能录入人修改!");
                        //if(row.)
                    }
                    
                    Search();
                }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CardPicture();
        }

        private void CardPicture()
        {
            //int hotX = hotPoint.X;
            //int hotY = hotPoint.Y;
            //Bitmap myNewCursor = new Bitmap(cursor.Width * 2 - hotX, cursor.Height * 2 - hotY);
            //Graphics g = Graphics.FromImage(myNewCursor);
            //g.Clear(Color.FromArgb(0, 0, 0, 0));
            //g.DrawImage(cursor, cursor.Width - hotX, cursor.Height - hotY, cursor.Width,
            //cursor.Height);
            //this.Cursor = new Cursor(myNewCursor.GetHicon());

            //g.Dispose();
            //myNewCursor.Dispose();

            CaptureImageTool capture = new CaptureImageTool();
            //capture.SelectCursor = CursorManager.ArrowNew;
            ////Cursor cu = new System.Windows.Forms.Cursor();
            
            //capture.DrawCursor = CursorManager.CrossNew;
            if (capture.ShowDialog() == DialogResult.OK)
            {
                string command = string.Empty;
                string path = string.Empty;
                //command = " select \"value\" from \"Value\" where \"id\"=999";
                //DataTable dt = sqlConn.GetDataTableBySql(command);
                //var path = dt.Rows[0][0].ToString();
                //if (!string.IsNullOrWhiteSpace(path))
                //    this.tbPath.Text = path;

                command = " select count(*) from \"PicPath\" where \"no\"='" + this.lbNo.Text + "'";
                DataTable dt = sqlConn.GetDataTableBySql(command);
                var noCount = int.Parse(dt.Rows[0][0].ToString()) + 1;

                string picName = string.Empty;
                picName = "流水号" + this.lbNo.Text + "(" + noCount + ").gif";
                //picName = "No" + this.lbNo.Text + "(" + noCount + ")";
                //path = path + picName;
                try
                {

                    image = capture.Image;
                    image.Save(picName, System.Drawing.Imaging.ImageFormat.Gif);
                    image.Dispose();

                    //上传图片
                    this.lbTip.Text = "图片上传中,请稍等";
                    this.lbTip.Visible = true;
                    FontFamily ff = new FontFamily(this.lbTip.Font.Name);
                    var fontStyle = this.lbTip.Font.Style;
                    this.lbTip.Font = new Font(ff, 30, fontStyle, GraphicsUnit.World);
                    this.lbTip.BackColor = Color.Transparent;
                    System.Windows.Forms.Application.DoEvents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("图片保存失败:" + ex.Message);
                    return;
                }
                //this.lbTip.Font.Size = 20;
                //网站地址
                //string webSite = "http://hyw2530790001.my3w.com/UploadFile.aspx";
                string webSite = "http://kefutk.xin/UploadFile.aspx";
                myWebClient = new System.Net.WebClient();
                try
                {
                    var respone = myWebClient.UploadFile(webSite, "POST", picName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("图片上传失败:" + ex.Message);
                    return;
                }
                finally
                {
                    if (File.Exists(picName))
                        File.Delete(picName);

                    myWebClient.Dispose();
                   // myWebClient.
                }
                //path = "http://hyw2530790001.my3w.com/upload/" + picName;
                path = "http://kefutk.xin/upload/" + picName;
                this.lbTip.Visible = false;
                //xx = respone.ToString();
                //image.Save("\\\\192.168.0.102\\sharePic\\3.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                command = " insert into \"PicPath\"(\"no\",\"path\",\"picName\") Values('" + this.lbNo.Text + "','" + path + "','" + picName + "')";
                string message = sqlConn.ExecuteSql(command);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    MessageBox.Show("操作失败,失败原因:" + message);
                    
                }
                else
                    MessageBox.Show("截图保存成功!");
            }
        }

        private void btnUpdatePath_Click(object sender, EventArgs e)
        {
            string command = " select \"value\" from \"Value\" where \"id\"=999";
            DataTable dt = sqlConn.GetDataTableBySql(command);
            var path = dt.Rows[0][0].ToString();
            if (!string.IsNullOrWhiteSpace(path))
            {
                UpdatePicPathUI updateUI = new UpdatePicPathUI(path);
                updateUI.ShowDialog();
                if (updateUI.DialogResult == DialogResult.Cancel &&updateUI.IsUpdate==true)
                {
                    if (!string.IsNullOrWhiteSpace(updateUI.PicPath))
                        this.tbPath.Text = updateUI.PicPath;
                }
            }
        }

        //窗口内监听键盘事件

        //private void Main_Load(object sender, EventArgs e)
        //{
        //    KeyDown += new KeyEventHandler(Form1_KeyDown);
        //}
        //private void Form1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if ((int)e.Modifiers == ((int)Keys.Control + (int)Keys.Alt) && e.KeyCode == Keys.A)
        //    {
        //        //MessageBox.Show("1111111111");
        //        button1_Click(sender,e);
        //    }
        //}

        protected override void WndProc(ref Message m)
        {

            const int WM_HOTKEY = 0x0312;
            //按快捷键 
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 100:    //按下的是Shift+S
                            //此处填写快捷键响应代码 
                            CardPicture();
                            break;
                        case 101:
                            Deal();
                            break;
                        case 102:
                            UnSuess();
                            break;
                        case 103:
                            UpdateRemark();
                            break;
                        case 104:
                            UnBuQuan();
                            break;
                        case 105:
                            copyMoney();
                            break;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.btnOpen.Text == "开启快捷键")
            {
                HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.CtrlAndAlt, Keys.A);
                HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Ctrl, Keys.B);
                HotKey.RegisterHotKey(Handle, 102, HotKey.KeyModifiers.Ctrl, Keys.Z);
                HotKey.RegisterHotKey(Handle, 103, HotKey.KeyModifiers.Ctrl, Keys.N);
                HotKey.RegisterHotKey(Handle, 104, HotKey.KeyModifiers.Ctrl, Keys.M);
                HotKey.RegisterHotKey(Handle, 105, HotKey.KeyModifiers.Ctrl, Keys.L);
                this.btnOpen.Text = "关闭快捷键";
            }
            else
            {
                this.btnOpen.Text = "开启快捷键";
                HotKey.UnregisterHotKey(Handle, 100);
                HotKey.UnregisterHotKey(Handle, 101);
                HotKey.UnregisterHotKey(Handle, 102);
                HotKey.UnregisterHotKey(Handle, 103);
                HotKey.UnregisterHotKey(Handle, 104);
                HotKey.UnregisterHotKey(Handle, 105);
            }

        }

        private void btnAuditing_Click(object sender, EventArgs e)
        {
            //if (this.dgShow.SelectedRows.Count > 0)
            //{
                
            //}
            Auid();
        }

        private void Auid()
        {
            if (MessageBox.Show("          确认审核?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var selectRow in this.resultList.Where(v => v.IsSelect == true).ToList())
                {
                    //foreach (var selectRow in this.dgShow.SelectedRows)
                    //{
                    //var gridRow = selectRow as DataGridViewRow;
                    //var row = gridRow.DataBoundItem as OrderEntry;
                    var row = selectRow;
                    string command = string.Empty;
                    if (row.Status == "待审核" && row.ID.HasValue)
                    {
                        command = "update \"OrderRecord\" set \"Status\"='未处理' where \"ID\"='" + row.ID + "'";
                        sqlConn.ExecuteSql(command);
                        //if (!string.IsNullOrWhiteSpace(message))
                        //    MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                        foreach (var item in resultList)
                        {
                            if (item.ID == row.ID)
                            {
                                item.Status = "未处理";
                            }
                        }
                    }

                }
                this.dgShow.DataSource = null;
                this.dgShow.DataSource = resultList;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ShowPicture set = new ShowPicture(null);
            set.Show();
        }

        private void dgShow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (this.dgShow.SelectedRows.Count > 0)
            //{
            //    var selectRow = this.dgShow.SelectedRows[0];
            //    var gridRow = selectRow as DataGridViewRow;
            //    var row = gridRow.DataBoundItem as OrderEntry;
            //    if (row != null)
            //    {
            //        ShowPicture manual = new ShowPicture(row.No);
            //        manual.Show();
            //    }
            //}
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            if (this.resultList.Where(v => v.IsSelect == true).ToList().Count()>0)
            {
                var selectRow = this.resultList.Where(v=>v.IsSelect==true).ToList()[0];
                //var gridRow = selectRow as DataGridViewRow;
                //var row = gridRow.DataBoundItem as OrderEntry;
                var row = selectRow;
                if (row != null)
                {
                    ShowPicture manual = new ShowPicture(row.No);
                    manual.Show();
                }
            }
        }

        private void ExportRefundExcel()
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
                for (int i = 0; i < this.dgRefundTotal.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dgRefundTotal.Columns[i].HeaderText;
                }
                //写入数值  
                for (int r = 0; r < dgRefundTotal.Rows.Count; r++)
                {
                    for (int i = 0; i < dgRefundTotal.ColumnCount; i++)
                    {
                        worksheet.Cells[r + 2, i + 1] = dgRefundTotal.Rows[r].Cells[i].Value;
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

        private void btnExportRefund_Click(object sender, EventArgs e)
        {
            if (totalMsg.Count() == 0)
            {
                //统计退款原因各自的金额
                totalMsg = new List<RefundDetail>();
                string total = string.Empty;
                foreach (var item in resultList)
                {
                    var detailList = this.QueryDetail(item.ID);
                    if (detailList.Count() > 0)
                    {
                        totalMsg.AddRange(detailList);
                    }
                    else
                    {
                        RefundDetail entry = new RefundDetail();
                        entry.RefundAmount = item.RefundAmount;
                        entry.RefundResoin = item.RefundResoin;
                        entry.orderId = item.ID.Value;
                        totalMsg.Add(entry);
                    }
                }
               
            }
            if (resultList.Count() > 0 && totalMsg.Count() > 0)
            {
                
                List<OrderEntry> finalList = new List<OrderEntry>();
                var refundGroup = totalMsg.GroupBy(v => new { v.code, v.RefundResoin }).ToList();
                int seq = 1;
                foreach(var item in refundGroup)
                {
                    var groupList = totalMsg.Where(v =>v.RefundAmount>0&&!string.IsNullOrWhiteSpace(v.RefundResoin)&& v.RefundResoin == item.Key.RefundResoin && v.code == item.Key.code).ToList();
                    if (groupList.Count() > 0)
                    {
                        OrderEntry entry = new OrderEntry();
                        entry.SeqNum =seq;
                        var detailEntry = groupList.FirstOrDefault();
                        var OrderRecord = this.resultList.Where(v => v.ID == detailEntry.orderId).FirstOrDefault();
                        entry.ID = detailEntry.orderId;
                        entry.RefundResoin = detailEntry.RefundResoin;
                        entry.code = detailEntry.code;
                        entry.RefundAmount = detailEntry.RefundAmount;
                        
                        entry.Status = OrderRecord.Status;
                        entry.No = OrderRecord.No;
                        entry.ShopName = OrderRecord.ShopName;
                        entry.RefundWay = OrderRecord.RefundWay;
                        entry.RefundAlipayNo = OrderRecord.RefundAlipayNo;
                        entry.AlipayNo = OrderRecord.AlipayNo;
                        entry.BuyersID = OrderRecord.BuyersID;
                        decimal totalAmount = 0;
                        foreach (var group in groupList)
                        {
                            totalAmount += group.RefundAmount;
                        }
                        entry.RefundTotalAmount = totalAmount;

                        finalList.Add(entry);
                        seq++;
                    }
                }
                this.dgRefundTotal.DataSource = null;
                this.dgRefundTotal.DataSource = finalList;
                ExportRefundExcel();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //KdApiOrderDistinguish api = new KdApiOrderDistinguish();
            //var result = api.orderTracesSubByJson();
            //KdApiSearchDemo api = new KdApiSearchDemo();
            //var result = api.getOrderTracesByJson();
            SearchExpressNo express = new SearchExpressNo();
            express.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //if (this.dgShow.SelectedRows.Count > 0)
            //{
               
            //}
            UnSuess();
        }
        private void UnSuess()
        {
            if (MessageBox.Show("        确认设置未成功?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var selectRow in this.resultList.Where(v => v.IsSelect == true).ToList())
                {
                    //}


                    //foreach (var selectRow in this.dgShow.SelectedRows)
                    //{
                    //var gridRow = selectRow as DataGridViewRow;
                    //var row = gridRow.DataBoundItem as OrderEntry;
                    var row = selectRow;
                    string command = string.Empty;
                    if (row != null && row.ID.HasValue && row.Status == "已处理" && row.RefundUserName == UserEntry.Name)
                    {
                        command = " update OrderRecord set Status='未成功' where id='" + row.ID + "'";
                        string message = sqlConn.ExecuteSql(command);
                        if (!string.IsNullOrWhiteSpace(message))
                        {
                            MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                            continue;
                        }
                        BackMoney(this.cmbAlipayNo.SelectedItem.ToString(), row.RefundAmount);
                        foreach (var item in resultList)
                        {
                            if (item.ID == row.ID)
                            {
                                item.Status = "未成功";
                            }
                        }
                    }
                }
                this.dgShow.DataSource = null;
                this.dgShow.DataSource = resultList;
            }
        }
        private void dgShow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.dgShow.SelectedCells.Count > 0)
                {
                    var selectCell = (DataGridViewCheckBoxCell)this.dgShow.SelectedCells[0];
                    if(selectCell!=null)
                    {
                        if ((bool)selectCell.Value == false)
                            selectCell.Value = true;
                        else
                            selectCell.Value = false;
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UnBuQuan();
        }

        private void UnBuQuan()
        {
            if (MessageBox.Show("        确认设置未补全?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var selectRow in this.resultList.Where(v => v.IsSelect == true).ToList())
                {
                    //}


                    //foreach (var selectRow in this.dgShow.SelectedRows)
                    //{
                    //var gridRow = selectRow as DataGridViewRow;
                    //var row = gridRow.DataBoundItem as OrderEntry;
                    var row = selectRow;
                    string command = string.Empty;
                    if (row != null && row.ID.HasValue && row.Status == "已处理" && row.RefundUserName == UserEntry.Name)
                    {
                        command = " update OrderRecord set Status='未补全' where id='" + row.ID + "'";
                        string message = sqlConn.ExecuteSql(command);
                        if (!string.IsNullOrWhiteSpace(message))
                        {
                            MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                            continue;
                        }
                        BackMoney(this.cmbAlipayNo.SelectedItem.ToString(),row.RefundAmount);
                        foreach (var item in resultList)
                        {
                            if (item.ID == row.ID)
                            {
                                item.Status = "未补全";
                            }
                        }
                    }
                }
                this.dgShow.DataSource = null;
                this.dgShow.DataSource = resultList;
            }
        }

        public void BackMoney(string alipayNo,decimal money)
        {
            string command = string.Empty;
            if (money > 0&&!string.IsNullOrWhiteSpace(alipayNo))
            {
                command = "update \"value\" set money=money+" + money + " where value='" + alipayNo + "'";
                var message = sqlConn.ExecuteSql(command);
                UpdateBalace();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ExpressReceive Receive = new ExpressReceive();
            Receive.Show();
        }

        private void btnUndeal_Click(object sender, EventArgs e)
        {
            UnDeal();
        }
        private void UnDeal()
        {
            if (MessageBox.Show("        确认设置未处理?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (var selectRow in this.resultList.Where(v => v.IsSelect == true).ToList())
                {
                    var row = selectRow;
                    string command = string.Empty;
                    if (row != null && row.ID.HasValue && (row.Status == "未补全" || row.Status == "未成功") && row.RefundUserName == UserEntry.Name)
                    {
                        command = " update OrderRecord set Status='未补全' where id='" + row.ID + "'";
                        string message = sqlConn.ExecuteSql(command);
                        if (!string.IsNullOrWhiteSpace(message))
                            MessageBox.Show("序号:" + row.SeqNum + " 处理失败" + "失败原因:" + message);
                        foreach (var item in resultList)
                        {
                            if (item.ID == row.ID)
                            {
                                item.Status = "未处理";
                            }
                        }
                    }
                }
                this.dgShow.DataSource = null;
                this.dgShow.DataSource = resultList;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.dgShow.SelectedCells.Count > 0)
            {
                foreach(DataGridViewCell cell in this.dgShow.SelectedCells)
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

        private void btnBalance_Click(object sender, EventArgs e)
        {
            BalanceSet set = new BalanceSet();
            set.Show();
        }

        private void UpdateBalace()
        {
            if (!string.IsNullOrEmpty(this.cmbAlipayNo.SelectedItem.ToString()))
            {
                string command = " select \"money\" from \"Value\" where \"value\"='" + this.cmbAlipayNo.SelectedItem.ToString() + "'";
                DataTable dt = sqlConn.GetDataTableBySql(command);
                if (dt.Rows.Count > 0)
                {
                    var balance = dt.Rows[0][0].ToString();
                    if (!string.IsNullOrWhiteSpace(balance))
                        this.lbBalance.Text = "余额:" + String.Format("{0:N2}", decimal.Parse(balance));
                }
                else
                {
                    this.lbBalance.Text = "";
                }
            }
        }

        private void cmbAlipayNo_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateBalace();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UpdateBalace();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            copyMoney();
        }

        private void copyMoney()
        {
            if (!string.IsNullOrEmpty(this.cmbAlipayNo.SelectedItem.ToString()))
            {
                string command = " select \"money\" from \"Value\" where \"value\"='" + this.cmbAlipayNo.SelectedItem.ToString() + "'";
                DataTable dt = sqlConn.GetDataTableBySql(command);
                if (dt.Rows.Count > 0)
                {
                    var balance = dt.Rows[0][0].ToString();
                    Clipboard.SetDataObject(balance);
                }
            }
        }
    }

    
}

