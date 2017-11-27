using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OrderManage
{
    public partial class AuthorizationSet : Form
    {
        SqlConn conn = new SqlConn();
        private List<string> AliPayNoList = new List<string>();
        private List<string> PicPathUserList = new List<string>();
        private List<string> AuditUserList = new List<string>();
        private List<string> PictureUserList = new List<string>();
        public AuthorizationSet(List<string> InputUser, List<string> DealUser, List<string> ResionList, List<string> ShopNameList, List<string> DeleteList)
        {
            InitializeComponent();
            ResionList.Remove("");
            ShopNameList.Remove("");
            this.label1.BackColor = Color.Transparent;
            this.label2.BackColor = Color.Transparent;
            this.label3.BackColor = Color.Transparent;
            this.label4.BackColor = Color.Transparent;
            this.label5.BackColor = Color.Transparent;
            this.label6.BackColor = Color.Transparent;
            this.txtInput.Text = string.Join(",",InputUser);
            this.txtDeal.Text = string.Join(",", DealUser);
            this.txtDelete.Text = string.Join(",", DeleteList);
            this.txtResion.Text = string.Join(",", ResionList);
            this.txtShopName.Text = string.Join(",", ShopNameList);
            string command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='AlipayNo'";
            AliPayNoList = new List<string>();
            DataTable dt = conn.GetDataTableBySql(command);
            var AliPayNo = dt.Rows[0][0].ToString().Split(',').ToList();
            foreach (var item in AliPayNo)
            {
                AliPayNoList.Add(item);
            }
            this.txtAlipayNo.Text = string.Join(",", AliPayNoList);
            command = string.Empty;
            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='UpdatePathUser'";
            PicPathUserList = new List<string>();
            dt = conn.GetDataTableBySql(command);
            var picPath = dt.Rows[0][0].ToString().Split(',').ToList();
            foreach (var item in picPath)
            {
                PicPathUserList.Add(item);
            }
            this.txtUpdatePath.Text = string.Join(",", PicPathUserList);
            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='AuditUser'";
            AuditUserList = new List<string>();
            dt = conn.GetDataTableBySql(command);
            var AuditUser = dt.Rows[0][0].ToString().Split(',').ToList();
            foreach (var item in AuditUser)
            {
                AuditUserList.Add(item);
            }
            this.txtAuditUser.Text = string.Join(",", AuditUserList);

            command = " select \"AuthorizationUser\" from \"Authorization\" where \"FunctionName\"='PictureUser'";
            PictureUserList = new List<string>();
            dt = conn.GetDataTableBySql(command);
            var PictureUser = dt.Rows[0][0].ToString().Split(',').ToList();
            foreach (var item in PictureUser)
            {
                PictureUserList.Add(item);
            }
            this.tbPictureUser.Text = string.Join(",", PictureUserList);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Main main = new Main();
            //main.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string command = string.Empty;
            //if(!string.IsNullOrWhiteSpace(this.txtInput.Text))
            //{
            command = "update \"Authorization\" set \"AuthorizationUser\"='" + this.txtInput.Text + "' where \"FunctionName\"='Input'";
            conn.ExecuteSql(command);
        //}
        //if (!string.IsNullOrWhiteSpace(this.txtDeal.Text))
        //{
            command = "update \"Authorization\" set \"AuthorizationUser\"='" + this.txtDeal.Text + "' where \"FunctionName\"='Deal'";
            conn.ExecuteSql(command);
        //}
        //if (!string.IsNullOrWhiteSpace(this.txtResion.Text))
        //{
            command = "update \"Authorization\" set \"AuthorizationUser\"='" + this.txtResion.Text + "' where \"FunctionName\"='Resion'";
            conn.ExecuteSql(command);
        //}
        //if (!string.IsNullOrWhiteSpace(this.txtShopName.Text))
        //{
            command = "update \"Authorization\" set \"AuthorizationUser\"='" + this.txtShopName.Text + "' where \"FunctionName\"='ShopName'";
            conn.ExecuteSql(command);
        //}
        //if (!string.IsNullOrWhiteSpace(this.txtDelete.Text))
        //{
            command = "update \"Authorization\" set \"AuthorizationUser\"='" + this.txtDelete.Text + "' where \"FunctionName\"='Delete'";
            conn.ExecuteSql(command);
        //}
        //if (!string.IsNullOrWhiteSpace(this.txtAlipayNo.Text))
        //{
            command = "update \"Authorization\" set \"AuthorizationUser\"='" + this.txtAlipayNo.Text + "' where \"FunctionName\"='AlipayNo'";
            conn.ExecuteSql(command);
        //}
        //if (!string.IsNullOrWhiteSpace(this.txtUpdatePath.Text))
        //{
            command = "update \"Authorization\" set \"AuthorizationUser\"='" + this.txtUpdatePath.Text + "' where \"FunctionName\"='UpdatePathUser'";
            conn.ExecuteSql(command);
        //}
            command = "update \"Authorization\" set \"AuthorizationUser\"='" + this.tbPictureUser.Text + "' where \"FunctionName\"='PictureUser'";
            conn.ExecuteSql(command);
            command = "select * from \"Authorization\" where \"FunctionName\"='AuditUser'";
            DataTable tb = conn.GetDataTableBySql(command);
            if (tb.Rows.Count == 0)
            {
                command = "insert into \"Authorization\"(\"AuthorizationUser\",\"FunctionName\") values('" + this.txtAuditUser.Text + "','AuditUser')";
                conn.ExecuteSql(command);
            }
            else
            {
                command = "update \"Authorization\" set \"AuthorizationUser\"='" + this.txtAuditUser.Text + "' where \"FunctionName\"='AuditUser'";
                conn.ExecuteSql(command);
            }

            var alipayList = this.txtAlipayNo.Text.Split(',').ToList();
            if (alipayList.Count() > 0)
            {
                foreach(var no in alipayList)
                {
                    if (string.IsNullOrWhiteSpace(no))
                        continue;
                    command = "select * from Value where value='"+no+"'";
                    tb = conn.GetDataTableBySql(command);
                    if (tb.Rows.Count == 0)
                    {
                        command = "insert into Value(value,money) values('"+no+"',0)";
                        conn.ExecuteSql(command);
                    }
                }
            }


            MessageBox.Show("保存成功!");
            //Main main = new Main();
            //main.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.tbNo.Text))
            {
                //int num = int.Parse(this.tbNo.Text;
                string command = string.Empty;
                command = "update \"Value\" set \"id\"='"+this.tbNo.Text+"' where \"value\"='test'";
                string message = conn.ExecuteSql(command);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    MessageBox.Show("修改!失败原因:" + message);
                }
                else
                    MessageBox.Show("重置成功");
            }
        }

        private void btnRepeatTip_Click(object sender, EventArgs e)
        {
            string command = string.Empty;
            command = "select \"AuthorizationUser\" from \"Authorization\" where FunctionName='RepeatTipItem'";
            DataTable dt = conn.GetDataTableBySql(command);
            List<string> selectItem = new List<string>();
            if (dt.Rows.Count > 0)
            {
                selectItem = dt.Rows[0][0].ToString().Split(',').ToList();
            }
            RepeatShow repeat = new RepeatShow(selectItem);
            repeat.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //SqlConnection localhost = new SqlConnection("data source=192.168.1.11;initial catalog=OrderManage;persist security info=False;user id=sa;password=sa;");
            SqlConnection localhost = new SqlConnection("data source=localhost;initial catalog=OrderManage;persist security info=False;user id=sa;password=sa;");
            List<OrderEntry> resultList = new List<OrderEntry>();
            List<RefundDetail> resultDetailList = new List<RefundDetail>();
            List<User> UserList = new List<User>();
            string command = string.Empty;
            command = "select * from OrderRecord Order by ID";
            DataTable dt = this.GetDataTableBySql(command, localhost);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    OrderEntry entry = new OrderEntry();
                    //entry.SeqNum = seq;
                    entry.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    entry.Status = dt.Rows[i]["Status"].ToString();
                    entry.ShopName = dt.Rows[i]["ShopName"].ToString();
                    entry.BuyersID = dt.Rows[i]["BuyersID"].ToString();
                    entry.RefundWay = dt.Rows[i]["RefundWay"].ToString();
                    entry.AlipayNo = dt.Rows[i]["AlipayNo"].ToString();
                    entry.RefundAmount = decimal.Parse(dt.Rows[i]["RefundAmount"].ToString());
                    entry.RefundResoin = dt.Rows[i]["RefundResoin"].ToString();
                    entry.Remark = dt.Rows[i]["Remark"].ToString();
                    entry.CreateUser = dt.Rows[i]["CreateUser"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[i]["RefundTime"].ToString()))
                        entry.RefundTime = DateTime.Parse(dt.Rows[i]["RefundTime"].ToString());

                    entry.RefundUserName = dt.Rows[i]["RefundUserName"].ToString();
                    if (dt.Rows[i]["IsDelete"].ToString() == "1")
                        entry.IsDelete = true;
                    else
                        entry.IsDelete = false;
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
                    //seq++;
                    resultList.Add(entry);
                }
            }
            command = "select * from RefundDetail Order by ID";
            dt = this.GetDataTableBySql(command, localhost);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    RefundDetail entry = new RefundDetail();
                    //entry.SeqNum = seq;
                    entry.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    entry.orderId = int.Parse(dt.Rows[i]["orderId"].ToString());
                    entry.RefundAmount = decimal.Parse(dt.Rows[i]["RefundAmount"].ToString());
                    entry.RefundResoin = dt.Rows[i]["RefundResoin"].ToString();
                    entry.code = dt.Rows[i]["code"].ToString();
                    resultDetailList.Add(entry);
                }
            }
            command = "select * from \"User\" Order by ID";
            dt = this.GetDataTableBySql(command, localhost);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    User entry = new User();
                    //entry.SeqNum = seq;
                    entry.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    entry.UserName = dt.Rows[i]["UserName"].ToString();
                    entry.Password = dt.Rows[i]["Password"].ToString();
                    entry.Name = dt.Rows[i]["Name"].ToString();
                    
                    UserList.Add(entry);
                }
            }
            this.tbCount.Text = (resultList.Count() + resultDetailList.Count() + UserList.Count()).ToString();
            int insertCount = 0;
            foreach(var user in UserList)
            {
                command = "set IDENTITY_INSERT \"User\" ON ;";
                command += "insert into \"User\"(\"ID\",\"UserName\",\"Password\",\"Name\") values('"+user.ID+"','" + user.UserName + "','" + user.Password + "','" + user.Name + "')";
                command += " set IDENTITY_INSERT \"User\" off;";
                string message = conn.ExecuteSql(command);
                insertCount++;
                this.tbInsert.Text = insertCount.ToString();
                System.Windows.Forms.Application.DoEvents();
            }
            foreach (var detail in resultDetailList)
            {
                command = "set IDENTITY_INSERT RefundDetail ON ;";
                command += "insert into \"RefundDetail\"(\"ID\",\"orderId\",\"RefundAmount\",\"RefundResoin\",\"code\") values('" + detail.ID + "','" + detail.orderId + "','" + detail.RefundAmount + "','" + detail.RefundResoin + "','"+detail.code+"')";
                command += " set IDENTITY_INSERT RefundDetail off;";
                string message = conn.ExecuteSql(command);
                insertCount++;
                this.tbInsert.Text = insertCount.ToString();
                System.Windows.Forms.Application.DoEvents();
            }
            foreach (var result in resultList)
            {
                //\"RefundTime\"='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'
                string isDelete = string.Empty;
                if (result.IsDelete == true)
                    isDelete = "True";
                else
                    isDelete = "False";
                string refundTime = string.Empty;
                string createTime = string.Empty;
                if (result.RefundTime.HasValue)
                    refundTime = result.RefundTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                if(result.CreateTime.HasValue)
                    createTime = result.CreateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                command = "set IDENTITY_INSERT OrderRecord ON ;";
                command += " insert into \"OrderRecord\"(\"id\",\"Status\",\"ShopName\",\"BuyersID\",\"RefundWay\",\"AlipayNo\",\"RefundAmount\",\"RefundResoin\",\"Remark\",\"CreateUser\",\"IsDelete\",\"PDName1\",\"PDName2\",\"PDName3\",\"PDName4\",\"PDName5\",\"ExpressNo2\",\"ExpressNo3\",\"CreateTime\",\"RefundTime\",\"RefundUserName\",\"No\",\"RefundAlipayNo\") Values(" + result.ID + ",'" + result.Status + "','" + result.ShopName + "','" + result.BuyersID + "','" + result.RefundWay + "','" + result.AlipayNo + "','" + result.RefundAmount + "','" + result.RefundResoin + "','" + result.Remark + "','" + result.CreateUser + "','" + isDelete + "','" + result.PDName1 + "','" + result.PDName2 + "','" + result.PDName3 + "','" + result.PDName4 + "','" + result.PDName5 + "','" + result.ExpressNo2 + "','" + result.ExpressNo3 + "','" + createTime + "','" + refundTime + "','" + result.RefundUserName + "','" + result.No + "','" + result.RefundAlipayNo + "') ;";
                command += " set IDENTITY_INSERT OrderRecord off;";
                string message = conn.ExecuteSql(command);
                insertCount++;
                this.tbInsert.Text = insertCount.ToString();
                System.Windows.Forms.Application.DoEvents();
            }
            
        }
        public DataTable GetDataTableBySql(string commandText,SqlConnection conn)
        {
            DataTable result = new DataTable();
            using (SqlDataAdapter ad = new SqlDataAdapter(commandText, conn))
            {
                ad.Fill(result);
            }
            return result;
        }
        public string ExecuteSql(string sql,SqlConnection conn)
        {
            try
            {
                conn.Open();
                SqlCommand myCmd = new SqlCommand(sql, conn);
                myCmd.ExecuteNonQuery();

                conn.Close();
                return "";
            }
            catch (Exception exc)
            {
                string s = exc.ToString();

                conn.Close();
                return s;
            }
        }
    }
}
