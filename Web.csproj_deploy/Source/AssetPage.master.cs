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
using System.Text.RegularExpressions;
using System.Text;

public partial class _AssetPage : System.Web.UI.MasterPage
{
    public StringBuilder SubMenuList = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        LeftFrame1.CardType = "0";
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        string[] mainMenu ={ "资产设置", "资产管理", "合同管理", "报表查询", "安全退出" };
        string[][][] subMenu ={
            new string[][]{
                new string[]{ "部门管理", "SysManage/DeptManage.aspx" },
                new string[]{ "类别管理", "FixedAsset/ClassManage.aspx" },
                new string[]{ "增减方式", "FixedAsset/DITypeManage.aspx" }},
            new string[][]{
                new string[]{ "资产录入", "FixedAsset/FixedAssetCard.aspx" },
                new string[]{ "资产管理", "FixedAsset/FixedAssetList.aspx" },
                new string[]{ "计提制单", "FixedAsset/CarryMonthDepr.aspx" }},
            //new string[][]{
            //    new string[]{ "资产租赁申请", "FinanceFlow/ApplyList.aspx?flowtype=1" }},
            new string[][]{
                new string[]{ "资产流转", "Contract/TurnFixedAsset.aspx" },
                new string[]{ "合同管理", "Contract/LeaseList.aspx?ctype=0" },
                new string[]{ "合同到期查询", "Contract/DueRemindLease.aspx?ctype=0" },
                new string[]{ "收款到期查询", "Contract/DueRemindPay.aspx?ctype=0" }},
            new string[][]{
                new string[]{ "资产明细表", "FixedAsset/FixedAssetDetail.aspx" },
                new string[]{ "资产明细账", "FixedAsset/FADetailAccount.aspx" },
                new string[]{ "资产流转情况", "Contract/TurnList.aspx?ctype=0" },
                new string[]{ "经济合同查询", "Contract/LeaseContractQuery.aspx?ctype=0" },
                new string[]{ "经济合同台账", "Contract/LeaseContractDetail.aspx?ctype=0" }},
            new string[][]{new string[]{ "TargetSelf" }}
        };
        for (int i = 0; i < mainMenu.Length; i++)
        {
            if (subMenu[i][0].Length == 1)
            {
                SubMenuList.Append("<li class=\"imatm\"  style=\"width:120px;\"><a href=\"Javascript:MenuBarClick('" + subMenu[i][0][0]
                    + "')\"><span class=\"imea imeam\"><span></span></span>");
                SubMenuList.Append(mainMenu[i] + "</a></li>");
            }
            else
            {
                SubMenuList.Append("<li class=\"imatm\"  style=\"width:120px;\"><a href=\"Javascript:\"><span class=\"imea imeam\"><span></span></span>");
                SubMenuList.Append(mainMenu[i] + "</a><div class=\"imsc\"><div class=\"imsubc\" style=\"width:150px;top:0px;left:0px;\"><ul>");
                for (int k = 0; k < subMenu[i].Length; k++)
                {
                    SubMenuList.Append("<li style=\"height:20px;border-bottom: 1px solid buttonface;\"><a href=\"Javascript:MenuBarClick('"
                        + subMenu[i][k][1] + "')\"><img src='Images/21.gif'>&nbsp;&nbsp;" + subMenu[i][k][0] + "</a></li>");
                }
                SubMenuList.Append("</ul></div></div></li>");
            }
        }
    }
}
