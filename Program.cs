using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OrderManage
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new login());
        }
    }

    public class UserEntry
    {
        public static string UserName { get; set; }
        public static string Name { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public string Password { get; set; }
        public  string UserName { get; set; }
        public  string Name { get; set; }
    }

    public class RefundDetail
    {
        public int ID { get; set; }
        public int orderId { get; set; }
        public string RefundResoin { get; set; }
        public decimal RefundAmount { get; set; }
        public string code { get; set; }
    }

    public class PicShowEntry
    {
        public string picPath { get; set; }
        public string picName { get; set; }
        public int no { get; set; }
    }

    public class OrderEntry
    {
        //public bool IsSelected { get; set; }
        public int? ID { get; set; }
        public int SeqNum { get; set; }
        public bool IsSelect { get; set; }
        public string Status { get; set; }
        public string ShopName { get; set; }
        public string BuyersID { get; set; }
        public string RefundWay { get; set; }
        public string AlipayNo { get; set; }
        public decimal RefundAmount { get; set; }
        public string RefundResoin { get; set; }
        public string code { get; set; }
        public string Remark { get; set; }
        public string CreateUser { get; set; }
        public DateTime? RefundTime { get; set; }
        public string RefundUserName { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreateTime { get; set; }
        public string PDName1 { get; set; }
        public string PDName2 { get; set; }
        public string PDName3 { get; set; }
        public string PDName4 { get; set; }
        public string PDName5 { get; set; }
        public string ExpressNo2 { get; set; }
        public string ExpressNo3 { get; set; }
        public int? No { get; set; }
        public string RefundAlipayNo { get; set; }//退款支付宝帐号
        public decimal RefundTotalAmount { get; set; }
        public string AlipayName { get; set; }//支付宝姓名
        public int InputCount { get; set; }
    }

    public class ExpressReceiveEntry
    {
        //public bool IsSelected { get; set; }
        public int? ID { get; set; }
        public int SeqNum { get; set; }
        public bool IsSelect { get; set; }
        public string Status { get; set; }
        public string ExpressNo { get; set; }
        public string ExpressCompany { get; set; }
        public string BuyAdress { get; set; }
        public string Resion { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string DealUser { get; set; }
        public DateTime? DealTime { get; set; }
        public DateTime? FinishTime { get; set; }
        public string Remark { get; set; }
        public string Remark2 { get; set; }
        public string Remark3 { get; set; }
        public string Remark4 { get; set; }
        public string Result { get; set; }
    }

    //public class ResionEntry
    //{
    //    public string Resion { get; set; }
    //    public int Days { get; set; }
    //}



}
