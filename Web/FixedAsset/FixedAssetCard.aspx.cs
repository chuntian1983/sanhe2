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

public partial class FixedAsset_FixedAssetCard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000013")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            DateTime accountDate = MainClass.GetAccountDate();
            AccountDate.Value = accountDate.ToString("yyyy-MM-dd");
            ClassID.Attributes.Add("readonly", "readonly");
            CName.Attributes.Add("readonly", "readonly");
            DeptName.Attributes.Add("readonly", "readonly");
            SUseDate.Attributes.Add("readonly", "readonly");
            UsedMonths.Attributes.Add("readonly", "readonly");
            CurrencyType.Attributes.Add("readonly", "readonly");
            MonthZJL.Attributes.Add("readonly", "readonly");
            MonthZJE.Attributes.Add("readonly", "readonly");
            DeprSubject.Attributes.Add("readonly", "readonly");
            OldPrice.Attributes.Add("onkeyup", "ChangeOldPrice()");
            ZheJiu.Attributes.Add("onkeyup", "ChangeZheJiu()");
            NewPrice.Attributes.Add("onkeyup", "ChangeNewPrice()");
            JingCZL.Attributes.Add("onkeyup", "ChangeJingCZL()");
            JingCZ.Attributes.Add("onkeyup", "ChangeJingCZ()");
            UseLife0.Attributes.Add("onkeyup", "ChangeUseLife()");
            UsedMonths.Attributes.Add("onkeyup", "ChangeUsedMonths()");
            AAmount.Attributes.Add("onkeyup", "ChangeAAmount()");
            OldPrice.Attributes.Add("onkeypress", "return ValidateNumber(this.value);");
            ZheJiu.Attributes.Add("onkeypress", "return ValidateNumber(this.value);");
            NewPrice.Attributes.Add("onkeypress", "return ValidateNumber(this.value);");
            JingCZL.Attributes.Add("onkeypress", "return ValidateNumber(this.value);");
            JingCZ.Attributes.Add("onkeypress", "return ValidateNumber(this.value);");
            UseLife0.Attributes.Add("onkeypress", "return (event.keyCode>=48&&event.keyCode<=57);");
            UsedMonths.Attributes.Add("onkeypress", "return (event.keyCode>=48&&event.keyCode<=57);");
            AAmount.Attributes.Add("onkeypress", "return (event.keyCode>=48&&event.keyCode<=57)||event.keyCode==46;");
            APrice.Attributes.Add("onkeypress", "return (event.keyCode>=48&&event.keyCode<=57)||event.keyCode==46;");
            UseLife1.Attributes.Add("onchange", "ChangeUseLife()");
            UseState.Attributes.Add("onchange", "ChangeMonthZJ()");
            DeprMethod.Attributes.Add("onchange", "ChangeMonthZJ()");
            SUseDate.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].SUseDate,'yyyy-mm-dd')");
            ClassID.Attributes.Add("onclick", "SelectItem(0)");
            CName.Attributes.Add("onclick", "SelectItem(0)");
            DeptName.Attributes.Add("onclick", "SelectItem(1)");
            DeprSubject.Attributes.Add("onclick", "SelectItem(2)");
            //设置控件值为空时自动设为默认值
            UtilsPage.SetTextBoxAutoValue(CardID);
            UtilsPage.SetTextBoxAutoValue(AssetNo);
            UtilsPage.SetTextBoxAutoValue(UseLife0, "0");
            UtilsPage.SetTextBoxAutoValue(OldPrice, "0");
            UtilsPage.SetTextBoxAutoValue(ZheJiu, "0");
            UtilsPage.SetTextBoxAutoValue(NewPrice, "0");
            UtilsPage.SetTextBoxAutoValue(JingCZL, "0");
            UtilsPage.SetTextBoxAutoValue(JingCZ, "0");
            UtilsPage.SetTextBoxAutoValue(AAmount, "0");
            UtilsPage.SetTextBoxAutoValue(APrice, "0");
            //--
            DataSet ds = CommClass.GetDataSet("select id,tname from cw_ditype where ttype='1' order by id");
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                AddType.Items.Add(new ListItem(row["tname"].ToString(), row["id"].ToString()));
            }
            string RealLastCarry = MainClass.GetFieldFromID(UserInfo.AccountID, "RealLastCarry", "cw_account");
            if (string.Compare(RealLastCarry, accountDate.ToString("yyyy年MM月")) >= 0)
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Attributes.Add("onclick", "return CheckSubmit();");
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CommClass.CheckExist("cw_assetcard", "CardID='" + CardID.Text + "'"))
        {
            ExeScript.Text = "<script>alert('该卡片编号[" + CardID.Text + "]已存在！');</script>";
            return;
        }
        if (CardID.Text == "[自动编号]" || CardID.Text.Length == 0)
        {
            CardID.Text = CommClass.GetRecordID("CW_AssetCard");
        }
        if (AssetNo.Text == "[自动编号]" || AssetNo.Text.Length == 0)
        {
            AssetNo.Text = CommClass.GetRecordID("CW_AssetNo");
        }
        string deprState = string.Empty;
        string assetState = string.Empty;
        decimal _NewPrice = decimal.Parse(NewPrice.Text);
        decimal _JingCZ = decimal.Parse(JingCZ.Text);
        decimal _UsedMonths = decimal.Parse(UsedMonths.Text);
        decimal _UseLife = decimal.Parse(UseLife0.Text) * 12 + decimal.Parse(UseLife1.SelectedValue);
        if (_UseLife <= _UsedMonths || _NewPrice <= _JingCZ)
        {
            deprState = "1";
            assetState = "1";
        }
        else
        {
            deprState = "0";
            assetState = "0";
        }
        string uploadFile = UtilsPage.UploadFiles();
        CommClass.ExecuteSQL("insert into cw_assetcard(ID,CardID,AssetNo,AssetName,ClassID,CName,AssetModel,DeptName,AddType,Depositary,UseState,"
            + "UseLife,DeprMethod,SUseDate,UsedMonths,CurrencyType,OldPrice,OldPrice0,JingCZL,JingCZ,ZheJiu,ZheJiu0,MonthZJL,MonthZJE,NewPrice,DeprSubject,AssetItem,"
            + "AUnit,AAmount,APrice,APicture,AssetAdmin,DeprState,AssetState,CVoucher,BookTime,BookDate,BookMan)values('"
            + CommClass.GetRecordID("CW_AssetID") + "','"
            + CardID.Text + "','"
            + AssetNo.Text + "','"
            + AssetName.Text + "','"
            + ClassID.Text + "','"
            + CName.Text + "','"
            + AssetModel.Text + "','"
            + DeptName.Text + "','"
            + AddType.SelectedValue + "','"
            + Depositary.Text + "','"
            + UseState.SelectedValue + "','"
            + UseLife0.Text + "." + UseLife1.SelectedValue + "','"
            + DeprMethod.SelectedValue + "','"
            + SUseDate.Text + "','"
            + UsedMonths.Text + "','"
            + CurrencyType.Text + "','"
            + OldPrice.Text + "','"
            + OldPrice.Text + "','"
            + JingCZL.Text + "','"
            + JingCZ.Text + "','"
            + ZheJiu.Text + "','"
            + ZheJiu.Text + "','"
            + MonthZJL.Text + "','"
            + MonthZJE.Text + "','"
            + NewPrice.Text + "','"
            + DeprSubject.Text + "','"
            + AssetItem.Text + "','"
            + AUnit.Text + "','"
            + AAmount.Text + "','"
            + APrice.Text + "','"
            + uploadFile + "','"
            + AssetAdmin.Text + "','"
            + deprState + "','"
            + assetState + "','"
            + "0','"
            + DateTime.Now.ToString("yyyy-MM-dd") + "','"
            + AccountDate.Value + "','"
            + Session["RealName"].ToString() + "')");
        ExeScript.Text = "<script>alert('固定资产卡片录入成功！');location.href='FixedAssetCard.aspx';</script>";
        //写入操作日志
        CommClass.WriteCTL_Log("100018", "录入资产卡片，编号：" + CardID.Text);
        //--
    }
}
