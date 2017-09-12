<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountInit_SummaryManage" Codebehind="SummaryManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("Contents").value=="")
    {
      $("Contents").focus();
      alert("摘要内容不能为空！");
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
                    <span style="font-size: 16pt; font-family: 隶书">常用摘要维护</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t3" style="width: 15%; text-align: center; height: 33px;">
                    摘要内容：</td>
                <td class="t3" style="width: 50%; text-align: center; height: 33px;">
                    <asp:TextBox ID="Contents" runat="server" Width="300px"></asp:TextBox></td>
                <td class="t4" style="width: 35%; text-align: center; height: 33px;">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加摘要" /></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CaptionAlign="Left" OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDataBound="GridView1_RowDataBound" OnRowUpdating="GridView1_RowUpdating"
            Style="color: navy" Width="750px" AllowPaging="True" OnRowEditing="GridView1_RowEditing">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField HeaderText="摘要内容" DataField="Contents" >
                    <ItemStyle Width="400px" />
                    <ControlStyle Width="390px" />
                </asp:BoundField>
                <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                    <ItemStyle HorizontalAlign="Center" Width="180px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="删除">
                    <ItemStyle HorizontalAlign="Center" Width="160px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("id") %>'
                            CommandName="btnDelete" OnClick="btnDelete_Click">删除</asp:LinkButton>
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
        <asp:Label ID="ExeScript" runat="server"></asp:Label></div>
    </form>
</body>
</html>
