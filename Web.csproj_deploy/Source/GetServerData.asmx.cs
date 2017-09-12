using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Text;

namespace SanZi.Web
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class GetDataFromServer : System.Web.Services.WebService
    {
        public bool VerfiyCheckCode(string checkcode)
        {
            string mycheck = PageClass.Hash_SHA1("山东农友软件公司06315626026陶伟1103");
            return (mycheck == checkcode);
        }
        
        [WebMethod(EnableSession = true)]
        public DataTable GetDataFromAccounts(string checkcode, string towns, string columns, string codition)
        {
            if (VerfiyCheckCode(checkcode) == false)
            {
                return null;
            }
            if (columns.Length == 0)
            {
                return null;
            }
            HttpContext.Current.Session["SessionFlag"] = "SessionFlag";
            if (codition.Length > 0)
            {
                codition = " where " + codition;
            }
            string[] townarr = towns.Split(',');
            List<string> towslist = new List<string>();
            foreach (string town in townarr)
            {
                string townid = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[UnitName='" + town + "']", "ID");
                if (townid.Length > 0)
                {
                    towslist.Add(string.Concat("'", townid, "'"));
                }
            }
            if (towslist.Count > 0)
            {
                if (codition.Length > 0)
                {
                    codition += " and (unitid in (" + string.Join(",", towslist.ToArray()) + "))";
                }
                else
                {
                    codition = " where unitid in (" + string.Join(",", towslist.ToArray()) + ")";
                }
            }
            else
            {
                codition = " where 1=2 ";
            }
            DataTable accounts = MainClass.GetDataTable(string.Concat("select ", columns, ",unitid from cw_account ", codition));
            accounts.Columns.Add("townname");
            for (int i = 0; i < accounts.Rows.Count; i++)
            {
                string townname = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + accounts.Rows[i]["unitid"].ToString() + "']", "UnitName");
                accounts.Rows[i]["townname"] = townname;
            }
            return accounts;
        }

        [WebMethod(EnableSession = true)]
        public DataTable GetDataFromUsers(string checkcode, string columns, string codition)
        {
            if (VerfiyCheckCode(checkcode) == false)
            {
                return null;
            }
            if (columns.Length == 0)
            {
                return null;
            }
            HttpContext.Current.Session["SessionFlag"] = "SessionFlag";
            if (codition.Length > 0)
            {
                codition = " where " + codition;
            }
            DataTable users = MainClass.GetDataTable(string.Concat("select ", columns, ",unitid from cw_users ", codition));
            users.Columns.Add("townname");
            for (int i = 0; i < users.Rows.Count; i++)
            {
                string townname = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + users.Rows[i]["unitid"].ToString() + "']", "UnitName");
                users.Rows[i]["townname"] = townname;
            }
            return users;
        }

        [WebMethod(EnableSession = true)]
        public DataTable GetSubjectSum(string checkcode, string unitname, string YearMonth)
        {
            if (VerfiyCheckCode(checkcode) == false || unitname.Length == 0 || YearMonth.Length == 0)
            {
                return null;
            }
            HttpContext.Current.Session["SessionFlag"] = "SessionFlag";
            StringBuilder sb = new StringBuilder();
            GetCollectAccount(ref sb, unitname);
            string idlist = sb.ToString();
            string[] accounts = idlist.Split('$');
            DataTable data = new DataTable("data");
            string codition = string.Empty;
            if (YearMonth.Length > 0)
            {
                codition = "length(SubjectNo)<=5 and YearMonth='" + YearMonth + "'";
            }
            else
            {
                codition = "length(SubjectNo)<=5";
            }
            string sql = string.Concat("select SubjectNo,YearMonth,BeginBalance,Lead,Onloan,LeadSum,OnloanSum,FinalBalance from cw_viewsubjectsum where ", codition);
            foreach (string account in accounts)
            {
                if (account.Length == 0)
                {
                    continue;
                }
                HttpContext.Current.Session["UnitID"] = "000000";
                UserInfo.AccountID = account;
                DataTable sdata = CommClass.GetDataTable(sql);
                if (data.Columns.Count == 0)
                {
                    data = sdata;
                }
                else
                {
                    foreach (DataRow drow in sdata.Rows)
                    {
                        DataRow[] nrows = data.Select("SubjectNo='" + drow["SubjectNo"].ToString() + "'");
                        if (nrows.Length == 0)
                        {
                            data.Rows.Add(drow.ItemArray);
                        }
                        else
                        {
                            for (int i = 2; i < 8; i++)
                            {
                                decimal v = TypeParse.StrToDecimal(nrows[0][i].ToString(), 0) + TypeParse.StrToDecimal(drow[i].ToString(), 0);
                                nrows[0][i] = v;
                            }
                        }
                    }
                }
            }
            return data;
        }

        [WebMethod(EnableSession = true)]
        public string GetExprValue(string checkcode, string unitname, string YearMonth, string Expr)
        {
            if (VerfiyCheckCode(checkcode) == false || unitname.Length == 0 || YearMonth.Length == 0 || Expr.Length == 0)
            {
                return "0";
            }
            HttpContext.Current.Session["SessionFlag"] = "SessionFlag";
            StringBuilder sb = new StringBuilder();
            GetCollectAccount(ref sb, unitname);
            ClsCalculate clsCalculate = new ClsCalculate();
            clsCalculate.DesignID = "000000";
            clsCalculate.AccountList = sb.ToString();
            clsCalculate.ReportDate = YearMonth;
            return clsCalculate.GetExprValue(Expr).ToString();
        }

        [WebMethod(EnableSession = true)]
        public string GetFixedAssetPrice(string checkcode, string unitname, string codition)
        {
            if (VerfiyCheckCode(checkcode) == false || unitname.Length == 0 || codition.Length == 0)
            {
                return "0";
            }
            HttpContext.Current.Session["SessionFlag"] = "SessionFlag";
            StringBuilder sb = new StringBuilder();
            GetCollectAccount(ref sb, unitname);
            string idlist = sb.ToString();
            string[] accounts = idlist.Split('$');
            decimal sum = 0;
            foreach (string account in accounts)
            {
                if (account.Length == 0)
                {
                    continue;
                }
                HttpContext.Current.Session["UnitID"] = "000000";
                UserInfo.AccountID = account;
                string sdata = CommClass.GetTableValue("cw_assetcard", "sum(oldprice)", codition, "0");
                sum += TypeParse.StrToDecimal(sdata, 0);
            }
            return sum.ToString();
        }

        [WebMethod(EnableSession = true)]
        public string GetResourceAmount(string checkcode, string unitname, string codition)
        {
            if (VerfiyCheckCode(checkcode) == false || unitname.Length == 0 || codition.Length == 0)
            {
                return "0";
            }
            HttpContext.Current.Session["SessionFlag"] = "SessionFlag";
            StringBuilder sb = new StringBuilder();
            GetCollectAccount(ref sb, unitname);
            string idlist = sb.ToString();
            string[] accounts = idlist.Split('$');
            decimal sum = 0;
            foreach (string account in accounts)
            {
                if (account.Length == 0)
                {
                    continue;
                }
                HttpContext.Current.Session["UnitID"] = "000000";
                UserInfo.AccountID = account;
                string sdata = CommClass.GetTableValue("cw_rescard", "sum(ResAmount)", codition, "0");
                sum += TypeParse.StrToDecimal(sdata, 0);
            }
            return sum.ToString();
        }

        public void GetCollectAccount(ref StringBuilder AllAccount, string unitname)
        {
            if (unitname.Contains(","))
            {
                string[] arr = unitname.Split(',');
                foreach (string uname in arr)
                {
                    string unitid = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[UnitName='" + uname + "']/ID");
                    if (unitid.Length > 0)
                    {
                        GetCollectAccountFromID(ref AllAccount, unitid);
                    }
                }
            }
            else
            {
                string unitid = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[UnitName='" + unitname + "']/ID");
                if (unitid.Length > 0)
                {
                    GetCollectAccountFromID(ref AllAccount, unitid);
                }
            }
        }

        public void GetCollectAccountFromID(ref StringBuilder AllAccount, string unitid)
        {
            DataSet ds = MainClass.GetDataSet("select id from cw_account where unitid='" + unitid + "'");
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                AllAccount.Append(row["id"].ToString() + "$");
            }
            DataRow[] rows = ValidateClass.GetRegRows("CUnits", "parentid='" + unitid + "'");
            if (rows != null)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    GetCollectAccountFromID(ref AllAccount, rows[i]["id"].ToString());
                }
            }
        }
    }
}
