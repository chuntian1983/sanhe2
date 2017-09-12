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

public partial class SysManage_AddUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            PowerType.Items[0].Attributes.Add("onclick", "SelPower(this.value);");
            PowerType.Items[1].Attributes.Add("onclick", "SelPower(this.value);");
            PowerType.Items[2].Attributes.Add("onclick", "SelPower(this.value);");
            DataSet ds = MainClass.GetDataSet("select id,accountname from cw_account where unitid='" + UserInfo.UnitID + "' order by levelid,id");
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Accounts.Items.Add(new ListItem(row["accountname"].ToString(), row["id"].ToString()));
            }
            ListItem SelAllBox = new ListItem();
            SelAllBox.Attributes["style"] = "color:red;background:#f6f6f6";
            SelAllBox.Attributes["onclick"] = "for(var i=0;i<" + Accounts.Items.Count.ToString() + ";i++)$('Accounts_'+i).checked=this.checked;";
            SelAllBox.Text = "选择所有账套";
            SelAllBox.Value = "999999";
            Accounts.Items.Add(SelAllBox);
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
            StringBuilder _Accounts = new StringBuilder();
            StringBuilder _Powers = new StringBuilder();
            foreach (ListItem cb in Accounts.Items)
            {
                if (cb.Selected && cb.Value != "999999") { _Accounts.Append(cb.Value + "$"); }
            }
            foreach (ListItem cb in Powers.Items)
            {
                if (cb.Selected) { _Powers.Append(cb.Value + "$"); }
            }
            MainClass.ExecuteSQL("insert cw_users(id,unitid,realname,username,password,accountid,powers,logincounts)values('"
                + MainClass.GetRecordID("CW_Users") + "','"
                + Session["UnitID"].ToString() + "','"
                + RealName.Text + "','"
                + UserName.Text + "','"
                + Password.Text + "','"
                + _Accounts.ToString() + "','"
                + _Powers.ToString() + "','0')");
            ExeScript.Text = "<script language=javascript>if(confirm('添加成功！您需要继续添加吗？')){" +
                          "location.replace('AddUser.aspx');}else{location.replace('UserManage.aspx');}</script>";
        }
    }
}
