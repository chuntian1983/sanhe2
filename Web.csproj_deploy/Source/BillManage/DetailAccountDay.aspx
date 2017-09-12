<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountQuery_DetailAccountDay" Codebehind="DetailAccountDay.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript" src="../Images/SelDate/getcalendar.js"></script>
<script type="text/javascript">
function Number.prototype.str(s){var a=""+this;return s.substring(0,s.length-a.length)+a;}
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function SetObjectPos(OParent)
{
    $("CloseButton").style.display="";
    $("SubjectList").style.display="";
    var aTag = document.getElementById(OParent);
    var leftpos=aTag.offsetLeft+1;
    var toppos=aTag.offsetTop+aTag.offsetHeight;
    while(aTag = aTag.offsetParent)
    {
        leftpos += aTag.offsetLeft;
        toppos += aTag.offsetTop;
    }
    $("SubjectList").style.left=leftpos;
    $("SubjectList").style.top=toppos;
    $("CloseButton").style.left=leftpos+$("SubjectList").offsetWidth-$("CloseButton").offsetWidth-20;
    $("CloseButton").style.top=toppos+4;
}
function SelAMonth(v)
{
    var a=eval($("QEMonth").value);
    for(var i=$("QEMonth").options.length-1;i>=0;i--)
    {
        $("QEMonth").options.remove(i);
    }
    for(var i=eval(v);i<=12;i++)
    {
        $("QEMonth").options.add(new Option(i.str("00")+"月",i.str("00")));
        if(a==i){$("QEMonth").selectedIndex=$("QEMonth").options.length-1;}
    }
}
function OnTreeClick(v)
{
    $("QSubjectNo").value=v;
    $("SubjectList").style.display="none";
    $("CloseButton").style.display="none";
}
function ShowVoucher(vid)
{
   window.showModalDialog("../BillManage/JournalShow.aspx?flag="+$("HidSubjectNo").value+"&id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=750px;dialogHeight:490px;center=yes;");
}
function SelSubject()
{
    var returnV=window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=0&filter=102&g="+(new Date()).getTime(),"","dialogWidth=402px;dialogHeight:387px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        $("QSubjectNo").value=returnV[0]+"["+returnV[1]+"]";
        $("HidSubjectNo").value=returnV[1];
    }
}
function submitQ()
{
    if($("QSDay").value.substring(0,4)!=$("QEDay").value.substring(0,4))
    {
        alert("不能跨年度查询日记账！")
        return false;
    }
    if($("QSubjectNo").value=="")
    {
        alert("请选择或输入需要查询的科目！")
        return false;
    }
    return true;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t1" style="width: 12%; text-align: center">
                    查询日期：</td>
                <td class="t1" style="width: 32%; text-align: center;">
                    <asp:TextBox ID="QSDay" runat="server" Width="95px"></asp:TextBox>&nbsp; -&nbsp;
                    <asp:TextBox ID="QEDay" runat="server" Width="95px"></asp:TextBox></td>
                <td class="t1" style="width: 12%; text-align: center">
                    选择科目：</td>
                <td class="t1" style="width: 35%">
                    <asp:TextBox ID="QSubjectNo" runat="server" Width="255px"></asp:TextBox></td>
                <td class="t2" style="width: 9%; text-align: center">
                    <asp:DropDownList ID="ReportType" runat="server">
                    <asp:ListItem Value="0">纵向</asp:ListItem>
                    <asp:ListItem Value="1">横向</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="t4" colspan="5" style="height: 31px; text-align: center">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Width="183px" />
                    &nbsp;&nbsp;
                    <input id="Button2" onclick="window.open('../PrintWeb.html','','');" style="width:180px" type="button" value="打印报表" />
                    &nbsp;&nbsp;
                    <input id="Button4" onclick="location.href='PrintDetail.aspx';" style="width:180px" type="button" value="连续打印" />
                    &nbsp;&nbsp;
                    <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="150px" /></td>
            </tr>
        </table>
        <hr style="width: 750px; color:Red; height:2px; text-align:left" />
        <!--NoPrintEnd-->
        <asp:HiddenField ID="GridViewWidth" runat="server" Value="750" />
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="vertical-align:top; height: 235px;">
                    <asp:PlaceHolder ID="ShowPageContent" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
        </table>
     </div>
     <asp:HiddenField ID="MSubjectTitle" runat="server" />
     <asp:HiddenField ID="DSubjectTitle" runat="server" />
     <asp:HiddenField ID="HidSubjectNo" runat="server" />
    </form>
</body>
</html>
