<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cyssgl.aspx.cs" Inherits="SanZi.Web.zhaobiao.cyssgl" %>

<%@ Register Src="../UserControl/Pagination.ascx" TagName="Pagination" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>村预算书管理</title>
    <link href="../Style.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            height: 59px;
            font-size: x-large; line-height:60px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table cellpadding="0" cellspacing="0" border="0" style="width: 780px">
            <tr>
                <td class="style1">
                    村预算书管理
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" DataKeyNames="ID"
                        CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="780px" OnRowDataBound="GridView1_RowDataBound">
                        <RowStyle BackColor="#F7F7DE" />
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                            <asp:BoundField DataField="Title" HeaderText="标题">
                                <ItemStyle Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="xmmc" HeaderText="项目名称">
                                <ItemStyle Width="25%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="xmsssj" HeaderText="项目实施时间">
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DanWei" HeaderText="单位">
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="subTime" HeaderText="提交时间">
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="编辑">
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="Edit" CommandArgument='<%# Bind("id") %>'>编辑</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" OnClick="Delete" CommandArgument='<%# Bind("id") %>'>删除</asp:LinkButton>
                                </ItemTemplate>
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
        <uc1:Pagination ID="pageBar" runat="server" PageSize="9" OnPageIndexChanged="pageBar_PageIndexChanged" />
    </div>
    </form>
</body>
</html>
