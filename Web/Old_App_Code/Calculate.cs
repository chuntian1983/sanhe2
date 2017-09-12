using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography; 

/// <summary>
/// 账务计算类
/// </summary>

    public class ClsCalculate
    {
        public GridView GV;
        public string DesignID;
        public string AccountID;
        public string ReportDate;
        public string AccountList;
        public string StartYearMonth;
        public string LastYearMonth;
        public DataTable subdataTable;
        public DataTable sumDataTable;
        public List<string> sumYearMonth = new List<string>();

        public ClsCalculate()
        {
            //构造函数
        }

        /// 函数名称：CalculateExpr
        /// 函数作者：朱坤堂
        /// 创建时间：2010-01-27
        /// <summary>
        /// 报表公式计算
        /// </summary>
        /// <param name="TC"></param>
        /// 下步优化：字典缓存TC.Text
        public void CalculateExpr(TableCell TC)
        {
            if (Regex.IsMatch(TC.Text.Replace(",", ""), "-?\\d+\\.\\d{0,2}"))
            {
                return;
            }
            //封存单元格公式
            if (TC.Text == "&nbsp;")
            {
                if (TC.Attributes["ItemExpr"] == null)
                {
                    TC.Attributes["ItemExpr"] = "";
                }
                return;
            }
            else
            {
                TC.Attributes["ItemExpr"] = TC.Text;
            }
            //计算报表单元格公式
            if (TC.Text.IndexOf("本表行列") == -1)
            {
                //纯科目运算公式
                TC.Text = Convert.ToDecimal(GetExprValue(TC.Text)).ToString("#,##0.00");
                if (TC.Text == "0.00") { TC.Text = "&nbsp;"; }
            }
            else
            {
                //科目运算以及汇总项
                string[] ExprItem = TC.Text.Split(new char[] { '+', '-', '*', '/' });
                foreach (string Item in ExprItem)
                {
                    if (Item.IndexOf(":") == -1 || Item.Length == 0) { continue; }
                    if (Item.IndexOf("本表行列") == -1)
                    {
                        string[] ex = Item.Split(':');
                        string SubjectNo = Regex.Replace(ex[0], @"(.*\[)|\]", "");
                        TC.Text = TC.Text.Replace(Item, "(" + GetExprItemValue(SubjectNo, ex[1]) + ")");
                    }
                    else
                    {
                        int NoPos = 1;
                        bool ExistFlag = false;
                        string RowNo = Item.Substring(5, 2);
                        int CPos = int.Parse(Item.Substring(8, 2));
                        switch (CPos)
                        {
                            case 3:
                                NoPos = 1;
                                break;
                            case 7:
                                NoPos = 5;
                                break;
                            default:
                                NoPos = CPos - 1;
                                break;
                        }
                        for (int v = 0; v < GV.Rows.Count; v++)
                        {
                            if (GV.Rows[v].Cells[NoPos].Text == RowNo)
                            {
                                string text = GV.Rows[v].Cells[CPos].Text.Replace(",", "");
                                if (text == "&nbsp;" || text.Length == 0) { break; }
                                if (Regex.IsMatch(text, "-?\\d+\\.\\d{0,2}"))
                                {
                                    ExistFlag = true;
                                    TC.Text = TC.Text.Replace(Item, "(" + text + ")");
                                }
                                else
                                {
                                    CalculateExpr(GV.Rows[v].Cells[CPos]);
                                    text = GV.Rows[v].Cells[CPos].Text.Replace(",", "");
                                    if (Regex.IsMatch(text, "-?\\d+\\.\\d{0,2}"))
                                    {
                                        ExistFlag = true;
                                        TC.Text = TC.Text.Replace(Item, "(" + text + ")");
                                    }
                                }
                                break;
                            }
                        }
                        if (!ExistFlag) { TC.Text = TC.Text.Replace(Item, "0"); }
                    }
                }
                decimal ExprValue = Convert.ToDecimal(JSEval(TC.Text));
                TC.Text = ExprValue == 0 ? "&nbsp;" : ExprValue.ToString("#,##0.00");
            }
        }

        /// 函数名称：GetExprValue
        /// 函数作者：朱坤堂
        /// 创建时间：2010-01-27
        /// <summary>
        /// 公式计算
        /// </summary>
        /// <param name="Expr"></param>
        /// <returns></returns>
        public object @GetExprValue(string Expr)
        {
            if (Expr.Length == 0 && Expr.IndexOf("本表行列") != -1)
            {
                return (object)"0";
            }
            string[] ExprItem = Expr.Split(new char[] { '+', '-', '*', '/' });
            try
            {
                foreach (string ei in ExprItem)
                {
                    string[] ex = ei.Split(':');
                    string SubjectNo = Regex.Replace(ex[0], @"(.*\[)|\]", "");
                    Expr = Expr.Replace(ei, "(" + GetExprItemValue(SubjectNo, ex[1]) + ")");
                }
                return JSEval(Expr);
            }
            catch
            {
                return (object)"0";
            }
        }

        /// 函数名称：GetExprItemValue
        /// 函数作者：朱坤堂
        /// 创建时间：2010-01-27
        /// <summary>
        /// 公式项计算
        /// </summary>
        /// <param name="SubjectNo"></param>
        /// <param name="SumType"></param>
        /// <returns></returns>
        public string GetExprItemValue(string SubjectNo, string SumType)
        {
            string ExprValue = "0";
            if (SubjectNo.Length == 0 || ReportDate.Length < 8)
            {
                return ExprValue;
            }
            if (ReportDate.Length > 8)
            {
                ReportDate = ReportDate.Substring(0, 8);
            }
            string reportDate = string.Empty;
            int year = int.Parse(ReportDate.Substring(0, 4));
            bool isDebit = (SumType.IndexOf("贷方分析余额") == -1);
            switch (SumType)
            {
                case "借方金额余额":
                    ExprValue = GetExprItemSubjectSum(SubjectNo, "finalbalance", ReportDate);
                    break;
                case "贷方金额余额":
                    ExprValue = "-(" + GetExprItemSubjectSum(SubjectNo, "finalbalance", ReportDate) + ")";
                    break;
                case "本月借方金额":
                    ExprValue = GetExprItemSubjectSum(SubjectNo, "lead", ReportDate);
                    break;
                case "本月贷方金额":
                    ExprValue = GetExprItemSubjectSum(SubjectNo, "onloan", ReportDate);
                    break;
                case "月初借方余额":
                    ExprValue = GetExprItemSubjectSum(SubjectNo, "beginbalance", ReportDate);
                    break;
                case "月初贷方余额":
                    ExprValue = "-(" + GetExprItemSubjectSum(SubjectNo, "beginbalance", ReportDate) + ")";
                    break;
                case "年初借方余额":
                    ExprValue = GetExprItemSubjectSum(SubjectNo, "beginbalance", year.ToString() + "年01月");
                    break;
                case "年初贷方余额":
                    ExprValue = "-(" + GetExprItemSubjectSum(SubjectNo, "beginbalance", year.ToString() + "年01月") + ")";
                    break;
                case "本年借方累计":
                    ExprValue = GetExprItemSubjectSum(SubjectNo, "leadsum", ReportDate);
                    break;
                case "本年贷方累计":
                    ExprValue = GetExprItemSubjectSum(SubjectNo, "onloansum", ReportDate);
                    break;
                case "本年年初余额":
                    ExprValue = GetExprItemSubjectSum(SubjectNo, "beginbalance", year.ToString() + "年01月");
                    break;
                case "月初借方分析余额":
                case "月初贷方分析余额":
                case "年初借方分析余额":
                case "年初贷方分析余额":
                    if (SumType.StartsWith("年初"))
                    {
                        reportDate = year.ToString() + "年01月";
                    }
                    else
                    {
                        reportDate = ReportDate;
                    }
                    string balance = GetExprItemSubjectSum(SubjectNo, "beginbalance", reportDate);
                    if (isDebit)
                    {
                        ExprValue = balance.StartsWith("-") ? "0" : balance;
                    }
                    else
                    {
                        ExprValue = balance.StartsWith("-") ? balance.Substring(1) : "0";
                    }
                    break;
                case "年初明细借方分析余额":
                case "年初明细贷方分析余额":
                case "明细科目借方分析余额":
                case "明细科目贷方分析余额":
                    string Field = string.Empty;
                    if (SumType.StartsWith("年初"))
                    {
                        Field = "beginbalance";
                        reportDate = year.ToString() + "年01月";
                    }
                    else
                    {
                        Field = "finalbalance";
                        reportDate = ReportDate;
                    }
                    decimal evalue = 0;
                    DataTable dsubject = CommClass.GetDataTable(string.Concat("select subjectno from cw_subject where isdetail='1' and subjectno like '", SubjectNo, "%'"));
                    foreach (DataRow drow in dsubject.Rows)
                    {
                        string balanceD = GetExprItemSubjectSum(drow["subjectno"].ToString(), Field, reportDate);
                        if (isDebit)
                        {
                            if (balanceD.StartsWith("-") == false)
                            {
                                evalue += TypeParse.StrToDecimal(balanceD, 0);
                            }
                        }
                        else
                        {
                            if (balanceD.StartsWith("-"))
                            {
                                evalue -= TypeParse.StrToDecimal(balanceD, 0);
                            }
                        }
                    }
                    ExprValue = evalue.ToString();
                    break;
            }
            return ExprValue.IndexOf("NoDataItem") >= 0 || ExprValue == "-0" ? "0" : ExprValue;
        }

        /// 函数名称：GetExprItemSubjectSum
        /// 函数作者：朱坤堂
        /// 创建时间：2010-01-27
        /// <summary>
        /// 获取科目余额
        /// </summary>
        /// <param name="SubjectNo"></param>
        /// <param name="Field"></param>
        /// <param name="YearMonth"></param>
        /// <returns></returns>
        public string GetExprItemSubjectSum(string SubjectNo, string Field, string YearMonth)
        {
            if (AccountList == null)
            {
                return GetExprItemMonthSum(SubjectNo, Field, YearMonth);
            }
            else
            {
                decimal sum = 0;
                string[] accountList = AccountList.Split('$');
                for (int i = 0; i < accountList.Length; i++)
                {
                    if (accountList[i].Length > 0)
                    {
                        UserInfo.AccountID = accountList[i];
                        sum += decimal.Parse(GetExprItemMonthSum(SubjectNo, Field, YearMonth));
                    }
                }
                return sum.ToString();
            }
        }

        /// 函数名称：GetExprItemMonthSum
        /// 函数作者：朱坤堂
        /// 创建时间：2010-01-27
        /// <summary>
        /// 获取月余额
        /// </summary>
        /// <param name="SubjectNo"></param>
        /// <param name="Field"></param>
        /// <param name="YearMonth"></param>
        /// <returns></returns>
        public string GetExprItemMonthSum(string SubjectNo, string Field, string YearMonth)
        {
            string sum = string.Empty;
            if ("000004,000000".IndexOf(DesignID) == -1 && YearMonth.EndsWith("12月"))
            {
                sum = CommClass.GetTableValue("cw_lastmonthsum", Field, string.Format("subjectno='{0}' and yearmonth='{1}'", SubjectNo, YearMonth));
            }
            else
            {
                if (AccountList == null)
                {
                    sum = GetSubjectSumFromDataTable(SubjectNo, Field, YearMonth).ToString();
                }
                else
                {
                    sum = GetSubjectSum(SubjectNo, Field, YearMonth);
                }
            }
            return sum == "NoDataItem" ? "0" : sum;
        }

        /// 函数名称：GetSubjectSumFromDataTable
        /// 函数作者：朱坤堂
        /// 创建时间：2010-01-27
        /// <summary>
        /// 获取科目余额（缓存）
        /// </summary>
        /// <param name="SubjectNo"></param>
        /// <param name="Field"></param>
        /// <param name="YearMonth"></param>
        /// <returns></returns>
        public decimal @GetSubjectSumFromDataTable(string SubjectNo, string Field, string YearMonth)
        {
            return GetSubjectSumFromDataTable(SubjectNo, Field, YearMonth, false);
        }
        public decimal @GetSubjectSumFromDataTable(string SubjectNo, string Field, string YearMonth, bool isReturn)
        {
            FillSumDataTable(YearMonth);
            DataRow[] data = sumDataTable.Select(string.Format("subjectno='{0}' and yearmonth='{1}'", SubjectNo, YearMonth));
            if (data.Length > 0)
            {
                return decimal.Parse(data[0][Field].ToString());
            }
            else
            {
                if (isReturn) { return 0; }
                if (Field.ToLower() == "beginbalance")
                {
                    if (LastYearMonth.Length == 0)
                    {
                        return GetSubjectSumFromDataTable(SubjectNo, "beginbalance", "0000年00月", true);
                    }
                    else
                    {
                        if (string.Compare(YearMonth, LastYearMonth) > 0)
                        {
                            return GetSubjectSumFromDataTable(SubjectNo, "finalbalance", LastYearMonth, true);
                        }
                        else
                        {
                            if (string.Compare(YearMonth, StartYearMonth) > 0)
                            {
                                return GetSubjectSumFromDataTable(SubjectNo, "beginbalance", YearMonth, true);
                            }
                            else
                            {
                                return GetSubjectSumFromDataTable(SubjectNo, "beginbalance", StartYearMonth, true);
                            }
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        /// 函数名称：FillSumDataTable
        /// 函数作者：朱坤堂
        /// 创建时间：2010-01-27
        /// <summary>
        /// 抽取余额填充内存
        /// </summary>
        /// <param name="YearMonth"></param>
        public void FillSumDataTable(string YearMonth)
        {
            if (AccountID != UserInfo.AccountID)
            {
                AccountID = UserInfo.AccountID;
                sumYearMonth.Clear();
                SetInit(true);
            }
            if (sumYearMonth.Contains(YearMonth) == false)
            {
                sumYearMonth.Add(YearMonth);
                DataTable dt = CommClass.GetDataTable(string.Format("select SubjectNo,BeginBalance,Lead,Onloan,LeadSum,OnloanSum,FinalBalance,YearMonth from cw_viewsubjectsum where YearMonth='{0}'", YearMonth));
                sumDataTable.Merge(dt, true, MissingSchemaAction.Ignore);
            }
        }

        /// 函数名称：SetInit
        /// 函数作者：朱坤堂
        /// 创建时间：2010-01-27
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="isLoadData"></param>
        public void SetInit(bool isLoadData)
        {
            StartYearMonth = MainClass.GetFieldFromID(UserInfo.AccountID, "StartAccountDate", "cw_account").Substring(0, 8);
            LastYearMonth = MainClass.GetFieldFromID(UserInfo.AccountID, "LastCarryDate", "cw_account");
            if (LastYearMonth.Length >= 8)
            {
                LastYearMonth = LastYearMonth.Substring(0, 8);
            }
            if (isLoadData)
            {
                subdataTable = CommClass.GetDataTable("select SubjectNo,BeginBalance from cw_subject");
                sumDataTable = CommClass.GetDataTable("select SubjectNo,BeginBalance,Lead,Onloan,LeadSum,OnloanSum,FinalBalance,YearMonth from cw_viewsubjectsum where 1=2");
                foreach (DataRow row in subdataTable.Rows)
                {
                    sumDataTable.Rows.Add(new object[] { row["subjectno"], row["BeginBalance"], 0, 0, 0, 0, 0, "0000年00月" });
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //静态函数区
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// 函数名称：GetSubjectSum
        /// 函数作者：朱坤堂
        /// 创建时间：2010-01-27
        /// <summary>
        /// 获取科目余额
        /// </summary>
        /// <param name="SubjectNo"></param>
        /// <param name="Field"></param>
        /// <param name="YearMonth"></param>
        /// <returns></returns>
        public static string GetSubjectSum(string SubjectNo, string Field, string YearMonth)
        {
            string sum = CommClass.GetTableValue("cw_viewsubjectsum", Field, string.Format("subjectno='{0}' and yearmonth='{1}'", SubjectNo, YearMonth));
            if (sum == "NoDataItem" && Field.ToLower() == "beginbalance")
            {
                string LastCarryDate = MainClass.GetFieldFromID(UserInfo.AccountID, "LastCarryDate", "cw_account");
                if (LastCarryDate.Length == 0)
                {
                    sum = CommClass.GetTableValue("cw_subject", "beginbalance", string.Format("subjectno='{0}'", SubjectNo));
                }
                else
                {
                    string LastYearMonth = LastCarryDate.Substring(0, 8);
                    if (string.Compare(YearMonth, LastYearMonth) > 0)
                    {
                        sum = CommClass.GetTableValue("cw_viewsubjectsum", "finalbalance", string.Format("subjectno='{0}' and yearmonth='{1}'", SubjectNo, LastYearMonth));
                    }
                    else
                    {
                        string StartAccountDate = MainClass.GetFieldFromID(UserInfo.AccountID, "StartAccountDate", "cw_account");
                        YearMonth = StartAccountDate.Substring(0, 8);
                        sum = CommClass.GetTableValue("cw_viewsubjectsum", Field, string.Format("subjectno='{0}' and yearmonth='{1}'", SubjectNo, YearMonth));
                    }
                }
            }
            return sum == "NoDataItem" ? "0" : sum;
        }

        public static decimal GetSubjectSumDecimal(string SubjectNo, string Field, string YearMonth)
        {
            return decimal.Parse(GetSubjectSum(SubjectNo, Field, YearMonth));
        }

        public static string GetOnlySubjectSum(string SubjectNo, string Field, string YearMonth)
        {
            string sum = CommClass.GetTableValue("cw_viewsubjectsum", Field, string.Format("subjectno='{0}' and yearmonth='{1}'", SubjectNo, YearMonth));
            return sum == "NoDataItem" ? "0" : sum;
        }

        /// <summary>
        /// 检验主Dll_Hash签名验证
        /// </summary>
        public static void VerifyHashValue()
        {
            //[$DeleteRowFromHere$]
            return;
            //[$DeleteRowFromEnd$]
            try
            {
                string p = string.Concat(new string[] { SysConfigs.baseDirectory, "\\bi", "n\\NY", "Fin", "an", "ceC", "lass.d", "ll" });
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] b = File.ReadAllBytes(p);
                byte[] h = md5.ComputeHash(b);
                string dp = string.Concat(new string[] { SysConfigs.baseDirectory, "Im", "ag", "es\\H", "ashS", "ign.i", "ni" });
                string dh = Convert.ToBase64String(h);
                string ds = IniFileProvider.ReadIniValue("h_a_sh".Replace("_", ""), "d_l_lha_shs_ign".Replace("_", ""), dp).Trim();
                string dd = IniFileProvider.ReadIniValue("h_a_sh".Replace("_", ""), "d_l_lha_shd_ata".Replace("_", ""), dp).Trim();
                ASCIIEncoding ByteConverter = new ASCIIEncoding();
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAalg.ImportCspBlob(Convert.FromBase64String(dd));
                byte[] sd = Convert.FromBase64String(ds);
                byte[] dv = ByteConverter.GetBytes(dh);
                if (dd.IndexOf("DxoBpdCqs4Z0db8GiC04RUr674E9m0V5") == -1 || RSAalg.VerifyData(dv, new SHA1CryptoServiceProvider(), sd) == false)
                {
                    UserInfo.AccountID = null;
                }
            }
            catch { }
        }

        /// 函数名称：GetMarginSubjectSum
        /// 函数作者：朱坤堂
        /// 创建时间：2010-01-27
        /// <summary>
        /// 获取期间余额
        /// </summary>
        /// <param name="SubjectNo"></param>
        /// <param name="sYearMonth"></param>
        /// <param name="eYearMonth"></param>
        /// <param name="isOnloan"></param>
        /// <returns></returns>
        public static decimal GetMarginSubjectSum(string SubjectNo, string sYearMonth, string eYearMonth, bool isOnloan)
        {
            decimal sum = 0;
            string StartAccountDate = MainClass.GetFieldFromID(UserInfo.AccountID, "StartAccountDate", "cw_account");
            string LastCarryDate = MainClass.GetFieldFromID(UserInfo.AccountID, "LastCarryDate", "cw_account");
            string StartYearMonth = StartAccountDate.Length >= 8 ? StartAccountDate.Substring(0, 8) : "2099年12月";
            string LastYearMonth = LastCarryDate.Length >= 8 ? LastCarryDate.Substring(0, 8) : "1900年01月";
            if (string.Compare(sYearMonth, LastYearMonth) > 0 || string.Compare(eYearMonth, StartYearMonth) < 0)
            {
                return 0;
            }
            else
            {
                if (string.Compare(sYearMonth, StartYearMonth) >= 0)
                {
                    sum = decimal.Parse(GetSubjectSum(SubjectNo, "beginbalance", sYearMonth));
                }
                if (string.Compare(eYearMonth, LastYearMonth) <= 0)
                {
                    sum = decimal.Parse(GetSubjectSum(SubjectNo, "finalbalance", eYearMonth)) - sum;
                }
                else
                {
                    sum = decimal.Parse(GetSubjectSum(SubjectNo, "finalbalance", LastYearMonth)) - sum;
                }
            }
            if (isOnloan) { sum = 0 - sum; }
            return sum;
        }

        /// 函数名称：GetMarginSum
        /// 函数作者：朱坤堂
        /// 创建时间：2010-01-27
        /// <summary>
        /// 获取期间余额，不含年末结转
        /// </summary>
        /// <param name="SubjectNo"></param>
        /// <param name="sYearMonth"></param>
        /// <param name="eYearMonth"></param>
        /// <param name="isOnloan"></param>
        /// <returns></returns>
        public static decimal GetMarginSum(string SubjectNo, string sYearMonth, string eYearMonth, bool isOnloan)
        {
            decimal sum = 0;
            string StartAccountDate = MainClass.GetFieldFromID(UserInfo.AccountID, "StartAccountDate", "cw_account");
            string LastCarryDate = MainClass.GetFieldFromID(UserInfo.AccountID, "LastCarryDate", "cw_account");
            string StartYearMonth = StartAccountDate.Length >= 8 ? StartAccountDate.Substring(0, 8) : "2099年12月";
            string LastYearMonth = LastCarryDate.Length >= 8 ? LastCarryDate.Substring(0, 8) : "1900年01月";
            if (string.Compare(sYearMonth, LastYearMonth) > 0 || string.Compare(eYearMonth, StartYearMonth) < 0)
            {
                return 0;
            }
            else
            {
                if (string.Compare(sYearMonth, StartYearMonth) >= 0)
                {
                    sum = GetSubjectSumMargin(SubjectNo, "beginbalance", sYearMonth);
                }
                if (string.Compare(eYearMonth, LastYearMonth) <= 0)
                {
                    sum = GetSubjectSumMargin(SubjectNo, "finalbalance", eYearMonth) - sum;
                }
                else
                {
                    sum = GetSubjectSumMargin(SubjectNo, "finalbalance", LastYearMonth) - sum;
                }
            }
            if (isOnloan) { sum = 0 - sum; }
            return sum;
        }

        /// 函数名称：GetSubjectSumMargin
        /// 函数作者：朱坤堂
        /// 创建时间：2010-01-27
        /// <summary>
        /// 获取科目余额
        /// </summary>
        /// <param name="SubjectNo"></param>
        /// <param name="Field"></param>
        /// <param name="YearMonth"></param>
        /// <returns></returns>
        public static decimal GetSubjectSumMargin(string SubjectNo, string Field, string YearMonth)
        {
            string sum = GetMonthSum(SubjectNo, Field, YearMonth);
            if (sum == "NoDataItem" && Field.ToLower() == "beginbalance")
            {
                string LastCarryDate = MainClass.GetFieldFromID(UserInfo.AccountID, "LastCarryDate", "cw_account");
                if (LastCarryDate.Length == 0)
                {
                    sum = CommClass.GetTableValue("cw_subject", "beginbalance", string.Format("subjectno='{0}'", SubjectNo));
                    sum = GetMonthSum(SubjectNo, "beginbalance", YearMonth);
                }
                else
                {
                    string LastYearMonth = LastCarryDate.Substring(0, 8);
                    if (string.Compare(YearMonth, LastYearMonth) > 0)
                    {
                        sum = GetMonthSum(SubjectNo, "finalbalance", YearMonth);
                    }
                    else
                    {
                        string StartAccountDate = MainClass.GetFieldFromID(UserInfo.AccountID, "StartAccountDate", "cw_account");
                        string StartYearMonth = StartAccountDate.Substring(0, 8);
                        sum = GetMonthSum(SubjectNo, Field, StartYearMonth);
                    }
                }
            }
            return sum == "NoDataItem" ? 0 : decimal.Parse(sum);
        }

        public static string GetMonthSum(string SubjectNo, string Field, string YearMonth)
        {
            string sum = "0";
            if (YearMonth.EndsWith("12月"))
            {
                sum = CommClass.GetTableValue("cw_lastmonthsum", Field, string.Format("subjectno='{0}' and yearmonth='{1}'", SubjectNo, YearMonth));
            }
            else
            {
                sum = CommClass.GetTableValue("cw_viewsubjectsum", Field, string.Format("subjectno='{0}' and yearmonth='{1}'", SubjectNo, YearMonth));
            }
            return sum;
        }

        /// 函数名称：JSEval
        /// 函数作者：朱坤堂
        /// 创建时间：2010-01-27
        /// <summary>
        /// 字符串格式数字运算
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static object JSEval(string Expression)
        {
            string _Expression = Expression.Replace("(", "").Replace(")", "").Replace("--", "+").Replace("++", "+").Replace("+-", "-");
            try
            {
                return Microsoft.JScript.Eval.JScriptEvaluate(_Expression, Microsoft.JScript.Vsa.VsaEngine.CreateEngine());
            }
            catch
            {
                return (object)0;
            }
        }
    }

