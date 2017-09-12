<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountCollect_IndexMonitorShow1" Codebehind="IndexMonitorShow1.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>指标监控</title>
<base target="_self" />
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
function resetDialogSize()
{
    var ua = navigator.userAgent;
    if(ua.lastIndexOf("MSIE 7.0") == -1)
    {
        var height = document.body.offsetHeight;
        var width = document.body.offsetWidth;
        if(ua.lastIndexOf("Windows NT 5.2") == -1)
        {
            window.dialogHeight=(height+53)+"px";
            window.dialogWidth=(width+6)+"px";
        }
        else
        {
            window.dialogHeight=(height+46)+"px";
            window.dialogWidth=(width+6)+"px";
        }
    }
}
window.onload = resetDialogSize;
</script>
</head>
<body style="text-align:center; width:760px">
    <form id="form1" runat="server">
    <div style="width:750px; text-align:left">
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span id="ReportTitle" style="color:green; font-size: 16pt;" runat="server">指标监控 —— 月累计发生额</span></td>
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
                <td class="t1" style="width: 20%; text-align: center">
                    <asp:ImageButton ID="SMinus" runat="server" ImageUrl="~/Images/jian.gif" />
                    <asp:TextBox ID="SelYear" runat="server" BorderWidth="0px" Width="27px" Height="18px">2009</asp:TextBox>&nbsp;
                    <asp:ImageButton ID="SPlus" runat="server" ImageUrl="~/Images/jia.gif" /></td>
                <td class="t1" style="width: 10%; text-align: center">
                    余额方向：</td>
                <td class="t2" style="width: 22%; text-align: center">
                    <asp:RadioButtonList ID="BalanceType" runat="server" RepeatDirection="Horizontal" Width="135px">
                        <asp:ListItem Selected="True" Value="0">借方</asp:ListItem>
                        <asp:ListItem Value="1">贷方</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td class="t4" colspan="8" style="text-align: center; height:30px">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Width="150px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印报表" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button3" type="button" value="关闭" onclick="window.close();" style="width:100px" /></td>
            </tr>
        </table>
        <hr style="width: 750px; color:Red; height:2px; text-align:left" />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt; color:Red">
            <tr>
                <td style="width: 41%; height: 25px; text-align: left">
                    &nbsp;指标科目：<asp:Label ID="sIndexSubject" runat="server"></asp:Label></td>
                <td style="width: 29%; height: 25px; text-align: left">
                    指标类型：<asp:Label ID="sIndexType" runat="server"></asp:Label></td>
                <td style="width: 30%; height: 25px; text-align: left">
                    指标限额：<asp:Label ID="sIndexValue" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width:41%; text-align: left">
                    &nbsp;<span style="color: red">编制单位：<asp:Label ID="AName" runat="server"></asp:Label></span></td>
                <td style="width:29%; text-align: left">
                    <span style="color: red">报表年月：</span><span style="color: blue"><asp:TextBox ID="ReportDate"
                        runat="server" BorderWidth="0px" ForeColor="Red" Width="70px"></asp:TextBox></span></td>
                <td style="width:30%; text-align: left">
                    <span style="color: red">单位：元&nbsp;&nbsp;</span></td>
            </tr>
        </table>
        <br />
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt; border-bottom:1px solid #FFCCCC">
            <tr style="background-color:#f6f6f6">
                <td class='t1' style='width: 30%; height: 25px; text-align: center'>月份</td>
                <td class='t1' style='width: 35%;'>借方</td>
                <td class='t2' style='width: 35%;'>贷方</td>
            </tr>
            <%=showValue.ToString() %>
        </table>
        <br />
        <!--#include file="../AccountQuery/ReportBottom.aspx"-->
    </div>
    <asp:HiddenField ID="IndexSubject" runat="server" />
    <asp:HiddenField ID="IndexValue" runat="server" />
    <asp:HiddenField ID="GAccountList" runat="server" />
    <asp:Label ID="ExeScript" runat="server"></asp:Label>
    </form>
</body>
</html>
