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

public partial class AccountInit_PopWBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            form1.Attributes.Add("onsubmit", "return SubmitForm();");
            CreditSubject.Value = SysConfigs.CreditSubject;
            BalanceType.Attributes.Add("onchange", "SelectAccountType()");
            MonthBalance.Attributes.Add("onkeyup", "WriteBalance()");
            MonthBalance.Attributes.Add("onkeypress", "return CheckWrite();");
            UtilsPage.SetTextBoxAutoValue(MonthBalance, "0");
            UtilsPage.SetTextBoxAutoValue(SCount, "0");
            InitWebControl();
        }
    }

    protected void InitWebControl()
    {
        DataSet ds = CommClass.GetDataSet("select * from cw_subject where id='" + Request.QueryString["id"] + "'");
        SubjectNo.Text = ds.Tables[0].Rows[0]["SubjectNo"].ToString();
        SubjectName.Text = ds.Tables[0].Rows[0]["SubjectName"].ToString();
        string[] _SubjectType ={ "", "资产类", "负债类", "权益类", "成本类", "损益类" };
        SubjectType.Text = _SubjectType[int.Parse(ds.Tables[0].Rows[0]["SubjectType"].ToString())];
        HSubjectType.Value = ds.Tables[0].Rows[0]["SubjectType"].ToString();
        if (ds.Tables[0].Rows[0]["BeginBalance"].ToString().IndexOf("-") == -1)
        {
            if (ds.Tables[0].Rows[0]["BeginBalance"].ToString() != "0")
            {
                BalanceType.Text = "+";
            }
        }
        else
        {
            BalanceType.Text = "-";
        }
        MonthBalance.Text = ds.Tables[0].Rows[0]["BeginBalance"].ToString().Replace("-", "");
        if (ds.Tables[0].Rows[0]["AccountType"].ToString() == "0")
        {
            AccountType.Value = "0";
            AccountTypeShow.Text = "一般金额账";
        }
        else
        {
            AccountType.Value = "1";
            AccountTypeShow.Text = "数量金额账";
        }
        if (ds.Tables[0].Rows[0]["AccountType"].ToString() == "0")
        {
            SCount.Enabled = false;
            SUnit.Enabled = false;
            SType.Enabled = false;
            SClass.Enabled = false;
        }
        else
        {
            ds = CommClass.GetDataSet("select * from cw_subjectdata where subjectid='" + Request.QueryString["id"] + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                SCount.Text = ds.Tables[0].Rows[0]["amount"].ToString();
                SUnit.Text = ds.Tables[0].Rows[0]["SUnit"].ToString();
                SType.Text = ds.Tables[0].Rows[0]["SType"].ToString();
                SClass.Text = ds.Tables[0].Rows[0]["SClass"].ToString();
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string CID = Request.QueryString["id"].ToString();
        StringBuilder SQLString = new StringBuilder();
        string NewBalance = MonthBalance.Text.Length == 0 ? "0" : (BalanceType.SelectedValue + MonthBalance.Text);
        string OldBalance = CommClass.GetFieldFromNo(SubjectNo.Text, "BeginBalance");
        if (NewBalance != OldBalance)
        {
            string ID = CID;
            decimal SubBalance = decimal.Parse(NewBalance) - decimal.Parse(OldBalance);
            while(true)
            {
                ID = CommClass.GetFieldFromNo(CommClass.GetFieldFromID(ID, "parentno"), "id");
                if (ID == "NoDataItem") { break; }
                SQLString.Append("update cw_subject set BeginBalance=BeginBalance+(" + SubBalance.ToString() + ") where id='" + ID + "'#sql#");
            }
        }
        SQLString.Append("update cw_subject set BeginBalance=" + NewBalance + " where id='" + CID + "'");
        CommClass.ExecuteTransaction(SQLString.ToString());
        if (AccountType.Value == "1")
        {
            //数量金额账录入
            DataSet ds = CommClass.GetDataSet("select * from cw_subjectdata where subjectid='" + CID + "'");
            if (ds.Tables[0].Rows.Count == 0)
            {
                DataRow row = ds.Tables[0].NewRow();
                row["subjectid"] = CID;
                row["amount"] = SCount.Text;
                row["balance"] = NewBalance;
                row["SUnit"] = SUnit.Text;
                row["SType"] = SType.Text;
                row["SClass"] = SClass.Text;
                ds.Tables[0].Rows.Add(row);
            }
            else
            {
                ds.Tables[0].Rows[0]["amount"] = SCount.Text;
                ds.Tables[0].Rows[0]["balance"] = NewBalance;
                ds.Tables[0].Rows[0]["SUnit"] = SUnit.Text;
                ds.Tables[0].Rows[0]["SType"] = SType.Text;
                ds.Tables[0].Rows[0]["SClass"] = SClass.Text;
            }
            CommClass.UpdateDataSet(ds);
        }
        if (sender.Equals(Button3))
        {
            ExeScript.Text = "<script>window.close();</script>";
            return;
        }
        InitWebControl();
        ExeScript.Text = "<script>alert('期初余额录入成功！');</script>";
    }
}
