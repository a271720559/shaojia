using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ExpressManage
{
    public class SqlConn
    {
        public static SqlConnection conn = new SqlConnection("data source=hds117139552.my3w.com;initial catalog=hds117139552_db;persist security info=False;user id=hds117139552;password=m12345678;");
        public static DataTable BindTable(string sqlstr) //静态方法；参数sqlstr为数据库查询语句,将从数据库中获得的数据填充到一个DataTable中，返回该DataTable
        {
            SqlDataAdapter cmd = new SqlDataAdapter(sqlstr, conn);
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            return dt;
        }

        public static DataTable BindTable(string sqlstr, string condition) //静态方法；参数sqlstr为数据库查询语句,参数condition为查询条件,将从数据库中获得的数据填充到一个DataTable中，返回该DataTable
        {
            SqlDataAdapter cmd = new SqlDataAdapter(sqlstr + condition, conn);
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            return dt;
        }
        public static SqlDataReader BindReader(string sqlstr) //静态方法；参数sqlstr为数据库查询语句,将SqlDataReader指向从数据库中获得的数据，返回该SqlDataReader
        {
            SqlCommand cmd = new SqlCommand(sqlstr, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
        public DataTable GetDataTableBySql(string commandText)
        {
            DataTable result = new DataTable();
            using (SqlDataAdapter ad = new SqlDataAdapter(commandText, conn))
            {
                ad.Fill(result);
            }
            return result;
        }
        public string ExecuteSql(string sql)
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
