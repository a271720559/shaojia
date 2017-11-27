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
    public partial class Register : Form
    {
        SqlConn sqlConn = new SqlConn();
        public Register()
        {
            InitializeComponent();
            this.label1.BackColor = Color.Transparent;
            this.label2.BackColor = Color.Transparent;
            this.label3.BackColor = Color.Transparent;
            this.label4.BackColor = Color.Transparent;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var userName = this.txtUserName.Text;
            var password = this.txtPassword.Text;
            var password2 = this.tbPwd2.Text;
            var name = this.tbName.Text;
            if(string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("请输入用户名");
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("请输入密码");
                return;
            }
            if (string.IsNullOrWhiteSpace(password2))
            {
                MessageBox.Show("请输入确认");
                return;
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("请输入姓名");
                return;
            }
            if(password!=password2)
            {
                MessageBox.Show("两次密码输入不一致");
                return;
            }
            RegisterUser(userName,password,name);

        }
        private void RegisterUser(string userName,string password,string name)
        {
            string command = string.Empty;
            command = "select * from \"User\" where \"UserName\"='"+userName+"'";
            DataTable tb = sqlConn.GetDataTableBySql(command);
            string sql = string.Empty;
            sql = " select * from \"User\" where \"Name\"='"+name+"'";
            DataTable tName = sqlConn.GetDataTableBySql(sql);
            if (tb.Rows.Count > 0)
            {
                MessageBox.Show("用户名已经被注册");
                return;
            }
            else if (tName.Rows.Count > 0)
            {
                MessageBox.Show("姓名已经被注册");
                return;
            }
            else
            {
                command = "insert into \"User\"(\"UserName\",\"Password\",\"Name\",\"IsDelete\") values('" + userName + "','" + password + "','" + name + "',1)";
                string message = sqlConn.ExecuteSql(command);
                if (string.IsNullOrWhiteSpace(message))
                {
                    MessageBox.Show("注册成功");
                    this.Hide();
                }
                else
                    MessageBox.Show("注册失败,失败原因:" + message);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtPassword.Text = "";
            this.txtUserName.Text = "";
            this.tbPwd2.Text = "";
            this.tbName.Text = "";
        }
    }
}
