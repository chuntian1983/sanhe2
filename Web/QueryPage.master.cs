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

public partial class _QueryPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        string[] mainMenu ={ "账簿查询", "凭证查询", "报表查询", "报表分析" };
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
                new string[]{ "科目余额表", "AccountQuery/SubjectBalance.aspx","0" },
                new string[]{ "收支明细表", "AccountQuery/CommReport.aspx?DesignID=000003","0" },
                new string[]{ "资产负债表", "AccountQuery/CommReport.aspx?DesignID=000004","0" },
                new string[]{ "收益分配表", "AccountQuery/IncomeDistribution.aspx","0" },
                new string[]{ "财务公开榜(月表)", "AccountQuery/CommReport.aspx?DesignID=000006","0" },
                new string[]{ "财务公开榜(季表)", "AccountQuery/QuarterReport.aspx","0" },
                new string[]{ "内部往来余额表", "AccountQuery/InternalDemand.aspx","0" },
                new string[]{ "收支逐笔公开榜", "AccountQuery/EachAccPublish.aspx","0" }},
            new string[][]{
                new string[]{ "村级收入情况", "AccountQuery/Analysis04.aspx?QType=0","0" },
                new string[]{ "村级支出情况", "AccountQuery/Analysis04.aspx?QType=1","0" },
                new string[]{ "村级福利费收入表", "AccountQuery/Analysis04.aspx?QType=2","0" },
                new string[]{ "村级福利费支出表", "AccountQuery/Analysis04.aspx?QType=3","0" },
                new string[]{ "财务收支分析表", "AccountQuery/Analysis01.aspx","0" },
                new string[]{ "福利费收支分析表", "AccountQuery/Analysis02.aspx","0" },
                new string[]{ "资产负债分析表", "AccountQuery/Analysis03.aspx","0" }},
            new string[][]{
                new string[]{ "密码修改", "ChangePassword.aspx","0" },
                new string[]{ "返回首页", "../BusinessList.aspx","1" },
                new string[]{ "退出系统", "LoginOut.aspx","1" }}
        };
        for (int i = 0; i < mainMenu.Length; i++)
        {
            if (subMenu[i][0].Length == 1)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = "<a href=\"javascript:location.href='" + subMenu[i][0][0] + "'\">退出查询系统</a>";
                TreeView1.Nodes[0].ChildNodes.Add(newNode);
            }
            else
            {
                TreeNode newNode = new TreeNode(mainMenu[i]);
                newNode.SelectAction = TreeNodeSelectAction.Expand;
                TreeView1.Nodes[0].ChildNodes.Add(newNode);
                for (int k = 0; k < subMenu[i].Length; k++)
                {
                    TreeNode node = new TreeNode();
                    if (subMenu[i][k][2] == "0")
                    {
                        node.Text = "<a href=\"javascript:OpenUrl('" + subMenu[i][k][1] + "')\">" + subMenu[i][k][0] + "</a>";
                    }
                    else
                    {
                        node.Text = "<a href=\"javascript:location.href='" + subMenu[i][k][1] + "'\">" + subMenu[i][k][0] + "</a>";
                    }
                    newNode.ChildNodes.Add(node);
                }
            }
        }
        TreeView1.ExpandAll();
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
    }
}
