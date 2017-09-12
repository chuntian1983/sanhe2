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
using System.Text.RegularExpressions;

public partial class _ResourcePage : System.Web.UI.MasterPage
{
    public StringBuilder SubMenuList = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        LeftFrame1.CardType = "1";
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        string[] mainMenu ={ "资源设置", "资源管理", "合同管理", "报表查询", "安全退出" };
        string[][][] subMenu ={
            new string[][]{
                new string[]{ "部门管理", "SysManage/DeptManage.aspx" }},
            new string[][]{
                new string[]{ "资源录入", "ResManage/ResourceCard.aspx" },
                new string[]{ "资源管理", "ResManage/ResourceList.aspx" }},
            //new string[][]{
            //    new string[]{ "资源发包申请", "FinanceFlow/ApplyList.aspx?flowtype=2" }},
            new string[][]{
                new string[]{ "资源流转", "Contract/TurnResource.aspx" },
                new string[]{ "合同管理", "Contract/LeaseList.aspx?ctype=1" },
                new string[]{ "合同到期查询", "Contract/DueRemindLease.aspx?ctype=1" },
                new string[]{ "收款到期查询", "Contract/DueRemindPay.aspx?ctype=1" }},
            new string[][]{
                new string[]{ "资源明细表", "ResManage/ResourceDetail.aspx" },
                new string[]{ "资源流转情况", "Contract/TurnList.aspx?ctype=1" },
                new string[]{ "经济合同查询", "Contract/LeaseContractQuery.aspx?ctype=1" },
                new string[]{ "经济合同台账", "Contract/LeaseContractDetail.aspx?ctype=1" }},
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
