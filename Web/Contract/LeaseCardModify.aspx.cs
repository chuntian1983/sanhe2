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

public partial class Contract_LeaseCardModify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            DelFile.Attributes.Add("onclick", "return confirm('您确定需要删除附件吗？')");
            ModifyDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ModifyDate.Attributes.Add("readonly", "readonly");
            ModifyDate.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].ModifyDate,'yyyy-mm-dd')");
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
            ResUnitName.Attributes.Add("readonly", "readonly");
            ResUnitName.Text = UserInfo.AccountName;
            ContractDate.Attributes.Add("readonly", "readonly");
            ContractDate.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].ContractDate,'yyyy-mm-dd')");
            UtilsPage.SetTextBoxAutoValue(ResAmount, "0");
            UtilsPage.SetTextBoxAutoValue(YearRental, "0");
            UtilsPage.SetTextBoxAutoValue(SumRental, "0");
            UtilsPage.SetTextBoxAutoValue(NextPayMoney, "0");
            UtilsPage.SetTextBoxAutoValue(ContractMoney, "0");
            UtilsPage.SetTextBoxAutoValue(ContractYears, "0");
            if (string.IsNullOrEmpty(Request.QueryString["id"]) == false)
            {
                CardID.Value = Request.QueryString["id"];
                InitWebControl();
            }
            if (Request.QueryString["ctype"] == "0")
            {
                TD_Name.InnerText = "租赁资产：";
            }
            else if (Request.QueryString["ctype"] == "1")
            {
                TD_Name.InnerText = "租赁资源：";
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
        }
    }
    private void InitWebControl()
    {
        //初始化控件值
        DataRow row = CommClass.GetDataRow("select * from cw_resleasecard where id='" + CardID.Value + "'");
        OldCardNo.Value = row["CardNo"].ToString();
        CardNo.Text = row["CardNo"].ToString();
        ResourceID.Value = row["ResourceID"].ToString();
        if (Request.QueryString["ctype"] == "0")
        {
            HasAmount.Value = CommClass.GetTableValue("cw_assetcard", "AAmount-HasAmount", string.Format("id='{0}'", ResourceID.Value));
        }
        else
        {
            HasAmount.Value = CommClass.GetTableValue("cw_rescard", "ResAmount-HasAmount", string.Format("id='{0}'", ResourceID.Value));
        }
        ResourceName.Text = row["ResourceName"].ToString();
        ResUnit.Text = row["ResUnit"].ToString();
        ResAmount.Text = row["ResAmount"].ToString();
        OldAmount.Value = ResAmount.Text;
        SumRental.Text = row["SumRental"].ToString();
        YearRental.Text = row["YearRental"].ToString();
        PayType.Text = row["PayType"].ToString();
        IncomeSubject.Text = row["IncomeSubject"].ToString();
        PaySubject.Text = row["PaySubject"].ToString();
        NextPayDate.Text = row["NextPayDate"].ToString();
        NextPayMoney.Text = row["NextPayMoney"].ToString();
        SLease.Text = row["StartLease"].ToString();
        ELease.Text = row["EndLease"].ToString();
        hidELease.Value = ELease.Text;
        LeaseHolder.Text = row["LeaseHolder"].ToString();
        LinkTel.Text = row["LinkTel"].ToString();
        Notes.Text = row["Notes"].ToString();
        ContractCo.Text = row["ContractCo"].ToString();
        ResUnitName.Text = row["ResUnitName"].ToString();
        ContractNo.Text = row["ContractNo"].ToString();
        ContractDate.Text = row["ContractDate"].ToString();
        ContractType.Text = row["ContractType"].ToString();
        ContractName.Text = row["ContractName"].ToString();
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
        LeaseCard.Add("CardNo", CardNo.Text);
        LeaseCard.Add("LeaseHolder", LeaseHolder.Text);
        LeaseCard.Add("LinkTel", LinkTel.Text);
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
        LeaseCard.Add("ModifyDate", ModifyDate.Text);
        if (uploadFile.Length != 0)
        {
            ShowFile.NavigateUrl = uploadFile;
            LeaseCard.Add("Appendix", uploadFile);
        }
        //合同基本信息
        LeaseCard.Add("ContractCo", ContractCo.Text);
        LeaseCard.Add("ResUnitName", ResUnitName.Text);
        LeaseCard.Add("ContractNo", ContractNo.Text);
        LeaseCard.Add("ContractName", ContractName.Text);
        LeaseCard.Add("ContractDate", ContractDate.Text);
        LeaseCard.Add("ContractType", ContractType.SelectedValue);
        LeaseCard.Add("ContractMoney", ContractMoney.Text);
        LeaseCard.Add("ContractYears", ContractYears.Text);
        LeaseCard.Add("ContractContent", ContractContent.Text);
        LeaseCard.Add("ContractNote", ContractNote.Text);
        //恢复到期处理状态
        if (hidELease.Value != ELease.Text)
        {
            LeaseCard.Add("DoState", "0");
        }
        CommClass.ExecuteSQL("CW_ResLeaseCard", LeaseCard, "id='" + CardID.Value + "'");
        DelFile.Enabled = ShowFile.NavigateUrl.Length != 0;
        //写入操作日志
        CommClass.WriteCTL_Log("100018", "变更合同，编号：" + CardNo.Text);
        //--
        PageClass.ExcuteScript(this.Page, "TableSelect($('ShowFlag').value);alert('合同卡片变更成功！');");
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
