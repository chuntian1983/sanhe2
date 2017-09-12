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

public partial class Contract_ContractList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            AName.Text = UserInfo.AccountName;
            ReportDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            SLease.Attributes.Add("readonly", "readonly");
            SLease.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].SLease,'yyyy-mm-dd')");
            ELease.Attributes.Add("readonly", "readonly");
            ELease.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].ELease,'yyyy-mm-dd')");
            InitWebControl();
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        string QPay = string.Empty;
        string QueryString = string.Empty;
        if (CardID.Text.Length > 0) { QueryString += "$CardNo like '%" + CardID.Text + "%'"; }
        if (ContractNo.Text.Length > 0) { QueryString += "$ContractNo like '%" + ContractNo.Text + "%'"; }
        if (ContractName.Text.Length > 0) { QueryString += "$ContractName like '%" + ContractName.Text + "%'"; }
        if (LeaseHolder.Text.Length > 0) { QueryString += "$LeaseHolder like '%" + LeaseHolder.Text + "%'"; }
        string[] Relation = { "=", "<>", ">", "<=", "<", ">=" };
        string R0 = Relation[Relation0.SelectedIndex];
        string R1 = Relation[Relation1.SelectedIndex];
        if (DateType.SelectedValue == "0")
        {
            if (SLease.Text.Length > 0)
            {
                QueryString += "$StartLease " + R0 + " '" + SLease.Text + "'";
            }
            if (ELease.Text.Length > 0)
            {
                QueryString += "$EndLease " + R1 + " '" + ELease.Text + "'";
            }
        }
        else
        {
            if (SLease.Text.Length > 0)
            {
                QPay += "$StartPay " + R0 + " '" + SLease.Text + "'";
            }
            if (ELease.Text.Length > 0)
            {
                QPay += "$EndPay " + R1 + " '" + ELease.Text + "'";
            }
            if (LeaseState.SelectedValue != "0" && LeaseState.SelectedValue != "1")
            {
                QueryString += "$id in (select cardid from cw_respayperiod where 1=1 " + QPay + ")";
            }
        }
        switch (LeaseState.SelectedValue)
        {
            case "0":
                QueryString += "$LeaseState='0'";
                QueryString += "$id in (select cardid from cw_respayperiod where PayState='0' " + QPay + ")";
                break;
            case "1":
                QueryString += "$LeaseState='0'";
                QueryString += "$id in (select cardid from cw_respayperiod where PayState='1' " + QPay + ")";
                break;
            case "2":
                QueryString += "$LeaseState<>'0'";
                break;
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataSet ds = CommClass.GetDataSet("select * from cw_resleasecard " + QueryString + " order by cardno desc");
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
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        Response.Redirect(string.Format("LeaseCardModify.aspx?id={0}&ctype={1}", btn.CommandName, btn.CommandArgument));
    }
    protected void btnTurn_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        DataRow row = CommClass.GetDataRow(string.Format("select ResourceID,ResAmount,CardType from cw_resleasecard where id='{0}'", btn.CommandName));
        string cardType = row["CardType"].ToString();
        string resourceID = row["ResourceID"].ToString();
        decimal ResAmount = TypeParse.StrToDecimal(row["ResAmount"].ToString(), 0);
        if (cardType == "0")
        {
            if (btn.CommandArgument == "1")
            {
                decimal HasAmount = TypeParse.StrToDecimal(CommClass.GetFieldFromID(resourceID, "AAmount-HasAmount", "cw_assetcard"), 0);
                if (ResAmount > HasAmount)
                {
                    PageClass.ShowAlertMsg(this.Page, string.Format("可流转数量或面积小于{0}", ResAmount));
                }
                else
                {
                    CommClass.ExecuteSQL(string.Format("update cw_resleasecard set LeaseState='0' where id='{0}'", btn.CommandName));
                    CommClass.ExecuteSQL(string.Format("update cw_assetcard set HasAmount=HasAmount+({0}) where id='{1}'", ResAmount, resourceID));
                }
            }
            else
            {
                CommClass.ExecuteSQL(string.Format("update cw_resleasecard set LeaseState='1' where id='{0}'", btn.CommandName));
                CommClass.ExecuteSQL(string.Format("update cw_assetcard set HasAmount=HasAmount-({0}) where id='{1}'", ResAmount, resourceID));
            }
        }
        else if (cardType == "1")
        {
            if (btn.CommandArgument == "1")
            {
                decimal HasAmount = TypeParse.StrToDecimal(CommClass.GetFieldFromID(resourceID, "ResAmount-HasAmount", "cw_rescard"), 0);
                if (ResAmount > HasAmount)
                {
                    PageClass.ShowAlertMsg(this.Page, string.Format("可流转数量或面积小于{0}", ResAmount));
                }
                else
                {
                    CommClass.ExecuteSQL(string.Format("update cw_resleasecard set LeaseState='0' where id='{0}'", btn.CommandName));
                    CommClass.ExecuteSQL(string.Format("update cw_rescard set HasAmount=HasAmount+({0}) where id='{1}'", ResAmount, resourceID));
                }
            }
            else
            {
                CommClass.ExecuteSQL(string.Format("update cw_resleasecard set LeaseState='1' where id='{0}'", btn.CommandName));
                CommClass.ExecuteSQL(string.Format("update cw_rescard set HasAmount=HasAmount-({0}) where id='{1}'", ResAmount, resourceID));
            }
        }
        else
        {
            if (btn.CommandArgument == "1")
            {
                CommClass.ExecuteSQL(string.Format("update cw_resleasecard set LeaseState='0' where id='{0}'", btn.CommandName));
            }
            else
            {
                CommClass.ExecuteSQL(string.Format("update cw_resleasecard set LeaseState='1' where id='{0}'", btn.CommandName));
            }
        }
        InitWebControl();
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        if (btn.CommandArgument == "0")
        {
            DataRow row = CommClass.GetDataRow(string.Format("select ResourceID,ResAmount from cw_resleasecard where id='{0}'", btn.CommandName));
            string resourceID = row["ResourceID"].ToString();
            string ResAmount = row["ResAmount"].ToString();
            if (Request.QueryString["ctype"] == "0")
            {
                CommClass.ExecuteSQL(string.Format("update cw_assetcard set HasAmount=HasAmount-({0}) where id='{1}'", ResAmount, resourceID));
            }
            else if (Request.QueryString["ctype"] == "1")
            {
                CommClass.ExecuteSQL(string.Format("update cw_rescard set HasAmount=HasAmount-({0}) where id='{1}'", ResAmount, resourceID));
            }
        }
        CommClass.ExecuteSQL("delete from cw_respayperiod where cardid='" + btn.CommandName + "'");
        CommClass.ExecuteSQL("delete from cw_resleasecard where id='" + btn.CommandName + "'");
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnCardShow = (LinkButton)e.Row.FindControl("btnCardShow");
            btnCardShow.Attributes["onclick"] = string.Format("return ShowLeaseCard({1},'{0}');", btnCardShow.CommandName, btnCardShow.CommandArgument);
            LinkButton btnPay = (LinkButton)e.Row.FindControl("btnPay");
            btnPay.Attributes["onclick"] = string.Format("return LeasePay('{0}','{1}');", btnPay.CommandName, btnPay.CommandArgument);
            LinkButton btnTurn = (LinkButton)e.Row.FindControl("btnTurn");
            if (btnTurn.CommandArgument == "0")
            {
                btnTurn.Attributes.Add("onclick", "javascript:return confirm('您确定需要终止合同吗？')");
            }
            else
            {
                btnTurn.Text = "启用";
            }
            LinkButton btnDel = (LinkButton)e.Row.FindControl("btnDel");
            btnDel.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除吗？')");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
    protected void OutputDataToExcel_Click(object sender, EventArgs e)
    {
        InitWebControl();
        GridView1.Columns[7].Visible = false;
        GridView1.Columns[8].Visible = false;
        GridView1.Columns[9].Visible = false;
        GridView1.Columns[10].Visible = false;
        GridView1.Columns[11].Visible = false;
        PageClass.ToExcel(GridView1);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}
