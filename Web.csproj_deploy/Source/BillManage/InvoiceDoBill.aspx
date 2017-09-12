<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_InvoiceDoBill" Codebehind="InvoiceDoBill.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>发票管理</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<base target="_self" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if ($("CustomName").value == "") {
        $("CustomName").focus();
        alert("客户名称不能为空！");
        return false;
    }
    if ($("InvoiceMoney").value != "0" && $("InvoiceMoney").value != $("DoBillMoney").value) {
        return confirm("该发票为定额发票，但是发票定额与开具发票金额不符。\n\n是否继续保存？");
    }
    return true;
}
function cal() {
    $("TaxMoney").value = formatFloat(eval($("DoBillMoney").value) * eval($("TaxRate").value) / 100);
    $("SumMoney").value = formatFloat(eval($("DoBillMoney").value) + eval($("TaxMoney").value));
}
function formatFloat(src) {
    return Math.round(src * Math.pow(10, 2)) / Math.pow(10, 2);
}
function OnWinClose()
{
    window.returnValue = $("RefreshFlag").value;
}
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
window.onload = function () {
    resetDialogSize();
    cal();
}
function selSubject(ctl, f) {
    var returnV = window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=1" + f + "&g=" + (new Date()).getTime(), "", "dialogWidth=380px;dialogHeight=401px;center=yes;");
    if (typeof (returnV) != "undefined" && returnV[0] != "" && returnV[0] != "+" && returnV[0] != "-") {
        $(ctl).value = returnV[1] + "." + returnV[0];
    }
}
</script>
</head>
<body onunload="OnWinClose();">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" style="width: 550px">
                <tr>
                    <td style="height: 35px; text-align: center">
                        <span style="font-size: 16pt; font-family: 隶书">开具发票</span>&nbsp;</td>
                </tr>
            </table>
        </div>
        <table id="EditorTable" cellpadding="0" cellspacing="0" style="width: 550px" runat="server">
            <tr style="background-color:#f8f8f8; color:blue">
                <td class="t1" style="width:15%; height:29px; text-align: right">
                    领购日期：</td>
                <td class="t1" style="width:35%;">
                    <asp:Label ID="BuyDate" runat="server"></asp:Label>&nbsp;</td>
                <td class="t1" style="width:15%; height:29px; text-align: right">
                    验旧提醒：</td>
                <td class="t2" style="width:35%;">
                    <asp:Label ID="OldTestDate" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr style="background-color:#f8f8f8; color:blue">
                <td class="t1" style="height:29px; text-align: right">
                    发票代码：</td>
                <td class="t1">
                    <asp:Label ID="InvoiceCode" runat="server"></asp:Label>&nbsp;</td>
                <td class="t1" style="height:29px; text-align: right">
                    发票号码：</td>
                <td class="t2">
                    <asp:Label ID="InvoiceNo" runat="server"></asp:Label>&nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    客户名称：</td>
                <td class="t1">
                    <asp:TextBox ID="CustomName" runat="server" BorderWidth="1px" Width="150px"></asp:TextBox>
                </td>
                <td class="t1" style="height:29px; text-align: right">
                    客户税号：</td>
                <td class="t2">
                    <asp:TextBox ID="CustomTaxNo" runat="server" BorderWidth="1px" Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    开票人：</td>
                <td class="t1">
                    <asp:TextBox ID="DoBillMan" runat="server" BorderWidth="1px" Width="80px"></asp:TextBox>
                </td>
                <td class="t1" style="height:29px; text-align: right">
                    开票日期：</td>
                <td class="t2">
                    <asp:TextBox ID="DoBillDate" runat="server" BorderWidth="1px" Width="80px"></asp:TextBox>
                </td>
            </tr>
            <tr id="redDiv0" runat="server">
                <td class="t1" style="height:29px; text-align: right">
                    开票金额：</td>
                <td class="t1">
                    <asp:TextBox ID="DoBillMoney" runat="server" BorderWidth="1px" Width="80px"></asp:TextBox>
                </td>
                <td class="t1" style="height:29px; text-align: right">
                    税率：</td>
                <td class="t2">
                    <asp:TextBox ID="TaxRate" runat="server" BorderWidth="1px" Width="80px" Text="0"></asp:TextBox>%
                </td>
            </tr>
            <tr id="redDiv1" runat="server">
                <td class="t1" style="height:29px; text-align: right">
                    税额：</td>
                <td class="t1">
                    <asp:TextBox ID="TaxMoney" runat="server" BorderWidth="1px" Width="80px"></asp:TextBox>
                </td>
                <td class="t1" style="height:29px; text-align: right">
                    价税合计：</td>
                <td class="t2">
                    <asp:TextBox ID="SumMoney" runat="server" BorderWidth="1px" Width="80px"></asp:TextBox>
                </td>
            </tr>
            <tr style="background-color:#f8f8f8;">
                <td class="t1" style="height:29px; text-align: right">
                    操作：</td>
                <td class="t1">
                    <asp:CheckBox ID="SameUpdate" runat="server" Text="记入日记账" /></td>
                <td class="t1" style="height:29px; text-align: right">
                    摘要：</td>
                <td class="t2">
                    <asp:TextBox ID="VSummary" runat="server" BorderWidth="1px" Width="180px"></asp:TextBox></td>
            </tr>
            <tr style="background-color:#f8f8f8;">
                <td class="t1" style="height:29px; text-align: right">
                    记账科目：</td>
                <td class="t1">
                    <asp:TextBox ID="DoBillSubject" runat="server" BorderWidth="1px" Width="150px"></asp:TextBox></td>
                <td class="t1" style="height:29px; text-align: right">
                    附件：</td>
                <td class="t2">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="185px" /></td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 60px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="保存" Width="180px" Height="30px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" style="width:180px; height:30px" value="关闭" onclick="window.close();" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="RefreshFlag" runat="server" Value="0" />
        <asp:HiddenField ID="InvoiceID" runat="server" />
        <asp:HiddenField ID="InvoiceMoney" runat="server" Value="0" />
        <asp:HiddenField ID="OldInvoiceID" runat="server" />
    </form>
</body>
</html>
