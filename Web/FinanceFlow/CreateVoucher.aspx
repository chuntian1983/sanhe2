<%@ Page Language="C#" AutoEventWireup="true" Inherits="FinanceFlow_CreateVoucher" Codebehind="CreateVoucher.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>生成凭证</title>
<base target="_self" />
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function SelectItem(t)
{
    var a=(t==2?"&filter=101|102&hidelock=1":"");
    var returnV=window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=1"+a+"&g="+(new Date()).getTime(),"","dialogWidth=360px;dialogHeight:400px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        switch(t)
        {
            case 1:
                $("DebitSubject").value=returnV[1]+"."+returnV[0];
                break;
            case 2:
                $("CreditSubject").value=returnV[1]+"."+returnV[0];
                break;
        }
    }
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
<body onunload="window.returnValue=$('VoucherID').value;">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 400px">
            <tr>
                <td class="t2" style="height: 38px; text-align: center" colspan="4">
                    <span id="DivTitle" style="font-size: 16pt; font-family: 隶书" runat="server">生成凭证</span>
                </td>
            </tr>
            <tr>
                <td class="t1" style="width: 20%; height: 25px; text-align: center">
                    用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;途：</td>
                <td class="t1" style="width: 30%; height: 25px;">
                    <asp:TextBox ID="ApplyUsage" runat="server" Width="110px"></asp:TextBox></td>
                <td class="t1" style="width: 20%; height: 25px; text-align: center">
                    批复金额：</td>
                <td class="t2" style="width: 30%; height: 25px;">
                    <asp:TextBox ID="ReplyMoney" runat="server" Width="110px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: center">
                    借方科目：</td>
                <td class="t1" style="height: 25px">
                    <asp:TextBox ID="DebitSubject" runat="server" Width="110px"></asp:TextBox></td>
                <td class="t1" style="height: 25px; text-align: center">
                    贷方科目：</td>
                <td class="t2" style="height: 25px">
                    <asp:TextBox ID="CreditSubject" runat="server" Width="110px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="height: 25px; text-align: center">
                    摘&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;要：</td>
                <td class="t2" style="height: 25px;" colspan="3">
                    <asp:TextBox ID="Notes" runat="server" Width="309px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 40px; text-align: center">
                    <asp:Button ID="Button2" runat="server" Height="25px" Text="生成凭证" Width="120px" OnClick="Button2_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button4" type="button" style="width: 100px; height: 25px" value="关闭" onclick="window.close();" /></td>
            </tr>
        </table>
        <asp:HiddenField ID="VoucherID" runat="server" Value="000000" />
    </div>
    </form>
</body>
</html>
