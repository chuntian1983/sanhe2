<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountQuery_SuperQuery" Codebehind="SuperQuery.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>高级查询</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<base target="_self" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function OnWriteValue(o)
{
    if($("DropDownList1").value=="voucherdate")
    {
        popUpCalendar(o,document.forms[0].QValue,"yyyy年mm月dd日");
    }
}
function NoFunction(){}
function CommitQuery()
{
    window.returnValue=$("TextBox3").value;
    window.close();
}
function AddQuery()
{
    $("TextBox3").value+=$("DropDownList4").value+" "+ $("DropDownList3").value+" ";
    var a=">,>=,<,<=,=,!=".split(",");
    if($("DropDownList2").value=="6")
    {
        $("TextBox3").value+=$("DropDownList1").value+" like '%"+$("QValue").value+"%'";
    }
    else if($("DropDownList2").value=="6")
    {
        $("TextBox3").value+=$("DropDownList1").value+" not like '%"+$("QValue").value+"%'";
    }
    else
    {
        if($("DropDownList1").value=="voucheryear")
        {
            $("TextBox3").value+=" voucherdate like '%"+$("QValue").value+"%'";
        }
        else
        {
            $("TextBox3").value+=$("DropDownList1").value+" "+a[eval($("DropDownList2").value)]+" '"+$("QValue").value+"'";
        }
    }
}
window.onload=function (){
    $("TextBox3").value=window.dialogArguments;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">高级查询选项设置</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    连接符：</td>
                <td class="t1" style="width: 40%">
                    <asp:DropDownList ID="DropDownList3" runat="server">
                        <asp:ListItem Value="">无</asp:ListItem>
                        <asp:ListItem Value="And">并且</asp:ListItem>
                        <asp:ListItem Value="OR">或者</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 15%; text-align: right">
                    边界符：</td>
                <td class="t2" style="width: 35%">
                    <asp:DropDownList ID="DropDownList4" runat="server">
                        <asp:ListItem Value="">无</asp:ListItem>
                        <asp:ListItem>(</asp:ListItem>
                        <asp:ListItem>((</asp:ListItem>
                        <asp:ListItem>(((</asp:ListItem>
                        <asp:ListItem>)</asp:ListItem>
                        <asp:ListItem>))</asp:ListItem>
                        <asp:ListItem>)))</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    条件项：</td>
                <td class="t1" style="width: 40%">
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="subjectno">科目编号</asp:ListItem>
                        <asp:ListItem Value="voucherno">凭证编号</asp:ListItem>
                        <asp:ListItem Value="voucherdate">凭证日期</asp:ListItem>
                        <asp:ListItem Value="summoney">分录金额</asp:ListItem>
                        <asp:ListItem Value="dobill">制单人</asp:ListItem>
                        <asp:ListItem Value="assessor">审核人</asp:ListItem>
                        <asp:ListItem Value="vsummary">摘要</asp:ListItem>
                        <asp:ListItem Value="voucheryear">年度</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 15%; text-align: right">
                    操作符：</td>
                <td class="t2" style="width: 35%">
                    <asp:DropDownList ID="DropDownList2" runat="server">
                        <asp:ListItem Value="0">大于</asp:ListItem>
                        <asp:ListItem Value="1">大于等于</asp:ListItem>
                        <asp:ListItem Value="2">小于</asp:ListItem>
                        <asp:ListItem Value="3">小于等于</asp:ListItem>
                        <asp:ListItem Value="4">等于</asp:ListItem>
                        <asp:ListItem Value="5">不等于</asp:ListItem>
                        <asp:ListItem Value="6">包含</asp:ListItem>
                        <asp:ListItem Value="7">不包含</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    匹配值：</td>
                <td class="t2" colspan="3">
                    <asp:TextBox ID="QValue" runat="server" Width="505px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 37px; text-align: center">
                    <input id="Button1" type="button" value="添加查询组" onclick="AddQuery();" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" value="清除查询组" onclick="$('TextBox3').value='';" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button3" type="button" value="确认并关闭" style="width:125px" onclick="CommitQuery();" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button4" type="button" value="取消并关闭" style="width:125px" onclick="window.close();" /></td>
            </tr>
            <tr>
                <td class="t2" colspan="4" style="text-align: center; background:#f6f6f6">
                    已组合条件</td>
            </tr>
            <tr>
                <td class="t2" colspan="4">
                    <asp:TextBox ID="TextBox3" runat="server" Height="100px" TextMode="MultiLine" Width="590px" ReadOnly="True"></asp:TextBox></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
