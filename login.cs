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
    public partial class login : Form
    {
        SqlConn sqlConn = new SqlConn();
        public login()
        {
            InitializeComponent();
            this.label1.BackColor =  Color.Transparent;
            this.label2.BackColor = Color.Transparent;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Logining();
        }
        private void Logining()
        {
            string userName = this.txtUserName.Text;
            string password = this.txtPassword.Text;
            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("请输入用户名");
                return;
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("请输入密码");
                return;
            }
            else
            {
                string sql = " SELECT   \"UserName\",\"Password\",\"Name\",\"IsDelete\"  FROM  \"User\" where \"UserName\"='" + userName + "' ";
                DataTable dt = sqlConn.GetDataTableBySql(sql);
                if (dt.Rows.Count > 0)
                {
                    string dataUsername = dt.Rows[0][0].ToString();
                    string dataPassword = dt.Rows[0][1].ToString();
                    string name = dt.Rows[0][2].ToString();
                    if (userName != dataUsername || password != dataPassword)
                    {
                        MessageBox.Show("用户名或密码不正确");
                        return;
                    }
                    else
                    {
                        var delete = dt.Rows[0][3].ToString();
                        if (delete == "True")
                        {
                            MessageBox.Show("用户无效,请联系管理员!");
                            return;
                        }
                        //MessageBoxTimeout
                        //MessageBox.Show("登录成功");
                        UserEntry.UserName = userName;
                        UserEntry.Name = name;
                        Main main = new Main();
                        main.Show();
                        this.Hide();

                    }

                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            //this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtPassword.Text = "";
            this.txtUserName.Text = "";
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Logining();
            }
        }
    }
}
