<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jjztbgl.aspx.cs" Inherits="SanZi.Web.zhaobiao.jjztbgl" %>

<%@ Register src="../UserControl/Pagination.ascx" tagname="Pagination" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>竞价招投标管理</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
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
    <table cellpadding="0" cellspacing="0" border="0" style=" width:880px">
    <tr><td class="style1">竞价招投标管理</td></tr>
    <tr>
    <td> 
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" 
            BorderWidth="1px" DataKeyNames="ID" 
            CellPadding="3" ForeColor="Black" GridLines="Vertical" Width="880px" 
            onrowdatabound="GridView1_RowDataBound" 
            AllowPaging="True" >
            <RowStyle BackColor="#F7F7DE" />
            <PagerSettings Visible="False" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                <asp:BoundField DataField="xmmc" HeaderText="项目名称" >
                <ItemStyle Width="14%" />
                </asp:BoundField>
                <asp:BoundField DataField="subTime" HeaderText="时间">
                <ItemStyle Width="13%" />
                </asp:BoundField>
                <asp:BoundField DataField="adress" HeaderText="地点">
                <ItemStyle Width="10%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="cyry" HeaderText="参加人员" >
                <ItemStyle Width="6%" />
                </asp:BoundField>
                <asp:BoundField DataField="zcr" HeaderText="主持人" >
                <ItemStyle Width="6%" />
                </asp:BoundField>
                <asp:BoundField DataField="cbr" HeaderText="唱标人">
                <ItemStyle Width="6%" />
                </asp:BoundField>
                <asp:BoundField DataField="jlr" HeaderText="记录人">
                <ItemStyle Width="6%" />
                </asp:BoundField>
                <asp:BoundField DataField="zynr" HeaderText="主要内容">
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
    <uc1:Pagination ID="pageBar" runat="server" PageSize="9"  OnPageIndexChanged="pageBar_PageIndexChanged" />
    </div>
    </form>
</body>
</html>
