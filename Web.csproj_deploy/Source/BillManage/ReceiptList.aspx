<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_ReceiptList" Codebehind="ReceiptList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
    function $(o) { return (typeof (o) == "object") ? o : document.getElementById(o); }
    function checkBook() {
        var r = window.showModalDialog("ReceiptBook.aspx?g=" + (new Date()).getTime(), "", "dialogWidth=552px;dialogHeight:350px;center=yes;");
        if (r == "1") {
            __doPostBack('Button1', '');
        }
        return false;
    }
    function doCheck(u, d, n) {
        var r = window.showModalDialog(u + "?bid=" + d + "&bno=" + n + "&g=" + (new Date()).getTime(), "", "dialogWidth=552px;dialogHeight:350px;center=yes;");
        if (r == "1") {
            __doPostBack('Button1', '');
        }
        return false;
    }
    function showBill(bid) {
        window.showModalDialog("ReceiptShow.aspx?bid=" + bid + "&g=" + (new Date()).getTime(), "", "dialogWidth=552px;dialogHeight:350px;center=yes;");
        return false;
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">收据管理</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 10%; height: 29px; text-align: center">
                    交款单位：</td>
                <td class="t1" style="width: 30%; height: 29px; text-align: center">
                    <asp:TextBox ID="PayUnit" runat="server" BorderWidth="1px" Width="177px"></asp:TextBox></td>
                <td class="t1" style="width: 10%; height: 29px; text-align: center">
                    交款方式：</td>
                <td class="t2" style="width: 50%; height: 29px; text-align: left;">
                    &nbsp;
                    <asp:DropDownList ID="PayType" runat="server">
                        <asp:ListItem Selected="True" Value="-">全部</asp:ListItem>
                        <asp:ListItem Value="0">现金</asp:ListItem>
                        <asp:ListItem Value="1">现金支票</asp:ListItem>
                        <asp:ListItem Value="2">转账支票</asp:ListItem>
                        <asp:ListItem Value="3">电汇凭证</asp:ListItem>
                        <asp:ListItem Value="4">贷记凭证</asp:ListItem>
                        <asp:ListItem Value="5">商业承兑汇票</asp:ListItem>
                        <asp:ListItem Value="6">银行承兑汇票</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t1" style="width: 10%; height: 29px; text-align: center">
                    收款日期：</td>
                <td class="t1" style="width: 30%; height: 29px; text-align: center">
                    <asp:TextBox ID="SDate" runat="server" Width="80px"></asp:TextBox>&nbsp;~
                    <asp:TextBox ID="EDate" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="t1" style="width: 10%; height: 29px; text-align: center">
                    当前状态：</td>
                <td class="t2" style="width: 50%; height: 29px; text-align: left;">
                    <asp:RadioButtonList ID="ReveiveState" runat="server" RepeatDirection="Horizontal" 
                        Width="339px">
                        <asp:ListItem Selected="True" Value="-">全部</asp:ListItem>
                        <asp:ListItem Value="0">已开</asp:ListItem>
                        <asp:ListItem Value="1">已换开发票</asp:ListItem>
                        <asp:ListItem Value="2">已并开发票</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="t4" style="height: 35px; text-align: center; background:#f8f8f8" colspan="4">
                    <input id="Button3" type="button" value="登记" onclick="checkBook();" style="width: 108px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Width="108px" />
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
                <asp:BoundField DataField="ReceiveNo" HeaderText="序号">
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ReceiveDate" HeaderText="收款日期">
                    <ItemStyle Width="65px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ReveiveState" HeaderText="当前状态">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PayType" HeaderText="交款方式">
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PayReason" HeaderText="交款原因">
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="PayUnit" HeaderText="交款单位">
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ReceiveMoney" HeaderText="收款金额" HtmlEncode="false" DataFormatString="{0:f}">
                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="InvoiceNo" HeaderText="票据号码">
                    <ItemStyle Width="85px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="操作">
                    <ItemStyle HorizontalAlign="Center" Width="110px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%# Bind("id") %>'>编辑</asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("id") %>' OnClick="btnDelete_Click">删除</asp:LinkButton>
                        <asp:LinkButton ID="btnShow" runat="server" CommandArgument='<%# Bind("id") %>'>查看</asp:LinkButton>
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
