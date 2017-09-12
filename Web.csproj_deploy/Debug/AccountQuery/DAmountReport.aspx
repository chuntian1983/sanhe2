<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountQuery_DAmountReport" Codebehind="DAmountReport.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<link type="text/css" href="../Images/css.css" rel="Stylesheet" />
<script type="text/javascript">
function Number.prototype.str(s){var a=""+this;return s.substring(0,s.length-a.length)+a;}
function $(o){return (typeof(o)=="object")?o:document.getElementById(o);}
function OnRowClick(row,rowid)
{
    if(row==0){return;}
    if($("curRowID").value!="")
    {
        $($("curRowID").value).style.backgroundColor="";
    }
    $(rowid).style.backgroundColor="#f6f6f6";
    $("curRowID").value=rowid;
    $("curRowIndex").value=row;
}
function OnCell0Click(cellid)
{
    $("ControlDiv").style.display="";
    var v=$(cellid).innerHTML.replace(/&nbsp;&nbsp;/g," ");
    if(v=="&nbsp;"||v==" "){v="";}
    $("CellText").value=v;
    $("CellText").select();
    $("TempC").value=cellid;
    SetObjectPos(cellid,"ControlDiv");
}
function wCellText()
{
    $("ControlDiv").style.display="none";
    $($("TempC").value).innerHTML=$("CellText").value.replace(/ /g,"&nbsp;&nbsp;");
}
function OnCell1Click(cellid)
{
    $("ControlDiv").style.display="none";
    var returnV=window.showModalDialog("../AccountInit/DefineExpr.aspx?f=1&g="+(new Date()).getTime(),$(cellid).ItemExpr,"dialogWidth=402px;dialogHeight:359px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        $(cellid).ItemExpr=returnV;
    }
}
function OnCell2Click(cellid)
{
    if($(cellid).innerHTML.indexOf("TempInput")!=-1){return;}
    var v=$(cellid).innerText;
    if(v==" "){v="";}
    $(cellid).innerHTML="<input type=text id=TempInput onblur=\"$('"+cellid+"').innerHTML=this.value;\" value=\""+v+"\" class=TempInput2>";
    $("TempInput").focus();
    $("TempInput").select();
}
function SelSubject()
{
    $("ControlDiv").style.display="none";
    var returnV=window.showModalDialog("../AccountInit/SelAllSubject.aspx?t=0&g="+(new Date()).getTime(),"","dialogWidth=402px;dialogHeight:387px;center=yes;");
    if(typeof(returnV)!="undefined")
    {
        $($("TempC").value).innerText=returnV[0]+"["+returnV[1]+"]";
    }
}
function DelRow()
{
    $("ControlDiv").style.display="none";
    if($("curRowIndex").value=="")
    {
        alert("当删除行背景色更改后为选定状态，此时才可执行删除操作！\n\n请单击需要删除行。");
        return false;
    }
    if(!confirm("您确定删除此行数据吗？")){return false;}
    return true;
}
function SetObjectPos(OParent,offsetObj)
{
    var aTag = document.getElementById(OParent);
    var leftpos=aTag.offsetLeft+1;
    var toppos=aTag.offsetTop;
    while(aTag = aTag.offsetParent)
    {
        leftpos += aTag.offsetLeft;
        toppos += aTag.offsetTop;
    }
    document.getElementById(offsetObj).style.left=leftpos;
    document.getElementById(offsetObj).style.top=toppos;
}
function _OnSubmit()
{
    $("RowItemText").value="";
    $("GridViewRowFlag").value=$("GridView1").GridViewRowFlag;
    var RowsCount=eval($("RowsCount").value);
    if($("GridView1").CountRowIndex!="")
    {
        RowsCount=eval($("GridView1").CountRowIndex);
    }
    for(var i=1;i<=RowsCount;i++)
    {
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(0).innerHTML+"!_1_!";
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(1).innerHTML+"!_1_!";
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(2).ItemExpr+"!_1_!";
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(3).ItemExpr+"!_0_!";
    }
    $("RowItemText").value+="|!_0_!";
    for(var i=1;i<=RowsCount;i++)
    {
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(4).innerHTML+"!_1_!";
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(5).innerHTML+"!_1_!";
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(6).ItemExpr+"!_1_!";
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(7).ItemExpr+"!_0_!";
    }
    $("RowItemText").value+="|!_0_!";
    for(var i=RowsCount+1;i<eval($("RowsCount").value);i++)
    {
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(0).innerHTML+"!_1_!";
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(1).innerHTML+"!_1_!";
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(2).ItemExpr+"!_1_!";
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(3).ItemExpr+"!_0_!";
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(4).innerHTML+"!_1_!";
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(5).innerHTML+"!_1_!";
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(6).ItemExpr+"!_1_!";
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(7).ItemExpr+"!_0_!";
    }
    $("RowItemText").value=$("RowItemText").value.replace(/&nbsp;&nbsp;/g," ");
    $("RowItemText").value=$("RowItemText").value.replace(/&nbsp;/g,"");
}
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
document.onclick = function onClick(ev)
{
     ev = ev || window.event;   
     var target = ev.target || ev.srcElement;
     var oid=target.getAttribute("id");
     if(("ControlDiv,CellText,"+$("TempC").value).indexOf(oid)==-1||oid=="")
     {
         if($("ControlDiv").style.display==""){wCellText();}
         $("ControlDiv").style.display="none";
     }
}
</script>
</head>
[dll]
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span id="ReportTitle" style="color:Blue; font-size: 20pt;" runat="server"></span></td>
            </tr>
        </table>
        <br />
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t3" style="height: 31px; text-align: center; width: 12%">
                    查询日期：</td>
                <td class="t3" style="height: 31px; text-align: center; width: 20%">
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
                <td style="height: 31px; text-align: center" class="t4">
                <asp:Button ID="QDataClick" runat="server" Text="统计查询" OnClick="QDataClick_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="SaveSheet" runat="server" Text="保存设置" OnClick="SaveSheet_Click" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="GetTemplate" runat="server" Text="取用模板库" OnClick="GetTemplate_Click" />&nbsp;&nbsp;&nbsp;
                <input id="Button4" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印报表" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="120px" /></td>
        </tr>
        </table>
        <hr style="width: 750px; color:Red; height:2px; text-align:left" />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt;">
            <tr>
                <td style="width:45%; text-align: left">
                    &nbsp; <span style="color: red">编制单位：<asp:Label ID="AName" runat="server"></asp:Label></span></td>
                <td style="width:25%; text-align: left">
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
            </asp:GridView><table cellpadding="0" cellspacing="0" style="width: 750px">
        </table>
        dllhashvalue=
        <!--#include file="ReportBottom.aspx"-->
    </div>
    <asp:HiddenField ID="RowsCount" runat="server" Value="20" />
    <asp:HiddenField ID="curRowID" runat="server" />
    <asp:HiddenField ID="curRowIndex" runat="server" />
    <asp:HiddenField ID="RowItemText" runat="server" />
    <asp:HiddenField ID="GridViewRowFlag" runat="server" />
    <asp:HiddenField ID="TempC" runat="server" />
    <div id="ControlDiv" style="border-right: red 1px solid; border-top: red 1px solid;
        display: none; z-index: 1; left: 194px; border-left: red 1px solid; width: 332px;
        border-bottom: red 1px solid; position: absolute; top: 298px; height: 22px; background-color: #f6f6f6;
        text-align: center">
        <asp:TextBox ID="CellText" runat="server" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>&nbsp;
        <input id="Button2" onclick="wCellText();" style="width: 60px; height: 20px" type="button" value="录入文本" />&nbsp;
        <input id="Button1" onclick="SelSubject();" style="width: 60px; height: 20px" type="button" value="提取科目" />&nbsp;
        <input id="Button3" onclick="$('ControlDiv').style.display='none';" style="height: 20px" type="button" value="关闭" />
    </div>
    </form>
</body>
</html>
