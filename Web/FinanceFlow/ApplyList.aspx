<%@ Page Language="C#" AutoEventWireup="true" Inherits="FinanceFlow_ApplyList" Codebehind="ApplyList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("FlowNameNew").value=="")
    {
      $("FlowNameNew").focus();
      alert("流程名称不能为空！");
      return false;
    }
    return true;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span id="PageTitle" style="font-size: 16pt; font-family: 隶书" runat="server">资金申请</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t2" colspan="5" style="height: 25px; background:#f6f6f6; text-align: center;">新建流程</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 33px; text-align: center;">
                    流程名称：</td>
                <td class="t1" colspan="3" style="height: 33px; text-align: center">
                    <asp:TextBox ID="FlowNameNew" runat="server" Width="398px" MaxLength="50"></asp:TextBox></td>
                <td class="t2" style="width: 30%; height: 33px; text-align: center">
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="创建流程" Width="120px" /></td>
            </tr>
            <tr>
                <td class="t2" colspan="5" style="height: 25px; background:#f6f6f6; text-align: center;">流程查询</td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 33px; text-align: center;">
                    流程名称：</td>
                <td class="t1" style="width: 20%; height: 33px; text-align: center">
                    <asp:TextBox ID="FlowName" runat="server" Width="135px" MaxLength="50"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: center; height: 33px;">
                    申请日期：</td>
                <td class="t1" style="width: 20%; height: 33px; text-align: center;">
                    <asp:TextBox ID="ApplyDate" runat="server" Width="135px" MaxLength="50"></asp:TextBox></td>
                <td class="t4" style="width: 30%; text-align: center" rowspan="2">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询流程" Width="120px" Height="36px" />
                </td>
            </tr>
            <tr>
                <td class="t3" style="height: 33px; text-align: center">
                    当前步骤：</td>
                <td class="t3">
                    &nbsp;<asp:DropDownList ID="FlowCurrent" runat="server">
                        <asp:ListItem Value="-">全部</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t3" style="text-align: center">
                    流程状态：</td>
                <td class="t3">
                    &nbsp;<asp:DropDownList ID="FlowState" runat="server">
                        <asp:ListItem Value="-">全部</asp:ListItem>
                        <asp:ListItem Value="0">待申请</asp:ListItem>
                        <asp:ListItem Value="1">处理中</asp:ListItem>
                        <asp:ListItem Value="2">已完成</asp:ListItem>
                        <asp:ListItem Value="3">已否决</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" OnRowDataBound="GridView1_RowDataBound" Style="color: navy" Width="750px" PageSize="15" AllowPaging="True">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="FlowName" HeaderText="流程名称">
                    <ItemStyle Width="300px" />
                </asp:BoundField>
                <asp:BoundField DataField="FlowCurrent" HeaderText="当前步骤"  ReadOnly="True">
                    <ItemStyle Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="ApplyDate" HeaderText="申请日期" ReadOnly="True">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="FlowState" HeaderText="流程状态" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="编辑" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%# Bind("id") %>' OnClick="btnEdit_Click">编辑</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("id") %>' OnClick="btnDelete_Click">删除</asp:LinkButton>
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
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
            <AlternatingRowStyle BackColor="#EBF0F6" />
        </asp:GridView>
    </div>
    <asp:HiddenField ID="FlowType" runat="server" />
    </form>
</body>
</html>
