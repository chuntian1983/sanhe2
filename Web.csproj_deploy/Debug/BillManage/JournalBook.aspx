<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_JournalBook" Codebehind="JournalBook.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>日记账</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
input{margin-left:3px}
select{margin-left:3px}
</style>
<base target="_self" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript" id="HideScript" src=""></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if ($("AccSubjectNo").value == "") {
        $("AccSubjectNo").focus();
        alert("<%=sname %>不能为空！");
        return false;
    }
    if ($("VoucherDate").value == "") {
        $("VoucherDate").focus();
        alert("凭证日期不能为空！");
        return false;
    }
    if ($("VoucherNo").value == "") {
        $("VoucherNo").focus();
        alert("凭证号不能为空！");
        return false;
    }
    if ($("Handler").value == "") {
        $("Handler").focus();
        alert("经手人不能为空！");
        return false;
    }
    if ($("Notes").value == "") {
        $("Notes").focus();
        alert("摘要不能为空！");
        return false;
    }
    if(eval($("Balance").value)<0)
    {
        $("Notes").focus();
        alert("余额不能为负！");
        return false;
    }
    return true;
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
function SetVoucherDate(ctl)
{
    var returnV=window.showModalDialog("../AccountInit/SetAccountDate.aspx?g="+(new Date()).getTime(),"1","dialogWidth=330px;dialogHeight=226px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        $(ctl).value=returnV[0];
    }
}
function SelSubject(ctl, f) {
    var returnV = window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=1&" + f + "&g=" + (new Date()).getTime(), "", "dialogWidth=380px;dialogHeight=401px;center=yes;");
    if (typeof (returnV) != "undefined" && returnV[0] != "" && returnV[0] != "+" && returnV[0] != "-") {
        $(ctl).value = returnV[1] + "." + returnV[0];
        $("HideScript").src = "../AccountManage/GetDataFromAjax.aspx?flag=0&id=" + $("TableID").value + "&sno=" + returnV[1] + "&g=" + (new Date()).getTime();
    }
}
function inputMoeny(ctl)
{
    $(ctl).value="0.00";
    checkyue();
}
function formatFloat(src)
{
    return Math.round(src*Math.pow(10, 2))/Math.pow(10, 2);
}
function ShowVoucher(vid)
{
   window.showModalDialog("../AccountManage/LookVoucher.aspx?id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=767px;dialogHeight=385px;center=yes;");

}
function checkyue() {
    $("Balance").value = formatFloat(eval($("hidBalance").value) + eval($("DebitMoney").value) - eval($("CreditMoney").value));
}
window.onload = function () {
    resetDialogSize();
    checkyue();
}
</script>
</head>
<body onunload="OnWinClose();">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" style="width: 750px">
                <tr>
                    <td style="height: 35px; text-align: center">
                        <span style="font-size: 16pt; font-family: 隶书" id="PageTitle" runat="server">现金日记账</span>&nbsp;</td>
                </tr>
            </table>
        </div>
        <table id="EditorTable" cellpadding="0" cellspacing="0" style="width: 750px" runat="server">
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    <%=sname %>：</td>
                <td class="t1" colspan="3">
                    <asp:TextBox ID="AccSubjectNo" runat="server" BorderWidth="1px" Width="390px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    币种：</td>
                <td class="t2">
                    <asp:TextBox ID="AccCurrency" runat="server" BorderWidth="1px" Width="100px">人民币</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 12%; height:29px; text-align: right">
                    凭证日期：</td>
                <td class="t1" style="width: 19%">
                    <asp:TextBox ID="VoucherDate" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
                <td class="t1" style="width: 12%; text-align: right">
                    当日序号：</td>
                <td class="t1" style="width: 18%">
                    <asp:TextBox ID="DayNo" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
                <td class="t1" style="width: 12%; text-align: right">
                    分录号：&nbsp;</td>
                <td class="t2" style="width: 18%">
                    <asp:TextBox ID="EntryNo" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    凭证字：</td>
                <td class="t1" style="height: 29px">
                    <asp:DropDownList ID="VoucherType" runat="server">
                        <asp:ListItem Value="0">现付</asp:ListItem>
                        <asp:ListItem Value="1">现收</asp:ListItem>
                        <asp:ListItem Value="2">银付</asp:ListItem>
                        <asp:ListItem Value="3">银收</asp:ListItem>
                        <asp:ListItem Value="4">无</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t1" style="text-align: right; height: 29px;">
                    凭证号：</td>
                <td class="t1" style="height: 29px">
                    <asp:TextBox ID="VoucherNo" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
                <td class="t1" style="text-align: right; height: 29px;">
                    结算日期：</td>
                <td class="t2" style="height: 29px">
                    <asp:TextBox ID="SettleDate" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
            </tr>
            <tr id="DivSettle" runat="server">
                <td class="t1" style="height: 29px; text-align: right">
                    结算方式：</td>
                <td class="t1">
                    <asp:DropDownList ID="SettleType" runat="server">
                        <asp:ListItem Value="1">现金支票</asp:ListItem>
                        <asp:ListItem Value="2">转账支票</asp:ListItem>
                        <asp:ListItem Value="3">电汇凭证</asp:ListItem>
                        <asp:ListItem Value="4">贷记凭证</asp:ListItem>
                        <asp:ListItem Value="5">商业承兑汇票</asp:ListItem>
                        <asp:ListItem Value="6">银行承兑汇票</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="text-align: right">
                    结算号：</td>
                <td class="t1">
                    <asp:TextBox ID="SettleNo" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    余额：</td>
                <td class="t2">
                    <asp:TextBox ID="Balance" runat="server" BorderWidth="1px" Width="100px">0.00</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    借方金额：</td>
                <td class="t2">
                    <asp:TextBox ID="DebitMoney" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    贷方金额：</td>
                <td class="t1">
                    <asp:TextBox ID="CreditMoney" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    经手人：</td>
                <td class="t2">
                    <asp:TextBox ID="Handler" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height:29px; text-align: right">
                    摘要：</td>
                <td class="t1" colspan="3">
                    <asp:TextBox ID="Notes" runat="server" BorderWidth="1px" Width="390px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    制单人：</td>
                <td class="t2">
                    <asp:TextBox ID="DoBill" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 29px; text-align: right">
                    附件：</td>
                <td class="t1" colspan="3">
                    <asp:FileUpload ID="FileUpload1" unselectable="on" runat="server" Width="300px" />
                    &nbsp;&nbsp;<asp:HyperLink ID="ShowFile" runat="server" Target="_blank">查看</asp:HyperLink>
                    &nbsp;&nbsp;<asp:LinkButton ID="DelFile" runat="server" OnClick="DelFile_Click">删除</asp:LinkButton></td>
                <td class="t1" style="text-align: right">
                    &nbsp;</td>
                <td class="t2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="t4" colspan="6" style="height: 60px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="保存" Width="180px" Height="30px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" style="width:180px; height:30px" value="关闭" onclick="window.close();" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="RefreshFlag" runat="server" Value="0" />
        <asp:HiddenField ID="TableID" runat="server" />
        <asp:HiddenField ID="hidBalance" runat="server" Value="0" />
    </form>
</body>
</html>
