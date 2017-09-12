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

public partial class AccountCollect_IndexMonitorShow2 : System.Web.UI.Page
{
    public StringBuilder showValue = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            UnitName.Attributes["onclick"] = "SelectUnit()";
            UnitName.Attributes["readonly"] = "readonly";
            ReportDate.Text = DateTime.Now.ToString("yyyy年");
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
            //IndexType.Text = "年累计发生额";
            sIndexType.Text = "年累计发生额";
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
        ClsCalculate clsCalculate = new ClsCalculate();
        clsCalculate.DesignID = "000000";
        clsCalculate.ReportDate = string.Format("{0}年{1}月", SelYear.Text, SelMonth.SelectedValue);
        string expr0 = string.Format("{0}[{1}]:本年借方累计", sIndexSubject.Text, IndexSubject.Value);
        string expr1 = string.Format("{0}[{1}]:本年贷方累计", sIndexSubject.Text, IndexSubject.Value);
        string trTemplate = "<tr><td class='t1' style='height: 25px; text-align: center'>{0}月</td><td class='t1' {1}>{2}</td><td class='t2' {3}>{4}</td></tr>";
        string[] accounts = GAccountList.Value.Split('$');
        decimal lv = decimal.Parse(IndexValue.Value);
        for (int i = 0; i < accounts.Length - 1; i++)
        {
            UserInfo.AccountID = accounts[i];
            decimal v0 = 0;
            decimal v1 = 0;
            if (BalanceType.SelectedValue == "0")
            {
                v0 = decimal.Parse(clsCalculate.GetExprValue(expr0).ToString());
                if (v0 < lv)
                {
                    continue;
                }
                v1 = decimal.Parse(clsCalculate.GetExprValue(expr1).ToString());
            }
            else
            {
                v1 = decimal.Parse(clsCalculate.GetExprValue(expr1).ToString());
                if (v1 < lv)
                {
                    continue;
                }
                v0 = decimal.Parse(clsCalculate.GetExprValue(expr0).ToString());
            }
            showValue.AppendFormat(trTemplate, new string[] { UserInfo.AccountName, (v0 < lv ? "" : "style='color:red'"), v0.ToString("#0.00"), (v1 < lv ? "" : "style='color:red'"), v1.ToString("#0.00") });
        }
    }
}
