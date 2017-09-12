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

public partial class AccountCollect_FixedAssetSummary : System.Web.UI.Page
{
    private int RunLevel = 0;
    public string TableWidth = "750";
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            GridView1.Attributes.Add("onselectstart", "return false;");
            ReportDate.Text = DateTime.Now.ToString("yyyy年MM月");
            SMinus.Attributes["onclick"] = "return setYear('SelYear',-1);";
            SPlus.Attributes["onclick"] = "return setYear('SelYear',1);";
            SelYear.Attributes["readonly"] = "readonly";
            SelYear.Text = DateTime.Now.Year.ToString();
            InitSubject("固定资产", SysConfigs.FixedAssetSubject);
            DataRowCollection rows = MainClass.GetDataRows("select levelname,collectunits from cw_collectlevel where unitid='" + Session["UnitID"] + "'");
            if (rows != null)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    CollectUnit.Items.Add(new ListItem(rows[i]["levelname"].ToString(), rows[i]["collectunits"].ToString()));
                }
            }
            InitWebControl();
        }
    }
    protected void InitSubject(string CName, string ParentID)
    {
        SubjectList.Items.Add(new ListItem(new string('.', RunLevel * 6) + CName, ParentID));
        DataSet ds = MainClass.GetDataSet("select subjectno,subjectname from cw_subject where unitid='" + Session["UnitID"].ToString() + "' and parentno='" + ParentID + "' order by id asc");
        RunLevel++;
        if (ParentID == SysConfigs.FixedAssetSubject) { RunLevel = 1; }
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            InitSubject(row["subjectname"].ToString(), row["subjectno"].ToString());
        }
        RunLevel--;
    }
    protected void InitWebControl()
    {
        AName.Text = CollectUnit.SelectedItem.Text;
        //创建数据表
        DataTable BindTable = new DataTable();
        DataRowCollection rows = MainClass.GetDataRows("select subjectno,subjectname from cw_subject  where parentno='"
            + SubjectList.SelectedValue + "' and unitid='" + Session["UnitID"].ToString() + "' order by subjectno");
        if (rows == null)
        {
            return;
        }
        else
        {
            GridView1.Columns.Clear();
            BindTable.Columns.Add("F");
            BoundField head = new BoundField();
            head.ItemStyle.Width = 150;
            head.DataField = "F";
            head.HtmlEncode = false;
            head.ItemStyle.CssClass = "sbalance";
            GridView1.Columns.Add(head);
            for (int i = 0; i < rows.Count; i++)
            {
                //数量字段
                BindTable.Columns.Add("F0" + i.ToString());
                BoundField bf0 = new BoundField();
                bf0.HeaderText = rows[i]["subjectno"].ToString();
                bf0.ItemStyle.Width = 60;
                bf0.DataField = "F0" + i.ToString();
                bf0.HtmlEncode = false;
                bf0.ItemStyle.CssClass = "sbalance";
                bf0.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                GridView1.Columns.Add(bf0);
                //原值字段
                BindTable.Columns.Add("F1" + i.ToString());
                BoundField bf1 = new BoundField();
                bf1.HeaderText = rows[i]["subjectno"].ToString();
                bf1.ItemStyle.Width = 60;
                bf1.DataField = "F1" + i.ToString();
                bf1.HtmlEncode = false;
                bf1.ItemStyle.CssClass = "sbalance";
                bf1.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                GridView1.Columns.Add(bf1);
            }
            int _TableWidth = 160 + rows.Count * 124;
            //输出表格宽度
            if (_TableWidth < 750)
            {
                _TableWidth = 750;
            }
            GridView1.Width = _TableWidth;
            TableWidth = _TableWidth.ToString();
        }
        BindTable.Rows.Add(BindTable.NewRow());
        BindTable.Rows.Add(BindTable.NewRow());
        if (CollectUnit.SelectedValue != "000000")
        {
            if (CollectUnit.SelectedValue == "")
            {
                ExeScript.Text = "<script>alert('汇总单位【" + CollectUnit.SelectedItem.Text + "】下级无汇总账套！');</script>";
            }
            else
            {
                string _CollectUnit = "";
                StringBuilder AllAccount = new StringBuilder();
                StringBuilder NoCarryAccount = new StringBuilder();
                if (CollectUnit.SelectedValue == "XXXXXX")
                {
                    _CollectUnit = "-" + Session["UnitID"].ToString();
                }
                else
                {
                    _CollectUnit = CollectUnit.SelectedValue;
                }
                string[] _G = _CollectUnit.Split('-');
                AllAccount.Append(_G[0]);
                string[] UnitList = _G[1].Split('$');
                for (int i = 0; i < UnitList.Length; i++)
                {
                    GetCollectAccount(ref AllAccount, UnitList[i]);
                }
                if (AllAccount.Length == 0)
                {
                    ExeScript.Text = "<script>alert('汇总单位【" + CollectUnit.SelectedItem.Text + "】下级无汇总账套！');</script>";
                }
                else
                {
                    GAccountList.Value = AllAccount.ToString();
                    InitBindTable(BindTable);
                }
            }
        }
        //补充空行
        for (int i = BindTable.Rows.Count; i < 10; i++)
        {
            BindTable.Rows.Add(BindTable.NewRow());
        }
        BindTable.AcceptChanges();
        GridView1.DataSource = BindTable;
        GridView1.DataBind();
        GridView1.Rows[0].Cells[0].Text = "<center>科目名称→<br><br>账套名称↓</center>";
        GridView1.Rows[0].Cells[0].RowSpan = 2;
        GridView1.Rows[1].Cells[0].Visible = false;
        for (int i = 1; i < GridView1.Columns.Count; i += 2)
        {
            int rowIndex = (i + 1) / 2 - 1;
            if (rowIndex < rows.Count)
            {
                GridView1.Rows[0].Cells[i].Width = 120;
                GridView1.Rows[0].Cells[i].ColumnSpan = 2;
                GridView1.Rows[0].Cells[i].Text = string.Format("<center>{0}</center>", rows[rowIndex]["subjectname"].ToString());
                GridView1.Rows[0].Cells[i + 1].Visible = false;
                GridView1.Rows[1].Cells[i].Text = "<center>数量</center>";
                GridView1.Rows[1].Cells[i + 1].Text = "<center>原值</center>";
            }
        }
    }
    protected void GetCollectAccount(ref StringBuilder AllAccount, string UnitID)
    {
        DataSet ds = MainClass.GetDataSet("select id from cw_account where unitid='" + UnitID + "'");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            AllAccount.Append(row["id"].ToString() + "$");
        }
        DataRow[] rows = ValidateClass.GetRegRows("CUnits", "parentid='" + UnitID + "'");
        if (rows != null)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                GetCollectAccount(ref AllAccount, rows[i]["id"].ToString());
            }
        }
    }
    protected void InitBindTable(DataTable BindTable)
    {
        string[] AccountList = GAccountList.Value.Split('$');
        for (int i = 0; i < AccountList.Length; i++)
        {
            if (AccountList[i].Length == 0)
            {
                continue;
            }
            UserInfo.AccountID = AccountList[i];
            DataRow NewRow = BindTable.NewRow();
            string unitid = MainClass.GetFieldFromID(AccountList[i], "unitid", "cw_account");
            string towname = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + unitid + "']", "UnitName");
            NewRow[0] = string.Format("{0}，{1}", towname, MainClass.GetFieldFromID(AccountList[i], "accountname", "cw_account"));
            for (int k = 1; k < GridView1.Columns.Count; k += 2)
            {
                string SubjectNo = GridView1.Columns[k].HeaderText;
                NewRow[k] = CommClass.GetTableValue("cw_assetcard", "sum(AAmount)", "ClassID like '" + SubjectNo + "%' and SUseDate like '" + SelYear.Text + "%'");
                NewRow[k + 1] = CommClass.GetTableValue("cw_assetcard", "sum(OldPrice)", "ClassID like '" + SubjectNo + "%' and SUseDate like '" + SelYear.Text + "%'");
            }
            BindTable.Rows.Add(NewRow);
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
