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

public partial class _QueryPage2 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionFlag"] == null)
        {
            Session["SessionFlag"] = "SessionFlag";
            Session["UnitID"] = "000000";
            Session["UnitName"] = "公共查询";
            Session["UserID"] = "000000";
            Session["RealName"] = "gk";
            Session["UserName"] = "gk";
            StringBuilder sb = new StringBuilder();
            for (int i = 100000; i < 100025; i++)
            {
                sb.AppendFormat("{0}$", i.ToString());
            }
            Session["Powers"] = sb.ToString();
            Session["MyAccount"] = "000000";
            Session["UserType"] = "0";
        }
        if (!IsPostBack)
        {
            string[] mainMenu = { "账簿查询", "凭证查询", "报表查询", "报表分析", "资产", "资源" };
            string[][][] subMenu ={
            new string[][]{
                new string[]{ "总账查询", "AccountQuery/GeneralLedger.aspx","0" },
                new string[]{ "明细分类账", "AccountQuery/DetailAccount.aspx","0" },
                new string[]{ "科目余额表", "AccountQuery/SubjectBalanceDay.aspx","0" },
                new string[]{ "应收账款查询", "AccountQuery/AccountsReceivable.aspx","0" },
                new string[]{ "应付账款查询", "AccountQuery/AccountsPayable.aspx","0" }},
            new string[][]{
                new string[]{ "凭证列表查询", "AccountQuery/VoucherList.aspx","0" },
                new string[]{ "凭证单张查询", "AccountQuery/VoucherPage.aspx","0" },
                new string[]{ "凭证打印输出", "AccountQuery/PrintVoucher.aspx","0" }},
            new string[][]{
                new string[]{ "科目余额表", "AccountQuery/SubjectBalance.aspx","1" },
                new string[]{ "收支明细表", "AccountQuery/CommReport.aspx?DesignID=000003","1" },
                new string[]{ "资产负债表", "AccountQuery/CommReport.aspx?DesignID=000004","1" },
                new string[]{ "收益分配表", "AccountQuery/IncomeDistribution.aspx","0" },
                new string[]{ "财务公开榜(月表)", "AccountQuery/CommReport.aspx?DesignID=000006","1" },
                new string[]{ "财务公开榜(季表)", "AccountQuery/QuarterReport.aspx","1" },
                new string[]{ "内部往来余额表", "AccountQuery/InternalDemand.aspx","1" },
                new string[]{ "收支逐笔公开榜", "AccountQuery/EachAccPublish.aspx","1" }},
            new string[][]{
                new string[]{ "村级收入情况", "AccountQuery/Analysis04.aspx?QType=0","0" },
                new string[]{ "村级支出情况", "AccountQuery/Analysis04.aspx?QType=1","0" },
                new string[]{ "村级福利费收入表", "AccountQuery/Analysis04.aspx?QType=2","0" },
                new string[]{ "村级福利费支出表", "AccountQuery/Analysis04.aspx?QType=3","0" },
                new string[]{ "财务收支分析表", "AccountQuery/Analysis01.aspx","0" },
                new string[]{ "福利费收支分析表", "AccountQuery/Analysis02.aspx","0" },
                new string[]{ "资产负债分析表", "AccountQuery/Analysis03.aspx","0" }},
            new string[][]{
                new string[]{ "资产明细表", "FixedAsset/FixedAssetDetail.aspx","1" },
                new string[]{ "资产明细账", "FixedAsset/FADetailAccount.aspx","0" }},
            new string[][]{
                new string[]{ "资源明细表", "ResManage/ResourceDetail.aspx","1" }}
            };
            int t = TypeParse.StrToInt(Request.QueryString["t"], 9);
            if (t == 9)
            {
                for (int i = 0; i < mainMenu.Length; i++)
                {
                    for (int k = 0; k < subMenu[i].Length; k++)
                    {
                        if (subMenu[i][k][2] == "1")
                        {
                            TreeNode node = new TreeNode();
                            node.Text = "<a href=\"javascript:OpenUrl('" + subMenu[i][k][1] + "')\">" + subMenu[i][k][0] + "</a>";
                            TreeView1.Nodes[0].ChildNodes.Add(node);
                        }
                    }
                }
            }
            else
            {
                gongkai0.Visible = false;
                gongkai1.Visible = true;
                PageClass.ExcuteScript(this.Page, "togglem(1);document.getElementById('ctl00_LeftFrame1_UT').value='1';");
                AName.Text = UserInfo.AccountName;
                string AccountDate = MainClass.GetTableValue("cw_account", "accountdate", "id='" + UserInfo.AccountID + "'");
                if (AccountDate.Length > 0)
                {
                    Session["isStartAccount"] = "Yes";
                    PageClass.ExcuteScript(this.Page, "$('mFrame').src='HomePage.aspx';");
                }
                else
                {
                    Session["isStartAccount"] = null;
                    PageClass.ExcuteScript(this.Page, "$('mFrame').src='ErrorTip.aspx?errTip=" + Server.UrlEncode("当前账套尚未启用，请联系相应操作员启用账套！") + "'");
                }
                for (int k = 0; k < subMenu[t].Length; k++)
                {
                    if (subMenu[t][k][2] == "1")
                    {
                        TreeNode node = new TreeNode();
                        node.Text = "<a href=\"javascript:OpenUrl('" + subMenu[t][k][1] + "')\">" + subMenu[t][k][0] + "</a>";
                        TreeView1.Nodes[0].ChildNodes.Add(node);
                    }
                }
            }
            TreeView1.ExpandAll();
        }
        LeftFrame1.AName = AName;
    }
}
