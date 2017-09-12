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

public partial class BillManage_JournalList : System.Web.UI.Page
{
    private string[] arrPayType = { "现付", "现收", "银付", "银收", "无" };
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000081")) { return; }
        if (!IsPostBack)
        {
            if (Request.QueryString["flag"] == "102")
            {
                if (CommClass.CheckExist("cw_subject", "(subjectno like '102%')") == false)
                {
                    PageClass.UrlRedirect("请建立银行存款科目。", 3);
                }
                PageTitle.InnerHtml = "银行存款日记账管理";
            }
            else
            {
                if (CommClass.CheckExist("cw_subject", "(subjectno like '101%')") == false)
                {
                    PageClass.UrlRedirect("请建立现金科目。", 3);
                }
            }
            UtilsPage.SetTextBoxCalendar(QSDay, "yyyy年MM月dd日");
            UtilsPage.SetTextBoxCalendar(QEDay, "yyyy年MM月dd日");
            DateTime AccountDate = MainClass.GetAccountDate();
            QSDay.Text = AccountDate.ToString("yyyy年MM月01日");
            QEDay.Text = AccountDate.ToString("yyyy年MM月dd日");
            YearMonth.Value = AccountDate.ToString("yyyy年MM月");
            InitWebControl();
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        string QueryString = "$AccSubjectNo like '" + Request.QueryString["flag"] + "%'";
        //if (IsCreateVoucher.Checked)
        //{
        //    QueryString += "$VoucherID<>'-'";
        //}
        if (QSDay.Text.Length > 0)
        {
            QueryString += "$VoucherDate>='" + QSDay.Text + "'";
        }
        if (QEDay.Text.Length > 0)
        {
            QueryString += "$VoucherDate<='" + QEDay.Text + "'";
        }
        if (Notes.Text.Length > 0)
        {
            QueryString += "$Notes like '%" + Notes.Text + "%'";
        }
        if (VoucherType.SelectedValue != "-")
        {
            QueryString += "$VoucherType='" + VoucherType.SelectedValue + "'";
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataSet ds = CommClass.GetDataSet("select * from cw_dayaccount " + QueryString + " order by " + PaiXu.SelectedValue);
        if (ds.Tables[0].Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        CommClass.ExecuteSQL("delete from cw_dayaccount where id='" + btn.CommandArgument + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int at = TypeParse.StrToInt(e.Row.Cells[1].Text, 4);
            if (at > 4)
            {
                at = 4;
            }
            e.Row.Cells[1].Text = arrPayType[at] + "-" + e.Row.Cells[4].Text;
            if (e.Row.Cells[5].Text.StartsWith("-"))
            {
                e.Row.Cells[4].Text = "贷方";
                e.Row.Cells[5].Text = e.Row.Cells[5].Text.Substring(1);
            }
            else
            {
                e.Row.Cells[4].Text = "借方";
            }
            LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
            LinkButton btnShow = (LinkButton)e.Row.FindControl("btnShow");
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            btnShow.Attributes["onclick"] = string.Concat("return startPrint(0,'", btnShow.CommandArgument, "')");
            btnEdit.Attributes["onclick"] = string.Concat("return doCheck('JournalBook.aspx','", btnEdit.CommandArgument, "')");
            btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除吗？')");
            //if (e.Row.Cells[0].Text.StartsWith(YearMonth.Value))
            //{
            //    btnEdit.Attributes["onclick"] = string.Concat("return doCheck('JournalBook.aspx','", btnEdit.CommandArgument, "')");
            //    btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除吗？')");
            //}
            //else
            //{
            //    btnEdit.Enabled = false;
            //    btnDelete.Enabled = false;
            //}
            e.Row.Attributes["onmouseover"] = "bgColor=this.style.backgroundColor;this.style.backgroundColor='#dad5cc';fontColor=this.style.color;this.style.color='red';";
            e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=bgColor;this.style.color=fontColor;";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
    protected void PaiXu_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
