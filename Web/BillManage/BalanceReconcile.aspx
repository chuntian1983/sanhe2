<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_BalanceCeconcile" Codebehind="BalanceReconcile.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<style type="text/css">
.txtnum{border-width: 0px;width:80px; border-bottom: 1px solid black;text-align:right;color:blue;}
</style>
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
    function $(o) { return (typeof (o) == "object") ? o : document.getElementById(o); }
    function CheckSubmit() {
        if (eval($("Balance3").innerText) != eval($("Bank3").innerText)) {
            return confirm("银行存款余额调节表不平衡，是否继续保存？");
        }
        return true;
    }
    function calbalance() {
        $("Balance3").innerText = eval($("Balance0").innerText) + eval($("Balance1").value) - eval($("Balance2").value);
    }
    function calbank() {
        $("Bank3").innerText = eval($("Bank0").innerText) + eval($("Bank1").value) - eval($("Bank2").value);
    }
    window.onload = function () {
        calbalance();
        calbank();
    }
</script>
</head>
<body style="text-align:center">
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" style="width: 750px">
                <tr>
                    <td style="height: 50px; text-align: center">
                        <span style="font-size: 16pt; font-family: 隶书">银行存款余额调节表</span>&nbsp;</td>
                </tr>
            </table>
        </div>
        <table id="EditorTable" cellpadding="0" cellspacing="0" style="width: 750px" runat="server">
            <tr>
                <td class="b t" style="height:29px; width: 25%; text-align: left">
                    银行日记账余额：</td>
                <td class="b t" style="width: 15%; text-align: right">
                    <asp:Label ID="Balance0" runat="server"></asp:Label></td>
                <td class="b t r" style="width: 10%; text-align: right">&nbsp;</td>
                <td class="b t" style="width: 10%; text-align: right">&nbsp;</td>
                <td class="b t" style="width: 25%; text-align: left">
                    银行对账单余额：</td>
                <td class="b t" style="width: 15%; text-align: right">
                    <asp:Label ID="Bank0" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="b" style="height:29px; text-align: left">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;加：银行已收、单位未收款</td>
                <td class="b" style="text-align: right">
                    <asp:TextBox ID="Balance1" runat="server" CssClass="txtnum"></asp:TextBox></td>
                <td class="b r">&nbsp;</td>
                <td class="b">&nbsp;</td>
                <td class="b" style="text-align: left">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;加：单位已收、银行未收款</td>
                <td class="b" style="text-align: right">
                    <asp:TextBox ID="Bank1" runat="server" CssClass="txtnum"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="b" style="height:29px; text-align: left">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;减：银行已付、单位未付款</td>
                <td class="b" style="text-align: right">
                    <asp:TextBox ID="Balance2" runat="server" CssClass="txtnum"></asp:TextBox></td>
                <td class="b r">&nbsp;</td>
                <td class="b">&nbsp;</td>
                <td class="b" style="text-align: left">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;减：单位已付、银行付收款</td>
                <td class="b" style="text-align: right">
                    <asp:TextBox ID="Bank2" runat="server" CssClass="txtnum"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="b" style="height:29px; text-align: left">
                    调节后余额（本单位）：</td>
                <td class="b" style="text-align: right">
                    &nbsp;<asp:Label ID="Balance3" runat="server"></asp:Label></td>
                <td class="b r">&nbsp;</td>
                <td class="b">&nbsp;</td>
                <td class="b" style="text-align: left">
                    调节后余额（银行）：</td>
                <td class="b" style="text-align: right">
                    &nbsp;<asp:Label ID="Bank3" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="b" colspan="6" style="height: 60px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="保存" Width="180px" Height="30px" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
