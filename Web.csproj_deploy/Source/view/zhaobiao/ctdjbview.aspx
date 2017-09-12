<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ctdjbview.aspx.cs" Inherits="SanZi.Web.zhaobiao.ctdjbview" %>

<%@ Register src="../UserControl/Pagination.ascx" tagname="Pagination" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>管理参投登记表</title>
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
    <table cellpadding="0" cellspacing="0" border="0" style=" width:780px">
    <tr><td colspan="2" class="style1">参投登记表</td></tr>
    <tr id="trxm" runat="server"><td style="text-align: left">项目名称：<asp:Label ID="Labxmmc" runat="server" Text='<%# Eval("xmmc") %>'></asp:Label></td>
    <td style="text-align: right">
        <asp:Label ID="LabsunTime" runat="server" Text='<%# Eval("subTime") %>'></asp:Label></td></tr>
    <tr>
    <td colspan="2"> 
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" 
            BorderWidth="1px" DataKeyNames="ID"
            CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="780px" 
            onrowdatabound="GridView1_RowDataBound" 
            onrowcommand="GridView1_RowCommand">
            <RowStyle BackColor="#F7F7DE" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                <asp:BoundField DataField="xh" HeaderText="序号" >
                <ItemStyle Width="14%" />
                </asp:BoundField>
                <asp:BoundField DataField="tbr" HeaderText="投标人（单位）">
                <ItemStyle Width="13%" />
                </asp:BoundField>
                <asp:BoundField DataField="zizhi" HeaderText="资质">
                <ItemStyle Width="10%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="fzr" HeaderText="负责人" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="lxdh" HeaderText="联系电话" >
                <ItemStyle Width="10%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                    <asp:ImageButton ID="lbdel" runat="server" CausesValidation="False" ImageUrl="../Images/f2.gif" Width="8px" Height="8px" OnClientClick="return confirm('您确定要删除这条数据吗？')"  CommandName="del" CommandArgument='<%# Eval("ID") %>'
                                            Text=" 删除"></asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
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
    <asp:Label ID="LabxmID" runat="server" Visible="false"></asp:Label>
    </div>
    </form>
</body>
</html>
