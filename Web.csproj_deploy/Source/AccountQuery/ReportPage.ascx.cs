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

public partial class AccountQuery_ReportPage : System.Web.UI.UserControl
{
    private DataTable _BindTable;
    public DataTable BindTable
    {
        set { _BindTable = value; }
        get { return _BindTable; }
    }
    private string _reportDate;
    public string reportDate
    {
        set { _reportDate = value; }
        get { return _reportDate; }
    }
    private string _reportTitle;
    public string reportTitle
    {
        set { _reportTitle = value; }
        get { return _reportTitle; }
    }
    private string _reportTypeID;
    public string reportTypeID
    {
        set { _reportTypeID = value; }
        get { return _reportTypeID; }
    }
    private string _designID;
    public string designID
    {
        set { _designID = value; }
        get { return _designID; }
    }
    private string[] _parameters;
    public string[] parameters
    {
        set { _parameters = value; }
        get { return _parameters; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ReportTitle.InnerHtml = reportTitle;
        AName1.Text = UserInfo.AccountName;
        ReportDate.Text = reportDate;
        GridView1.Attributes.Add("onselectstart", "return false;");
        CommClass.CreateGridView(designID, BindTable, GridView1);
        int clickColumn = -1;
        switch (reportTypeID)
        {
            case "100001":
                //报表分析
                clickColumn = 1;
                TableHead01.Visible = true;
                break;
            case "100002":
            case "100003":
            case "100019":
                //收支逐笔公开榜
                clickColumn = 3;
                TableHead01.Visible = true;
                GridView1.Rows[0].Cells[0].Text = reportDate.Substring(0, 4);
                if (reportTypeID == "100003")
                {
                    GridView1.Rows[1].Cells[5].Text = "收入";
                    GridView1.Rows[1].Cells[6].Text = "支出";
                    for (int i = 2; i < GridView1.Rows.Count - 1; i++)
                    {
                        if (GridView1.Rows[i].Cells[5].Text == "&nbsp;")
                        {
                            GridView1.Rows[i].Cells[5].Text = GridView1.Rows[i].Cells[6].Text;
                            GridView1.Rows[i].Cells[6].Text = "&nbsp;";
                        }
                        else
                        {
                            GridView1.Rows[i].Cells[6].Text = GridView1.Rows[i].Cells[5].Text;
                            GridView1.Rows[i].Cells[5].Text = "&nbsp;";
                        }
                    }
                    int lastRowIndex = GridView1.Rows.Count - 1;
                    string tempValue = GridView1.Rows[lastRowIndex].Cells[6].Text;
                    GridView1.Rows[lastRowIndex].Cells[6].Text = GridView1.Rows[lastRowIndex].Cells[5].Text;
                    GridView1.Rows[lastRowIndex].Cells[5].Text = tempValue;
                }
                if (reportTypeID == "100019")
                {
                    clickColumn = 1;
                    TableHead01.Visible = false;
                    GridView1.Rows[0].Cells[0].Text += "年";
                }
                break;
            case "100008":
                //总账
                AName4.Text = AName1.Text;
                TableHead04.Visible = true;
                SubjectName.Text = reportTitle;
                GridView1.Rows[0].Cells[0].Text = reportDate;
                if (designID == "000001")
                {
                    TableHead04.Style["Width"] = "750px";
                    GridView1.Width = 750;
                }
                else
                {
                    TableHead04.Style["Width"] = "1004px";
                    GridView1.Width = 1004;
                }
                break;
            case "100004":
                //明细分类账
                clickColumn = 3;
                AName2.Text = AName1.Text;
                TableHead02.Visible = true;
                int k = reportTitle.IndexOf("$");
                MSubjectTitle.InnerHtml = reportTitle.Substring(0, k);
                DSubjectTitle.InnerHtml = reportTitle.Substring(k + 1);
                GridView1.Rows[0].Cells[0].Text = reportDate;
                switch (designID)
                {
                    case "000001":
                    case "010001":
                        TableHead02.Style["Width"] = "750px";
                        GridView1.Width = 750;
                        break;
                    case "000017":
                    case "010017":
                        TableHead02.Style["Width"] = "1004px";
                        GridView1.Width = 1004;
                        break;
                }
                break;
            case "100007":
                //固定资产明细账
                AName3.Text = AName1.Text;
                TableHead03.Visible = true;
                TableHead03.Style["Width"] = "1004px";
                GridView1.Width = 1004;
                if (parameters != null && parameters.Length == 3)
                {
                    AssetNo.Text = parameters[0];
                    AssetName.Text = parameters[1];
                    QDateTime.Text = parameters[2];
                }
                for (int i = 0; i < BindTable.Rows.Count; i++)
                {
                    string vid = BindTable.Rows[i][1].ToString();
                    if (vid.Length > 0 && vid != "--------------")
                    {
                        GridView1.Rows[i].Attributes.Add("ondblclick", "ShowVoucher('" + vid + "');");
                        GridView1.Rows[i].Attributes["title"] = "提示：双击行可以查看详细凭证。";
                    }
                }
                break;
            default:
                //通用报表
                TableHead01.Visible = true;
                break;
        }
        if (clickColumn != -1)
        {
            for (int i = 0; i < BindTable.Rows.Count; i++)
            {
                string[] noid = BindTable.Rows[i][clickColumn].ToString().Split('|');
                if (noid.Length == 2)
                {
                    GridView1.Rows[i].Cells[clickColumn].Text = noid[0];
                    GridView1.Rows[i].Attributes.Add("ondblclick", "ShowVoucher('" + noid[1] + "');");
                    GridView1.Rows[i].Attributes["title"] = "提示：双击行可以查看详细凭证。";
                }
            }
        }
    }
}
