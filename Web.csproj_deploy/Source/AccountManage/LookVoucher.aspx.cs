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

public partial class AccountManage_LookVoucher : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["aid"] != null)
        {
            Session["SessionFlag"] = "SessionFlag";
            Session["AccountID"] = Request.QueryString["aid"];
            Button3.Enabled = false;
            Button4.Enabled = false;
            Button5.Enabled = false;
        }
        else
        {
            UserInfo.CheckSession2();
        }
        if (!IsPostBack)
        {
            if (CommClass.CheckExist("cw_voucher", string.Format("id='{0}'", Request.QueryString["id"])) == false)
            {
                Response.Clear();
                Response.Write("<script>window.close();alert('该凭证已删除！');</script>");
                Response.End();
            }
            if (Request.QueryString["bid"] == null)
            {
                Button5.Visible = false;
            }
            else
            {
                if (CommClass.GetTableValue("cw_voucher", "IsAuditing", string.Format("id='{0}'", Request.QueryString["id"])) == "0")
                {
                    Button5.Visible = true;
                    Button5.Attributes["onclick"] = "return confirm('您确定需要审核该凭证吗？')";
                }
            }
            if (Session["Powers"] != null && Session["Powers"].ToString().IndexOf("000003") != -1)
            {
                Button3.Attributes["onclick"] = "return confirm('您确定需要复制该凭证的所有分录至新凭证吗？')";
                Button3.Enabled = true;
                Button4.Attributes["onclick"] = "return confirm('您确定需要对该凭证执行冲红操作吗？')";
                Button4.Enabled = true;
            }
        }
        else
        {
            ShowVoucher.RefreshFlag = 1;
        }
        ShowVoucher.VoucherID = Request.QueryString["id"];
        ShowVoucher.button = Button4;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        CopyVoucher(false);
        PageClass.ShowAlertMsg(this.Page, "凭证分录复制成功！");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        switch (Button4.CommandArgument)
        {
            case "DoReverse":
                CopyVoucher(true);
                Button4.Enabled = false;
                PageClass.ShowAlertMsg(this.Page, "凭证冲红成功！");
                break;
            case "CancelReverse":
                CommClass.ExecuteSQL("update cw_voucher set ReverseVoucherID='' where id='" + Request.QueryString["id"] + "'");
                ShowVoucher.InitVoucherInfo();
                Button4.Text = "冲红当前凭证";
                Button4.CommandArgument = "DoReverse";
                PageClass.ShowAlertMsg(this.Page, "取消冲红状态成功！");
                break;
            case "DelReverse":
                DataTable dt = CommClass.GetDataTable("select id from cw_voucher where ReverseVoucherID='" + Request.QueryString["id"] + "'");
                foreach (DataRow row in dt.Rows)
                {
                    CommClass.ExecuteSQL("delete from cw_entry where voucherid='" + row["id"].ToString() + "'");
                }
                CommClass.ExecuteSQL("delete from cw_voucher where ReverseVoucherID='" + Request.QueryString["id"] + "'");
                PageClass.ShowAlertMsg(this.Page, "删除冲红凭证成功！");
                break;
        }
    }
    private void CopyVoucher(bool copyFlag)
    {
        DateTime AccountDate = MainClass.GetAccountDate();
        string LastVoucherNo = CommClass.GetTableValue("cw_voucher", "voucherno", "voucherdate like '" + AccountDate.ToString("yyyy年MM月") + "%' order by voucherno desc");
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
        NewDataRow["voucherfrom"] = "GA";
        NewDataRow["voucherdate"] = AccountDate.ToString("yyyy年MM月dd日");
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
        if (copyFlag)
        {
            NewDataRow["ReverseVoucherID"] = Request.QueryString["id"];
        }
        Voucher.Tables[0].Rows.Add(NewDataRow);
        CommClass.UpdateDataSet(Voucher);
        StringBuilder SQLString = new StringBuilder();
        DataTable entryRows = CommClass.GetDataTable("select * from cw_entry where voucherid='" + Request.QueryString["id"] + "' order by id asc");
        foreach (DataRow row in entryRows.Rows)
        {
            string summoney = row["summoney"].ToString();
            if (copyFlag)
            {
                summoney = summoney.StartsWith("-") ? summoney.Substring(1) : "-" + summoney;
            }
            SQLString.Append("insert cw_entry(id,voucherid,vsummary,subjectno,summoney)values('"
                + CommClass.GetRecordID("CW_Entry") + "','"
                + VoucherID + "','"
                + row["vsummary"].ToString() + "','"
                + row["subjectno"].ToString() + "','"
                + summoney + "')#sql#");
        }
        CommClass.ExecuteTransaction(SQLString.ToString());
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        MainClass.ExecuteSQL(string.Format("update cw_balancealarm set DoState='1' where id='{0}'", Request.QueryString["bid"]));
        CommClass.ExecuteSQL(string.Format("update cw_voucher set IsAuditing='1' where id='{0}'", Request.QueryString["id"]));
        ShowVoucher.InitVoucherInfo();
        Button5.Visible = false;
    }
}
