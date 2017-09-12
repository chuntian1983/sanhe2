<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_CommDics" Codebehind="CommDics.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("ParaValue").value=="")
    {
      $("ParaValue").focus();
      alert("参数内容不能为空！");
      return false;
    }
    return true;
}
</script>
</head>
<body style="text-align:center">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 608px">
            <tr>
                <td class="b" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">系统参数设置</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 608px">
            <tr>
                <td class="t3" style="width: 15%; text-align: center; height: 33px;">
                    参数类别：</td>
                <td class="t3" style="text-align: center; width: 20%;">
                    <asp:DropDownList ID="ParaType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ParaType_SelectedIndexChanged">
                        <asp:ListItem Value="100001">村级职务</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="t3" style="width: 50%; text-align: center; height: 33px;">
                    <asp:TextBox ID="ParaValue" runat="server" Width="274px"></asp:TextBox></td>
                <td class="t4" style="width: 15%; text-align: center; height: 33px;">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加参数" /></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CaptionAlign="Left" OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDataBound="GridView1_RowDataBound" OnRowUpdating="GridView1_RowUpdating"
            Style="color: navy" Width="608px" AllowPaging="True" OnRowEditing="GridView1_RowEditing" PageSize="15">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="paraname" HeaderText="编号" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="参数类别" DataField="paratype" ReadOnly="True">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="paravalue" HeaderText="参数内容">
                    <ItemStyle Width="230px" HorizontalAlign="Left" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="删除">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("paraname") %>'
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
        </div>
    </form>
</body>
</html>
