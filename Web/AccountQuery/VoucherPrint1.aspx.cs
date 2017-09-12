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

public partial class AccountQuery_VoucherPrint1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            InitWebControl();
        }
    }
    protected void InitWebControl()
    {
        string sYearMonth = Request.QueryString["year"] + "年" + Request.QueryString["sm"] + "月01日";
        string eYearMonth = Request.QueryString["year"] + "年" + Request.QueryString["em"] + "月31日";
        string QueryString = "$voucherdate between '" + sYearMonth + "' and '" + eYearMonth + "'";
        if (Request.QueryString["alarm"] == "0")
        {
            QueryString += "$IsHasAlarm='0'";
        }
        if (Request.QueryString["sno"].Length > 0)
        {
            if (Request.QueryString["eno"].Length > 0)
            {
                QueryString += "$VoucherNo between '" + Request.QueryString["sno"] + "' and '" + Request.QueryString["eno"] + "'";
            }
            else
            {
                QueryString += "$VoucherNo='" + Request.QueryString["sno"] + "'";
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
            string VCount = "0";
            string EntryCount = "0";
            string TopSpace = "0";
            string VSpace = "0";
            string PrintPara = CommClass.GetSysPara("PrintPara");
            if (PrintPara != "NoDataItem")
            {
                string[] printPara = PrintPara.Split('|');
                VCount = printPara[0];
                EntryCount = printPara[1];
                TopSpace = printPara[2];
                VSpace = printPara[3];
            }
            int PageCount = 1;
            int PrintCount = 0;
            int PrintSplit = 0;
            int _VCount = int.Parse(VCount);
            int ECounts = int.Parse(EntryCount);
            List<UserControl> VoucherList = new List<UserControl>();
            for (int i = 0; i < rows.Count; i++)
            {
                int VCounts = CommClass.CountRecord("cw_entry", "voucherid='" + rows[i]["id"].ToString() + "'");
                if (EntryCount == "0" || VCounts <= ECounts)
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
                if (VCount != "0")
                {
                    PrintCount++;
                    PrintSplit = PrintCount % _VCount;
                }
                //设置距顶部间距
                if (PrintSplit == 1 || i == 0 || _VCount <= 1)
                {
                    LiteralControl setTop = new LiteralControl("<div style='width:750px;height:" + TopSpace + "px'></div>");
                    ShowPageContent.Controls.Add(setTop);
                }
                //输出凭证
                ShowPageContent.Controls.Add(VoucherList[i]);
                //打印分页设置
                if (PrintSplit == 0 || i == VoucherList.Count - 1)
                {
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
                    LiteralControl setSpace = new LiteralControl("<div style='width:750px;height:" + VSpace + "px;'>&nbsp;</div>");
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
        ShowTipImg.SetValue(ctl, false, null);
        PropertyInfo ShowReverseState = ctlType.GetProperty("ShowReverseState");
        ShowReverseState.SetValue(ctl, false, null);
        //套打
        PropertyInfo taoDa = ctlType.GetProperty("taoDa");
        taoDa.SetValue(ctl, true, null);
        return ctl;
    }
}
