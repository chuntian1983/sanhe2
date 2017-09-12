<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" Inherits="Contract_TurnFixedAsset" Codebehind="TurnFixedAsset.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function BookLeaseCard(btn,rid)
{
    window.showModalDialog("LeaseCard.aspx?ctype=0&rid="+rid+"&g="+(new Date()).getTime(),btn,"dialogWidth=750px;dialogHeight:480px;center=yes;");
    return false;
}
function ShowVoucher(stype,vid)
{
   window.showModalDialog("../FixedAsset/PrintFACard.aspx?id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=770px;dialogHeight=400px;center=yes;");
   return false;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">资产流转</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; text-align:center">
            <tr>
                <td class="t1" style="height: 25px;">
                    查询类型：</td>
                <td class="t1" style="height: 25px; text-align: left;">
                    <asp:DropDownList ID="QType" runat="server" OnSelectedIndexChanged="QType_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="0">按类别查询</asp:ListItem>
                        <asp:ListItem Value="1">按部门查询</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="height: 25px;">
                    类型列表：</td>
                <td class="t1" style="height: 25px; text-align: left;" colspan="3">
                    <asp:DropDownList ID="QList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="QList_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td class="t1" style="height: 25px">
                    卡片编号：</td>
                <td class="t2" style="height: 25px; text-align: left;">
                    <asp:TextBox ID="CardID" runat="server" Width="84px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 10%; height: 25px">
                    资产编号：</td>
                <td class="t1" style="width: 12%; height: 25px; text-align: left">
                    <asp:TextBox ID="AssetNo" runat="server" Width="84px"></asp:TextBox></td>
                <td class="t1" style="width: 12%; height: 25px">
                    资产名称：</td>
                <td class="t1" colspan="3" style="height: 25px; text-align: left">
                    <asp:TextBox ID="AssetName" runat="server" Width="174px"></asp:TextBox></td>
                <td class="t2" style="width: 10%; height: 25px; text-align: center">
                    输出类型：</td>
                <td class="t2" style="width: 10%; height: 25px; text-align: left;">
                    <asp:CheckBox ID="CheckBox2" runat="server" Checked="True" Text="分页显示" /></td>
            </tr>
            <tr>
                <td class="t4" colspan="8" style="height: 29px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Width="220px" />
                    &nbsp;&nbsp;&nbsp;
                    <input id="Button4" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印报表" style="width: 180px" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="220px" />
                </td>
            </tr>
        </table>
        <br />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt;">
            <tr>
                <td style="width:41%; text-align: left">
                    &nbsp;编制单位：<asp:Label ID="AName" runat="server"></asp:Label></td>
                <td style="width:29%; text-align: left">
                    报表年月：<asp:TextBox ID="ReportDate" runat="server" BorderWidth="0px" Width="70px"></asp:TextBox></td>
                <td style="width:30%; text-align: right">
                    单位：元&nbsp;&nbsp;</td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" Height="1px" OnRowDataBound="GridView1_RowDataBound"
            PageSize="18" Style="color: black;" BorderColor="#f6f6f6" Width="750px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="cardid" HeaderText="卡片编号">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="assetno" HeaderText="资产编号">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="assetname" HeaderText="资产名称">
                    <ItemStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="原值" DataField="oldprice" DataFormatString="{0:f}" HtmlEncode="False">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="数量或面积" DataField="aamount" DataFormatString="{0:f}" HtmlEncode="False">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="已流转" DataField="hasamount" DataFormatString="{0:f}" HtmlEncode="False">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="计量单位" DataField="aunit">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="查看">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnShow" runat="server" CommandArgument='<%# Bind("id") %>' OnClick="btnShow_Click">卡片</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="流转">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnTurn" runat="server" Text="流转" CommandArgument='<%# Bind("id") %>'></asp:LinkButton>
                        <asp:HiddenField ID="HasAmount" Value='<%# Bind("HasAmount") %>' runat="server" />
                        <asp:HiddenField ID="AAmount" Value='<%# Bind("AAmount") %>' runat="server" />
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
            <RowStyle Font-Size="10pt" Height="21px" />
            <SelectedRowStyle BackColor="#FFC0C0" BorderColor="Yellow" BorderStyle="Dotted" />
            <PagerStyle BackColor="White" ForeColor="Olive" />
            <HeaderStyle BackColor="#E0EFF6" Font-Size="10pt" ForeColor="black" Height="21px" />
            <AlternatingRowStyle BackColor="#F6FAFD" />
        </asp:GridView>
        <!--#include file="../AccountQuery/ReportBottom.aspx"-->
    </div>
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
