<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ctdjbgl.aspx.cs" Inherits="SanZi.Web.zhaobiao.ctdjbgl" %>

<%@ Register src="../UserControl/Pagination.ascx" tagname="Pagination" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>参投登记表管理</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
    <style type="text/css">
        .style1
        {
            height: 60px;
            font-size: x-large; line-height:60px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table cellpadding="0" cellspacing="0" border="0" style=" width:680px">
    <tr><td class="style1">招标项目列表</td></tr>
    <tr>
    <td> 
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" 
            BorderWidth="1px" DataKeyNames="ID"
            CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="680px" 
            onrowcommand="GridView1_RowCommand">
            <RowStyle BackColor="#F7F7DE" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                <asp:BoundField DataField="xmmc" HeaderText="项目名称" >
                <ItemStyle Width="15%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                    <asp:LinkButton ID="lbctdj" runat="server" CausesValidation="False" Text="查看参投登记表"   CommandName="view" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                   <HeaderStyle Width="8%" />
                                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </td>
    </tr>
    </table>
    <uc1:Pagination ID="pageBar" runat="server" PageSize="9"  OnPageIndexChanged="pageBar_PageIndexChanged" />
    </div> 
    </form>
</body>
</html>
