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

public partial class SysManage_AccountManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Overlay.Attributes["style"] = "display:none";
            Lightbox.Attributes["style"] = "display:none";
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            if (!ValidateClass.ValidateUseDate()) { return; }
            MainClass.ExecuteSQL("update cw_account set levelid=id where levelid='' or levelid is null");
            InitWebControl();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.EditIndex = -1;
        if (MainClass.CheckExist("cw_account", string.Format("accountname='{0}' and unitid='{1}'", AccountName.Text, Session["UnitID"].ToString())))
        {
            ExeScript.Text = "<script>alert('此账套名称【" + AccountName.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            try
            {
                //创建账套
                string AccountID = MainClass.GetRecordID("CW_Accounts");
                MainClass.CreateDatabase(AccountID, AccountType.SelectedValue);
                MainClass.ExecuteSQL(string.Format("insert cw_account(id,levelid,unitid,accountname,director)values('{0}','{0}','{1}','{2}','{3}')",
                    AccountID, Session["UnitID"].ToString(), AccountName.Text, Director.Text));
                ExeScript.Text = "<script>alert('恭喜您，账套【" + AccountName.Text + "】创建成功！')</script>";
            }
            catch
            {
                throw;
                ExeScript.Text = "<script>alert('账套【" + AccountName.Text + "】创建失败！')</script>";
            }
            AccountName.Text = "";
            Director.Text = "";
        }
        InitWebControl();
    }

    private void ImportData(string TableName, string TableColumns, DataSet ImData)
    {
        StringBuilder SQLString = new StringBuilder();
        SQLString.Append("insert into " + TableName + "(ID," + TableColumns + ")values");
        for (int i = 0; i < ImData.Tables[0].Rows.Count; i++)
        {
            SQLString.Append("('" + CommClass.GetRecordID(TableName));
            for (int k = 0; k < ImData.Tables[0].Columns.Count; k++)
            {
                SQLString.Append("','" + ImData.Tables[0].Rows[i][k].ToString());
            }
            SQLString.Append("'),");
        }
        if (SQLString.ToString().EndsWith(","))
        {
            SQLString.Remove(SQLString.Length - 1, 1);
            CommClass.ExecuteSQL(SQLString.ToString());
        }
        SQLString.Length = 0;
    }

    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        DataTable dt = MainClass.GetDataTable("select id,levelid,accountname,accountdate,startaccountdate,director from cw_account where unitid='" + Session["UnitID"].ToString() + "' order by levelid,id");
        int acount = dt.Rows.Count;
        if (acount == 0)
        {
            PageClass.BindNoRecords(GridView1, dt);
        }
        else
        {
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            //账套平移控制
            for (int i = 1; i < acount - 1; i++)
            {
                LinkButton btnUp = (LinkButton)GridView1.Rows[i].FindControl("btnUp");
                LinkButton btnDn = (LinkButton)GridView1.Rows[i].FindControl("btnDn");
                string uid = dt.Rows[i - 1]["id"].ToString();
                string did = dt.Rows[i + 1]["id"].ToString();
                btnUp.CommandArgument = uid;
                btnDn.CommandArgument = did;
            }
            LinkButton btnUp0 = (LinkButton)GridView1.Rows[0].FindControl("btnUp");
            btnUp0.Enabled = false;
            LinkButton btnDn0 = (LinkButton)GridView1.Rows[acount - 1].FindControl("btnDn");
            btnDn0.Enabled = false;
            if (acount > 1)
            {
                LinkButton btnDn1 = (LinkButton)GridView1.Rows[0].FindControl("btnDn");
                LinkButton btnUp1 = (LinkButton)GridView1.Rows[acount - 1].FindControl("btnUp");
                string did = dt.Rows[1]["id"].ToString();
                string uid = dt.Rows[acount - 2]["id"].ToString();
                btnDn1.CommandArgument = did;
                btnUp1.CommandArgument = uid;
            }
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        InitWebControl();
        ((TextBox)GridView1.Rows[e.NewEditIndex].Cells[1].Controls[0]).MaxLength = 50;
        ((TextBox)GridView1.Rows[e.NewEditIndex].Cells[2].Controls[0]).MaxLength = 50;
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        InitWebControl();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
        TextBox N = (TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0];
        if (MainClass.CheckExist("cw_account", string.Format("accountname='{0}' and id<>'{1}' and unitid='{2}'", N.Text, ID, Session["UnitID"].ToString())))
        {
            ExeScript.Text = "<script>alert('此账套名称【" + N.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            string levelid = ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0]).Text;
            string director = ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            MainClass.ExecuteSQL(string.Format("update cw_account set levelid='{3}',accountname='{0}',director='{1}' where id='{2}'", N.Text, director, ID, levelid));
            GridView1.EditIndex = -1;
        }
        InitWebControl();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text == "&nbsp;")
            {
                e.Row.Cells[3].Text = "未启用";
                e.Row.Cells[4].Text = "未启用";
            }
            string aname = e.Row.Cells[1].Controls.Count == 0 ? e.Row.Cells[1].Text : ((TextBox)e.Row.Cells[1].Controls[0]).Text;
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "javascript:return DelUnit('您确定需要删除账套：“" + aname + "”吗？')");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        GridView1.EditIndex = -1;
        LinkButton btnDelete = (LinkButton)sender;
        MainClass.DropDatabase(btnDelete.CommandArgument);
        MainClass.ExecuteSQL(string.Format("delete from cw_account where id='{0}'", btnDelete.CommandArgument));
        MainClass.ExecuteSQL(string.Format("update cw_users set accountid=replace(accountid,'{0}$','') where accountid like '%{0}%'", btnDelete.CommandArgument));
        //删除预警信息
        MainClass.ExecuteSQL(string.Format("delete from cw_balancealarm where AccountID='{0}'", btnDelete.CommandArgument));
        //--
        ExeScript.Text = "<script>alert('账套删除成功！')</script>";
        InitWebControl();
    }
    protected void btnMove_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        string uid = MainClass.GetTableValue("cw_account", "levelid", string.Format("id='{0}'", btn.CommandName));
        string did = MainClass.GetTableValue("cw_account", "levelid", string.Format("id='{0}'", btn.CommandArgument));
        MainClass.ExecuteSQL(string.Format("update cw_account set levelid='{0}' where id='{1}'", did, btn.CommandName));
        MainClass.ExecuteSQL(string.Format("update cw_account set levelid='{0}' where id='{1}'", uid, btn.CommandArgument));
        InitWebControl();
    }
}
