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

public partial class FixedAsset_AssetCleanup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000013")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            CardID.Attributes.Add("readonly", "readonly");
            AssetNo.Attributes.Add("readonly", "readonly");
            AssetName.Attributes.Add("readonly", "readonly");
            CDate.Attributes.Add("readonly", "readonly");
            LSubject.Attributes.Add("readonly", "readonly");
            CRSubject.Attributes.Add("readonly", "readonly");
            CCSubject.Attributes.Add("readonly", "readonly");
            CRMoney.Attributes.Add("onkeypress", "return ValidateNumber(this.value);");
            CCMoney.Attributes.Add("onkeypress", "return ValidateNumber(this.value);");
            LSubject.Attributes.Add("onclick", "SelectItem(0)");
            CRSubject.Attributes.Add("onclick", "VSelSubject(1)");
            CCSubject.Attributes.Add("onclick", "VSelSubject(2)");
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            CDate.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].CDate,'yyyy-mm-dd')");
            DataTable DiType = CommClass.GetDataTable("select id,tname from cw_ditype where ttype='2' order by id");
            foreach (DataRow row in DiType.Rows)
            {
                CType.Items.Add(new ListItem(row["tname"].ToString(), row["id"].ToString()));
            }
            CDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DataRow asset = CommClass.GetDataRow("select * from cw_assetcard where id='" + Request.QueryString["id"] + "'");
            CardID.Text = asset["cardid"].ToString();
            AssetNo.Text = asset["assetno"].ToString();
            AssetName.Text = asset["assetname"].ToString();
            string bdate = asset["BookDate"].ToString().Substring(0, 7);
            string adate = MainClass.GetAccountDate().ToString("yyyy-MM");
            if (asset["DeprMethod"].ToString() == "1" && bdate != adate)
            {
                Button3.Enabled = false;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        CommClass.ExecuteSQL("update cw_assetcard set "
            + "CDate='" + CDate.Text + "',"
            + "CType='" + CType.SelectedValue + "',"
            + "LSubject='" + LSubject.Text + "',"
            + "CRMoney='" + CRMoney.Text + "',"
            + "CRSubject='" + CRSubject.Text + "',"
            + "CCMoney='" + CCMoney.Text + "',"
            + "CCSubject='" + CCSubject.Text + "',"
            + "CNotes='" + CNotes.Text + "',"
            + "CleanDate='" + MainClass.GetAccountDate().ToString("yyyy-MM-dd") + "',"
            + "CVoucher='0',AssetState='2' where id='" + Request.QueryString["id"] + "'");
        ExeScript.Text = "<script>alert('资产清理成功！');location.href='FixedAssetList.aspx';</script>";
        //写入操作日志
        CommClass.WriteCTL_Log("100018", "清理资产编号：" + CardID.Text);
        //--
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        CommClass.ExecuteSQL("delete from cw_assetcard where id='" + Request.QueryString["id"] + "'");
        ExeScript.Text = "<script>alert('资产删除成功！');location.href='FixedAssetList.aspx';</script>";
        //写入操作日志
        CommClass.WriteCTL_Log("100018", "删除资产编号：" + CardID.Text);
        //--
    }
}
