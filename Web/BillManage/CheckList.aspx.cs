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

public partial class BillManage_CheckList : System.Web.UI.Page
{
    private string[] arrUseState = { "未领用", "已领用", "已作废", "已核销" };
    private string[] arrBillType = { "&nbsp;", "现金支票", "转账支票", "电汇凭证", "贷记凭证" };
    
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            DataTable subject = CommClass.GetDataTable("select subjectno,subjectname from cw_subject where parentno='102'");
            foreach (DataRow sub in subject.Rows)
            {
                string sno = string.Concat(sub["subjectno"].ToString(), ".", sub["subjectname"].ToString());
                BillBank.Items.Add(new ListItem(sno, sno));
            }
            UtilsPage.SetTextBoxCalendar(SDate, "");
            UtilsPage.SetTextBoxCalendar(EDate, "");
            SDate.Text = DateTime.Now.ToString("yyyy-MM-01");
            EDate.Text = DateTime.Now.ToString("yyyy-MM-") + UtilsPage.GetThisMonthLastDay("00");
            InitWebControl();
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        string QueryString = string.Empty;
        if (BillBank.SelectedValue != "-")
        {
            QueryString += "$BillBank='" + BillBank.SelectedValue + "'";
        }
        if (BillType.SelectedValue != "-")
        {
            QueryString += "$BillType='" + BillType.SelectedValue + "'";
        }
        if (UseSate.SelectedValue != "-")
        {
            QueryString += "$UseState='" + UseSate.SelectedValue + "'";
        }
        if (SDate.Text.Length > 0)
        {
            QueryString += "$BillDate>='" + SDate.Text + "'";
        }
        if (EDate.Text.Length > 0)
        {
            QueryString += "$BillDate<='" + EDate.Text + "'";
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataSet ds = CommClass.GetDataSet("select * from cw_billcheck " + QueryString + " order by BillNo asc");
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        CommClass.ExecuteSQL("update cw_billcheck set UseState='2' where id='" + btn.CommandArgument + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        CommClass.ExecuteSQL("delete from cw_billcheck where id='" + btn.CommandArgument + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = arrUseState[TypeParse.StrToInt(e.Row.Cells[1].Text, 0)];
            e.Row.Cells[3].Text = arrBillType[TypeParse.StrToInt(e.Row.Cells[3].Text, 0)];
            LinkButton btnReceive = (LinkButton)e.Row.FindControl("btnReceive");
            LinkButton btnCancel = (LinkButton)e.Row.FindControl("btnCancel");
            LinkButton btnConsume = (LinkButton)e.Row.FindControl("btnConsume");
            LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
            HiddenField hidUseState = (HiddenField)e.Row.FindControl("hidUseState");
            HiddenField hidBillID = (HiddenField)e.Row.FindControl("hidBillID");
            HiddenField hidBillNo = (HiddenField)e.Row.FindControl("hidBillNo");
            switch (hidUseState.Value)
            {
                case "1":
                    btnReceive.Enabled = false;
                    btnEdit.Enabled = false;
                    break;
                case "2":
                case "3":
                    btnReceive.Enabled = false;
                    btnCancel.Enabled = false;
                    btnConsume.Enabled = false;
                    btnEdit.Enabled = false;
                    break;
                default:
                    btnConsume.Enabled = false;
                    break;
            }
            if (btnReceive.Enabled)
            {
                btnReceive.Attributes["onclick"] = string.Concat("return doCheck('CheckReceive.aspx','", hidBillID.Value, "','", hidBillNo.Value, "')");
            }
            if (btnConsume.Enabled)
            {
                btnConsume.Attributes["onclick"] = string.Concat("return doCheck('CheckConsume.aspx','", hidBillID.Value, "','", hidBillNo.Value, "')");
            }
            if (btnEdit.Enabled)
            {
                btnEdit.Attributes["onclick"] = string.Concat("return doCheck('CheckEdit.aspx','", hidBillID.Value, "','", hidBillNo.Value, "')");
            }
            if (btnCancel.Enabled)
            {
                btnCancel.Attributes.Add("onclick", "javascript:return confirm('您确定需要作废该支票吗？此操作不可恢复！')");
            }
            LinkButton btnShow = (LinkButton)e.Row.FindControl("btnShow");
            btnShow.Attributes.Add("onclick", string.Concat("return showBill('", hidBillID.Value, "')"));
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除吗？')");
            e.Row.Attributes["onmouseover"] = "bgColor=this.style.backgroundColor;this.style.backgroundColor='#dad5cc';fontColor=this.style.color;this.style.color='red';";
            e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=bgColor;this.style.color=fontColor;";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
