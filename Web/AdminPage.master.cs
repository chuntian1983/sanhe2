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

public partial class _AdminPage : System.Web.UI.MasterPage
{
    public string MenuHeight;
    public string scriptstring;
    public StringBuilder DueContracts = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        MenuHeight = "533";
        StringBuilder ScriptString = new StringBuilder();
        this.Page.Title += " —— 当前单位：" + ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + Session["UnitID"].ToString() + "']", "UnitName");
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        int m = 0;
        int.TryParse(Request.QueryString["m"], out m);
        string mylevel = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + Session["UnitID"].ToString() + "']", "UnitLevel");
        string TotalLevel = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo", "LastLevel");
        string UnitLevel = "0";
        if (mylevel.Length > 0)
        {
            UnitLevel = mylevel;
        }
        switch (m)
        {
            default:
                ScriptString.Append("t=outlookbar.addtitle('系统管理');\n");
                if (Session["UnitID"].ToString() == "000000")
                {
                    //ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/bjsz.gif width=65 height=65 border=0 alt="
                    //    + "报警设置><br><br>报警设置',t,'SysManage/BalanceAlarm.aspx');\n");
                    //ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/bjcl.gif width=65 height=65 border=0 alt="
                    //    + "报警处理><br><br>报警处理',t,'SysManage/BalanceAlarmDo.aspx');\n");
                    //ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zyxecx.gif width=65 height=65 border=0 alt="
                    //    + "资产资源评估><br><br>资产资源评估',t,'SysManage/BalanceAssessDo.aspx');\n");
                    //ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zyjg.gif width=65 height=65 border=0 alt="
                    //    + "三资流程审批><br><br>三资流程审批',t,'SysManage/BalanceReplyDo.aspx');\n");
                }
                if (UnitLevel == TotalLevel)
                {
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/ztgl.gif width=65 height=65 border=0 alt="
                        + "账套管理><br><br>账套管理',t,'SysManage/AccountManage.aspx');\n");
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/czygl.gif width=65 height=65 border=0 alt="
                        + "操作员管理><br><br>操作员管理',t,'SysManage/UserManage.aspx');\n");
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/cjcqgl.gif width=65 height=65 border=0 alt="
                        + "村级查询管理><br><br>村级查询管理',t,'SysManage/QUserManage.aspx');\n");
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/szfykze.gif width=65 height=65 border=0 alt="
                        + "设置费用控制额><br><br>设置费用控制额',t,'SysManage/SetLimiteFee.aspx');\n");
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/czygl.gif width=65 height=65 border=0 alt="
                        + "操作日志查询><br><br>操作日志查询',t,'AccountInit/CTLLogManage.aspx');\n");
                }
                if (Session["UserType"].ToString() != "2")
                {
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/kmmbk.gif width=65 height=65 border=0 alt="
                        + "科目模板库维护><br><br>科目模板库维护',t,'SysManage/AccountSubject.aspx');\n");
                }
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/szbbqz.gif width=65 height=65 border=0 alt="
                    + "设置报表底部签字><br><br>设置报表底部签字',t,'SysManage/SetSignName.aspx');\n");
                if (Session["UserType"].ToString() == "2")
                {
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/szfykze.gif width=65 height=65 border=0 alt="
                        + "通用参数设置><br><br>通用参数设置',t,'SysManage/SetSysParas.aspx');\n");
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/cjcqgl.gif width=65 height=65 border=0 alt="
                        + "村级职务管理><br><br>村级职务管理',t,'SysManage/CommDics.aspx');\n");
                }
                string setCondition = ConfigurationManager.AppSettings["setCondition"];
                if (setCondition == null)
                {
                    setCondition = "0";
                }
                if (setCondition == UnitLevel || setCondition == "-")
                {
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zcfz.gif width=65 height=65 border=0 alt="
                        + "资金支出设置><br><br>资金支出设置',t,'view/PiBanKa/ConditionSet.aspx?deptid=1');\n");
                }
                string addBUser = ConfigurationManager.AppSettings["addBUser"];
                if (addBUser == null)
                {
                    addBUser = "0";
                }
                if (addBUser == UnitLevel || addBUser == "-")
                {
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/czygl.gif width=65 height=65 border=0 alt="
                        + "招投标用户管理><br><br>招投标用户管理',t,'SysManage/BidUserManage.aspx');\n");
                }
                if (Session["UserType"].ToString() == "2")
                {
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zylbgl.gif width=65 height=65 border=0 alt="
                        + "资源类别管理><br><br>资源类别管理',t,'SysManage/ResClassManage.aspx');\n");
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/kmmbk.gif width=65 height=65 border=0 alt="
                        + "系统科目模板库><br><br>系统科目模板库',t,'SysManage/SystemSubject.aspx');\n");
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/xtsjkwh.gif width=65 height=65 border=0 alt="
                        + "系统数据库维护><br><br>系统数据库维护',t,'SysManage/MDatabase.aspx');\n");
                }
                break;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        scriptstring = ScriptString.ToString();
    }
    private void GetDueContracts()
    {
        Dictionary<string, string> units = new Dictionary<string, string>();
        DueContracts.Append("<table cellpadding=0 cellspacing=0 style='width:96%;'>");
        DataTable dt = MainClass.GetDataTable("select a.ID,a.UnitID,a.AccountID,a.VoucherID,a.VoucherNo,a.VoucherDate,b.AccountName from cw_balancealarm a,cw_account b where a.AccountID=b.id and a.AlarmType='0' and a.DoState='0' order by a.BookTime desc");
        string rowStyle = "style='height:25px;border-bottom: 1px dotted #CCCCCC;cursor:hand'";
        foreach (DataRow row in dt.Rows)
        {
            string unitid = row["UnitID"].ToString();
            if (units.ContainsKey(unitid) == false)
            {
                units.Add(unitid, ValidateClass.ReadXMLNodeText(string.Format("FinancialDB/CUnits[ID='{0}']/UnitName", unitid)));
            }
            DueContracts.Append("<tr onmouseover=\"this.style.background='#E0EFF6\';\" onmouseout=\"this.style.background='';\">");
            DueContracts.AppendFormat("<td {0} onclick=ShowVoucher('{1}','{2}','{3}')><img src=Images/dot2.jpg>&nbsp;{4}，{5}，凭证编号：{6}，日期：{7}</td>",
                rowStyle, row["ID"].ToString(), row["AccountID"].ToString(), row["VoucherID"].ToString(), units[unitid], row["AccountName"].ToString(), row["VoucherNo"].ToString(), row["VoucherDate"].ToString());
            DueContracts.Append("</tr>");
        }
        DueContracts.Append("</table>");
        if (dt.Rows.Count > 0)
        {
            showmsg.Visible = true;
        }
    }
}
