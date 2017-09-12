<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountInit_PopWBalance" Codebehind="PopWBalance.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>科目本年初始余额录入</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<base target="_self" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function SelectAccountType()
{
    if($("AccountType").value=="1")
    {
        SetBalanceType();
        if(parseFloat($("MonthBalance").value)!=0&&$("BalanceType").selectedIndex==0)
        {
            alert("数量金额账不可直接选择平！");
        }
    }
    else
    {
        if($("BalanceType").selectedIndex==0)
        {
            $("MonthBalance").value="0";
        }
        else if(eval($("MonthBalance").value)==0)
        {
            $("BalanceType").selectedIndex=0;
        }
    }
}
function WriteBalance()
{
    SetBalanceType();
}
function SetBalanceType()
{
    if(parseFloat($("MonthBalance").value)==0)
    {
        $("BalanceType").selectedIndex=0;
    }
    else
    {
        var reg=new RegExp("^("+$("CreditSubject").value+")");
        if($("BalanceType").selectedIndex==0)
        {
            if(reg.test($("SubjectNo").value))
            {
                $("BalanceType").selectedIndex=2;
            }
            else
            {
                $("BalanceType").selectedIndex=1;
            }
        }
    }
}
function CheckWrite()
{
    if(event.keyCode==46&&$("MonthBalance").value.indexOf(".")>0)
    {
         return false;
    }
    else
    {
        if((event.keyCode<48||event.keyCode>57)&&event.keyCode!=46)
        {
             return false;
        }
        if($("MonthBalance").value=="0")
        {
            if (document.selection&&event.keyCode!=46)
            {
                return document.selection.createRange().text.length>0;
            }
        }
    }
}
function formatFloat(src)
{
    return Math.round(src*Math.pow(10, 2))/Math.pow(10, 2);
}
function AmountPrice()
{
    var patrn=/^\d{1,8}(\.\d{1,2})?$/;
    if(patrn.test($("SCount").value)&&patrn.test($("SPrice").value))
    {
        $("MonthBalance").value=formatFloat(eval($("SCount").value)*eval($("SPrice").value));
    }
    else
    {
        $("MonthBalance").value="0";
    }
    SetBalanceType();
}
function WinClose()
{
    var patrn=/^\d{1,15}(\.\d{1,4})?$/;
    if($("SaveFlag").value=="1" && patrn.test($("MonthBalance").value))
    {
        window.returnValue=new Array($("BalanceType").options[$("BalanceType").selectedIndex].innerText,FormatFloat($("MonthBalance").value));
    }
}
function SubmitForm()
{
    var patrn=/^\d{1,15}(\.\d{1,4})?$/;
    if(!patrn.test($("MonthBalance").value))
    {
       $("MonthBalance").focus();
       alert("金额输入格式有误！提示：最多15位整数，4位小数。");
       return false;
    }
    $("SaveFlag").value="1";
    return true;
}
function FormatFloat(v)
{
    var i=v.lastIndexOf(".");
    if(i==-1){return v+".00";}
    if(i==0){return v+"0";}
    return v;
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
<body onunload="WinClose();">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 400px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">科目本年初始余额录入</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 400px">
            <tr>
                <td class="t1" style="width: 30%; text-align: right">
                    科目代码：</td>
                <td class="t2" style="width: 70%" colspan="3">
                    <asp:TextBox ID="SubjectNo" runat="server" Width="252px" BorderWidth="0px" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 30%; text-align: right">
                    科目名称：</td>
                <td class="t2" style="width: 70%" colspan="3">
                    <asp:TextBox ID="SubjectName" runat="server" Width="252px" BorderWidth="0px" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="text-align: right">
                    科目属性：</td>
                <td class="t1">
                    <asp:TextBox ID="SubjectType" runat="server" BorderWidth="0px" ReadOnly="True" Width="100px"></asp:TextBox></td>
                <td class="t1" style="text-align: right">
                    账式类型：</td>
                <td class="t2">
                    <asp:TextBox ID="AccountTypeShow" runat="server" BorderWidth="0px" ReadOnly="True" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" colspan="4" style="height: 22px; background-color: #f6f6f6; text-align: center">
                    科目账式、本年初始余额录入</td>
            </tr>
            <tr>
                <td class="t1" style="width: 30%; text-align: right">
                    本年初始余额：</td>
                <td class="t2" style="width: 70%" colspan="3">
                    <asp:DropDownList ID="BalanceType" runat="server">
                        <asp:ListItem Value="0">平</asp:ListItem>
                        <asp:ListItem Value="+">借</asp:ListItem>
                        <asp:ListItem Value="-">贷</asp:ListItem>
                    </asp:DropDownList><asp:TextBox ID="MonthBalance" runat="server" Width="152px">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" colspan="4" style="height: 22px; background-color: #f6f6f6; text-align: center">
                    数量金额账数据</td>
            </tr>
            <tr>
                <td class="t1" style="width: 30%; text-align: right">
                    数量：</td>
                <td class="t1" style="width: 30%">
                    <asp:TextBox ID="SCount" runat="server" Width="100px">0</asp:TextBox></td>
                <td class="t1" style="width: 30%; text-align: right; height: 22px;">
                    单位：</td>
                <td class="t2" style="width: 30%; height: 22px;">
                    <asp:TextBox ID="SUnit" runat="server" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 30%; text-align: right; height: 22px;">
                    规格：</td>
                <td class="t1" style="width: 30%; height: 22px;">
                    <asp:TextBox ID="SType" runat="server" Width="100px"></asp:TextBox></td>
                <td class="t1" style="width: 30%; text-align: right">
                    类别：</td>
                <td class="t2" colspan="3" style="width: 70%;　height: 22px">
                    <asp:TextBox ID="SClass" runat="server" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" colspan="4" style="background-color: #f6f6f6">&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 37px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="保存录入" Width="90px" OnClick="Button1_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" Text="保存录入并关闭" Width="113px" OnClick="Button1_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" value="关闭窗口" onclick="window.close();" /></td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HSubjectType" runat="server" />
    <asp:HiddenField ID="SaveFlag" runat="server" Value="0" />
    <asp:HiddenField ID="AccountType" runat="server" Value="0" />
    <asp:HiddenField ID="CreditSubject" runat="server" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
