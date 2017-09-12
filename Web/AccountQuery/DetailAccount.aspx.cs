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
using System.Collections.Generic;
using System.Reflection;

public partial class AccountQuery_DetailAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000008$100002")) { return; }
        if (!IsPostBack)
        {
            QSubjectNo.Attributes["ondblclick"] = "SelSubject()";
            MainClass.InitAccountYear(QYear);
            DateTime AccountDate = MainClass.GetAccountDate();
            QSMonth.Attributes["onchange"] = "SelAMonth(this.value);";
            QSMonth.Text = AccountDate.Month.ToString("00");
            QEMonth.Text = AccountDate.Month.ToString("00");
            if (Request.QueryString["sno"] != null)
            {
                QSubjectNo.Text = Request.QueryString["sno"];
            }
            GetDetailInfo();
            //写入操作日志
            CommClass.WriteCTL_Log("100014", "账簿查询：明细账日记账");
            //--
        }
    }
    protected void GetDetailInfo()
    {
        string SubjectNo = string.Empty;
        string[] SubjectNoArr = QSubjectNo.Text.Replace("]", "").Split('[');
        if (SubjectNoArr.Length == 2)
        {
            SubjectNo = SubjectNoArr[1];
        }
        else
        {
            SubjectNo = QSubjectNo.Text;
        }
        if (ReportType.SelectedValue == "1" || CommClass.GetTableValue("cw_subject", "AccountType", string.Concat("subjectno='", SubjectNo, "'")) == "1")
        {
            GridViewWidth.Value = "1004";
        }
        else
        {
            GridViewWidth.Value = "750";
        }
        UserControl ctl = (UserControl)LoadControl("ShowDetail.ascx");
        Type ctlType = ctl.GetType();
        PropertyInfo _SubjectNo = ctlType.GetProperty("SubjectNo");
        _SubjectNo.SetValue(ctl, SubjectNo, null);
        PropertyInfo _QSMonth = ctlType.GetProperty("QSMonth");
        _QSMonth.SetValue(ctl, QSMonth.SelectedValue, null);
        PropertyInfo _QEMonth = ctlType.GetProperty("QEMonth");
        _QEMonth.SetValue(ctl, QEMonth.SelectedValue, null);
        PropertyInfo _QYear = ctlType.GetProperty("QYear");
        _QYear.SetValue(ctl, QYear.SelectedValue, null);
        PropertyInfo _ReportType = ctlType.GetProperty("ReportType");
        _ReportType.SetValue(ctl, ReportType.SelectedValue, null);
        PropertyInfo _PageType = ctlType.GetProperty("PageType");
        _PageType.SetValue(ctl, "0", null);
        if (ViewState["IsToExcel"] == (object)"1")
        {
            PropertyInfo _IsToExcel = ctlType.GetProperty("IsToExcel");
            _IsToExcel.SetValue(ctl, "1", null);
        }
        ShowPageContent.Controls.Add(ctl);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GetDetailInfo();
    }
    protected void OutputDataToExcel_Click(object sender, EventArgs e)
    {
        ViewState["IsToExcel"] = "1";
        GetDetailInfo();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}
