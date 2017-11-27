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
    public partial class BalanceSet : Form
    {
        SqlConn conn = new SqlConn();
        public BalanceSet()
        {
            InitializeComponent();
            string command = " select \"id\" from \"Value\" where \"value\"='balance'";
            DataTable dt = conn.GetDataTableBySql(command);
            //var balance = dt.Rows[0][0].ToString();
            //if (!string.IsNullOrWhiteSpace(balance))
            //    this.textBox1.Text = balance;
            List<string> AliPayNoList = new List<string>();
            //查询支付宝帐号
            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='AlipayNo'";
            dt = conn.GetDataTableBySql(command);
            var AliPayNo = dt.Rows[0][0].ToString().Split(',').ToList();
            AliPayNoList.Add("");
            foreach (var item in AliPayNo)
            {
                AliPayNoList.Add(item);
            }
            this.cmbAilpayNo.DataSource = null;
            this.cmbAilpayNo.DataSource = AliPayNoList;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cmbAilpayNo.SelectedItem.ToString()))
            {
                string command = string.Empty;
                command = "update \"Value\" set money='" + this.textBox1.Text + "' where \"value\"='" + this.cmbAilpayNo.SelectedItem.ToString() + "'";
                string message = conn.ExecuteSql(command);
                if (!string.IsNullOrWhiteSpace(message))
                    MessageBox.Show("保存失败："+message);
                else
                    MessageBox.Show("保存成功");
            }
        }

        private void cmbAilpayNo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cmbAilpayNo.SelectedItem.ToString()))
            {
                string command = " select \"money\" from \"Value\" where \"value\"='" + this.cmbAilpayNo.SelectedItem.ToString() + "'";
                DataTable dt = conn.GetDataTableBySql(command);
                if (dt.Rows.Count > 0)
                {
                    var balance = dt.Rows[0][0].ToString();
                    if (!string.IsNullOrWhiteSpace(balance))
                        this.textBox1.Text = balance;
                }
                else
                {
                    this.textBox1.Text = "0";
                }
            }
        }
    }
}
