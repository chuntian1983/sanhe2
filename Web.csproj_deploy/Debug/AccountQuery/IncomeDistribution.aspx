<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccountQuery_IncomeDistribution" Codebehind="IncomeDistribution.aspx.cs" %>

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
    $(cellid).innerHTML="<input type=text id=TempInput onblur=\"_OnBlur('"+cellid+"',this.value);\" value=\""+v+"\" class=TempInput3>";
    $("TempInput").focus();
    $("TempInput").select();
}
function _OnBlur(cellid,v)
{
    $(cellid).innerHTML=v;
}
function SetObjectPos(OParent,offsetObj)
{
    var aTag = document.getElementById(OParent);
    var leftpos=aTag.offsetLeft;
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
    for(var i=1;i<eval($("RowsCount").value);i++)
    {
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(0).innerHTML+"!_1_!";
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(1).innerText+"!_1_!";
        $("RowItemText").value+=$("GridView1").rows.item(i).cells.item(2).ItemExpr+"!_0_!";
    }
}
function OnSelChange(v)
{
    $("ReportDate").value=v+"年";
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
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td style="height: 28px; text-align: center">
                    <span style="color:Blue; font-size: 20pt;">收 益 及 收 益 分 配 表</span></td>
            </tr>
        </table>
        <br />
        <!--NoPrintStart-->
        <table cellpadding="0" cellspacing="0" style="width: 750px">
            <tr>
                <td class="t3" style="width: 10%; height: 31px; text-align: center">
                    查询年度：</td>
                <td class="t3" style="width: 10%; height: 31px; text-align: center">
                    <asp:DropDownList ID="SelYear" runat="server">
                    </asp:DropDownList></td>
                <td class="t3" style="width: 9%; text-align: center">
                    <asp:DropDownList ID="ReportType" runat="server">
                    <asp:ListItem Value="0">纵向</asp:ListItem>
                    <asp:ListItem Value="1">横向</asp:ListItem>
                    </asp:DropDownList></td>
                <td class="t4" style="height: 31px; text-align: center">
                    <asp:Button ID="QDataClick" runat="server" OnClick="QDataClick_Click" Text="统计查询" />
                    &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="SaveSheet" runat="server" OnClick="SaveSheet_Click" Text="保存设置" />
                    &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="GetTemplate" runat="server" OnClick="GetTemplate_Click" Text="取用模板库" />
                    &nbsp; &nbsp;&nbsp;
                    <input id="Button6" onclick="window.open('../PrintWeb.html','','');" type="button" value="打印报表" />
                    &nbsp; &nbsp;&nbsp;
                    <asp:Button ID="OutputDataToExcel" runat="server" OnClick="OutputDataToExcel_Click" Text="导出数据至Excel" Width="120px" /></td>
            </tr>
        </table>
        <hr style="width: 750px; color:Red; height:2px; text-align:left" />
        <!--NoPrintEnd-->
        <table cellpadding="0" cellspacing="0" style="width: 750px; font-size:10pt">
            <tr>
                <td style="width:47%; text-align: left">
                    &nbsp; <span style="color: red">编制单位：<asp:Label ID="AName" runat="server"></asp:Label></span></td>
                <td style="width:23%; text-align: left">
                    <span style="color: red"></span><span style="color: blue">
                    <asp:TextBox ID="ReportDate" runat="server" BorderWidth="0px" ForeColor="Red" Width="70px"></asp:TextBox></span></td>
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
    <asp:HiddenField ID="RowsCount" runat="server" Value="20" />
    <asp:HiddenField ID="curRowID" runat="server" />
    <asp:HiddenField ID="curRowIndex" runat="server" />
    <asp:HiddenField ID="RowItemText" runat="server" />
    <asp:HiddenField ID="TempC" runat="server" />
    <div id="ControlDiv" style="border-right: red 1px solid; border-top: red 1px solid;
        display: none; z-index: 1; left: 194px; border-left: red 1px solid; width: 545px;
        border-bottom: red 1px solid; position: absolute; top: 298px; height: 23px; background-color: #f6f6f6;
        text-align: center">
        <asp:TextBox ID="CellText" runat="server" BorderStyle="Solid" BorderWidth="1px" Width="541px"></asp:TextBox>&nbsp;
    </div>
    </form>
</body>
</html>
