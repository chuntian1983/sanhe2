using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// 通用外部调用类
/// CommOutCall,CreateNewVoucher,Dictionary
/// </summary>
public class CommOutCall
{
    public CommOutCall()
    {
        //--
    }

    public static string CreateNewVoucher(string vfalg, string vsummary, Dictionary<string, string> DebitSubject, Dictionary<string, string> CreditSubject)
    {
        return CreateNewVoucher(vfalg, vsummary, DebitSubject, CreditSubject, "", false, false);
    }
    public static string CreateNewVoucher(string vfalg, string vsummary, Dictionary<string, string> DebitSubject, Dictionary<string, string> CreditSubject, string addons)
    {
        return CreateNewVoucher(vfalg, vsummary, DebitSubject, CreditSubject, addons, false, false);
    }
    public static string CreateNewVoucher(string vfalg, string vsummary, Dictionary<string, string> DebitSubject, Dictionary<string, string> CreditSubject, string addons, bool isAudit, bool isRecord)
    {
        string VoucherDate = MainClass.GetAccountDate().ToString("yyyy年MM月dd日");
        if (VoucherDate.StartsWith("1900"))
        {
            throw new Exception("账套尚未启用！");
        }
        string VoucherID = GetVoucherID(vfalg, VoucherDate, addons, isAudit, isRecord);
        foreach (KeyValuePair<string, string> kv in DebitSubject)
        {
            CommClass.ExecuteSQL(string.Concat("insert cw_entry(id,voucherid,vsummary,subjectno,summoney)values('", CommClass.GetRecordID("CW_Entry"), "','", VoucherID, "','", vsummary, "','", kv.Key, "','", kv.Value, "')"));
        }
        foreach (KeyValuePair<string, string> kv in CreditSubject)
        {
            CommClass.ExecuteSQL(string.Concat("insert cw_entry(id,voucherid,vsummary,subjectno,summoney)values('", CommClass.GetRecordID("CW_Entry"), "','", VoucherID, "','", vsummary, "','", kv.Key, "','-", kv.Value, "')"));
        }
        return VoucherID;
    }

    private static string GetVoucherID(string vfalg, string VoucherDate, string addons, bool isAudit, bool isRecord)
    {
        //处理凭证附单
        int addonCount = 0;
        string fileID = string.Empty;
        if (addons.Length > 0)
        {
            string[] alist = addons.Split('$');
            for (int i = 0; i < alist.Length; i++)
            {
                if (alist[i].Length == 0)
                {
                    continue;
                }
                addonCount++;
                string AppendixID = CommClass.GetRecordID("Appendix");
                fileID = string.Concat(fileID, "Appendix", AppendixID, "$");
                string fileName = alist[i];
                string fileExtn = System.IO.Path.GetExtension(fileName);
                string fileThum = string.Empty;
                if (fileExtn.Length == 0)
                {
                    fileThum += "_thum";
                }
                else
                {
                    fileThum = fileName.Replace(fileExtn, "_thum" + fileExtn);
                }
                UtilsComm.MakeThumbnail(HttpContext.Current.Server.MapPath(fileName), HttpContext.Current.Server.MapPath(fileThum), 90, 90);
                CommClass.ExecuteSQL(string.Concat("insert into cw_syspara(ParaName,ParaValue,DefValue,ParaType,DefPara1)values('Appendix",
                    AppendixID, "','", fileName, "','", fileThum, "','1','", VoucherDate.Replace("年", "-").Replace("月", "-").Replace("日", ""), "')"));
            }
        }
        //创建凭证
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
        NewDataRow["voucherfrom"] = vfalg;
        NewDataRow["voucherdate"] = VoucherDate;
        NewDataRow["IsAuditing"] = isAudit ? "1" : "0";
        NewDataRow["IsRecord"] = isRecord ? "1" : "0";
        NewDataRow["Director"] = MainClass.GetFieldFromID(UserInfo.AccountID, "director", "cw_account");
        NewDataRow["DoBill"] = HttpContext.Current.Session["RealName"].ToString();
        NewDataRow["Assessor"] = HttpContext.Current.Session["RealName"].ToString();
        NewDataRow["Accountant"] = HttpContext.Current.Session["RealName"].ToString();
        NewDataRow["Addons"] = fileID;
        NewDataRow["AddonsCount"] = addonCount.ToString();
        NewDataRow["IsHasAlarm"] = "0";
        NewDataRow["DelFlag"] = "0";
        Voucher.Tables[0].Rows.Add(NewDataRow);
        CommClass.UpdateDataSet(Voucher);
        return VoucherID;
    }
}
