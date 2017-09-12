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

public partial class Contract_LeaseCardShow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession2();
        if (!IsPostBack)
        {
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
                TdAmount.Style["display"] = "none";
            }
            InitWebControl();
        }
    }
    private void InitWebControl()
    {
        //初始化控件值
        DataRow row = CommClass.GetDataRow("select * from cw_resleasecard where id='" + Request.QueryString["id"] + "'");
        CardNo.Text = row["CardNo"].ToString();
        BookDate.Text = row["BookDate"].ToString();
        ResourceName.Text = row["ResourceName"].ToString();
        ResUnit.Text = row["ResUnit"].ToString();
        ResAmount.Text = row["ResAmount"].ToString();
        SumRental.Text = row["SumRental"].ToString();
        YearRental.Text = row["YearRental"].ToString();
        PayType.Text = row["PayType"].ToString();
        switch (PayType.Text)
        {
            case "0":
                PayType.Text = "一次付清";
                break;
            case "1":
                PayType.Text = "半年一次";
                break;
            case "2":
                PayType.Text = "一年一次";
                break;
            case "8":
                PayType.Text = "自定义";
                break;
        }
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
        ContractDate.Text = row["ContractDate"].ToString();
        ContractName.Text = row["ContractName"].ToString();
        ContractType.Text = row["ContractType"].ToString();
        ContractMoney.Text = row["ContractMoney"].ToString();
        ContractYears.Text = row["ContractYears"].ToString();
        ContractContent.Text = row["ContractContent"].ToString();
        ContractNote.Text = row["ContractNote"].ToString();
        APicture.ImageUrl = row["Appendix"].ToString();
        if (APicture.ImageUrl.Length == 0)
        {
            DivAPicture.Visible = false;
        }
    }
}
