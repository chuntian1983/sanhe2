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
using System.Collections.Generic;

public partial class AccountManage_MonthCarryForward : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000006")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            DateTime accountDate = MainClass.GetAccountDate();
            string ThisYearMonth = accountDate.ToString("yyyy年MM月");
            AccountDate.Attributes.Add("readonly", "readonly");
            AccountDate.Text = accountDate.ToString("yyyy年MM月dd日");
            CurrentDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            AccountCarry.Attributes["onclick"] = "return ShowWaitBox('建议您月末结转前，请对数据进行完全备份！\\n\\n您确定需要进行月末结转操作吗？');";
            StringBuilder backTip = new StringBuilder();
            backTip.Append("return ShowWaitBox('建议您反结转前，请对数据进行完全备份！\\n\\n");
            backTip.Append("您确定需要反结转至上月吗？\\n\\n注意事项：");
            backTip.Append("\\n\\n1、反结转将清除上月结转科目余额但保留所有凭证数据；");
            backTip.Append("\\n\\n2、固定资产模块数据不参与反结转，且该模块将被锁定。');");
            BackCarry.Attributes["onclick"] = backTip.ToString();
            BackupDate.Attributes["onclick"] = "return ShowWaitBox('您确定需要进行月末结转前数据完全备份吗？');";
            //若使用固定资产则不可空结转
            if (SysConfigs.CheckFixedAssetModel == "0" || CommClass.CheckExist("cw_assetcard", "1=1") == false)
            {
                for (int i = accountDate.Month + 1; i <= 12; i++)
                {
                    string LastDay = DateTime.DaysInMonth(accountDate.Year, i).ToString("00");
                    EndCarryDate.Items.Add(string.Format("{0}年{1}月{2}日", accountDate.Year.ToString(), i.ToString("00"), LastDay));
                }
            }
            //检测凭证是否全部审核和记账
            DataRowCollection rowc = CommClass.GetDataRows("select voucherno,IsAuditing,IsRecord from cw_voucher where voucherdate like '%" + ThisYearMonth + "%' order by voucherno");
            if (rowc == null)
            {
                VoucherCount.Text = "0";
                VoucherNoList.Text = "无凭证！";
                AccountCarry.Enabled = true;
            }
            else
            {
                VoucherCount.Text = rowc.Count.ToString();
                VoucherNoList.Text = rowc[0]["voucherno"].ToString() + " ---- " + rowc[rowc.Count - 1]["voucherno"].ToString();
                StringBuilder HasAudit = new StringBuilder();
                StringBuilder HasRecord = new StringBuilder();
                foreach (DataRow row in rowc)
                {
                    if (row["IsAuditing"].ToString() == "0")
                    {
                        HasAudit.Append("，" + row["voucherno"].ToString());
                    }
                    if (row["IsRecord"].ToString() == "0")
                    {
                        HasRecord.Append("，" + row["voucherno"].ToString());
                    }
                }
                if (HasRecord.Length == 0)
                {
                    Label5.Text = "已全部审核！";
                    Label6.Text = "已全部记账！";
                    AccountCarry.Enabled = true;
                }
                else
                {
                    if (HasAudit.Length == 0)
                    {
                        Label5.Text = "已全部审核！";
                    }
                    else
                    {
                        Label5.Text = HasAudit.ToString().Substring(1);
                    }
                    Label6.Text = HasRecord.ToString().Substring(1);
                    ExeScript.Text = "<script language=javascript>alert('部分凭证尚未审核或记账！');</script>";
                    AccountCarry.Enabled = false;
                }
            }
            //建账首月及1月不能反结转
            string LastCarryDate = MainClass.GetFieldFromID(UserInfo.AccountID, "LastCarryDate", "cw_account");
            string StartAccountDate = MainClass.GetFieldFromID(UserInfo.AccountID, "StartAccountDate", "cw_account");
            if ((string.Compare(ThisYearMonth, StartAccountDate.Substring(0, 8)) <= 0 && LastCarryDate.Length == 0) || accountDate.Month == 1)
            {
                BackCarry.Enabled = false;
            }
            //最后一个月结转，若已年末收支自动结转则不可反结转
            if (LastCarryDate == string.Format("{0}年12月31日", accountDate.Year.ToString()))
            {
                AccountCarry.Enabled = false;
                accountDate = accountDate.AddYears(1);
                string AccountYear = MainClass.GetFieldFromID(UserInfo.AccountID, "AccountYear", "cw_account");
                if (AccountYear.IndexOf(accountDate.Year.ToString()) != -1)
                {
                    BackCarry.Enabled = false;
                }
            }
            //模拟月末结转
            if (AccountCarry.Enabled)
            {
                CarryForward();
            }
            //检测是否可进行月末结转
            Overlay.Attributes["style"] = "display:none";
            Lightbox.Attributes["style"] = "display:none";
            PageClass.CheckMainDllHash();
            if (PageClass.CheckAccountUsed(true))
            {
                ExeScript.Text = "";
                AccountCarry.Enabled = false;
                BackCarry.Enabled = false;
                BackupDate.Enabled = false;
                Lightbox.InnerHtml = "已有用户正在使用，请30秒后再尝试刷新！";
                PageClass.ExcuteScript(this.Page, "LimitControl('DoOverLay','Overlay','Lightbox');");
            }
            else
            {
                PageClass.ExcuteScript(this.Page, "RefreshD();");
            }
        }
    }
    protected void AccountCarry_Click(object sender, EventArgs e)
    {
        StringBuilder SQLString = new StringBuilder();
        DateTime accountDate = Convert.ToDateTime(AccountDate.Text);
        string ThisYearMonth = accountDate.ToString("yyyy年MM月");
        //软件使用日期检测
        string UseDate = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo/UseDate");
        string useDate = string.Format("{0}年{1}月", UseDate.Substring(0, 4), UseDate.Substring(5, 2));
        if (string.Compare(useDate, ThisYearMonth) <= 0)
        {
            PageClass.ShowAlertMsg(this.Page, string.Format("您当前所使用的软件到期日期：{0}，因此不可继续结转！", UseDate));
            AccountCarry.Enabled = false;
            return;
        }
        //--
        string RealLastCarry = MainClass.GetFieldFromID(UserInfo.AccountID, "RealLastCarry", "cw_account");
        //检测是否进行固定资产处理
        if (SysConfigs.CheckFixedAssetModel == "1" && string.Compare(RealLastCarry, ThisYearMonth) < 0)
        {
            //检测是否需要计提折旧或制单操作
            if (CommClass.CheckExist("cw_assetcard", "CVoucher='0' and UseState='101' and DeprMethod='1'"))
            {
                ExeScript.Text = "<script language=javascript>alert('请进行固定资产计提折旧或制单操作！');location.href='../FixedAsset/CarryMonthDepr.aspx';</script>";
                return;
            }
            //输出待清理资产原值
            DataTable DeprAsset = CommClass.GetDataTable("select ID,ClassID,AssetName,OldPrice,ZheJiu,ThisZJ from CW_AssetCard where UseState='101' and DeprMethod='1' and CVoucher='1' and DeprState='1' and AssetState='1' and ThisZJ=0 order by ClassID asc");
            foreach (DataRow row in DeprAsset.Rows)
            {
                CommClass.ExecuteSQL("insert into cw_assetda(id,cardid,classid,voucherid,voucherdate,vsummary,oldprice0,oldprice1,zhejiu0,zhejiu1,thiszhejiu)values('"
                    + CommClass.GetRecordID("CW_AssetDA") + "','"
                    + row["ID"].ToString() + "','"
                    + row["ClassID"].ToString() + "','0000000000','"
                    + AccountDate.Text + "','"
                    + row["AssetName"].ToString() + "','0','"
                    + row["OldPrice"].ToString() + "','0','"
                    + row["ZheJiu"].ToString() + "','"
                    + row["ThisZJ"].ToString() + "')");
            }
        }
        /////////////////////////////////////////////////////////////
        StringBuilder CarryYearMonths = new StringBuilder();
        CarryYearMonths.Append(ThisYearMonth);
        ClsCalculate.VerifyHashValue();
        //删除因网络故障残留的月结数据
        if (CommClass.CheckExist("cw_subjectsum", "yearmonth='" + ThisYearMonth + "'"))
        {
            CommClass.ExecuteSQL("delete from cw_subjectsum where yearmonth='" + ThisYearMonth + "'");
        }
        //写入科目余额表
        //CommClass.ExecuteTransaction(CarrySqlString.Value);
        CommClass.ExecuteTransaction(ViewState["CarrySqlString"].ToString());
        //更新凭证科目名称
        DataTable VoucherEntry = CommClass.GetDataTable(string.Concat("select id,subjectno from cw_voucherentry where voucherdate>='", ThisYearMonth, "01日' or (subjectname is null) or subjectname='' or (subjectname like '%NoDataItem%')"));
        foreach (DataRow row in VoucherEntry.Rows)
        {
            CommClass.ExecuteSQL(string.Format("update cw_entry set subjectname='{0}' where id='{1}'", CommClass.GetSubjectNo2Name(row["subjectno"].ToString()), row["id"].ToString()));
        }
        //封存本月科目表
        string sqlTemplate = "select * from cw_subjectrec where YearMonth='{0}'";
        UpdateTableData(string.Format(sqlTemplate, ThisYearMonth), CommClass.GetDataTable("select * from cw_subject"), "YearMonth", ThisYearMonth);
        //空月末结转
        if (EndCarryDate.SelectedValue != "000000")
        {
            DataTable dt = CommClass.GetDataTable("select SubjectID,SubjectNo,SubjectName,BeginBalance,LeadSum,OnloanSum,FinalBalance from cw_viewsubjectsum where yearmonth='" + ThisYearMonth + "'");
            SQLString.Append("insert into cw_subjectsum(subjectid,subjectno,subjectname,beginbalance,lead,onloan,leadsum,onloansum,FinalBalance,YearMonth)values");
            foreach (DataRow row in dt.Rows)
            {
                SQLString.Append("('"
                    + row["SubjectID"].ToString() + "','"
                    + row["SubjectNo"].ToString() + "','"
                    + row["SubjectName"].ToString() + "','"
                    + row["FinalBalance"].ToString() + "','0','0','"
                    + row["LeadSum"].ToString() + "','"
                    + row["OnloanSum"].ToString() + "','"
                    + row["FinalBalance"].ToString() + "','{$YearMonth$}'),");
            }
            if (SQLString.ToString().EndsWith(","))
            {
                SQLString = SQLString.Remove(SQLString.Length - 1, 1);
                string sqlString = SQLString.ToString();
                for (int i = 1; i <= EndCarryDate.SelectedIndex; i++)
                {
                    string YearMonth = EndCarryDate.Items[i].Value.Substring(0, 8);
                    //软件使用日期检测
                    if (string.Compare(useDate, YearMonth) <= 0)
                    {
                        PageClass.ShowAlertMsg(this.Page, string.Format("您当前所使用的软件到期日期：{0}，因此不可继续结转！", UseDate));
                        AccountCarry.Enabled = false;
                        break;
                    }
                    //--
                    CommClass.ExecuteSQL("delete from cw_subjectsum where yearmonth='" + YearMonth + "'");
                    CommClass.ExecuteSQL(sqlString.Replace("{$YearMonth$}", YearMonth));
                    accountDate = Convert.ToDateTime(EndCarryDate.Items[i].Value);
                    CarryYearMonths.Append("，" + YearMonth);
                    //封存本月科目表
                    UpdateTableData(string.Format(sqlTemplate, YearMonth), CommClass.GetDataTable("select * from cw_subject"), "YearMonth", YearMonth);
                }
            }
            ThisYearMonth = accountDate.ToString("yyyy年MM月");
            SQLString.Length = 0;
        }
        //12月份月科目余额备份
        if (accountDate.Month == 12)
        {
            string lastYearMonth = accountDate.Year.ToString() + "年12月";
            DataTable dt = CommClass.GetDataTable("select SubjectID,SubjectNo,SubjectName,BeginBalance,Lead,Onloan,LeadSum,OnloanSum,FinalBalance from cw_subjectsum where YearMonth='" + lastYearMonth + "'");
            SQLString.Append("insert into cw_lastmonthsum(subjectid,subjectno,subjectname,beginbalance,lead,onloan,leadsum,onloansum,FinalBalance,YearMonth)values");
            foreach (DataRow row in dt.Rows)
            {
                SQLString.Append("('"
                    + row["SubjectID"].ToString() + "','"
                    + row["SubjectNo"].ToString() + "','"
                    + row["SubjectName"].ToString() + "','"
                    + row["BeginBalance"].ToString() + "','"
                    + row["Lead"].ToString() + "','"
                    + row["Onloan"].ToString() + "','"
                    + row["LeadSum"].ToString() + "','"
                    + row["OnloanSum"].ToString() + "','"
                    + row["FinalBalance"].ToString() + "','"
                    + lastYearMonth + "'),");
            }
            if (SQLString.ToString().EndsWith(","))
            {
                SQLString = SQLString.Remove(SQLString.Length - 1, 1);
                CommClass.ExecuteSQL(SQLString.ToString());
            }
            SQLString.Length = 0;
        }
        //设置财务日期
        string NextAccountDate = string.Empty;
        if (accountDate.Month == 12)
        {
            NextAccountDate = accountDate.ToString("yyyy年MM月dd日");
        }
        else
        {
            NextAccountDate = accountDate.AddMonths(1).ToString("yyyy年MM月01日");
        }
        //月末结转真实标识（反结转都不变）
        string UpdateRealLastCarry = string.Empty;
        if (string.Compare(RealLastCarry, ThisYearMonth) < 0)
        {
            UpdateRealLastCarry = ",RealLastCarry='" + ThisYearMonth + "'";
            //固定资产处理状态调整
            CommClass.ExecuteSQL("update CW_AssetCard set DeprState='0',CVoucher='0' where AssetState='0'");
            CommClass.ExecuteSQL("update CW_AssetCard set ThisZJ=0 where ThisZJ<>0");
        }
        //更新账套结转信息
        MainClass.ExecuteSQL("update cw_account set accountdate='" + NextAccountDate + "',LastCarryDate='" + accountDate.ToString("yyyy年MM月dd日")
            + "'" + UpdateRealLastCarry + " where id='" + UserInfo.AccountID + "'");
        ///////////////////////////////////////////////////
        AccountCarry.Enabled = false;
        BackCarry.Enabled = true;
        AccountDate.Text = NextAccountDate;
        //备用金检测
        string byjTip = string.Empty;
        string byjSno = ConfigurationManager.AppSettings["BeiYongJin"];
        if (byjSno != null && byjSno.Length > 0)
        {
            decimal byj0 = TypeParse.StrToDecimal(MainClass.GetTableValue("cw_account", "define3", "id='" + UserInfo.AccountID + "'"), 0);
            decimal byj1 = TypeParse.StrToDecimal(CommClass.GetTableValue("cw_subject", "beginbalance", "subjectno='" + byjSno + "'"), 0);
            if (byj0 <= byj1)
            {
                byjTip = "备用金已超出上限，请及时处理！";
            }
        }
        //--
        string UADate = "parent.document.getElementById('ctl00_LeftFrame1_CurAccountDate').innerText='财务日期：" + NextAccountDate + "';";
        ExeScript.Text = "<script>" + UADate + "alert('恭喜您，月末结转成功！" + byjTip + "');</script>";
        TextBox1.Text = TextBox4.Text;
        TextBox2.Text = "0.00";
        TextBox3.Text = "0.00";
        //写入操作日志
        CommClass.WriteCTL_Log("100009", "月末结转，年月：" + CarryYearMonths.ToString());
        //执行数据备份
        if (SysConfigs.AutoBackupData == "1")
        {
            MySQLClass.BackupAllTable(ThisYearMonth + "月末结转备份（自动）");
        }
    }
    public bool UpdateTableData(string sql, DataTable table, string fieldName, string fieldValue)
    {
        try
        {
            if (fieldValue == null)
            {
                if (table.Columns.Contains(fieldName))
                {
                    table.Columns.Remove(fieldName);
                }
            }
            StringBuilder columns = new StringBuilder();
            for (int k = 0; k < table.Columns.Count; k++)
            {
                columns.AppendFormat(",{0}", table.Columns[k].ColumnName);
            }
            columns.Remove(0, 1);
            sql = sql.Replace("*", columns.ToString());
            DataTable udata = CommClass.GetDataTable(sql);
            if (udata != null)
            {
                for (int i = 0; i < udata.Rows.Count; i++)
                {
                    udata.Rows[i].Delete();
                }
                if (fieldValue == null)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        udata.Rows.Add(table.Rows[i].ItemArray);
                    }
                }
                else
                {
                    string id = DateTime.Now.ToString("yyyyMMddHHmmss");
                    udata.Columns.Add(fieldName);
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        udata.Rows.Add(table.Rows[i].ItemArray);
                        udata.Rows[i][fieldName] = fieldValue;
                        udata.Rows[i]["id"] = id + i.ToString("000");
                    }
                }
                CommClass.UpdateDataTable(udata);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
    protected void CarryForward()
    {
        //模拟月末结转
        DateTime accountDate = MainClass.GetAccountDate();
        string ThisYearMonth = accountDate.ToString("yyyy年MM月");
        string PreYearMonth = accountDate.AddMonths(-1).ToString("yyyy年MM月");
        int MonthLastDay = DateTime.DaysInMonth(accountDate.Year, accountDate.Month);
        if (accountDate.Day != MonthLastDay)
        {
            AccountCarry.Enabled = false;
            AccountCarryDate.Value = ThisYearMonth + MonthLastDay.ToString("00") + "日";
            ExeScript.Text = "<script>AutoSetDate('" + AccountCarryDate.Value + "');</script>";
        }
        /////////////////////////////////////////////////
        List<string> SubjectID = new List<string>();
        List<string> SubjectNo = new List<string>();
        List<decimal> MBeginBalance = new List<decimal>();
        /////////////////////////////////////////////////
        //本期借贷累计
        Hashtable LeadSum = new Hashtable();
        Hashtable OnloanSum = new Hashtable();
        /////////////////////////////////////////////////
        //获取科目列表
        DataSet ds = CommClass.GetDataSet("select id,subjectno,beginbalance from cw_subject");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            SubjectID.Add(row["id"].ToString());
            SubjectNo.Add(row["subjectno"].ToString());
            MBeginBalance.Add(decimal.Parse(row["beginbalance"].ToString()));
        }
        //获取上期借贷累计额
        ds = CommClass.GetDataSet("select subjectno,LeadSum,OnloanSum from cw_viewsubjectsum where yearmonth='" + PreYearMonth + "'");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            string subjectNo = row["subjectno"].ToString();
            if (OnloanSum.ContainsKey(subjectNo)) { continue; }
            //当年一月借贷累计清零
            if (accountDate.Month == 1)
            {
                LeadSum.Add(subjectNo, "0");
                OnloanSum.Add(subjectNo, "0");
            }
            else
            {
                LeadSum.Add(subjectNo, row["LeadSum"].ToString());
                OnloanSum.Add(subjectNo, row["OnloanSum"].ToString());
            }
        }
        //获取本月发生额
        ds = CommClass.GetDataSet("select subjectno,summoney from CW_VoucherEntry where voucherdate like '" + ThisYearMonth + "%' and delflag='0'");
        ////////////////////////////////////////
        //汇总本月科目借贷方余额以及生成期末余额
        ////////////////////////////////////////
        //单个科目本月借贷累计
        decimal EntrySumMoney = 0;
        decimal MLeadSum = 0;
        decimal MOnloanSum = 0;
        decimal MFinalBalance = 0;
        ////////////////////////////////////////
        //单个科目本年借贷累计
        decimal YLeadSum = 0;
        decimal YOnloanSum = 0;
        ////////////////////////////////////////
        //一级科目本月借贷累计
        decimal CountBeginBalance = 0;
        decimal CountLead = 0;
        decimal CountOnloan = 0;
        decimal CountFinalBalance = 0;
        ////////////////////////////////////////
        StringBuilder SQLString = new StringBuilder();
        ////////////////////////////////////////
        for (int i = 0; i < SubjectNo.Count; i++)
        {
            //单个科目本月借贷累计
            MLeadSum = 0;
            MOnloanSum = 0;
            DataRow[] EntryList = ds.Tables[0].Select("subjectno like '" + SubjectNo[i] + "%'");
            for (int k = 0; k < EntryList.Length; k++)
            {
                decimal.TryParse(EntryList[k]["summoney"].ToString(), out EntrySumMoney);
                if (EntrySumMoney > 0)
                {
                    MLeadSum += EntrySumMoney;
                }
                else
                {
                    MOnloanSum -= EntrySumMoney;
                }
            }
            MFinalBalance = MBeginBalance[i] + MLeadSum - MOnloanSum;
            //单个科目本年借贷累计
            YLeadSum = MLeadSum;
            YOnloanSum = MOnloanSum;
            if (LeadSum.ContainsKey((object)SubjectNo[i]))
            {
                YLeadSum += Convert.ToDecimal(LeadSum[(object)SubjectNo[i]]);
            }
            if (OnloanSum.ContainsKey((object)SubjectNo[i]))
            {
                YOnloanSum += Convert.ToDecimal(OnloanSum[(object)SubjectNo[i]]);
            }
            //一级科目本月借贷累计
            if (SubjectNo[i].Length == 3)
            {
                CountLead += MLeadSum;
                CountOnloan += MOnloanSum;
                CountBeginBalance += MBeginBalance[i];
            }
            //写入科目余额表
            SQLString.Append("insert into cw_subjectsum(subjectid,subjectno,yearmonth,beginbalance,lead,onloan,leadsum,onloansum,FinalBalance)values('"
                + SubjectID[i] + "','"
                + SubjectNo[i] + "','"
                + ThisYearMonth + "','"
                + MBeginBalance[i].ToString() + "','"
                + MLeadSum.ToString() + "','"
                + MOnloanSum.ToString() + "','"
                + YLeadSum.ToString() + "','"
                + YOnloanSum.ToString() + "','"
                + MFinalBalance.ToString() + "')#sql#");
            //更新科目表期初余额
            SQLString.Append("update cw_subject set beginbalance='" + MFinalBalance.ToString() + "' where id='" + SubjectID[i] + "'#sql#");
        }
        //CarrySqlString.Value = SQLString.ToString();
        ViewState["CarrySqlString"] = SQLString.ToString();
        CountFinalBalance = CountBeginBalance + CountLead - CountOnloan;
        TextBox1.Text = CountBeginBalance.ToString("#,##0.00");
        TextBox2.Text = CountLead.ToString("#,##0.00");
        TextBox3.Text = CountOnloan.ToString("#,##0.00");
        TextBox4.Text = CountFinalBalance.ToString("#,##0.00");
    }
    protected void BackCarry_Click(object sender, EventArgs e)
    {
        //反月末结转
        string accountID = UserInfo.AccountID;
        StringBuilder SQLString = new StringBuilder();
        DateTime accountDate = MainClass.GetAccountDate();
        string PreYearMonth = string.Empty;
        string PrePreYearMonth = string.Empty;
        string PreMonthLastDay = string.Empty;
        string PrePreMonthLastDay = string.Empty;
        string LastCarryDate = MainClass.GetFieldFromID(accountID, "LastCarryDate", "cw_account");
        string StartAccountDate = MainClass.GetFieldFromID(accountID, "StartAccountDate", "cw_account");
        if (LastCarryDate == string.Format("{0}年12月31日", accountDate.Year.ToString()))
        {
            PreYearMonth = accountDate.Year.ToString() + "年12月";
            PrePreYearMonth = accountDate.Year.ToString() + "年11月";
            PreMonthLastDay = "31";
            PrePreMonthLastDay = "30";
            //删除12月余额备份
            SQLString.Append("delete from cw_lastmonthsum where yearmonth='" + PreYearMonth + "'#sql#");
        }
        else
        {
            DateTime PreDate = accountDate.AddMonths(-1);
            DateTime PrePreDate = accountDate.AddMonths(-2);
            PreYearMonth = PreDate.ToString("yyyy年MM月");
            PrePreYearMonth = PrePreDate.ToString("yyyy年MM月");
            PreMonthLastDay = DateTime.DaysInMonth(PreDate.Year, PreDate.Month).ToString("00");
            PrePreMonthLastDay = DateTime.DaysInMonth(PrePreDate.Year, PrePreDate.Month).ToString("00");
        }
        //更新最后一次结转日
        if (string.Compare(PrePreYearMonth, StartAccountDate.Substring(0, 8)) < 0)
        {
            PrePreMonthLastDay = "";
        }
        else
        {
            PrePreMonthLastDay = string.Concat(PrePreYearMonth, PrePreMonthLastDay, "日");
        }
        //恢复科目表期初余额
        DataTable subject = CommClass.GetDataTable("select id,beginbalance from cw_subject");
        DataTable sum = CommClass.GetDataTable("select subjectid,beginbalance from cw_subjectsum where yearmonth='" + PreYearMonth + "'");
        sum.PrimaryKey = new DataColumn[] { sum.Columns["subjectid"] };
        foreach (DataRow row in subject.Rows)
        {
            DataRow srow = sum.Rows.Find(row["id"]);
            if (srow == null)
            {
                row["beginbalance"] = 0;
            }
            else
            {
                row["beginbalance"] = srow["beginbalance"];
            }
        }
        CommClass.UpdateDataTable(subject, "id,beginbalance");
        //删除上月结转数据
        SQLString.Append("delete from cw_subjectsum where yearmonth='" + PreYearMonth + "'#sql#");
        //删除封存科目表
        SQLString.Append("delete from cw_subjectrec where yearmonth='" + PreYearMonth + "'#sql#");
        //删除凭证分录科目名称
        SQLString.Append("update cw_entry a join cw_voucher b on (a.VoucherID=b.id and b.VoucherDate>='" + PreYearMonth + "01日') set a.subjectname=null#sql#");
        //--
        CommClass.ExecuteTransaction(SQLString.ToString());
        //恢复账套结转信息
        string PreAccountDate = string.Concat(PreYearMonth, PreMonthLastDay, "日");
        MainClass.ExecuteSQL("update cw_account set AccountDate='" + PreAccountDate + "',LastCarryDate='" + PrePreMonthLastDay + "' where id='" + accountID + "'");
        string UADate = "parent.document.getElementById('ctl00_LeftFrame1_CurAccountDate').innerText='财务日期：" + PreAccountDate + "';";
        ExeScript.Text = "<script>" + UADate + "alert('恭喜您，反结转成功！');location.href='MonthCarryForward.aspx';</script>";
        //写入操作日志
        CommClass.WriteCTL_Log("100009", "反月末结转，财务日期：" + PreAccountDate);
    }
    protected void BackupDate_Click(object sender, EventArgs e)
    {
        if (MySQLClass.BackupAllTable(AccountDate.Text.Substring(0, 8) + "月末结转备份（手动）"))
        {
            ExeScript.Text = "<script language=javascript>alert('数据完全备份成功！');</script>";
        }
        else
        {
            ExeScript.Text = "<script language=javascript>alert('数据完全备份失败！');</script>";
        }
        DateTime _AccountDate = Convert.ToDateTime(AccountDate.Text);
        int MonthLastDay = DateTime.DaysInMonth(_AccountDate.Year, _AccountDate.Month);
        if (_AccountDate.Day == MonthLastDay)
        {
            AccountCarry.Enabled = true;
        }
        else
        {
            AccountCarry.Enabled = false;
        }
    }
}
