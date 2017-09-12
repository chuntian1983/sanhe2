<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountInit_WriteBalance" Codebehind="WriteBalance.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" id="HideExeScript" src=""></script>
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
        else if(parseFloat($("MonthBalance").value)==0)
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
            if(reg.test($("CurSubjectNo").value))
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
function SubmitForm()
{
    var patrn=/^\d{1,15}(\.\d{1,4})?$/;
    if(!patrn.test($("MonthBalance").value))
    {
       $("MonthBalance").focus();
       alert("金额输入格式有误！提示：最多15位整数，4位小数。");
       return false;
    }
    $("MonthBalanceT").value=$("MonthBalance").value;
    return true;
}
function FormatFloat(v)
{
    var i=v.lastIndexOf(".");
    if(i==-1){return v+".00";}
    if(i==0){return v+"0";}
    return v;
}
function SelectSubject(n,v,t)
{
    if($("CurSelSubject").value==n&&t==0){return;}
    $("CurSelSubject").value=n;
    $("CurSubjectNo").value=v;
    $("HideExeScript").src="GetBalance.aspx?no="+v+"&g="+(new Date()).getTime();
}
window.onload = function()
{
    if($("CurSubjectNo").value!="")
    {
        SelectSubject($("CurSelSubject").value,$("CurSubjectNo").value,1);
    }
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">科目本年初始余额录入</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px;">
            <tr>
                <td class="t1" colspan="1" style="height: 22px; background-color: #f6f6f6; text-align: center; width: 36%;">科目选择
                </td>
                <td class="t2" colspan="4" style="height: 22px; background-color: #f6f6f6; text-align: center">
                    科目账式、本年初始余额录入</td>
            </tr>
            <tr>
                <td class="t3" rowspan="10" valign="top" style="height:380px">
                <div style="WIDTH: 100%; HEIGHT: 100%; overflow-y:scroll;">
                    <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ImageSet="XPFileExplorer"
                        NodeIndent="15" ShowLines="True">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>
                            <asp:TreeNode SelectAction="None" Text="一级科目" Value="000"></asp:TreeNode>
                        </Nodes>
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                            NodeSpacing="0px" VerticalPadding="2px" />
                    </asp:TreeView>
                 </div>
                </td>
                <td class="t1" style="width: 14%; text-align: right">
                    当前操作科目：</td>
                <td class="t2" style="width: 50%" colspan="3">
                    <asp:TextBox ID="CurSelSubject" runat="server" Width="255px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 14%; text-align: right">
                    科目账式类型：</td>
                <td class="t2" colspan="3" style="width: 50%">
                    <asp:TextBox ID="AccountTypeShow" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="255px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 14%; text-align: right">
                    本年初始余额：</td>
                <td class="t2" style="width: 50%" colspan="3">
                    <asp:DropDownList ID="BalanceType" runat="server">
                        <asp:ListItem Value="">平</asp:ListItem>
                        <asp:ListItem Value="+">借</asp:ListItem>
                        <asp:ListItem Value="-">贷</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="MonthBalance" runat="server" Width="212px">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" colspan="4" style="text-align: center; background-color: #f6f6f6; height: 28px;">数量金额账数据</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    数量：</td>
                <td class="t1" style="width: 25%">
                    <asp:TextBox ID="SCount" runat="server" Width="100px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    单位：</td>
                <td class="t2" style="width: 25%">
                    <asp:TextBox ID="SUnit" runat="server" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    规格：</td>
                <td class="t1" style="width: 25%">
                    <asp:TextBox ID="SType" runat="server" Width="100px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right;">
                    类别：</td>
                <td class="t2" style="width: 25%">
                    <asp:TextBox ID="SClass" runat="server" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t2" colspan="4" style="height: 32px">&nbsp;
                </td>
            </tr>
            <tr>
                <td class="t2" colspan="4" style="height: 26px; text-align: center">
                    <asp:Button ID="Button1" runat="server" Text="保存录入" Width="200px" OnClick="Button1_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" style="width: 184px" type="button" value="返回科目表维护" onclick="location.href='AccountSubject.aspx';" /></td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 120px; text-align: center">&nbsp;</td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="CurSubjectNo" runat="server" />
    <asp:HiddenField ID="AccountType" runat="server" Value="0" />
    <asp:HiddenField ID="HSubjectType" runat="server" Value="1" />
    <asp:HiddenField ID="MonthBalanceT" runat="server" Value="0" />
    <asp:HiddenField ID="CreditSubject" runat="server" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
