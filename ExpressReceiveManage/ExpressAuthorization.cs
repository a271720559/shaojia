using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OrderManage.ExpressReceiveManage
{
    public partial class ExpressAuthorization : Form
    {
        SqlConn conn = new SqlConn();
        private List<string> DealUserList = new List<string>();
        private List<string> FinishUserList = new List<string>();
        public ExpressAuthorization()
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
            string command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='ExpressDeal'";
            DealUserList = new List<string>();
            DataTable dt = conn.GetDataTableBySql(command);
            var DealUser = dt.Rows[0][0].ToString().Split(',').ToList();
            foreach (var item in DealUser)
            {
                DealUserList.Add(item);
            }
            this.txtDeal.Text = string.Join(",", DealUserList);

            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='ExpressFinish'";
            FinishUserList = new List<string>();
            dt = conn.GetDataTableBySql(command);
            var FinishUser = dt.Rows[0][0].ToString().Split(',').ToList();
            foreach (var item in FinishUser)
            {
                FinishUserList.Add(item);
            }
            this.txtFinish.Text = string.Join(",", FinishUserList);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string command = string.Empty;
            command = "update \"Authorization\" set \"AuthorizationUser\"='" + this.txtDeal.Text + "' where \"FunctionName\"='ExpressDeal'";
            conn.ExecuteSql(command);
            command = "update \"Authorization\" set \"AuthorizationUser\"='" + this.txtFinish.Text + "' where \"FunctionName\"='ExpressFinish'";
            conn.ExecuteSql(command);
            MessageBox.Show("保存成功!");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
