﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountCollect_SubjectCollect" Codebehind="SubjectCollect.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckQuery()
{
    if($("CollectUnit").value=="000000")
    {
        $("CollectUnit").focus();
        alert("请选择汇总单位！");
        return false;
    }
    if($("SubjectNo").value=="")
    {
        alert("请选择查询科目！");
        SelSubject()
        return false;
    }
    return true;
}
function SelSubject()
{
    var returnV=window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=0&f=0&g="+(new Date()).getTime(),"","dialogWidth=402px;dialogHeight:387px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        $("QSubject").value=returnV[0];
        $("SubjectNo").value=returnV[1];
    }
}
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
                    <span id="ReportTitle" style="color:green; font-size: 18pt;" runat="server">各村汇总表</span></td>
            </tr>
        </table>
        <br />
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 10%; text-align: right; height: 28px;">
                    查询日期：</td>
                <td class="t1" style="width: 20%; text-align:center">
                    <asp:ImageButton ID="SMinus" runat="server" ImageUrl="~/Images/jian.gif" />
                    <asp:TextBox ID="SelYear" runat="server" BorderWidth="0px" Width="27px" Height="18px">2009</asp:TextBox>&nbsp;
                    <asp:ImageButton ID="SPlus" runat="server" ImageUrl="~/Images/jia.gif" />
                    <asp:DropDownList ID="SelMonth" runat="server">
                    <asp:ListItem Value="01">01月</asp:ListItem>
                    <asp:ListItem Value="02">02月</asp:ListItem>
                    <asp:ListItem Value="03">03月</asp:ListItem>
                    <asp:ListItem Value="04">04月</asp:ListItem>
                    <asp:ListItem Value="05">05月</asp:ListItem>
                    <asp:ListItem Value="06">06月</asp:ListItem>
                    <asp:ListItem Value="07">07月</asp:ListItem>
                    <asp:ListItem Value="08">08月</asp:ListItem>
                    <asp:ListItem Value="09">09月</asp:ListItem>
                    <asp:ListItem Value="10">10月</asp:ListItem>
                    <asp:ListItem Value="11">11月</asp:ListItem>
                    <asp:ListItem Value="12">12月</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 10%; text-align: right">
                    汇总单位：</td>
                <td class="t1" style="width: 20%">
                    <asp:DropDownList ID="CollectUnit" runat="server">
                        <asp:ListItem Value="000000">选择汇总单位</asp:ListItem>
                        <asp:ListItem Value="XXXXXX">一级汇总单位</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 10%; text-align: right;">
                    查询科目：</td>
                <td class="t2" style="width: 30%; text-align: center">
                    <asp:TextBox ID="QSubject" runat="server" Width="213px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t4" colspan="6" style="height: 26px; text-align: center">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Width="200px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input id="Button4" onclick="window.open('../PrintWeb.html','','');" type="button" style="width:200px" value="打印报表" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="120px" /></td>
            </tr>
        </table>
        <hr style="width: 750px; color: green; height: 2px; text-align: left" />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt">
            <tr>
                <td style="width:41%; text-align: left">
                    &nbsp; <span style="color: red">核算单位：<asp:Label ID="AName" runat="server"></asp:Label></span></td>
                <td style="width:29%; text-align: left">
                    <span style="color: red">报表年月：<asp:TextBox ID="ReportDate" runat="server" BorderWidth="0px"
                        ForeColor="Red" Width="70px"></asp:TextBox></span></td>
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
    <asp:HiddenField ID="SubjectNo" runat="server" />
    <asp:HiddenField ID="GAccountList" runat="server" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>