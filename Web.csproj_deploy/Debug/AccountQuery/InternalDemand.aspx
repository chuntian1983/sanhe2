<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountQuery_InternalDemand" Codebehind="InternalDemand.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function OnSelChange(v,t)
{
    if(t=="0")
    {
        $("ReportDate").value=$("ReportDate").value.replace(/\d{4}年/,v+"年");
    }
    else
    {
        $("ReportDate").value=$("ReportDate").value.replace(/\d{2}月/,v+"月");
    }
}
function SetSortType(sn)
{
    if($('DataSortName').value==sn)
    {
        if($('DataSortType').value=="asc")
        {
            $('DataSortType').value="desc";
        }
        else
        {
            $('DataSortType').value="asc";
        }
    }
    else
    {
        $('DataSortType').value="asc";
    }
    $('DataSortName').value=sn;
    __doPostBack('QButton','');
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="color:Blue; font-size: 20pt;">内 部 往 来 余 额 表</span></td>
            </tr>
        </table>
        <br />
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="text-align: center; height: 30px;">
                    科目名称：</td>
                <td class="t1" style="height: 30px; text-align: left">
                    <asp:TextBox ID="SubjectName" runat="server"></asp:TextBox></td>
                <td class="t1" style="width: 10%; text-align: center; height: 30px;">
                    选择科目：</td>
                <td class="t2" style="width: 55%; height: 30px; text-align: left">
                    <asp:DropDownList ID="SubjectList" runat="server">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t3" style="width: 10%; height: 30px; text-align: center">
                    查询日期：</td>
                <td class="t3" style="width: 25%; height: 30px; text-align: left;">
                    <asp:DropDownList ID="SelYear" runat="server">
                    </asp:DropDownList><asp:DropDownList ID="SelMonth" runat="server">
                        <asp:ListItem Value="01">01月</asp:ListItem>
                        <asp:ListItem Value="02">02月</asp:ListItem>
                        <asp:ListItem Value="03">03月</asp:ListItem>
                        <asp:ListItem Value="04">04月</asp:ListItem>
                        <asp:ListItem Value="05">05月</asp:ListItem>
                        <asp:ListItem Value="06">06月</asp:ListItem>
                        <asp:ListItem Value="07">07月</asp:ListItem>
                        <asp:ListItem Value="08">08月</asp:ListItem>
                        <asp:ListItem Value="09">09月</asp:ListItem>
                        <asp:ListItem Value="10">10月</asp:ListItem>
                        <asp:ListItem Value="11">11月</asp:ListItem>
                        <asp:ListItem Value="12">12月</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t4" colspan="2" style="height: 30px; text-align: center">
                    <asp:Button ID="QButton" runat="server" Height="25px" OnClick="QButton_Click" Text="查询" Width="150px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button4" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印报表" style="width: 150px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="120px" />
                    <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton></td>
            </tr>
        </table>
        <hr style="width: 750px; color:Red; height:2px; text-align:left" />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size: 10pt">
            <tr>
                <td style="width:41%; text-align: left">
                    &nbsp; <span style="color: red">编制单位：<asp:Label ID="AName" runat="server"></asp:Label></span></td>
                <td style="width:29%; text-align: left">
                    <span style="color: red">报表年月：</span><span style="color: blue"><asp:TextBox ID="ReportDate"
                        runat="server" BorderWidth="0px" ForeColor="Red" Width="109px"></asp:TextBox></span></td>
                <td style="width:30%; text-align: right">
                    <span style="color: red">单位：元&nbsp;&nbsp;</span></td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CaptionAlign="Left" CssClass="onlyborder" Width="750px" ShowHeader="False">
            <PagerSettings Visible="False" />
            <RowStyle Font-Size="10pt" Height="21px" />
        </asp:GridView>
        <!--#include file="ReportBottom.aspx"-->
     </div>
     <asp:HiddenField ID="DataSortType" runat="server" Value="asc" />
     <asp:HiddenField ID="DataSortName" runat="server" Value="F0" />
    </form>
</body>
</html>
