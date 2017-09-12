﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountCollect_MonitorFinance" Codebehind="MonitorFinance.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function CheckSubmit()
{
    if($("GAccountList").value=="")
    {
      alert("没有可监控单位！");
      return false;
    }
    return true;
}
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
function ShowVoucher(vid,aid)
{
   window.showModalDialog("../AccountManage/LookVoucher.aspx?id="+vid+"&aid="+aid+"&g="+(new Date()).getTime(),"","dialogWidth=767px;dialogHeight=385px;center=yes;");
}
function SelectUnit()
{
    var returnV=window.showModalDialog("UnitList.aspx?g="+(new Date()).getTime(),"","dialogWidth=200px;dialogHeight=380px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        if(returnV[0].length>0)
        {
            $("UnitName").value=returnV[0];
            $("AName").innerText=returnV[0];
            $("GAccountList").value=returnV[1];
        }
    }
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span id="ReportTitle" style="color:green; font-size: 16pt;" runat="server">资金监管</span></td>
            </tr>
        </table>
        <br />
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 10%; height: 30px; text-align: center">
                    监控单位：</td>
                <td class="t1" colspan="2" style="width: 30%; text-align: center">
                    <asp:TextBox ID="UnitName" runat="server" Width="208px"></asp:TextBox></td>
                <td class="t1" style="width: 8%; text-align: center">
                    年月：</td>
                <td class="t1" style="width: 22%; text-align: center">
                    <asp:ImageButton ID="SMinus" runat="server" ImageUrl="~/Images/jian.gif" />
                    <asp:TextBox ID="SelYear" runat="server" BorderWidth="0px" Width="27px" Height="18px">2009</asp:TextBox>&nbsp;
                    <asp:ImageButton ID="SPlus" runat="server" ImageUrl="~/Images/jia.gif" />
                    <asp:DropDownList ID="SelMonth" runat="server">
                        <asp:ListItem Value="">不限</asp:ListItem>
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
                <td class="t1" style="width: 8%; text-align: center">
                    &nbsp;指标：</td>
                <td class="t2" style="width: 22%; text-align: center">
                    <asp:TextBox ID="IndexValue" runat="server">0</asp:TextBox></td>
            </tr>
            <tr>
                <td class="t4" colspan="8" style="text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Width="150px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印报表" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="150px" /></td>
            </tr>
        </table>
        <hr style="width: 750px; color:Red; height:2px; text-align:left" />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt">
            <tr>
                <td style="width:41%; text-align: left">
                    &nbsp; <span style="color: red">编制单位：<asp:Label ID="AName" runat="server"></asp:Label></span></td>
                <td style="width:29%; text-align: left">
                    <span style="color: red">报表年月：</span><span style="color: blue"><asp:TextBox ID="ReportDate"
                        runat="server" BorderWidth="0px" ForeColor="Red" Width="70px"></asp:TextBox></span></td>
                <td style="width:30%; text-align: right">
                    <span style="color: red">单位：元&nbsp;&nbsp;</span></td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            CaptionAlign="Left" CssClass="onlyborder" Width="750px" ShowHeader="False">
            <PagerSettings Visible="False" />
            <RowStyle Font-Size="10pt" Height="21px" />
        </asp:GridView>
        <!--#include file="../AccountQuery/ReportBottom.aspx"-->
    </div>
    <asp:HiddenField ID="GAccountList" runat="server" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
