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

public partial class AccountInit_GetBudget : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string BudgetBalance = CommClass.GetTableValue("cw_subjectbudget", "budgetbalance", "subjectno='" + Request.QueryString["no"] + "' and budgetyear='" + Request.QueryString["year"] + "'");
        if(BudgetBalance=="NoDataItem")
        {
            Response.Write("$('BudgetBalance').value='0';");
            Response.Write("$('BudgetBalanceOld').value='0';");
        }
        else
        {
            Response.Write("$('BudgetBalance').value='" + BudgetBalance + "';");
            Response.Write("$('BudgetBalanceOld').value='" + BudgetBalance + "';");
        }
    }
}
