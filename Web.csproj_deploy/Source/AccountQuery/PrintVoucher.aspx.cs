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
using System.Collections.Generic;
using System.Reflection;

public partial class AccountQuery_PrintVoucher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000014$100005")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            MainClass.InitAccountYear(QYear);
            DateTime AccountDate = MainClass.GetAccountDate();
            QSMonth.Attributes["onchange"] = "SelAMonth(this.value);";
            QSMonth.Text = AccountDate.Month.ToString("00");
            QEMonth.Text = AccountDate.Month.ToString("00");
            QButton.Attributes["onclick"] = "return CheckQ();";
            SavePrintPara.Attributes["onclick"] = "return CheckQ();";
            UtilsPage.SetTextBoxAutoValue(TopSpace, "0");
            UtilsPage.SetTextBoxAutoValue(VCount, "0");
            UtilsPage.SetTextBoxAutoValue(VSpace, "0");
            UtilsPage.SetTextBoxAutoValue(EntryCount, "0");
            string PrintPara = CommClass.GetSysPara("PrintPara");
            string[] printPara = PrintPara.Split('|');
            if (PrintPara.Length < 4)
            {
                string ParaValue = VCount.Text + "|" + EntryCount.Text + "|" + TopSpace.Text + "|" + VSpace.Text;
                CommClass.SetSysPara("PrintPara", ParaValue);
            }
            else
            {
                VCount.Text = printPara[0];
                EntryCount.Text = printPara[1];
                TopSpace.Text = printPara[2];
                VSpace.Text = printPara[3];
            }
            if (Session["Powers"].ToString().IndexOf("000014") == -1)
            {
                SavePrintPara.Enabled = false;
            }
            InitWebControl();
            //写入操作日志
            CommClass.WriteCTL_Log("100015", "报表查询：凭证连续打印");
            //--
        }
    }
    protected void InitWebControl()
    {
        string sYearMonth = QYear.SelectedValue + "年" + QSMonth.SelectedValue + "月01日";
        string eYearMonth = QYear.SelectedValue + "年" + QEMonth.SelectedValue + "月31日";
        string QueryString = "$voucherdate between '" + sYearMonth + "' and '" + eYearMonth + "'";
        if (cbxShowVoucher.Checked == false)
        {
            QueryString += "$IsHasAlarm='0'";
        }
        if (VoucherNoS.Text != "")
        {
            if (VoucherNoE.Text.Length > 0)
            {
                QueryString += "$VoucherNo between '" + VoucherNoS.Text + "' and '" + VoucherNoE.Text + "'";
            }
            else
            {
                QueryString += "$VoucherNo='" + VoucherNoS.Text + "'";
            }
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataRowCollection rows = CommClass.GetDataRows("select id from cw_voucher" + QueryString + " order by left(voucherdate,8),voucherno");
        if (rows == null)
        {
            LiteralControl showNoData = new LiteralControl("<center><br>无凭证！</center>");
            ShowPageContent.Controls.Add(showNoData);
        }
        else
        {
            int PageCount = 1;
            int PrintCount = 0;
            int PrintSplit = 0;
            int _VCount = int.Parse(VCount.Text);
            int ECounts = int.Parse(EntryCount.Text);
            List<UserControl> VoucherList = new List<UserControl>();
            for (int i = 0; i < rows.Count; i++)
            {
                int VCounts = CommClass.CountRecord("cw_entry", "voucherid='" + rows[i]["id"].ToString() + "'");
                if (EntryCount.Text == "0" || VCounts <= ECounts)
                {
                    VoucherList.Add(GetVoucherCTL(rows[i]["id"].ToString(), ECounts, 0));
                }
                else
                {
                    int MinCount = VCounts / ECounts;
                    int PCount = VCounts % ECounts == 0 ? MinCount : MinCount + 1;
                    for (int k = 0; k < PCount; k++)
                    {
                        VoucherList.Add(GetVoucherCTL(rows[i]["id"].ToString(), ECounts, k));
                    }
                }
            }
            for (int i = 0; i < VoucherList.Count; i++)
            {
                if (VCount.Text != "0")
                {
                    PrintCount++;
                    PrintSplit = PrintCount % _VCount;
                }
                //设置距顶部间距
                if (PrintSplit == 1 || i == 0 || _VCount <= 1)
                {
                    LiteralControl setTop = new LiteralControl("<div style='width:750px;height:" + TopSpace.Text + "px'></div>");
                    ShowPageContent.Controls.Add(setTop);
                }
                //输出凭证
                ShowPageContent.Controls.Add(VoucherList[i]);
                //打印分页设置
                if (PrintSplit == 0 || i == VoucherList.Count - 1)
                {
                    //输出分页页码
                    LiteralControl setPage = new LiteralControl("<!--NoPrintStart--><div style='width:750px;height:25px;text-align:center'>"
                        + "第".PadLeft(46, '=') + (PageCount++).ToString("000") + "页".PadRight(46, '=') + "</div><!--NoPrintEnd-->");
                    ShowPageContent.Controls.Add(setPage);
                    //设置打印分页
                    if (i < VoucherList.Count - 1)
                    {
                        LiteralControl setSplit = new LiteralControl("<div style='page-break-after:always;'>&nbsp;</div>");
                        ShowPageContent.Controls.Add(setSplit);
                    }
                }
                //设置凭证间距
                if ((_VCount == 0 ? true : PrintSplit != 0) && i < VoucherList.Count - 1)
                {
                    LiteralControl setSpace = new LiteralControl("<div style='width:750px;height:" + VSpace.Text + "px;'>&nbsp;</div>");
                    ShowPageContent.Controls.Add(setSpace);
                }
            }
        }
    }
    private UserControl GetVoucherCTL(string id, int psize, int pindex)
    {
        UserControl ctl = (UserControl)LoadControl("ShowVoucher.ascx");
        Type ctlType = ctl.GetType();
        PropertyInfo VoucherID = ctlType.GetProperty("VoucherID");
        VoucherID.SetValue(ctl, id, null);
        PropertyInfo AccountID = ctlType.GetProperty("AccountID");
        AccountID.SetValue(ctl, UserInfo.AccountID, null);
        PropertyInfo PageSize = ctlType.GetProperty("PageSize");
        PageSize.SetValue(ctl, psize, null);
        PropertyInfo PageIndex = ctlType.GetProperty("PageIndex");
        PageIndex.SetValue(ctl, pindex, null);
        PropertyInfo ShowTipImg = ctlType.GetProperty("ShowTipImg");
        ShowTipImg.SetValue(ctl, cbxShowTipImg.Checked, null);
        PropertyInfo ShowReverseState = ctlType.GetProperty("ShowReverseState");
        ShowReverseState.SetValue(ctl, cbxShowReverseState.Checked, null);
        return ctl;
    }
    protected void QButton_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
    protected void SavePrintPara_Click(object sender, EventArgs e)
    {
        string ParaValue = VCount.Text + "|" + EntryCount.Text + "|" + TopSpace.Text + "|" + VSpace.Text;
        CommClass.SetSysPara("PrintPara", ParaValue);
        PageClass.ShowAlertMsg(this.Page, "打印参数设置保存成功！");
        InitWebControl();
    }
}
