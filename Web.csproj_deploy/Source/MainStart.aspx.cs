using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _MainStart : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.QueryString["AccountID"] != null)
        //{
        //    UserInfo.AccountID = Request.QueryString["AccountID"];
        //}
        //string AccountDate = MainClass.GetTableValue("cw_account", "accountdate", "id='" + UserInfo.AccountID + "'");
        //if (AccountDate.Length > 0)
        //{
        //    Session["isStartAccount"] = "Yes";
        //}
        //else
        //{
        //    Session["isStartAccount"] = null;
        //    if (Session["Powers"].ToString().IndexOf("000001") == -1)
        //    {
        //        Response.Redirect("ErrorTip.aspx?errTip=当前账套尚未启用，请联系相应操作员启用账套！");
        //    }
        //    else
        //    {
        //        Response.Redirect("AccountInit/AccountSubject.aspx");
        //    }
        //}
    }
}

