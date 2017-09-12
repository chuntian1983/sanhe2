<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_AdminManage" Codebehind="AdminManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("RealName").value=="")
    {
      $("RealName").focus();
      alert("管理员姓名不能为空！");
      return false;
    }
    if($("UserName").value=="")
    {
      $("UserName").focus();
      alert("登录名称不能为空！");
      return false;
    }
    if($("Password").value=="")
    {
      $("Password").focus();
      alert("登录密码不能为空！");
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
                    <span style="font-size: 16pt; font-family: 隶书">管理员管理</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; height: 30px; text-align: right">
                    管理员姓名：</td>
                <td class="t1" style="width: 35%; height: 30px">
                    <asp:TextBox ID="RealName" runat="server" Width="252px"></asp:TextBox></td>
                <td class="t1" style="width: 15%; height: 30px; text-align: right">
                    登录名称：</td>
                <td class="t2" style="width: 35%; height: 30px">
                    <asp:TextBox ID="UserName" runat="server" Width="252px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; height: 30px; text-align: right">
                    登录密码：</td>
                <td class="t1" style="width: 35%; height: 30px">
                    <asp:TextBox ID="Password" runat="server" Width="252px">888888</asp:TextBox></td>
                <td class="t1" style="width: 15%; height: 30px; text-align: right">
                    从属单位：</td>
                <td class="t2" style="width: 35%; height: 30px">
                    <asp:DropDownList ID="UnitList" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 30px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加管理员" Width="150px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button3" onclick="location.href='UnitManage.aspx'" type="button" value="单位管理" style="width:150px" /></td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
            Style="color: navy" Width="750px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="realname" HeaderText="管理员姓名">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="username" HeaderText="登录名称">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="password" HeaderText="登录密码">
                    <ItemStyle Width="80px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="从属单位">
                    <EditItemTemplate>
                        <asp:DropDownList ID="UnitList" runat="server">
                        </asp:DropDownList>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("unitid") %>' Visible="False"></asp:Label>
                    </EditItemTemplate>
                    <ItemStyle Width="160px" />
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("unitid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="logincounts" HeaderText="登录次数" ReadOnly="True">
                    <ItemStyle Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="lastlogin" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    HeaderText="最后登录" HtmlEncode="False" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="删除">
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
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
