<%@ Page Language="C#" AutoEventWireup="true" Inherits="SysManage_SetBeiYongJin" Codebehind="SetBeiYongJin.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
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
                    <span style="font-size: 16pt; font-family: 隶书">备用金设置</span>
                </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CaptionAlign="Left" Height="1px" OnRowDataBound="GridView1_RowDataBound"
            Style="color: navy" Width="750px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="accountname" HeaderText="账套名称" ReadOnly="true">
                    <ItemStyle Width="650px" HorizontalAlign="Left" />
                    <ControlStyle Width="95%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="备用金">
                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                    <ItemTemplate>
                        <asp:TextBox ID="Define3" runat="server" Width="100px" Text='<%# Bind("Define3") %>'></asp:TextBox>
                    </ItemTemplate>
                    <ControlStyle Width="95%" />
                </asp:TemplateField>
            </Columns>
            <RowStyle Font-Size="10pt" Height="20px" />
            <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
            <PagerStyle BackColor="White" ForeColor="Olive" />
            <HeaderStyle BackColor="#F0F0F0" BorderColor="Red" Font-Size="10pt" ForeColor="Navy" HorizontalAlign="Center" Height="20px" />
        </asp:GridView>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 770px">
            <tr>
                <td class="t4" style="height: 29px; text-align: center;" colspan="4">
                    <asp:Button ID="btnSaveCheckFill" runat="server" OnClick="btnSaveCheckFill_Click" Text="- 保存 -" Width="150px" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
