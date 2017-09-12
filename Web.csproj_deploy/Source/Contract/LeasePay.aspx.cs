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

public partial class Contract_LeasePay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            Button1.Attributes["onclick"] = "return confirm('您确定需要执行“'+this.value+'”操作吗？');";
            PayDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            PayDate.Attributes.Add("readonly", "readonly");
            PayDate.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].PayDate,'yyyy-mm-dd')");
            DataRow row = CommClass.GetDataRow("select CardType,ResourceName,IncomeSubject,PaySubject from cw_resleasecard where id='" + Request.QueryString["lid"] + "'");
            ResName.Text = row["ResourceName"].ToString();
            IncomeSubject.Text = row["IncomeSubject"].ToString();
            PaySubject.Text = row["PaySubject"].ToString();
            PayUser.Text = UserInfo.RealName;
            CardType.Value = row["CardType"].ToString();
            if (CardType.Value == "0")
            {
                TD_Name.InnerText = "租赁资产：";
            }
            else
            {
                TD_Name.InnerText = "租赁资源：";
            }
            InitPayInfo();
        }
    }
    protected void PayList_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitPayInfo();
    }
    protected void InitPayInfo()
    {
        DataRow row = CommClass.GetDataRow("select * from CW_ResPayPeriod where id='" + Request.QueryString["pid"] + "'");
        PeriodName.Text = row["PeriodName"].ToString();
        SPay.Text = row["StartPay"].ToString();
        EPay.Text = row["EndPay"].ToString();
        PayMoney.Text = row["PayMoney"].ToString();
        Notes.Text = row["Notes"].ToString();
        PayState.Value = row["PayState"].ToString();
        VoucherID.Value = row["VoucherID"].ToString();
        InitButton();
    }
    protected void InitButton()
    {
        //设置按钮状态
        if (PayState.Value == "0")
        {
            Button1.Text = "收款";
            Button2.Enabled = false;
            Button3.Enabled = false;
        }
        else
        {
            Button1.Text = "取消收款";
            if (IncomeSubject.Text.Length == 0 || PaySubject.Text.Length == 0)
            {
                Button2.Enabled = false;
            }
            else
            {
                Button2.Enabled = true;
            }
            if (VoucherID.Value.Length == 0)
            {
                Button1.Enabled = true;
                Button2.Text = "生成凭证";
                Button3.Enabled = false;
            }
            else
            {
                Button1.Enabled = false;
                Button2.Enabled = true;
                Button2.Text = "删除凭证";
                Button3.Enabled = true;
                Button3.Attributes["onclick"] = "return ShowVoucher('" + VoucherID.Value + "');";
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (PayState.Value == "0")
        {
            //设置收款状态
            CommClass.ExecuteSQL("update CW_ResPayPeriod set PayState='1',PayUser='" + PayUser.Text
                + "',PayDate='" + PayDate.Text + "',Notes='" + Notes.Text + "' where id='" + Request.QueryString["pid"] + "'");
            CommClass.ExecuteSQL("update CW_ResLeaseCard set SumRental=SumRental+" + PayMoney.Text + " where id='" + Request.QueryString["lid"] + "'");
            PayState.Value = "1";
        }
        else
        {
            //取消收款状态
            PayState.Value = "0";
            CommClass.ExecuteSQL("update CW_ResPayPeriod set PayState='0',PayUser='',PayDate='',Notes='" + Notes.Text + "' where id='" + Request.QueryString["pid"] + "'");
            CommClass.ExecuteSQL("update CW_ResLeaseCard set SumRental=SumRental-" + PayMoney.Text + " where id='" + Request.QueryString["lid"] + "'");
        }
        PageClass.ExcuteScript(this.Page, "alert('操作成功！');");
        InitButton();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (VoucherID.Value.Length == 0)
        {
            //租赁收款
            if (PaySubject.Text.Length == 0 || IncomeSubject.Text.Length == 0)
            {
                PageClass.ExcuteScript(this.Page, "alert('操作失败！');");
            }
            else
            {
                VoucherID.Value = GetVoucherID();
                CommClass.ExecuteSQL("update CW_ResPayPeriod set VoucherID='" + VoucherID.Value + "' where id='" + Request.QueryString["pid"] + "'");
                PageClass.ExcuteScript(this.Page, "alert('操作成功！');");
            }
        }
        else
        {
            //删除收款凭证
            if (CommClass.CheckExist("cw_voucher", "ID='" + VoucherID.Value + "' and IsAuditing='1'"))
            {
                PageClass.ExcuteScript(this.Page, "alert('该凭证已审核不可删除！');");
            }
            else
            {
                CommClass.ExecuteSQL("delete from cw_voucher where ID='" + VoucherID.Value + "'");
                CommClass.ExecuteSQL("delete from cw_entry where VoucherID='" + VoucherID.Value + "'");
                CommClass.ExecuteSQL("update CW_ResPayPeriod set VoucherID='' where id='" + Request.QueryString["pid"] + "'");
                VoucherID.Value = "";
                PageClass.ExcuteScript(this.Page, "alert('操作成功！');");
            }
        }
        InitButton();
    }
    private string GetVoucherID()
    {
        DateTime accountDate = MainClass.GetAccountDate();
        string LastVoucherNo = CommClass.GetTableValue("cw_voucher", "voucherno", "voucherdate like '" + accountDate.ToString("yyyy年MM月") + "%' order by voucherno desc");
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
        if (CardType.Value == "0")
        {
            NewDataRow["voucherfrom"] = "FA";
        }
        else
        {
            NewDataRow["voucherfrom"] = "RS";
        }
        NewDataRow["voucherdate"] = accountDate.ToString("yyyy年MM月dd日");
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
        CommClass.ExecuteSQL("insert cw_entry(id,voucherid,vsummary,subjectno,summoney)values('"
            + CommClass.GetRecordID("CW_Entry") + "','"
            + VoucherID + "','租赁[" + ResName.Text + "]收款','"
            + PaySubject.Text.Substring(0, PaySubject.Text.IndexOf(".")) + "','"
            + PayMoney.Text + "')");
        CommClass.ExecuteSQL("insert cw_entry(id,voucherid,vsummary,subjectno,summoney)values('"
            + CommClass.GetRecordID("CW_Entry") + "','"
            + VoucherID + "','租赁[" + ResName.Text + "]收款','"
            + IncomeSubject.Text.Substring(0, IncomeSubject.Text.IndexOf(".")) + "','-"
            + PayMoney.Text + "')");
        return VoucherID;
    }
}
