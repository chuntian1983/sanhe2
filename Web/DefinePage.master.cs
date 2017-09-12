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

public partial class _DefinePage : System.Web.UI.MasterPage
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
        if (string.IsNullOrEmpty(Request.QueryString["m"]) == false)
        {
            int.TryParse(Request.QueryString["m"], out m);
        }
        string mylevel = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + Session["UnitID"].ToString() + "']", "UnitLevel");
        string TotalLevel = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo", "LastLevel");
        string UnitLevel = "0";
        if (mylevel.Length > 0)
        {
            UnitLevel = mylevel;
        }
        switch (m)
        {
            case 0:
                ScriptString.Append("t=outlookbar.addtitle('资金监管');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zjtj.gif width=65 height=65 border=0 alt="
                    + "资金统计><br><br>资金统计',t,'AccountCollect/MonitorChart.aspx?sno=101');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zjjg.gif width=65 height=65 border=0 alt="
                    + "资金监管><br><br>资金监管',t,'AccountCollect/MonitorFinance.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zjxecx.gif width=65 height=65 border=0 alt="
                    + "资金限额查询><br><br>资金限额查询',t,'AccountCollect/VoucherQuery.aspx');\n");
                break;
            case 1:
                ScriptString.Append("t=outlookbar.addtitle('资产监管');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zctj.gif width=65 height=65 border=0 alt="
                    + "资产统计><br><br>资产统计',t,'AccountCollect/MonitorFixedAssetChart.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zcjg.gif width=65 height=65 border=0 alt="
                    + "资产监管><br><br>资产监管',t,'AccountCollect/MonitorFixedAsset.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zzxecx.gif width=65 height=65 border=0 alt="
                    + "资产限额查询><br><br>资产限额查询',t,'AccountCollect/FixedAssetQuery.aspx');\n");
                break;
            case 2:
                ScriptString.Append("t=outlookbar.addtitle('资源监管');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zytj.gif width=65 height=65 border=0 alt="
                    + "资源统计><br><br>资源统计',t,'AccountCollect/MonitorResourceChart.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zyjg.gif width=65 height=65 border=0 alt="
                    + "资源监管><br><br>资源监管',t,'AccountCollect/MonitorResource.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zyxecx.gif width=65 height=65 border=0 alt="
                    + "资源限额查询><br><br>资源限额查询',t,'AccountCollect/ResourceQuery.aspx');\n");
                break;
            case 3:
                ScriptString.Append("t=outlookbar.addtitle('视频监控');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zzjdcq.gif width=65 height=65 border=0 alt="
                    + "视频监控><br><br>视频监控',t,'gongzuo.aspx');\n");
                break;
            case 4:
                ScriptString.Append("t=outlookbar.addtitle('三资汇总');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/szhzjb.gif width=65 height=65 border=0 alt="
                    + "设置汇总级别><br><br>设置汇总级别',t,'SysManage/GatherLevel.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/kmyeb.gif width=65 height=65 border=0 alt="
                    + "科目余额表><br><br>科目余额表',t,'AccountCollect/SubjectBalance.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/szmx.gif width=65 height=65 border=0 alt="
                    + "收支明细表><br><br>收支明细表',t,'AccountCollect/CommReport.aspx?DesignID=000003');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zcfz.gif width=65 height=65 border=0 alt="
                    + "资产负债表><br><br>资产负债表',t,'AccountCollect/CommReport.aspx?DesignID=000004');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/syfpb.gif width=65 height=65 border=0 alt="
                    + "收益分配表><br><br>收益分配表',t,'AccountCollect/CommReport.aspx?DesignID=000005');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/cwgk.gif width=65 height=65 border=0 alt="
                    + "财务公开榜><br><br>财务公开榜',t,'AccountCollect/CommReport.aspx?DesignID=000006');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/kmyedb.gif width=65 height=65 border=0 alt="
                    + "科目余额对比表><br><br>科目余额对比表',t,'AccountCollect/SubjectCollect.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/kmyehx.gif width=65 height=65 border=0 alt="
                    + "科目余额横向表><br><br>科目余额横向表',t,'AccountCollect/SubjectList.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/assetsum.gif width=65 height=65 border=0 alt="
                    + "固定资产汇总表><br><br>固定资产汇总表',t,'AccountCollect/FixedAssetSummary.aspx');\n");
                break;
            case 5:
                ScriptString.Append("t=outlookbar.addtitle('村级查询');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/szcxzt.gif width=65 height=65 border=0 alt="
                    + "设置查询账套><br><br>设置查询账套',t,'ChooseAccount.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/kmmbk.gif width=65 height=65 border=0 alt="
                    + "账套科目表><br><br>账套科目表',t,'AccountInit/AccountSubject.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/kmyeb.gif width=65 height=65 border=0 alt="
                    + "科目余额表><br><br>科目余额表',t,'AccountQuery/SubjectBalanceDay.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/pzlbcx.gif width=65 height=65 border=0 alt="
                    + "凭证列表查询><br><br>凭证列表查询',t,'AccountQuery/VoucherList.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/pzlbcx.gif width=65 height=65 border=0 alt="
                    + "凭证单张查询><br><br>凭证单张查询',t,'AccountQuery/VoucherPage.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/pzdysc.gif width=65 height=65 border=0 alt="
                    + "凭证打印输出><br><br>凭证打印输出',t,'AccountQuery/PrintVoucher.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zzcx.gif width=65 height=65 border=0 alt="
                    + "总账查询><br><br>总账查询',t,'AccountQuery/GeneralLedger.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/mxflz.gif width=65 height=65 border=0 alt="
                    + "明细分类账><br><br>明细分类账',t,'AccountQuery/DetailAccount.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/yszk.gif width=65 height=65 border=0 alt="
                    + "应收账款查询><br><br>应收账款查询',t,'AccountQuery/AccountsReceivable.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/yszk.gif width=65 height=65 border=0 alt="
                    + "应付账款查询><br><br>应付账款查询',t,'AccountQuery/AccountsPayable.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><div style=\\'background-color:#f6f6f6;color:green;font-size:12px\\'>——————————</div>',t,'###');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/kmyeb.gif width=65 height=65 border=0 alt="
                    + "科目余额汇总表><br><br>科目余额汇总表',t,'AccountQuery/SubjectBalance.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/szmx.gif width=65 height=65 border=0 alt="
                    + "收支明细表><br><br>收支明细表',t,'AccountQuery/CommReport.aspx?DesignID=000003');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zcfz.gif width=65 height=65 border=0 alt="
                    + "资产负债表><br><br>资产负债表',t,'AccountQuery/CommReport.aspx?DesignID=000004');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/nbwlz.gif width=65 height=65 border=0 alt="
                    + "内部往来余额表><br><br>内部往来余额表',t,'AccountQuery/InternalDemand.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/syfpb.gif width=65 height=65 border=0 alt="
                    + "收益分配表><br><br>收益分配表',t,'AccountQuery/IncomeDistribution.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/cwgk_y.gif width=65 height=65 border=0 alt="
                    + "财务公开榜(月表)><br><br>财务公开榜(月表)',t,'AccountQuery/CommReport.aspx?DesignID=000006');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/cwgk_j.gif width=65 height=65 border=0 alt="
                    + "财务公开榜(季表)><br><br>财务公开榜(季表)',t,'AccountQuery/QuarterReport.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/szzbgk.gif width=65 height=65 border=0 alt="
                    + "收支逐笔公开榜><br><br>收支逐笔公开榜',t,'AccountQuery/EachAccPublish.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/szzbgk.gif width=65 height=65 border=0 alt="
                    + "资产明细表><br><br>资产明细表',t,'FixedAsset/FixedAssetDetail.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/szzbgk.gif width=65 height=65 border=0 alt="
                    + "资产明细账><br><br>资产明细账',t,'FixedAsset/FADetailAccount.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/ziyuanmingxi.gif width=65 height=65 border=0 alt="
                    + "资源明细表><br><br>资源明细表',t,'ResManage/ResourceDetail.aspx');\n");
                break;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        scriptstring = ScriptString.ToString();
    }
}
