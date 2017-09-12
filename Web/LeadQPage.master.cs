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

public partial class _LeadQPage : System.Web.UI.MasterPage
{
    public string MenuHeight;
    public string scriptstring;
    public StringBuilder DueContracts = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        MenuHeight = "513";
        StringBuilder ScriptString = new StringBuilder();
        this.Page.Title += " —— 当前单位：" + ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + Session["UnitID"].ToString() + "']", "UnitName");
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        int m = 0;
        int.TryParse(Request.QueryString["m"], out m);
        switch (m)
        {
            case 2:
                ScriptString.Append("t=outlookbar.addtitle('报表汇总');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/szhzjb.gif width=65 height=65 border=0 alt="
                    + "设置汇总级别><br><br>设置汇总级别',t,'SysManage/GatherLevel.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/kmyeb.gif width=65 height=65 border=0 alt="
                    + "科目余额表><br><br>科目余额表',t,'AccountCollect/SubjectBalance.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/mxflz.gif width=65 height=65 border=0 alt="
                    + "代管资金汇总><br><br>代管资金汇总',t,'BillManage/DetailAccountSum.aspx');\n");
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
            case 3:
                ScriptString.Append("t=outlookbar.addtitle('账簿查询');\n");
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
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/mxflz.gif width=65 height=65 border=0 alt="
                    + "代管资金汇总><br><br>代管资金汇总',t,'BillManage/DetailAccountDay.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/yszk.gif width=65 height=65 border=0 alt="
                    + "应收账款查询><br><br>应收账款查询',t,'AccountQuery/AccountsReceivable.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/yszk.gif width=65 height=65 border=0 alt="
                    + "应付账款查询><br><br>应付账款查询',t,'AccountQuery/AccountsPayable.aspx');\n");
                break;
            case 4:
                ScriptString.Append("t=outlookbar.addtitle('报表查询');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/szcxzt.gif width=65 height=65 border=0 alt="
                    + "设置查询账套><br><br>设置查询账套',t,'ChooseAccount.aspx');\n");
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
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zzxecx.gif width=65 height=65 border=0 alt="
                    + "资产合同查询><br><br>资产合同查询',t,'Contract/LeaseContractQuery.aspx?ctype=0');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zzjdcq.gif width=65 height=65 border=0 alt="
                    + "资源合同查询><br><br>资源合同查询',t,'Contract/LeaseContractQuery.aspx?ctype=1');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/szmx.gif width=65 height=65 border=0 alt="
                    + "经济合同台账><br><br>经济合同台账',t,'Contract/LeaseContractDetail.aspx');\n");
                break;
            case 5:
                ScriptString.Append("t=outlookbar.addtitle('报表分析');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/szcxzt.gif width=65 height=65 border=0 alt="
                    + "设置查询账套><br><br>设置查询账套',t,'ChooseAccount.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/kmyeb.gif width=65 height=65 border=0 alt="
                    + "村级收入情况><br><br>村级收入情况',t,'AccountQuery/Analysis04.aspx?QType=0');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/szmx.gif width=65 height=65 border=0 alt="
                    + "村级支出情况><br><br>村级支出情况',t,'AccountQuery/Analysis04.aspx?QType=1');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zcfz.gif width=65 height=65 border=0 alt="
                    + "村级福利费收入表><br><br>村级福利费收入表',t,'AccountQuery/Analysis04.aspx?QType=2');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/nbwlz.gif width=65 height=65 border=0 alt="
                    + "村级福利费支出表><br><br>村级福利费支出表',t,'AccountQuery/Analysis04.aspx?QType=3');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/syfpb.gif width=65 height=65 border=0 alt="
                    + "财务收支分析表><br><br>财务收支分析表',t,'AccountQuery/Analysis01.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/cwgk_y.gif width=65 height=65 border=0 alt="
                    + "福利费收支分析表><br><br>福利费收支分析表',t,'AccountQuery/Analysis02.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/cwgk_j.gif width=65 height=65 border=0 alt="
                    + "资产负债分析表><br><br>资产负债分析表',t,'AccountQuery/Analysis03.aspx');\n");
                break;
            case 10:
                ScriptString.Append("t=outlookbar.addtitle('视频监控');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zzjdcq.gif width=65 height=65 border=0 alt="
                    + "视频监控><br><br>视频监控',t,'gongzuo.aspx');\n");
                PageClass.ExcuteScript(this.Page, "SetFrameUrl('gongzuo.aspx')");
                break;
            case 7:
                ScriptString.Append("t=outlookbar.addtitle('网上测评');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zzjdcq.gif width=65 height=65 border=0 alt="
                    + "网上测评><br><br>网上测评',t,'view/CePing/cpgl.aspx');\n");
                PageClass.ExcuteScript(this.Page, "SetFrameUrl('view/CePing/cpgl.aspx')");
                break;
            case 8:
                ScriptString.Append("t=outlookbar.addtitle('我要投诉');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zzjdcq.gif width=65 height=65 border=0 alt="
                    + "我要投诉><br><br>我要投诉',t,'view/Tousu/index.aspx');\n");
                PageClass.ExcuteScript(this.Page, "SetFrameUrl('view/Tousu/index.aspx')");
                break;
            case 9:
                ScriptString.Append("t=outlookbar.addtitle('信访举报');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zzjdcq.gif width=65 height=65 border=0 alt="
                    + "信访举报><br><br>信访举报',t,'view/Tousu2/index.aspx');\n");
                PageClass.ExcuteScript(this.Page, "SetFrameUrl('view/Tousu2/index.aspx')");
                break;
            default:
                ScriptString.Append("t=outlookbar.addtitle('三资监管');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zzjdcq.gif width=65 height=65 border=0 alt="
                    + "做账进度查询><br><br>做账进度查询',t,'SysManage/AccountProgress.aspx');\n");
                if (Session["UnitID"].ToString() == "000000")
                {
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zzjdcq.gif width=65 height=65 border=0 alt="
                        + "做账进度汇总><br><br>做账进度汇总',t,'SysManage/AccountProgressSum.aspx');\n");
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/bjsz.gif width=65 height=65 border=0 alt="
                        + "预警设置><br><br>预警设置',t,'SysManage/BalanceAlarm.aspx');\n");
                    ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/bjcl.gif width=65 height=65 border=0 alt="
                        + "预警处理><br><br>预警处理',t,'SysManage/BalanceAlarmDo.aspx');\n");
                    //ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zyxecx.gif width=65 height=65 border=0 alt="
                    //    + "资产资源评估><br><br>资产资源评估',t,'SysManage/BalanceAssessDo.aspx');\n");
                    //ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zyjg.gif width=65 height=65 border=0 alt="
                    //    + "三资流程审批><br><br>三资流程审批',t,'SysManage/BalanceReplyDo.aspx');\n");
                    GetDueContracts();
                }
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/dxtj.gif width=65 height=65 border=0 alt="
                    + "单项统计><br><br>单项统计',t,'AccountCollect/Statistics.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zbsz.gif width=65 height=65 border=0 alt="
                    + "指标控制><br><br>指标控制',t,'AccountCollect/IndexMonitorSet.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/tbtj.gif width=65 height=65 border=0 alt="
                    + "图表统计><br><br>图表统计',t,'AccountCollect/MonitorChart.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><div style=\\'background-color:#f6f6f6;color:green;font-size:12px\\'>——————————</div>',t,'###');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zjtj.gif width=65 height=65 border=0 alt="
                    + "资金统计><br><br>资金统计',t,'AccountCollect/MonitorChart.aspx?sno=101');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zjjg.gif width=65 height=65 border=0 alt="
                    + "资金监管><br><br>资金监管',t,'AccountCollect/MonitorFinance.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zjxecx.gif width=65 height=65 border=0 alt="
                    + "资金限额查询><br><br>资金限额查询',t,'AccountCollect/VoucherQuery.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><div style=\\'background-color:#f6f6f6;color:green;font-size:12px\\'>——————————</div>',t,'###');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zctj.gif width=65 height=65 border=0 alt="
                    + "资产统计><br><br>资产统计',t,'AccountCollect/MonitorFixedAssetChart.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zcjg.gif width=65 height=65 border=0 alt="
                    + "资产监管><br><br>资产监管',t,'AccountCollect/MonitorFixedAsset.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zzxecx.gif width=65 height=65 border=0 alt="
                    + "资产限额查询><br><br>资产限额查询',t,'AccountCollect/FixedAssetQuery.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><div style=\\'background-color:#f6f6f6;color:green;font-size:12px\\'>——————————</div>',t,'###');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zytj.gif width=65 height=65 border=0 alt="
                    + "资源统计><br><br>资源统计',t,'AccountCollect/MonitorResourceChart.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zyjg.gif width=65 height=65 border=0 alt="
                    + "资源监管><br><br>资源监管',t,'AccountCollect/MonitorResource.aspx');\n");
                ScriptString.Append("outlookbar.additem('<br><img src=Images/AdminImg/zyxecx.gif width=65 height=65 border=0 alt="
                    + "资源限额查询><br><br>资源限额查询',t,'AccountCollect/ResourceQuery.aspx');\n");
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
