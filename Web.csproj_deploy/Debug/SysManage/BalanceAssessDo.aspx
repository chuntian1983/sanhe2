<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_BalanceAssessDo" Codebehind="BalanceAssessDo.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function OpenMonitor(aid,vid)
{
    window.showModalDialog("../AccountManage/LookVoucher.aspx?aid="+aid+"&id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=767px;dialogHeight=385px;center=yes;");
    return false;
}
var msg=parent.window.document.getElementById("ctl00_showmsg");
if(msg)
{
    msg.style.display="none";
}
function ShowApply(url)
{
    location.href=url;
    return false;
}
</script>
</head>
<body id="HBody">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">资产资源评估一览表</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 12%; height: 33px; text-align: right; font-size:10pt">
                    乡镇名称：</td>
                <td class="t1" style="width: 35%; height: 33px;">
                    &nbsp;<asp:TextBox ID="TownName" runat="server" Width="200px" MaxLength="50"></asp:TextBox></td>
                <td class="t1" style="font-size: 10pt; width: 10%; height: 33px; text-align: right">
                    评估状态：</td>
                <td class="t2" style="height: 33px; text-align: center">
                    <asp:RadioButtonList ID="DoState" runat="server" RepeatDirection="Horizontal" Width="228px">
                        <asp:ListItem Selected="True" Value="000000">所有</asp:ListItem>
                        <asp:ListItem Value="0">未评估</asp:ListItem>
                        <asp:ListItem Value="1">已评估</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="t3" style="width: 12%; text-align: right; height: 33px; font-size:10pt">
                    账套名称：</td>
                <td class="t3" style="width: 41%; height: 33px;">
                    &nbsp;<asp:TextBox ID="AccountName" runat="server" Width="200px" MaxLength="50"></asp:TextBox></td>
                <td class="t4" colspan="2" style="height: 33px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Width="150px" /></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left"
            OnRowDataBound="GridView1_RowDataBound"
            Style="color: navy" Width="750px" PageSize="15">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="UnitID" HeaderText="乡镇名称">
                    <ItemStyle Width="130px" />
                </asp:BoundField>
                <asp:BoundField DataField="AccountID" HeaderText="账套名称">
                    <ItemStyle Width="180px" />
                </asp:BoundField>
                <asp:BoundField DataField="VoucherNo" HeaderText="申请类型">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="VoucherDate" HeaderText="申请日期">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DoState" HeaderText="评估状态">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="VoucherID" HeaderText="评估日期">
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="评估">
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnAudit" runat="server" CommandName='<%# Bind("id") %>' CommandArgument='<%# Bind("AccountID") %>' OnClick="btnAudit_Click">评估</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle Font-Size="10pt" />
            <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
            <PagerStyle BackColor="White" ForeColor="Olive" />
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
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
        </asp:GridView>
      </div>
    </form>
</body>
</html>
