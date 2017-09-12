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

public partial class AccountManage_VoucherRM : System.Web.UI.Page
{
    DataSet AllVoucher;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000005")) { return; }
        if (!IsPostBack)
        {
            DateTime _AccountDate = MainClass.GetAccountDate();
            //检测是否强制进行年末收支自动结转
            string lastCarryDate = MainClass.GetFieldFromID(UserInfo.AccountID, "LastCarryDate", "cw_account");
            if (lastCarryDate == string.Format("{0}年12月31日", _AccountDate.Year.ToString()))
            {
                string accountYear = MainClass.GetFieldFromID(UserInfo.AccountID, "AccountYear", "cw_account");
                if (accountYear.IndexOf(_AccountDate.AddYears(1).Year.ToString()) == -1)
                {
                    PageClass.UrlRedirect("请进行年末收支自动结转操作！", 3);
                    return;
                }
            }
            ////////////////////////////////////////////////////////////////
            BackControl.Attributes.Add("onclick", "return confirm('您确定需要反记账该凭证吗？')");
            AuditAllVoucher.Attributes.Add("onclick", "return confirm('您确定需要使本月所有凭证更改为“记账”状态吗？')");
            AuditQVoucher.Attributes.Add("onclick", "return confirm('您确定需要使满足查询条件的所有凭证更改为“记账”状态吗？')");
            AuditVoucher.Attributes.Add("onclick", "return confirm('您确定需要使该凭证更改为“记账”状态吗？')");
            QVDate.Attributes.Add("readonly", "readonly");
            QVDate.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].QVDate,'yyyy年mm月dd日')");
            aYear.Value = _AccountDate.Year.ToString();
            aMonth.Value = _AccountDate.Month.ToString("00");
            aDay.Value = _AccountDate.Day.ToString("00");
            tYear.Value = DateTime.Now.Year.ToString();
            tMonth.Value = DateTime.Now.Month.ToString("00");
            tDay.Value = DateTime.Now.Day.ToString("00");
            string[] WeekDay ={ "日", "一", "二", "三", "四", "五", "六" };
            tWeek.Value = WeekDay[Convert.ToInt32(DateTime.Now.DayOfWeek)];
            GetAllVoucher();
        }
        AllVoucher = (DataSet)Session["AllVoucher"];
        if (!IsPostBack)
        {
            InitWebControl();
        }
    }
    protected void GetAllVoucher()
    {
        string QueryString = "$DelFlag='0'";
        if (aMonth.Value == "12")
        {
            string LastCarryDate = MainClass.GetFieldFromID(UserInfo.AccountID, "LastCarryDate", "cw_account");
            if (LastCarryDate.IndexOf("12月") != -1)
            {
                string YearCarryVoucher = MainClass.GetFieldFromID(UserInfo.AccountID, "YearCarryVoucher", "cw_account");
                string[] CarryVoucherNo = YearCarryVoucher.Split('-');
                QueryString += "$VoucherNo>'" + CarryVoucherNo[0] + "'";
            }
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
        if (QVDate.Text.Length > 0)
        {
            QueryString += "$VoucherDate='" + QVDate.Text + "'";
        }
        else
        {
            QueryString += "$VoucherDate like '" + aYear.Value + "年" + aMonth.Value + "月" + "%'";
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        AllVoucher = CommClass.GetDataSet("select * from cw_voucher " + QueryString + " order by voucherno");
        Session["AllVoucher"] = AllVoucher;
    }
    protected void InitWebControl()
    {
        if (AllVoucher.Tables[0].Rows.Count > 0)
        {
            //初始化凭证信息
            int _RecordRowIndex = int.Parse(RecordRowIndex.Value);
            if (_RecordRowIndex < 0 || _RecordRowIndex > AllVoucher.Tables[0].Rows.Count - 1)
            {
                _RecordRowIndex = 0;
                RecordRowIndex.Value = "0";
            }
            UserControl ctl = (UserControl)LoadControl("../AccountQuery/ShowVoucher.ascx");
            Type ctlType = ctl.GetType();
            PropertyInfo VoucherID = ctlType.GetProperty("VoucherID");
            VoucherID.SetValue(ctl, AllVoucher.Tables[0].Rows[_RecordRowIndex]["id"].ToString(), null);
            ShowPageContent.Controls.Add(ctl);
            if (AllVoucher.Tables[0].Rows[_RecordRowIndex]["IsAuditing"].ToString() == "0")
            {
                AuditVoucher.Enabled = false;
                BackControl.Enabled = false;
            }
            else
            {
                if (AllVoucher.Tables[0].Rows[_RecordRowIndex]["IsRecord"].ToString() == "0")
                {
                    AuditVoucher.Enabled = true;
                    BackControl.Enabled = false;
                }
                else
                {
                    AuditVoucher.Enabled = false;
                    BackControl.Enabled = true;
                }
            }
        }
        else
        {
            BackControl.Enabled = false;
            AuditVoucher.Enabled = false;
            LiteralControl showNoData = new LiteralControl("<center><br><br>没有查询到凭证！</center>");
            ShowPageContent.Controls.Add(showNoData);
        }
    }
    protected void PreVoucher_Click(object sender, EventArgs e)
    {
        if (int.Parse(RecordRowIndex.Value) > 0)
        {
            RecordRowIndex.Value = (int.Parse(RecordRowIndex.Value) - 1).ToString();
        }
        InitWebControl();
    }
    protected void NextVoucher_Click(object sender, EventArgs e)
    {
        if (int.Parse(RecordRowIndex.Value) < AllVoucher.Tables[0].Rows.Count - 1)
        {
            RecordRowIndex.Value = (int.Parse(RecordRowIndex.Value) + 1).ToString();
        }
        InitWebControl();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        RecordRowIndex.Value = "0";
        InitWebControl();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        RecordRowIndex.Value = (AllVoucher.Tables[0].Rows.Count - 1).ToString();
        InitWebControl();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        GetAllVoucher();
        InitWebControl();
    }
    protected void AuditVoucher_Click(object sender, EventArgs e)
    {
        int _RecordRowIndex = int.Parse(RecordRowIndex.Value);
        string _VoucherID = AllVoucher.Tables[0].Rows[_RecordRowIndex]["id"].ToString();
        string _VoucherNo = AllVoucher.Tables[0].Rows[_RecordRowIndex]["voucherno"].ToString();
        AllVoucher.Tables[0].Rows[_RecordRowIndex]["IsRecord"] = "1";
        AllVoucher.Tables[0].Rows[_RecordRowIndex]["Accountant"] = Session["RealName"].ToString();
        Session["AllVoucher"] = AllVoucher;
        CommClass.ExecuteSQL("update cw_voucher set IsRecord='1',Accountant='" + Session["RealName"].ToString() + "' where id='" + _VoucherID + "'");
        InitWebControl();
        //PageClass.ShowAlertMsg(this.Page, "凭证：" + _VoucherNo + "，记账成功！");
        //写入操作日志
        CommClass.WriteCTL_Log("100008", "凭证记账：" + _VoucherNo + "，凭证日期：" + AllVoucher.Tables[0].Rows[_RecordRowIndex]["voucherdate"].ToString());
        //--
    }
    protected void AuditAllVoucher_Click(object sender, EventArgs e)
    {
        CommClass.ExecuteSQL("update cw_voucher set IsRecord='1',Accountant='" + Session["RealName"].ToString() + "' where VoucherDate like '"
            + aYear.Value + "年" + aMonth.Value + "月" + "%' and IsAuditing='1' and IsRecord='0'");
        GetAllVoucher();
        InitWebControl();
        //PageClass.ShowAlertMsg(this.Page, "凭证批量记账成功！");
        //写入操作日志
        CommClass.WriteCTL_Log("100008", "凭证批量记账，记账月份：" + aYear.Value + "年" + aMonth.Value + "月");
        //--
    }
    protected void AuditQVoucher_Click(object sender, EventArgs e)
    {
        GetAllVoucher();
        for (int i = 0; i < AllVoucher.Tables[0].Rows.Count; i++)
        {
            if (AllVoucher.Tables[0].Rows[i]["IsAuditing"].ToString() == "1" && AllVoucher.Tables[0].Rows[i]["IsRecord"].ToString() == "0")
            {
                AllVoucher.Tables[0].Rows[i]["IsRecord"] = "1";
                AllVoucher.Tables[0].Rows[i]["Accountant"] = Session["RealName"].ToString();
            }
        }
        CommClass.UpdateDataSet(AllVoucher);
        Session["AllVoucher"] = AllVoucher;
        InitWebControl();
        //PageClass.ShowAlertMsg(this.Page, "凭证按条件记账成功！");
        //写入操作日志
        CommClass.WriteCTL_Log("100008", "凭证按条件记账，记账月份：" + aYear.Value + "年" + aMonth.Value + "月");
        //--
    }
    protected void BackControl_Click(object sender, EventArgs e)
    {
        int _RecordRowIndex = int.Parse(RecordRowIndex.Value);
        string _VoucherID = AllVoucher.Tables[0].Rows[_RecordRowIndex]["id"].ToString();
        string _VoucherNo = AllVoucher.Tables[0].Rows[_RecordRowIndex]["voucherno"].ToString();
        AllVoucher.Tables[0].Rows[_RecordRowIndex]["IsRecord"] = "0";
        AllVoucher.Tables[0].Rows[_RecordRowIndex]["Accountant"] = "";
        Session["AllVoucher"] = AllVoucher;
        CommClass.ExecuteSQL("update cw_voucher set IsRecord='0',Accountant='' where id='" + _VoucherID + "'");
        InitWebControl();
        //PageClass.ShowAlertMsg(this.Page, "凭证：" + _VoucherNo + "，反记账成功！");
        //写入操作日志
        CommClass.WriteCTL_Log("100008", "凭证反记账：" + _VoucherNo + "，凭证日期：" + AllVoucher.Tables[0].Rows[_RecordRowIndex]["voucherdate"].ToString());
        //--
    }
}
