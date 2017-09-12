<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cpgl.aspx.cs" Inherits="SanZi.Web.CePing.cpgl" %>
<%@ Register TagPrefix="uc1" TagName="Pagination" Src="../UserControl/Pagination.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>网上测评管理</title>
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
    <table cellpadding="0" cellspacing="0" border="0" style=" width:660px">
    <tr><td class="style1">村务工作综合测评管理</td></tr>
    <tr>
    <td> 
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" 
            BorderWidth="1px" DataKeyNames="ID"
            CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="660px" 
            onrowdatabound="GridView1_RowDataBound" 
            onrowcommand="GridView1_RowCommand">
            <RowStyle BackColor="#F7F7DE" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                <asp:BoundField DataField="DeptName" HeaderText="单位名称" />
                <asp:BoundField DataField="OptionChecked" HeaderText="综合评价" />
                <asp:BoundField DataField="SubIP" HeaderText="提交人IP" />
                <asp:BoundField DataField="SubTime" HeaderText="提交时间" />
                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                    <asp:ImageButton ID="lbdel" runat="server" CausesValidation="False" ImageUrl="../Images/f2.gif" Width="8px" Height="8px" OnClientClick="return confirm('您确定要删除这条数据吗？')"  CommandName="del" CommandArgument='<%# Eval("ID") %>'
                                            Text=" 删除"></asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
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
