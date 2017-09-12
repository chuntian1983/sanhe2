<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_JournalList" Codebehind="JournalList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
    function $(o) { return (typeof (o) == "object") ? o : document.getElementById(o); }
    function checkBook() {
        var r = window.showModalDialog("JournalBook.aspx?flag=<%=Request.QueryString["flag"] %>&g=" + (new Date()).getTime(), "", "dialogWidth=750px;dialogHeight:400px;center=yes;");
        if (r == "1") {
            __doPostBack('Button1', '');
        }
        return false;
    }
    function doCheck(u, d, n) {
        var r = window.showModalDialog(u + "?flag=<%=Request.QueryString["flag"] %>&id=" + d + "&bno=" + n + "&g=" + (new Date()).getTime(), "", "dialogWidth=750px;dialogHeight:490px;center=yes;");
        if (r == "1") {
            __doPostBack('Button1', '');
        }
        return false;
    }
    function showBill(url,bid) {
        window.showModalDialog(url + "?flag=<%=Request.QueryString["flag"] %>&id=" + bid + "&g=" + (new Date()).getTime(), "", "dialogWidth=501px;dialogHeight:480px;center=yes;");
        return false;
    }
    function startPrint(v,vid) {
        doCheck("JournalShow.aspx",vid,'');
        return false;
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="YearMonth" runat="server" />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书" id="PageTitle" runat="server">现金日记账管理</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 10%; height: 35px; text-align: right">
                    摘要：</td>
                <td class="t1" style="width: 35%; text-align: left">
                    &nbsp;&nbsp;<asp:TextBox ID="Notes" runat="server" BorderWidth="1px" Width="200px"></asp:TextBox></td>
                <td class="t1" style="width: 10%; text-align: right">
                    凭证字：</td>
                <td class="t1" style="width: 15%; text-align: left;">
                    &nbsp;
                    <asp:DropDownList ID="VoucherType" runat="server">
                        <asp:ListItem Selected="True" Value="-">全部</asp:ListItem>
                        <asp:ListItem Value="0">现付</asp:ListItem>
                        <asp:ListItem Value="1">现收</asp:ListItem>
                        <asp:ListItem Value="2">银付</asp:ListItem>
                        <asp:ListItem Value="3">银收</asp:ListItem>
                        <asp:ListItem Value="4">无</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t2" style="width: 30%; text-align: center">
                    <asp:RadioButtonList ID="PaiXu" runat="server" RepeatDirection="Horizontal"
                        Width="230px" AutoPostBack="True" OnSelectedIndexChanged="PaiXu_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="VoucherType asc,VoucherNo asc">按凭证字排序</asp:ListItem>
                        <asp:ListItem Value="DayNo asc">按当日序号排序</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="t3" style="text-align: right">
                    查询日期：</td>
                <td class="t3" style="text-align: left;">
                    &nbsp;&nbsp;<asp:TextBox ID="QSDay" runat="server" Width="95px"></asp:TextBox>&nbsp; -&nbsp;
                    <asp:TextBox ID="QEDay" runat="server" Width="95px"></asp:TextBox></td>
                <td class="t3" style="text-align: right;">
                    &nbsp;</td>
                <td class="t3" style="text-align: left;">
                    &nbsp;</td>
                <td class="t4" style="height: 35px; text-align: center; background:#f8f8f8">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Width="95px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button3" type="button" value="记账" onclick="checkBook();" style="width: 95px" />
                    <asp:LinkButton ID="doPostBackEvent" runat="server"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" Height="1px" OnRowDataBound="GridView1_RowDataBound"
            PageSize="10" Style="color: navy" Width="750px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="VoucherDate" HeaderText="凭证日期">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="VoucherType" HeaderText="凭证字号">
                    <ItemStyle Width="110px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DayNo" HeaderText="当日序号">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Notes" HeaderText="摘要">
                    <ItemStyle Width="220px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="VoucherNo" HeaderText="方向">
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="AccMoney" HeaderText="金额" HtmlEncode="false" DataFormatString="{0:f}">
                    <ItemStyle Width="70px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Handler" HeaderText="经手人">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="操作">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%# Bind("id") %>'>编辑</asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("id") %>' OnClick="btnDelete_Click">删除</asp:LinkButton>
                        <asp:LinkButton ID="btnShow" runat="server" CommandArgument='<%# Bind("id") %>'>打印</asp:LinkButton>
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
            <RowStyle Font-Size="10pt" Height="22px" />
            <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
            <PagerStyle BackColor="White" ForeColor="Olive" />
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" Height="25px" ForeColor="Navy" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
