﻿<%@ Page Language="C#" MasterPageFile="~/view/MasterPage.master" AutoEventWireup="true" CodeBehind="Modify.aspx.cs" Inherits="SanZi.Web.pibankabarcode.Modify" Title="修改页" %>
<%@ MasterType VirtualPath="~/view/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		auto_increment
	：</td>
	<td height="25" width="*" align="left">
		<asp:label id="lblID" runat="server"></asp:label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		PID
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtPID" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		BarCode
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtBarCode" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		SubTime
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtSubTime" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		DelFlag
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtDelFlag" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" colspan="2"><div align="center">
		<asp:Button ID="btnUpdate" runat="server" Text="· 提交 ·" OnClick="btnUpdate_Click" ></asp:Button>
	</div></td></tr>
</table>

</asp:Content>
