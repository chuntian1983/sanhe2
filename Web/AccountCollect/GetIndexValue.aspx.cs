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

public partial class AccountCollect_GetIndexValue : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string IndexSingle = MainClass.GetSysPara(string.Format("Index{0}_{1}", Request.QueryString["uid"], Request.QueryString["no"]));
        string[] indexValue = new string[] { "0", "0", "0" };
        if (IndexSingle != "NoDataItem")
        {
            indexValue = IndexSingle.Split('|');
        }
        Response.Write("$('IndexSingle').value='" + indexValue[0] + "';");
        Response.Write("$('IndexMonth').value='" + indexValue[1] + "';");
        Response.Write("$('IndexYear').value='" + indexValue[2] + "';");
    }
}
