<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountQuery_QuarterReport" Codebehind="QuarterReport.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function OnSelChange()
{
    $("ReportDate").value=$("SelYear").options[$("SelYear").selectedIndex].text+$("SelMonth").options[$("SelMonth").selectedIndex].text;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="color:Blue; font-size: 20pt;">财 务 公 开 榜</span></td>
            </tr>
        </table>
        <br />
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t3" style="width: 10%; text-align: center; height: 31px;">查询日期：</td>
                <td class="t3" colspan="3" style="width: 30%; height: 31px; text-align: center;">
            <asp:DropDownList ID="SelYear" runat="server"></asp:DropDownList>
            <asp:DropDownList ID="SelMonth" runat="server">
                <asp:ListItem Value="3">一季度</asp:ListItem>
                <asp:ListItem Value="6">二季度</asp:ListItem>
                <asp:ListItem Value="9">三季度</asp:ListItem>
                <asp:ListItem Value="12">四季度</asp:ListItem>
            </asp:DropDownList></td>
            <td class="t4" colspan="2" style="width: 55%; text-align: center; height: 31px;">
            <asp:Button ID="Button1" runat="server" OnClick="QDataClick_Click" Text="查询" Width="100px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input id="Button4" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印报表" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="120px" /></td>
            </tr>
        </table>
        <hr style="width: 750px; color: green; height: 2px; text-align: left" />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt">
            <tr>
                <td style="width:40%; text-align: left">
                    &nbsp; <span style="color: red">编制单位：<asp:Label ID="AName" runat="server"></asp:Label></span></td>
                <td style="width:30%; text-align: left">
                    <span style="color: red">报表年月：</span><span style="color: blue"><asp:TextBox ID="ReportDate"
                        runat="server" BorderWidth="0px" ForeColor="Red" Width="111px"></asp:TextBox></span></td>
                <td style="width:30%; text-align: right">
                    <span style="color: red">单位：元&nbsp;&nbsp;</span></td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CaptionAlign="Left" CssClass="onlyborder" Width="750px" ShowHeader="False">
            <PagerSettings Visible="False" />
            <RowStyle Font-Size="10pt" Height="21px" />
        </asp:GridView>
        <!--#include file="ReportBottom.aspx"-->
    </div>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
