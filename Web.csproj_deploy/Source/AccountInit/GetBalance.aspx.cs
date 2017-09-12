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

public partial class AccountInit_GetBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds = CommClass.GetDataSet("select * from cw_subject where subjectno='" + Request.QueryString["no"] + "'");
        if (ds.Tables[0].Rows[0]["BeginBalance"].ToString().IndexOf("-") == -1)
        {
            if (ds.Tables[0].Rows[0]["BeginBalance"].ToString() != "0")
            {
                Response.Write("$('BalanceType').selectedIndex=1;");
            }
            else
            {
                Response.Write("$('BalanceType').selectedIndex=0;");
            }
        }
        else
        {
            Response.Write("$('BalanceType').selectedIndex=2;");
        }
        string _BeginBalance = ds.Tables[0].Rows[0]["BeginBalance"].ToString().ToString().Replace("-", "");
        Response.Write("$('MonthBalance').value='" + _BeginBalance + "';");
        Response.Write("$('AccountType').value='" + ds.Tables[0].Rows[0]["AccountType"].ToString() + "';");
        Response.Write("$('HSubjectType').value='" + ds.Tables[0].Rows[0]["SubjectType"].ToString() + "';");
        Response.Write("$('SCount').value='0';");
        Response.Write("$('SUnit').value='';");
        Response.Write("$('SType').value='';");
        Response.Write("$('SClass').value='';");
        if (ds.Tables[0].Rows[0]["AccountType"].ToString() == "0")
        {
            Response.Write("$('SCount').disabled='disabled';");
            Response.Write("$('SUnit').disabled='disabled';");
            Response.Write("$('SType').disabled='disabled';");
            Response.Write("$('SClass').disabled='disabled';");
        }
        else
        {
            Response.Write("$('SCount').disabled='';");
            Response.Write("$('SUnit').disabled='';");
            Response.Write("$('SType').disabled='';");
            Response.Write("$('SClass').disabled='';");
            ds = CommClass.GetDataSet("select * from cw_subjectdata where subjectid='" + ds.Tables[0].Rows[0]["id"].ToString() + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Response.Write("$('SCount').value='" + ds.Tables[0].Rows[0]["amount"].ToString() + "';");
                Response.Write("$('SUnit').value='" + ds.Tables[0].Rows[0]["SUnit"].ToString() + "';");
                Response.Write("$('SType').value='" + ds.Tables[0].Rows[0]["SType"].ToString() + "';");
                Response.Write("$('SClass').value='" + ds.Tables[0].Rows[0]["SClass"].ToString() + "';");
            }
        }
    }
}
