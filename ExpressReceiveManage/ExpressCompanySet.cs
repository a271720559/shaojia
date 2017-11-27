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
    public partial class ExpressCompanySet : Form
    {
       private SqlConn conn = new SqlConn();
       private BindingList<CompanyEntry> CompanyList = new BindingList<CompanyEntry>();
        public ExpressCompanySet()
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = CompanyList;

            string command = string.Empty;
            command = "select * from CompanySet ";
            DataTable dt = conn.GetDataTableBySql(command);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CompanyEntry entry = new CompanyEntry();
                    entry.CompanyName = dt.Rows[i]["CompanyName"].ToString();
                    entry.ExpressNo = dt.Rows[i]["ExpressNo"].ToString();
                    this.CompanyList.Add(entry);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string command = string.Empty;
                //先删除所有数据
                command = "delete from CompanySet";
                conn.ExecuteSql(command);
                foreach (var item in this.CompanyList)
                {

                    if (!string.IsNullOrWhiteSpace(item.CompanyName) && !string.IsNullOrWhiteSpace(item.ExpressNo))
                    {
                        command = " insert into CompanySet(CompanyName,ExpressNo) values('" + item.CompanyName + "'," + item.ExpressNo + ")";
                        conn.ExecuteSql(command);
                    }
                }
                MessageBox.Show("保存成功");
            }
            catch(Exception ex)
            {
                MessageBox.Show("保存失败，原因："+ex.Message);
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in this.dataGridView1.SelectedRows)
            {
                if (!r.IsNewRow)
                {
                    this.dataGridView1.Rows.Remove(r);
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            CompanyEntry entry = new CompanyEntry();
            this.CompanyList.Add(entry);
        }
    }
    public class CompanyEntry
    {
        public string CompanyName { get; set; }
        public string ExpressNo { get;set; }
    }
}
