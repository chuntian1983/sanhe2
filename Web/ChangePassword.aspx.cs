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

public partial class _ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            form1.Attributes.Add("onsubmit", "return CheckSubmit();");
            RealName.Text = Session["RealName"].ToString();
            UserName.Text = Session["UserName"].ToString();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string OPassword = MainClass.GetTableValue("cw_users", "password", "id='" + Session["UserID"] + "'");
        if (OldPassword.Text == OPassword)
        {
            MainClass.ExecuteSQL("update cw_users set password='" + NewPassword.Text + "' where id='" + Session["UserID"] + "'");
            PageClass.ShowAlertMsg(this.Page, "密码修改成功！");
        }
        else
        {
            PageClass.ShowAlertMsg(this.Page, "密码错误，请核实！");
        }
    }
}
