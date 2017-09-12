<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_InvoiceList" Codebehind="InvoiceList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
    function $(o) { return (typeof (o) == "object") ? o : document.getElementById(o); }
    function checkBook() {
        var r = window.showModalDialog("InvoiceBook.aspx?g=" + (new Date()).getTime(), "", "dialogWidth=552px;dialogHeight:350px;center=yes;");
        if (r == "1") {
            __doPostBack('Button1', '');
        }
        return false;
    }
    function doCheck(u) {
        var r = window.showModalDialog(u + "&g=" + (new Date()).getTime(), "", "dialogWidth=552px;dialogHeight:350px;center=yes;");
        if (r == "1") {
            __doPostBack('Button1', '');
        }
        return false;
    }
    function showBill(bid) {
        window.showModalDialog("InvoiceShow.aspx?bid=" + bid + "&g=" + (new Date()).getTime(), "", "dialogWidth=552px;dialogHeight:350px;center=yes;");
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
                    <span style="font-size: 16pt; font-family: 隶书">发票管理</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 10%; height: 29px; text-align: center">
                    领购日期：</td>
                <td class="t1" style="width: 25%; height: 29px; text-align: center">
                    <asp:TextBox ID="SDate" runat="server" Width="80px"></asp:TextBox>&nbsp;~
                    <asp:TextBox ID="EDate" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="t1" style="width: 10%; height: 29px; text-align: center">
                    发票种类：</td>
                <td class="t1" style="width: 42%; height: 29px; text-align: left;">
                    <asp:RadioButtonList ID="InvoiceType" runat="server" RepeatDirection="Horizontal" 
                        Width="260px">
                        <asp:ListItem Selected="True" Value="-">全部</asp:ListItem>
                        <asp:ListItem Value="0">增值税专用发票</asp:ListItem>
                        <asp:ListItem Value="1">普通发票</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="t2" style="width: 13%; height: 29px; text-align: center">
                    <asp:CheckBox ID="OldTestState" runat="server" Text="已 验 旧" /></td>
            </tr>
            <tr>
                <td class="t1" style="height: 29px; text-align: center">
                    验旧提醒：</td>
                <td class="t1" style="height: 29px; text-align: center">
                    <asp:TextBox ID="VSDate" runat="server" Width="80px"></asp:TextBox>&nbsp;~
                    <asp:TextBox ID="VEDate" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="t1" style="height: 29px; text-align: center">
                    当前状态：</td>
                <td class="t2" style="height: 29px; text-align: left;" colspan="2">
                    <asp:RadioButtonList ID="InvoiceState" runat="server" RepeatDirection="Horizontal" 
                        Width="410px">
                        <asp:ListItem Selected="True" Value="-">全部</asp:ListItem>
                        <asp:ListItem Value="0">未使用</asp:ListItem>
                        <asp:ListItem Value="1">已开票</asp:ListItem>
                        <asp:ListItem Value="2">已作废</asp:ListItem>
                        <asp:ListItem Value="3">已丢失</asp:ListItem>
                        <asp:ListItem Value="4">红字发票</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="t4" style="height: 35px; text-align: center; background:#f8f8f8" colspan="5">
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
                <asp:BoundField DataField="InvoiceType" HeaderText="发票种类">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="InvoiceCode" HeaderText="发票代码">
                    <ItemStyle Width="110px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="InvoiceNo" HeaderText="发票号码">
                    <ItemStyle Width="110px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="InvoiceState" HeaderText="当前状态">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="BuyDate" HeaderText="购买日期">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="IsRedInvoice" HeaderText="红字">
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="操作">
                    <ItemStyle HorizontalAlign="Center" Width="280px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDoBill" runat="server" CommandArgument='<%# Bind("id") %>'>开票</asp:LinkButton>
                        <asp:LinkButton ID="btnSetRed" runat="server" CommandArgument='<%# Bind("IsRedInvoice") %>'>红字</asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" runat="server" CommandArgument='<%# Bind("id") %>' OnClick="btnCancel_Click">作废</asp:LinkButton>
                        <asp:LinkButton ID="btnLost" runat="server" CommandArgument='<%# Bind("id") %>' OnClick="btnLost_Click">丢失</asp:LinkButton>
                        <asp:LinkButton ID="btnOldTest" runat="server" CommandArgument='<%# Bind("id") %>' OnClick="btnOldTest_Click">验旧</asp:LinkButton>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%# Bind("id") %>'>编辑</asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("id") %>' OnClick="btnDelete_Click">删除</asp:LinkButton>
                        <asp:LinkButton ID="btnShow" runat="server" CommandArgument='<%# Bind("id") %>'>查看</asp:LinkButton>
                        <asp:HiddenField ID="hidInvoiceState" runat="server" Value='<%# Bind("InvoiceState") %>' />
                        <asp:HiddenField ID="hidOldTestState" runat="server" Value='<%# Bind("OldTestState") %>' />
                        <asp:HiddenField ID="hidInvoiceID" runat="server" Value='<%# Bind("ID") %>' />
                        <asp:HiddenField ID="hidInvoiceNo" runat="server" Value='<%# Bind("InvoiceNo") %>' />
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
