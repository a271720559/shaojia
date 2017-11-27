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
    public partial class ResionInput : Form
    {
        private SqlConn conn = new SqlConn();
        private BindingList<ResionEntry> ResionList = new BindingList<ResionEntry>();
        public ResionInput()
        {
            InitializeComponent();
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = ResionList;

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
            command = "select * from ResionSet ";
            DataTable dt = conn.GetDataTableBySql(command);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ResionEntry entry = new ResionEntry();
                    entry.Resion = dt.Rows[i]["Resion"].ToString();
                    entry.Days = int.Parse(dt.Rows[i]["Days"].ToString());
                    this.ResionList.Add(entry);
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
                command = "delete from ResionSet";
                conn.ExecuteSql(command);
                foreach (var item in this.ResionList)
                {
                    
                    if (item.Days > 0)
                    {
                        command = " insert into ResionSet(Resion,Days) values('" + item.Resion + "'," + item.Days + ")";
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
            ResionEntry entry = new ResionEntry();
            this.ResionList.Add(entry);
        }
    }
    public class ResionEntry
    {
        public string Resion { get; set; }
        public int Days { get;set; }
    }
}
