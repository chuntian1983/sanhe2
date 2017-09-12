﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class FinanceFlow_MoneyApply : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000003")) { return; }
        if (!IsPostBack)
        {
            FlowID.Value = Request.QueryString["id"];
            UtilsPage.SetTextBoxCalendar(ApplyDate);
            UtilsPage.SetTextBoxCalendar(ReplyDate);
            UtilsPage.SetTextBoxAutoValue(ApplyMoney, "0");
            SaveApply.Attributes.Add("onclick", "return CheckSubmit();");
            SaveAndSubmit.Attributes.Add("onclick", "return CheckSubmit();");
            lbnUploadFile.Attributes.Add("onclick", "return UploadFile();");
            lbnShowAppendices.Attributes.Add("onclick", string.Concat("return ShowAppendices('", FlowID.Value, "',1);"));
            AddItem.Attributes.Add("onclick", "return CheckSubmit2();");
            AccountName.Text = UserInfo.AccountName;
            TownName.Text = ValidateClass.ReadXMLNodeText(string.Format("FinancialDB/CUnits[ID='{0}']/UnitName", UserInfo.UnitID));
            DataRow row = CommClass.GetDataRow(string.Concat("select * from cw_flowlist where id='", FlowID.Value, "'"));
            if (row != null)
            {
                FlowName.Text = row["FlowName"].ToString();
                ApplyDate.Text = row["ApplyDate"].ToString();
                FlowState.Value = row["FlowState"].ToString();
                FlowCurrent.Value = row["FlowCurrent"].ToString();
                HasSelAppendices.Value = row["Appendices0"].ToString();
                if (FlowState.Value == "2" || FlowState.Value == "3")
                {
                    ReplyArea.Visible = true;
                    ReplyDate.Text = row["ReplyDate"].ToString();
                    AuditState.Text = row["AuditState"].ToString() == "1" ? "同意" : "否决";
                    AuditOpinion.Text = row["AuditOpinion"].ToString();
                }
            }
            InitWebControl();
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        if (ApplyDate.Text.Length == 0)
        {
            ApplyDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        if (FlowState.Value != "0" || FlowCurrent.Value != "0")
        {
            EditArea.Visible = false;
            GridView1.Columns[3].ItemStyle.Width = 360;
            GridView1.Columns[4].Visible = false;
            GridView1.Columns[5].Visible = false;
            GridView1.Columns[6].Visible = true;
            lbnUploadFile.Text = "查看附件";
            lbnUploadFile.Attributes.Add("onclick", string.Concat("return ShowAppendices('", FlowID.Value, "',0);"));
        }
        if (FlowCurrent.Value == "2")
        {
            PageClass.ExcuteScript(this.Page, "$('btnShowPayBill').style.display='';");
        }
        DataTable dt = CommClass.GetDataTable(string.Concat("select * from cw_flowmoney where flowid='", FlowID.Value, "' order by id desc"));
        if (dt.Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, dt);
        }
        else
        {
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
        }
        ApplyCount.Value = dt.Rows.Count.ToString();
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
        CommClass.ExecuteSQL("delete from cw_flowmoney where id='" + btn.CommandArgument + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        InitWebControl();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Dictionary<string, string> sql = new Dictionary<string, string>();
        sql.Add("ApplyUsage", ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0]).Text);
        sql.Add("ApplyMoney", ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text);
        sql.Add("ApplyNotes", ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text);
        CommClass.ExecuteSQL("CW_FlowMoney", sql, string.Concat("id='", GridView1.DataKeys[e.RowIndex].Value.ToString(), "'"));
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除吗？')");
            LinkButton btnCreate = (LinkButton)e.Row.FindControl("btnCreate");
            if (e.Row.Cells[2].Text == "0.00")
            {
                btnCreate.Enabled = false;
            }
            else
            {
                HiddenField hidVoucherID = (HiddenField)e.Row.FindControl("hidVoucherID");
                if (hidVoucherID.Value.Length == 0)
                {
                    if (FlowState.Value == "2")
                    {
                        btnCreate.Attributes.Add("onclick", "return CreateVoucher('" + GridView1.DataKeys[e.Row.RowIndex].Value.ToString() + "');");
                    }
                    else
                    {
                        btnCreate.Enabled = false;
                    }
                }
                else
                {
                    btnCreate.Attributes.Add("onclick", "return ShowVoucher('" + hidVoucherID.Value + "');");
                    btnCreate.Text = "查看";
                }
            }
        }
    }
    protected void SaveApply_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> sql = new Dictionary<string, string>();
        sql.Add("FlowName", FlowName.Text);
        sql.Add("ApplyDate", ApplyDate.Text);
        sql.Add("Appendices0", HasSelAppendices.Value);
        Button btn = (Button)sender;
        if (btn.ID == "SaveAndSubmit")
        {
            FlowState.Value = "1";
            FlowCurrent.Value = "1";
            sql.Add("FlowState", FlowState.Value);
            sql.Add("FlowCurrent", FlowCurrent.Value);
            MainClass.ExecuteSQL(string.Format("insert into cw_balancealarm(id,UnitID,AccountID,VoucherID,VoucherNo,VoucherDate,AlarmType,DoState,BookTime)values('{0}','{1}','{2}','Reply{3}','{4}','{5}','{6}','{7}','{8}')",
                                new object[] { MainClass.GetRecordID("CW_BalanceAlarm"), UserInfo.UnitID, UserInfo.AccountID, FlowID.Value, "0", ApplyDate.Text, "1", "0", DateTime.Now.ToString() }));
        }
        CommClass.ExecuteSQL("CW_FlowList", sql, string.Concat("id='", FlowID.Value, "'"));
        PageClass.ShowAlertMsg(this.Page, "保存成功！");
        InitWebControl();
    }
    protected void AddItem_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> sql = new Dictionary<string, string>();
        sql.Add("ID", CommClass.GetRecordID("CW_FlowMoney"));
        sql.Add("FlowID", FlowID.Value);
        sql.Add("ApplyUsage", ApplyUsage.Text);
        sql.Add("ApplyMoney", ApplyMoney.Text);
        sql.Add("ApplyNotes", ApplyNotes.Text);
        CommClass.ExecuteSQL("CW_FlowMoney", sql);
        PageClass.ShowAlertMsg(this.Page, "添加成功！");
        ApplyUsage.Text = "";
        ApplyMoney.Text = "0";
        ApplyNotes.Text = "";
        InitWebControl();
    }
    protected void doPostBackButton_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
