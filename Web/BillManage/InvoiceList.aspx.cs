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

public partial class BillManage_InvoiceList : System.Web.UI.Page
{
    private string[] arrInvoiceState = { "未使用", "已开票", "已作废", "已丢失" };
    private string[] arrInvoiceType = { "增值税专用发票", "普通发票" };
    
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            UtilsPage.SetTextBoxCalendar(SDate, "");
            UtilsPage.SetTextBoxCalendar(EDate, "");
            UtilsPage.SetTextBoxCalendar(VSDate, "");
            UtilsPage.SetTextBoxCalendar(VEDate, "");
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
        if (InvoiceType.SelectedValue != "-")
        {
            QueryString += "$InvoiceType='" + InvoiceType.SelectedValue + "'";
        }
        if (InvoiceState.SelectedValue != "-")
        {
            if (InvoiceState.SelectedValue == "4")
            {
                QueryString += "$IsRedInvoice='1'";
            }
            else
            {
                QueryString += "$InvoiceState='" + InvoiceState.SelectedValue + "'";
            }
        }
        if (SDate.Text.Length > 0)
        {
            QueryString += "$BuyDate>='" + SDate.Text + "'";
        }
        if (EDate.Text.Length > 0)
        {
            QueryString += "$BuyDate<='" + EDate.Text + "'";
        }
        if (VSDate.Text.Length > 0)
        {
            QueryString += "$OldTestDate>='" + VSDate.Text + "'";
        }
        if (VEDate.Text.Length > 0)
        {
            QueryString += "$OldTestDate<='" + VEDate.Text + "'";
        }
        if (OldTestState.Checked)
        {
            QueryString += "$OldTestState='1'";
        }
        else
        {
            QueryString += "$OldTestState='0'";
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataSet ds = CommClass.GetDataSet("select * from cw_billinvoice " + QueryString + " order by InvoiceCode,InvoiceNo");
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
        CommClass.ExecuteSQL("update cw_billinvoice set InvoiceState='2' where id='" + btn.CommandArgument + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void btnLost_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        CommClass.ExecuteSQL("update cw_billinvoice set InvoiceState='3' where id='" + btn.CommandArgument + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void btnOldTest_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        CommClass.ExecuteSQL("update cw_billinvoice set OldTestState='1' where id='" + btn.CommandArgument + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        CommClass.ExecuteSQL("delete from cw_billinvoice where id='" + btn.CommandArgument + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = arrInvoiceType[TypeParse.StrToInt(e.Row.Cells[0].Text, 0)];
            e.Row.Cells[3].Text = arrInvoiceState[TypeParse.StrToInt(e.Row.Cells[3].Text, 0)];
            e.Row.Cells[5].Text = (e.Row.Cells[5].Text == "1" ? "是" : "否");
            LinkButton btnDoBill = (LinkButton)e.Row.FindControl("btnDoBill");
            LinkButton btnSetRed = (LinkButton)e.Row.FindControl("btnSetRed");
            LinkButton btnCancel = (LinkButton)e.Row.FindControl("btnCancel");
            LinkButton btnLost = (LinkButton)e.Row.FindControl("btnLost");
            LinkButton btnOldTest = (LinkButton)e.Row.FindControl("btnOldTest");
            LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            HiddenField hidInvoiceState = (HiddenField)e.Row.FindControl("hidInvoiceState");
            HiddenField hidOldTestState = (HiddenField)e.Row.FindControl("hidOldTestState");
            HiddenField hidInvoiceID = (HiddenField)e.Row.FindControl("hidInvoiceID");
            HiddenField hidInvoiceNo = (HiddenField)e.Row.FindControl("hidInvoiceNo");
            if (hidOldTestState.Value == "1")
            {
                btnDoBill.Enabled = false;
                btnCancel.Enabled = false;
                btnLost.Enabled = false;
                btnSetRed.Enabled = false;
                btnEdit.Enabled = false;
                btnOldTest.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                switch (hidInvoiceState.Value)
                {
                    case "0":
                        btnSetRed.Enabled = false;
                        break;
                    case "1":
                        btnDoBill.Enabled = false;
                        btnLost.Enabled = false;
                        btnEdit.Enabled = false;
                        break;
                    case "2":
                    case "3":
                        btnDoBill.Enabled = false;
                        btnCancel.Enabled = false;
                        btnLost.Enabled = false;
                        btnSetRed.Enabled = false;
                        btnEdit.Enabled = false;
                        break;
                }
                if (btnSetRed.CommandArgument == "1")
                {
                    btnSetRed.Enabled = false;
                }
            }
            if (btnDoBill.Enabled)
            {
                btnDoBill.Attributes["onclick"] = string.Concat("return doCheck('InvoiceDoBill.aspx?bid=", hidInvoiceID.Value, "&bno=", hidInvoiceNo.Value, "')");
            }
            if (btnSetRed.Enabled)
            {
                btnSetRed.Attributes["onclick"] = string.Concat("return doCheck('InvoiceDoBill.aspx?bid=", hidInvoiceID.Value, "&bno=", hidInvoiceNo.Value, "&red=1')");
            }
            if (btnEdit.Enabled)
            {
                btnEdit.Attributes["onclick"] = string.Concat("return doCheck('InvoiceEdit.aspx?bid=", hidInvoiceID.Value, "&bno=", hidInvoiceNo.Value, "')");
            }
            if (btnCancel.Enabled)
            {
                btnCancel.Attributes.Add("onclick", "javascript:return confirm('您确定需要作废该支票吗？此操作不可恢复！')");
            }
            if (btnLost.Enabled)
            {
                btnLost.Attributes.Add("onclick", "javascript:return confirm('您确定需要丢失处理该支票吗？此操作不可恢复！')");
            }
            if (btnOldTest.Enabled)
            {
                btnOldTest.Attributes.Add("onclick", "javascript:return confirm('您确定需要验旧处理该支票吗？此操作不可恢复！')");
            }
            if (btnDelete.Enabled)
            {
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除吗？')");
            }
            LinkButton btnShow = (LinkButton)e.Row.FindControl("btnShow");
            btnShow.Attributes.Add("onclick", string.Concat("return showBill('", hidInvoiceID.Value, "')"));
            e.Row.Attributes["onmouseover"] = "bgColor=this.style.backgroundColor;this.style.backgroundColor='#dad5cc';fontColor=this.style.color;this.style.color='red';";
            e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=bgColor;this.style.color=fontColor;";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
