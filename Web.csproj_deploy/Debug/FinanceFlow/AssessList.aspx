<%@ Page Language="C#" AutoEventWireup="true" Inherits="FinanceFlow_AssessList" Codebehind="AssessList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span id="PageTitle" style="font-size: 16pt; font-family: 隶书" runat="server">资金批复</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t2" colspan="5" style="height: 25px; background:#f6f6f6; text-align: center;">流程查询</td>
            </tr>
            <tr>
                <td class="t3" style="width: 15%; height: 33px; text-align: center;">
                    流程名称：</td>
                <td class="t3" style="width: 20%; height: 33px; text-align: center">
                    <asp:TextBox ID="FlowName" runat="server" Width="135px" MaxLength="50"></asp:TextBox></td>
                <td class="t3" style="width: 15%; text-align: center; height: 33px;">
                    流程开始日期：</td>
                <td class="t3" style="width: 20%; height: 33px; text-align: center;">
                    <asp:TextBox ID="FlowStartDate" runat="server" Width="135px" MaxLength="50"></asp:TextBox></td>
                <td class="t4" style="width: 30%; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询流程" Width="120px" />
                    <asp:DropDownList ID="FlowCurrent" runat="server" Visible="false">
                        <asp:ListItem Value="-">全部</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="FlowState" runat="server" Visible="false">
                        <asp:ListItem Value="-">全部</asp:ListItem>
                        <asp:ListItem Value="0">待申请</asp:ListItem>
                        <asp:ListItem Value="1">处理中</asp:ListItem>
                        <asp:ListItem Value="2">已完成</asp:ListItem>
                        <asp:ListItem Value="3">已否决</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" OnRowDataBound="GridView1_RowDataBound" Style="color: navy" Width="750px" PageSize="15" AllowPaging="True">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="FlowName" HeaderText="流程名称">
                    <ItemStyle Width="250px" />
                </asp:BoundField>
                <asp:BoundField DataField="FlowCurrent" HeaderText="当前步骤"  ReadOnly="True">
                    <ItemStyle Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="FlowStartDate" HeaderText="流程开始日期" ReadOnly="True">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="FlowState" HeaderText="流程状态" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="评估" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnAssess" runat="server" CommandArgument='<%# Bind("id") %>' OnClick="btnAssess_Click">评估</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
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
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
        </asp:GridView>
    </div>
    <asp:HiddenField ID="FlowType" runat="server" />
    </form>
</body>
</html>
