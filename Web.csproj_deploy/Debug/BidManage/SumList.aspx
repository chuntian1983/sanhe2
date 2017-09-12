<%@ Page Language="C#" AutoEventWireup="true" Inherits="BidManage_SumList" Codebehind="SumList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script language="javascript" type="text/javascript" src="../Images/My97DatePicker/WdatePicker.js" defer="defer"></script>
<script type="text/javascript">
function $(o) { return (typeof (o) == "object") ? o : document.getElementById(o); }
</script>
</head>
<body style="margin:0px;text-align:center">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width:750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">村民代表会议</span>&nbsp;
                </td>
            </tr>
        </table>
        <!--NoPrintStart-->
        <br />
        <table cellpadding="0" cellspacing="0" style="width:750px">
            <tr>
                <td class="t1" style="width: 12%; height: 29px; text-align: center">
                    项目来源：</td>
                <td class="t1" style="width: 31%; height: 29px; text-align: left">
                    &nbsp;
                    <asp:DropDownList ID="ProFrom" runat="server">
                        <asp:ListItem Value="-">全部</asp:ListItem>
                        <asp:ListItem Value="0">市级</asp:ListItem>
                        <asp:ListItem Value="1">镇级</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 12%; height: 29px; text-align: center">
                    &nbsp;</td>
                <td class="t2" style="width: 45%; height: 29px; text-align: left;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="t1" style="width: 12%; height: 29px; text-align: center">
                    查询日期：</td>
                <td class="t1" style="width: 31%; height: 29px; text-align: left">
                    &nbsp;
                    <asp:TextBox ID="SDate" runat="server" Width="80px"></asp:TextBox>&nbsp;~
                    <asp:TextBox ID="EDate" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="t1" style="width: 12%; height: 29px; text-align: center">
                    项目类型：</td>
                <td class="t2" style="width: 45%; height: 29px; text-align: left;">
                    &nbsp;
                    <asp:DropDownList ID="ProjectType" runat="server">
                        <asp:ListItem Selected="True" Value="-">全部</asp:ListItem>
                        <asp:ListItem>建设项目</asp:ListItem>
                        <asp:ListItem>出租项目</asp:ListItem>
                        <asp:ListItem>出售项目</asp:ListItem>
                        <asp:ListItem>发包项目</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="t1" style="height: 29px; text-align: center">
                    中标金额：</td>
                <td class="t1" style="text-align: left">
                    &nbsp;
                    <asp:TextBox ID="BidMoney" runat="server" Width="80px"></asp:TextBox>元以上</td>
                <td class="t1" style="text-align: center">
                    是否到期：</td>
                <td class="t2" style="text-align: left;">
                    <asp:RadioButtonList ID="DueType" runat="server" RepeatDirection="Horizontal" Width="200px">
                        <asp:ListItem Selected="True" Value="-">全部</asp:ListItem>
                        <asp:ListItem Value="1">已到期</asp:ListItem>
                        <asp:ListItem Value="0">未到期</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="t4" style="height: 35px; text-align: center" colspan="4">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="- 查询 -" Width="108px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button3" onclick="window.open('../PrintWeb.html','','');" type="button" style="width:120px;" value="打印报表" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="- 导出数据至Excel -" Width="150px" />
                    <asp:LinkButton ID="doPostBackEvent" runat="server"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <br />
        <!--NoPrintEnd-->
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" Height="1px" OnRowDataBound="GridView1_RowDataBound"
            PageSize="10" Style="color: navy" Width="750px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="level" HeaderText="序号">
                    <ItemStyle Width="40px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="townname" HeaderText="乡镇">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="accountname" HeaderText="村街">
                    <ItemStyle Width="90px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ProjectName" HeaderText="项目名称">
                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ProjectType" HeaderText="项目类型">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="BiaoDiMoeny" HeaderText="标底金额" DataFormatString="{0:f}" HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="BidMoney" HeaderText="中标金额" DataFormatString="{0:f}" HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="节约开支">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="增收金额">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="EDate" HeaderText="履约情况">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
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
            <RowStyle Font-Size="10pt" Height="25px" BackColor="#ffffff" />
            <PagerStyle BackColor="White" ForeColor="Olive" />
            <HeaderStyle BackColor="#D1E0F5" BorderColor="Red" Font-Size="10pt" Height="25px" ForeColor="Navy" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
