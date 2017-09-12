<%@ Page Language="C#" AutoEventWireup="true" Inherits="BillManage_BankStatementCheck" Codebehind="BankStatementCheck.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
    function $(o) { return (typeof (o) == "object") ? o : document.getElementById(o); }
    function checkBook() {
        var r = window.showModalDialog("BankStatement.aspx?g=" + (new Date()).getTime(), "", "dialogWidth=552px;dialogHeight:350px;center=yes;");
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
    function selSubject(ctl, f) {
        var returnV = window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=1" + f + "&g=" + (new Date()).getTime(), "", "dialogWidth=380px;dialogHeight=401px;center=yes;");
        if (typeof (returnV) != "undefined" && returnV[0] != "" && returnV[0] != "+" && returnV[0] != "-") {
            $(ctl).value = returnV[1] + "." + returnV[0];
        }
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span id="ReportTitle" runat="server" style="font-size: 16pt; font-family: 隶书">银行存款对账</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 10%; height: 29px; text-align: center">
                    结算日期：</td>
                <td class="t1" style="width: 28%; height: 29px; text-align: center">
                    <asp:TextBox ID="SDate" runat="server" Width="80px"></asp:TextBox>&nbsp;~
                    <asp:TextBox ID="EDate" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="t1" style="width: 10%; height: 29px; text-align: center">
                    结算号：</td>
                <td class="t1" style="width: 22%; height: 29px; text-align: center">
                    <asp:TextBox ID="SettleNo" runat="server" BorderWidth="1px" Width="136px"></asp:TextBox>
                </td>
                <td class="t1" style="width: 10%; height: 29px; text-align: center">
                    结算方式：</td>
                <td class="t2" style="width: 20%; height: 29px; text-align: center;">
                    <asp:DropDownList ID="SettleType" runat="server">
                        <asp:ListItem Selected="True" Value="-">全部</asp:ListItem>
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
                <td class="t3" style="text-align: center">
                    银行科目：</td>
                <td class="t3" style="text-align: center">
                    <asp:TextBox ID="SettleSubject" runat="server" BorderWidth="1px" Width="148px"></asp:TextBox>
                    <a href="#" onclick="$('SettleSubject').value=''">清除</a></td>
                <td class="t3" style="text-align: center">
                    凭证日期：</td>
                <td class="t3" style="text-align: left" colspan="2">
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="VSDate" runat="server" Width="95px"></asp:TextBox>&nbsp;~
                    <asp:TextBox ID="VEDate" runat="server" Width="95px"></asp:TextBox>
                </td>
                <td class="t4" style="height: 35px; text-align: center; background:#f8f8f8">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Width="108px" />
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="height: 35px; text-align: center; background:#f8f8f8; color:Red" colspan="4">对账条件</td>
                <td class="t2" style="height: 35px; text-align: center; background:#f8f8f8">
                    <asp:Button ID="CheckBankStatement0" runat="server" OnClick="CheckBankStatement_Click" Text="勾选对账通过" Width="100px" />
                    <asp:Button ID="CheckBankStatement1" runat="server" OnClick="CheckBankStatement_Click" Text="全部通过" Width="60px" />
                </td>
            </tr>
            <tr>
                <td class="t3" style="width: 32%; height: 29px; text-align: center">
                    <asp:CheckBox ID="CheckBox3" runat="server" Text="允许凭证日期相差" />
                    <asp:TextBox ID="SpanDays" runat="server" BorderWidth="1px" Width="40px" Height="16px" Text="0"></asp:TextBox>&nbsp;&nbsp;天
                </td>
                <td class="t3" style="width: 15%; text-align: center;">
                    <asp:CheckBox ID="SameUpdate" runat="server" Text="结算方式相同" /></td>
                <td class="t3" style="width: 15%; text-align: center;">
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="结算号相同" /></td>
                <td class="t3" style="width: 15%; text-align: center;">
                    <asp:CheckBox ID="CheckBox2" runat="server" Text="结算日期相同" /></td>
                <td class="t4" style="width: 23%; height: 35px; text-align: center; background:#f8f8f8">
                    <asp:Button ID="Button2" runat="server" OnClick="Button1_Click" Text="针对勾选对账" Width="100px" />
                    <asp:Button ID="Button3" runat="server" OnClick="Button1_Click" Text="自动对账" Width="60px" />
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center; background:#f8f8f8; color:Red">银行对账单</td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="False" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" Height="1px" 
            OnRowDataBound="GridView1_RowDataBound" Style="color: navy" Width="750px" 
            EnableModelValidation="True">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:TemplateField HeaderText="勾对">
                    <ItemTemplate>
                        <asp:CheckBox ID="SelChecked" runat="server" />
                        <asp:HiddenField ID="hidBillID" runat="server" Value='<%# Bind("ID") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="VoucherDate" HeaderText="凭证日期">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SettleDate" HeaderText="结算日期">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SettleNo" HeaderText="结算号">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SettleType" HeaderText="结算方式">
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SettleMoney" HeaderText="方向">
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SettleMoney" HeaderText="金额" HtmlEncode="false" DataFormatString="{0:f}">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="VSummary" HeaderText="摘要">
                    <ItemStyle Width="230px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <RowStyle Font-Size="10pt" Height="22px" />
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" Height="25px" ForeColor="Navy" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
        </asp:GridView>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center; background:#f8f8f8; color:Red">银行日记账</td>
            </tr>
        </table>
        <asp:GridView ID="GridView2" runat="server" AllowPaging="False" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" Height="1px" 
            OnRowDataBound="GridView1_RowDataBound" Style="color: navy" Width="750px" 
            EnableModelValidation="True">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:TemplateField HeaderText="勾对">
                    <ItemTemplate>
                        <asp:CheckBox ID="SelChecked" runat="server" />
                        <asp:HiddenField ID="hidBillID" runat="server" Value='<%# Bind("ID") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="VoucherDate" HeaderText="凭证日期">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SettleDate" HeaderText="结算日期">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SettleNo" HeaderText="结算号">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SettleType" HeaderText="结算方式">
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SumMoney" HeaderText="方向">
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SumMoney" HeaderText="金额" HtmlEncode="false" DataFormatString="{0:f}">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="VSummary" HeaderText="摘要">
                    <ItemStyle Width="230px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <RowStyle Font-Size="10pt" Height="22px" />
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" Height="25px" ForeColor="Navy" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
