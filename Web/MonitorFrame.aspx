﻿<%@ Page Language="C#" MasterPageFile="~/MonitorPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" Inherits="_MonitorFrame" Codebehind="MonitorFrame.aspx.cs" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div style="text-align:right">
<iframe id="mFrame" name="mFrame" src="HomePage.aspx" style="width: 100%; height: 100%; border:0px;" frameborder="0" runat="server"></iframe>
</div>
</asp:Content>
