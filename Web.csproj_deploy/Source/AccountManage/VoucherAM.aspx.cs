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

public partial class AccountManage_VoucherAM : System.Web.UI.Page
{
    DataSet AllVoucher;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000004")) { return; }
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
            //获取报警参数
            DataTable alarm = MainClass.GetDataTable("select IndexSubject,IndexValue from cw_indexmonitor where UnitID='999999' order by IndexSubject asc");
            StringBuilder alarms = new StringBuilder();
            foreach (DataRow row in alarm.Rows)
            {
                string subjectNo = row["IndexSubject"].ToString();
                subjectNo = subjectNo.Substring(0, subjectNo.IndexOf("."));
                if (subjectNo.StartsWith("101"))
                {
                    alarms.AppendFormat(" or (subjectno like '{0}%' and SumMoney{2}{1})", subjectNo, row["IndexValue"].ToString(), "{0}");
                }
                else
                {
                    alarms.AppendFormat(" or (subjectno like '{0}%' and SumMoney{2}{1})", subjectNo, row["IndexValue"].ToString(), "{1}");
                }
            }
            Alarms.Value += alarms.ToString();
            ////////////////////////////////////////////////////////////////
            BackControl.Attributes.Add("onclick", "return confirm('您确定需要反审核该凭证吗？')");
            AuditAllVoucher.Attributes.Add("onclick", "return confirm('您确定需要使本月所有凭证更改为“审核”状态吗？')");
            AuditQVoucher.Attributes.Add("onclick", "return confirm('您确定需要使满足查询条件的所有凭证更改为“审核”状态吗？')");
            AuditVoucher.Attributes.Add("onclick", "return confirm('您确定需要使该凭证更改为“审核”状态吗？')");
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
            DataRow voucherRow = AllVoucher.Tables[0].Rows[_RecordRowIndex];
            UserControl ctl = (UserControl)LoadControl("../AccountQuery/ShowVoucher.ascx");
            Type ctlType = ctl.GetType();
            PropertyInfo VoucherID = ctlType.GetProperty("VoucherID");
            VoucherID.SetValue(ctl, voucherRow["id"].ToString(), null);
            ShowPageContent.Controls.Add(ctl);
            if (voucherRow["IsAuditing"].ToString() == "0")
            {
                AuditVoucher.Enabled = true;
                BackControl.Enabled = false;
            }
            else
            {
                AuditVoucher.Enabled = false;
                if (voucherRow["IsRecord"].ToString() == "0" && voucherRow["IsHasAlarm"].ToString() == "0")
                {
                    BackControl.Enabled = true;
                }
                else
                {
                    BackControl.Enabled = false;
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
        //单张审核凭证
        int _RecordRowIndex = int.Parse(RecordRowIndex.Value);
        string _VoucherID = AllVoucher.Tables[0].Rows[_RecordRowIndex]["id"].ToString();
        string _VoucherNo = AllVoucher.Tables[0].Rows[_RecordRowIndex]["voucherno"].ToString();
        if (Alarms.Value == "1=2" || AllVoucher.Tables[0].Rows[_RecordRowIndex]["IsHasAlarm"].ToString() == "0")
        {
            AllVoucher.Tables[0].Rows[_RecordRowIndex]["IsAuditing"] = "1";
            AllVoucher.Tables[0].Rows[_RecordRowIndex]["Assessor"] = Session["RealName"].ToString();
            Session["AllVoucher"] = AllVoucher;
            CommClass.ExecuteSQL("update cw_voucher set IsAuditing='1',Assessor='" + Session["RealName"].ToString() + "' where id='" + _VoucherID + "'");
            //PageClass.ShowAlertMsg(this.Page, "凭证：" + _VoucherNo + "，审核成功！");
            //写入操作日志
            CommClass.WriteCTL_Log("100007", "凭证审核：" + _VoucherNo + "，凭证日期：" + AllVoucher.Tables[0].Rows[_RecordRowIndex]["voucherdate"].ToString());
            //--
        }
        else
        {
            //输出超限报警信息
            DataTable entry = CommClass.GetDataTable(string.Format("select SubjectNo,SubjectName from cw_entry where VoucherID='{0}' and ({1})", _VoucherID, string.Format(Alarms.Value, "<=-", ">=")));
            StringBuilder tips = new StringBuilder();
            tips.Append("以下分录科目超限：\\n\\n");
            foreach (DataRow row in entry.Rows)
            {
                string subjectno = row["SubjectNo"].ToString();
                tips.AppendFormat("- {0}.{1}\\n", subjectno, CommClass.GetFieldFromNo(subjectno, "SubjectName"));
            }
            tips.Append("\\n该凭证含有超限分录，必须由监督管理部门审核");
            PageClass.ShowAlertMsg(this.Page, tips.ToString());
        }
        InitWebControl();
    }
    protected void AuditAllVoucher_Click(object sender, EventArgs e)
    {
        //批量审核凭证
        DataTable vouchers = CommClass.GetDataTable(string.Format("select * from cw_voucher where VoucherDate like '{0}年{1}月%' and IsAuditing='0' and DelFlag='0'",
            aYear.Value, aMonth.Value));
        StringBuilder tips = new StringBuilder();
        for (int i = 0; i < vouchers.Rows.Count; i++)
        {
            if (vouchers.Rows[i]["IsAuditing"].ToString() == "0")
            {
                if (vouchers.Rows[i]["IsHasAlarm"].ToString() == "0")
                {
                    vouchers.Rows[i]["IsAuditing"] = "1";
                    vouchers.Rows[i]["Assessor"] = Session["RealName"].ToString();
                }
                else
                {
                    tips.AppendFormat(",{0}", vouchers.Rows[i]["voucherno"].ToString());
                }
            }
        }
        CommClass.UpdateDataTable(vouchers);
        //输出超限报警信息
        if (tips.Length > 0)
        {
            tips.Remove(0, 1);
            tips.Insert(0, "以下凭证含有超限分录：\\n\\n");
            tips.Append("\\n\\n以上凭证必须由监督管理部门审核");
            PageClass.ShowAlertMsg(this.Page, tips.ToString());
        }
        GetAllVoucher();
        InitWebControl();
        //PageClass.ShowAlertMsg(this.Page, "凭证批量审核成功！");
        //写入操作日志
        CommClass.WriteCTL_Log("100007", "凭证批量审核，审核月份：" + aYear.Value + "年" + aMonth.Value + "月");
        //--
    }
    protected void AuditQVoucher_Click(object sender, EventArgs e)
    {
        //按条件审核凭证
        GetAllVoucher();
        StringBuilder tips = new StringBuilder();
        for (int i = 0; i < AllVoucher.Tables[0].Rows.Count; i++)
        {
            if (AllVoucher.Tables[0].Rows[i]["IsAuditing"].ToString() == "0")
            {
                if (AllVoucher.Tables[0].Rows[i]["IsHasAlarm"].ToString() == "0")
                {
                    AllVoucher.Tables[0].Rows[i]["IsAuditing"] = "1";
                    AllVoucher.Tables[0].Rows[i]["Assessor"] = Session["RealName"].ToString();
                }
                else
                {
                    tips.AppendFormat(",{0}", AllVoucher.Tables[0].Rows[i]["voucherno"]);
                }
            }
        }
        CommClass.UpdateDataSet(AllVoucher);
        Session["AllVoucher"] = AllVoucher;
        InitWebControl();
        //输出超限报警信息
        if (tips.Length > 0)
        {
            tips.Remove(0, 1);
            tips.Insert(0, "以下凭证含有超限分录：\\n\\n");
            tips.Append("\\n\\n以上凭证必须由监督管理部门审核");
            PageClass.ShowAlertMsg(this.Page, tips.ToString());
        }
        //PageClass.ShowAlertMsg(this.Page, "凭证按条件审核成功！");
        //写入操作日志
        CommClass.WriteCTL_Log("100007", "凭证按条件审核，审核月份：" + aYear.Value + "年" + aMonth.Value + "月");
        //--
    }
    protected void BackControl_Click(object sender, EventArgs e)
    {
        int _RecordRowIndex = int.Parse(RecordRowIndex.Value);
        string _VoucherID = AllVoucher.Tables[0].Rows[_RecordRowIndex]["id"].ToString();
        string _VoucherNo = AllVoucher.Tables[0].Rows[_RecordRowIndex]["voucherno"].ToString();
        AllVoucher.Tables[0].Rows[_RecordRowIndex]["IsAuditing"] = "0";
        AllVoucher.Tables[0].Rows[_RecordRowIndex]["Assessor"] = "";
        Session["AllVoucher"] = AllVoucher;
        CommClass.ExecuteSQL("update cw_voucher set IsAuditing='0',Assessor='' where id='" + _VoucherID + "'");
        InitWebControl();
        //PageClass.ShowAlertMsg(this.Page, "凭证：" + _VoucherNo + "，反审核成功！");
        //写入操作日志
        CommClass.WriteCTL_Log("100007", "凭证反审核：" + _VoucherNo + "，凭证日期：" + AllVoucher.Tables[0].Rows[_RecordRowIndex]["voucherdate"].ToString());
        //--
    }
}
