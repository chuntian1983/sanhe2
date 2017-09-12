<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_CTLLogManage" Codebehind="CTLLogManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">操作日志查询</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 12%; height: 29px; text-align: center">开始日期：</td>
                <td class="t1" style="width: 18%; height: 29px; text-align: center;">
                    <asp:TextBox ID="QSDate" runat="server" BorderWidth="1px" ForeColor="black" Width="110px"></asp:TextBox></td>
                <td class="t1" style="width: 12%; height: 29px; text-align: center">结束日期：</td>
                <td class="t1" style="width: 18%; height: 29px; text-align: center;">
                    <asp:TextBox ID="QEDate" runat="server" BorderWidth="1px" ForeColor="black" Width="110px"></asp:TextBox></td>
                <td class="t1" style="text-align: center" rowspan="2">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查找日志" Width="95px" /></td>
            </tr>
            <tr>
                <td class="t3" style="width: 12%; height: 29px; text-align: center">真实姓名：</td>
                <td class="t3" style="width: 18%; height: 29px; text-align: center;">
                    <asp:TextBox ID="RealName" runat="server" Width="110px" BorderWidth="1px"></asp:TextBox></td>
                <td class="t3" style="width: 12%; height: 29px; text-align: center">登录名称：</td>
                <td class="t3" style="width: 18%; height: 29px; text-align: center;">
                    <asp:TextBox ID="UserName" runat="server" Width="110px" BorderWidth="1px"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" Height="1px" OnRowDataBound="GridView1_RowDataBound"
            PageSize="18" Style="color: navy" Width="750px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="编号">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="loguser" HeaderText="姓名">
                    <ItemStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="logcontent" HeaderText="日志内容" ReadOnly="True">
                    <ItemStyle Width="270px" />
                </asp:BoundField>
                <asp:BoundField DataField="loguid" HeaderText="操作单位">
                    <ItemStyle Width="130px" />
                </asp:BoundField>
                <asp:BoundField DataField="logtime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    HeaderText="操作时间" HtmlEncode="False" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="logdefine2" HeaderText="操作IP">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
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
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
