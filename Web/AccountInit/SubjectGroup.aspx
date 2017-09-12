<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountInit_SubjectGroup" Codebehind="SubjectGroup.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>期初余额录入</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<base target="_self" />
<script type="text/javascript" id="HideExeScript" src=""></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckAdd()
{
    if($("GroupName").value=="")
    {
        alert("请填写分组名称！");
        $("GroupName").focus();
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
                    <span style="font-size: 16pt; font-family: 隶书">科目表分组</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px;">
            <tr>
                <td class="t1" colspan="3" style="height: 22px; background-color: #f6f6f6; text-align: center">分组添加</td>
                <td class="t1" colspan="1" style="width: 2%; height: 22px; background-color: #f6f6f6; text-align: center">&nbsp;</td>
                <td class="t2" colspan="1" style="height: 22px; background-color: #f6f6f6; text-align: center; width: 33%;">科目选择</td>
            </tr>
            <tr>
                <td class="t1" style="width: 10%; text-align: center; height:22px">分组名称：</td>
                <td class="t1" style="width: 20%; text-align: center;"><asp:TextBox ID="GroupName" runat="server"></asp:TextBox></td>
                <td class="t1" style="width: 25%; text-align: center; height:28px">
                    <asp:Button ID="Button1" runat="server" Text="添加分组" OnClick="Button1_Click" />
                    &nbsp;&nbsp;
                    <input id="Button2" type="button" value="科目表维护" onclick="location.href='AccountSubject.aspx';" />
                </td>
                <td class="t3" rowspan="3" style="height: 380px" valign="top">&nbsp;</td>
                <td class="t4" rowspan="3" valign="top" style="height:380px">
                <div style="WIDTH: 100%; HEIGHT: 100%; overflow-y:scroll;">
                    <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ImageSet="XPFileExplorer"
                        NodeIndent="15" ShowLines="True" ShowCheckBoxes="All">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <Nodes>
                            <asp:TreeNode Text="一级科目" Value="000" ShowCheckBox="False"></asp:TreeNode>
                        </Nodes>
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                            NodeSpacing="0px" VerticalPadding="2px" />
                    </asp:TreeView>
                 </div>
                </td>
            </tr>
            <tr>
                <td class="t1" colspan="3" style="text-align: center; background-color: #f6f6f6; height:22px">分组管理</td>
            </tr>
            <tr>
                <td class="t3" colspan="3" style="text-align: center; height: 308px; vertical-align:top">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CaptionAlign="Left" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                        OnRowDataBound="GridView1_RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                        Style="color: navy; margin-top:2px" Width="100%">
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
                        <Columns>
                            <asp:BoundField DataField="groupname" HeaderText="分组名称">
                                <ItemStyle Width="290px" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:CommandField HeaderText="编辑" ShowEditButton="True">
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Bind("id") %>'
                                        CommandName="btnDelete" OnClick="btnDelete_Click">删除</asp:LinkButton>
                                    <asp:HiddenField ID="GroupSubjectNo" runat="server" Value='<%# Bind("SubjectNo") %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
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
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
