<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" Inherits="ResManage_ResourceList" Codebehind="ResourceList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function BookLeaseCard(btn,rid)
{
    window.showModalDialog("../Contract/LeaseCard.aspx?ctype=1&rid="+rid+"&g="+(new Date()).getTime(),btn,"dialogWidth=750px;dialogHeight:480px;center=yes;");
    return false;
}
function ShowVoucher(stype,vid)
{
   window.showModalDialog("ResourceCardShow.aspx?id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=750px;dialogHeight=400px;center=yes;");
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
                    <span style="font-size: 16pt; font-family: 隶书">资源管理</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; text-align:center">
            <tr>
                <td class="t1" style="width: 10%; height: 25px;">
                    查询类型：</td>
                <td class="t1" style="width: 23%; height: 25px; text-align: left;">
                    <asp:DropDownList ID="QType" runat="server" OnSelectedIndexChanged="QType_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="0">按类别查询</asp:ListItem>
                        <asp:ListItem Value="1">按部门查询</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 10%; height: 25px;">
                    类型列表：</td>
                <td class="t1" style="width: 23%; height: 25px; text-align: left;">
                    <asp:DropDownList ID="QList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="QList_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td class="t1" style="width: 10%; height: 25px">
                    增加方式：</td>
                <td class="t2" style="width: 23%; height: 25px; text-align: left;">
                    <asp:DropDownList ID="BookType" runat="server">
                        <asp:ListItem>不限</asp:ListItem>
                        <asp:ListItem>转包</asp:ListItem>
                        <asp:ListItem>互换</asp:ListItem>
                        <asp:ListItem>入股</asp:ListItem>
                        <asp:ListItem>合作</asp:ListItem>
                        <asp:ListItem>借用</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t1" style="width: 10%; height: 25px">
                    卡片编号：</td>
                <td class="t1" style="width: 23%; height: 25px; text-align: left">
                    <asp:TextBox ID="CardID" runat="server" Width="84px"></asp:TextBox></td>
                <td class="t1" style="width: 10%; height: 25px">
                    资源编号：</td>
                <td class="t1" style="width: 23%; height: 25px; text-align: left;">
                    <asp:TextBox ID="AssetNo" runat="server" Width="84px"></asp:TextBox></td>
                <td class="t1" style="width: 10%; height: 25px; text-align: center">
                    资源名称：</td>
                <td class="t2" style="width: 23%; height: 25px; text-align: left;">
                    <asp:TextBox ID="AssetName" runat="server" Width="160px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 10%; height: 25px">
                    开始日期：</td>
                <td class="t1" style="width: 23%; height: 25px; text-align: left">
                    <asp:TextBox ID="SLease" runat="server" BackColor="#F6F6F6" BorderWidth="1px" Width="84px"></asp:TextBox>
                    <asp:DropDownList ID="Relation0" runat="server">
                        <asp:ListItem Value="0">等于</asp:ListItem>
                        <asp:ListItem Value="1">不等于</asp:ListItem>
                        <asp:ListItem Value="2">大于</asp:ListItem>
                        <asp:ListItem Value="3">不大于</asp:ListItem>
                        <asp:ListItem Value="4">小于</asp:ListItem>
                        <asp:ListItem Selected="True" Value="5">不小于</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 10%; height: 25px">
                    终止日期：</td>
                <td class="t1" style="width: 23%; height: 25px; text-align: left">
                    <asp:TextBox ID="ELease" runat="server" BackColor="#F6F6F6" BorderWidth="1px" Width="84px"></asp:TextBox>
                    <asp:DropDownList ID="Relation1" runat="server">
                        <asp:ListItem Value="0">等于</asp:ListItem>
                        <asp:ListItem Value="1">不等于</asp:ListItem>
                        <asp:ListItem Value="2">大于</asp:ListItem>
                        <asp:ListItem Value="3" Selected="True">不大于</asp:ListItem>
                        <asp:ListItem Value="4">小于</asp:ListItem>
                        <asp:ListItem Value="5">不小于</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 10%; height: 25px; text-align: center">
                    欠费情况：</td>
                <td class="t2" style="width: 23%; height: 25px; text-align: left">
                    <asp:TextBox ID="ContractState" runat="server" Width="160px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t4" colspan="6" style="height: 29px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Width="220px" />
                    &nbsp;&nbsp;&nbsp;
                    <input id="Button4" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印报表" style="width:180px" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="220px" />
                </td>
            </tr>
        </table>
        <br />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt;">
            <tr>
                <td style="width:39%; text-align: left">
                    &nbsp;编制单位：<asp:Label ID="AName" runat="server"></asp:Label></td>
                <td style="width:31%; text-align: left">
                    报表日期：<asp:TextBox ID="ReportDate" runat="server" BorderWidth="0px" Width="102px"></asp:TextBox></td>
                <td style="width:30%; text-align: right">
                    单位：元&nbsp;&nbsp;</td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" Height="1px" OnRowDataBound="GridView1_RowDataBound"
            PageSize="18" Style="color: black;" BorderColor="#f6f6f6" Width="750px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="cardno" HeaderText="卡片编号">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="resno" HeaderText="资源编号">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="classname" HeaderText="资源类别">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="resname" HeaderText="资源名称">
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="locality" HeaderText="坐落位置">
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="resamount" HeaderText="数量或面积" DataFormatString="{0:f}" HtmlEncode="False">
                    <ItemStyle Width="100px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="计量单位" DataField="resunit">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="查看">
                    <ItemStyle HorizontalAlign="Center" Width="35px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnShow" runat="server" CommandArgument='<%# Bind("id") %>' OnClick="btnShow_Click">卡片</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="变更">
                    <ItemStyle HorizontalAlign="Center"　Width="35px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%# Bind("id") %>' OnClick="btnEdit_Click">变更</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除">
                    <ItemStyle HorizontalAlign="Center" Width="35px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDel" runat="server" CommandArgument='<%# Bind("id") %>' OnClick="btnDel_Click">删除</asp:LinkButton>
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
