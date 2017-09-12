<%@ Page Language="C#" MasterPageFile="~/view/MasterPage.master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="SanZi.Web.usertitle.Show" Title="显示页" %>
<%@ MasterType VirtualPath="~/view/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		auto_increment
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblTitleID" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		TitleName
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblTitleName" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		TitleDesc
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblTitleDesc" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		SubTime
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblSubTime" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		SubUID
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblSubUID" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		DelFlag
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblDelFlag" runat="server"></asp:Label>
	</td></tr>
</table>

</asp:Content>
