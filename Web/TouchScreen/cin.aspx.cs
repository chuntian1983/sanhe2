using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace yuxi
{
    public partial class cin : System.Web.UI.Page
    {
        public string strurl = "#";
        string[][] str = new string[][]{
                //new string[]{ "总账查询", "AccountQuery/GeneralLedger.aspx","0" },
                //new string[]{ "明细分类账", "AccountQuery/DetailAccount.aspx","0" },
                //new string[]{ "科目余额表", "AccountQuery/SubjectBalanceDay.aspx","0" },
                new string[]{ "应收账款查询", "AccountQuery/AccountsReceivable.aspx","0" },
                new string[]{ "应付账款查询", "AccountQuery/AccountsPayable.aspx","0" }};
        string[][] str1 = new string[][]{
                new string[]{ "凭证列表查询", "AccountQuery/VoucherList.aspx","0" },
                new string[]{ "凭证单张查询", "AccountQuery/VoucherPage.aspx","0" },
                new string[]{ "凭证打印输出", "AccountQuery/PrintVoucher.aspx","0" }};
        string[][] str2 = new string[][]{
                //new string[]{ "科目余额表", "AccountQuery/SubjectBalance.aspx","0" },
                //new string[]{ "收支明细表", "AccountQuery/CommReport.aspx?DesignID=000003","0" },
                //new string[]{ "资产负债表", "AccountQuery/CommReport.aspx?DesignID=000004","0" },
                //new string[]{ "收益分配表", "AccountQuery/IncomeDistribution.aspx","0" },
                new string[]{ "财务公开榜(月表)", "AccountQuery/CommReport.aspx?DesignID=000006","0" },
                new string[]{ "财务公开榜(季表)", "AccountQuery/QuarterReport.aspx","0" },
                //new string[]{ "内部往来余额表", "AccountQuery/InternalDemand.aspx","0" },
                new string[]{ "收支逐笔公开榜", "AccountQuery/EachAccPublish.aspx","0" }};
        string[][] str3 = new string[][]{
                new string[]{ "村级收入情况", "AccountQuery/Analysis04.aspx?QType=0","0" },
                new string[]{ "村级支出情况", "AccountQuery/Analysis04.aspx?QType=1","0" },
                //new string[]{ "村级福利费收入表", "AccountQuery/Analysis04.aspx?QType=2","0" },
                //new string[]{ "村级福利费支出表", "AccountQuery/Analysis04.aspx?QType=3","0" },
                //new string[]{ "财务收支分析表", "AccountQuery/Analysis01.aspx","0" },
                //new string[]{ "福利费收支分析表", "AccountQuery/Analysis02.aspx","0" },
                //new string[]{ "资产负债分析表", "AccountQuery/Analysis03.aspx","0" }
        };
        string[][] str4 = new string[][] {
                new string[] { "支农惠农", "view/GongKai/viewlist.aspx" } };
        string[][] str5 = new string[][]{
                new string[]{ "资产明细表", "FixedAsset/FixedAssetDetail.aspx","0" },
                new string[]{ "资产明细账", "FixedAsset/FADetailAccount.aspx","0" },
                new string[]{ "资源明细表", "ResManage/ResourceDetail.aspx","0" }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string kid;
                string ca;
                if (Request.QueryString["kid"] != null)
                {
                    kid = Request.QueryString["kid"];
                    ca = Request.QueryString["case"];
                    switch (ca)
                    {
                        case "1":
                            Bindview(str);
                            break;
                        case "2":
                            Bindview(str1);
                            break;
                        case "3":
                            Bindview(str2);
                            break;
                        case "4":
                            Bindview(str3);
                            break;
                        case "5":
                            Bindview(str4);
                            break;
                        case "6":
                            Bindview(str5);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        protected void Bindview(string[][] strd)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            for (int i = 0; i < strd.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = strd[i][1].ToString();
                dr["name"] = strd[i][0].ToString();
                dt.Rows.Add(dr);
            }
            this.DataList1.DataSource = dt;
            this.DataList1.DataBind();
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "view")
            {
                string id = e.CommandArgument.ToString();
                id = ValidateClass.ip + id;
                if (Request.QueryString["kid"] != null)
                {
                    string kid = Request.QueryString["kid"];
                    strurl = id;
                }
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            if (Request.QueryString["kid"] != null)
            {
                string kid = Request.QueryString["kid"];
                Response.Redirect("cunnew.aspx?kid=" + kid + "");
            }
        }
    }
}