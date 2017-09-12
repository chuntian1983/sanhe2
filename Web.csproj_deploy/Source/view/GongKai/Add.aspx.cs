using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using LTP.Common;

namespace SanZi.Web.GongKai
{
    public partial class add : System.Web.UI.Page
    {
        public string strurl;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtDeptName.Text = UserInfo.AccountName;
            string url = "http://" + Request.ServerVariables["HTTP_HOST"].ToString() + Request.ServerVariables["PATH_INFO"].ToString();  //获得URL的值
            int i = url.LastIndexOf("/");
            if (Request.QueryString["lbid"] != null)
            {
                hidlbid.Value = Request.QueryString["lbid"];
                string strsql = "select * from cwgk_lbb where id='" + Request.QueryString["lbid"] + "'";
                DataTable dt = MainClass.GetDataTable(strsql);
                if (dt.Rows.Count > 0)
                {
                    url = url.Substring(0, i) + "/" + dt.Rows[0]["filename"].ToString();
                }
            }
            this.Session["URL"] = url.Substring(0, i) + "/cwgkb.xls";
            strurl = url.Substring(0, i) + "/savefile.aspx";
            this.Session["strurl"] = strurl;
            hidDeptID.Value = UserInfo.AccountID;
        }
    }
}
