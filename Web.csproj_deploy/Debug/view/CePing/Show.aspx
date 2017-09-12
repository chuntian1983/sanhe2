<%@ Page Language="C#" MasterPageFile="~/view/MasterPage.master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="SanZi.Web.ceping.Show" Title="显示页" %>
<%@ MasterType VirtualPath="~/view/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		auto_increment
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblID" runat="server"></asp:Label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		Evaluation
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblEvaluation" runat="server"></asp:Label>
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
		SubIP
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblSubIP" runat="server"></asp:Label>
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
		OptionChecked
	：</td>
	<td height="25" width="*" align="left">
		<asp:Label id="lblOptionChecked" runat="server"></asp:Label>
	</td></tr>
</table>

</asp:Content>
