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
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

public partial class AccountQuery_PrintDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000008")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            MainClass.InitAccountYear(QYear);
            DateTime AccountDate = MainClass.GetAccountDate();
            QSMonth.Attributes["onchange"] = "SelAMonth(this.value);";
            QSMonth.Text = AccountDate.Month.ToString("00");
            QEMonth.Text = AccountDate.Month.ToString("00");
            InitWebControl();
            //写入操作日志
            CommClass.WriteCTL_Log("100014", "账簿查询：明细账分类账连续打印");
            //--
        }
    }
    protected void InitWebControl()
    {
        string QueryString = "$IsDetail='1'";
        string sYearMonth = QYear.SelectedValue + "年" + QSMonth.SelectedValue + "月";
        string eYearMonth = QYear.SelectedValue + "年" + QEMonth.SelectedValue + "月";
        if (ReportType.SelectedValue == "0")
        {
            GridViewWidth.Value = "750";
        }
        else
        {
            GridViewWidth.Value = "1004";
        }
        if (SubjectNoS.Text.Length > 0)
        {
            if (SubjectNoE.Text.Length > 0)
            {
                QueryString += "$SubjectNo between '" + SubjectNoS.Text + "' and '" + SubjectNoE.Text + "9999999999999999'";
            }
            else
            {
                QueryString += "$SubjectNo like '" + SubjectNoS.Text + "%'";
            }
        }
        if (!isShowFlag.Checked)
        {
            QueryString += "$YearMonth between '" + sYearMonth + "' and '" + eYearMonth + "'$(BeginBalance <>0 or lead<>0 or onloan<>0 or FinalBalance<>0)";
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataRowCollection rows = CommClass.GetDataRows("select subjectno from cw_viewsubjectsum " + QueryString + " group by subjectno order by subjectno");
        if (rows == null)
        {
            LiteralControl showNoData = new LiteralControl("<center><br>无任何科目！</center>");
            ShowPageContent.Controls.Add(showNoData);
        }
        else
        {
            int PageCount = 1;
            bool ExsitFlag = false;
            for (int i = 0; i < rows.Count; i++)
            {
                if (ExsitFlag)
                {
                    //设置打印分页
                    LiteralControl splitPage = new LiteralControl("<div style='page-break-after:always'>&nbsp;</div>");
                    ShowPageContent.Controls.Add(splitPage);
                }
                UserControl ctl = (UserControl)LoadControl("ShowDetail.ascx");
                Type ctlType = ctl.GetType();
                PropertyInfo _SubjectNo = ctlType.GetProperty("SubjectNo");
                _SubjectNo.SetValue(ctl, rows[i]["subjectno"].ToString(), null);
                PropertyInfo _QSMonth = ctlType.GetProperty("QSMonth");
                _QSMonth.SetValue(ctl, QSMonth.SelectedValue, null);
                PropertyInfo _QEMonth = ctlType.GetProperty("QEMonth");
                _QEMonth.SetValue(ctl, QEMonth.SelectedValue, null);
                PropertyInfo _QYear = ctlType.GetProperty("QYear");
                _QYear.SetValue(ctl, QYear.SelectedValue, null);
                PropertyInfo _ReportType = ctlType.GetProperty("ReportType");
                _ReportType.SetValue(ctl, ReportType.SelectedValue, null);
                PropertyInfo _PageType = ctlType.GetProperty("PageType");
                _PageType.SetValue(ctl, PageType.SelectedValue, null);
                ShowPageContent.Controls.Add(ctl);
                if (PageType.SelectedValue == "1")
                {
                    //输出分页页码
                    LiteralControl showSplit = new LiteralControl(string.Concat("<!--NoPrintStart--><br><div style='width:750px;height:25px;text-align:center'>"
                        , "第".PadLeft(51, '='), (PageCount++).ToString("000"), "页".PadRight(51, '='), "</div><!--NoPrintEnd-->"));
                    ShowPageContent.Controls.Add(showSplit);
                }
                ExsitFlag = true;
            }
            if (!ExsitFlag)
            {
                LiteralControl showNoData = new LiteralControl("<center><br>无任何数据输出！</center>");
                ShowPageContent.Controls.Add(showNoData);
            }
        }
    }
    protected void QButton_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
