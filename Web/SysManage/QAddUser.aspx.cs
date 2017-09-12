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

public partial class SysManage_QAddUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            DataTable dt = MainClass.GetDataTable("select id,accountname from cw_account where unitid='" + UserInfo.UnitID + "' order by levelid,id");
            foreach (DataRow row in dt.Rows)
            {
                AccountList.Items.Add(new ListItem(row["accountname"].ToString(), row["id"].ToString()));
            }
            int PowerCount = Powers.Items.Count - 1;
            Powers.Items[PowerCount].Attributes["style"] = "color:red;background:#f6f6f6";
            Powers.Items[PowerCount].Attributes["onclick"] = "for(var i=0;i<" + PowerCount.ToString() + ";i++)$('Powers_'+i).checked=this.checked;";
            AccountList.Attributes["onchange"] = "OnSelAccount();";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (MainClass.CheckExist("cw_users", "username='" + UserName.Text + "'"))
        {
            ExeScript.Text = "<script>alert('登录名称【" + UserName.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            /////////////////////////////////////版权校验//////////////////////////////////////
            if (!ValidateClass.ValidateUser(this.Page, UserName.Text, 0)) { return; }
            ///////////////////////////////////////////////////////////////////////////////////
            StringBuilder _Powers = new StringBuilder();
            foreach (ListItem cb in Powers.Items)
            {
                if (cb.Selected) { _Powers.Append(cb.Value + "$"); }
            }
            MainClass.ExecuteSQL("insert cw_users(id,unitid,realname,username,password,accountid,powers,logincounts)values('"
                + MainClass.GetRecordID("CW_Users") + "','999999"
                + Session["UnitID"].ToString() + "','"
                + RealName.Text + "','"
                + UserName.Text + "','"
                + Password.Text + "','"
                + AccountList.SelectedValue + "','"
                + _Powers.ToString() + "','0')");
            ExeScript.Text = "<script language=javascript>if(confirm('添加成功！您需要继续添加吗？')){" +
                          "location.replace('QAddUser.aspx');}else{location.replace('QUserManage.aspx');}</script>";
        }
    }
}
