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

public partial class Contract_TurnList : System.Web.UI.Page
{
    private int RunLevel = 0;
    
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
            if (Request.QueryString["ctype"] == "0")
            {
                InitFixedAsset("固定资产", SysConfigs.FixedAssetSubject);
                ReportTitle.InnerHtml = "资产流转一览表";
                GridView1.Columns[3].HeaderText = "资产名称";
                TD_Class.InnerText = "资产类别";
                TD_No.InnerText = "资产编号";
                TD_Name.InnerText = "资产名称";
            }
            else
            {
                InitResource("资源", "000");
                ReportTitle.InnerHtml = "资源流转一览表";
                GridView1.Columns[3].HeaderText = "资源名称";
                TD_Class.InnerText = "资源类别";
                TD_No.InnerText = "资源编号";
                TD_Name.InnerText = "资源名称";
            }
            InitWebControl();
        }
    }
    protected void InitResource(string CName, string ParentID)
    {
        QList.Items.Add(new ListItem(new string('.', RunLevel * 6) + CName, ParentID));
        DataSet ds = MainClass.GetDataSet("select id,classname from cw_resclass where parentid='" + ParentID + "' order by id asc");
        RunLevel++;
        if (ParentID == "000") { RunLevel = 1; }
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            InitResource(row["classname"].ToString(), row["id"].ToString());
        }
        RunLevel--;
    }
    protected void InitFixedAsset(string CName, string ParentID)
    {
        QList.Items.Add(new ListItem(new string('.', RunLevel * 6) + CName, ParentID));
        DataSet ds = CommClass.GetDataSet("select id,cname from cw_assetclass where pid='" + ParentID + "' order by id asc");
        RunLevel++;
        if (ParentID == "000") { RunLevel = 1; }
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            InitFixedAsset(row["cname"].ToString(), row["id"].ToString());
        }
        RunLevel--;
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        string QPay = string.Empty;
        string QueryString = "$cardtype='" + Request.QueryString["ctype"] + "'";
        if (CardID.Text.Length > 0) { QueryString += "$CardNo='" + CardID.Text + "'"; }
        if (AssetNo.Text.Length > 0) { QueryString += "$ResourceID='" + AssetNo.Text + "'"; }
        if (AssetName.Text.Length > 0) { QueryString += "$ResourceName like '%" + AssetName.Text + "%'"; }
        if (LeaseHolder.Text.Length > 0) { QueryString += "$LeaseHolder like '%" + LeaseHolder.Text + "%'"; }
        if (QList.SelectedValue != "000" && QList.SelectedValue != "000000")
        {
            if (Request.QueryString["ctype"] == "0")
            {
                QueryString += "$ResourceID in (select id from cw_assetcard where ClassID like '" + QList.SelectedValue + "%')";
            }
            else
            {
                QueryString += "$ResourceID in (select id from cw_rescard where ClassID like '" + QList.SelectedValue + "%')";
            }
        }
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
            //Label lb = (Label)GridView1.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
            //lb.Text = "记录数：" + ds.Tables[0].Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            //lb.Text += "总页数：" + (GridView1.PageIndex + 1) + "/" + GridView1.PageCount + "页";
            //DropDownList ddl = (DropDownList)GridView1.BottomPagerRow.Cells[0].FindControl("JumpPage");
            //ddl.Items.Clear();
            //for (int i = 0; i < GridView1.PageCount; i++)
            //{
            //    ddl.Items.Add(new ListItem("第" + (i + 1).ToString() + "页", i.ToString()));
            //}
            //ddl.SelectedIndex = GridView1.PageIndex;
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
            //--
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
    protected void OutputDataToExcel_Click(object sender, EventArgs e)
    {
        InitWebControl();
        PageClass.ToExcel(GridView1);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}
