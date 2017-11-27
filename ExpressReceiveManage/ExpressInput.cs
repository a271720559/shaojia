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
    public partial class ExpressInput : Form
    {
        private SqlConn conn = new SqlConn();
        private bool isAutoRe = false;
        public ExpressInput()
        {
            InitializeComponent();
            RoadResion();
            this.txtCreateUser.Text = UserEntry.Name;
            foreach (var control in this.Controls)
            {
                if (control is Label)
                {
                    var lb = control as Label;
                    lb.BackColor = Color.Transparent;
                }
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

        private void txtExpressNo_KeyDown(object sender, KeyEventArgs e)
        {
            //if()
            //RecongExpress();
        }

        private void txtExpressNo_Leave(object sender, EventArgs e)
        {
            RecongExpress();
        }
        /// <summary>
        /// 识别快递单号
        /// </summary>
        private void RecongExpress()
        {
            if (!string.IsNullOrWhiteSpace(this.txtExpressNo.Text)&&this.txtExpressNo.Text.Length>=5&&isAutoRe==false)
            {
                var six = this.txtExpressNo.Text.Substring(0, 5);
                string command = string.Empty;
                command = "select CompanyName from CompanySet where ExpressNo='" + six + "'";
                DataTable dt = conn.GetDataTableBySql(command);
                if (dt.Rows.Count > 0)
                {
                    var company = dt.Rows[0][0].ToString();
                    this.txtExpressCompany.Text = company;
                }
                else
                    this.txtExpressCompany.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string command = string.Empty;

            if (string.IsNullOrWhiteSpace(this.txtExpressNo.Text))
            {
                MessageBox.Show("请输入快递单号!");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtExpressCompany.Text))
            {
                MessageBox.Show("请输入快递公司!");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.cmbResion.Text))
            {
                MessageBox.Show("请输入原因!");
                return;
            }
            command = " insert into ExpressReceive(ExpressNo,ExpressCompany,Resion,Status,CreateUser,BuyAdress,IsDelete) values('"+this.txtExpressNo.Text+"','"+this.txtExpressCompany.Text+"','"+this.cmbResion.Text+"','未处理','"+this.txtCreateUser.Text+"','"+this.txtBuyAdress.Text+"','False') ";
            string message = conn.ExecuteSql(command);
            if (!string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("保存失败!失败原因:" + message);
            }
            else
            {
                MessageBox.Show("录入成功");
                this.txtExpressNo.Text = "";
                this.txtExpressCompany.Text = "";
                this.txtBuyAdress.Text = "";
                this.cmbResion.Text = "";
                this.isAutoRe = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txtBuyAdress.Text))
            {
                //string adress = string.Empty;
                //string num = string.Empty;
                //for (int x = 0; x < this.txtBuyAdress.Text.Length; x++)
                //{
                //    if ((int)this.txtBuyAdress.Text[x] > 127)
                //    {
                //        adress += this.txtBuyAdress.Text[x];
                //    }
                //    else
                //    {
                //        num += this.txtBuyAdress.Text[x];
                //    }
                //}
                //this.txtBuyAdress.Text = adress;
                //this.txtExpressNo.Text = num;

                var msList = this.txtBuyAdress.Text.Split(' ').ToList();
                if (msList.Count() == 3)
                {
                    this.txtBuyID.Text = msList[0];
                    this.txtExpressNo.Text = msList[1];
                    //this.txtBuyAdress.Text = msList[2];
                    this.txtBuyAdress.Text = this.txtBuyAdress.Text.Replace(msList[0], "").Replace(msList[1], "").TrimStart();
                    isAutoRe = true;
                }
            }
        }
    }
}
