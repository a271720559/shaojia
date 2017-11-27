using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OrderManage
{
    public partial class UpdatePicPathUI : Form
    {
        public string PicPath = string.Empty;
        public bool IsUpdate = false;
        SqlConn sqlConn = new SqlConn();
        public UpdatePicPathUI(string picPath)
        {
            InitializeComponent();
            this.label1.BackColor = Color.Transparent;
            this.StartPosition = FormStartPosition.CenterParent;
            //PicPath = picPath;
            this.txtInput.Text = picPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.txtInput.Text))
            {
                if (Directory.Exists(this.txtInput.Text) == true)
                {
                    string command = string.Empty;
                    command = "update \"Value\" set \"value\"='" + this.txtInput.Text + "' where \"id\"='999'";
                    string message = sqlConn.ExecuteSql(command);
                    if (!string.IsNullOrWhiteSpace(message))
                        MessageBox.Show("操作失败,失败原因:" + message);
                    else
                    {
                        this.PicPath = this.txtInput.Text;
                        IsUpdate = true;
                        MessageBox.Show("保存成功");
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("输入的路径不存在,请检查!");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
