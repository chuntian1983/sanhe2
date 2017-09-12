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

public partial class AccountInit_DefineExpr : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        Constant.Attributes["onclick"] = "$$('ConType1').checked=true;";
        RowNo.Attributes.Add("onclick", "$$('ConType2').checked=true;");
        CellNo.Attributes.Add("onclick", "$$('ConType2').checked=true;");
        AllExprItems.Attributes["readonly"] = "readonly";
    }
}
