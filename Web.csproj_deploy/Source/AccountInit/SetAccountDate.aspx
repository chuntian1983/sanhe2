<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountInit_SetAccountDate" Codebehind="SetAccountDate.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>设置财务日期</title>
<base target="_self" />
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function OnCellClick(rowid,d)
{
    if($("curRowID").value!="")
    {
        $($("curRowID").value).style.backgroundColor="";
    }
    $(rowid).style.backgroundColor="red";
    $("curRowID").value=rowid;
    $("curDate").value=$("curDate").value.replace(/\d{2}日/,d+"日");
}
function _SetCarry()
{
    if($("LastMonthDay").value!=$("curDate").value)
    {
        alert("当前财务日期："+$("curDate").value+"，必须在当月最后一天方可结转！");
        return false;
    }
    return true;
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
function WinClose()
{
    if(dialogArguments=="1")
    {
        if($("curDate").value==$("CurSetDate").value)
        {
            if($("IsMonthCarry").value=="1")
            {
                window.returnValue=new Array($("curDate").value,"1");
            }
            else
            {
                window.returnValue=new Array($("curDate").value,"8");
            }
        }
        else
        {
            window.returnValue=new Array($("curDate").value,$("IsMonthCarry").value);
        }
    }
    else
    {
        window.returnValue=new Array($("CurSetDate").value,$("IsMonthCarry").value);
    }
}
window.onload = resetDialogSize;
</script>
</head>
<body class="margin00" onunload="WinClose();">
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 330px">
            <tr>
                <td class="t2" style="height: 25px; text-align: center">
                当前财务日期：<asp:TextBox ID="curDate" runat="server" BorderWidth="0px" Width="98px"></asp:TextBox></td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CaptionAlign="Left" ShowHeader="False" Width="330px">
            <PagerSettings Visible="False" />
            <RowStyle Font-Size="10pt" Height="21px" />
            <Columns>
                <asp:BoundField>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <table cellpadding="0" cellspacing="0" style="width: 330px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <asp:Button ID="SetDate" runat="server" Text="设置日期" Width="70px" OnClick="SetDate_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="SetCarry" runat="server" Text="设置并月末结转" Width="110px" OnClick="SetCarry_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="BackupDate" runat="server" Text="数据备份" Width="70px" OnClick="BackupDate_Click" />
                    &nbsp;&nbsp;
                    <input id="Button1" type="button" value="关闭" onclick="window.close();" /></td>
            </tr>
        </table>
        <asp:HiddenField ID="CurSetDate" runat="server" />
        <asp:HiddenField ID="IsMonthCarry" runat="server" Value="0" />
        <asp:HiddenField ID="curRowID" runat="server" />
        <asp:HiddenField ID="LastMonthDay" runat="server" />
        <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
