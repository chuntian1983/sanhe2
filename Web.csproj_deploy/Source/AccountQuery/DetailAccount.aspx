<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountQuery_DetailAccount" Codebehind="DetailAccount.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
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
   window.showModalDialog("../AccountManage/LookVoucher.aspx?id="+vid+"&g="+(new Date()).getTime(),"","dialogWidth=767px;dialogHeight=385px;center=yes;");
}
function SelSubject()
{
    var returnV=window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=0&g="+(new Date()).getTime(),"","dialogWidth=402px;dialogHeight:387px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        $("QSubjectNo").value=returnV[0]+"["+returnV[1]+"]";
        $("SubjectNo").value=returnV[1];
    }
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
                    <asp:DropDownList ID="QYear" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="QSMonth" runat="server">
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
                    </asp:DropDownList>&nbsp; ^^^^&nbsp;
                    <asp:DropDownList ID="QEMonth" runat="server">
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
     <asp:HiddenField ID="SubjectNo" runat="server" />
     <script type="text/javascript">SelAMonth($("QSMonth").value);</script>
    </form>
</body>
</html>
