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

public partial class BidManage_FileList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitWebControl();
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        string QueryString = "$ProjectID='" + Request.QueryString["pid"] + "'$StepFlag='" + Request.QueryString["step"] + "'";
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataTable data = MainClass.GetDataTable("select * from projattachs " + QueryString + " order by id");
        if (data.Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, data);
        }
        else
        {
            GridView1.DataSource = data.DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        MainClass.ExecuteSQL("delete from projattachs where id='" + btn.CommandName + "'");
        string filepath = Server.MapPath(btn.CommandArgument);
        try
        {
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
        }
        catch { }
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            btnDelete.Attributes["onclick"] = "return confirm('您确定删除吗？')";
            e.Row.Cells[0].Text = string.Concat("<a href='", btnDelete.CommandArgument, "' target=_blank>", e.Row.Cells[0].Text, "</a>");
            e.Row.Attributes["onmouseover"] = "bgColor=this.style.backgroundColor;this.style.backgroundColor='#dad5cc';fontColor=this.style.color;this.style.color='red';";
            e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=bgColor;this.style.color=fontColor;";
        }
    }
}
