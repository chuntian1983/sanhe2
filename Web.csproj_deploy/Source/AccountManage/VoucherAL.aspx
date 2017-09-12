<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountManage_VoucherAL" Codebehind="VoucherAL.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function SelectAll(ck)
{
    var gridView1 = document.getElementById("GridView1");
    var items = gridView1.getElementsByTagName("input");
    for(var i = 0; i < items.length; i++)
    {
        if(items[i].type=="checkbox"&&items[i].disabled=="")
        {
           items[i].checked=ck;
        }
    }
}
function _confirm(msg)
{
    var selFlag=false;
    var gridView1 = document.getElementById("GridView1");
    var items = gridView1.getElementsByTagName("input");
    for(var i = 0; i < items.length; i++)
    {
        if(items[i].type=="checkbox"&&items[i].checked)
        {
           selFlag=true;
           break;
        }
    }
    if(selFlag)
    {
        return confirm(msg);
    }
    else
    {
        alert("请选择需要审核的凭证！")
        return false;
    }
}
function ShowVoucher(vid)
{
   window.showModalDialog("LookVoucher.aspx?id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=767px;dialogHeight=385px;center=yes;");
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t4" style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">凭证审核 -- 列表</span>&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    凭证日期：</td>
                <td class="t1" style="width: 35%">
                    <asp:TextBox ID="QVDate" runat="server"></asp:TextBox></td>
                <td class="t1" style="width: 15%; text-align: right">
                    凭证编号：</td>
                <td class="t2" style="width: 35%">
                    <asp:TextBox ID="VoucherNo" runat="server" Width="71px"></asp:TextBox>&nbsp; ^^^^^
                    &nbsp;
                    <asp:TextBox ID="VoucherNo2" runat="server" Width="71px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 15%; text-align: right">
                    审核状态：</td>
                <td class="t1" style="width: 35%">
                    <asp:DropDownList ID="IsAuditing" runat="server">
                        <asp:ListItem Value="000000">审核状态</asp:ListItem>
                        <asp:ListItem Value="0">未审核</asp:ListItem>
                        <asp:ListItem Value="1">已审核</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 15%; text-align: right">
                    记账状态：</td>
                <td class="t2" style="width: 35%">
                    <asp:DropDownList ID="IsRecord" runat="server">
                        <asp:ListItem Value="000000">记账状态</asp:ListItem>
                        <asp:ListItem Value="0">未记账</asp:ListItem>
                        <asp:ListItem Value="1">已记账</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t4" colspan="4" style="height: 36px; text-align: center">
                    <asp:Button ID="Button3" runat="server" Height="25px" OnClick="Button3_Click" Text="查询凭证"
                        Width="150px" /></td>
            </tr>
        </table>
        <br /><table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t3" style="width: 15%; text-align: right; height: 31px;">
                    操作选项：</td>
                <td class="t3" style="width: 35%; height: 31px; text-align: center;">
                    <asp:Button ID="AuditVoucher" runat="server" Height="25px" OnClick="AuditVoucher_Click"
                        Text="凭证审核" />
                    &nbsp; &nbsp;
                    <input id="SelectAllID" type="checkbox" onclick="SelectAll(this.checked);" /><label for="SelectAllID">全选</label></td>
                <td class="t3" style="width: 15%; text-align: right; height: 31px;">
                    显示选项：</td>
                <td class="t4" style="width: 35%; height: 31px; text-align: center;">
                    <input id="Button2" type="button" value="单张凭证显示" onclick="location.href='VoucherAM.aspx';" />
                    &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="无分页显示" CommandArgument="0" Width="120px" />
                    </td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" PageSize="15" Style="color: navy"
            Width="750px" OnRowDataBound="GridView1_RowDataBound">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="voucherno" HeaderText="凭证编号">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="voucherdate" HeaderText="凭证日期">
                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="IsAuditing" HeaderText="审核状态">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="IsRecord" HeaderText="记账状态">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="操作">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="IsAuditing" runat="server" CommandName='<%# Bind("id") %>' CommandArgument='<%# Bind("IsAuditing") %>' OnClick="IsAuditing_Click">审核</asp:LinkButton>
                        <asp:HiddenField ID="IsHasAlarm" runat="server" Value='<%# Bind("IsHasAlarm") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="查看凭证">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LookVoucher" runat="server" CommandArgument='<%# Bind("id") %>'>查看凭证</asp:LinkButton>
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
        <asp:HiddenField ID="SelectID" runat="server" />
        <asp:HiddenField ID="QuotField" runat="server" />
        <asp:HiddenField ID="RecordRowIndex" runat="server" Value="0" />
        <asp:HiddenField ID="aDay" runat="server" Value="2" />
        <asp:HiddenField ID="aMonth" runat="server" Value="1" />
        <asp:HiddenField ID="aYear" runat="server" Value="2008" />
        <asp:HiddenField ID="tDay" runat="server" Value="20" />
        <asp:HiddenField ID="tMonth" runat="server" Value="5" />
        <asp:HiddenField ID="tYear" runat="server" Value="2008" />
        <asp:HiddenField ID="tWeek" runat="server" Value="一" />
        <asp:HiddenField ID="Alarms" runat="server" Value="1=2" />
        <asp:Label ID="ExeScript" runat="server" Text="Label"></asp:Label></div>
    </form>
    <script type="text/javascript" src="../Images/SelDate/popcalendar2.js"></script>
</body>
</html>
