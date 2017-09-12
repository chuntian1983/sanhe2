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

public partial class AccountManage_ChooseSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            InitWebControl();
        }
    }
    protected void InitWebControl()
    {
        DataSet ds = CommClass.GetDataSet("select * from cw_summary");
        if (ds.Tables[0].Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = "$('Summary').value='" + e.Row.Cells[0].Text + "';";
            e.Row.Attributes["ondblclick"] = "WinClose();";
            e.Row.Attributes["style"] = "cursor:hand";
            e.Row.Attributes["onmouseover"] = "this.style.background='#EBF0F6';";
            if (e.Row.RowIndex % 2 == 0)
            {
                e.Row.Attributes["onmouseout"] = "this.style.background='';";
            }
            else
            {
                e.Row.Attributes["onmouseout"] = "this.style.background='#f6f6f6';";
            }
            LinkButton btnDelete = (LinkButton)e.Row.Cells[1].FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除吗？')");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btnDelete = (LinkButton)sender;
        CommClass.ExecuteSQL("delete from cw_summary where id='" + btnDelete.CommandArgument + "'");
        InitWebControl();
    }
    protected void AddSummary_Click(object sender, EventArgs e)
    {
        CommClass.ExecuteSQL("insert cw_summary(id,Contents)values('" + CommClass.GetRecordID("CW_Summary") + "','" + Summary.Text + "')");
        InitWebControl();
    }
}
