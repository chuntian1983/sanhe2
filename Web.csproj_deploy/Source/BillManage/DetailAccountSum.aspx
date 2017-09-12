<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_DetailAccountSum" Codebehind="DetailAccountSum.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function setYear(o,v)
{
    var m=eval($(o).value)+v;
    $(o).value=m;
    $("ReportDate").value=$("ReportDate").value.replace(/\d{4}年/,m+"年");
    return false;
}
function setMonth(v)
{
    $("ReportDate").value=$("ReportDate").value.replace(/\d{2}月/,v+"月");
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="color:green; font-size: 18pt;">代管资金汇总表</span></td>
            </tr>
        </table>
        <br />
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; text-align: center; height: 30px;">
                    查询日期：</td>
                <td class="t1" style="width: 35%;">&nbsp;
                    <asp:TextBox ID="sdate" runat="server" Width="95"></asp:TextBox>
                    ～
                    <asp:TextBox ID="edate" runat="server" Width="95"></asp:TextBox>
                    </td>
                <td class="t1" style="width: 15%; text-align: center">
                    汇总单位：</td>
                <td class="t2" style="width: 35%; text-align: left;">&nbsp;
                    <asp:DropDownList ID="CollectUnit" runat="server">
                        <asp:ListItem Value="000000">选择汇总单位</asp:ListItem>
                        <asp:ListItem Value="XXXXXX">一级汇总单位</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 30px; text-align: center">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Width="150px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input id="Button4" onclick="window.open('../PrintWeb.html','','');" type="button" style="width:150px" value="打印报表" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="150px" /></td>
            </tr>
        </table>
        <hr style="width: 750px; color: red; height: 2px; text-align: left" />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt">
            <tr>
                <td style="width:33%; text-align: left">
                    &nbsp; <span style="color: red">核算单位：<asp:Label ID="AName" runat="server"></asp:Label></span></td>
                <td style="width:37%; text-align: left">
                    <span style="color: red">报表年月：<asp:Label ID="ReportDate" runat="server"></asp:Label></span></td>
                <td style="width:30%; text-align: right">
                    <span style="color: red">单位：元 &nbsp; </span></td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CaptionAlign="Left" CssClass="onlyborder"
            ShowHeader="False" Width="750px" OnRowDataBound="GridView1_RowDataBound">
            <PagerSettings Visible="False" />
            <RowStyle Font-Size="10pt" Height="21px" />
        </asp:GridView>
        <!--#include file="../AccountQuery/ReportBottom.aspx"-->
    </div>
    <asp:HiddenField ID="RowsCount" runat="server" Value="0" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
