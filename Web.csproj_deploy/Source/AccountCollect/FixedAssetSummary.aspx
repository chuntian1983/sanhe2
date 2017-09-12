<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" Inherits="AccountCollect_FixedAssetSummary" Codebehind="FixedAssetSummary.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function setYear(o,v)
{
    var m=eval($(o).value)+v;
    $(o).value=m;
    $("ReportDate").value=$("ReportDate").value.replace(/\d{4}年/,m+"年");
    return false;
}
function setMonth(v)
{
    var rdate=$("ReportDate").value;
    if(v.length==0)
    {
        $("ReportDate").value=rdate.replace(/\d{0,2}月/,"");
        return;
    }
    if(rdate.indexOf("月")==-1)
    {
        $("ReportDate").value=$("ReportDate").value+v+"月";
    }
    else
    {
        $("ReportDate").value=$("ReportDate").value.replace(/\d{2}月/,v+"月");
    }
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width:  <%=TableWidth %>px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="font-size: 16pt; font-family: 隶书">固定资产汇总表</span>&nbsp;</td>
            </tr>
        </table>
        <br />
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; text-align:center">
            <tr>
                <td class="t1" style="width: 10%; text-align: right; height: 28px;">
                    查询日期：</td>
                <td class="t1" style="width: 10%; height: 28px; text-align:center">
                    <asp:ImageButton ID="SMinus" runat="server" ImageUrl="~/Images/jian.gif" />
                    <asp:TextBox ID="SelYear" runat="server" BorderWidth="0px" Width="27px" Height="18px">2009</asp:TextBox>&nbsp;
                    <asp:ImageButton ID="SPlus" runat="server" ImageUrl="~/Images/jia.gif" /></td>
                <td class="t1" style="width: 10%; text-align: right; height: 28px;">
                    汇总单位：</td>
                <td class="t1" style="width: 20%; height: 28px; text-align: left;">
                    <asp:DropDownList ID="CollectUnit" runat="server">
                        <asp:ListItem Value="000000">选择汇总单位</asp:ListItem>
                        <asp:ListItem Value="XXXXXX">一级汇总单位</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t1" style="width: 10%; height: 25px;">
                    科目列表：</td>
                <td class="t2" style="width: 23%; height: 25px; text-align: left;">
                    <asp:DropDownList ID="SubjectList" runat="server">
                    </asp:DropDownList></td>
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
        <table cellpadding="0" cellspacing="0" style="width:  <%=TableWidth %>px; font-size:10pt;">
            <tr>
                <td style="width:43%; text-align: left">
                    &nbsp;编制单位：<asp:Label ID="AName" runat="server"></asp:Label></td>
                <td style="width:27%; text-align: left">
                    报表年月：<asp:TextBox ID="ReportDate" runat="server" BorderWidth="0px" Width="70px"></asp:TextBox></td>
                <td style="width:30%; text-align: right">
                    单位：元&nbsp;&nbsp;</td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CaptionAlign="Left" CssClass="onlyborder"
            ShowHeader="False" Width="750px">
            <PagerSettings Visible="False" />
            <RowStyle Font-Size="10pt" Height="21px" />
        </asp:GridView>
        <!--#include file="../AccountQuery/ReportBottom.aspx"-->
    </div>
    <asp:HiddenField ID="RowsCount" runat="server" Value="0" />
    <asp:HiddenField ID="GAccountList" runat="server" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
