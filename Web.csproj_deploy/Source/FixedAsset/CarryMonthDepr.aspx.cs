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
using System.Text;

public partial class FixedAsset_CarryMonthDepr : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000013")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Overlay.Attributes["style"] = "display:none";
            Lightbox.Attributes["style"] = "display:none";
            string YearMonth = MainClass.GetAccountDate().ToString("yyyy年MM月");
            string RealLastCarry = MainClass.GetFieldFromID(UserInfo.AccountID, "RealLastCarry", "cw_account");
            if (string.Compare(RealLastCarry, YearMonth) >= 0)
            {
                Button1.Enabled = false;
                Button2.Enabled = false;
                Button3.Enabled = false;
            }
            Button1.Attributes.Add("onclick", "return _Confirm('本操作将计提本月折旧，您确定继续吗？','计提本月折旧')");
            Button2.Attributes.Add("onclick", "return _Confirm('本操作将对固定资产批量制单，您确定继续吗？','固定资产批量制单')");
            Button3.Attributes.Add("onclick", "return _Confirm('本操作将对固定资产批量反制单，您确定继续吗？','固定资产批量反制单')");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime AccountDate = MainClass.GetAccountDate();
        //计提累计折旧
        DataTable FixedAssets = CommClass.GetDataTable("select ID,UseLife,UsedMonths,NewPrice,JingCZ,MonthZJE,ThisZJ,DeprState,AssetState from CW_AssetCard"
            + " where DeprState='0' and AssetState<>'1' and UseState='101' and DeprMethod='1' and SUseDate not like '" + AccountDate.ToString("yyyy-MM") + "%'");
        foreach (DataRow row in FixedAssets.Rows)
        {
            decimal UsedMonths = decimal.Parse(row["UsedMonths"].ToString());
            if (row["ThisZJ"].ToString() == "0")
            {
                UsedMonths++;
            }
            decimal NewPrice = decimal.Parse(row["NewPrice"].ToString());
            decimal MonthZJE = decimal.Parse(row["MonthZJE"].ToString());
            decimal JingCZ = decimal.Parse(row["JingCZ"].ToString());
            string _UseLife = row["UseLife"].ToString();
            int dotpos = _UseLife.IndexOf(".");
            string LifeYear = _UseLife.Substring(0, dotpos);
            string LifeMonth = _UseLife.Substring(dotpos + 1);
            decimal UseLife = decimal.Parse(LifeYear) * 12 + decimal.Parse(LifeMonth);
            if (UseLife <= UsedMonths || NewPrice - MonthZJE <= JingCZ)
            {
                decimal ThisZJ = NewPrice - JingCZ;
                //使用年限已到或净值只剩净残值
                CommClass.ExecuteSQL("update CW_AssetCard set UsedMonths='" + UsedMonths.ToString()
                    + "',ZheJiu=OldPrice-JingCZ,ZheJiu0=OldPrice-JingCZ,NewPrice=JingCZ,ThisZJ=" + ThisZJ.ToString() + ",DeprState='1',AssetState='1'"
                    + " where id='" + row["id"].ToString() + "'");
            }
            else
            {
                //正常计提折旧
                CommClass.ExecuteSQL("update CW_AssetCard set UsedMonths='" + UsedMonths.ToString()
                    + "',ZheJiu=ZheJiu+MonthZJE,ZheJiu0=ZheJiu0+MonthZJE,NewPrice=NewPrice-MonthZJE,ThisZJ=MonthZJE,DeprState='1'"
                    + " where id='" + row["id"].ToString() + "'");
            }
        }
        ExeScript.Text = "<script>if(confirm('已计提完毕，是否查看计提清单？')){location.href='FixedAssetList.aspx';}</script>";
        //写入操作日志
        CommClass.WriteCTL_Log("100018", "计提折旧，计提年月：" + AccountDate.ToString("yyyy年MM月"));
        //--
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //批量制做凭证
        string VoucherID = "0000000000";
        List<string> IDList = new List<string>();
        DateTime AccountDate = MainClass.GetAccountDate();
        string ThisYearMonth = AccountDate.ToString("yyyy-MM");
        string VoucherDate = AccountDate.ToString("yyyy年MM月dd日");
        //计提折旧检测
        if (CommClass.CheckExist("CW_AssetCard", "DeprState='0' and AssetState<>'1' and UseState='101' and DeprMethod='1' and SUseDate not like '" + ThisYearMonth + "%'"))
        {
            ExeScript.Text = "<script language=javascript>alert('请先进行固定资产计提折旧！');</script>";
            return;
        }
        //获取需制单固定资产
        //DataTable NewAsset = CommClass.GetDataTable("select ID,ClassID,AddType,AssetName,OldPrice,CVoucher from CW_AssetCard where UseState='101' and DeprMethod='1' and CVoucher='0' and SUseDate like '" + ThisYearMonth + "%'");
        DataTable NewAsset = CommClass.GetDataTable("select ID,ClassID,AddType,AssetName,OldPrice,CVoucher from CW_AssetCard where UseState='101' and CVoucher='0' and (DeprMethod='0' or (SUseDate like '" + ThisYearMonth + "%'))");
        DataTable DeprAsset = CommClass.GetDataTable("select ID,ClassID,AddType,AssetName,OldPrice,OldPrice0,ZheJiu,ZheJiu0,ThisZJ,DeprSubject,CVoucher,BookDate from CW_AssetCard where UseState='101' and DeprMethod='1' and CVoucher='0' and DeprState='1' order by ClassID asc");
        //DataTable CleanAsset = CommClass.GetDataTable("select ID,ClassID,AddType,AssetName,OldPrice,ZheJiu,NewPrice,LSubject,CRMoney,CRSubject,CCMoney,CCSubject,CType,CVoucher from CW_AssetCard where UseState='101' and DeprMethod='1'  and CVoucher='0' and AssetState='2'");
        DataTable CleanAsset = CommClass.GetDataTable("select ID,ClassID,AddType,AssetName,OldPrice,ZheJiu,NewPrice,LSubject,CRMoney,CRSubject,CCMoney,CCSubject,CType,CVoucher from CW_AssetCard where UseState='101' and CVoucher='0' and AssetState='2'");
        //本月资产增加
        foreach (DataRow row in NewAsset.Rows)
        {
            IDList.Add(row["id"].ToString());
            //固定资产入账科目
            string LinkSubject0 = CommClass.GetFieldFromID(row["ClassID"].ToString(), "LinkSubject", "cw_assetclass");
            LinkSubject0 = SplitSNO(LinkSubject0);
            //固资对应入账科目
            string LinkSubject1 = CommClass.GetFieldFromID(row["AddType"].ToString(), "LinkSubject", "cw_ditype");
            LinkSubject1 = SplitSNO(LinkSubject1);
            //相关凭证摘要信息
            string VSummary = CommClass.GetFieldFromID(row["AddType"].ToString(), "TName", "cw_ditype");
            //创建资产增加凭证
            VoucherID = GetNewVoucher(VoucherDate, LinkSubject0, LinkSubject1, VSummary, row["OldPrice"].ToString());
            //固定资产原值入明细账
            CommClass.ExecuteSQL("insert into cw_assetda(id,cardid,classid,voucherid,voucherdate,vsummary,oldprice0,oldprice1,zhejiu0,zhejiu1,thiszhejiu)values('"
                + CommClass.GetRecordID("CW_AssetDA") + "','"
                + row["ID"].ToString() + "','"
                + row["ClassID"].ToString() + "','"
                + VoucherID + "','"
                + VoucherDate + "','["
                + VSummary + "]" + row["AssetName"].ToString() + "','0','"
                + row["OldPrice"].ToString() + "','0','0','0')");
        }
        //本月累计折旧
        int AssetVoucherECount = 0;
        decimal SumDeprValue = 0;
        string PrefixSubject = "-";
        string AccumulatedDeprSubject = SysConfigs.AccumulatedDeprSubject;
        int MergeVoucherSLevel = Convert.ToInt32(SysConfigs.MergeVoucherSLevel);
        foreach (DataRow row in DeprAsset.Rows)
        {
            IDList.Add(row["id"].ToString());
            string AssetClassSubject = row["ClassID"].ToString();
            string DeprSubject = SplitSNO(row["DeprSubject"].ToString());
            //本月折旧制单
            string ThisZJ = row["ThisZJ"].ToString();
            if (ThisZJ != "0")
            {
                if (AssetClassSubject.StartsWith(PrefixSubject) && AssetVoucherECount < 7)
                {
                    SumDeprValue += decimal.Parse(ThisZJ);
                }
                else
                {
                    if (SumDeprValue != 0)
                    {
                        SumDeprValue = 0 - SumDeprValue;
                        InsertVoucherEntry(VoucherID, AccumulatedDeprSubject, "累计折旧汇总", SumDeprValue.ToString());
                    }
                    PrefixSubject = AssetClassSubject.Length >= MergeVoucherSLevel ? AssetClassSubject.Substring(0, MergeVoucherSLevel) : AssetClassSubject;
                    VoucherID = GetVoucherID(VoucherDate);
                    SumDeprValue = decimal.Parse(ThisZJ);
                    AssetVoucherECount = 0;
                }
                AssetVoucherECount++;
                InsertVoucherEntry(VoucherID, DeprSubject, string.Format("[折旧]{0}", row["AssetName"].ToString()), ThisZJ);
            }
            //资产原值变更
            decimal CValue0 = 0;
            if (row["OldPrice"].ToString() != row["OldPrice0"].ToString())
            {
                //资产原值变更数
                CValue0 = decimal.Parse(row["OldPrice"].ToString()) - decimal.Parse(row["OldPrice0"].ToString());
                //固定资产入账科目
                string LinkSubject0 = CommClass.GetFieldFromID(AssetClassSubject, "LinkSubject", "cw_assetclass");
                LinkSubject0 = SplitSNO(LinkSubject0);
                //固资对应入账科目
                string LinkSubject1 = CommClass.GetFieldFromID(row["AddType"].ToString(), "NValueSubject", "cw_ditype");
                LinkSubject1 = SplitSNO(LinkSubject1);
                GetNewVoucher(VoucherDate, LinkSubject0, LinkSubject1, "固定资产原值变动", CValue0.ToString());
                CommClass.ExecuteSQL("update CW_AssetCard set OldPrice0=OldPrice where id='" + row["ID"].ToString() + "'");
            }
            //累计折旧变更
            decimal CValue1 = 0;
            if (row["ZheJiu"].ToString() != row["ZheJiu0"].ToString())
            {
                //累计折旧变更数
                CValue1 = decimal.Parse(row["ZheJiu"].ToString()) - decimal.Parse(row["ZheJiu0"].ToString());
                GetNewVoucher(VoucherDate, DeprSubject, AccumulatedDeprSubject, "累计折旧变动", CValue1.ToString());
                CommClass.ExecuteSQL("update CW_AssetCard set ZheJiu0=ZheJiu where id='" + row["ID"].ToString() + "'");
            }
            //累计折旧入明细账
            if (ThisZJ == "0")
            {
                CommClass.ExecuteSQL("insert into cw_assetda(id,cardid,classid,voucherid,voucherdate,vsummary,oldprice0,oldprice1,zhejiu0,zhejiu1,thiszhejiu)values('"
                    + CommClass.GetRecordID("CW_AssetDA") + "','"
                    + row["ID"].ToString() + "','"
                    + AssetClassSubject + "','0000000000','"
                    + VoucherDate + "','"
                    + row["AssetName"].ToString() + "','"
                    + CValue0.ToString() + "','"
                    + row["OldPrice"].ToString() + "','"
                    + CValue1.ToString() + "','"
                    + row["ZheJiu"].ToString() + "','"
                    + row["ThisZJ"].ToString() + "')");
            }
            else
            {
                CommClass.ExecuteSQL("insert into cw_assetda(id,cardid,classid,voucherid,voucherdate,vsummary,oldprice0,oldprice1,zhejiu0,zhejiu1,thiszhejiu)values('"
                    + CommClass.GetRecordID("CW_AssetDA") + "','"
                    + row["ID"].ToString() + "','"
                    + AssetClassSubject + "','"
                    + VoucherID + "','"
                    + VoucherDate + "','[折旧]"
                    + row["AssetName"].ToString() + "','"
                    + CValue0.ToString() + "','"
                    + row["OldPrice"].ToString() + "','"
                    + CValue1.ToString() + "','"
                    + row["ZheJiu"].ToString() + "','"
                    + row["ThisZJ"].ToString() + "')");
            }
            /////////////////////////////////////////////////////////////////////////////////////////////
        }
        if (SumDeprValue != 0)
        {
            SumDeprValue = 0 - SumDeprValue;
            InsertVoucherEntry(VoucherID, AccumulatedDeprSubject, "累计折旧汇总", SumDeprValue.ToString());
        }
        //本月资产减少
        foreach (DataRow row in CleanAsset.Rows)
        {
            IDList.Add(row["id"].ToString());
            decimal LostValue = decimal.Parse(row["NewPrice"].ToString());
            //资产入账科目
            string DeSubject = CommClass.GetFieldFromID(row["ClassID"].ToString(), "LinkSubject", "cw_assetclass");
            DeSubject = SplitSNO(DeSubject);
            //资产清理科目
            string CleanSubject = CommClass.GetFieldFromID(row["CType"].ToString(), "LinkSubject", "cw_ditype");
            CleanSubject = SplitSNO(CleanSubject);
            //清理损失科目
            string LostSubject = SplitSNO(row["LSubject"].ToString());
            //创建资产账面价值凭证
            GetNewVoucher(VoucherDate, CleanSubject, DeSubject, row);
            //创建清理费用凭证
            string CCMoney = row["CCMoney"].ToString();
            if (CCMoney != "0" && CCMoney.Length > 0)
            {
                GetNewVoucher(VoucherDate, CleanSubject, SplitSNO(row["CCSubject"].ToString()), "资产清理费用", CCMoney);
                LostValue += decimal.Parse(CCMoney);
            }
            //创建清理收入凭证
            string CRMoney = row["CRMoney"].ToString();
            if (CRMoney != "0" && CRMoney.Length > 0)
            {
                GetNewVoucher(VoucherDate, SplitSNO(row["CRSubject"].ToString()), CleanSubject, "资产清理收入", CRMoney);
                LostValue -= decimal.Parse(CRMoney);
            }
            //创建结转固定资产清理凭证
            if (LostValue != 0)
            {
                GetNewVoucher(VoucherDate, LostSubject, CleanSubject, "结转固定资产清理", LostValue.ToString());
            }
            //清除净值、净残值
            CommClass.ExecuteSQL(string.Format("update CW_AssetCard set NewPrice=0,JingCZ=0 where id='{0}'", row["id"].ToString()));
        }
        //更新资产制单状态
        foreach (string id in IDList)
        {
            CommClass.ExecuteSQL(string.Format("update CW_AssetCard set CVoucher='1' where id='{0}'", id));
        }
        ExeScript.Text = "<script>alert('已对所有卡片进行批量制单！\\n\\n提示：如果录入了原始卡片，请手动填制资产账面价值凭证。');location.href='FixedAssetList.aspx';</script>";
        //写入操作日志
        CommClass.WriteCTL_Log("100018", "资产卡片批量制单，制单凭证日期：" + VoucherDate);
        //--
    }

    private void InsertVoucherEntry(string VoucherID, string SubjectNo, string VSummary, string SumMoney)
    {
        if (SubjectNo.Length != 0)
        {
            CommClass.ExecuteSQL("insert cw_entry(id,voucherid,vsummary,subjectno,summoney)values('"
                + CommClass.GetRecordID("CW_Entry") + "','"
                + VoucherID + "','"
                + VSummary + "','"
                + SubjectNo + "','"
                + SumMoney + "')");
        }
    }

    private string GetNewVoucher(string VoucherDate, string LinkSubject0, string LinkSubject1, string VSummary, string SumMoney)
    {
        if (LinkSubject0.Length == 0 || LinkSubject1.Length == 0) { return "0000000000"; }
        string SumMoney0 = string.Empty;
        string SumMoney1 = string.Empty;
        if (SumMoney.StartsWith("-"))
        {
            SumMoney0 = SumMoney;
            SumMoney1 = SumMoney.Substring(1);
        }
        else
        {
            SumMoney0 = SumMoney;
            SumMoney1 = "-" + SumMoney;
        }
        string VoucherID = GetVoucherID(VoucherDate);
        CommClass.ExecuteSQL("insert cw_entry(id,voucherid,vsummary,subjectno,summoney)values('" + CommClass.GetRecordID("CW_Entry")
            + "','" + VoucherID + "','" + VSummary + "','" + LinkSubject0 + "','" + SumMoney0 + "')");
        CommClass.ExecuteSQL("insert cw_entry(id,voucherid,vsummary,subjectno,summoney)values('" + CommClass.GetRecordID("CW_Entry")
            + "','" + VoucherID + "','" + VSummary + "','" + LinkSubject1 + "','" + SumMoney1 + "')");
        return VoucherID;
    }

    private void GetNewVoucher(string VoucherDate, string LSubject, string ASubject, DataRow row)
    {
        if (LSubject.Length == 0 || ASubject.Length == 0) { return; }
        string VoucherID = GetVoucherID(VoucherDate);
        string NewPrice = row["NewPrice"].ToString();
        if (NewPrice != "0")
        {
            CommClass.ExecuteSQL("insert cw_entry(id,voucherid,vsummary,subjectno,summoney)values('" + CommClass.GetRecordID("CW_Entry")
                + "','" + VoucherID + "','结转固定资产账面价值','" + LSubject + "','" + NewPrice + "')");
        }
        CommClass.ExecuteSQL("insert cw_entry(id,voucherid,vsummary,subjectno,summoney)values('" + CommClass.GetRecordID("CW_Entry")
            + "','" + VoucherID + "','结转固定资产账面价值','" + SysConfigs.AccumulatedDeprSubject + "','" + row["ZheJiu"].ToString() + "')");
        CommClass.ExecuteSQL("insert cw_entry(id,voucherid,vsummary,subjectno,summoney)values('" + CommClass.GetRecordID("CW_Entry")
            + "','" + VoucherID + "','结转固定资产账面价值','" + ASubject + "','-" + row["OldPrice"].ToString() + "')");
        //固定资产清理入明细账
        CommClass.ExecuteSQL("insert into cw_assetda(id,cardid,classid,voucherid,voucherdate,vsummary,oldprice0,oldprice1,zhejiu0,zhejiu1,thiszhejiu)values('"
            + CommClass.GetRecordID("CW_AssetDA") + "','"
            + row["ID"].ToString() + "','"
            + row["ClassID"].ToString() + "','"
            + VoucherID + "','"
            + VoucherDate + "','"
            + "结转固定资产账面价值','-"
            + row["OldPrice"].ToString() + "','0','-"
            + row["ZheJiu"].ToString() + "','0','0')");
    }

    private string GetVoucherID(string VoucherDate)
    {
        string LastVoucherNo = CommClass.GetTableValue("cw_voucher", "voucherno", "voucherdate like '" + VoucherDate.Substring(0, 8) + "%' order by voucherno desc");
        if (LastVoucherNo == "NoDataItem")
        {
            LastVoucherNo = "100000";
        }
        int _LastVoucherNo = int.Parse(LastVoucherNo) + 1;
        DataSet Voucher = CommClass.GetDataSet("select * from cw_voucher where 1=2");
        DataRow NewDataRow = Voucher.Tables[0].NewRow();
        string VoucherID = CommClass.GetRecordID("CW_Voucher");
        NewDataRow["id"] = VoucherID;
        NewDataRow["voucherno"] = _LastVoucherNo.ToString();
        NewDataRow["voucherfrom"] = "FA";
        NewDataRow["voucherdate"] = VoucherDate;
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

    private string SplitSNO(string sno)
    {
        int dotpos = sno.IndexOf(".");
        if (dotpos == -1)
        {
            return "";
        }
        else
        {
            return sno.Substring(0, dotpos);
        }
    }

    /// <summary>
    /// 固定资产反制单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        string YearMonth = MainClass.GetAccountDate().ToString("yyyy年MM月");
        //删除总账凭证
        DataTable FA_Voucher = CommClass.GetDataTable("select id from cw_voucher where VoucherFrom='FA' and VoucherDate like '" + YearMonth + "%'");
        foreach (DataRow row in FA_Voucher.Rows)
        {
            CommClass.ExecuteSQL("delete from cw_entry where VoucherID='" + row["id"].ToString() + "'");
        }
        CommClass.ExecuteSQL("delete from cw_voucher where VoucherFrom='FA' and VoucherDate like '" + YearMonth + "%'");
        //调整资产状态
        DataTable FA_AssetDa = CommClass.GetDataTable("select cardid from cw_assetda where VoucherDate like '" + YearMonth + "%'");
        foreach (DataRow row in FA_AssetDa.Rows)
        {
            CommClass.ExecuteSQL("update cw_assetcard set CVoucher='0' where id='" + row["cardid"].ToString() + "'");
        }
        //删除固定资产明细账
        CommClass.ExecuteSQL("delete from cw_assetda where VoucherDate like '" + YearMonth + "%'");
        //--
        ExeScript.Text = "<script>alert('已成功执行批量反制单操作！');</script>";
    }
}
