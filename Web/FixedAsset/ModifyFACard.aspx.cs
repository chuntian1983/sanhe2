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

public partial class FixedAsset_ModifyFACard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000013")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            AccountDate.Value = MainClass.GetAccountDate().ToString("yyyy-MM-dd");
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
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            //设置控件值为空时自动设为默认值
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
            InitWebControl();
            InitModifyLog();
        }
    }
    protected void InitWebControl()
    {
        DataRow row = CommClass.GetDataRow("select * from cw_assetcard where id='" + Request.QueryString["id"] + "'");
        OldCardID.Value = row["CardID"].ToString();
        CardID.Text = row["CardID"].ToString();
        AssetNo.Text = row["AssetNo"].ToString();
        AssetName.Text = row["AssetName"].ToString();
        ClassID.Text = row["ClassID"].ToString();
        CName.Text = row["CName"].ToString();
        AssetModel.Text = row["AssetModel"].ToString();
        DeptName.Text = row["DeptName"].ToString();
        AddType.Text = row["AddType"].ToString();
        Depositary.Text = row["Depositary"].ToString();
        UseState.Text = row["UseState"].ToString();
        int dotpos = row["uselife"].ToString().IndexOf(".");
        if (dotpos > 0)
        {
            UseLife0.Text = row["uselife"].ToString().Substring(0, dotpos);
            UseLife1.Text = row["uselife"].ToString().Substring(dotpos + 1);
        }
        DeprMethod.Text = row["DeprMethod"].ToString();
        SUseDate.Text = row["SUseDate"].ToString();
        UsedMonths.Text = row["UsedMonths"].ToString();
        CurrencyType.Text = row["CurrencyType"].ToString();
        OldPrice.Text = row["OldPrice"].ToString();
        JingCZL.Text = row["JingCZL"].ToString();
        JingCZ.Text = row["JingCZ"].ToString();
        ZheJiu.Text = row["ZheJiu"].ToString();
        MonthZJL.Text = row["MonthZJL"].ToString();
        MonthZJE.Text = row["MonthZJE"].ToString();
        NewPrice.Text = row["NewPrice"].ToString();
        DeprSubject.Text = row["DeprSubject"].ToString();
        AssetItem.Text = row["AssetItem"].ToString();
        AUnit.Text = row["AUnit"].ToString();
        AAmount.Text = row["AAmount"].ToString();
        APrice.Text = row["APrice"].ToString();
        AssetAdmin.Text = row["AssetAdmin"].ToString();
        BookDate.Value = row["BookDate"].ToString();
        AssetState.Value = row["AssetState"].ToString();
        DeprState.Value = row["DeprState"].ToString();
        CVoucher.Value = row["CVoucher"].ToString();
        if (BookDate.Value.StartsWith(AccountDate.Value.Substring(0, 7)))
        {
            IsNewCard.Value = "1";
        }
        else
        {
            IsNewCard.Value = "0";
            SUseDate.Attributes.Remove("onclick");
        }
        ShowFile.NavigateUrl = row["APicture"].ToString();
        DelFile.Enabled = ShowFile.NavigateUrl.Length != 0;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CardID.Text != OldCardID.Value && CommClass.CheckExist("cw_assetcard", "CardID='" + CardID.Text + "'"))
        {
            ExeScript.Text = "<script>alert('该卡片编号[" + CardID.Text + "]已存在！');</script>";
            return;
        }
        string UpdateStr = string.Empty;
        if (IsNewCard.Value == "1")
        {
            UpdateStr = ",OldPrice0='" + OldPrice.Text + "',ZheJiu0='" + ZheJiu.Text + "'";
        }
        decimal _NewPrice = decimal.Parse(NewPrice.Text);
        decimal _JingCZ = decimal.Parse(JingCZ.Text);
        decimal _UsedMonths = decimal.Parse(UsedMonths.Text);
        decimal _UseLife = decimal.Parse(UseLife0.Text) * 12 + decimal.Parse(UseLife1.SelectedValue);
        if (DeprState.Value == "1" && CVoucher.Value == "1")
        {
            string YearMonth = MainClass.GetAccountDate().ToString("yyyy年MM月");
            CommClass.ExecuteSQL("delete from cw_assetda where cardid='" + Request.QueryString["id"] + "' and VoucherDate like '" + YearMonth + "%'");
        }
        if (DeprMethod.SelectedValue == "1")
        {
            if (_UseLife <= _UsedMonths || _NewPrice <= _JingCZ)
            {
                UpdateStr += ",DeprState='1',AssetState='1',CVoucher='0',ThisZJ=0";
                DeprState.Value = "1";
                AssetState.Value = "1";
            }
            else
            {
                UpdateStr += ",DeprState='0',AssetState='0',CVoucher='0',ThisZJ=0";
                DeprState.Value = "0";
                AssetState.Value = "0";
            }
        }
        CVoucher.Value = "0";
        string uploadFile = UtilsPage.UploadFiles();
        if (uploadFile.Length == 0)
        {
            uploadFile = ShowFile.NavigateUrl;
        }
        else
        {
            ShowFile.NavigateUrl = uploadFile;
        }
        CommClass.ExecuteSQL("update cw_assetcard set "
            + "CardID='" + CardID.Text + "',"
            + "AssetNo='" + AssetNo.Text + "',"
            + "AssetName='" + AssetName.Text + "',"
            + "ClassID='" + ClassID.Text + "',"
            + "CName='" + CName.Text + "',"
            + "AssetModel='" + AssetModel.Text + "',"
            + "DeptName='" + DeptName.Text + "',"
            + "AddType='" + AddType.SelectedValue + "',"
            + "Depositary='" + Depositary.Text + "',"
            + "UseState='" + UseState.SelectedValue + "',"
            + "UseLife='" + UseLife0.Text + "." + UseLife1.SelectedValue + "',"
            + "DeprMethod='" + DeprMethod.SelectedValue + "',"
            + "SUseDate='" + SUseDate.Text + "',"
            + "UsedMonths='" + UsedMonths.Text + "',"
            + "CurrencyType='" + CurrencyType.Text + "',"
            + "OldPrice='" + OldPrice.Text + "',"
            + "JingCZL='" + JingCZL.Text + "',"
            + "JingCZ='" + JingCZ.Text + "',"
            + "ZheJiu='" + ZheJiu.Text + "',"
            + "MonthZJL='" + MonthZJL.Text + "',"
            + "MonthZJE='" + MonthZJE.Text + "',"
            + "NewPrice='" + NewPrice.Text + "',"
            + "DeprSubject='" + DeprSubject.Text + "',"
            + "AssetItem='" + AssetItem.Text + "',"
            + "AUnit='" + AUnit.Text + "',"
            + "AAmount='" + AAmount.Text + "',"
            + "APrice='" + APrice.Text + "',"
            + "AssetAdmin='" + AssetAdmin.Text + "',"
            + "APicture='" + uploadFile + "'"
            + UpdateStr + " where id='" + Request.QueryString["id"] + "'");
        DelFile.Enabled = ShowFile.NavigateUrl.Length != 0;
        OldCardID.Value = CardID.Text;
        ExeScript.Text = "<script>alert('固定资产卡片保存成功！');</script>";
        //写入操作日志
        CommClass.WriteCTL_Log("100018", "资产变更，编号：" + CardID.Text);
        //变更记录
        if (ChangeType.SelectedValue != "选择项目" && ChangeNotes.Text.Length > 0)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("id", CommClass.GetRecordID("CW_Logs"));
            dic.Add("logcontent", ChangeNotes.Text);
            dic.Add("loguser", UserInfo.UserName);
            dic.Add("logname", UserInfo.RealName);
            dic.Add("loguid", "XXXXXX");
            dic.Add("logpid", "XXXXXX");
            dic.Add("logtime", DateTime.Now.ToString());
            dic.Add("logdefine1", "000030-" + Request.QueryString["id"]);
            dic.Add("logdefine2", HttpContext.Current.Request.UserHostAddress);
            dic.Add("logdefine3", ChangeType.SelectedValue);
            CommClass.ExecuteSQL("cw_logs", dic);
            ChangeType.SelectedIndex = 0;
            ChangeNotes.Text = "";
        }
        InitModifyLog();
    }
    protected void DelFile_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath(ShowFile.NavigateUrl);
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
        CommClass.ExecuteSQL("update cw_assetcard set APicture='' where id='" + Request.QueryString["id"] + "'");
        DelFile.Enabled = false;
        ShowFile.NavigateUrl = "";
        InitModifyLog();
    }
    protected void InitModifyLog()
    {
        DataSet ds = CommClass.GetDataSet(string.Concat("select * from cw_logs ", "where logdefine1='000030-", Request.QueryString["id"], "' order by id desc"));
        if (ds.Tables[0].Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
        }
    }
}
