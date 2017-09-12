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

public partial class Contract_DueRemindPay : System.Web.UI.Page
{
    private int RunLevel = 0;
    private string QName = "classname";
    private string QTable = "cw_resclass";

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
            SLease.Text = DateTime.Now.ToString("yyyy-MM-01");
            ELease.Attributes.Add("readonly", "readonly");
            ELease.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].ELease,'yyyy-mm-dd')");
            ELease.Text = DateTime.Now.ToString("yyyy-MM-") + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
            if (Request.QueryString["ctype"] == "0")
            {
                GridView1.Columns[1].HeaderText = "资产名称";
                TD_Class.InnerText = "资产类别";
                TD_No.InnerText = "资产编号";
                TD_Name.InnerText = "资产名称";
            }
            else
            {
                GridView1.Columns[1].HeaderText = "资源名称";
                TD_Class.InnerText = "资源类别";
                TD_No.InnerText = "资源编号";
                TD_Name.InnerText = "资源名称";
            }
            InitQList();
            InitWebControl();
        }
    }
    protected void InitQList()
    {
        if (QType.SelectedValue == "1")
        {
            QList.Items.Add(new ListItem(UserInfo.AccountName, "000000"));
            DataSet ds = CommClass.GetDataSet("select id," + QName + " from " + QTable + " order by id");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                QList.Items.Add(new ListItem(ds.Tables[0].Rows[i][QName].ToString(), ds.Tables[0].Rows[i]["id"].ToString()));
            }
        }
        else
        {
            if (Request.QueryString["ctype"] == "0")
            {
                InitFixedAsset("固定资产", SysConfigs.FixedAssetSubject);
            }
            else
            {
                InitResource("资源", "000");
            }
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
    protected void QType_SelectedIndexChanged(object sender, EventArgs e)
    {
        QList.Items.Clear();
        if (QType.SelectedValue == "0")
        {
            QName = "classname";
            QTable = "cw_resclass";
        }
        else
        {
            QName = "deptname";
            QTable = "cw_department";
        }
        InitQList();
        InitWebControl();
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        string QueryString = string.Format("$b.CardID=a.id and a.cardtype='{0}' and a.LeaseState='0' and b.PayState='0'", Request.QueryString["ctype"]);
        if (CardID.Text.Length > 0) { QueryString += "$a.CardNo='" + CardID.Text + "'"; }
        if (DoState.SelectedValue != "000000") { QueryString += "$b.DoState='" + DoState.SelectedValue + "'"; }
        if (AssetNo.Text.Length > 0) { QueryString += "$a.ResourceID='" + AssetNo.Text + "'"; }
        if (AssetName.Text.Length > 0) { QueryString += "$a.ResourceName like '%" + AssetName.Text + "%'"; }
        if (LeaseHolder.Text.Length > 0) { QueryString += "$a.LeaseHolder like '%" + LeaseHolder.Text + "%'"; }
        if (QList.SelectedValue != "000" && QList.SelectedValue != "000000")
        {
            if (QType.SelectedValue == "0")
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
            else
            {
                QueryString += "$a.ResourceID in (select id from cw_rescard where DeptName like '%" + QList.SelectedValue + "%')";
            }
        }
        string[] Relation = { "=", "<>", ">", "<=", "<", ">=" };
        if (SLease.Text.Length > 0)
        {
            QueryString += "$b.EndPay " + Relation[Relation0.SelectedIndex] + " '" + SLease.Text + "'";
        }
        if (ELease.Text.Length > 0)
        {
            QueryString += "$b.EndPay " + Relation[Relation1.SelectedIndex] + " '" + ELease.Text + "'";
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataSet ds = CommClass.GetDataSet("select a.id lid,a.cardno,a.resourcename,a.LeaseHolder,a.LinkTel,b.id,b.PeriodName,b.PayMoney,b.StartPay,b.EndPay,b.DoState from cw_resleasecard a,cw_respayperiod b " + QueryString + " order by b.EndPay asc");
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnPrint = (LinkButton)e.Row.FindControl("btnPrint");
            btnPrint.Attributes["onclick"] = string.Format("return PrintDueCard('{0}','{1}');", btnPrint.CommandArgument, btnPrint.CommandName);
            LinkButton btnDo = (LinkButton)e.Row.FindControl("btnDo");
            if (btnDo.CommandArgument == "1")
            {
                btnDo.Enabled = false;
            }
            else
            {
                btnDo.Attributes.Add("onclick", "javascript:return confirm('您确定需要设置为已处理状态吗？')");
            }
        }
    }
    protected void btnDo_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        CommClass.ExecuteSQL("update cw_respayperiod set DoState='1' where id='" + btn.CommandName + "'");
        InitWebControl();
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
