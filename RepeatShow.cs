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
    public partial class RepeatShow : Form
    {
        SqlConn conn = new SqlConn();
        public RepeatShow(List<string> isCheckItem)
        {
            InitializeComponent();
            foreach (Control c in Controls)
            {
                if (c is CheckBox)
                {
                    //var checkbox = (CheckBox)c;
                    c.BackColor = Color.Transparent;
                    if (isCheckItem.Where(v => v == c.Text).Count() > 0)
                    {
                        ((CheckBox)c).Checked = true;
                        //checkbox.Checked = true;
                    }
                    //listCheckBox.Add(c);
                }
            }
            //foreach (var item in isCheckItem)
            //{
            //    switch(item)
            //    {
                    
            //    }
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<string> selectItem = new List<string>();
            List<CheckBox> checkBoxList = new List<CheckBox>();
            foreach (Control c in Controls)
            {
                if (c is CheckBox)
                {
                    if (((CheckBox)c).Checked == true)
                    {
                        //selectItem.Add(c.Text);
                        checkBoxList.Add((CheckBox)c);
                    }
                }
            }
            if (checkBoxList.Count() > 0)
            {
                checkBoxList = checkBoxList.OrderBy(v => v.TabIndex).ToList();
                foreach (var checkbox in checkBoxList)
                {
                    selectItem.Add(checkbox.Text);
                }
            }
            if (selectItem.Count() > 0)
            {
                string command = string.Empty;
                command = "select * from \"Authorization\" where FunctionName='RepeatTipItem' ";
                DataTable dt = conn.GetDataTableBySql(command);
                string item = string.Join(",",selectItem);
                string error = string.Empty;
                if (dt.Rows.Count == 0)
                {
                    command = "insert into \"Authorization\"(FunctionName,AuthorizationUser) values('RepeatTipItem','" + item + "')";
                    error = conn.ExecuteSql(command);
                }
                else
                {
                    command = "update \"Authorization\" set AuthorizationUser='" + item + "' where FunctionName='RepeatTipItem'";
                    error = conn.ExecuteSql(command);
                }
                if (!string.IsNullOrWhiteSpace(error))
                    MessageBox.Show("保存失败,失败原因:" + error);
                else
                {
                    MessageBox.Show("保存成功!");
                    this.Hide();
                }
            }
        }

        private void setCheckBox(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                switch (text)
                {
                    case "店铺名称":
                        this.cbShopName.Checked = true;
                        break;
                    case "买家ID":
                        this.cbShopName.Checked = true;
                        break;
                    case "退款方式":
                        this.cbShopName.Checked = true;
                        break;
                    case "支付宝帐号":
                        this.cbShopName.Checked = true;
                        break;
                    case "退款金额":
                        this.cbShopName.Checked = true;
                        break;
                    case "退款原因":
                        this.cbShopName.Checked = true;
                        break;
                    case "录入人":
                        this.cbShopName.Checked = true;
                        break;
                    case "备注":
                        this.cbShopName.Checked = true;
                        break;
                    case "商品名称1":
                        this.cbShopName.Checked = true;
                        break;
                    case "商品名称2":
                        this.cbShopName.Checked = true;
                        break;
                    case "商品名称3":
                        this.cbShopName.Checked = true;
                        break;
                    case "商品名称4":
                        this.cbShopName.Checked = true;
                        break;
                    case "快递单号1":
                        this.cbShopName.Checked = true;
                        break;
                    case "快递单号2":
                        this.cbShopName.Checked = true;
                        break;
                    case "快递单号3":
                        this.cbShopName.Checked = true;
                        break;
                    case "录入时间":
                        this.cbCreatetime.Checked = true;
                        break;
                    case "退款时间":
                        this.cbRefundTime.Checked = true;
                        break;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
