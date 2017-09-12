<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ctdjb.aspx.cs" Inherits="SanZi.Web.zhaobiao.ctdjb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>参投登记表</title>
    <link href="../Style.css" type="text/css" rel="stylesheet"/>
    <script src="../js/calendar2.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            font-size: x-large; line-height:60px;
            font-weight: bold;
            height: 50px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table  cellspacing="0" cellpadding="0" width="100%"  style="width:640px">
    <tr>
    <td colspan="5" class="style1">参投登记表</td>
    </tr>
    <tr><td colspan="3" align="left">项目名称：<asp:DropDownList 
            ID="ddlxmmc" runat="server" AutoPostBack="true" Width="186px" 
            onselectedindexchanged="ddlxmmc_SelectedIndexChanged">
        </asp:DropDownList>
        </td>
    <td colspan="2" align="right"><asp:TextBox ID="tbsubtime" 
            runat="server" onfocus="setday(this)" Width="119px"></asp:TextBox></td></tr>
    <tr><td colspan="5">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#E7E7FF"  BorderWidth="1px" 
           Width="660px">
            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                <asp:BoundField DataField="xh" HeaderText="序号" >
                <ItemStyle Width="88px" />
                </asp:BoundField>
                <asp:BoundField DataField="tbr" HeaderText="投标人（单位）" >
                <ItemStyle Width="168px" />
                </asp:BoundField>
                <asp:BoundField DataField="zizhi" HeaderText="资质" >
                <ItemStyle Width="148px" />
                </asp:BoundField>
                <asp:BoundField DataField="fzr" HeaderText="负责人" >
                <ItemStyle Width="118px" />
                </asp:BoundField>
                <asp:BoundField DataField="lxdh" HeaderText="联系电话" >
                <ItemStyle Width="118px" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
            <EmptyDataTemplate>
                <table  cellspacing="0" cellpadding="0" style="width:660px; border-color:#E7E7FF; border-width:1px;" border="1px" >
                <tr style="color:#F7F7F7;background-color:#4A3C8C;font-weight:bold;">
                <td style="width:88px">序号</td>
                <td style="width:168px">投标人（单位）</td>
                <td style="width:148px">资质</td>
                <td style="width:118px">负责人</td>
                <td style="width:118px">联系电话</td></tr></table>
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <AlternatingRowStyle BackColor="#F7F7F7" />
        </asp:GridView>
    </td></tr>
    <tr>
    <td>
        <asp:TextBox ID="tbxh" runat="server" Width="86px"></asp:TextBox></td>
        <td>
        <asp:TextBox ID="tbtbr" runat="server" Width="166px"></asp:TextBox></td>
        <td>
        <asp:TextBox ID="tbzizhi" runat="server" Width="146px"></asp:TextBox></td>
        <td>
        <asp:TextBox ID="tbfzr" runat="server" Width="116px"></asp:TextBox></td>
        <td>
        <asp:TextBox ID="tblxdh" runat="server" Width="116px"></asp:TextBox></td></tr>
        <tr><td style="height:40px" colspan="5">
            <asp:Button ID="btn_add" runat="server" Text="添加" onclick="btn_add_Click" /></td></tr>
    </table>
    </div>
    </form>
</body>
</html>
