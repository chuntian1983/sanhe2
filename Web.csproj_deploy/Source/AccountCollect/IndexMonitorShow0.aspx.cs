﻿using System;
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
using System.Reflection;
using System.Text;

public partial class AccountCollect_IndexMonitorShow0 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            GridView1.Attributes.Add("onselectstart", "return false;");
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            UnitName.Attributes["onclick"] = "SelectUnit()";
            UnitName.Attributes["readonly"] = "readonly";
            ReportDate.Text = DateTime.Now.ToString("yyyy年MM月");
            ReportDate.Attributes["readonly"] = "readonly";
            SMinus.Attributes["onclick"] = "return setYear('SelYear',-1);";
            SPlus.Attributes["onclick"] = "return setYear('SelYear',1);";
            SelYear.Attributes["readonly"] = "readonly";
            SelYear.Text = DateTime.Now.Year.ToString();
            SelMonth.Attributes["onchange"] = "setMonth(this.value);";
            SelMonth.Text = DateTime.Now.Month.ToString("00");
            DataRow row = MainClass.GetDataRow("select IndexSubject,IndexType,IndexValue from cw_indexmonitor where id='" + Request.QueryString["id"] + "'");
            string _IndexSubject = row["IndexSubject"].ToString();
            int dindex = _IndexSubject.IndexOf(".");
            IndexSubject.Value = _IndexSubject.Substring(0, dindex);
            sIndexSubject.Text = _IndexSubject.Substring(dindex + 1);
            IndexValue.Value = row["IndexValue"].ToString();
            sIndexValue.Text = IndexValue.Value;
            //string[] _IndexType ={ "单笔发生额", "月累计发生额", "年累计发生额" };
            //IndexType.Text = "单笔发生额";
            if (row["IndexType"].ToString() == "0")
            {
                sIndexType.Text = "借方单笔发生额";
            }
            else
            {
                sIndexType.Text = "贷方单笔发生额";
                BalanceType.SelectedIndex = 1;
            }
            BalanceType.Enabled = false;
            GetCommDetailInfo();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GetCommDetailInfo();
    }
    protected void GetCommDetailInfo()
    {
        AName.Text = UnitName.Text;
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 5; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        string[] AccountList = GAccountList.Value.Split('$');
        string balanceExpr = string.Empty;
        if (BalanceType.SelectedValue == "0")
        {
            balanceExpr = "SumMoney>=" + IndexValue.Value;
        }
        else
        {
            balanceExpr = "SumMoney<=-" + IndexValue.Value;
        }
        for (int i = 0; i < AccountList.Length; i++)
        {
            if (AccountList[i].Length > 0)
            {
                FillDataTable(BindTable, AccountList[i], balanceExpr);
            }
        }
        MainClass.CreateGridView("000016", BindTable, GridView1);
        for (int i = 1; i < GridView1.Rows.Count; i++)
        {
            string[] noid = BindTable.Rows[i][2].ToString().Split('|');
            if (noid.Length == 3)
            {
                GridView1.Rows[i].Cells[2].Text = noid[0];
                GridView1.Rows[i].Attributes.Add("ondblclick", "ShowVoucher('" + noid[1] + "','" + noid[2] + "');");
                GridView1.Rows[i].Attributes["title"] = "提示：双击行可以查看详细凭证。";
            }
        }
    }
    private void FillDataTable(DataTable BindTable, string accountID, string balanceExpr)
    {
        UserInfo.AccountID = accountID;
        DataTable ve = CommClass.GetDataTable(string.Concat("select VoucherDate,VoucherNo,VoucherID,VSummary,SumMoney from CW_VoucherEntry where voucherdate like '",
            SelYear.Text, "年", SelMonth.SelectedValue, "%' and ", balanceExpr, " and subjectno like '", IndexSubject.Value, "%' order by left(VoucherDate,8),VoucherNo"));
        if (ve.Rows.Count > 0)
        {
            foreach (DataRow row in ve.Rows)
            {
                DataRow NewRow = BindTable.NewRow();
                string unitid = MainClass.GetFieldFromID(accountID, "unitid", "cw_account");
                string towname = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + unitid + "']", "UnitName");
                NewRow[0] = string.Format("{0}-{1}", towname, MainClass.GetFieldFromID(accountID, "accountname", "cw_account"));
                NewRow[1] = row["VoucherDate"].ToString();
                NewRow[2] = string.Format("{0}|{1}|{2}", row["VoucherNo"].ToString(), row["VoucherID"].ToString(), accountID);
                NewRow[3] = row["VSummary"].ToString();
                decimal SumMoney = Math.Abs(decimal.Parse(row["SumMoney"].ToString()));
                NewRow[4] = SumMoney.ToString("#,##0.00");
                BindTable.Rows.Add(NewRow);
            }
        }
    }
    protected void OutputDataToExcel_Click(object sender, EventArgs e)
    {
        PageClass.ToExcel(GridView1);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}