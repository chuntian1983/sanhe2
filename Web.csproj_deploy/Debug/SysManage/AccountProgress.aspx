<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_AccountProgress" Codebehind="AccountProgress.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function setYear(o,v)
{
    $(o).value=eval($(o).value)+v;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">做账进度查询</span></td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 30%; height: 22px; background-color: #f6f6f6; text-align: center">
                    选择所查乡镇
                </td>
                <td class="t1" style="text-align: center; background-color: #f6f6f6;">
                    进度列表
                </td>
                <td class="t1" style="text-align: center">
                    查询年度→
                </td>
                <td class="t2" style="text-align: center">
                    <asp:ImageButton ID="SMinus" runat="server" ImageUrl="~/Images/jian.gif" />
                    <asp:TextBox ID="SelYear" runat="server" BorderWidth="0px" Width="27px" Height="18px">2009</asp:TextBox>&nbsp;
                    <asp:ImageButton ID="SPlus" runat="server" ImageUrl="~/Images/jia.gif" />
                </td>
            </tr>
            <tr>
                <td class="t3" style="height: 380px" valign="top">
                    <div style="overflow-y: scroll; width: 100%; height: 100%">
                        <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ImageSet="XPFileExplorer"
                            NodeIndent="15" ShowLines="True">
                            <ParentNodeStyle Font-Bold="False" />
                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px"
                                VerticalPadding="0px" />
                            <Nodes>
                                <asp:TreeNode SelectAction="None" Text="一级科目" Value="000"></asp:TreeNode>
                            </Nodes>
                            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px"
                                NodeSpacing="0px" VerticalPadding="2px" />
                        </asp:TreeView>
                    </div>
                </td>
                <td class="t4" colspan="4" style="text-align: center; vertical-align:top">
                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        CaptionAlign="Left" OnRowDataBound="GridView1_RowDataBound" Style="color: navy; margin:2px" Width="520px">
                        <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
                        <Columns>
                            <asp:BoundField DataField="accountname" HeaderText="账套名称">
                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="startaccountdate" HeaderText="做账进度">
                                <ItemStyle Width="350px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="lastcarrydate" HeaderText="当前结转" >
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                        </Columns>
                        <RowStyle Font-Size="10pt" Height="22px" />
                        <PagerStyle BackColor="White" ForeColor="Olive" />
                        <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" />
                        <AlternatingRowStyle BackColor="#EBF0F6" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="TotalLevel" runat="server" Value="0" />
    </form>
</body>
</html>
