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

public partial class _MonitorPage : System.Web.UI.MasterPage
{
    public string MenuHeight;
    public string scriptstring;

    protected void Page_Load(object sender, EventArgs e)
    {
        //自动登录信息
        Session["SessionFlag"] = "SessionFlag";
        Session["UserID"] = "000000";
        Session["RealName"] = "管理员";
        Session["UserName"] = "Administrator";
        Session["UnitID"] = "000000";
        Session["Powers"] = "000000|100014";
        Session["UserType"] = "1";
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        MenuHeight = "583";
        StringBuilder ScriptString = new StringBuilder();
        this.Page.Title += " —— 当前单位：" + ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + Session["UnitID"].ToString() + "']", "UnitName");
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ScriptString.Append("t=outlookbar.addtitle('三资监管');\n");
        ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/cwgk.gif width=65 height=65 border=0 alt="
            + "收支公开榜><br><br>收支公开榜',t,'AccountQuery/EachAccPublish.aspx');\n");
        ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/dxtj.gif width=65 height=65 border=0 alt="
            + "单项统计><br><br>单项统计',t,'AccountCollect/Statistics.aspx');\n");
        ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zbsz.gif width=65 height=65 border=0 alt="
            + "指标控制><br><br>指标控制',t,'AccountCollect/IndexMonitorSet.aspx');\n");
        ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zjjg.gif width=65 height=65 border=0 alt="
            + "资金监管><br><br>资金监管',t,'AccountCollect/MonitorFinance.aspx');\n");
        ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zcjg.gif width=65 height=65 border=0 alt="
            + "资产监管><br><br>资产监管',t,'AccountCollect/MonitorFixedAsset.aspx');\n");
        ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zyjg.gif width=65 height=65 border=0 alt="
            + "资源监管><br><br>资源监管',t,'AccountCollect/MonitorResource.aspx');\n");
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        scriptstring = ScriptString.ToString();
    }
}
