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
using System.Text.RegularExpressions;
using System.Xml;

public partial class _ReportOpenPage : System.Web.UI.MasterPage
{
    public StringBuilder SubMenuList = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        //UserInfo.CheckSession();
    }
}
