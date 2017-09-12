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

public partial class FinanceFlow_ResourceAssess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            FlowID.Value = Request.QueryString["id"];
            UtilsPage.SetTextBoxCalendar(AssessDate);
            SaveAssess.Attributes.Add("onclick", "return CheckSubmit();");
            SaveAndSubmit.Attributes.Add("onclick", "return CheckSubmit();");
            lbnUploadFile.Attributes.Add("onclick", "return UploadFile();");
            AccountName.Text = UserInfo.AccountName;
            TownName.Text = ValidateClass.ReadXMLNodeText(string.Format("FinancialDB/CUnits[ID='{0}']/UnitName", UserInfo.UnitID));
            InitWebControl();
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        DataRow row = CommClass.GetDataRow(string.Concat("select FlowName,FlowCurrent,ApplyDate,AssessDate,FlowState,Appendices2 from cw_flowlist where id='", FlowID.Value, "'"));
        if (row != null)
        {
            FlowName.Text = row["FlowName"].ToString();
            AssessDate.Text = row["AssessDate"].ToString();
            FlowState.Value = row["FlowState"].ToString();
            FlowCurrent.Value = row["FlowCurrent"].ToString();
            HasSelAppendices.Value = row["Appendices2"].ToString();
            ApplyDate.Value = row["ApplyDate"].ToString();
            if (AssessDate.Text.Length == 0)
            {
                AssessDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
        DataTable dt = CommClass.GetDataTable(string.Concat("select * from cw_flowasset where flowid='", FlowID.Value, "' order by id desc"));
        if (dt.Rows.Count == 0)
        {
            SaveAssess.Enabled = false;
            SaveAndSubmit.Enabled = false;
            PageClass.BindNoRecords(GridView1, dt);
        }
        else
        {
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            if (FlowCurrent.Value != "1")
            {
                SaveAssess.Enabled = false;
                SaveAndSubmit.Enabled = false;
                GridView1.Columns[8].Visible = false;
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        CommClass.ExecuteSQL("delete from cw_flowasset where id='" + btn.CommandArgument + "'");
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
        sql.Add("AssessBasePrice", ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text);
        CommClass.ExecuteSQL("CW_FlowAsset", sql, string.Concat("id='", GridView1.DataKeys[e.RowIndex].Value.ToString(), "'"));
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = CommClass.GetFieldFromID(e.Row.Cells[0].Text, "AssetName", "cw_assetcard");
        }
    }
    protected void SaveApply_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> sql = new Dictionary<string, string>();
        sql.Add("AssessDate", AssessDate.Text);
        sql.Add("Appendices2", HasSelAppendices.Value);
        Button btn = (Button)sender;
        if (btn.ID == "SaveAndSubmit")
        {
            FlowCurrent.Value = "2";
            sql.Add("FlowCurrent", FlowCurrent.Value);
            CommClass.ExecuteSQL("CW_FlowList", sql, string.Concat("id='", FlowID.Value, "'"));
            MainClass.ExecuteSQL(string.Format("update cw_balancealarm set DoState='1' where id='{0}'", Request.QueryString["bid"]));
            MainClass.ExecuteSQL(string.Format("insert into cw_balancealarm(id,UnitID,AccountID,VoucherID,VoucherNo,VoucherDate,AlarmType,DoState,BookTime)values('{0}','{1}','{2}','Reply{3}','{4}','{5}','{6}','{7}','{8}')",
                                new object[] { MainClass.GetRecordID("CW_BalanceAlarm"), MainClass.GetFieldFromID(Request.QueryString["bid"], "UnitID", "cw_balancealarm"), UserInfo.AccountID, FlowID.Value, "2", ApplyDate.Value, "1", "0", DateTime.Now.ToString() }));
        }
        else
        {
            CommClass.ExecuteSQL("CW_FlowList", sql, string.Concat("id='", FlowID.Value, "'"));
        }
        PageClass.ShowAlertMsg(this.Page, "保存成功！");
        InitWebControl();
    }
}
