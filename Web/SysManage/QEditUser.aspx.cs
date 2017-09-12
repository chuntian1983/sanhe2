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

public partial class SysManage_QEditUser : System.Web.UI.Page
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
            AccountList.Attributes["onchange"] = "OnSelAccount();";
            int PowerCount = Powers.Items.Count - 1;
            Powers.Items[PowerCount].Attributes["style"] = "color:red;background:#f6f6f6";
            Powers.Items[PowerCount].Attributes["onclick"] = "for(var i=0;i<" + PowerCount.ToString() + ";i++)$('Powers_'+i).checked=this.checked;";
            InitWebControl();
        }
    }
    protected void InitWebControl()
    {
        DataRow row = MainClass.GetDataRow("select * from cw_users where id='" + Request.QueryString["id"] + "'");
        RealName.Text = row["RealName"].ToString();
        UserName.Text = row["UserName"].ToString();
        Password.Text = row["Password"].ToString();
        AccountList.Text = row["AccountID"].ToString();
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
            StringBuilder _Powers = new StringBuilder();
            foreach (ListItem cb in Powers.Items)
            {
                if (cb.Selected) { _Powers.Append(cb.Value + "$"); }
            }
            if (Password.Text.Length == 0)
            {
                MainClass.ExecuteSQL("update cw_users set "
                    + "realname='" + RealName.Text + "',"
                    + "username='" + UserName.Text + "',"
                    + "accountid='" + AccountList.SelectedValue + "',"
                    + "powers='" + _Powers.ToString() + "' where id='" + Request.QueryString["id"] + "'");
            }
            else
            {
                MainClass.ExecuteSQL("update cw_users set "
                    + "realname='" + RealName.Text + "',"
                    + "username='" + UserName.Text + "',"
                    + "password='" + Password.Text + "',"
                    + "accountid='" + AccountList.SelectedValue + "',"
                    + "powers='" + _Powers.ToString() + "' where id='" + Request.QueryString["id"] + "'");
            }
            ExeScript.Text = "<script>alert('数据保存成功！')</script>";
        }
        InitWebControl();
    }
}
