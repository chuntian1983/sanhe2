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

public partial class Contract_LeaseContractQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        //账务查询跳转
        if (UserInfo.UserType != "0")
        {
            if (UserInfo.AccountID == null || CommClass.ConnString.Length == 0)
            {
                PageClass.UrlRedirect("../ChooseAccount.aspx", 4);
                return;
            }
        }
        if (!IsPostBack)
        {
            AName.Text = UserInfo.AccountName;
            ReportDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            UtilsPage.SetTextBoxCalendar(SLease);
            UtilsPage.SetTextBoxCalendar(ELease);
            if (Request.QueryString["ctype"] == "0")
            {
                ReportTitle.InnerText = "资产经济合同查询";
                GridView1.Columns[0].HeaderText = "资产名称";
                tempTD.InnerText = "资产名称";
            }
            else if (Request.QueryString["ctype"] == "1")
            {
                ReportTitle.InnerText = "资源经济合同查询";
                GridView1.Columns[0].HeaderText = "资源名称";
                tempTD.InnerText = "资源名称";
            }
            else
            {
                ReportTitle.InnerText = "经济合同查询";
                GridView1.Columns[0].HeaderText = "资产/资源";
                tempTD.InnerText = "资产/资源";
            }
            InitWebControl();
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        StringBuilder sql = new StringBuilder();
        if (Request.QueryString["ctype"] != null && Request.QueryString["ctype"].Length > 0)
        {
            sql.AppendFormat("and cardtype='{0}' ", Request.QueryString["ctype"]);
        }
        if (ContractNo.Text.Length > 0)
        {
            sql.Append("and ContractNo like '%" + ContractNo.Text + "%' ");
        }
        if (ContractType.SelectedValue != "不限")
        {
            sql.Append("and ContractType='" + ContractType.SelectedValue + "' ");
        }
        if (ContractName.Text.Length > 0)
        {
            sql.Append("and ContractName like '%" + ContractName.Text + "%' ");
        }
        if (ResourceName.Text.Length > 0)
        {
            sql.Append("and ResourceName like '%" + ResourceName.Text + "%' ");
        }
        if (ResUnitName.Text.Length > 0)
        {
            sql.Append("and ResUnitName like '%" + ResUnitName.Text + "%' ");
        }
        if (SLease.Text.Length > 0)
        {
            sql.Append("and StartLease >= '" + SLease.Text + "' ");
        }
        if (ELease.Text.Length > 0)
        {
            sql.Append("and EndLease <= '" + ELease.Text + "' ");
        }
        if (sql.Length > 0)
        {
            sql.Remove(0, 4);
            sql.Insert(0, " where ");
        }
        sql.Insert(0, "select * from cw_resleasecard ");
        sql.Append("order by cardno desc");
        DataSet ds = CommClass.GetDataSet(sql.ToString());
        if (ds.Tables[0].Rows.Count == 0)
        {
            OutputDataToExcel.Enabled = false;
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
            OutputDataToExcel.Enabled = true;
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            Label lb = (Label)GridView1.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
            lb.Text = "记录数：" + ds.Tables[0].Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lb.Text += "总页数：" + (GridView1.PageIndex + 1) + "/" + GridView1.PageCount + "页";
            DropDownList ddl = (DropDownList)GridView1.BottomPagerRow.Cells[0].FindControl("JumpPage");
            ddl.Items.Clear();
            for (int i = 0; i < GridView1.PageCount; i++)
            {
                ddl.Items.Add(new ListItem("第" + (i + 1).ToString() + "页", i.ToString()));
            }
            ddl.SelectedIndex = GridView1.PageIndex;
        }
    }
    protected void JumpPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridView1.PageIndex = Convert.ToInt32(ddl.SelectedValue);
        InitWebControl();
    }
    protected void FirstPage_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        InitWebControl();
    }
    protected void PreviousPage_Click(object sender, EventArgs e)
    {
        if (GridView1.PageIndex > 0)
        {
            GridView1.PageIndex -= 1;
            InitWebControl();
        }
    }
    protected void NextPage_Click(object sender, EventArgs e)
    {
        if (GridView1.PageIndex < GridView1.PageCount)
        {
            GridView1.PageIndex += 1;
            InitWebControl();
        }
    }
    protected void LastPage_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridView1.PageCount;
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnContract = (LinkButton)e.Row.FindControl("btnContract");
            LinkButton btnResource = (LinkButton)e.Row.FindControl("btnResource");
            btnContract.Attributes["onclick"] = string.Format("return ShowVoucher(0,'{0}');", btnContract.CommandArgument);
            btnResource.Attributes["onclick"] = string.Format("return ShowVoucher(1,'{0}');", btnResource.CommandArgument);
            if (Request.QueryString["ctype"] == "0")
            {
                btnResource.Text = "资产";
            }
            else
            {
                btnResource.Text = "资源";
            }
        }
    }
    protected void OutputDataToExcel_Click(object sender, EventArgs e)
    {
        InitWebControl();
        GridView1.Columns[8].Visible = false;
        PageClass.ToExcel(GridView1);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
