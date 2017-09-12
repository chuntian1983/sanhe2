<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_UserManage" Codebehind="UserManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>操作员管理</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
</head>
<body style="text-align:center">
    <form id="form1" runat="server">
    <div style="width:750px; text-align:left">
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">操作员管理</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t3" style="width: 20%; height: 29px; text-align: center; background:#f6f6f6">
                    <input id="Button2" type="button" value="添加操作员" onclick="location.href='AddUser.aspx';" style="width: 108px" /></td>
                <td class="t3" style="width: 12%; height: 29px; text-align: right">
                    真实姓名：</td>
                <td class="t3" style="width: 18%; height: 29px">
                    <asp:TextBox ID="RealName" runat="server" Width="150px"></asp:TextBox></td>
                <td class="t3" style="width: 12%; height: 29px; text-align: right">
                    登录名称：</td>
                <td class="t3" style="width: 18%; height: 29px">
                    <asp:TextBox ID="UserName" runat="server" Width="150px"></asp:TextBox></td>
                <td class="t4" style="width: 20%; height: 29px; text-align: center;">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查找操作员" Width="108px" /></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" Height="1px" OnRowDataBound="GridView1_RowDataBound"
            PageSize="15" Style="color: navy" Width="750px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="true">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="realname" HeaderText="真实姓名">
                    <ItemStyle Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="username" HeaderText="登录名称">
                    <ItemStyle Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="logincounts" HeaderText="登录次数" ReadOnly="True">
                    <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="lastlogin" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="最后登录" HtmlEncode="False" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="160px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="编辑">
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" CommandArgument='<%# Bind("id") %>'>编辑</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除">
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CommandArgument='<%# Bind("id") %>'>删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
                &nbsp;<asp:LinkButton ID="FirstPage" runat="server" Font-Size="10pt" OnClick="FirstPage_Click">首页</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="PreviousPage" runat="server" Font-Size="10pt" OnClick="PreviousPage_Click">上一页</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="NextPage" runat="server" Font-Size="10pt" OnClick="NextPage_Click">下一页</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="LastPage" runat="server" Font-Size="10pt" OnClick="LastPage_Click">尾页</asp:LinkButton>
                &nbsp;
                <asp:Label ID="ShowPageInfo" runat="server" Font-Size="10pt" Text="总页数："></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" Font-Size="10pt" ForeColor="Navy" Text="跳转到："></asp:Label>
                <asp:DropDownList ID="JumpPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="JumpPage_SelectedIndexChanged">
                </asp:DropDownList>
            </PagerTemplate>
            <RowStyle Font-Size="10pt" />
            <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
            <PagerStyle BackColor="White" ForeColor="Olive" />
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" HorizontalAlign="Center" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
