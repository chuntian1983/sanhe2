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

public partial class SysManage_EditUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
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
            InitWebControl();
        }
        PowerType.Items[0].Attributes.Add("onclick", "SelPower(this.value);");
        PowerType.Items[1].Attributes.Add("onclick", "SelPower(this.value);");
        PowerType.Items[2].Attributes.Add("onclick", "SelPower(this.value);");
    }
    protected void InitWebControl()
    {
        DataRow row = MainClass.GetDataRow("select * from cw_users where id='" + Request.QueryString["id"] + "'");
        RealName.Text = row["RealName"].ToString();
        UserName.Text = row["UserName"].ToString();
        Password.Text = row["Password"].ToString();
        UtilsPage.InitCheckBoxList(Accounts, row["AccountID"].ToString());
        UtilsPage.InitCheckBoxList(Powers, row["Powers"].ToString());
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (MainClass.CheckExist("cw_users", "username='" + UserName.Text + "' and id<>'" + Request.QueryString["id"] + "'"))
        {
            ExeScript.Text = "<script>alert('登录名称【" + UserName.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
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
            if (Password.Text.Length == 0)
            {
                MainClass.ExecuteSQL("update cw_users set "
                    + "realname='" + RealName.Text + "',"
                    + "username='" + UserName.Text + "',"
                    + "accountid='" + _Accounts.ToString() + "',"
                    + "powers='" + _Powers.ToString() + "' where id='" + Request.QueryString["id"] + "'");
            }
            else
            {
                MainClass.ExecuteSQL("update cw_users set "
                    + "realname='" + RealName.Text + "',"
                    + "username='" + UserName.Text + "',"
                    + "password='" + Password.Text + "',"
                    + "accountid='" + _Accounts.ToString() + "',"
                    + "powers='" + _Powers.ToString() + "' where id='" + Request.QueryString["id"] + "'");
            }
            ExeScript.Text = "<script>alert('数据保存成功！')</script>";
        }
        InitWebControl();
    }
}
