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

public partial class Contract_LeaseCard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            BookDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            BookDate.Attributes.Add("readonly", "readonly");
            BookDate.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].BookDate,'yyyy-mm-dd')");
            SLease.Attributes.Add("readonly", "readonly");
            SLease.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].SLease,'yyyy-mm-dd')");
            ELease.Attributes.Add("readonly", "readonly");
            ELease.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].ELease,'yyyy-mm-dd')");
            NextPayDate.Attributes.Add("readonly", "readonly");
            NextPayDate.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].NextPayDate,'yyyy-mm-dd')");
            ResourceName.Attributes.Add("readonly", "readonly");
            IncomeSubject.Attributes.Add("readonly", "readonly");
            IncomeSubject.Attributes.Add("onclick", "SelectItem(1)");
            PaySubject.Attributes.Add("readonly", "readonly");
            PaySubject.Attributes.Add("onclick", "SelectItem(2)");
            CardNo.Text = "[自动编号]";
            CardNo.Attributes.Add("onclick", "if(this.value=='[自动编号]')this.value='';");
            CardNo.Attributes.Add("onblur", "if(this.value.length==0)this.value='[自动编号]';");
            PayType.Attributes["onchange"] = "SelPayType();";
            DelFile.Enabled = ShowFile.NavigateUrl.Length != 0;
            ResUnitName.Attributes.Add("readonly", "readonly");
            ContractDate.Attributes.Add("readonly", "readonly");
            ContractDate.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].ContractDate,'yyyy-mm-dd')");
            UtilsPage.SetTextBoxAutoValue(ResAmount, "0");
            UtilsPage.SetTextBoxAutoValue(YearRental, "0");
            UtilsPage.SetTextBoxAutoValue(SumRental, "0");
            UtilsPage.SetTextBoxAutoValue(NextPayMoney, "0");
            UtilsPage.SetTextBoxAutoValue(ContractMoney, "0");
            UtilsPage.SetTextBoxAutoValue(ContractYears, "0");
            if (string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                ResUnitName.Text = UserInfo.AccountName;
                ContractCo.Text = ResUnitName.Text;
                ResourceID.Value = Request.QueryString["rid"];
                if (Request.QueryString["ctype"] == "0")
                {
                    DataRow row = CommClass.GetDataRow("select AssetName,AUnit,AAmount from cw_assetcard where id='" + ResourceID.Value + "'");
                    ResourceName.Text = row["AssetName"].ToString();
                    ResUnit.Text = row["AUnit"].ToString();
                    ResAmount.Text = row["AAmount"].ToString();
                }
                else if (Request.QueryString["ctype"] == "1")
                {
                    DataRow row = CommClass.GetDataRow("select ResName,ResUnit,ResAmount from cw_rescard where id='" + ResourceID.Value + "'");
                    ResourceName.Text = row["ResName"].ToString();
                    ResUnit.Text = row["ResUnit"].ToString();
                    ResAmount.Text = row["ResAmount"].ToString();
                }
            }
            else
            {
                CardID.Value = Request.QueryString["id"];
                InitWebControl();
            }
            if (Request.QueryString["ctype"] == "0")
            {
                TD_Name.InnerText = "租赁资产：";
                HasAmount.Value = CommClass.GetTableValue("cw_assetcard", "AAmount-HasAmount", string.Format("id='{0}'", ResourceID.Value));
            }
            else if (Request.QueryString["ctype"] == "1")
            {
                TD_Name.InnerText = "租赁资源：";
                HasAmount.Value = CommClass.GetTableValue("cw_rescard", "ResAmount-HasAmount", string.Format("id='{0}'", ResourceID.Value));
            }
            else
            {
                TD_Name.InnerText = "资产/资源：";
                HasAmount.Value = "0";
                TdAmount.Style["display"] = "none";
                ResourceName.Attributes.Remove("readonly");
                ResourceName.BackColor = System.Drawing.Color.White;
                //ResourceName.Style["display"] = "none";
                //PageClass.ExcuteScript(this.Page, "$('Button4').style.display=''");
            }
            ResAmount.Text = HasAmount.Value;
            if (Request.QueryString["aid"] != null && Request.QueryString["aid"].Length > 0)
            {
                DataRow arow = CommClass.GetDataRow("select AssetModel,LAmount,LYears,LTotalBalance,PayType,PayMoney,AssessBasePrice from cw_flowasset where id='" + Request.QueryString["aid"] + "'");
                YearRental.Text = arow["PayMoney"].ToString();
                ResAmount.Text = arow["LAmount"].ToString();
                ContractYears.Text = arow["LYears"].ToString();
                ContractMoney.Text = arow["LTotalBalance"].ToString();
            }
        }
    }
    private void InitWebControl()
    {
        //初始化控件值
        DataRow row = CommClass.GetDataRow("select * from cw_resleasecard where id='" + CardID.Value + "'");
        OldCardNo.Value = row["CardNo"].ToString();
        CardNo.Text = row["CardNo"].ToString();
        ResourceID.Value = row["ResourceID"].ToString();
        ResourceName.Text = row["ResourceName"].ToString();
        ResUnit.Text = row["ResUnit"].ToString();
        ResAmount.Text = row["ResAmount"].ToString();
        OldAmount.Value = ResAmount.Text;
        SumRental.Text = row["SumRental"].ToString();
        YearRental.Text = row["YearRental"].ToString();
        PayType.Text = row["PayType"].ToString();
        NextPayDate.Text = row["NextPayDate"].ToString();
        NextPayMoney.Text = row["NextPayMoney"].ToString();
        SLease.Text = row["StartLease"].ToString();
        ELease.Text = row["EndLease"].ToString();
        LeaseHolder.Text = row["LeaseHolder"].ToString();
        LinkTel.Text = row["LinkTel"].ToString();
        Notes.Text = row["Notes"].ToString();
        ContractCo.Text = row["ContractCo"].ToString();
        ResUnitName.Text = row["ResUnitName"].ToString();
        ContractNo.Text = row["ContractNo"].ToString();
        ContractName.Text = row["ContractName"].ToString();
        ContractType.Text = row["ContractType"].ToString();
        ContractType.Text = row["ContractType"].ToString();
        ContractMoney.Text = row["ContractMoney"].ToString();
        ContractYears.Text = row["ContractYears"].ToString();
        ContractContent.Text = row["ContractContent"].ToString();
        ContractNote.Text = row["ContractNote"].ToString();
        ShowFile.NavigateUrl = row["Appendix"].ToString();
        DelFile.Enabled = ShowFile.NavigateUrl.Length != 0;
        //判断是否可编辑
        if (row["LeaseState"].ToString() == "0")
        {
            PayType.Attributes["onchange"] = "SelPayType();";
        }
        else
        {
            Button1.Enabled = false;
            PageClass.ExcuteScript(this.Page, "$('Button3').disabled='disabled';");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CardNo.Text != OldCardNo.Value && CommClass.CheckExist("CW_ResLeaseCard", string.Format("CardNo='{0}' and cardtype='{1}'", CardNo.Text, Request.QueryString["ctype"])))
        {
            ExeScript.Text = "<script>alert('该卡片编号[" + CardNo.Text + "]已存在！');</script>";
            return;
        }
        if (CardNo.Text == "[自动编号]" || CardNo.Text.Length == 0)
        {
            CardNo.Text = CommClass.GetRecordID("CW_ResLeaseCard");
        }
        string uploadFile = UtilsPage.UploadFiles();
        Dictionary<string, string> LeaseCard = new Dictionary<string, string>();
        OldCardNo.Value = CardNo.Text;
        LeaseCard.Add("CardNo", CardNo.Text);
        LeaseCard.Add("CardType", Request.QueryString["ctype"]);
        LeaseCard.Add("LeaseHolder", LeaseHolder.Text);
        LeaseCard.Add("LinkTel", LinkTel.Text);
        LeaseCard.Add("ResourceID", ResourceID.Value);
        LeaseCard.Add("ResourceName", ResourceName.Text);
        LeaseCard.Add("ResUnit", ResUnit.Text);
        LeaseCard.Add("ResAmount", ResAmount.Text);
        LeaseCard.Add("IncomeSubject", IncomeSubject.Text);
        LeaseCard.Add("PaySubject", PaySubject.Text);
        if (ResAmount.Text != OldAmount.Value)
        {
            decimal DiffValue = TypeParse.StrToDecimal(ResAmount.Text, 0) - TypeParse.StrToDecimal(OldAmount.Value, 0);
            if (Request.QueryString["ctype"] == "0")
            {
                CommClass.ExecuteSQL(string.Format("update cw_assetcard set HasAmount=HasAmount+({0}) where id='{1}'", DiffValue.ToString(), ResourceID.Value));
            }
            else
            {
                CommClass.ExecuteSQL(string.Format("update cw_rescard set HasAmount=HasAmount+({0}) where id='{1}'", DiffValue.ToString(), ResourceID.Value));
            }
            decimal hasAmount = TypeParse.StrToDecimal(HasAmount.Value, 0) - DiffValue;
            HasAmount.Value = hasAmount.ToString();
            OldAmount.Value = ResAmount.Text;
        }
        LeaseCard.Add("YearRental", YearRental.Text);
        LeaseCard.Add("PayType", PayType.SelectedValue);
        LeaseCard.Add("SumRental", SumRental.Text);
        LeaseCard.Add("NextPayDate", NextPayDate.Text);
        LeaseCard.Add("NextPayMoney", NextPayMoney.Text);
        LeaseCard.Add("StartLease", SLease.Text);
        LeaseCard.Add("EndLease", ELease.Text);
        LeaseCard.Add("Notes", Notes.Text);
        LeaseCard.Add("ModifyDate", BookDate.Text);
        if (uploadFile.Length != 0)
        {
            ShowFile.NavigateUrl = uploadFile;
            LeaseCard.Add("Appendix", uploadFile);
        }
        DelFile.Enabled = ShowFile.NavigateUrl.Length != 0;
        //合同基本信息
        LeaseCard.Add("ContractCo", ContractCo.Text);
        LeaseCard.Add("ResUnitName", ResUnitName.Text);
        LeaseCard.Add("ContractNo", ContractNo.Text);
        LeaseCard.Add("ContractDate", ContractDate.Text);
        LeaseCard.Add("ContractName", ContractName.Text);
        LeaseCard.Add("ContractType", ContractType.SelectedValue);
        LeaseCard.Add("ContractMoney", ContractMoney.Text);
        LeaseCard.Add("ContractYears", ContractYears.Text);
        LeaseCard.Add("ContractContent", ContractContent.Text);
        LeaseCard.Add("ContractNote", ContractNote.Text);
        if (CardID.Value == "000000")
        {
            CardID.Value = CommClass.GetRecordID("CW_LeaseID");
            LeaseCard.Add("ID", CardID.Value);
            LeaseCard.Add("LeaseState", "0");
            LeaseCard.Add("DoState", "0");
            LeaseCard.Add("BookTime", DateTime.Now.ToString());
            LeaseCard.Add("BookDate", BookDate.Text);
            CommClass.ExecuteSQL("CW_ResLeaseCard", LeaseCard);
            //租赁收款过程
            switch (PayType.SelectedValue)
            {
                case "0":
                    CommClass.ExecuteSQL("delete from cw_respayperiod where CardID='" + CardID.Value + "' and PayState='0'");
                    CommClass.ExecuteSQL("insert into cw_respayperiod(ID,CardID,PeriodName,PayMoney,StartPay,EndPay,PayState,DoState)values('"
                        + CommClass.GetRecordID("CW_ResPayPeriod") + "','"
                        + CardID.Value + "','"
                        + "第0001期','"
                        + YearRental.Text + "','"
                        + SLease.Text + "','"
                        + ELease.Text + "','0','0')");
                    break;
                case "1":
                case "2":
                    CommClass.ExecuteSQL("delete from cw_respayperiod where CardID='" + CardID.Value + "' and PayState='0'");
                    int i = 1;
                    string startPeriod = SLease.Text;
                    string endPeriod = string.Empty;
                    int PeriodSpan = PayType.SelectedValue == "1" ? 6 : 12;
                    DateTime PeriodDate = Convert.ToDateTime(SLease.Text);
                    DateTime endLease = Convert.ToDateTime(ELease.Text);
                    PeriodDate = PeriodDate.AddMonths(PeriodSpan);
                    endPeriod = PeriodDate.AddDays(-1).ToString("yyyy-MM-dd");
                    while (string.Compare(endPeriod, ELease.Text) < 0)
                    {
                        CommClass.ExecuteSQL("insert into cw_respayperiod(ID,CardID,PeriodName,PayMoney,StartPay,EndPay,PayState,DoState)values('"
                            + CommClass.GetRecordID("CW_ResPayPeriod") + "','"
                            + CardID.Value + "','"
                            + "第" + i.ToString("0000") + "期','"
                            + YearRental.Text + "','"
                            + startPeriod + "','"
                            + endPeriod + "','0','0')");
                        startPeriod = PeriodDate.ToString("yyyy-MM-dd");
                        PeriodDate = PeriodDate.AddMonths(PeriodSpan);
                        endPeriod = PeriodDate.AddDays(-1).ToString("yyyy-MM-dd");
                        i++;
                    }
                    PeriodDate = PeriodDate.AddMonths(0 - PeriodSpan);
                    startPeriod = PeriodDate.ToString("yyyy-MM-dd");
                    if (string.Compare(startPeriod, ELease.Text) < 0)
                    {
                        CommClass.ExecuteSQL("insert into cw_respayperiod(ID,CardID,PeriodName,PayMoney,StartPay,EndPay,PayState,DoState)values('"
                            + CommClass.GetRecordID("CW_ResPayPeriod") + "','"
                            + CardID.Value + "','"
                            + "第" + i.ToString("0000") + "期','"
                            + YearRental.Text + "','"
                            + startPeriod + "','"
                            + ELease.Text + "','0','0')");
                    }
                    break;
            }
            //写入操作日志
            CommClass.WriteCTL_Log("100018", "录入合同，编号：" + CardNo.Text);
            //--
        }
        else
        {
            CommClass.ExecuteSQL("CW_ResLeaseCard", LeaseCard, "id='" + CardID.Value + "'");
            //写入操作日志
            CommClass.WriteCTL_Log("100018", "变更合同，编号：" + CardNo.Text);
            //--
        }
        ExeScript.Text = "<script>WinClose();</script>";
    }
    protected void DelFile_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath(ShowFile.NavigateUrl);
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
        CommClass.ExecuteSQL("update cw_resleasecard set Appendix='' where id='" + CardID.Value + "'");
        ShowFile.NavigateUrl = "";
        DelFile.Enabled = false;
    }
}
