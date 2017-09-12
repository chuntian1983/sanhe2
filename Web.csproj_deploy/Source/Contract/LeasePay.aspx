<%@ Page Language="C#" AutoEventWireup="true" Inherits="Contract_LeasePay" Codebehind="LeasePay.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>收款管理</title>
<base target="_self" />
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function ShowVoucher(vid)
{
   window.showModalDialog("../AccountManage/LookVoucher.aspx?id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=767px;dialogHeight=385px;center=yes;");
   return false;
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
window.onload = resetDialogSize;
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 400px">
            <tr>
                <td class="t1" style="height: 38px; text-align: center" colspan="4">
                    <span id="DivTitle" style="font-size: 16pt; font-family: 隶书" runat="server">租赁收款</span>
                </td>
            </tr>
            <tr>
                <td class="t1" style="text-align: center; height: 25px;">
                    收款日期：</td>
                <td class="t1" style="height: 25px;">
                    <asp:TextBox ID="PayDate" runat="server" BackColor="#F6F6F6" BorderWidth="1px" Width="110px"></asp:TextBox></td>
                <td class="t1" style="text-align: center; height: 25px;">
                    收款人：</td>
                <td class="t2" style="height: 25px;">
                    <asp:TextBox ID="PayUser" runat="server" BorderWidth="1px" Width="110px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="text-align: center; height: 25px;" id="TD_Name" runat="server">
                    租赁资源：</td>
                <td class="t2" style="height: 25px;" colspan="3">
                    <asp:TextBox ID="ResName" runat="server" Width="310px" BackColor="#F6F6F6" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: center">
                    收入科目：</td>
                <td class="t1" style="height: 25px">
                    <asp:TextBox ID="IncomeSubject" runat="server" BackColor="#F6F6F6" BorderWidth="1px" Width="110px" ReadOnly="True"></asp:TextBox></td>
                <td class="t1" style="height: 25px; text-align: center">
                    收款科目：</td>
                <td class="t2" style="height: 25px">
                    <asp:TextBox ID="PaySubject" runat="server" BackColor="#F6F6F6" BorderWidth="1px" Width="110px" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 20%; height: 25px; text-align: center">
                    支付批次：</td>
                <td class="t1" style="width: 30%; height: 25px;">
                    <asp:TextBox ID="PeriodName" runat="server" BackColor="#F6F6F6" BorderWidth="1px" ReadOnly="True" Width="110px"></asp:TextBox></td>
                <td class="t1" style="width: 20%; height: 25px; text-align: center">
                    收款金额：</td>
                <td class="t2" style="width: 30%; height: 25px;">
                    <asp:TextBox ID="PayMoney" runat="server" Width="110px" BorderWidth="1px" BackColor="#F6F6F6" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: center">
                    开始日期：</td>
                <td class="t1" style="height: 25px">
                    <asp:TextBox ID="SPay" runat="server" BackColor="#F6F6F6" BorderWidth="1px" Width="110px" ReadOnly="True"></asp:TextBox></td>
                <td class="t1" style="height: 25px; text-align: center">
                    结束日期：</td>
                <td class="t2" style="height: 25px">
                    <asp:TextBox ID="EPay" runat="server" BackColor="#F6F6F6" BorderWidth="1px" Width="110px" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: center">
                    收款备注：</td>
                <td class="t2" style="height: 25px;" colspan="3">
                    <asp:TextBox ID="Notes" runat="server" Height="50px" TextMode="MultiLine" Width="309px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 40px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Height="25px" Text="收款" Width="80px" OnClick="Button1_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Height="25px" Text="生成凭证" Width="80px" OnClick="Button2_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" Height="25px" Text="查看凭证" Width="80px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button4" type="button" style="width: 80px; height: 25px" value="关闭" onclick="window.close();" /></td>
            </tr>
        </table>
        <asp:HiddenField ID="PayState" runat="server" />
        <asp:HiddenField ID="CardType" runat="server" />
        <asp:HiddenField ID="VoucherID" runat="server" />
    </div>
    </form>
</body>
</html>
