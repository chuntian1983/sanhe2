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

public partial class SysManage_BalanceAlarmDo : System.Web.UI.Page
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
        sql.Append("$AlarmType='0'");
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
            e.Row.Cells[0].Text = ValidateClass.ReadXMLNodeText(string.Format("FinancialDB/CUnits[ID='{0}']/UnitName", e.Row.Cells[0].Text));
            e.Row.Cells[1].Text = MainClass.GetFieldFromID(e.Row.Cells[1].Text, "accountname", "cw_account");
            e.Row.Cells[4].Text = (e.Row.Cells[4].Text == "0" ? "未审核" : "已审核");
            LinkButton btnAudit = (LinkButton)e.Row.FindControl("btnAudit");
            LinkButton btnMonitor = (LinkButton)e.Row.FindControl("btnMonitor");
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            if (e.Row.Cells[1].Text == "NoDataItem")
            {
                e.Row.Cells[1].Text = "账套已删除";
                btnAudit.Enabled = false;
                btnMonitor.Enabled = false;
            }
            if (btnAudit.CommandArgument == "0")
            {
                btnAudit.Text = "审&nbsp;&nbsp;&nbsp;&nbsp;核";
                btnAudit.Attributes.Add("onclick", "javascript:return confirm('您确定需要审核该凭证吗？')");

            }
            else
            {
                btnAudit.Text = "反审核";
                btnAudit.Attributes.Add("onclick", "javascript:return confirm('您确定需要反审核该凭证吗？')");
            }
            btnMonitor.Attributes["onclick"] = string.Format("return OpenMonitor('{0}','{1}');", btnMonitor.CommandArgument, btnMonitor.CommandName);
            btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除报警信息吗？')");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        GridView1.EditIndex = -1;
        LinkButton btnDelete = (LinkButton)sender;
        if (MainClass.CheckExist("cw_account", string.Format("id='{0}'", btnDelete.CommandArgument)))
        {
            UserInfo.AccountID = btnDelete.CommandArgument;
            string voucherID = MainClass.GetFieldFromID(btnDelete.CommandName, "VoucherID", "cw_balancealarm");
            CommClass.ExecuteSQL(string.Format("update cw_voucher set IsHasAlarm='0' where id='{0}'", voucherID));
        }
        MainClass.ExecuteSQL(string.Format("delete from cw_balancealarm where id='{0}'", btnDelete.CommandName));
        PageClass.ShowAlertMsg(this.Page, "报警信息删除成功！");
        InitWebControl();
    }
    protected void btnAudit_Click(object sender, EventArgs e)
    {
        LinkButton btnAudit = (LinkButton)sender;
        DataRow row = MainClass.GetDataRow(string.Format("select AccountID,VoucherID from cw_balancealarm where id='{0}'", btnAudit.CommandName));
        UserInfo.AccountID = row["AccountID"].ToString();
        string voucherID = row["VoucherID"].ToString();
        if (CommClass.CheckExist("cw_voucher", string.Format("id='{0}'", voucherID)))
        {
            if (btnAudit.CommandArgument == "0")
            {
                MainClass.ExecuteSQL(string.Format("update cw_balancealarm set DoState='1' where id='{0}'", btnAudit.CommandName));
                CommClass.ExecuteSQL(string.Format("update cw_voucher set IsAuditing='1' where id='{0}'", voucherID));
            }
            else
            {
                if (CommClass.GetTableValue("cw_voucher", "IsRecord", string.Format("id='{0}'", voucherID)) == "0")
                {
                    MainClass.ExecuteSQL(string.Format("update cw_balancealarm set DoState='0' where id='{0}'", btnAudit.CommandName));
                    CommClass.ExecuteSQL(string.Format("update cw_voucher set IsAuditing='0' where id='{0}'", voucherID));
                }
                else
                {
                    PageClass.ShowAlertMsg(this.Page, "该凭证已记账不可反审核！");
                }
            }
        }
        else
        {
            PageClass.ShowAlertMsg(this.Page, "该凭证已删除不可审核！");
        }
        InitWebControl();
    }
}
