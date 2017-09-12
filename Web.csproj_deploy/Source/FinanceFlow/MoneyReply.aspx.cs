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

public partial class FinanceFlow_MoneyReply : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            FlowID.Value = Request.QueryString["id"];
            UtilsPage.SetTextBoxCalendar(ReplyDate);
            SaveReply.Attributes.Add("onclick", "return CheckSubmit();");
            SaveAndSubmit.Attributes.Add("onclick", "return CheckSubmit();");
            AccountName.Text = UserInfo.AccountName;
            TownName.Text = ValidateClass.ReadXMLNodeText(string.Format("FinancialDB/CUnits[ID='{0}']/UnitName", UserInfo.UnitID));
            FlowName.Text = CommClass.GetTableValue("cw_flowlist", "FlowName", string.Concat("id='", FlowID.Value, "'"));
            HasSelAppendices.Value = CommClass.GetTableValue("cw_flowlist", "Appendices1", string.Concat("id='", FlowID.Value, "'"));
            DataRow row = CommClass.GetDataRow(string.Concat("select FlowName,FlowCurrent,FlowState,ReplyDate,Appendices1,AuditState,AuditOpinion from cw_flowlist where id='", Request.QueryString["id"], "'"));
            if (row != null)
            {
                FlowName.Text = row["FlowName"].ToString();
                ReplyDate.Text = row["ReplyDate"].ToString();
                FlowCurrent.Value = row["FlowCurrent"].ToString();
                FlowState.Value = row["FlowState"].ToString();
                AuditState.Text = row["AuditState"].ToString();
                AuditOpinion.Text = row["AuditOpinion"].ToString();
                HasSelAppendices.Value = row["Appendices1"].ToString();
                if (ReplyDate.Text.Length == 0)
                {
                    ReplyDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            lbnUploadFile.Attributes.Add("onclick", "return UploadFile();");
            lbnShowAppendices.Attributes.Add("onclick", string.Concat("return ShowAppendices('", FlowID.Value, "');"));
            InitWebControl();
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        if (FlowState.Value == "2" || FlowState.Value == "3")
        {
            SaveReply.Enabled = false;
            SaveAndSubmit.Enabled = false;
        }
        DataTable dt = CommClass.GetDataTable(string.Concat("select * from cw_flowmoney where flowid='", FlowID.Value, "' order by id desc"));
        if (dt.Rows.Count == 0)
        {
            SaveReply.Enabled = false;
            SaveAndSubmit.Enabled = false;
            PageClass.BindNoRecords(GridView1, dt);
        }
        else
        {
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
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
        sql.Add("ApplyNotes", ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text);
        CommClass.ExecuteSQL("CW_FlowMoney", sql, string.Concat("id='", GridView1.DataKeys[e.RowIndex].Value.ToString(), "'"));
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox tbox = (TextBox)e.Row.FindControl("ReplyMoney");
            UtilsPage.SetTextBoxAutoValue(tbox, 0);
        }
    }
    protected void SaveReply_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> sql = new Dictionary<string, string>();
        sql.Add("ReplyDate", ReplyDate.Text);
        sql.Add("AuditState", AuditState.SelectedValue);
        sql.Add("AuditOpinion", AuditOpinion.Text);
        sql.Add("Appendices1", HasSelAppendices.Value);
        Button btn = (Button)sender;
        if (btn.ID == "SaveAndSubmit")
        {
            if (AuditState.SelectedValue == "1")
            {
                FlowState.Value = "2";
                FlowCurrent.Value = "2";
                sql.Add("FlowCurrent", FlowCurrent.Value);
            }
            else
            {
                FlowState.Value = "3";
            }
            sql.Add("FlowState", FlowState.Value);
            MainClass.ExecuteSQL(string.Format("update cw_balancealarm set DoState='1' where id='{0}'", Request.QueryString["bid"]));
        }
        if (AuditState.SelectedValue == "1")
        {
            DataTable dt = CommClass.GetDataTable(string.Concat("select id,ReplyMoney from cw_flowmoney where flowid='", FlowID.Value, "' order by id desc"));
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                dt.Rows[i]["ReplyMoney"] = ((TextBox)GridView1.Rows[i].FindControl("ReplyMoney")).Text;
            }
            CommClass.UpdateDataTable(dt, "id,ReplyMoney");
        }
        else
        {
            CommClass.ExecuteSQL(string.Concat("update cw_flowmoney set ReplyMoney=0 where flowid='", FlowID.Value, "'"));
        }
        CommClass.ExecuteSQL("CW_FlowList", sql, string.Concat("id='", FlowID.Value, "'"));
        PageClass.ShowAlertMsg(this.Page, "保存成功！");
        InitWebControl();
    }
}
