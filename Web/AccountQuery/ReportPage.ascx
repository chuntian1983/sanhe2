<%@ Control Language="C#" AutoEventWireup="true" Inherits="AccountQuery_ReportPage" Codebehind="ReportPage.ascx.cs" %>

<table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt;" id="TableHead01" runat="server" visible="false">
    <tr>
        <td style="height: 28px; text-align: center" colspan="3">
            <span id="ReportTitle" style="color:Blue; font-size: 18pt" runat="server">标题</span>
        </td>
    </tr>
    <tr><td style="height: 20px;"></td></tr>
    <tr>
        <td style="width:41%; text-align: left">
            &nbsp; <span style="color: red">编制单位：<asp:Label ID="AName1" runat="server"></asp:Label></span></td>
        <td style="width:29%; text-align: left">
            <span style="color: red">报表年月：<asp:Label ID="ReportDate" runat="server"></asp:Label></span></td>
        <td style="width:30%; text-align: right">
            <span style="color: red">单位：元&nbsp;&nbsp;</span></td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt" id="TableHead04" runat="server" visible="false">
    <tr>
        <td style="height: 35px; text-align:center;">
            <span style="font-size: 18pt; font-family: 隶书; color:Red">总&nbsp;&nbsp;&nbsp;&nbsp;账</span>
        </td>
    </tr>
    <tr>
        <td style="height: 22px; text-align:left; color:Blue">&nbsp;核算单位：<asp:Label ID="AName4" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="height: 28px; text-align: left; color:Blue">&nbsp;科目名称：<asp:Label ID="SubjectName" runat="server"></asp:Label></td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt;" id="TableHead02" runat="server" visible="false">
    <tr>
        <td style="height: 35px; text-align:center">
            <span id="MSubjectTitle" style="font-size: 18pt;text-decoration:underline;border-bottom:1 solid black" class="borderbottom" runat="server"></span>
        </td>
    </tr>
    <tr>
        <td style="height: 22px; text-align:left; color:Blue">&nbsp;核算单位：<asp:Label ID="AName2" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="height: 28px; text-align:left">
            <span id="DSubjectTitle" style="font-size: 10pt;text-decoration:underline;" class="borderbottom" runat="server"></span>
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0" style="width: 1004px; font-size:10pt" id="TableHead03" runat="server" visible="false">
    <tr>
        <td colspan="4" style="height: 28px; text-align: center">
            <span style="color:Blue; font-size: 18pt;">固 定 资 产 明 细 账</span></td>
    </tr>
    <tr>
        <td colspan="4" style="height: 20px; text-align: center">&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 30%">&nbsp;核算单位：<asp:Label ID="AName3" runat="server"></asp:Label></td>
        <td style="width: 30%">资产名称：<asp:Label ID="AssetName" runat="server"></asp:Label></td>
        <td style="width: 20%">资产编号：<asp:Label ID="AssetNo" runat="server"></asp:Label></td>
        <td style="width: 20%">期间：<asp:Label ID="QDateTime" runat="server"></asp:Label></td>
    </tr>
</table>
<asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CaptionAlign="Left" CssClass="onlyborder" Width="750px" ShowHeader="False">
<PagerSettings Visible="False" />
<RowStyle Font-Size="11pt" Height="21px" />
</asp:GridView>
<!--#include file="ReportBottom.aspx"-->
