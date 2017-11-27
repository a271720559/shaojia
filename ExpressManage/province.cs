using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExpressManage
{
    public partial class province : Form
    {
        private SqlConn conn = new SqlConn();
        private BindingList<ProvinceEntry> ProvinceList = new BindingList<ProvinceEntry>();
        public province()
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = ProvinceList;

            //ProvinceEntry test = new ProvinceEntry();
            //test.provinceName = "广东";
            //test.days = 2;
            //this.ProvinceList.Add(test);
            //ProvinceEntry test1 = new ProvinceEntry();
            //test1.provinceName = "广西";
            //test1.days = 3;
            //this.ProvinceList.Add(test1);
            //this.dataGridView1.bind
            //ProvinceList = new BindingList<ProvinceEntry>();
            string command = string.Empty;
            command = "select * from ProvinceSet ";
            DataTable dt = conn.GetDataTableBySql(command);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ProvinceEntry entry = new ProvinceEntry();
                    entry.provinceName = dt.Rows[i]["province"].ToString();
                    entry.days = int.Parse(dt.Rows[i]["days"].ToString());
                    this.ProvinceList.Add(entry);
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
                command = "delete from ProvinceSet";
                conn.ExecuteSql(command);
                foreach (var item in this.ProvinceList)
                {
                    
                    if (item.days > 0)
                    {
                        command = " insert into ProvinceSet(province,days) values('" + item.provinceName + "'," + item.days + ")";
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
            ProvinceEntry entry = new ProvinceEntry();
            this.ProvinceList.Add(entry);
        }
    }
    public class ProvinceEntry
    {
        public string provinceName { get; set; }
        public int days { get;set; }
    }
}
