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

public partial class AccountCollect_MonitorFixedAsset : System.Web.UI.Page
{
    private int RunLevel = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            GridView1.Attributes.Add("onselectstart", "return false;");
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            UnitName.Attributes["onclick"] = "SelectUnit()";
            UnitName.Attributes["readonly"] = "readonly";
            ReportDate.Text = DateTime.Now.ToString("yyyy年MM月");
            SMinus.Attributes["onclick"] = "return setYear('SelYear',-1);";
            SPlus.Attributes["onclick"] = "return setYear('SelYear',1);";
            SelYear.Attributes["readonly"] = "readonly";
            SelYear.Text = DateTime.Now.Year.ToString();
            SelMonth.Attributes["onchange"] = "setMonth(this.value);";
            SelMonth.Text = DateTime.Now.Month.ToString("00");
            UtilsPage.SetTextBoxAutoValue(IndexValue, "0");
            string UnitID = Session["UnitID"].ToString();
            string TopUnitID = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo", "TopUnitID");
            if (TopUnitID.Length == 0)
            {
                TopUnitID = "000000";
            }
            if (UnitID == "000000")
            {
                UnitID = TopUnitID;
            }
            DataTable subjects = MainClass.GetDataTable(string.Format("select parentno,subjectno,subjectname from cw_subject where parentno like '{0}%' and unitid='{1}' order by subjectno asc", SysConfigs.FixedAssetSubject, UnitID));
            InitSubject("固定资产", SysConfigs.FixedAssetSubject, subjects);
            InitWebControl();
        }
    }
    protected void InitSubject(string CName, string ParentID, DataTable subjects)
    {
        QList.Items.Add(new ListItem(new string('.', RunLevel * 6) + CName, ParentID));
        DataView DV = new DataView(subjects, "parentno='" + ParentID + "'", "", DataViewRowState.CurrentRows);
        RunLevel++;
        if (ParentID == SysConfigs.FixedAssetSubject) { RunLevel = 1; }
        for (int i = 0; i < DV.Count; i++)
        {
            InitSubject(DV[i]["subjectname"].ToString(), DV[i]["subjectno"].ToString(), subjects);
        }
        RunLevel--;
    }
    protected void InitWebControl()
    {
        AName.Text = UnitName.Text;
        DataTable BindTable = new DataTable();
        BindTable.Columns.Add("id");
        BindTable.Columns.Add("accountid");
        BindTable.Columns.Add("accountname");
        BindTable.Columns.Add("assetno");
        BindTable.Columns.Add("assetname");
        BindTable.Columns.Add("oldprice", typeof(double));
        BindTable.Columns.Add("zhejiu", typeof(double));
        BindTable.Columns.Add("jingcz", typeof(double));
        BindTable.Columns.Add("monthzjl");
        BindTable.Columns.Add("thiszj", typeof(double));
        string[] AccountList = GAccountList.Value.Split('$');
        for (int i = 0; i < AccountList.Length; i++)
        {
            if (AccountList[i].Length > 0)
            {
                FillDataTable(BindTable, AccountList[i]);
            }
        }
        BindGridView(BindTable);
    }
    private void FillDataTable(DataTable BindTable, string accountID)
    {
        UserInfo.AccountID = accountID;
        DataTable ve = CommClass.GetDataTable("select id,assetno,assetname,oldprice,zhejiu,jingcz,monthzjl,thiszj from cw_assetcard where (ClassID like '" + QList.SelectedValue + "%') and (BookDate like '"
            + SelYear.Text + "-" + SelMonth.SelectedValue + "%') and (oldprice>=" + IndexValue.Text + ") order by left(SUseDate,8),assetno");
        if (ve.Rows.Count > 0)
        {
            foreach (DataRow row in ve.Rows)
            {
                DataRow NewRow = BindTable.NewRow();
                NewRow["id"] = row["id"].ToString();
                NewRow["accountid"] = accountID;
                string unitid = MainClass.GetFieldFromID(accountID, "unitid", "cw_account");
                string towname = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + unitid + "']", "UnitName");
                NewRow["accountname"] = string.Format("{0}-{1}", towname, MainClass.GetFieldFromID(accountID, "accountname", "cw_account"));
                NewRow["assetno"] = row["assetno"].ToString();
                NewRow["assetname"] = row["assetname"].ToString();
                NewRow["oldprice"] = row["oldprice"].ToString();
                NewRow["zhejiu"] = row["zhejiu"].ToString();
                NewRow["jingcz"] = row["jingcz"].ToString();
                NewRow["monthzjl"] = row["monthzjl"].ToString();
                NewRow["thiszj"] = row["thiszj"].ToString();
                BindTable.Rows.Add(NewRow);
            }
            DataRow sumRow = BindTable.NewRow();
            sumRow["id"] = "";
            sumRow["accountid"] = "";
            sumRow["accountname"] = "SumRow";
            sumRow["assetno"] = "";
            sumRow["assetname"] = "";
            sumRow["oldprice"] = ve.Compute("sum(oldprice)", "");
            sumRow["zhejiu"] = ve.Compute("sum(zhejiu)", "");
            sumRow["jingcz"] = ve.Compute("sum(jingcz)", "");
            sumRow["monthzjl"] = "0";
            sumRow["thiszj"] = ve.Compute("sum(thiszj)", "");
            BindTable.Rows.Add(sumRow);
        }
    }
    protected void BindGridView(DataTable BindTable)
    {
        if (BindTable.Rows.Count == 0)
        {
            OutputDataToExcel.Enabled = false;
            PageClass.BindNoRecords(GridView1, BindTable);
        }
        else
        {
            OutputDataToExcel.Enabled = true;
            GridView1.DataSource = BindTable.DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            if (GridView1.AllowPaging)
            {
                Label lb = (Label)GridView1.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
                lb.Text = "记录数：" + BindTable.Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
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
    protected void btnShow_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        Response.Redirect("PrintFACard.aspx?id=" + btn.CommandArgument + "&aid=" + btn.CommandName);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == "SumRow")
            {
                e.Row.Cells[0].Text = "合计";
                e.Row.Cells[6].Text = "&nbsp;";
                e.Row.Cells[8].Text = "&nbsp;";
                e.Row.Style["color"] = "red";
            }
            else
            {
                LinkButton btn = (LinkButton)e.Row.FindControl("btnShow");
                btn.Attributes.Add("onclick", string.Format("return ShowVoucher('{0}','{1}');", btn.CommandArgument, btn.CommandName));
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
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
}
