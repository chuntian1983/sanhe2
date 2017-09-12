<%@ Page Language="C#" AutoEventWireup="true" Inherits="_MonitorList" Codebehind="MonitorList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="Images/css.css" rel="Stylesheet" />
<style type="text/css">
td{border-bottom:1px dotted #c2c2c2}
</style>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
</script>
</head>
<body style="text-align:center">
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 700px; margin-top:50px">
            <tr>
                <td style="width: 4%; text-align:center">
                    <img src="images/21.gif" /><img src="images/21.gif" /></td>
                <td style="width: 32%">
                    <a href="SysManage/AccountProgress.aspx">
                    <img alt="" src="Images/AdminImg/zzjdcq.gif" style="border-width:0px" /><br />
                    做账进度</a></td>
                <td style="width: 32%">
                    <a href="AccountCollect/IndexMonitorSet.aspx">
                    <img  alt=""src="Images/AdminImg/zbsz.gif" style="border-width:0px" /><br />
                    指标控制</a></td>
                <td style="height: 100px; width: 32%">
                    <a href="AccountCollect/MonitorChart.aspx">
                    <img alt="" src="Images/AdminImg/zzjdcq.gif" style="border-width:0px" /><br />
                    图表统计</a></td>
            </tr>
            <tr>
                <td style="height: 100px; text-align:center">
                    <img src="images/21.gif" /><img src="images/21.gif" /></td>
                <td style="height: 100px">
                    <a href="AccountCollect/MonitorChart.aspx?sno=101">
                    <img alt="" src="Images/AdminImg/zjtj.gif" style="border-width:0px" /><br />
                    资金统计</a></td>
                <td>
                    <a href="AccountCollect/MonitorFinance.aspx">
                    <img alt="" src="Images/AdminImg/zjjg.gif" style="border-width:0px" /><br />
                    资金监管</a></td>
                <td style="height: 100px">
                    <a href="AccountCollect/VoucherQuery.aspx">
                    <img alt="" src="Images/AdminImg/zjxecx.gif" style="border-width:0px" /><br />
                    资金限额查询</a></td>

            </tr>
            <tr>
                <td style="height: 100px; text-align:center">
                    <img src="images/21.gif" /><img src="images/21.gif" /></td>
                <td style="height: 100px">
                    <a href="AccountCollect/MonitorFixedAssetChart.aspx">
                    <img alt="" src="Images/AdminImg/zctj.gif" style="border-width:0px" /><br />
                    资产统计</a></td>
                <td>
                    <a href="AccountCollect/MonitorFixedAsset.aspx">
                    <img alt="" src="Images/AdminImg/zcjg.gif" style="border-width:0px" /><br />
                    资产监管</a></td>
                <td>
                    <a href="AccountCollect/FixedAssetQuery.aspx">
                    <img alt="" src="Images/AdminImg/zzxecx.gif" style="border-width:0px" /><br />
                    资产限额查询</a></td>
            </tr>
            <tr>
                <td style="height: 100px; text-align:center">
                    <img src="images/21.gif" /><img src="images/21.gif" /></td>
                <td style="height: 100px">
                    <a href="AccountCollect/MonitorResourceChart.aspx">
                    <img alt="" src="Images/AdminImg/zytj.gif" style="border-width:0px" /><br />
                    资源统计</a></td>
                <td>
                    <a href="AccountCollect/MonitorResource.aspx">
                    <img alt="" src="Images/AdminImg/zyjg.gif" style="border-width:0px" /><br />
                    资源监管</a></td>
                <td>
                    <a href="AccountCollect/ResourceQuery.aspx">
                    <img alt="" src="Images/AdminImg/zyxecx.gif" style="border-width:0px" /><br />
                    资源限额查询</a></td>
            </tr>
        </table>
    </form>
</body>
</html>
