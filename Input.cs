using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace OrderManage
{
    public partial class Input : Form
    {
        private List<string> shopNameList = new List<string>();
        private List<string> refundWay = new List<string>();
        bool isShowTip = false;
        SqlConn conn = new SqlConn();
        List<RefundDetail> detailList = new List<RefundDetail>();
        public Input(List<string> ShopNameList,List<string> RefundWay)
        {
            InitializeComponent();
            
            foreach(var control in this.Controls)
            {
                if (control is Label)
                {
                    var lb = control as Label;
                    lb.BackColor = Color.Transparent;
                }
            }
            shopNameList = ShopNameList;
            refundWay = RefundWay;
            this.cmbShopName.DataSource = null;
            this.cmbShopName.DataSource = ShopNameList;
            this.cmbRefundResoin.DataSource = null;
            this.cmbRefundResoin.DataSource = RefundWay;
            List<string> refundWay1 = new List<string>();
            refundWay1.AddRange(RefundWay);
            this.cmbRefundResoin1.DataSource = null;
            this.cmbRefundResoin1.DataSource = refundWay1;
            List<string> refundWay2= new List<string>();
            refundWay2.AddRange(RefundWay);
            this.cmbRefundResoin2.DataSource = null;
            this.cmbRefundResoin2.DataSource = refundWay2;
            List<string> refundWay3 = new List<string>();
            refundWay3.AddRange(RefundWay);
            this.cmbRefundResoin3.DataSource = null;
            this.cmbRefundResoin3.DataSource = refundWay3;
            List<string> refundWay4 = new List<string>();
            refundWay4.AddRange(RefundWay);
            this.cmbRefundResoin4.DataSource = null;
            this.cmbRefundResoin4.DataSource = refundWay4;
            List<string> refundWay5 = new List<string>();
            refundWay5.AddRange(RefundWay);
            this.cmbRefundResoin5.DataSource = null;
            this.cmbRefundResoin5.DataSource = refundWay5;
            List<string> way = new List<string>();
            way.Add("");
            way.Add("支付宝");
            way.Add("订单退款");
            way.Add("代付");
            way.Add("微信退款");
            way.Add("补发");
            this.cmbRefundWay.DataSource = null;
            this.cmbRefundWay.DataSource = way;
            this.tbCreateUser.Text = UserEntry.Name;
            this.cmbShopName.Text = "大哥要啥";
            this.cmbRefundWay.Text = "支付宝";
            //this.dataGridView1.AutoGenerateColumns = false;
            //List<string> datagridWayList = new List<string>();
            //this.refundWay.ForEach(v => datagridWayList.Add(v));
            //DataGridViewComboBoxColumn bb = new DataGridViewComboBoxColumn();
            //bb.DataSource = datagridWayList;
            //bb.HeaderText = "退款原因";
            //bb.DataPropertyName = "RefundResion";
            //this.dataGridView1.Columns.Add(bb);
            //this.dataGridView1.Rows.Add();
            //this.dataGridView1.Rows.Add();
            //this.dataGridView1.Rows.Add();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string command = string.Empty;
            if (isShowTip == false)
            {
                if (!string.IsNullOrWhiteSpace(this.tbBuyerID.Text))
                {
                    command = string.Empty;
                    command = "select * from \"OrderRecord\" where \"BuyersID\" like '%" + this.tbBuyerID.Text.Trim() + "%' and \"IsDelete\"='False' order by convert(datetime,CreateTime) desc";
                    DataTable dt = conn.GetDataTableBySql(command);
                    if (dt.Rows.Count > 0)
                    {
                        DateTime createTime = DateTime.Parse(dt.Rows[0]["CreateTime"].ToString());
                        var time = DateTime.Now - createTime;
                        if (time.TotalDays <= 30)
                        {
                            if (MessageBox.Show("    买家ID已存在退款记录,请核对!是否继续保存?", "提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                
                            }
                            else
                                return;
                            //if(MessageBox.Show("买家ID已存在退款记录,请核对!", "提示"));
                            //return;
                        }
                    }
                }
            }
            
            if(string.IsNullOrWhiteSpace(this.cmbShopName.Text))
            {
                MessageBox.Show("请输入店铺名称!");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.tbBuyerID.Text))
            {
                MessageBox.Show("请输入买家ID!");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.cmbRefundWay.Text))
            {
                MessageBox.Show("请输入退款方式!");
                return;
            }
            if (this.cmbRefundWay.Text == "支付宝")
            {
                if (string.IsNullOrWhiteSpace(this.tbAlipayNo.Text))
                {
                    MessageBox.Show("请输入支付宝帐号!");
                    return;
                }
                else if (this.tbAlipayNo.Text.Length < 5)
                {
                    MessageBox.Show("支付宝账号应大于5个字符");
                    return;
                }
            }
            
            

            int id = 0;
            command = "select max(id) from OrderRecord ";
            DataTable dt1 = conn.GetDataTableBySql(command);
            if (dt1.Rows.Count > 0)
                id = int.Parse(dt1.Rows[0][0].ToString());
            id++;

            string refundResion = string.Empty;
            decimal amount1 = 0;
            decimal refundAmount = 0;
            detailList = new List<RefundDetail>();
            RefundDetail entry = new RefundDetail();
            entry.orderId = id;
            if (!string.IsNullOrWhiteSpace(this.tbRefundAmount.Text))
                entry.RefundAmount = decimal.Parse(this.tbRefundAmount.Text);
            entry.RefundResoin = this.cmbRefundResoin.Text;
            entry.code = this.txtCode.Text;
            detailList.Add(entry);
            refundAmount += entry.RefundAmount;
            refundResion = entry.RefundResoin + "     " + entry.RefundAmount + "     "+entry.code + Environment.NewLine;

            #region 退款原因赋值
            if (!string.IsNullOrWhiteSpace(this.tbCode1.Text) || !string.IsNullOrWhiteSpace(this.tbRefundAmount1.Text) || !string.IsNullOrWhiteSpace(this.cmbRefundResoin1.Text))
            {
                RefundDetail entry1 = new RefundDetail();
                entry1.orderId = id;
                if (!string.IsNullOrWhiteSpace(this.tbRefundAmount1.Text))
                    entry1.RefundAmount = decimal.Parse(this.tbRefundAmount1.Text);
                amount1 = entry1.RefundAmount;
                entry1.RefundResoin = this.cmbRefundResoin1.Text;
                entry1.code = this.tbCode1.Text;
                refundAmount += entry1.RefundAmount;
                refundResion += entry1.RefundResoin + "     " + amount1 + "     " + entry1.code + Environment.NewLine;
                detailList.Add(entry1);
            }
            if (!string.IsNullOrWhiteSpace(this.tbCode2.Text) || !string.IsNullOrWhiteSpace(this.tbRefundAmount2.Text) || !string.IsNullOrWhiteSpace(this.cmbRefundResoin2.Text))
            {
                RefundDetail entry1 = new RefundDetail();
                entry1.orderId = id;
                if (!string.IsNullOrWhiteSpace(this.tbRefundAmount2.Text))
                    entry1.RefundAmount = decimal.Parse(this.tbRefundAmount2.Text);
                amount1 = entry1.RefundAmount;
                entry1.RefundResoin = this.cmbRefundResoin2.Text;
                entry1.code = this.tbCode2.Text;
                refundAmount += entry1.RefundAmount;
                refundResion += entry1.RefundResoin + "     " + amount1 + "     " + entry1.code + Environment.NewLine;
                detailList.Add(entry1);
            }
            if (!string.IsNullOrWhiteSpace(this.tbCode3.Text) || !string.IsNullOrWhiteSpace(this.tbRefundAmount3.Text) || !string.IsNullOrWhiteSpace(this.cmbRefundResoin3.Text))
            {
                RefundDetail entry1 = new RefundDetail();
                entry1.orderId = id;
                if (!string.IsNullOrWhiteSpace(this.tbRefundAmount3.Text))
                    entry1.RefundAmount = decimal.Parse(this.tbRefundAmount3.Text);
                amount1 = entry1.RefundAmount;
                entry1.RefundResoin = this.cmbRefundResoin3.Text;
                entry1.code = this.tbCode3.Text;
                refundAmount += entry1.RefundAmount;
                refundResion += entry1.RefundResoin + "     " + amount1 + "     " + entry1.code + Environment.NewLine;
                detailList.Add(entry1);
            }
            if (!string.IsNullOrWhiteSpace(this.tbCode4.Text) || !string.IsNullOrWhiteSpace(this.tbRefundAmount4.Text) || !string.IsNullOrWhiteSpace(this.cmbRefundResoin4.Text))
            {
                RefundDetail entry1 = new RefundDetail();
                entry1.orderId = id;
                if (!string.IsNullOrWhiteSpace(this.tbRefundAmount4.Text))
                    entry1.RefundAmount = decimal.Parse(this.tbRefundAmount4.Text);
                amount1 = entry1.RefundAmount;
                entry1.RefundResoin = this.cmbRefundResoin4.Text;
                entry1.code = this.tbCode4.Text;
                refundAmount += entry1.RefundAmount;
                refundResion += entry1.RefundResoin + "     " + amount1 + "     " + entry1.code + Environment.NewLine;
                detailList.Add(entry1);
            }
            if (!string.IsNullOrWhiteSpace(this.tbCode5.Text) || !string.IsNullOrWhiteSpace(this.tbRefundAmount5.Text) || !string.IsNullOrWhiteSpace(this.cmbRefundResoin5.Text))
            {
                RefundDetail entry1 = new RefundDetail();
                entry1.orderId = id;
                if (!string.IsNullOrWhiteSpace(this.tbRefundAmount5.Text))
                    entry1.RefundAmount = decimal.Parse(this.tbRefundAmount5.Text);
                amount1 = entry1.RefundAmount;
                entry1.RefundResoin = this.cmbRefundResoin5.Text;
                entry1.code = this.tbCode5.Text;
                refundAmount += entry1.RefundAmount;
                refundResion += entry1.RefundResoin + "     " + amount1 + "     " + entry1.code + Environment.NewLine;
                detailList.Add(entry1);
            }
            #endregion
            //if (this.dataGridView1.Rows.Count > 0)
            //{
            //    foreach (var item in this.dataGridView1.Rows)
            //    {
            //        var gridRow = item as DataGridViewRow;
            //        //var row = gridRow.DataBoundItem as RefundDetail;
            //        if (gridRow.Cells[0].Value != null)
            //        {
            //            if (!string.IsNullOrWhiteSpace(gridRow.Cells[0].Value.ToString()))
            //                amount1 = decimal.Parse(gridRow.Cells[0].Value.ToString());
            //            refundAmount += amount1;
            //            refundResion += gridRow.Cells[2].Value + "     " + amount1 + "     " + gridRow.Cells[1].Value + Environment.NewLine;
            //            RefundDetail entry1 = new RefundDetail();
            //            entry1.orderId = id;
            //            entry1.RefundAmount = amount1;
            //            if (gridRow.Cells[2].Value!=null)
            //            entry1.RefundResoin = gridRow.Cells[2].Value.ToString();
            //            if (gridRow.Cells[1].Value != null)
            //                entry1.code = gridRow.Cells[1].Value.ToString();
            //            detailList.Add(entry1);
            //        }
            //    }

            //}
            //else
            //{
            //    refundAmount = decimal.Parse(this.tbRefundAmount.Text);
            //    refundResion = this.cmbRefundResoin.Text;
            //}
            if (string.IsNullOrWhiteSpace(this.tbRefundAmount.Text) && detailList.Where(v => v.RefundAmount > 0).Count() == 0 && this.cmbRefundWay.Text != "补发")
            {
                MessageBox.Show("请输入退款金额!");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.cmbRefundResoin.Text) && detailList.Where(v =>!string.IsNullOrWhiteSpace(v.RefundResoin)).Count() == 0)
            {
                MessageBox.Show("请输入退款原因!");
                return;
            }
            //decimal amount = decimal.Parse(decimal.Parse(this.tbRefundAmount.Text).ToString("F2"));
            decimal amount = 0;
            amount = decimal.Parse(refundAmount.ToString("F2"));
            string status = string.Empty;
            if ((this.cmbRefundWay.Text == "支付宝" && amount >= 15) || (this.cmbRefundResoin.Text == "漏发" && amount > 10))
            {
                status = "待审核";
            }
            else
                status = "未处理";

            //处理录入次数
            int inputCount = this.QueryInputCount(this.tbBuyerID.Text);

            command = "set IDENTITY_INSERT OrderRecord ON ;";
            command += " insert into \"OrderRecord\"(\"id\",\"Status\",\"ShopName\",\"BuyersID\",\"RefundWay\",\"AlipayNo\",\"RefundAmount\",\"RefundResoin\",\"Remark\",\"CreateUser\",\"IsDelete\",\"PDName1\",\"PDName2\",\"PDName3\",\"PDName4\",\"PDName5\",\"ExpressNo2\",\"ExpressNo3\",InputCount) Values(" + id + ",'" + status + "','" + this.cmbShopName.Text + "','" + this.tbBuyerID.Text + "','" + this.cmbRefundWay.Text + "','" + this.tbAlipayNo.Text + "','" + amount + "','" + refundResion + "','" + this.tbRemark.Text + "','" + this.tbCreateUser.Text + "','false','" + this.tbPdName1.Text + "','" + this.tbPdName2.Text + "','" + this.tbPdName3.Text + "','" + this.tbPdName4.Text + "','" + this.tbExpressNo1.Text + "','" + this.tbExpressNo2.Text + "','" + this.tbExpressNo3.Text + "',"+inputCount+") ;";
            command += "set IDENTITY_INSERT OrderRecord off;";
            string message = conn.ExecuteSql(command);
            if (!string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("保存失败!失败原因:" + message);
            }
            else
            {
                MessageBox.Show("录入成功");
                if (detailList.Count() > 0)
                {
                    foreach(var item in detailList)
                    {
                        command = "insert into RefundDetail(orderId,RefundAmount,RefundResoin,code) values('" + item.orderId + "','" + item.RefundAmount + "','" + item.RefundResoin + "','" + item.code + "')";
                        message = conn.ExecuteSql(command);
                    }
                }

                //this.dataGridView1.DataSource = null;
                //foreach (var item in this.dataGridView1.Rows)
                //{
                //    var gridRow = item as DataGridViewRow;
                //    this.dataGridView1.Rows.Remove(gridRow);
                //}
                this.tbBuyerID.Text = "";
                this.tbAlipayNo.Text = "";
                this.tbRefundAmount.Text = "";
                //this.cmbRefundWay.Text = "";
                this.cmbRefundResoin.Text="";
                this.tbCreateUser.Text = UserEntry.Name;
                this.tbRemark.Text = "";
                this.tbPdName1.Text = "";
                this.tbPdName2.Text = "";
                this.tbPdName3.Text = "";
                this.tbPdName4.Text = "";
                this.tbExpressNo1.Text = "";
                this.tbExpressNo2.Text = "";
                this.tbExpressNo3.Text = "";
                this.tbUp.Text = "";

                this.tbCode1.Text = "";
                this.tbCode2.Text = "";
                this.tbCode3.Text = "";
                this.tbCode4.Text = "";
                this.tbCode5.Text = "";
                this.tbRefundAmount1.Text = "";
                this.tbRefundAmount2.Text = "";
                this.tbRefundAmount3.Text = "";
                this.tbRefundAmount4.Text = "";
                this.tbRefundAmount5.Text = "";
                this.cmbRefundResoin1.SelectedIndex = 0;
                this.cmbRefundResoin2.SelectedIndex = 0;
                this.cmbRefundResoin3.SelectedIndex = 0;
                this.cmbRefundResoin4.SelectedIndex = 0;
                this.cmbRefundResoin5.SelectedIndex = 0;
                //this.dataGridView1.DataSource = null;
                //while (this.dataGridView1.Rows.Count > 0)
                //{
                //    this.dataGridView1.Rows.RemoveAt(0);
                //}
                ////this.dataGridView1.i
                //this.dataGridView1.Rows.Add();
                //this.dataGridView1.Rows.Add();
                //this.dataGridView1.Rows.Add();
                //this.dataGridView1.Rows.Add();
                //this.dataGridView1.Rows.Add();
                //Input input = new Input(shopNameList, refundWay);
                //this.Hide();
                //input.Show();
                //this.Hide();
            }
        }

        private int QueryInputCount(string buyID)
        {
            int count = 1;
            string command = string.Empty;
            DateTime now = DateTime.Now;
            DateTime lastMonth = now.AddMonths(-1);
            command = "select count(*) from OrderRecord where BuyersID='" + buyID + "' and convert(datetime,CreateTime) >='" + lastMonth.ToString("yyyy-MM-dd HH:mm:ss") + "' and IsDelete='false'";
            DataTable dt = conn.GetDataTableBySql(command);
            if (dt.Rows.Count > 0)
            {
                count += int.Parse(dt.Rows[0][0].ToString());
            }
            return count;
        }
        
        private void tbAlipayNo_Leave(object sender, EventArgs e)
        {
            QueryAlipay();
        }
        private void QueryAlipay()
        {
            if (!string.IsNullOrWhiteSpace(this.tbAlipayNo.Text) && this.tbAlipayNo.Text != "拒绝")
            {
                string command = string.Empty;
                command = "select * from \"OrderRecord\" where \"AlipayNo\" like '%" + this.tbAlipayNo.Text + "%' and \"IsDelete\"='False'  and \"RefundResoin\" not  like '%拒绝%' order by \"CreateTime\" desc";
                DataTable dt = conn.GetDataTableBySql(command);
                if (dt.Rows.Count > 0)
                {
                    string showText = string.Empty;
                    for (int i = 0; i < dt.Rows.Count;i++ )
                    {
                        DateTime createTime = DateTime.Parse(dt.Rows[0]["CreateTime"].ToString());
                        var time = DateTime.Now - createTime;
                        if (time.TotalDays <= 30)
                        {
                            command = "select AuthorizationUser from \"Authorization\" where FunctionName='RepeatTipItem'";
                            DataTable dt1 = conn.GetDataTableBySql(command);
                            List<string> selectItem = new List<string>();
                            if (dt1.Rows.Count > 0)
                            {
                                selectItem = dt1.Rows[0][0].ToString().Split(',').ToList();
                            }
                            foreach (var item in selectItem)
                            {
                                var text = this.query(item, dt, i);
                                showText += item + " : " + text + Environment.NewLine;
                            }
                            isShowTip = true;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(showText))
                    {
                        MessageBox.Show("支付宝帐号已存在退款记录,请核对!", "提示");
                        ShowRepeatItem showItem = new ShowRepeatItem(showText);
                        showItem.Show();
                    }
                }
            
            }
        }
        private void tbBuyerID_Leave(object sender, EventArgs e)
        {
            QueryBuyID();
        }
        private void QueryBuyID()
        {
            if (!string.IsNullOrWhiteSpace(this.tbBuyerID.Text) && this.cmbRefundResoin.Text != "拒绝")
            {
                string command = string.Empty;
                command = "select * from \"OrderRecord\" where \"BuyersID\" like '%" + this.tbBuyerID.Text.Trim() + "%' and \"IsDelete\"='False' and \"RefundResoin\" not like '%拒绝%' order by \"CreateTime\" desc";
                DataTable dt = conn.GetDataTableBySql(command);
                if (dt.Rows.Count > 0)
                {
                    string showText = string.Empty;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DateTime createTime = DateTime.Parse(dt.Rows[i]["CreateTime"].ToString());
                        var time = DateTime.Now - createTime;
                        if (time.TotalDays <= 30)
                        {

                            //if (MessageBox.Show("买家ID已存在退款记录,请核对!,是否显示记录信息", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            //{

                            command = "select AuthorizationUser from \"Authorization\" where FunctionName='RepeatTipItem'";
                            DataTable dt1 = conn.GetDataTableBySql(command);
                            List<string> selectItem = new List<string>();
                            if (dt1.Rows.Count > 0)
                            {
                                selectItem = dt1.Rows[0][0].ToString().Split(',').ToList();
                            }
                            foreach (var item in selectItem)
                            {
                                var text = this.query(item, dt, i);
                                showText += item + " : " + text + Environment.NewLine;
                            }
                            isShowTip = true;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(showText))
                    {
                        MessageBox.Show("买家ID已存在退款记录,请核对!", "提示");
                        ShowRepeatItem showItem = new ShowRepeatItem(showText);
                        showItem.Show();
                    }
                }
            }
        }
        private void cmbRefundWay_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.cmbRefundWay.SelectedValue.ToString()))
            {
                var selectValue = this.cmbRefundWay.SelectedValue.ToString();
                if (selectValue == "订单退款" || selectValue == "代付")
                    tbAlipayNo.ReadOnly = true;
                else
                    tbAlipayNo.ReadOnly = false;
            }
        }

        private string query(string item,DataTable dt,int i)
        {
            string value = string.Empty;
            if (!string.IsNullOrWhiteSpace(item))
            {
                switch (item)
                {
                    case "店铺名称":
                        value = dt.Rows[i]["ShopName"].ToString();
                        break;
                    case "买家ID":
                        value = dt.Rows[i]["BuyersID"].ToString();
                        break;
                    case "退款方式":
                        value = dt.Rows[i]["RefundWay"].ToString();
                        break;
                    case "支付宝帐号":
                        value = dt.Rows[i]["AlipayNo"].ToString();
                        break;
                    case "退款金额":
                        //value = dt.Rows[i]["RefundAmount"].ToString();
                        value = this.QueryRefundDetailAmount(int.Parse(dt.Rows[i]["ID"].ToString()));
                        break;
                    case "退款原因":
                        //value = dt.Rows[i]["RefundResoin"].ToString();
                        value = this.QueryRefundDetailResion(int.Parse(dt.Rows[i]["ID"].ToString()));
                        break;
                    case "商品条码":
                        //value = dt.Rows[i]["RefundResoin"].ToString();
                        value = this.QueryRefundDetailCode(int.Parse(dt.Rows[i]["ID"].ToString()));
                        break;
                    case "录入人":
                        value = dt.Rows[i]["CreateUser"].ToString();
                        break;
                    case "备注":
                        value = dt.Rows[i]["Remark"].ToString();
                        break;
                    case "商品名称1":
                        value = dt.Rows[i]["PDName1"].ToString();
                        break;
                    case "商品名称2":
                        value = dt.Rows[i]["PDName2"].ToString();
                        break;
                    case "商品名称3":
                        value = dt.Rows[i]["PDName3"].ToString();
                        break;
                    case "商品名称4":
                        value = dt.Rows[i]["PDName4"].ToString();
                        break;
                    case "快递单号1":
                        value = dt.Rows[i]["PDName5"].ToString();
                        break;
                    case "快递单号2":
                        value = dt.Rows[i]["ExpressNo2"].ToString();
                        break;
                    case "快递单号3":
                        value = dt.Rows[i]["ExpressNo3"].ToString();
                        break;
                    case "退款时间":
                        value = dt.Rows[i]["RefundTime"].ToString();
                        break;
                    case "录入时间":
                        value = dt.Rows[i]["CreateTime"].ToString();
                        break;
                }
            }
            value = value.Replace(Environment.NewLine, "");

            return value;
        }
        public List<RefundDetail> QueryDetail(int? orderID)
        {
            List<RefundDetail> result = new List<RefundDetail>();
            string command = string.Empty;
            command = "select RefundAmount,RefundResoin,code from RefundDetail where orderId ='" + orderID + "'";
            DataTable dt = conn.GetDataTableBySql(command);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RefundDetail entry = new RefundDetail();
                    entry.RefundAmount = decimal.Parse(dt.Rows[i][0].ToString());
                    entry.RefundResoin = dt.Rows[i][1].ToString();
                    entry.code = dt.Rows[i][2].ToString();
                    result.Add(entry);
                }
            }
            return result;
        }
        private string QueryRefundDetailAmount(int orderRecordID)
        {
            string result = string.Empty;
            var detailList = this.QueryDetail(orderRecordID);
            foreach(var item in detailList)
            {
                result += item.RefundAmount + Environment.NewLine;
            }
            return result;
        }

        private string QueryRefundDetailCode(int orderRecordID)
        {
            string result = string.Empty;
            var detailList = this.QueryDetail(orderRecordID);
            foreach (var item in detailList)
            {
                result += item.code + Environment.NewLine;
            }
            return result;
        }

        private string QueryRefundDetailResion(int orderRecordID)
        {
            string result = string.Empty;
            var detailList = this.QueryDetail(orderRecordID);
            foreach (var item in detailList)
            {
                result += item.RefundResoin + Environment.NewLine;
            }
            return result;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

            //RefundDetail entry = new RefundDetail();
            //detailList.Add(entry);
            //this.dataGridView1.DataSource = detailList;
            this.dataGridView1.Rows.Add();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                foreach (var selectRow in this.dataGridView1.SelectedRows)
                {
                    var gridRow = selectRow as DataGridViewRow;
                    this.dataGridView1.Rows.Remove(gridRow);
                }
            }
        }


        private void btnUp_Click(object sender, EventArgs e)
        {
            this.ClearInput();
            if (this.cmbRefundWay.Text == "支付宝")
            {
                var upList = this.tbUp.Text.Split(' ').Where(v => !string.IsNullOrWhiteSpace(v)).ToList();
                if (upList.Count() > 3)
                {
                    if (this.isExists(upList[3]) == false)
                    {
                        for (int i = 0; i < upList.Count(); i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    this.tbBuyerID.Text = upList[0];
                                    break;
                                case 1:
                                    this.tbAlipayNo.Text = upList[1];
                                    break;
                                case 2:
                                    this.tbRefundAmount.Text = upList[2];
                                    break;
                                case 3:
                                    this.tbExpressNo1.Text = upList[3];
                                    break;
                                case 4:
                                    this.cmbRefundResoin.Text = upList[4];
                                    break;
                                case 5:
                                    this.tbExpressNo2.Text = upList[5];
                                    break;
                                case 6:
                                    this.tbExpressNo3.Text = upList[6];
                                    break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < upList.Count(); i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    this.tbBuyerID.Text = upList[0];
                                    break;
                                case 1:
                                    this.tbAlipayNo.Text = upList[1];
                                    break;
                                case 2:
                                    this.tbRefundAmount1.Text = upList[2];
                                    break;
                                case 3:
                                    this.tbCode1.Text = upList[3];
                                    break;
                                case 4:
                                    this.cmbRefundResoin1.Text = upList[4];
                                    break;
                                case 5:
                                    this.tbRefundAmount2.Text = upList[5];
                                    break;
                                case 6:
                                    this.tbCode2.Text = upList[6];
                                    break;
                                case 7:
                                    this.cmbRefundResoin2.Text = upList[7];
                                    break;
                                case 8:
                                    this.tbRefundAmount3.Text = upList[8];
                                    break;
                                case 9:
                                    this.tbCode3.Text = upList[9];
                                    break;
                                case 10:
                                    this.cmbRefundResoin3.Text = upList[10];
                                    break;
                                case 11:
                                    this.tbRefundAmount4.Text = upList[11];
                                    break;
                                case 12:
                                    this.tbCode4.Text = upList[12];
                                    break;
                                case 13:
                                    this.cmbRefundResoin4.Text = upList[13];
                                    break;
                                case 14:
                                    this.tbRefundAmount5.Text = upList[14];
                                    break;
                                case 15:
                                    this.tbCode5.Text = upList[15];
                                    break;
                                case 16:
                                    this.cmbRefundResoin5.Text = upList[16];
                                    break;
                            }
                        }
                    }
                }
                
            }
            else if (this.cmbRefundWay.Text == "订单退款")
            {
                var upList = this.tbUp.Text.Split(' ').Where(v => !string.IsNullOrWhiteSpace(v)).ToList();
                if (upList.Count() > 2)
                {
                    if (this.isExists(upList[2]) == false)
                    {
                        for (int i = 0; i < upList.Count(); i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    this.tbBuyerID.Text = upList[0];
                                    break;
                                case 1:
                                    this.tbRefundAmount.Text = upList[1];
                                    break;
                                case 2:
                                    this.tbExpressNo1.Text = upList[2];
                                    break;
                                case 3:
                                    this.cmbRefundResoin.Text = upList[3];
                                    break;
                                case 4:
                                    this.tbExpressNo2.Text = upList[4];
                                    break;
                                case 5:
                                    this.tbExpressNo3.Text = upList[5];
                                    break;

                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < upList.Count(); i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    this.tbBuyerID.Text = upList[0];
                                    break;
                                case 1:
                                    this.tbRefundAmount1.Text = upList[1];
                                    break;
                                case 2:
                                    this.tbCode1.Text = upList[2];
                                    break;
                                case 3:
                                    this.cmbRefundResoin1.Text = upList[3];
                                    break;
                                case 4:
                                    this.tbRefundAmount2.Text = upList[4];
                                    break;
                                case 5:
                                    this.tbCode2.Text = upList[5];
                                    break;
                                case 6:
                                    this.cmbRefundResoin2.Text = upList[6];
                                    break;
                                case 7:
                                    this.tbRefundAmount3.Text = upList[7];
                                    break;
                                case 8:
                                    this.tbCode3.Text = upList[8];
                                    break;
                                case 9:
                                    this.cmbRefundResoin3.Text = upList[9];
                                    break;
                                case 10:
                                    this.tbRefundAmount4.Text = upList[10];
                                    break;
                                case 11:
                                    this.tbCode4.Text = upList[11];
                                    break;
                                case 12:
                                    this.cmbRefundResoin4.Text = upList[12];
                                    break;
                                case 13:
                                    this.tbRefundAmount5.Text = upList[13];
                                    break;
                                case 14:
                                    this.tbCode5.Text = upList[14];
                                    break;
                                case 15:
                                    this.cmbRefundResoin5.Text = upList[15];
                                    break;
                            }
                        }
                    }
                }
                
            }
            this.QueryBuyID();
            this.QueryAlipay();
        }
        public bool isExists(string str)
        {
            return Regex.Matches(str, "[a-zA-Z]").Count > 0;
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            if (this.cmbRefundWay.Text == "支付宝")
            {
                var upList = this.tbDown.Text.Split(' ').Where(v => !string.IsNullOrWhiteSpace(v)).ToList();
                for (int i = 0; i < upList.Count(); i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.tbBuyerID.Text = upList[0];
                            break;
                        case 1:
                            this.tbAlipayNo.Text = upList[1];
                            break;
                        case 2:
                            this.tbRefundAmount1.Text = upList[2];
                            break;
                        case 3:
                            this.tbCode1.Text = upList[3];
                            break;
                        case 4:
                            this.cmbRefundResoin1.Text = upList[4];
                            break;
                        case 5:
                            this.tbRefundAmount2.Text = upList[5];
                            break;
                        case 6:
                            this.tbCode2.Text = upList[6];
                            break;
                        case 7:
                            this.cmbRefundResoin2.Text = upList[7];
                            break;
                        case 8:
                            this.tbRefundAmount3.Text = upList[8];
                            break;
                        case 9:
                            this.tbCode3.Text = upList[9];
                            break;
                        case 10:
                            this.cmbRefundResoin3.Text = upList[10];
                            break;
                        case 11:
                            this.tbRefundAmount4.Text = upList[11];
                            break;
                        case 12:
                            this.tbCode4.Text = upList[12];
                            break;
                        case 13:
                            this.cmbRefundResoin4.Text = upList[13];
                            break;
                        case 14:
                            this.tbRefundAmount5.Text = upList[14];
                            break;
                        case 15:
                            this.tbCode5.Text = upList[15];
                            break;
                        case 16:
                            this.cmbRefundResoin5.Text = upList[16];
                            break;
                    }
                }
            }
            else if (this.cmbRefundWay.Text == "订单退款")
            {
                var upList = this.tbDown.Text.Split(' ').Where(v=>!string.IsNullOrWhiteSpace(v)).ToList();
                for (int i = 0; i < upList.Count(); i++)
                {
                    switch (i)
                    {
                        case 0:
                            this.tbBuyerID.Text = upList[0];
                            break;
                        case 1:
                            this.tbRefundAmount1.Text = upList[1];
                            break;
                        case 2:
                            this.tbCode1.Text = upList[2];
                            break;
                        case 3:
                            this.cmbRefundResoin1.Text = upList[3];
                            break;
                        case 4:
                            this.tbRefundAmount2.Text = upList[4];
                            break;
                        case 5:
                            this.tbCode2.Text = upList[5];
                            break;
                        case 6:
                            this.cmbRefundResoin2.Text = upList[6];
                            break;
                        case 7:
                            this.tbRefundAmount3.Text = upList[7];
                            break;
                        case 8:
                            this.tbCode3.Text = upList[8];
                            break;
                        case 9:
                            this.cmbRefundResoin3.Text = upList[9];
                            break;
                        case 10:
                            this.tbRefundAmount4.Text = upList[10];
                            break;
                        case 11:
                            this.tbCode4.Text = upList[11];
                            break;
                        case 12:
                            this.cmbRefundResoin4.Text = upList[12];
                            break;
                        case 13:
                            this.tbRefundAmount5.Text = upList[13];
                            break;
                        case 14:
                            this.tbCode5.Text = upList[14];
                            break;
                        case 15:
                            this.cmbRefundResoin5.Text = upList[15];
                            break;
                    }
                }
            }
            this.QueryBuyID();
            this.QueryAlipay();
        }
        public void ClearInput()
        {
            this.tbBuyerID.Text = "";
            this.tbAlipayNo.Text = "";
            this.tbRefundAmount.Text = "";
            //this.cmbRefundWay.Text = "";
            this.cmbRefundResoin.Text = "";
            this.tbCreateUser.Text = UserEntry.Name;
            this.tbRemark.Text = "";
            this.tbPdName1.Text = "";
            this.tbPdName2.Text = "";
            this.tbPdName3.Text = "";
            this.tbPdName4.Text = "";
            this.tbExpressNo1.Text = "";
            this.tbExpressNo2.Text = "";
            this.tbExpressNo3.Text = "";
            //this.tbUp.Text = "";
            this.tbCode1.Text = "";
            this.tbCode2.Text = "";
            this.tbCode3.Text = "";
            this.tbCode4.Text = "";
            this.tbCode5.Text = "";
            this.tbRefundAmount1.Text = "";
            this.tbRefundAmount2.Text = "";
            this.tbRefundAmount3.Text = "";
            this.tbRefundAmount4.Text = "";
            this.tbRefundAmount5.Text = "";
            this.cmbRefundResoin1.SelectedIndex = 0;
            this.cmbRefundResoin2.SelectedIndex = 0;
            this.cmbRefundResoin3.SelectedIndex = 0;
            this.cmbRefundResoin4.SelectedIndex = 0;
            this.cmbRefundResoin5.SelectedIndex = 0;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
        }
    }
}
