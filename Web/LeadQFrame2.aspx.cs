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
using System.Text;

public partial class _LeadQFrame2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (Request.QueryString["m"])
        {
            case "2":
                mFrame.Attributes["src"] = "SysManage/GatherLevel.aspx";
                break;
            case "3":
            case "4":
            case "5":
                mFrame.Attributes["src"] = "ChooseAccount.aspx";
                break;
            case "6":
                mFrame.Attributes["src"] = "MonitorList.aspx";
                break;
        }
    }
}
