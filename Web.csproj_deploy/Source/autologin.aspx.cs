using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace SanZi.Web
{
    public partial class autologin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("P3P", "CP=CAO PSA OUR");
            string uid = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[UnitName='" + Request.QueryString["uid"] + "']", "ID");
            if (uid.Length == 0)
            {
                PageClass.UrlRedirect("该单位不存在！", 3);
            }
            else
            {
                Session["SessionFlag"] = "SessionFlag";
                Session["UnitID"] = uid;
                Session["UnitName"] = "公共查询";
                Session["UserID"] = "000000";
                Session["RealName"] = "领导监督";
                Session["UserName"] = "领导监督";
                StringBuilder sb = new StringBuilder();
                for (int i = 100000; i < 100025; i++)
                {
                    sb.AppendFormat("{0}$", i.ToString());
                }
                Session["Powers"] = sb.ToString();
                Session["MyAccount"] = "000000";
                Session["UserType"] = "0";
                Response.Redirect(Request.QueryString["jump"], true);
            }
        }
    }
}