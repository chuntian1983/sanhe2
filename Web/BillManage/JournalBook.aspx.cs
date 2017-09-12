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

public partial class BillManage_JournalBook : System.Web.UI.Page
{
    public string sname;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
            if (Request.QueryString["flag"] == "102")
            {
                sname = "银行存款";
                PageTitle.InnerHtml = "银行存款日记账管理";
                VoucherType.Items.RemoveAt(0);
                VoucherType.Items.RemoveAt(0);
            }
            else
            {
                sname = "现金科目";
                DivSettle.Style["display"] = "none";
                VoucherType.Items.RemoveAt(2);
                VoucherType.Items.RemoveAt(2);
            }
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            UtilsPage.SetTextBoxAutoValue(DebitMoney, 0);
            UtilsPage.SetTextBoxAutoValue(CreditMoney, 0);
            //UtilsPage.SetTextBoxReadOnly(VoucherDate);
            UtilsPage.SetTextBoxReadOnly(AccCurrency);
            UtilsPage.SetTextBoxReadOnly(Balance);
            UtilsPage.SetTextBoxReadOnly(DoBill);
            UtilsPage.SetTextBoxReadOnly(AccSubjectNo);
            //UtilsPage.SetTextBoxReadOnly(LinkSubjectNo);
            UtilsPage.SetTextBoxCalendar(SettleDate);
            UtilsPage.SetTextBoxCalendar(VoucherDate, "yyyy年MM月dd日");
            //VoucherDate.Attributes["onclick"] = "SetVoucherDate('VoucherDate')";
            AccSubjectNo.Attributes["onclick"] = "SelSubject('AccSubjectNo','filter=" + Request.QueryString["flag"] + "')";
            //LinkSubjectNo.Attributes["onclick"] = "SelSubject('LinkSubjectNo','')";
            DebitMoney.Attributes["onkeyup"] = "inputMoeny('CreditMoney')";
            CreditMoney.Attributes["onkeyup"] = "inputMoeny('DebitMoney')";
            Handler.Text = UserInfo.RealName;
            DoBill.Text = UserInfo.RealName;
            TableID.Value = Request.QueryString["id"];
            DateTime adate = MainClass.GetAccountDate();
            VoucherDate.Text = adate.ToString("yyyy年MM月dd日");
            string subjectno = Request.QueryString["flag"];
            if (TableID.Value.Length > 0)
            {
                DataRow brow = CommClass.GetDataRow("select * from cw_dayaccount where id='" + TableID.Value + "'");
                AccSubjectNo.Text = brow["AccSubjectNo"].ToString();
                subjectno = AccSubjectNo.Text.Substring(0, AccSubjectNo.Text.IndexOf("."));
                AccCurrency.Text = brow["AccCurrency"].ToString();
                VoucherDate.Text = brow["VoucherDate"].ToString();
                DayNo.Text = brow["DayNo"].ToString();
                VoucherType.Text = brow["VoucherType"].ToString();
                VoucherNo.Text = brow["VoucherNo"].ToString();
                EntryNo.Text = brow["EntryNo"].ToString();
                SettleType.Text = brow["SettleType"].ToString();
                SettleNo.Text = brow["SettleNo"].ToString();
                SettleDate.Text = brow["SettleDate"].ToString();
                string accMoney = brow["AccMoney"].ToString();
                if (accMoney.StartsWith("-"))
                {
                    CreditMoney.Text = accMoney.Substring(1);
                }
                else
                {
                    DebitMoney.Text = accMoney;
                }
                //LinkSubjectNo.Text = brow["LinkSubjectNo"].ToString();
                Handler.Text = brow["Handler"].ToString();
                DoBill.Text = brow["DoBill"].ToString();
                Notes.Text = brow["Notes"].ToString();
                ShowFile.NavigateUrl = brow["AttachFile"].ToString();
                //string vid = brow["VoucherID"].ToString();
                //if (vid.Length > 0 && vid != "-")
                //{
                //    Button1.Enabled = false;
                //    IsCreateVoucher.Enabled = false;
                //    aShowVoucher.NavigateUrl = string.Concat("javascript:ShowVoucher('", vid, "')");
                //}
            }
            DelFile.Enabled = ShowFile.NavigateUrl.Length != 0;
            string yearmonth = adate.ToString("yyyy年MM月");
            //decimal begin = TypeParse.StrToDecimal(ClsCalculate.GetSubjectSum(subjectno, "beginbalance", yearmonth), 0);
            //decimal balance = TypeParse.StrToDecimal(CommClass.GetTableValue("cw_dayaccount", "sum(AccMoney)", string.Concat("id<>'", TableID.Value, "' and (AccSubjectNo like '", subjectno, "%') and (VoucherDate like '", yearmonth, "%') and VoucherDate<='", yearmonth, "31日'"), "0"), 0);
            //balance += begin;
            decimal balance = TypeParse.StrToDecimal(CommClass.GetTableValue("cw_dayaccount", "sum(AccMoney)", string.Concat("id<>'", TableID.Value, "' and (AccSubjectNo like '", subjectno, "%')"), "0"), 0);
            hidBalance.Value = balance.ToString();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bool isNew = (TableID.Value.Length == 0);
        Dictionary<string, string> DicFeilds = new Dictionary<string, string>();
        if (isNew)
        {
            TableID.Value = CommClass.GetRecordID("cw_dayaccount");
            DicFeilds["ID"] = TableID.Value;
            DicFeilds["VoucherID"] = "-";
        }
        DicFeilds.Add("AccSubjectNo", AccSubjectNo.Text);
        DicFeilds.Add("AccCurrency", AccCurrency.Text);
        DicFeilds.Add("VoucherDate", VoucherDate.Text);
        DicFeilds.Add("DayNo", DayNo.Text);
        DicFeilds.Add("VoucherType", VoucherType.SelectedValue);
        DicFeilds.Add("VoucherNo", VoucherNo.Text);
        DicFeilds.Add("EntryNo", EntryNo.Text);
        DicFeilds.Add("SettleType", SettleType.SelectedValue);
        DicFeilds.Add("SettleNo", SettleNo.Text);
        DicFeilds.Add("SettleDate", SettleDate.Text);
        string moeny = string.Empty;
        if (CreditMoney.Text == "0" || CreditMoney.Text == "0.00")
        {
            moeny = DebitMoney.Text;
            DicFeilds.Add("AccMoney", DebitMoney.Text);
        }
        else
        {
            moeny = CreditMoney.Text;
            DicFeilds.Add("AccMoney", "-" + CreditMoney.Text);
        }
        //DicFeilds.Add("LinkSubjectNo", LinkSubjectNo.Text);
        DicFeilds.Add("Handler", Handler.Text);
        DicFeilds.Add("DoBill", DoBill.Text);
        DicFeilds.Add("Notes", Notes.Text);
        string file = UtilsPage.UploadFiles();
        DicFeilds.Add("AttachFile", file);
        //if (IsCreateVoucher.Checked)
        //{
        //    Dictionary<string, string> d = new Dictionary<string, string>();
        //    d.Add(AccSubjectNo.Text.Substring(0, AccSubjectNo.Text.IndexOf(".")), moeny);
        //    Dictionary<string, string> c = new Dictionary<string, string>();
        //    c.Add(LinkSubjectNo.Text.Substring(0, LinkSubjectNo.Text.IndexOf(".")), moeny);
        //    string subID = string.Empty;
        //    if (CreditMoney.Text == "0" || CreditMoney.Text == "0.00")
        //    {
        //        subID = CommOutCall.CreateNewVoucher("DC", Notes.Text, d, c, file, true, true);
        //    }
        //    else
        //    {
        //        subID = CommOutCall.CreateNewVoucher("DC", Notes.Text, c, d, file, true, true);
        //    }
        //    DicFeilds.Add("VoucherID", subID);
        //}
        if (isNew)
        {
            CommClass.ExecuteSQL("cw_dayaccount", DicFeilds);
        }
        else
        {
            CommClass.ExecuteSQL("cw_dayaccount", DicFeilds, string.Concat("id='", TableID.Value, "'"));
        }
        PageClass.ExcuteScript(this.Page, "window.returnValue='1';alert('保存成功！');window.close();");
        RefreshFlag.Value = "1";
    }
    protected void DelFile_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath(ShowFile.NavigateUrl);
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
        CommClass.ExecuteSQL("update cw_dayaccount set AttachFile='' where id='" + TableID.Value + "'");
        DelFile.Enabled = false;
        ShowFile.NavigateUrl = "";
    }
}
