<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" Inherits="Contract_LeaseContractQuery" Codebehind="LeaseContractQuery.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function ShowVoucher(stype,vid)
{
   var ctype="<%=Request.QueryString["ctype"] %>";
   if(stype==0)
   {
       window.showModalDialog("LeaseCardShow.aspx?ctype="+ctype+"&id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=770px;dialogHeight=400px;center=yes;");
   }
   else
   {
       if(ctype=="0")
       {
           window.showModalDialog("../FixedAsset/PrintFACard.aspx?id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=770px;dialogHeight=400px;center=yes;");
       }
       else
       {
           window.showModalDialog("../ResManage/ResourceCardShow.aspx?id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=750px;dialogHeight=400px;center=yes;");
       }
   }
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
                <td class="t1" style="width: 10%; text-align: center">
                    单位名称</td>
                <td class="t1" colspan="3" style="width: 40%; text-align: center">
                    <asp:TextBox ID="ResUnitName" runat="server" BorderWidth="1px" Width="289px"></asp:TextBox></td>
                <td class="t1" style="width: 10%; text-align: center">
                    合同名称</td>
                <td class="t2" style="width: 40%; text-align: center">
                    <asp:TextBox ID="ContractName" runat="server" BorderWidth="1px" Width="289px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t1" style="width: 10%; height: 30px; text-align: center">
                    合同编号</td>
                <td class="t1" style="width: 15%; text-align: center">
                    <asp:TextBox ID="ContractNo" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
                <td class="t1" style="width: 10%; text-align: center">
                    合同类别</td>
                <td class="t1" style="width: 15%; text-align: center">
                    <asp:DropDownList ID="ContractType" runat="server">
                        <asp:ListItem>不限</asp:ListItem>
                        <asp:ListItem>转包</asp:ListItem>
                        <asp:ListItem>转让</asp:ListItem>
                        <asp:ListItem>出租</asp:ListItem>
                        <asp:ListItem>互换</asp:ListItem>
                        <asp:ListItem>入股</asp:ListItem>
                        <asp:ListItem>合作</asp:ListItem>
                        <asp:ListItem>借用</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 10%; height: 30px; text-align: center" id="tempTD" runat="server">
                    资源名称</td>
                <td class="t2" style="text-align: center">
                    <asp:TextBox ID="ResourceName" runat="server" BorderWidth="1px" Width="289px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="t3" style="text-align: center; width:10%; height:30px">
                    起始日期</td>
                <td class="t3" style="text-align: center; width:15%">
                    <asp:TextBox ID="SLease" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
                <td class="t3" style="text-align: center; width:10%">
                    终止日期</td>
                <td class="t3" style="text-align: center; width:15%">
                    <asp:TextBox ID="ELease" runat="server" BorderWidth="1px" Width="100px"></asp:TextBox></td>
                <td class="t4" colspan="2" style="text-align: center; width:50%">
                <asp:Button ID="Button1" runat="server" Text="--  查询 --" Width="100px" OnClick="Button1_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <input id="Button3" onclick="window.open('../PrintWeb.html','','');" type="button" value="--  打印 --" style="width: 100px" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="120px" />
                </td>
            </tr>
        </table>
        <hr style="width: 750px; color:Red; height:2px; text-align:left" />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书" id="ReportTitle" runat="server">资源经济合同查询</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt;">
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
            PageSize="18" Style="color: black;" BorderColor="#f6f6f6" Width="750px">
            <PagerSettings FirstPageText="首页" LastPageText="尾页" Mode="NumericFirstLast" />
            <Columns>
                <asp:BoundField HeaderText="资源名称" DataField="ResourceName">
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ContractNo" HeaderText="合同编号">
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ContractName" HeaderText="合同名称">
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="ContractMoney" HeaderText="合同总金额" DataFormatString="{0:f}" HtmlEncode="False">
                    <ItemStyle Width="90px" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="ContractType" HeaderText="合同类别">
                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="起始日期" DataField="StartLease">
                    <ItemStyle Width="78px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="终止日期" DataField="EndLease">
                    <ItemStyle Width="78px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LeaseHolder" HeaderText="承包人">
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="查看卡片">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <ItemTemplate>
                        <asp:LinkButton ID="btnContract" CommandArgument='<%# Bind("id") %>' runat="server">合同</asp:LinkButton>
                        <asp:LinkButton ID="btnResource" CommandArgument='<%# Bind("ResourceID") %>' runat="server">资源</asp:LinkButton>
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
