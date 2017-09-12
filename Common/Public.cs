using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Maticsoft.DBUtility;//请先添加引用

namespace LTP.Common
{
    public class Public
    {
        public static string AccountName
        {
            get
            {
                if (HttpContext.Current.Session["AccountName"] == null)
                {
                    HttpContext.Current.Session["AccountName"] = DbHelperMySQL.GetFieldFromID(HttpContext.Current.Session["AccountID"].ToString(), "AccountName", "cw_account");
                }
                return HttpContext.Current.Session["AccountName"].ToString();
            }
            set { HttpContext.Current.Session["AccountName"] = value; }
        }
        //public static string AccountName
        //{
        //    get
        //    {
        //       return "1";
        //    }
        //    set { HttpContext.Current.Session["AccountName"] = value; }
        //}
    }
}
