<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" Inherits="Contract_LeaseContractDetail" Codebehind="LeaseContractDetail.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function setYear(ctl,v)
{
    var m=eval($(ctl).value)+v;
    if(ctl=="SYear")
    {
        if(eval($("EYear").value)<m){$("EYear").value=m;}
    }
    else
    {
        if(eval($("SYear").value)>m){$("SYear").value=m;}
    }
    $(ctl).value=m;
    return false;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px;">
            <tr>
                <td class="t3" style="text-align: center; width:10%; height:30px">
                    起始年度</td>
                <td class="t3" style="text-align: center; width:15%">
                    <asp:ImageButton ID="SMinus" runat="server" ImageUrl="~/Images/jian.gif" />
                    <asp:TextBox ID="SYear" runat="server" BorderWidth="0px" Width="27px" Height="18px">2009</asp:TextBox>&nbsp;&nbsp;
                    <asp:ImageButton ID="SPlus" runat="server" ImageUrl="~/Images/jia.gif" />
                </td>
                <td class="t3" style="text-align: center; width:10%">
                    终止年度</td>
                <td class="t3" style="text-align: center; width:15%">
                    <asp:ImageButton ID="EMinus" runat="server" ImageUrl="~/Images/jian.gif" />
                    <asp:TextBox ID="EYear" runat="server" BorderWidth="0px" Width="27px" Height="18px">2009</asp:TextBox>&nbsp;&nbsp;
                    <asp:ImageButton ID="EPlus" runat="server" ImageUrl="~/Images/jia.gif" />
                </td>
                <td class="t4" style="text-align: center; width:50%">
                <asp:Button ID="Button1" runat="server" Text="--  汇总 --" Width="100px" OnClick="Button1_Click" />
                &nbsp;&nbsp;
                <input id="Button3" onclick="window.open('../PrintWeb.html','','');" type="button" value="--  打印 --" style="width: 100px" />
                &nbsp;&nbsp;
                <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="120px" />
                </td>
            </tr>
        </table>
        <hr style="width: 750px; color:Red; height:2px; text-align:left" />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 1004px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书" id="ReportTitle" runat="server">资源经济合同台账</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 1004px; font-size:10pt;">
            <tr>
                <td style="width:42%; text-align: left">
                    &nbsp;编制单位：<asp:Label ID="AName" runat="server"></asp:Label></td>
                <td style="width:28%; text-align: left">
                    报表日期：<asp:TextBox ID="ReportDate" runat="server" BorderWidth="0px" Width="102px"></asp:TextBox></td>
                <td style="width:30%; text-align: right">
                    单位：元&nbsp;&nbsp;</td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CaptionAlign="Left" Height="1px" OnRowDataBound="GridView1_RowDataBound"
            PageSize="18" Style="color: black;" BorderColor="#f6f6f6" Width="1060px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField DataField="ContractNo" HeaderText="合同编号">
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ContractName" HeaderText="合同名称">
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ContractMoney" HeaderText="合同总金额" DataFormatString="{0:f}" HtmlEncode="False">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="ContractYears" HeaderText="合同年限">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ContractType" HeaderText="合同类别">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="起始日期" DataField="StartLease">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="终止日期" DataField="EndLease">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="资源名称" DataField="ResourceName">
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ContractCo" HeaderText="承包人">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ContractContent" HeaderText="合同内容摘要">
                    <ItemStyle Width="200px" />
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
