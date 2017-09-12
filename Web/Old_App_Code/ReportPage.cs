using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;

/// <summary>
/// 分页输出报表
/// </summary>
public class ReportPage : Page
{
    private int totalPage = 1;

    public int TableWidth;
    public string IsToExcel;
    public string DesignID;
    public string ReportDate;
    public string ReportTitle;
    public string ReportTypeID;
    public string PageType = "0";
    public string PageRowCount;
    public string[] Parameters;
    public DataTable BindTable;
    public PlaceHolder ShowPageContent;

    public ReportPage()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public void ShowReportPage()
    {
        int pageCount = 1;
        int showCount = 0;
        if (PageType == "0")
        {
            if ("100003,100019".Contains(ReportTypeID))
            {
                int.TryParse(CommClass.GetSysPara("PageRowCount100002"), out showCount);
            }
            else
            {
                int.TryParse(CommClass.GetSysPara("PageRowCount" + ReportTypeID), out showCount);
            }
        }
        bool isPaging = (showCount > 0);
        DataTable dt = BindTable.Clone();
        if (isPaging)
        {
            totalPage = BindTable.Rows.Count / showCount;
            if (BindTable.Rows.Count % showCount != 0)
            {
                totalPage++;
            }
        }
        for (int i = 0; i < BindTable.Rows.Count; i++)
        {
            if (isPaging && dt.Rows.Count == showCount)
            {
                AddPage(dt, pageCount++);
                AddPageSplit();
                dt = BindTable.Clone();
            }
            dt.Rows.Add(BindTable.Rows[i].ItemArray);
        }
        if (dt.Rows.Count > 0)
        {
            AddPage(dt, pageCount);
        }
        LiteralControl setSplitWidth = new LiteralControl("<script>var pNumber=document.getElementsByName('PageNumber');for(var i=0;i<pNumber.length;i++)pNumber[i].style.width=gWidth</script>");
        ShowPageContent.Controls.Add(setSplitWidth);
        if (IsToExcel == "1")
        {
            GridView gridView = new GridView();
            gridView.AutoGenerateColumns = false;
            gridView.CssClass = "onlyborder";
            gridView.Width = 750;
            gridView.RowStyle.Height = 21;
            gridView.RowStyle.Font.Size = FontUnit.Parse("10pt");
            CommClass.CreateGridView(DesignID, BindTable, gridView);
            PageClass.ToExcel(gridView);
        }
    }

    private void AddPage(DataTable dt, int pageCount)
    {
        ShowPageContent.Controls.Add(GetPageCTL(dt));
        if (PageType == "0")
        {
            LiteralControl setPage = new LiteralControl(string.Format("<div id='PageNumber' class='PageNumber'>—&nbsp;&nbsp;第{0}页&nbsp;&nbsp;共{1}页&nbsp;&nbsp;—</div>", pageCount, totalPage));
            ShowPageContent.Controls.Add(setPage);
        }
    }

    private void AddPageSplit()
    {
        LiteralControl setSplit = new LiteralControl("<div class='pageBreakAfter'>&nbsp;</div>");
        ShowPageContent.Controls.Add(setSplit);
    }

    private UserControl GetPageCTL(DataTable dt)
    {
        UserControl ctl = (UserControl)LoadControl("AccountQuery\\ReportPage.ascx");
        Type ctlType = ctl.GetType();
        PropertyInfo bindTable = ctlType.GetProperty("BindTable");
        bindTable.SetValue(ctl, dt, null);
        PropertyInfo reportDate = ctlType.GetProperty("reportDate");
        reportDate.SetValue(ctl, ReportDate, null);
        PropertyInfo reportTitle = ctlType.GetProperty("reportTitle");
        reportTitle.SetValue(ctl, ReportTitle, null);
        PropertyInfo reportTypeID = ctlType.GetProperty("reportTypeID");
        reportTypeID.SetValue(ctl, ReportTypeID, null);
        PropertyInfo designID = ctlType.GetProperty("designID");
        designID.SetValue(ctl, DesignID, null);
        PropertyInfo parameters = ctlType.GetProperty("parameters");
        parameters.SetValue(ctl, Parameters, null);
        return ctl;
    }
}
