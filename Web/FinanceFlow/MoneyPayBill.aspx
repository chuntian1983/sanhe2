﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="FinanceFlow_MoneyPayBill" Codebehind="MoneyPayBill.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>通知单打印</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function resetDialogSize()
{
    var ua = navigator.userAgent;
    if(ua.lastIndexOf("MSIE 7.0") == -1)
    {
        var height = document.body.offsetHeight;
        var width = document.body.offsetWidth;
        if(ua.lastIndexOf("Windows NT 5.2") == -1)
        {
            window.dialogHeight=(height+53)+"px";
            window.dialogWidth=(width+6)+"px";
        }
        else
        {
            window.dialogHeight=(height+46)+"px";
            window.dialogWidth=(width+6)+"px";
        }
    }
}
window.onload = resetDialogSize;
</script>
</head>
<body style="width: 620px;">
    <form id="form1" runat="server">
    <div style="text-align:center;">
    <div style="margin: 5px; border:1px dotted gray; width: 620px;">
        <table cellpadding="0" cellspacing="0" style="margin:5px; font-size:12pt; width: 615px;">
            <tr>
                <td style="font-size:15pt; text-align:center; height:100px">
                    现金支付单
                </td>
            </tr>
            <tr>
                <td style="text-align:left; vertical-align:top; line-height:35px; height:auto;">
                    人民币：<asp:Label ID="Money0" runat="server"></asp:Label>（大写），￥<asp:Label ID="Money1" runat="server"></asp:Label>元<br /><br />
                    上款系：镇（乡）“三资代理”服务中心批给<asp:Label ID="AName" runat="server"></asp:Label>的可使用资金总额<br /><br />
                    用途：<asp:Label ID="ApplyUsage" runat="server"></asp:Label><br /><br />
                    备注：请严格按照“三资代理”服务中心的批复使用，业务完成后次日上交正规原始凭证，并健全手续（经手人、领收人、财务负责人、民主理财小组等签字（或盖章））。<br /><br />
                    收款单位：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（财务盖章）<br /><br />
                    收款人：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（签字盖章）<br /><br />
                    “三资代理”服务中心主任：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（签字盖章）<br /><br />
                    经办人：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;（签字盖章）<br /><br />
                </td>
            </tr>
            <tr>
                <td style="text-align:right; height:25px">
                    <asp:Label ID="PrintDate" runat="server" Width="150px"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <!--NoPrintStart-->
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 632px; margin-top: 15px">
            <tr>
                <td style="background-position: center center; background-repeat: no-repeat; height: 32px; text-align: center">
                    <input id="Button1" type="button" value="打印现金支付单" onclick="window.open('../PrintWeb.html','','');" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" value="关闭窗口" onclick="window.close();" />
                </td>
            </tr>
        </table>
    </div>
    <!--NoPrintEnd-->
    </div>
    </form>
</body>
</html>
