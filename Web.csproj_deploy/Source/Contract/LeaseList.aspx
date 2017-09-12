<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" Inherits="Contract_LeaseList" Codebehind="LeaseList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function LeasePay(lid,lstate)
{
    window.showModalDialog("DefinePay.aspx?id="+lid+"&lstate="+lstate+"&g="+(new Date()).getTime(),"","dialogWidth=650px;dialogHeight:325px;center=yes;");
    __doPostBack('Button1','');
    return false;
}
function ShowVoucher(stype,vid)
{
   window.showModalDialog("LeaseCardShow.aspx?ctype=<%=Request.QueryString["ctype"] %>&id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=770px;dialogHeight=400px;center=yes;");
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
                    <span style="font-size: 16pt; font-family: 隶书">合同管理</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; text-align:center">
            <tr>
                <td class="t1" style="width: 10%; height: 25px;">
                    日期类型：</td>
                <td class="t1" style="width: 23%; height: 25px; text-align: left;">
                    <asp:RadioButtonList ID="DateType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">租赁日期</asp:ListItem>
                        <asp:ListItem Value="1">收款日期</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td class="t1" style="width: 10%; height: 25px;" id="TD_Class" runat="server">
                    资源类别：</td>
                <td class="t1" style="width: 23%; height: 25px; text-align: left;">
                    <asp:DropDownList ID="QList" runat="server" Width="171px">
                    </asp:DropDownList></td>
                <td class="t1" style="width: 10%; height: 25px">
                    租赁状态：</td>
                <td class="t2" style="width: 23%; height: 25px; text-align: left;">
                    <asp:DropDownList ID="LeaseState" runat="server">
                        <asp:ListItem Value="000000">不限制</asp:ListItem>
                        <asp:ListItem Value="0">未收款</asp:ListItem>
                        <asp:ListItem Value="1">已收款</asp:ListItem>
                        <asp:ListItem Value="2">已终止</asp:ListItem>
                    </asp:DropDownList></td>
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
                <td class="t1" style="width: 10%; height: 25px" id="TD_No" runat="server">
                    资源编号：</td>
                <td class="t1" style="width: 23%; height: 25px; text-align: left;"><asp:TextBox ID="AssetNo" runat="server" Width="164px"></asp:TextBox></td>
                <td class="t2" style="width: 10%; height: 25px; text-align: center">
                    卡片编号：</td>
                <td class="t2" style="width: 23%; height: 25px; text-align: left;">
                    <asp:TextBox ID="CardID" runat="server" Width="164px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 10%; height: 25px">
                    结束日期：</td>
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
                <td class="t1" style="width: 10%; height: 25px"  id="TD_Name" runat="server">
                    资源名称：</td>
                <td class="t1" style="width: 23%; height: 25px; text-align: left">
                    <asp:TextBox ID="AssetName" runat="server" Width="164px"></asp:TextBox></td>
                <td class="t2" style="width: 10%; height: 25px; text-align: center">
                    承&nbsp;&nbsp;包&nbsp;&nbsp;人：</td>
                <td class="t2" style="width: 23%; height: 25px; text-align: left">
                    <asp:TextBox ID="LeaseHolder" runat="server" Width="164px"></asp:TextBox></td>
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
                    报表日期：<asp:TextBox ID="ReportDate" runat="server" BorderWidth="0px" Width="107px"></asp:TextBox></td>
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
                <asp:BoundField DataField="resourcename" HeaderText="资源名称">
                    <ItemStyle Width="130px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="合同总金额" DataField="contractmoney" HtmlEncode="false" DataFormatString="{0:f}">
                    <ItemStyle Width="75px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="SumRental" HeaderText="已收款总额" HtmlEncode="false" DataFormatString="{0:f}">
                    <ItemStyle Width="75px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="数量或面积" DataField="resamount" HtmlEncode="false" DataFormatString="{0:f}">
                    <ItemStyle Width="75px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="endlease" HeaderText="结束日期">
                    <ItemStyle HorizontalAlign="Center" Width="65px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="承包人" DataField="LeaseHolder">
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="卡片">
                    <ItemStyle HorizontalAlign="Center" Width="35px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnCardShow" runat="server" CommandArgument='<%# Bind("id") %>'>卡片</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="变更">
                    <ItemStyle HorizontalAlign="Center"　Width="35px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%# Bind("id") %>' OnClick="btnEdit_Click">变更</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="收款">
                    <ItemStyle HorizontalAlign="Center" Width="35px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnPay" runat="server" CommandName='<%# Bind("id") %>' CommandArgument='<%# Bind("LeaseState") %>'>查看</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态">
                    <ItemStyle HorizontalAlign="Center" Width="35px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnTurn" runat="server" CommandName='<%# Bind("id") %>' CommandArgument='<%# Bind("LeaseState") %>' OnClick="btnTurn_Click">终止</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="删除">
                    <ItemStyle HorizontalAlign="Center" Width="35px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDel" runat="server" CommandName='<%# Bind("id") %>' CommandArgument='<%# Bind("LeaseState") %>' OnClick="btnDel_Click">删除</asp:LinkButton>
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
