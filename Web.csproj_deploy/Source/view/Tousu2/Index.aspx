<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SanZi.Web.tousu2.tsgl" %>
<%@ Register TagPrefix="uc1" TagName="Pagination" Src="../UserControl/Pagination.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>举报管理</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table style="width:500px;border:1px solid #000800;margin-top:5px;background:#000800;" cellspacing="1" cellpadding="3" >
    <tr style="background:#ffffff;">
        <td class="tableTitle2">举报管理</td>
    </tr>
    <tr style="background:#ffffff;">
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
                    <asp:BoundField DataField="DeptName" HeaderText="举报部门" >
                    <ItemStyle Width="20%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Content" HeaderText="举报内容" >
                    <ItemStyle Width="25%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SubIP" HeaderText="举报人IP" >
                    <ItemStyle Width="20%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SubTime" HeaderText="举报时间" >
                    <ItemStyle Width="25%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:ImageButton ID="lbdel" runat="server" CausesValidation="False" ToolTip="删除" ImageUrl="../Images/f2.gif" Width="8px" Height="8px" OnClientClick="return confirm('您确定要删除这条数据吗？')"  CommandName="del" CommandArgument='<%# Eval("ID") %>'
                                    Text=" 删除"></asp:ImageButton>
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
