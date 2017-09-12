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

public partial class AccountManage_YearCarryForward : System.Web.UI.Page
{
    private string G_AccountYear = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000007")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            CarryForward0.Attributes["onclick"] = "return _Confirm('您确定需要进行年末收支自动结转吗？','年末收支自动结转')";
            CarryForward1.Attributes["onclick"] = "return _Confirm('您确定需要进行年末结转吗？','年末结转')";
            BackCarryForward.Attributes["onclick"] = "return _Confirm('您确定需要进行反年末结转吗？','反年末结转')";
            //检测是否可进行年末结转
            Overlay.Attributes["style"] = "display:none";
            Lightbox.Attributes["style"] = "display:none";
            if (PageClass.CheckAccountUsed(true))
            {
                CarryForward0.Enabled = false;
                CarryForward1.Enabled = false;
                BackCarryForward.Enabled = false;
                Lightbox.InnerHtml = "<br />已有用户正在使用，请30秒后再尝试刷新！";
                PageClass.ExcuteScript(this.Page, "LimitControl();");
            }
            else
            {
                PageClass.ExcuteScript(this.Page, "RefreshD();");
            }
            InitWebControl();
        }
    }
    protected void InitWebControl()
    {
        DateTime AccountDate = MainClass.GetAccountDate();
        string LastCarryDate = MainClass.GetFieldFromID(UserInfo.AccountID, "LastCarryDate", "cw_account");
        BackCarryForward.Enabled = (LastCarryDate.Length > 0 && AccountDate.Month == 1);
        if (LastCarryDate == string.Format("{0}年12月31日", AccountDate.Year.ToString()))
        {
            string AccountYear = MainClass.GetFieldFromID(UserInfo.AccountID, "AccountYear", "cw_account");
            if (AccountYear.IndexOf(AccountDate.AddYears(1).Year.ToString()) == -1)
            {
                CarryForward0.Enabled = true;
                CarryForward1.Enabled = false;
            }
            else
            {
                CarryForward0.Enabled = false;
                CarryForward1.Enabled = true;
            }
        }
        else
        {
            CarryForward0.Enabled = false;
            CarryForward1.Enabled = false;
        }
        //软件使用日期检测
        if (CarryForward0.Enabled || CarryForward1.Enabled)
        {
            string UseDate = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo/UseDate");
            string useDate = string.Format("{0}年{1}月", UseDate.Substring(0, 4), UseDate.Substring(5, 2));
            if (string.Compare(useDate, AccountDate.ToString("yyyy年MM月")) <= 0)
            {
                PageClass.ShowAlertMsg(this.Page, string.Format("您当前所使用的软件到期日期：{0}，因此不可继续结转！", UseDate));
                CarryForward0.Enabled = false;
                CarryForward1.Enabled = false;
            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //年末收支自动结转
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void CarryForward0_Click(object sender, EventArgs e)
    {
        DateTime AccountDate = MainClass.GetAccountDate();
        string YearMonth = AccountDate.ToString("yyyy年MM月");
        if (CommClass.CheckExist("cw_voucher", string.Format("isrecord='0' and voucherdate like '{0}%'", YearMonth)))
        {
            ExeScript.Text = "<script>alert('部分凭证尚未审核或记账！');</script>";
            CarryForward0.Enabled = false;
            return;
        }
        int AccountYear = AccountDate.Year;
        //创建结转凭证
        string LastVoucherNo = CommClass.GetTableValue("cw_voucher", "voucherno", string.Format("voucherdate like '{0}%' order by voucherno desc", YearMonth));
        if (LastVoucherNo == "NoDataItem")
        {
            LastVoucherNo = "100000";
        }
        int VoucherNo = int.Parse(LastVoucherNo);
        DataTable VoucherEntry = CommClass.GetDataTable("select subjectno,beginbalance from cw_subject where isdetail='1' and subjectno like '5%' order by subjectno");
        //结转本年收支
        decimal SumIncome = 0;
        decimal SumExpense = 0;
        decimal SumProfit = 0;
        string ExpenseSubject = SysConfigs.ExpenseSubject;
        StringBuilder SQLString = new StringBuilder();
        SQLString.Append("insert into cw_entry(id,voucherid,vsummary,subjectno,summoney)values");
        foreach (DataRow row in VoucherEntry.Rows)
        {
            if (row["beginbalance"].ToString() == "0") { continue; }
            decimal EntrySumMoney = 0 - decimal.Parse(row["beginbalance"].ToString());
            string subjectNo = row["subjectno"].ToString();
            if (ExpenseSubject.IndexOf(subjectNo.Substring(0, 3)) == -1)
            {
                //结转收入
                SQLString.Append("('" + CommClass.GetRecordID("CW_Entry") + "','{$VoucherIncome$}','年末收支自动结转','" + subjectNo + "','" + EntrySumMoney.ToString() + "'),");
                SumIncome -= EntrySumMoney;
            }
            else
            {
                //结转支出
                SQLString.Append("('" + CommClass.GetRecordID("CW_Entry") + "','{$VoucherExpense$}','年末收支自动结转','" + subjectNo + "','" + EntrySumMoney.ToString() + "'),");
                SumExpense -= EntrySumMoney;
            }
        }
        if (SumIncome != 0)
        {
            string VoucherIncome = CreateNewVoucher(++VoucherNo, AccountYear);
            SQLString.Append("('" + CommClass.GetRecordID("CW_Entry") + "','{$VoucherIncome$}','年末收支自动结转','" + SysConfigs.YearProfitSubject + "','" + SumIncome.ToString() + "'),");
            SQLString.Replace("{$VoucherIncome$}", VoucherIncome);
        }
        if (SumExpense != 0)
        {
            string VoucherExpense = CreateNewVoucher(++VoucherNo, AccountYear);
            SQLString.Append("('" + CommClass.GetRecordID("CW_Entry") + "','{$VoucherExpense$}','年末收支自动结转','" + SysConfigs.YearProfitSubject + "','" + SumExpense.ToString() + "'),");
            SQLString.Replace("{$VoucherExpense$}", VoucherExpense);
        }
        //结转本年收益
        SumProfit = 0 - SumIncome - SumExpense;
        if (SumProfit > 0)
        {
            string VoucherProfit = CreateNewVoucher(++VoucherNo, AccountYear);
            SQLString.Append("('" + CommClass.GetRecordID("CW_Entry") + "','" + VoucherProfit + "','年末收支自动结转','" + SysConfigs.YearProfitSubject + "','" + SumProfit.ToString() + "'),");
            SQLString.Append("('" + CommClass.GetRecordID("CW_Entry") + "','" + VoucherProfit + "','年末收支自动结转','" + SysConfigs.UndistributedProfit + "','-" + SumProfit.ToString() + "'),");
        }
        //结转数据写入数据库
        if (SQLString.ToString().EndsWith(","))
        {
            SQLString = SQLString.Remove(SQLString.Length - 1, 1);
            CommClass.ExecuteSQL(SQLString.ToString());
        }
        SQLString.Length = 0;
        //设置财务年度为下年，并记录结转凭证编号，作为自动结转标识
        string YearCarryFlag = string.Empty;
        if (SumProfit <= 0)
        {
            YearCarryFlag = ",YearCarryFlag='0'";
        }
        else
        {
            YearCarryFlag = ",YearCarryFlag='1'";
        }
        MainClass.ExecuteSQL("update cw_account set accountyear=concat(accountyear,'|" + (AccountYear + 1).ToString() + "'),YearCarryVoucher='"
            + LastVoucherNo + "-" + VoucherNo.ToString() + "'" + YearCarryFlag + " where id='" + UserInfo.AccountID + "'");
        CarryForward0.Enabled = false;
        ExeScript.Text = "<script language=javascript>alert('恭喜您，年末收支自动结转成功！');location.href='DoVoucher.aspx';</script>";
        //写入操作日志
        CommClass.WriteCTL_Log("100009", "年末收支自动结转，年份：" + AccountYear.ToString());
    }
    private string CreateNewVoucher(int VoucherNo, int AccountYear)
    {
        DataSet Voucher = CommClass.GetDataSet("select * from cw_voucher where 1=2");
        DataRow NewDataRow = Voucher.Tables[0].NewRow();
        string VoucherID = CommClass.GetRecordID("CW_Voucher");
        NewDataRow["id"] = VoucherID;
        NewDataRow["voucherno"] = VoucherNo.ToString();
        NewDataRow["voucherfrom"] = "GA";
        NewDataRow["voucherdate"] = AccountYear.ToString() + "年12月31日";
        NewDataRow["IsAuditing"] = "0";
        NewDataRow["IsRecord"] = "0";
        NewDataRow["Director"] = MainClass.GetFieldFromID(UserInfo.AccountID, "director", "cw_account");
        NewDataRow["DoBill"] = Session["RealName"].ToString();
        NewDataRow["Assessor"] = "";
        NewDataRow["Accountant"] = "";
        NewDataRow["Addons"] = "";
        NewDataRow["AddonsCount"] = "";
        NewDataRow["IsHasAlarm"] = "0";
        NewDataRow["DelFlag"] = "0";
        Voucher.Tables[0].Rows.Add(NewDataRow);
        CommClass.UpdateDataSet(Voucher);
        return VoucherID;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //年末结转
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void CarryForward1_Click(object sender, EventArgs e)
    {
        string AccountID = UserInfo.AccountID;
        string YearCarryFlag = string.Empty;
        if (SysConfigs.LockYearCarry == "0")
        {
            YearCarryFlag = "0";
        }
        else
        {
            YearCarryFlag = MainClass.GetFieldFromID(AccountID, "YearCarryFlag", "cw_account");
        }
        string YearCarryVoucher = MainClass.GetFieldFromID(AccountID, "YearCarryVoucher", "cw_account");
        string[] CarryVoucherNo = YearCarryVoucher.Split('-');
        if (CarryVoucherNo.Length != 2)
        {
            ExeScript.Text = "<script>alert('年末结转失败，原因：参数不正确！');</script>";
            CarryForward1.Enabled = false;
            return;
        }
        int AccountYear = MainClass.GetAccountDate().Year;
        DataTable VoucherEntry = CommClass.GetDataTable("select voucherno,subjectno,summoney,isrecord from CW_VoucherEntry where voucherdate like '"
            + AccountYear.ToString() + "年12月%' and voucherno>'" + CarryVoucherNo[0] + "' and delflag='0' order by voucherno asc");
        if (VoucherEntry.Select("isrecord='0'").Length > 0)
        {
            ExeScript.Text = "<script>alert('部分凭证尚未审核或记账！');</script>";
            CarryForward1.Enabled = false;
            return;
        }
        if (YearCarryFlag == "1" && VoucherEntry.Rows.Count > 0 && VoucherEntry.Select("voucherno>'" + CarryVoucherNo[1] + "'").Length == 0)
        {
            ExeScript.Text = "<script>alert('年末收支自动结转后，尚未填写凭证！');</script>";
        }
        else
        {
            int SubjectLength = 0;
            Hashtable SumMoney = new Hashtable();
            string[] SubjectLevel = SysConfigs.SubjectLevel.Split(',');
            foreach (DataRow row in VoucherEntry.Rows)
            {
                string SubjectNo = (row["summoney"].ToString().IndexOf("-") == -1 ? "+" : "-") + row["subjectno"].ToString();
                if (SumMoney.ContainsKey(SubjectNo))
                {
                    SumMoney[SubjectNo] = decimal.Parse(SumMoney[SubjectNo].ToString()) + decimal.Parse(row["summoney"].ToString().Replace("-", ""));
                }
                else
                {
                    SumMoney.Add(SubjectNo, row["summoney"].ToString().Replace("-", ""));
                }
                //结转入收益分配表
                if (string.Compare(row["voucherno"].ToString(), CarryVoucherNo[1]) > 0)
                {
                    for (int i = 1; i < SubjectLevel.Length; i++)
                    {
                        SubjectLength = int.Parse(SubjectLevel[i]);
                        if (SubjectLength <= SubjectNo.Length - 1)
                        {
                            DoLastMonthSum(SubjectNo, SubjectLength, row["summoney"].ToString().Replace("-", ""), AccountYear);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            foreach (DictionaryEntry de in SumMoney)
            {
                for (int i = 1; i < SubjectLevel.Length; i++)
                {
                    SubjectLength = int.Parse(SubjectLevel[i]);
                    if (SubjectLength <= de.Key.ToString().Length - 1)
                    {
                        DoRecordAccount(de.Key.ToString(), SubjectLength, de.Value.ToString(), AccountYear);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            int NextAccountYear = AccountYear + 1;
            G_AccountYear = NextAccountYear.ToString();
            //设置全年预算
            DataTable SubjectList = CommClass.GetDataTable("select subjectno from cw_subject where parentno='000' and (subjectno like '5%' or subjectno='212') order by subjectno");
            StringBuilder InsertBudget = new StringBuilder();
            InsertBudget.Append("insert into cw_subjectbudget(subjectno,budgetyear,budgetbalance)values");
            foreach (DataRow row in SubjectList.Rows)
            {
                SetSubjectBudget(row["subjectno"].ToString(), InsertBudget);
            }
            if (InsertBudget.ToString().EndsWith(","))
            {
                InsertBudget.Remove(InsertBudget.Length - 1, 1);
                CommClass.ExecuteSQL(InsertBudget.ToString());
            }
            InsertBudget.Length = 0;
            //设置账套信息
            string nowADate = NextAccountYear.ToString() + "年01月01日";
            MainClass.ExecuteSQL("update cw_account set accountdate='" + nowADate + "' where id='" + AccountID + "'");
            string UADate = "parent.document.getElementById('ctl00_LeftFrame1_CurAccountDate').innerText='财务日期：" + nowADate + "';";
            ExeScript.Text = "<script language=javascript>" + UADate + "alert('恭喜您，年末结转成功！');location.href='DoVoucher.aspx';</script>";
            BackCarryForward.Enabled = true;
            //写入操作日志
            CommClass.WriteCTL_Log("100009", "年末结转，年份：" + AccountYear.ToString());
            DbHelper.ValidateVerifySignedHash();
            //执行数据备份
            if (SysConfigs.AutoBackupData == "1")
            {
                MySQLClass.BackupAllTable(AccountYear.ToString() + "年年末结转备份（自动）");
            }
        }
        CarryForward1.Enabled = false;
    }
    //年末科目记账
    private void DoRecordAccount(string SubjectNo, int SubjectLength, string SubjectSum, int AccountYear)
    {
        string LOFlag = SubjectNo.Substring(0, 1);
        SubjectNo = SubjectNo.Substring(1, SubjectLength);
        string SubjectID = CommClass.GetFieldFromNo(SubjectNo, "id");
        if (LOFlag == "-")
        {
            CommClass.ExecuteSQL("update cw_subjectsum set onloan=onloan+(" + SubjectSum + "),onloansum=onloansum+(" + SubjectSum + "),"
                + "finalbalance=finalbalance-(" + SubjectSum + ") where subjectid='"
                + SubjectID + "' and yearmonth='" + AccountYear.ToString() + "年12月'");
            CommClass.ExecuteSQL("update cw_subject set beginbalance=beginbalance-(" + SubjectSum + ") where id='" + SubjectID + "'");
        }
        else
        {
            CommClass.ExecuteSQL("update cw_subjectsum set lead=lead+(" + SubjectSum + "),leadsum=leadsum+(" + SubjectSum + "),"
                + "finalbalance=finalbalance+(" + SubjectSum + ") where subjectid='"
                + SubjectID + "' and yearmonth='" + AccountYear.ToString() + "年12月'");
            CommClass.ExecuteSQL("update cw_subject set beginbalance=beginbalance+(" + SubjectSum + ") where id='" + SubjectID + "'");
        }
    }
    //结转入收益分配表
    private void DoLastMonthSum(string SubjectNo, int SubjectLength, string SubjectSum, int AccountYear)
    {
        string LOFlag = SubjectNo.Substring(0, 1);
        SubjectNo = SubjectNo.Substring(1, SubjectLength);
        string SubjectID = CommClass.GetFieldFromNo(SubjectNo, "id");
        if (LOFlag == "-")
        {
            CommClass.ExecuteSQL("update cw_lastmonthsum set onloan=onloan+(" + SubjectSum + "),onloansum=onloansum+(" + SubjectSum + "),"
                + "finalbalance=finalbalance-(" + SubjectSum + ") where subjectid='"
                + SubjectID + "' and yearmonth='" + AccountYear.ToString() + "年12月'");
        }
        else
        {
            CommClass.ExecuteSQL("update cw_lastmonthsum set lead=lead+(" + SubjectSum + "),leadsum=leadsum+(" + SubjectSum + "),"
                + "finalbalance=finalbalance+(" + SubjectSum + ") where subjectid='"
                + SubjectID + "' and yearmonth='" + AccountYear.ToString() + "年12月'");
        }
    }
    //设置全年预算
    private void SetSubjectBudget(string subjectNo, StringBuilder InsertBudget)
    {
        InsertBudget.AppendFormat("('{0}','{1}','0'),", subjectNo, G_AccountYear);
        DataTable child = CommClass.GetDataTable(string.Concat("select subjectno from cw_subject where parentno='", subjectNo, "'"));
        foreach (DataRow rowc in child.Rows)
        {
            SetSubjectBudget(rowc["subjectno"].ToString(), InsertBudget);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //反年末结转
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void BackCarryForward_Click(object sender, EventArgs e)
    {
        int SubjectLength = 0;
        DateTime AccountDate = MainClass.GetAccountDate();
        int AccountYear = AccountDate.Year - 1;
        string ThisYearMonth = AccountYear.ToString() + "年12月";
        string[] SubjectLevel = SysConfigs.SubjectLevel.Split(',');
        string YearCarryVoucher = MainClass.GetFieldFromID(UserInfo.AccountID, "YearCarryVoucher", "cw_account");
        if (YearCarryVoucher.IndexOf("-") == -1)
        {
            ExeScript.Text = "<script language=javascript>alert('反年末结转失败！');</script>";
            return;
        }
        //反年末结转
        string[] CarryVoucherNo = YearCarryVoucher.Split('-');
        DataTable VoucherEntry = CommClass.GetDataTable("select voucherid,voucherno,subjectno,summoney,isrecord from CW_VoucherEntry where voucherdate like '"
            + ThisYearMonth + "%' and voucherno>'" + CarryVoucherNo[0] + "' and delflag='0' order by voucherno");
        Hashtable SumMoney = new Hashtable();
        StringBuilder AllVoucher = new StringBuilder();
        foreach (DataRow row in VoucherEntry.Rows)
        {
            string SubjectNo = (row["summoney"].ToString().IndexOf("-") == -1 ? "+" : "-") + row["subjectno"].ToString();
            if (SumMoney.ContainsKey(SubjectNo))
            {
                SumMoney[SubjectNo] = decimal.Parse(SumMoney[SubjectNo].ToString()) + decimal.Parse(row["summoney"].ToString().Replace("-", ""));
            }
            else
            {
                SumMoney.Add(SubjectNo, row["summoney"].ToString().Replace("-", ""));
            }
            AllVoucher.Append(",'" + row["voucherid"].ToString() + "'");
            //结转出收益分配表
            if (string.Compare(row["voucherno"].ToString(), CarryVoucherNo[1]) > 0)
            {
                for (int i = 1; i < SubjectLevel.Length; i++)
                {
                    SubjectLength = int.Parse(SubjectLevel[i]);
                    if (SubjectLength <= SubjectNo.Length - 1)
                    {
                        DoLastMonthSum2(SubjectNo, SubjectLength, row["summoney"].ToString().Replace("-", ""), AccountYear);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        if (AllVoucher.Length > 0)
        {
            AllVoucher.Remove(0, 1);
            foreach (DictionaryEntry de in SumMoney)
            {
                for (int i = 1; i < SubjectLevel.Length; i++)
                {
                    SubjectLength = int.Parse(SubjectLevel[i]);
                    if (SubjectLength <= de.Key.ToString().Length - 1)
                    {
                        DoRecordAccount2(de.Key.ToString(), SubjectLength, de.Value.ToString(), AccountYear);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            CommClass.ExecuteSQL("delete from CW_Voucher where voucherdate like '" + ThisYearMonth + "%' and voucherno>'" + CarryVoucherNo[0] + "'");
            CommClass.ExecuteSQL("delete from CW_Entry where voucherid in (" + AllVoucher.ToString() + ")");
        }
        string nowYear = AccountDate.Year.ToString();
        MainClass.ExecuteSQL(string.Format("update cw_account set AccountDate='{0}31日',AccountYear=replace(AccountYear,'|{1}',''),YearCarryVoucher='' where id='{2}'",
            ThisYearMonth, nowYear, UserInfo.AccountID));
        string UADate = "parent.document.getElementById('ctl00_LeftFrame1_CurAccountDate').innerText='财务日期：" + ThisYearMonth + "31日';";
        ExeScript.Text = "<script language=javascript>" + UADate + "alert('恭喜您，反年末结转成功！');</script>";
        BackCarryForward.Enabled = false;
        CarryForward0.Enabled = true;
        //删除全年预算
        CommClass.ExecuteSQL("delete from CW_SubjectBudget where budgetyear='" + nowYear + "'");
        //写入操作日志
        CommClass.WriteCTL_Log("100009", "反年末结转，反结转至年份：" + AccountYear.ToString());
    }
    //反年末科目记账
    private void DoRecordAccount2(string SubjectNo, int SubjectLength, string SubjectSum, int AccountYear)
    {
        string LOFlag = SubjectNo.Substring(0, 1);
        SubjectNo = SubjectNo.Substring(1, SubjectLength);
        string SubjectID = CommClass.GetFieldFromNo(SubjectNo, "id");
        if (LOFlag == "-")
        {
            CommClass.ExecuteSQL("update cw_subjectsum set onloan=onloan-(" + SubjectSum + "),onloansum=onloansum-(" + SubjectSum + "),"
                + "finalbalance=finalbalance+(" + SubjectSum + ") where subjectid='"
                + SubjectID + "' and yearmonth='" + AccountYear.ToString() + "年12月'");
            CommClass.ExecuteSQL("update cw_subject set beginbalance=beginbalance+(" + SubjectSum + ") where id='" + SubjectID + "'");
        }
        else
        {
            CommClass.ExecuteSQL("update cw_subjectsum set lead=lead-(" + SubjectSum + "),leadsum=leadsum-(" + SubjectSum + "),"
                + "finalbalance=finalbalance-(" + SubjectSum + ") where subjectid='"
                + SubjectID + "' and yearmonth='" + AccountYear.ToString() + "年12月'");
            CommClass.ExecuteSQL("update cw_subject set beginbalance=beginbalance-(" + SubjectSum + ") where id='" + SubjectID + "'");
        }
    }
    //结转出收益分配表
    private void DoLastMonthSum2(string SubjectNo, int SubjectLength, string SubjectSum, int AccountYear)
    {
        string LOFlag = SubjectNo.Substring(0, 1);
        SubjectNo = SubjectNo.Substring(1, SubjectLength);
        string SubjectID = CommClass.GetFieldFromNo(SubjectNo, "id");
        if (LOFlag == "-")
        {
            CommClass.ExecuteSQL("update cw_lastmonthsum set onloan=onloan-(" + SubjectSum + "),onloansum=onloansum-(" + SubjectSum + "),"
                + "finalbalance=finalbalance+(" + SubjectSum + ") where subjectid='"
                + SubjectID + "' and yearmonth='" + AccountYear.ToString() + "年12月'");
        }
        else
        {
            CommClass.ExecuteSQL("update cw_lastmonthsum set lead=lead-(" + SubjectSum + "),leadsum=leadsum-(" + SubjectSum + "),"
                + "finalbalance=finalbalance-(" + SubjectSum + ") where subjectid='"
                + SubjectID + "' and yearmonth='" + AccountYear.ToString() + "年12月'");
        }
    }
}
