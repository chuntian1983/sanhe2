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
using System.Text.RegularExpressions;

public partial class _ztbLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Abandon();
            ValidateClass.GetRegDataSet();
            ImageButton2.Attributes["onclick"] = "return resetText();";
            txtUserName.Focus();
        }
    }
    protected void btnLogin_OnClick(object sender, ImageClickEventArgs e)
    {
        if (Regex.IsMatch(txtPassword.Text, "select|insert|update|where|=|'", RegexOptions.IgnoreCase))
        {
            PageClass.ShowAlertMsg(this.Page, "密码中含有非法数据，请核实！");
            txtPassword.Focus();
        }
        DataRow row = MainClass.GetDataRow("select id,unitid,password,realname,lastlogin,accountid from cw_users where username='" + txtUserName.Text + "' and unitid='X00001'");
        if (row == null)
        {
            PageClass.ShowAlertMsg(this.Page, "系统中无此用户，请核实！");
            txtUserName.Focus();
        }
        else
        {
            if (txtPassword.Text == row["password"].ToString())
            {
                Session["UFlag"] = "0";
                Session["SessionFlag"] = "SessionFlag";
                Session["UserID"] = row["id"].ToString();
                Session["UserName"] = txtUserName.Text;
                Session["RealName"] = row["RealName"].ToString();
                Session["LastLogin"] = row["LastLogin"].ToString();
                Session["UnitID"] = row["accountid"].ToString();
                Session["UnitName"] = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + row["accountid"].ToString() + "']", "UnitName");
                MainClass.ExecuteSQL("update cw_users set LoginCounts=LoginCounts+1,LastLogin='" + DateTime.Now.ToString() + "' where id='" + row["id"].ToString() + "'");
                //Response.Redirect("bform.aspx");
                Response.Redirect("zhaotoubiao.aspx");
            }
            else
            {
                PageClass.ShowAlertMsg(this.Page, "登录密码不正确，请核实！");
                txtPassword.Focus();
            }
        }
    }
}
