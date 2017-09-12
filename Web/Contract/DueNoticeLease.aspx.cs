using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Contract_DueNoticeLease : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        AName.Text = UserInfo.AccountName;
        DataRow row = CommClass.GetDataRow("select LeaseHolder,ResourceName,EndLease from cw_resleasecard where id='" + Request.QueryString["id"] + "'");
        LeaseHolder.Text = row["LeaseHolder"].ToString();
        ResourceName.Text = row["ResourceName"].ToString();
        EndLease.Text = DateTime.Parse(row["EndLease"].ToString()).ToString("yyyy年MM月dd日");
        PrintDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
    }
}
