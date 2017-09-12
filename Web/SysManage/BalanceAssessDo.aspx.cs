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

public partial class SysManage_BalanceAssessDo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            InitWebControl();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }

    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        StringBuilder sql = new StringBuilder();
        sql.Append("$AlarmType='2'");
        if (DoState.SelectedValue != "000000")
        {
            sql.AppendFormat("$DoState='{0}'", DoState.SelectedValue);
        }
        if (TownName.Text.Length > 0)
        {
            string townID = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[UnitName='" + TownName.Text + "']/ID");
            sql.AppendFormat("$UnitID='{0}'", townID);
        }
        if (AccountName.Text.Length > 0)
        {
            sql.AppendFormat("$AccountID in (select id from cw_account where accountname like '%{0}%')", AccountName.Text);
        }
        if (sql.Length > 0)
        {
            sql.Remove(0, 1).Replace("$", " and ").Insert(0, " where ");
        }
        sql.Insert(0, "select * from cw_balancealarm ");
        sql.Append(" order by BookTime desc");
        DataSet ds = MainClass.GetDataSet(sql.ToString());
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
            if (GridView1.AllowPaging)
            {
                DropDownList ddl = (DropDownList)GridView1.BottomPagerRow.Cells[0].FindControl("JumpPage");
                ddl.Items.Clear();
                for (int i = 0; i < GridView1.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem("第" + (i + 1).ToString() + "页", i.ToString()));
                }
                ddl.SelectedIndex = GridView1.PageIndex;
            }
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
            LinkButton btnAudit = (LinkButton)e.Row.FindControl("btnAudit");
            if (e.Row.Cells[4].Text == "0")
            {
                e.Row.Cells[4].Text = "未评估";
                e.Row.Cells[5].Text = "&nbsp;";
            }
            else
            {
                e.Row.Cells[4].Text = "已评估";
                btnAudit.Text = "查看";
                UserInfo.AccountID = e.Row.Cells[1].Text;
                e.Row.Cells[5].Text = CommClass.GetFieldFromID(e.Row.Cells[5].Text.Replace("Assess", ""), "AssessDate", "cw_flowlist");
            }
            if (e.Row.Cells[1].Text == "NoDataItem")
            {
                e.Row.Cells[1].Text = "账套已删除";
                btnAudit.Enabled = false;
            }
            switch (e.Row.Cells[2].Text)
            {
                case "0":
                    e.Row.Cells[2].Text = "资金";
                    break;
                case "1":
                    e.Row.Cells[2].Text = "资产";
                    break;
                case "2":
                    e.Row.Cells[2].Text = "资源";
                    break;
            }
            e.Row.Cells[0].Text = ValidateClass.ReadXMLNodeText(string.Format("FinancialDB/CUnits[ID='{0}']/UnitName", e.Row.Cells[0].Text));
            e.Row.Cells[1].Text = MainClass.GetFieldFromID(e.Row.Cells[1].Text, "accountname", "cw_account");
        }
    }

    protected void btnAudit_Click(object sender, EventArgs e)
    {
        LinkButton btnAudit = (LinkButton)sender;
        if (MainClass.CheckExist("cw_account", string.Format("id='{0}'", btnAudit.CommandArgument)))
        {
            UserInfo.AccountID = btnAudit.CommandArgument;
            string voucherID = MainClass.GetFieldFromID(btnAudit.CommandName, "VoucherID", "cw_balancealarm").Replace("Assess", "");
            switch (MainClass.GetFieldFromID(btnAudit.CommandName, "VoucherNo", "cw_balancealarm"))
            {
                case "0":
                    Response.Redirect("../FinanceFlow/MoneyAssess.aspx?bid=" + btnAudit.CommandName + "&id=" + voucherID);
                    break;
                case "1":
                    Response.Redirect("../FinanceFlow/AssetAssess.aspx?bid=" + btnAudit.CommandName + "&id=" + voucherID);
                    break;
                case "2":
                    Response.Redirect("../FinanceFlow/ResourceAssess.aspx?bid=" + btnAudit.CommandName + "&id=" + voucherID);
                    break;
            }
        }
    }
}
