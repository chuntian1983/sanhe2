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
using System.Reflection;
using System.Text;

public partial class AccountCollect_VoucherQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            GridView1.Attributes.Add("onselectstart", "return false;");
            ReportDate.Text = DateTime.Now.ToString("yyyy年MM月");
            ReportDate.Attributes["readonly"] = "readonly";
            SMinus.Attributes["onclick"] = "return setYear('SelYear',-1);";
            SPlus.Attributes["onclick"] = "return setYear('SelYear',1);";
            SelYear.Attributes["readonly"] = "readonly";
            SelYear.Text = DateTime.Now.Year.ToString();
            SelMonth.Attributes["onchange"] = "setMonth(this.value);";
            SelMonth.Text = DateTime.Now.Month.ToString("00");
            UtilsPage.SetTextBoxOnlyNumber(MoneyUp);
            UtilsPage.SetTextBoxOnlyNumber(MoneyDown);
            DataRowCollection rows = MainClass.GetDataRows("select levelname,collectunits from cw_collectlevel where unitid='" + Session["UnitID"].ToString() + "'");
            if (rows != null)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    CollectUnit.Items.Add(new ListItem(rows[i]["levelname"].ToString(), rows[i]["collectunits"].ToString()));
                }
            }
            GetCommDetailInfo();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GetCommDetailInfo();
    }
    protected void GetCommDetailInfo()
    {
        switch (CollectUnit.SelectedValue)
        {
            case "000000":
                AName.Text = "";
                break;
            case "XXXXXX":
                AName.Text = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + Session["UnitID"].ToString() + "']", "UnitName");
                break;
            default:
                AName.Text = CollectUnit.SelectedItem.Text;
                break;
        }
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 5; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        if (CollectUnit.SelectedValue != "000000")
        {
            if (CollectUnit.SelectedValue == "")
            {
                ExeScript.Text = "<script>alert('汇总单位【" + CollectUnit.SelectedItem.Text + "】无汇总账套或下级单位！');</script>";
            }
            else
            {
                string _CollectUnit = "";
                StringBuilder AllAccount = new StringBuilder();
                if (CollectUnit.SelectedValue == "XXXXXX")
                {
                    _CollectUnit = "-" + Session["UnitID"].ToString();
                }
                else
                {
                    _CollectUnit = CollectUnit.SelectedValue;
                }
                string[] _G = _CollectUnit.Split('-');
                AllAccount.Append(_G[0]);
                string[] UnitList = _G[1].Split('$');
                for (int i = 0; i < UnitList.Length; i++)
                {
                    GetCollectAccount(ref AllAccount, UnitList[i]);
                }
                if (AllAccount.Length == 0)
                {
                    ExeScript.Text = "<script>alert('汇总单位【" + CollectUnit.SelectedItem.Text + "】下级无汇总账套！');</script>";
                }
                else
                {
                    string moneySet = string.Empty;
                    string[] Relation = { "=", "<>", ">", "<=", "<", ">=" };
                    if (MoneyUp.Text.Length > 0)
                    {
                        moneySet += string.Format(" and SumMoney {0} {1}", Relation[Relation0.SelectedIndex], MoneyUp.Text);
                    }
                    if (MoneyDown.Text.Length > 0)
                    {
                        moneySet += string.Format(" and SumMoney {0} {1}", Relation[Relation1.SelectedIndex], MoneyDown.Text);
                    }
                    ViewState["MoneySet"] = moneySet;
                    string[] AccountList = AllAccount.ToString().Split('$');
                    for (int i = 0; i < AccountList.Length; i++)
                    {
                        if (AccountList[i].Length > 0)
                        {
                            FillDataTable(BindTable, AccountList[i]);
                        }
                    }
                }
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
    protected void GetCollectAccount(ref StringBuilder AllAccount, string UnitID)
    {
        DataSet ds = MainClass.GetDataSet("select id from cw_account where unitid='" + UnitID + "'");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            AllAccount.Append(row["id"].ToString() + "$");
        }
        DataRow[] rows = ValidateClass.GetRegRows("CUnits", "parentid='" + UnitID + "'");
        if (rows != null)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                GetCollectAccount(ref AllAccount, rows[i]["id"].ToString());
            }
        }
    }
    private void FillDataTable(DataTable BindTable, string accountID)
    {
        UserInfo.AccountID = accountID;
        DataTable ve = CommClass.GetDataTable("select VoucherDate,VoucherNo,VoucherID,VSummary,SumMoney from CW_VoucherEntry where voucherdate like '"
            + SelYear.Text + "年" + SelMonth.SelectedValue + "%' " + ViewState["MoneySet"].ToString() + " and ((subjectno like '101%') or (subjectno like '102%')) order by left(VoucherDate,8),VoucherNo");
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
