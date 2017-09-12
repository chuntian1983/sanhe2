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

public partial class FixedAsset_PrintFACard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["aid"]))
        {
            if (!PageClass.CheckVisitQuot("000013")) { return; }
        }
        else
        {
            UserInfo.CheckSession2();
            UserInfo.AccountID = Request.QueryString["aid"];
        }
        if (!IsPostBack)
        {
            PrintDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DataRow row = CommClass.GetDataRow("select * from cw_assetcard where id='" + Request.QueryString["id"] + "'");
            CardID.Text = row["CardID"].ToString();
            AssetNo.Text = row["AssetNo"].ToString();
            AssetName.Text = row["AssetName"].ToString();
            ClassID.Text = row["ClassID"].ToString();
            CName.Text = row["CName"].ToString();
            AssetModel.Text = row["AssetModel"].ToString();
            string _DeptName = row["DeptName"].ToString();
            DeptName.Text = _DeptName.Substring(_DeptName.IndexOf(".") + 1);
            AddType.Text = CommClass.GetFieldFromID(row["AddType"].ToString(), "tname", "cw_ditype");
            if (AddType.Text == "NoDataItem")
            {
                AddType.Text = "";
            }
            Depositary.Text = row["Depositary"].ToString();
            int UState = int.Parse(row["UseState"].ToString()) - 101;
            string[] _UseState ={ "使用中", "未使用", "不需要" };
            UseState.Text = _UseState[UState];
            UseLife.Text = row["UseLife"].ToString().Replace(".", "年") + "月";
            if (row["DeprMethod"].ToString() == "1")
            {
                DeprMethod.Text = "平均年限法";
            }
            else
            {
                DeprMethod.Text = "不提折旧";
            }
            SUseDate.Text = row["SUseDate"].ToString();
            UsedMonths.Text = row["UsedMonths"].ToString();
            CurrencyType.Text = row["CurrencyType"].ToString();
            OldPrice.Text = TypeParse.StrToDecimal(row["OldPrice"].ToString(), 0).ToString("#,##0.00");
            JingCZL.Text = row["JingCZL"].ToString() + "%";
            JingCZ.Text = TypeParse.StrToDecimal(row["JingCZ"].ToString(), 0).ToString("#,##0.00");
            ZheJiu.Text = TypeParse.StrToDecimal(row["ZheJiu"].ToString(), 0).ToString("#,##0.00");
            MonthZJL.Text = row["MonthZJL"].ToString();
            MonthZJE.Text = row["MonthZJE"].ToString();
            NewPrice.Text = TypeParse.StrToDecimal(row["NewPrice"].ToString(), 0).ToString("#,##0.00");
            DeprSubject.Text = row["DeprSubject"].ToString();
            AssetItem.Text = row["AssetItem"].ToString();
            AUnit.Text = row["AUnit"].ToString();
            AAmount.Text = TypeParse.StrToDecimal(row["AAmount"].ToString(), 0).ToString("#,##0.00");
            APrice.Text = TypeParse.StrToDecimal(row["APrice"].ToString(), 0).ToString("#,##0.00");
            BookMan.Text = row["BookMan"].ToString();
            BookTime.Text = row["BookTime"].ToString();
            APicture.ImageUrl = row["APicture"].ToString();
            if (APicture.ImageUrl.Length == 0)
            {
                DivAPicture.Visible = false;
            }
        }
    }
}
